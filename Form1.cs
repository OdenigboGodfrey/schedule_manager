using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Microsoft.Win32;

namespace ScheduleManager
{
    public partial class Form1 : Form
    {
        //System.Diagnostics.Debug.WriteLine(i)

        private int componentId = 0;
        private int currentTaskId = -1;
        
        //adding new task
        private Dictionary<int, CheckBox> checkBoxList = new Dictionary<int, CheckBox>();
        private Dictionary<int, TextBox> textBoxList = new Dictionary<int, TextBox>();
        private Dictionary<int, Label> labelList = new Dictionary<int, Label>();
        private Dictionary<int, Button> buttonList = new Dictionary<int, Button>();
        private Dictionary<int, NumericUpDown> durationTextBoxList = new Dictionary<int, NumericUpDown>();

        //loading saved tasks
        private Dictionary<int, Label> tasksLabelList = new Dictionary<int, Label>();
        private Dictionary<int, Label> tasksActiveInactiveLabelList = new Dictionary<int, Label>();
        private Dictionary<int, Button> tasksButtonList = new Dictionary<int, Button>();
        private Dictionary<int, Button> tasksActiveStateButtonList = new Dictionary<int, Button>();

        //itnerval TImer history
        private List<string> intervalHistory = new List<string>();

        private double intervalDuration = 0;

        private DBConnection dbCon;

        private bool isInBetweenChecked = false;
        private bool timerActive = false;
        private bool timerRunning = false;

        private string DBName = "schedule_manager";
        private string textBoxName = "taskTextBox_";
        private string durationTextBoxName = "durationTextBox_";
        private string checkBoxName = "taskCheckBox_";
        private string defaultTextBoxText = "Enter Task";
        private string defaultDurationTextBoxText = "Duration";
        private string interval = "";
        private string hour = "hour";
        private string min = "min";
        private string sec = "sec";
        private string intervalLabelText = "Interval Ends";
        private string intervalStartLabelText = "Interval Started";
        private string messageLabelText = "Message: ";

        private Timer timer1;

        DateTime dueTime;

        

        public enum messageType
        {
            negative = -1,
            neutral = 0,
            positive = 1,
            messageBox = 2,
        };

        public Form1()
        {
            InitializeComponent();
            this.init();
        }

        private void init()
        {
            this.initDB();
            DBHelper.checkTables(this.dbCon);
            this.loadInterval();
            this.getTodaysTask();

            //powerModeChanged();

            start_end_button.Text = (!this.timerActive) ? "Start" : "End";
        }



        public void initDB()
        {
            this.dbCon = DBConnection.Instance();
            this.dbCon.DatabaseName = this.DBName;
            if (!dbCon.IsConnect())
            {
                MessageBox.Show("Connection to DB could not be made, Exiting.");
                Environment.Exit(0);
                Application.Exit();
            }
        }

        private void powerModeChanged()
        {
            SystemEvents.PowerModeChanged += (object s, PowerModeChangedEventArgs e) => {
                switch (e.Mode)
                {
                    case PowerModes.Resume:
                        // show application
                        MessageBox.Show("App Resumed");
                        // move from taskbar to main screen 
                        if (FormWindowState.Minimized == this.WindowState)
                        {
                            this.Show();
                            this.WindowState = FormWindowState.Normal;
                        }

                        break;
                    case PowerModes.Suspend:
                        // pause timer
                        this.timerActive = false;

                        start_end_button.Text = "Start";

                        if (timer1 != null)
                        {
                            timer1.Enabled = false;
                        }

                        this.showMessage("System Suspended. Timer Deactived", messageType.positive);
                        break;
                }
            };
        }
        private void loadInterval()
        {
            //get time interval from db
            var data = DBHelper.Get(this.dbCon, "duration", limit: 1, orderBy: "id DESC");
            if(data.Keys.Count > 0)
            {
                this.interval = data[0]["time"];
                this.interval += " " + data[0]["type"];

                timer_label.Text = this.interval;
            }
            else
            {
                showMessage("No saved interval.", messageType.messageBox);
            }
            
        }

