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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.start_end_button = new System.Windows.Forms.Button();
            this.change_timer = new System.Windows.Forms.Button();
            this.pause_button = new System.Windows.Forms.Button();
            this.panel8 = new System.Windows.Forms.Panel();
            this.timer_label = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.active_task = new System.Windows.Forms.FlowLayoutPanel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.message_label = new System.Windows.Forms.Label();
            this.due_time_label = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.form_panel = new System.Windows.Forms.FlowLayoutPanel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.add_task = new System.Windows.Forms.Button();
            this.save = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.active_task_label = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.mynotifyicon = new System.Windows.Forms.NotifyIcon(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel9.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.active_task_label);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(877, 37);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.start_end_button);
            this.panel2.Controls.Add(this.change_timer);
            this.panel2.Controls.Add(this.pause_button);
            this.panel2.Controls.Add(this.panel8);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 37);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(877, 334);
            this.panel2.TabIndex = 3;
            // 
            // start_end_button
            // 
            this.start_end_button.Location = new System.Drawing.Point(419, 180);
            this.start_end_button.Name = "start_end_button";
            this.start_end_button.Size = new System.Drawing.Size(59, 23);
            this.start_end_button.TabIndex = 5;
            this.start_end_button.Text = "Start";
            this.start_end_button.UseVisualStyleBackColor = true;
            this.start_end_button.Click += new System.EventHandler(this.start_end_button_Click);
            // 
            // change_timer
            // 
            this.change_timer.Location = new System.Drawing.Point(419, 209);
            this.change_timer.Name = "change_timer";
            this.change_timer.Size = new System.Drawing.Size(59, 53);
            this.change_timer.TabIndex = 4;
            this.change_timer.Text = "Change Timer";
            this.change_timer.UseVisualStyleBackColor = true;
            this.change_timer.Click += new System.EventHandler(this.change_timer_Click);
            // 
            // pause_button
            // 
            this.pause_button.Location = new System.Drawing.Point(419, 268);
            this.pause_button.Name = "pause_button";
            this.pause_button.Size = new System.Drawing.Size(59, 23);
            this.pause_button.TabIndex = 3;
            this.pause_button.Text = "Pause";
            this.pause_button.UseVisualStyleBackColor = true;
            this.pause_button.Visible = false;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.timer_label);
            this.panel8.Location = new System.Drawing.Point(419, 302);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(48, 30);
            this.panel8.TabIndex = 2;
            // 
            // timer_label
            // 
            this.timer_label.AutoSize = true;
            this.timer_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timer_label.Location = new System.Drawing.Point(3, 8);
            this.timer_label.Name = "timer_label";
            this.timer_label.Size = new System.Drawing.Size(21, 13);
            this.timer_label.TabIndex = 0;
            this.timer_label.Text = "45";
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.Controls.Add(this.active_task);
            this.panel3.Controls.Add(this.panel7);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(476, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(401, 334);
            this.panel3.TabIndex = 1;
            // 
            // active_task
            // 
            this.active_task.AutoScroll = true;
            this.active_task.Location = new System.Drawing.Point(4, 42);
            this.active_task.Name = "active_task";
            this.active_task.Size = new System.Drawing.Size(394, 290);
            this.active_task.TabIndex = 3;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.message_label);
            this.panel7.Controls.Add(this.due_time_label);
            this.panel7.Controls.Add(this.label3);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(401, 36);
            this.panel7.TabIndex = 2;
            // 
            // message_label
            // 
            this.message_label.AutoSize = true;
            this.message_label.ForeColor = System.Drawing.Color.Black;
            this.message_label.Location = new System.Drawing.Point(103, 11);
            this.message_label.Name = "message_label";
            this.message_label.Size = new System.Drawing.Size(115, 13);
            this.message_label.TabIndex = 4;
            this.message_label.Text = "Message: No message";
            // 
            // due_time_label
            // 
            this.due_time_label.AutoSize = true;
            this.due_time_label.Location = new System.Drawing.Point(256, 11);
            this.due_time_label.Name = "due_time_label";
            this.due_time_label.Size = new System.Drawing.Size(69, 13);
            this.due_time_label.TabIndex = 3;
            this.due_time_label.Text = "Interval Ends";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(15, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Today\'s Tasks";
            // 
            // panel4
            // 
            this.panel4.AutoScroll = true;
            this.panel4.AutoSize = true;
            this.panel4.Controls.Add(this.form_panel);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(420, 334);
            this.panel4.TabIndex = 0;
            // 
            // form_panel
            // 
            this.form_panel.AutoScroll = true;
            this.form_panel.Location = new System.Drawing.Point(0, 46);
            this.form_panel.Name = "form_panel";
            this.form_panel.Size = new System.Drawing.Size(417, 286);
            this.form_panel.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(420, 39);
            this.panel5.TabIndex = 0;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.panel9);
            this.panel6.Controls.Add(this.label4);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(420, 39);
            this.panel6.TabIndex = 2;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.add_task);
            this.panel9.Controls.Add(this.save);
            this.panel9.Controls.Add(this.label5);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(0, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(420, 39);
            this.panel9.TabIndex = 3;
            // 
            // add_task
            // 
            this.add_task.Location = new System.Drawing.Point(173, 8);
            this.add_task.Name = "add_task";
            this.add_task.Size = new System.Drawing.Size(75, 23);
            this.add_task.TabIndex = 3;
            this.add_task.Text = "Add Task";
            this.add_task.UseVisualStyleBackColor = true;
            this.add_task.Click += new System.EventHandler(this.add_task_Click);
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(301, 8);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(75, 23);
            this.save.TabIndex = 2;
            this.save.Text = "Save";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 16);
            this.label5.TabIndex = 1;
            this.label5.Text = "Add New Task";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 16);
            this.label4.TabIndex = 1;
            this.label4.Text = "Add New Task";
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Schedule Manager";
            // 
            // active_task_label
            // 
            this.active_task_label.AutoSize = true;
            this.active_task_label.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.active_task_label.Location = new System.Drawing.Point(546, 9);
            this.active_task_label.Name = "active_task_label";
            this.active_task_label.Size = new System.Drawing.Size(32, 19);
            this.active_task_label.TabIndex = 1;
            this.active_task_label.Text = "NA";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(473, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 16);
            this.label6.TabIndex = 2;
            this.label6.Text = "Active Task";
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
            this.ClientSize = new System.Drawing.Size(877, 371);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label active_task_label;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button start_end_button;
        private System.Windows.Forms.Button change_timer;
        private System.Windows.Forms.Button pause_button;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label timer_label;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.FlowLayoutPanel active_task;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label message_label;
        private System.Windows.Forms.Label due_time_label;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.FlowLayoutPanel form_panel;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Button add_task;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NotifyIcon mynotifyicon;
    }
}

