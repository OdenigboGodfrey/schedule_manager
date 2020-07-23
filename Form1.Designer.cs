namespace ScheduleManager
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.active_task = new System.Windows.Forms.FlowLayoutPanel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.timer_start_label = new System.Windows.Forms.Label();
            this.due_time_label = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.form_panel = new System.Windows.Forms.FlowLayoutPanel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.save = new System.Windows.Forms.Button();
            this.add_task = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.interval_history = new System.Windows.Forms.Button();
            this.message_label = new System.Windows.Forms.Label();
            this.change_timer = new System.Windows.Forms.Button();
            this.timer_label = new System.Windows.Forms.Label();
            this.pause_button = new System.Windows.Forms.Button();
            this.start_end_button = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.active_task_label = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.mynotifyicon = new System.Windows.Forms.NotifyIcon(this.components);
            this.panel2.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel8);
            this.panel2.Controls.Add(this.panel6);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1064, 408);
            this.panel2.TabIndex = 3;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.SystemColors.Control;
            this.panel8.Controls.Add(this.active_task);
            this.panel8.Controls.Add(this.panel9);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel8.Location = new System.Drawing.Point(546, 57);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(518, 306);
            this.panel8.TabIndex = 6;
            // 
            // active_task
            // 
            this.active_task.AutoScroll = true;
            this.active_task.Dock = System.Windows.Forms.DockStyle.Fill;
            this.active_task.Location = new System.Drawing.Point(0, 32);
            this.active_task.Name = "active_task";
            this.active_task.Size = new System.Drawing.Size(518, 274);
            this.active_task.TabIndex = 1;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.timer_start_label);
            this.panel9.Controls.Add(this.due_time_label);
            this.panel9.Controls.Add(this.label3);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(0, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(518, 32);
            this.panel9.TabIndex = 0;
            // 
            // timer_start_label
            // 
            this.timer_start_label.AutoSize = true;
            this.timer_start_label.Location = new System.Drawing.Point(184, 8);
            this.timer_start_label.Name = "timer_start_label";
            this.timer_start_label.Size = new System.Drawing.Size(79, 13);
            this.timer_start_label.TabIndex = 7;
            this.timer_start_label.Text = "Interval Started";
            // 
            // due_time_label
            // 
            this.due_time_label.AutoSize = true;
            this.due_time_label.Location = new System.Drawing.Point(359, 8);
            this.due_time_label.Name = "due_time_label";
            this.due_time_label.Size = new System.Drawing.Size(69, 13);
            this.due_time_label.TabIndex = 6;
            this.due_time_label.Text = "Interval Ends";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Today\'s Tasks";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.form_panel);
            this.panel6.Controls.Add(this.panel7);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel6.Location = new System.Drawing.Point(0, 57);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(543, 306);
            this.panel6.TabIndex = 5;
            // 
            // form_panel
            // 
            this.form_panel.AutoScroll = true;
            this.form_panel.BackColor = System.Drawing.SystemColors.Control;
            this.form_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.form_panel.Location = new System.Drawing.Point(0, 32);
            this.form_panel.Name = "form_panel";
            this.form_panel.Size = new System.Drawing.Size(543, 274);
            this.form_panel.TabIndex = 2;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.save);
            this.panel7.Controls.Add(this.add_task);
            this.panel7.Controls.Add(this.label5);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(543, 32);
            this.panel7.TabIndex = 0;
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(407, 3);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(75, 23);
            this.save.TabIndex = 5;
            this.save.Text = "Save";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // add_task
            // 
            this.add_task.Location = new System.Drawing.Point(326, 3);
            this.add_task.Name = "add_task";
            this.add_task.Size = new System.Drawing.Size(75, 23);
            this.add_task.TabIndex = 4;
            this.add_task.Text = "Add Task";
            this.add_task.UseVisualStyleBackColor = true;
            this.add_task.Click += new System.EventHandler(this.add_task_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 16);
            this.label5.TabIndex = 2;
            this.label5.Text = "Add New Task";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.interval_history);
            this.panel3.Controls.Add(this.message_label);
            this.panel3.Controls.Add(this.change_timer);
            this.panel3.Controls.Add(this.timer_label);
            this.panel3.Controls.Add(this.pause_button);
            this.panel3.Controls.Add(this.start_end_button);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 363);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1064, 45);
            this.panel3.TabIndex = 4;
            // 
            // interval_history
            // 
            this.interval_history.Location = new System.Drawing.Point(303, 10);
            this.interval_history.Name = "interval_history";
            this.interval_history.Size = new System.Drawing.Size(74, 23);
            this.interval_history.TabIndex = 13;
            this.interval_history.Text = "View History";
            this.interval_history.UseVisualStyleBackColor = true;
            this.interval_history.Click += new System.EventHandler(this.interval_history_Click);
            // 
            // message_label
            // 
            this.message_label.AutoSize = true;
            this.message_label.ForeColor = System.Drawing.Color.Black;
            this.message_label.Location = new System.Drawing.Point(681, 20);
            this.message_label.Name = "message_label";
            this.message_label.Size = new System.Drawing.Size(115, 13);
            this.message_label.TabIndex = 12;
            this.message_label.Text = "Message: No message";
            // 
            // change_timer
            // 
            this.change_timer.Location = new System.Drawing.Point(158, 12);
            this.change_timer.Name = "change_timer";
            this.change_timer.Size = new System.Drawing.Size(139, 21);
            this.change_timer.TabIndex = 11;
            this.change_timer.Text = "Change Default Timer";
            this.change_timer.UseVisualStyleBackColor = true;
            // 
            // timer_label
            // 
            this.timer_label.AutoSize = true;
            this.timer_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timer_label.Location = new System.Drawing.Point(507, 20);
            this.timer_label.Name = "timer_label";
            this.timer_label.Size = new System.Drawing.Size(21, 13);
            this.timer_label.TabIndex = 10;
            this.timer_label.Text = "45";
            // 
            // pause_button
            // 
            this.pause_button.Location = new System.Drawing.Point(93, 10);
            this.pause_button.Name = "pause_button";
            this.pause_button.Size = new System.Drawing.Size(59, 23);
            this.pause_button.TabIndex = 7;
            this.pause_button.Text = "Pause";
            this.pause_button.UseVisualStyleBackColor = true;
            this.pause_button.Click += new System.EventHandler(this.pause_button_Click);
            // 
            // start_end_button
            // 
            this.start_end_button.Location = new System.Drawing.Point(16, 10);
            this.start_end_button.Name = "start_end_button";
            this.start_end_button.Size = new System.Drawing.Size(59, 23);
            this.start_end_button.TabIndex = 6;
            this.start_end_button.Text = "Start";
            this.start_end_button.UseVisualStyleBackColor = true;
            this.start_end_button.Click += new System.EventHandler(this.start_end_button_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.active_task_label);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1064, 57);
            this.panel1.TabIndex = 1;
            // 
            // active_task_label
            // 
            this.active_task_label.AutoSize = true;
            this.active_task_label.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.active_task_label.Location = new System.Drawing.Point(656, 13);
            this.active_task_label.Name = "active_task_label";
            this.active_task_label.Size = new System.Drawing.Size(32, 19);
            this.active_task_label.TabIndex = 4;
            this.active_task_label.Text = "NA";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(583, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 16);
            this.label6.TabIndex = 3;
            this.label6.Text = "Active Task";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Schedule Manager";
            // 
            // panel4
            // 
            this.panel4.AutoScroll = true;
            this.panel4.AutoSize = true;
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(0, 408);
            this.panel4.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(0, 39);
            this.panel5.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Add New Task";
            // 
            // mynotifyicon
            // 
            this.mynotifyicon.BalloonTipText = "Schedule Manager";
            this.mynotifyicon.Icon = ((System.Drawing.Icon)(resources.GetObject("mynotifyicon.Icon")));
            this.mynotifyicon.Text = "notifyIcon1";
            this.mynotifyicon.Visible = true;
            this.mynotifyicon.Click += new System.EventHandler(this.mynotifyicon_Click);
            this.mynotifyicon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.mynotifyicon_MouseDoubleClick);
            this.mynotifyicon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mynotifyicon_MouseMove);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 408);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Schedule Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NotifyIcon mynotifyicon;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label active_task_label;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button pause_button;
        private System.Windows.Forms.Button start_end_button;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.FlowLayoutPanel form_panel;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.Button add_task;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label due_time_label;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FlowLayoutPanel active_task;
        private System.Windows.Forms.Button change_timer;
        private System.Windows.Forms.Label timer_label;
        private System.Windows.Forms.Label timer_start_label;
        private System.Windows.Forms.Label message_label;
        private System.Windows.Forms.Button interval_history;
    }
}