        private void getTodaysTask()
        {
            var data = DBHelper.Get(this.dbCon, "tasks", orderBy: "id ASC", whereClause: "created_at = '" + DateTime.Today.ToString("yyyy-MM-dd") + "'");

            if (data.Keys.Count > 0)
            {
                var keys = data.Keys;
                // count the number of children in the active_task control
                int counter = active_task.Controls.Count;


                foreach (var key in keys)
                {
                    
                    int id = Convert.ToInt32(data[key]["id"]);

                    if (tasksButtonList.ContainsKey(id))
                    {
                        //record already exist
                        continue;
                    }

                    counter++;


                    FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
                    //flowLayoutPanel.BackColor = Color.Beige;
                    
                    flowLayoutPanel.Width = 350;
                    flowLayoutPanel.Height = 30;
                    flowLayoutPanel.Tag = data[key]["id"];
                    flowLayoutPanel.Anchor = AnchorStyles.Left;

                    Label taskCounter = new Label() { Margin = new Padding(0, 10, 0, 0), Width = 20 };
                    taskCounter.Text = "(" + counter + ")";

                    Label taskTitle = new Label() { Margin = new Padding(0, 10, 0, 0) };
                    taskTitle.Text = data[key]["task"];

                    Button deactivateButton = new Button();
                    deactivateButton.Text = (data[key]["current"] == "0") ? "Activate" : "Deactivate";
                    deactivateButton.Tag = data[key]["current"];

                    Button turnOffButton = new Button();
                    turnOffButton.Text = (data[key]["active"] == "0") ? "Turn On" : "Turn Off";
                    turnOffButton.Tag = data[key]["active"];

                    //Label activeLabel = new Label() { Margin = new Padding(0, 7, 0, 0) };
                    //activeLabel.Text = (data[key]["current"] == "0") ? "Inactive" : "Active" ;


                    deactivateButton.Click += delegate 
                    {
                        var taskId = data[key]["id"];
                        this.activateTask(Convert.ToInt32(taskId));
                    };

                    turnOffButton.Click += delegate
                    {
                        var taskId = id;
                        var status = this.tasksActiveStateButtonList[taskId].Tag.ToString();
                        status = (status == "0") ? "1" : "0";
                        
                        //deactivate/activate task
                        Tasks.toggleTaskActiveStatus(this.dbCon, taskId, status);

                        this.tasksActiveStateButtonList[taskId].Text = (status == "0") ? "Turn On" : "Turn Off";
                        this.tasksActiveStateButtonList[taskId].Tag = status;

                        // show message to user
                        this.showMessage(status == "1" ? "Task Activated" : "Task Deactivated", messageType.positive);
                    };

                    flowLayoutPanel.Controls.Add(taskCounter);
                    flowLayoutPanel.Controls.Add(taskTitle);
                    flowLayoutPanel.Controls.Add(deactivateButton);
                    flowLayoutPanel.Controls.Add(turnOffButton);
                    //flowLayoutPanel.Controls.Add(activeLabel);

                    //tasksActiveInactiveLabelList.Add(Convert.ToInt32(data[key]["id"]), activeLabel);

                    tasksButtonList.Add(id, deactivateButton);
                    tasksLabelList.Add(id, taskTitle);
                    tasksActiveStateButtonList.Add(id, turnOffButton);

                    active_task.Controls.Add(flowLayoutPanel);

                    //set active
                    if (data[key]["current"] == "1" && this.currentTaskId == -1)
                    {

                        this.activateTask(id);
                    }
                }

                if (this.currentTaskId == -1)
                {
                    // no task was set as default current
                    // make first task default
                    int id = Convert.ToInt32(data[0]["id"]);
                    //this.currentTaskId = id;
                    //this.tasksActiveInactiveLabelList[id].Text = "Active";
                    this.activateTask(id);

                }

                //empty "checkBoxList" as it's used to track newly created tasks
                this.checkBoxList.Clear();
            }
            else
            {
                this.showMessage("No Tasks for the day.", messageType.neutral);
            }
        }

