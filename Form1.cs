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
        //id for task to be loaded in between intervals
        private int inBetweenId = -1;
        //id for task loaded before in-between task
        private int taskIdBeforeInBetweenId = -1;
        
        //duration value from setting timer
        int durationValue { set => durationValue = value; get => getDurationValue(durationValue); }

        //adding new task
        private Dictionary<int, CheckBox> checkBoxList = new Dictionary<int, CheckBox>();
        private Dictionary<int, TextBox> textBoxList = new Dictionary<int, TextBox>();
        private Dictionary<int, Label> labelList = new Dictionary<int, Label>();
        private Dictionary<int, Button> buttonList = new Dictionary<int, Button>();
        private Dictionary<int, Button> durationTextButtonList = new Dictionary<int, Button>();
        private Dictionary<int, Dictionary<string, string>> durationList = new Dictionary<int, Dictionary<string, string>>();

        //loading saved tasks
        private Dictionary<int, Label> tasksLabelList = new Dictionary<int, Label>();
        private Dictionary<int, Label> tasksActiveInactiveLabelList = new Dictionary<int, Label>();
        private Dictionary<int, Button> tasksButtonList = new Dictionary<int, Button>();
        private Dictionary<int, Button> tasksActiveStateButtonList = new Dictionary<int, Button>();
        private Dictionary<int, string> taskDurationList = new Dictionary<int, string>();

        //itnerval TImer history
        private List<string> intervalHistory = new List<string>();

        private double intervalDuration = 0;

        private DBConnection dbCon;

        private bool isInBetweenChecked = false;
        // lifetime has an in betweeen task
        private bool hasInBetween = false;
        // toggle in between status
        private bool inBetweenToggleStatus = false;
        private bool timerActive = false;
        private bool timerRunning = false;

        private string DBName = "schedule_manager";
        private string textBoxName = "taskTextBox_";
        private string durationTextBoxName = "durationTextBox_";
        private string checkBoxName = "taskCheckBox_";
        private string defaultTextBoxText = "Enter Task";
        private string defaultDurationTextBoxText = "Duration";
        private string defaultInterval = "";
        private string interval = "";
        private string hour = "hour";
        private string min = "min";
        private string sec = "sec";
        private string intervalLabelText = "Interval Ends";
        private string intervalStartLabelText = "Interval Started";
        private string messageLabelText = "Message: ";
        //table names
        private string duration_table = "duration";
        private string tasks_table = "tasks";
        private string date_time_diff = "date_time_diff";

        private Timer timer1;

        //due time for task
        DateTime dueTime;
        //time difference for current task till due time
        TimeSpan dateTimeDiff;


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

            powerModeChanged();

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
                        var data = DBHelper.Get(this.dbCon, this.date_time_diff, limit: 1, orderBy: "id DESC", whereClause: "active=0");

                        if (data.Keys.Count > 0)
                        {
                            // previous timer record exists
                            pause_button.Text = "Resume";
                        }
                        
                        
                        // move from taskbar to main screen 
                        if (FormWindowState.Minimized == this.WindowState)
                        {
                            this.Show();
                            this.WindowState = FormWindowState.Normal;
                        }

                        break;
                    case PowerModes.Suspend:
                        
                        if (this.timerRunning)
                        {
                            // pause timer only if timer isnt paused already
                            pause_button.Text = "Pause";
                            this.pauseTimer();

                            this.showMessage("System Suspended. Timer Paused.", messageType.positive);
                        }
                        

                        break;
                }
            };
        }

        private void loadInterval()
        {
            //get time interval from db
            var data = DBHelper.Get(this.dbCon, this.duration_table, limit: 1, orderBy: "id DESC", whereClause: "task_id is null");
            if(data.Keys.Count > 0)
            {
                this.defaultInterval = data[0]["time"] + " " + data[0]["type"];
                this.changeTimerLabelText(data[0]["time"], data[0]["type"]);
            }
            else
            {
                //save default itnerval timer as 45 mins
                var type =  this.min;
                var time = "45";
                var createdAt = DateTime.Today.ToString("yyyy-MM-dd");
                this.saveIntervalTimer(time, createdAt, type);

                showMessage("No saved interval.", messageType.messageBox);
            }
            
        }

        private void changeTimerLabelText(string time, string type)
        {
            string interval = time;
            interval += " " + type;

            timer_label.Text = interval;
        }

        private int getDurationValue (int value)
        {
            return value;
        }

        private void getTodaysTask()
        {
            var data = DBHelper.Get(this.dbCon, this.tasks_table, orderBy: "id ASC", whereClause: "created_at = '" + DateTime.Today.ToString("yyyy-MM-dd") + "'");

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

                    if (data[key]["in_between"] == "1")
                    {
                        //tasks for the day has an in-between task
                        this.hasInBetween = true;
                        this.inBetweenId = id;
                        //after first interval run ,toggle in-between
                        
                        //if inbetween task is first (current) task set inBetweenToggleStatus as false
                        //let next task run after in between is run in first interval
                        this.inBetweenToggleStatus = (data[key]["current"] == "1") ? false : true;

                    }
                    //check if task has custom timer
                    var result = DBHelper.Get(this.dbCon, this.duration_table, whereClause: ("task_id=" + id));
                    if (result.Count > 0)
                    {
                        this.taskDurationList.Add(id, result[0]["time"] + " " + result[0]["type"]);
                    }

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

                        this.activateTask(id, "initial");
                    }
                }

                if (this.currentTaskId == -1)
                {
                    // no task was set as default current
                    // make first task default
                    int id = Convert.ToInt32(data[0]["id"]);
                    //this.currentTaskId = id;
                    //this.tasksActiveInactiveLabelList[id].Text = "Active";
                    this.activateTask(id, "initial");

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
                
                //check if current task has custom timer
                // if source is button, set due time
                if (this.taskDurationList.ContainsKey(id))
                {
                    this.interval = this.taskDurationList[id];
                    // if custom timer is == 0, then user reset custom timer for this task to default
                    if (this.getInterval()[0] == "0")
                    {
                        this.interval = "";
                    }
                }
                else
                {
                    this.interval = "";
                }

                if (source is "button")
                {
                    // user manually activated a task, reset timer to match task duration and update timer label
                    this.stopTimer();
                    this.setDueTime();
                    this.startTimer();

                }
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

            Button durationTextButton = new Button();
            durationTextButton.Name = this.durationTextBoxName + componentId;
            durationTextButton.Text = "Set Custom Time";
            durationTextButton.Width = 100;

            durationTextButton.Click += delegate 
            {
                this.ShowCustomDialog("", componentId);
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
                    this.durationTextButtonList.Remove(componentId);
                }
                
            };

            flowLayoutPanel.Controls.Add(label);
            flowLayoutPanel.Controls.Add(textBox);
            flowLayoutPanel.Controls.Add(durationTextButton);
            flowLayoutPanel.Controls.Add(checkBox);
            flowLayoutPanel.Controls.Add(removeButton);


            //add to controls dictionary 
            this.checkBoxList.Add(componentId, checkBox);
            this.labelList.Add(componentId, label);
            this.textBoxList.Add(componentId, textBox);
            this.buttonList.Add(componentId, removeButton);
            this.durationTextButtonList.Add(componentId, durationTextButton);

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
                    
                    DBHelper.Insert(this.dbCon, this.tasks_table, dict);

                    System.Diagnostics.Debug.WriteLine("Task - Inserting " + key + " - " + (this.durationList.ContainsKey(key)) + " - ");
                    if (this.durationList.ContainsKey(key))
                    {
                        // task has a custom duration
                        //get id for current task to save for duration (FK)
                        var result = DBHelper.Get(this.dbCon, this.tasks_table, limit: 1, orderBy: "id DESC");

                        if (result.Count > 0)
                        {
                            var durationDict = new Dictionary<string, string>();
                            durationDict.Add("time", this.durationList[key]["time"]);
                            durationDict.Add("type", this.durationList[key]["type"]);
                            durationDict.Add("created_at", this.durationList[key]["createdAt"]);
                            durationDict.Add("task_id", result[0]["id"]);

                            DBHelper.Insert(this.dbCon, this.duration_table, durationDict);
                        }
                    }
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

        private void ShowCustomDialog(string caption, int taskId=-1)
        {
            int durationValue = -1;

            Form prompt = new Form();
            prompt.Width = 250;
            prompt.Height = 250;
            prompt.Text = caption;

            Label textLabel = new Label() { Left = 50, Top = 20, Text = "Duration" };
            NumericUpDown inputBox = new NumericUpDown() { Left = 50, Top = 50, Width = 150 };

            FlowLayoutPanel buttonFlowLayoutPanel = new FlowLayoutPanel() { Width = 250, Top = 120, Left = 10 };
            buttonFlowLayoutPanel.Anchor = AnchorStyles.Left;

            Button confirmation = new Button() { Text = "Save", Width = 100 };
            Button exitButton = new Button() { Text = "Cancel", Width = 100 };

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
                if (!(inputBox.Value > 0) && taskId == -1)
                {
                    MessageBox.Show("Invalid Value");
                    return;
                }

                var type = (minCheckBox.Checked) ? this.min : (hourCheckBox.Checked) ? this.hour : this.sec;
                var time = inputBox.Value.ToString();
                var createdAt = DateTime.Today.ToString("yyyy-MM-dd");

                if (taskId != -1)
                {
                    // custom timer for a task
                    // create key to task duration list if task doesnt have an existing key
                    // [0 => time, 1 => createdAt, 2 => type]
                    if (!this.durationList.ContainsKey(taskId))
                    {
                        this.durationList.Add(taskId, new Dictionary<string, string>());
                        this.durationList[taskId].Add("time", time);
                        this.durationList[taskId].Add("createdAt", createdAt);
                        this.durationList[taskId].Add("type", type);
                    } 
                    else
                    {
                        //update timer
                        this.durationList[taskId]["time"] = time;
                        this.durationList[taskId]["createdAt"] = createdAt;
                        this.durationList[taskId]["type"] = type;
                    }
                    
                }
                else
                {
                    // default timer for app
                    this.saveIntervalTimer(time, createdAt, type);
                }

                //close form
                prompt.Close();
            };

            exitButton.Click += delegate
            {

                //close form
                prompt.Close();
            };

            flowLayoutPanel.Controls.Add(secCheckBox);
            flowLayoutPanel.Controls.Add(minCheckBox);
            flowLayoutPanel.Controls.Add(hourCheckBox);

            buttonFlowLayoutPanel.Controls.Add(confirmation);
            buttonFlowLayoutPanel.Controls.Add(exitButton);


            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(inputBox);
            prompt.Controls.Add(flowLayoutPanel);
            prompt.Controls.Add(buttonFlowLayoutPanel);

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

        private void saveIntervalTimer(String time, String createdAt, String type)
        {
            var data = new Dictionary<string, string>();
            data.Add("time", time);
            data.Add("created_at", createdAt);
            data.Add("type", type);
            //save to db
            DBHelper.Insert(this.dbCon, this.duration_table, data);
            //update timer
            this.defaultInterval = time + " " + type;
            this.loadInterval();
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
            int nextId = -1;

            if (this.currentTaskId > 0)
            {
                
                if (FormWindowState.Minimized == this.WindowState)
                {
                    // move from taskbar to main screen 
                    this.Show();
                    this.WindowState = FormWindowState.Normal;
                }

                //bool used to check if the in between task is valid e.g is active
                bool isInBetweenValid = true;

                if (this.hasInBetween)
                {
                    var nextTaskState = this.tasksActiveStateButtonList[this.inBetweenId].Tag.ToString();
                    if (nextTaskState == "0")
                    {
                        //in between task is not set to be active
                        isInBetweenValid = false;
                    }
                }
                //&& (this.currentTaskId != this.inBetweenId)
                if (this.hasInBetween && this.inBetweenToggleStatus && isInBetweenValid)
                {
                    // task has inbetween and interval is on inbetween
                    this.taskIdBeforeInBetweenId = this.currentTaskId;
                    this.activateTask(this.inBetweenId, "interval");
                    
                }
                else
                {
                    if (this.hasInBetween && this.taskIdBeforeInBetweenId != -1)
                    {
                        // deactivate in between task
                        this.activateTask(-1, oldTaskId: this.inBetweenId);
                        
                        // update db, de-activate previous task before inbetween
                        this.currentTaskId = this.taskIdBeforeInBetweenId;
                    }

                    // update db un-set current active
                    var updateData = new Dictionary<string, string>();
                    updateData.Add("current", "0");

                    DBHelper.Update(this.dbCon, this.tasks_table, updateData, "id=" + this.currentTaskId);

                    // update db set current active to next
                    nextId = getNextTaskId();
                    int skipCounter = 0;
                
                    for (; ; )
                    {
                        
                        if (this.hasInBetween && nextId == this.inBetweenId)
                        {
                            //deactivate previous id
                            this.activateTask(-1, oldTaskId: this.currentTaskId);
                            
                            //if next id is inbetween task's id move to the next id
                            this.currentTaskId = nextId;
                            nextId = getNextTaskId();
                        }

                        var nextTaskState = this.tasksActiveStateButtonList[nextId].Tag.ToString();

                        if (nextTaskState == "0")
                        {
                            // make checks, next task is deactivated skip
                            this.activateTask(-1, oldTaskId: this.currentTaskId);
                            this.currentTaskId = nextId;
                            nextId = getNextTaskId();

                            skipCounter++;
                        }
                        else if (nextTaskState == "1")
                        {
                            // next active task selected
                            break;
                        }

                        if (skipCounter == (this.tasksActiveStateButtonList.Count - 1))
                        {
                            // all tasks were turned off
                            skipCounter = 0;
                            this.showMessage("No Task turned on.", messageType.negative);
                            return;
                        }
                    }


                    updateData = new Dictionary<string, string>();
                    updateData.Add("current", "1");

                    DBHelper.Update(this.dbCon, this.tasks_table, updateData, "id=" + nextId);
                    this.activateTask(nextId, "interval");

                }


                if (this.hasInBetween)
                {
                    if (isInBetweenValid && nextId != -1)
                    {
                        this.taskIdBeforeInBetweenId = nextId;
                    }

                    this.inBetweenToggleStatus = !this.inBetweenToggleStatus;
                }
                
                
                // update timer properties
                this.setDueTime();
            }
            else
            {
                this.showMessage("Current active task not set.", messageType.negative);
            }
        }

        private string[] getInterval()
        {
            //break the interval into a string array (e.g 45 min [45, min])
            return this.interval != "" ? this.interval.Split(' ') : this.defaultInterval.Split(' ');
        }

        private void setDueTime(bool isPlayed=false, double timeDiff=-1)
        {
            // if isplayed, use time difference till previous end of interval
            string[] interval = new string[2] { "", "" };
            double intervalDuration = 0;

            

            if (!isPlayed || (isPlayed && timeDiff == -1))
            {
                
                interval = this.getInterval();
                
                intervalDuration = Convert.ToInt32(interval[0]);
                

                //update timer label
                this.changeTimerLabelText(interval[0], interval[1]);
            }
            else
            {
                intervalDuration = timeDiff;
            }

            // for due time
            dueTime = DateTime.Now;
            var startingTime = DateTime.Now;


            if (interval[1] == this.hour)
            {
                dueTime = DateTime.Now.AddHours(Convert.ToInt32(intervalDuration));
                intervalDuration = Convert.ToInt32(this.ConvertHoursToMilliseconds(intervalDuration));
            }

            if (isPlayed || interval[1] == this.min)
            {
                //when timer is resumed(isplayed) timespan value sent is always in minutes
                dueTime = DateTime.Now.AddMinutes(Convert.ToInt32(intervalDuration));
                intervalDuration = Convert.ToInt32(this.ConvertMinutesToMilliseconds(intervalDuration));
                    
            }

            if (interval[1] == this.sec)
            {
                dueTime = DateTime.Now.AddSeconds(Convert.ToInt32(intervalDuration));
                intervalDuration = Convert.ToInt32(this.ConvertSecondsToMilliseconds(intervalDuration));
            }

            if (this.timerActive)
            {
                timer_start_label.Text = this.intervalStartLabelText + " - " + startingTime.ToShortTimeString();
                due_time_label.Text = this.intervalLabelText + " - " + dueTime.ToShortTimeString();
            }
            else
            {
                timer_start_label.Text = this.intervalStartLabelText;
                due_time_label.Text = this.intervalLabelText;

                //return;
            }

            //MessageBox.Show("Time Interval: " + this.interval + " DefTime Interval: " + this.defaultInterval + " intervalDuration " + intervalDuration);
            this.intervalDuration = intervalDuration;

            if (this.timerActive)  intervalHistory.Add(string.Format("Started At: {0} - Ending At: {1}", startingTime.ToLongTimeString(), dueTime.ToLongTimeString()));

            //update timer interval
            if (timer1 != null) timer1.Interval = Convert.ToInt32(this.intervalDuration);

        }

        private void pauseTimer()
        {
            var currentDateTime = DateTime.Now;
            //get difference between now and dueTime
            this.dateTimeDiff = dueTime.Subtract(currentDateTime);
            this.timerRunning = false;
            showMessage("Timer Paused", messageType.positive);

            intervalHistory.Add(string.Format("Paused At: {0}", DateTime.Now.ToLongTimeString()));

            stopTimer();
            //save time difference to db
            this.saveTimeLeftToDb();
        }

        private void saveTimeLeftToDb()
        {
            
            var data = new Dictionary<string, string>();
            data.Add("minutes_left", this.dateTimeDiff.TotalMinutes.ToString());
            data.Add("created_at", DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss"));
            data.Add("active", "1");
            //save to db
            DBHelper.Insert(this.dbCon, this.date_time_diff, data);
        }

        private void updateTimeLeftRecord(string currentId)
        {
            var updateData = new Dictionary<string, string>();
            updateData.Add("active", "0");

            DBHelper.Update(this.dbCon, this.date_time_diff, updateData, "id=" + currentId);
        }

        private void pause_button_Click(object sender, EventArgs e)
        {
            //if timer is running -> get length to next stop
            //-on resume timer is set to run till end of remaining time
            //-on end of remaining time, reset interval to default from db

            if (this.timerRunning)
            {
                this.pauseTimer();
            }
            else
            {

                this.timerRunning = true;
                //get time difference from latest row in db
                var data = DBHelper.Get(this.dbCon, this.date_time_diff, limit: 1, orderBy: "id DESC", whereClause: "active=1");
                double minsLeft = 0;

                if (data.Keys.Count > 0)
                {
                    minsLeft = Convert.ToDouble(data[0]["minutes_left"]);
                    //update record in db
                    this.updateTimeLeftRecord(data[0]["id"]);
                }

                setDueTime(isPlayed: true, timeDiff: minsLeft);
                this.startTimer();

                intervalHistory.Add(string.Format("Resumed At: {0}", DateTime.Now.ToLongTimeString()));

                showMessage("Timer Resumed", messageType.positive);
            }

            pause_button.Text = (this.timerRunning) ? "Pause" : "Resume";
        }

        private void start_end_button_Click(object sender, EventArgs e)
        {
            if (this.tasksButtonList.Count == 0)
            {
                showMessage("No Tasks loaded", messageType.negative);
                return;
            }
            this.timerActive = !this.timerActive;
            this.timerRunning = this.timerActive;
            
            start_end_button.Text = (!this.timerActive) ? "Start" : "End";
            

            setDueTime();

            if (this.timerActive)
            {
                this.startTimer();
                this.showMessage("Timer Active", messageType.neutral);
            }
            else
            {
                // timer is stopped
                pause_button.Text = "Pause";

                this.stopTimer();
                this.showMessage("Timer Deactived", messageType.positive);
            }

        }

        private void startTimer()
        {
            if (timer1 == null)
            {
                timer1 = new Timer
                {
                    Interval = Convert.ToInt32(intervalDuration)
                };

                timer1.Start();
                timer1.Enabled = true;
            }

            if (timer1 != null)
            {
                // subscribe to event
                timer1.Tick += timerEvent;
            }
        }

        private void stopTimer()
        {
            if (timer1 != null)
            {
                timer1.Stop();
                timer1.Enabled = false;
                timer1.Dispose();
                //unsubscribe from timer event
                timer1.Tick -= timerEvent;
                //reset to null
                timer1 = null;
            }
        }

        private void timerEvent (object s, EventArgs ev)
        {
            if (!this.timerActive)
            {
                timer1.Stop();
                MessageBox.Show("Timer Stopped");
            }
            else
            {
                this.intervalCheck();
            }
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
                this.ShowInTaskbar = true;
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
            flowLayoutPanel.AutoScroll = true;

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
