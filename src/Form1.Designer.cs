namespace zTimer
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
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.test1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.minutesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hourToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.minutesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.minutesToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.minutesToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.minutesToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.minutesToolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.hourToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ReminderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addReminderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.russianÐóññêèéToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripMenuItem();
            this.noneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loudToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripSeparator();
            this.testSoundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem14 = new System.Windows.Forms.ToolStripMenuItem();
            this.classicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "zTimer";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            this.notifyIcon1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem3,
            this.toolStripSeparator1,
            this.toolStripMenuItem6,
            this.toolStripMenuItem5,
            this.toolStripMenuItem4,
            this.test1ToolStripMenuItem,
            this.toolStripMenuItem2,
            this.minutesToolStripMenuItem,
            this.hourToolStripMenuItem1,
            this.toolStripMenuItem8,
            this.ReminderMenuItem,
            this.toolStripMenuItem1,
            this.toolStripMenuItem13,
            this.toolStripMenuItem14,
            this.toolStripSeparator3,
            this.toolStripMenuItem11,
            this.toolStripMenuItem10,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 380);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem3.Text = "Show window";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.showTimer_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem6.Text = "3 seconds";
            this.toolStripMenuItem6.Visible = false;
            this.toolStripMenuItem6.Click += new System.EventHandler(this.toolStripMenuItem6_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem5.Text = "3 minutes";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem4.Text = "5 minutes";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // test1ToolStripMenuItem
            // 
            this.test1ToolStripMenuItem.Name = "test1ToolStripMenuItem";
            this.test1ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.test1ToolStripMenuItem.Text = "10 minutes";
            this.test1ToolStripMenuItem.Click += new System.EventHandler(this.test1ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem2.Text = "20 minutes";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // minutesToolStripMenuItem
            // 
            this.minutesToolStripMenuItem.Name = "minutesToolStripMenuItem";
            this.minutesToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.minutesToolStripMenuItem.Text = "40 minutes";
            this.minutesToolStripMenuItem.Click += new System.EventHandler(this.minutesToolStripMenuItem_Click);
            // 
            // hourToolStripMenuItem1
            // 
            this.hourToolStripMenuItem1.Name = "hourToolStripMenuItem1";
            this.hourToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.hourToolStripMenuItem1.Text = "1 hour";
            this.hourToolStripMenuItem1.Click += new System.EventHandler(this.hourToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.minutesToolStripMenuItem1,
            this.toolStripMenuItem9,
            this.minutesToolStripMenuItem2,
            this.minutesToolStripMenuItem3,
            this.minutesToolStripMenuItem4,
            this.minutesToolStripMenuItem5,
            this.hourToolStripMenuItem});
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem8.Text = "Add time";
            // 
            // minutesToolStripMenuItem1
            // 
            this.minutesToolStripMenuItem1.Name = "minutesToolStripMenuItem1";
            this.minutesToolStripMenuItem1.Size = new System.Drawing.Size(137, 22);
            this.minutesToolStripMenuItem1.Text = "5 minutes";
            this.minutesToolStripMenuItem1.Click += new System.EventHandler(this.minutesToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(137, 22);
            this.toolStripMenuItem9.Text = "10 minutes";
            this.toolStripMenuItem9.Click += new System.EventHandler(this.toolStripMenuItem9_Click);
            // 
            // minutesToolStripMenuItem2
            // 
            this.minutesToolStripMenuItem2.Name = "minutesToolStripMenuItem2";
            this.minutesToolStripMenuItem2.Size = new System.Drawing.Size(137, 22);
            this.minutesToolStripMenuItem2.Text = "20 minutes";
            this.minutesToolStripMenuItem2.Click += new System.EventHandler(this.minutesToolStripMenuItem2_Click);
            // 
            // minutesToolStripMenuItem3
            // 
            this.minutesToolStripMenuItem3.Name = "minutesToolStripMenuItem3";
            this.minutesToolStripMenuItem3.Size = new System.Drawing.Size(137, 22);
            this.minutesToolStripMenuItem3.Text = "30 minutes";
            this.minutesToolStripMenuItem3.Click += new System.EventHandler(this.minutesToolStripMenuItem3_Click);
            // 
            // minutesToolStripMenuItem4
            // 
            this.minutesToolStripMenuItem4.Name = "minutesToolStripMenuItem4";
            this.minutesToolStripMenuItem4.Size = new System.Drawing.Size(137, 22);
            this.minutesToolStripMenuItem4.Text = "40 minutes";
            this.minutesToolStripMenuItem4.Click += new System.EventHandler(this.minutesToolStripMenuItem4_Click);
            // 
            // minutesToolStripMenuItem5
            // 
            this.minutesToolStripMenuItem5.Name = "minutesToolStripMenuItem5";
            this.minutesToolStripMenuItem5.Size = new System.Drawing.Size(137, 22);
            this.minutesToolStripMenuItem5.Text = "50 minutes";
            this.minutesToolStripMenuItem5.Click += new System.EventHandler(this.minutesToolStripMenuItem5_Click);
            // 
            // hourToolStripMenuItem
            // 
            this.hourToolStripMenuItem.Name = "hourToolStripMenuItem";
            this.hourToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.hourToolStripMenuItem.Text = "1 hour";
            this.hourToolStripMenuItem.Click += new System.EventHandler(this.hourToolStripMenuItem_Click);
            // 
            // ReminderMenuItem
            // 
            this.ReminderMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addReminderToolStripMenuItem});
            this.ReminderMenuItem.Name = "ReminderMenuItem";
            this.ReminderMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ReminderMenuItem.Text = "Reminders";
            this.ReminderMenuItem.Click += new System.EventHandler(this.toolStripMenuItem12_Click);
            // 
            // addReminderToolStripMenuItem
            // 
            this.addReminderToolStripMenuItem.Name = "addReminderToolStripMenuItem";
            this.addReminderToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.addReminderToolStripMenuItem.Text = "Add reminder";
            this.addReminderToolStripMenuItem.Click += new System.EventHandler(this.addReminderToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.englishToolStripMenuItem,
            this.russianÐóññêèéToolStripMenuItem});
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem11.Text = "Language";
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            this.englishToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.englishToolStripMenuItem.Text = "English";
            this.englishToolStripMenuItem.Click += new System.EventHandler(this.englishToolStripMenuItem_Click);
            // 
            // russianÐóññêèéToolStripMenuItem
            // 
            this.russianÐóññêèéToolStripMenuItem.Name = "russianÐóññêèéToolStripMenuItem";
            this.russianÐóññêèéToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.russianÐóññêèéToolStripMenuItem.Text = "Russian (Ðóññêèé)";
            this.russianÐóññêèéToolStripMenuItem.Click += new System.EventHandler(this.russianÐóññêèéToolStripMenuItem_Click);
            // 
            // toolStripMenuItem13
            // 
            this.toolStripMenuItem13.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noneToolStripMenuItem,
            this.loudToolStripMenuItem,
            this.toolStripMenuItem12,
            this.testSoundToolStripMenuItem});
            this.toolStripMenuItem13.Name = "toolStripMenuItem13";
            this.toolStripMenuItem13.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem13.Text = "Sound";
            // 
            // noneToolStripMenuItem
            // 
            this.noneToolStripMenuItem.Name = "noneToolStripMenuItem";
            this.noneToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.noneToolStripMenuItem.Text = "Off";
            this.noneToolStripMenuItem.Click += new System.EventHandler(this.noneToolStripMenuItem_Click);
            // 
            // loudToolStripMenuItem
            // 
            this.loudToolStripMenuItem.Name = "loudToolStripMenuItem";
            this.loudToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.loudToolStripMenuItem.Text = "On (may be loud!)";
            this.loudToolStripMenuItem.Click += new System.EventHandler(this.loudToolStripMenuItem_Click);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem10.Text = "About";
            this.toolStripMenuItem10.Click += new System.EventHandler(this.toolStripMenuItem10_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem7.Text = "New time";
            // 
            // toolStripMenuItem12
            // 
            this.toolStripMenuItem12.Name = "toolStripMenuItem12";
            this.toolStripMenuItem12.Size = new System.Drawing.Size(169, 6);
            // 
            // testSoundToolStripMenuItem
            // 
            this.testSoundToolStripMenuItem.Name = "testSoundToolStripMenuItem";
            this.testSoundToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.testSoundToolStripMenuItem.Text = "Test sound";
            this.testSoundToolStripMenuItem.Click += new System.EventHandler(this.testSoundToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(149, 6);
            // 
            // toolStripMenuItem14
            // 
            this.toolStripMenuItem14.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smallToolStripMenuItem,
            this.classicToolStripMenuItem});
            this.toolStripMenuItem14.Name = "toolStripMenuItem14";
            this.toolStripMenuItem14.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem14.Text = "Window size";
            // 
            // classicToolStripMenuItem
            // 
            this.classicToolStripMenuItem.Name = "classicToolStripMenuItem";
            this.classicToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.classicToolStripMenuItem.Text = "Classic";
            this.classicToolStripMenuItem.Click += new System.EventHandler(this.classicToolStripMenuItem_Click);
            // 
            // smallToolStripMenuItem
            // 
            this.smallToolStripMenuItem.Name = "smallToolStripMenuItem";
            this.smallToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.smallToolStripMenuItem.Text = "Small";
            this.smallToolStripMenuItem.Click += new System.EventHandler(this.smallToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 210);
            this.Name = "Form1";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem test1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem minutesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hourToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem minutesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem minutesToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem minutesToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem minutesToolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem minutesToolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem hourToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem11;
        private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem russianÐóññêèéToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ReminderMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem13;
        private System.Windows.Forms.ToolStripMenuItem noneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loudToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addReminderToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem12;
        private System.Windows.Forms.ToolStripMenuItem testSoundToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem14;
        private System.Windows.Forms.ToolStripMenuItem classicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smallToolStripMenuItem;
	}
}