        private void activateTask(int id, string source="button", int oldTaskId = -1)
        {
            Tasks.setCurrentTask(this.dbCon, this.currentTaskId, id);

            //if oldTaskId is not -1, a forced deactivation of said ID would occur
            if (this.currentTaskId > 0)
            {
                int currentId = (oldTaskId == -1) ? this.currentTaskId : oldTaskId;
                
                //    //deactivate previous
                //this.tasksActiveInactiveLabelList[this.currentTaskId].Text = "Inactive";
                this.tasksButtonList[currentId].Text = "Activate";
                this.tasksButtonList[currentId].Tag = "0";
                this.tasksLabelList[currentId].Font = new Font(this.tasksLabelList[currentId].Font, FontStyle.Regular);

            }

            if (oldTaskId == -1)
            {
                //activate current
                this.currentTaskId = id;
                //this.tasksActiveInactiveLabelList[id].Text = "Active";
                this.tasksButtonList[id].Text = "Deactivate";
                this.tasksButtonList[id].Tag = "1";
                this.tasksLabelList[id].Font = new Font(this.tasksLabelList[id].Font, FontStyle.Bold);
                active_task_label.Text = this.tasksLabelList[id].Text;
            }
            


            if (source != "button") this.showMessage("Task Activated.", messageType.positive);

        }

        protected void showMessage(string message, messageType type)
        {
            message_label.Text = messageLabelText + message;
            switch(type)
            {
                case messageType.negative:
                    message_label.ForeColor = Color.Red;
                    break;
                case messageType.neutral:
                    message_label.ForeColor = Color.DarkCyan;
                    break;
                case messageType.positive:
                    message_label.ForeColor = Color.Green;
                    break;
                case messageType.messageBox:
                    MessageBox.Show(message);
                    break;
            }
        }

        private void add_task_Click(object sender, EventArgs e)
        {
            int componentId = this.componentId;

            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
            flowLayoutPanel.Width = 450;
            flowLayoutPanel.Height = 30;
            flowLayoutPanel.Tag = componentId;
            flowLayoutPanel.Anchor = AnchorStyles.Left;

            Label label = new Label();
            label.Width = 50;
            label.Margin = new Padding(0, 5, 0, 0);
            label.Text = "Task " + componentId;
            
            TextBox textBox = new TextBox();
            textBox.Name = this.textBoxName + componentId;
            textBox.Text = this.defaultTextBoxText;

            textBox.GotFocus += delegate {
                if (this.defaultTextBoxText == textBox.Text)
                {
                    textBox.Text = "";
                }  
            };
            textBox.Leave += delegate
            {
                if (textBox.Text == "")
                {
                    textBox.Text = this.defaultTextBoxText;
                }
            };

            NumericUpDown durationTextBox = new NumericUpDown();
            durationTextBox.Name = this.durationTextBoxName + componentId;
            durationTextBox.Text = this.defaultDurationTextBoxText;
            durationTextBox.Width = 60;

            durationTextBox.GotFocus += delegate {
                if (this.defaultDurationTextBoxText == durationTextBox.Text)
                {
                    durationTextBox.Text = "";
                }
            };

            durationTextBox.Leave += delegate
            {
                if (durationTextBox.Text == "")
                {
                    durationTextBox.Text = this.defaultDurationTextBoxText;
                }
            };

            CheckBox checkBox = new CheckBox();
            checkBox.Name = this.checkBoxName + componentId;
            checkBox.Text = "In Between";
            checkBox.CheckedChanged += delegate 
            {
                var id = componentId;
                var keys = this.checkBoxList.Keys;
                
                foreach (var key in keys)
                {
                    if (key != id)
                    {
                        //foreach checkbox in list disable if not selected checkbox
                        this.checkBoxList[key].Enabled = this.isInBetweenChecked;
                    }
                }

                this.isInBetweenChecked = !this.isInBetweenChecked;

            };

            Button removeButton = new Button();
            removeButton.Text = "Remove";
            removeButton.Width = 60;
            removeButton.Click += delegate 
            {
                //MessageBox.Show("Deleting..." + componentId);
                // on remove clear this row of controls
                var id = componentId;
                var row = flowLayoutPanel;

                //var a = MessageBoxButtons.OKCancel;
                var action = MessageBox.Show("Confirm Delete", "Alert", MessageBoxButtons.YesNo);

                if (action == DialogResult.Yes)
                {
                    form_panel.Controls.Remove(row);

                    this.buttonList.Remove(componentId);
                    this.checkBoxList.Remove(componentId);
                    this.labelList.Remove(componentId);
                    this.textBoxList.Remove(componentId);
                    this.durationTextBoxList.Remove(componentId);
                }
                
            };

            flowLayoutPanel.Controls.Add(label);
            flowLayoutPanel.Controls.Add(textBox);
            //flowLayoutPanel.Controls.Add(durationTextBox);
            flowLayoutPanel.Controls.Add(checkBox);
            flowLayoutPanel.Controls.Add(removeButton);


            //add to controls dictionary 
            this.checkBoxList.Add(componentId, checkBox);
            this.labelList.Add(componentId, label);
            this.textBoxList.Add(componentId, textBox);
            this.buttonList.Add(componentId, removeButton);
            this.durationTextBoxList.Add(componentId, durationTextBox);

            form_panel.Controls.Add(flowLayoutPanel);
            
            //increment textboxid
            this.componentId++;
        }

        private void save_Click(object sender, EventArgs e)
        {
            var keys = this.checkBoxList.Keys;
            
            if (keys.Count > 0)
            {
            
                foreach (var key in keys)
                {
                    if (this.textBoxList[key].Text == this.defaultTextBoxText) continue;

                    var dict = new Dictionary<string, string>();
                    dict.Add("task", this.textBoxList[key].Text);
                    dict.Add("in_between", ((this.isInBetweenChecked && this.checkBoxList[key].Checked) ? 1 : 0).ToString());
                    dict.Add("created_at", DateTime.Today.ToString("yyyy-MM-dd"));
                    dict.Add("active", "1");
                    dict.Add("current", "0");
                    if (this.durationTextBoxList[key].Text != this.defaultDurationTextBoxText)
                    {
                        // task has a custom duration
                        //dict.Add("duration", );
                    }


                    DBHelper.Insert(this.dbCon, "tasks", dict);
                    //MessageBox.Show("Task " + task + " In Between " + inBetween.ToString());
                }
                this.showMessage("Task(s) Saved.", messageType.positive);
                this.getTodaysTask();

                form_panel.Controls.Clear();
            }
            else
            {
                this.showMessage("No tasks to save.", messageType.negative);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.dbCon.Close();
        }

        private void ShowCustomDialog(string caption)
        {
            int durationValue = -1;

            Form prompt = new Form();
            prompt.Width = 250;
            prompt.Height = 250;
            prompt.Text = caption;

            Label textLabel = new Label() { Left = 50, Top = 20, Text = "Duration" };
            NumericUpDown inputBox = new NumericUpDown() { Left = 50, Top = 50, Width = 150 };
            Button confirmation = new Button() { Text = "Save", Left = 100, Width = 100, Top = 120 };

            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel() { Left = 50, Top = 70};
            
            flowLayoutPanel.Width = 200;
            flowLayoutPanel.Height = 30;
            flowLayoutPanel.Tag = componentId;
            flowLayoutPanel.Anchor = AnchorStyles.Left;

            CheckBox secCheckBox = new CheckBox();
            secCheckBox.Text = this.sec;
            secCheckBox.Height = 30;
            secCheckBox.Width = 45;
            secCheckBox.Checked = false;

            CheckBox minCheckBox = new CheckBox();
            minCheckBox.Text = this.min;
            minCheckBox.Height = 30;
            minCheckBox.Width = 45;
            minCheckBox.Checked = true;
            

            CheckBox hourCheckBox = new CheckBox();
            hourCheckBox.Text = this.hour;
            hourCheckBox.Height = 30;
            hourCheckBox.Width = 50;

            secCheckBox.Click += delegate
            {
                hourCheckBox.Checked = false;
                minCheckBox.Checked = false;
                secCheckBox.Checked = true;
            };

            minCheckBox.Click += delegate 
            {
                hourCheckBox.Checked = false;
                minCheckBox.Checked = true;
                secCheckBox.Checked = false;
            };


            hourCheckBox.Click += delegate 
            {
                hourCheckBox.Checked = true;
                minCheckBox.Checked = false;
                secCheckBox.Checked = false;
            };

            confirmation.Click += (sender, e) =>
            {
                if (!(inputBox.Value > 0))
                {
                    MessageBox.Show("Invalid Value");
                    return;
                }

                var data = new Dictionary<string, string>();
                data.Add("time", inputBox.Value.ToString());
                data.Add("created_at", DateTime.Today.ToString("yyyy-MM-dd"));
                data.Add("type", (minCheckBox.Checked) ? this.min : this.hour);
                //save to db
                DBHelper.Insert(this.dbCon, "duration", data);
                //update timer

                //close form
                prompt.Close();
            };

            
            flowLayoutPanel.Controls.Add(secCheckBox);
            flowLayoutPanel.Controls.Add(minCheckBox);
            flowLayoutPanel.Controls.Add(hourCheckBox);


            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(inputBox);
            prompt.Controls.Add(flowLayoutPanel);

            prompt.FormClosing += (s, ev) => 
            {
                if (ev.CloseReason == CloseReason.UserClosing)
                {
                    durationValue = -1;
                }
                
            };

            inputBox.ValueChanged += (s, ev) =>
            {
                
                durationValue = (int) inputBox.Value;
            };

            prompt.ShowDialog();
        }

        private void change_timer_Click(object sender, EventArgs e)
        {
            this.ShowCustomDialog("caption");
        }

        public double ConvertSecondsToMilliseconds(double seconds)
        {
            return TimeSpan.FromSeconds(seconds).TotalMilliseconds;
        }

        public double ConvertMinutesToMilliseconds(double minutes)
        {
            return TimeSpan.FromMinutes(minutes).TotalMilliseconds;
        }

        public double ConvertHoursToMilliseconds(double hours)
        {
            return TimeSpan.FromHours(hours).TotalMilliseconds;
        }

        private int getNextTaskId()
        {
            //get list of all ids 
            var ids = this.tasksLabelList.Keys.ToList();
            int indexOfCurrentId = ids.IndexOf(this.currentTaskId);
            int nextId = ids[0];

            if ((indexOfCurrentId + 1) < ids.Count)
            {
                nextId = ids[(indexOfCurrentId + 1)];
            }

            return nextId;
        }

        private void intervalCheck()
        {
            if (this.currentTaskId > 0)
            {
                // move from taskbar to main screen 
                if (FormWindowState.Minimized == this.WindowState)
                {
                    this.Show();
                    this.WindowState = FormWindowState.Normal;
                }
                

                // update db un-set current active
                var updateData = new Dictionary<string, string>();
                updateData.Add("current", "0");

                DBHelper.Update(this.dbCon, "tasks", updateData, "id=" + this.currentTaskId);

                // update db set current active to next
                int nextId = getNextTaskId();

                for (; ; )
                {
                    if (this.tasksActiveStateButtonList[nextId].Tag.ToString() == "0")
                    {
                        // make checks, next task is deactivated skip
                        this.activateTask(-1, oldTaskId: this.currentTaskId);
                        this.currentTaskId = nextId;
                        nextId = getNextTaskId();
                    }
                    else
                    {
                        break;
                    }
                }


                updateData = new Dictionary<string, string>();
                updateData.Add("current", "1");

                DBHelper.Update(this.dbCon, "tasks", updateData, "id=" + nextId);
                this.activateTask(nextId, "interval");

                // update timer properties
                this.setDueTime();
            }
            else
            {
                this.showMessage("Current active task not set.", messageType.negative);
            }
        }

        private void setDueTime()
        {
            var interval = this.interval.Split(' ');
            double intervalDuration = Convert.ToInt32(interval[0]);

            // for due time
            dueTime = DateTime.Now;
            var startingTime = DateTime.Now.ToShortTimeString();

            if (this.timerActive)
            {
                timer_start_label.Text = this.intervalStartLabelText + " - " + startingTime;
                due_time_label.Text = this.intervalLabelText + " - " + dueTime.ToShortTimeString();
            }
            else
            {
                timer_start_label.Text = this.intervalStartLabelText;
                due_time_label.Text = this.intervalLabelText;

                return;
            }


            if (interval[1] == this.hour)
            {
                dueTime = DateTime.Now.AddHours(Convert.ToInt32(intervalDuration));
                intervalDuration = Convert.ToInt32(this.ConvertHoursToMilliseconds(intervalDuration));
                //due_time_label.Text = dueTime.ToShortTimeString();
                    
            }

            if (interval[1] == this.min)
            {
                dueTime = DateTime.Now.AddMinutes(Convert.ToInt32(intervalDuration));
                intervalDuration = Convert.ToInt32(this.ConvertMinutesToMilliseconds(intervalDuration));

                //if (this.timerActive)
                //{
                //    timer_start_label.Text = this.intervalStartLabelText + " - " + startingTime;
                //    due_time_label.Text = this.intervalLabelText + " - " + dueTime.ToShortTimeString();
                //}
                //else
                //{
                //    timer_start_label.Text = this.intervalStartLabelText;
                //    due_time_label.Text = this.intervalLabelText;
                //}
                    
            }

            if (interval[1] == this.sec)
            {
                dueTime = DateTime.Now.AddSeconds(Convert.ToInt32(intervalDuration));
                intervalDuration = Convert.ToInt32(this.ConvertSecondsToMilliseconds(intervalDuration));

                //if (this.timerActive)
                //    due_time_label.Text = this.intervalLabelText + " - " + dueTime.ToShortTimeString();
                //else
                //    due_time_label.Text = this.intervalLabelText;
            }

            

            this.intervalDuration = intervalDuration;

            if (this.timerActive)  intervalHistory.Add(string.Format("Started At: {0} - Ending At: {1}", startingTime, dueTime.ToShortTimeString()));

            //update timer interval
            if (timer1 != null) timer1.Interval = Convert.ToInt32(this.intervalDuration);

        }

        private void pause_button_Click(object sender, EventArgs e)
        {
            //if timer is running -> get length to next stop
            //-on resume timer is set to run till end of remaining time
            //-on end of remaining time, reset interval to default from db

            if (this.timerActive)
            {
                var currentDateTime = DateTime.Now;
                //get difference between now and dueTime
                var dateTimeDiff = dueTime.Subtract(currentDateTime);
                showMessage(dateTimeDiff.TotalMinutes.ToString(), messageType.neutral);
                this.timerActive = false;
                timer1.Stop();
                timer1.Enabled = false;
                
            }
        }

        private void start_end_button_Click(object sender, EventArgs e)
        {
            this.timerActive = !this.timerActive;
            
            start_end_button.Text = (!this.timerActive) ? "Start" : "End";

            setDueTime();

            //Convert.ToInt32(intervalDuration)
            timer1 = new Timer
            {
                Interval = Convert.ToInt32(intervalDuration)
            };

            if (this.timerActive)
            {
                timer1.Start();
                timer1.Enabled = true;
                this.showMessage("Timer Active", messageType.neutral);
            }
            else
            {
                timer1.Stop();
                timer1.Enabled = false;
                
                this.showMessage("Timer Deactived", messageType.positive);
            }

            int counter = 0;
            timer1.Tick += new EventHandler((s, ev) =>
            {
                
                if (!this.timerActive)
                {
                    timer1.Stop();
                    MessageBox.Show("Timer Stopped");
                }
                else
                {
                    //if ()
                    //{

                    //}
                    this.intervalCheck();
                    MessageBox.Show("Timer Interval Reached");
                }

                counter++;
            });
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                mynotifyicon.BalloonTipText = active_task_label.Text;
                mynotifyicon.Visible = true;
                mynotifyicon.ShowBalloonTip(500);

                this.ShowInTaskbar = true;

                this.Hide();
            }

            else if (FormWindowState.Normal == this.WindowState)
            {
                mynotifyicon.Visible = false;
                this.ShowInTaskbar = false;
            }
        }

        private void mynotifyicon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        private void mynotifyicon_MouseMove(object sender, MouseEventArgs e)
        {
            mynotifyicon.BalloonTipText = active_task_label.Text;
        }

        private void mynotifyicon_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void interval_history_Click(object sender, EventArgs e)
        {
            Form prompt = new Form();
            prompt.Width = 250;
            prompt.Height = 250;
            prompt.Text = "Interval History";

            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel() { Top = 10 };

            flowLayoutPanel.Width = 250;
            flowLayoutPanel.Height = 30;
            flowLayoutPanel.Tag = componentId;
            flowLayoutPanel.Anchor = AnchorStyles.Bottom;
            flowLayoutPanel.Dock = DockStyle.Fill;

            intervalHistory.ForEach((item) => 
            {
                Label label = new Label() { Text = item, Width=230 };

                flowLayoutPanel.Controls.Add(label);
            });

            prompt.Controls.Add(flowLayoutPanel);

            prompt.ShowDialog();
        }

        
    }

    
}
