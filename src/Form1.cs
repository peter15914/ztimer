using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace zTimer
{
	public partial class Form1 : Form
	{
		private Form2 m_form2;

        private bool m_bPaused = true;
        public bool Paused
        {
            get
            {
                return m_bPaused;
            }
            set
            {
                if (m_bPaused != value)
                {
                    if (!value)
                    {
                        //сняли с паузы
                        m_StartTime = DateTime.Now;
                        if (m_GlobalStartTime == default(DateTime))
                            m_GlobalStartTime = DateTime.Now;
                    }
                    else
                    {
                        //поставили на паузу
                        System.Diagnostics.Debug.Assert(DateTime.Now >= m_StartTime);
                        m_TimerTime -= (DateTime.Now - m_StartTime);
                        m_TimeSpend += (DateTime.Now - m_StartTime);
                    }
                    m_bPaused = value;

                    if (m_form2 != null)
                        m_form2.refresh_BtnImage();
                }
            }

        }

        private bool m_bPauseOnLock = true;
        public bool PauseOnLock { get { return m_bPauseOnLock; } set { m_bPauseOnLock = value;  } }

        private DateTime m_StartTime;
        public DateTime StartTime { get { return m_StartTime; } }
        private DateTime m_GlobalStartTime;
        public DateTime GlobalStartTime { get { return m_GlobalStartTime; } }


        public List<REMINDER_STRUCT> m_reminders = new List<REMINDER_STRUCT>();

        public void add_reminder(int minutes, string text, bool sound)
        {
            m_reminders.Add(new REMINDER_STRUCT(minutes, text, sound));
            save_reminders_to_registry();

            _refresh_reminders_menu();
        }


        public void ResetReminderTime(int reminderNum)
        {
            if (reminderNum >= 0 && reminderNum < m_reminders.Count)
                m_reminders[reminderNum].ResetReminderTime();
        }

        private TimeSpan m_TimerTime;       //сколько времени осталось в таймере

        public TimeSpan TimeLeft
        {
            get
            {
                if (Paused)
                    return m_TimerTime;

                TimeSpan dt = m_TimerTime - (DateTime.Now - m_StartTime);
                return dt;
            }
            set
            {
                if (Paused)
                    m_TimerTime = value;
                else
                    m_TimerTime = value + (DateTime.Now - m_StartTime);

                //if (m_TimerTime < TimeSpan.Zero)
                //   m_TimerTime = TimeSpan.Zero;
            }
        }

        private TimeSpan m_TimeSpend;       //сколько времени потрачено
        public TimeSpan TimeSpend
        {
            get
            {
                if (Paused)
                    return m_TimeSpend;

                TimeSpan dt = m_TimeSpend + (DateTime.Now - m_StartTime);
                return dt;
            }
        }

        public Form1()
        {
            InitializeComponent();

            SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEvents_SessionSwitch);

            _show_Form2();

            load_reminders_from_registry();
            _refresh_reminders_menu();
        }

		private void Form1_Resize(object sender, EventArgs e)
		{
			//if (FormWindowState.Minimized == WindowState)
				//Hide();
		}

        private void beginTimer(TimeSpan time)
        {
            if (m_form2 == null)
            {
                m_form2 = new Form2(this);
                m_form2.move_DefaultPos();
            }

            m_form2.blink_OnAttention(false);
            m_form2.Show();

            Paused = true;
            TimeLeft = time;
            m_TimeSpend = new TimeSpan(0, 0, 0);
            m_GlobalStartTime = default(DateTime);
        }

        private void _show_Form2()
        {
            if (m_form2 != null)
                m_form2.Show();
            else
                beginTimer(new TimeSpan(0, 5, 0));
        }

        private void _toggle_Form2()
        {
            if (m_form2 == null || !m_form2.Visible)
                _show_Form2();
            else
                m_form2.Hide();
        }

		private void notifyIcon1_DoubleClick(object sender, EventArgs e)
		{
            //toggle form on double click
            if (m_LastMouseBtnDown == System.Windows.Forms.MouseButtons.Left)
                _toggle_Form2();
        }

		private void Form1_Activated(object sender, EventArgs e)
		{
		}

		private void Form1_FormClosed(object sender, FormClosedEventArgs e)
		{
			Application.ExitThread();
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (m_form2 != null)
				m_form2.Dispose();
            m_form2 = null;

            notifyIcon1.Visible = false;
			notifyIcon1.Dispose();

            Application.Exit();
        }

		private void test1ToolStripMenuItem_Click(object sender, EventArgs e)
		{
            beginTimer(new TimeSpan(0, 10, 0));
		}

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            int lang = ParamsServ.Inst.GetLanguage(1);
            String text = (m_form2 != null && m_form2.Visible) ?
                (lang == 1 ? "Hide window" : "Скрыть окно")
                :
                (lang == 1 ? "Show window" : "Показать окно");
            this.contextMenuStrip1.Items[0].Text = text;

            refresh_reminders_menu_texts();

            _translateMenu();
            _checkMenuItemsFromSettings();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            beginTimer(new TimeSpan(0, 20, 0));
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            beginTimer(new TimeSpan(0, 5, 0));
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            beginTimer(new TimeSpan(0, 3, 0));
        }

        private bool m_bWasPausedBeforeLock = false;
        void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            switch (e.Reason)
            {
                case SessionSwitchReason.SessionLock:
                    m_bWasPausedBeforeLock = Paused;
                    if (!Paused && PauseOnLock)
                        Paused = true;
                    break;
                case SessionSwitchReason.SessionUnlock:
                    if (!m_bWasPausedBeforeLock)
                        this.m_form2.blink_OnAttention();
                    m_bWasPausedBeforeLock = false;
                    break;
            }
        }

        private void showTimer_Click(object sender, EventArgs e)
        {
            if (m_form2 != null && m_form2.Visible)
                m_form2.Hide();
            else
                _show_Form2();
        }

        public void show_ContextMenu(Point p)
        {
            contextMenuStrip1.Show(p);
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Middle)
                _toggle_Form2();
        }

        private MouseButtons m_LastMouseBtnDown;
        private void notifyIcon1_MouseDown(object sender, MouseEventArgs e)
        {
            m_LastMouseBtnDown = e.Button;
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            beginTimer(new TimeSpan(0, 0, 3));
        }

        private void minutesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            beginTimer(new TimeSpan(0, 40, 0));
        }

        private void minutesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TimeLeft += new TimeSpan(0, 5, 0);
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            TimeLeft += new TimeSpan(0, 10, 0);
        }

        private void minutesToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            TimeLeft += new TimeSpan(0, 20, 0);
        }

        private void minutesToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            TimeLeft += new TimeSpan(0, 30, 0);
        }

        private void minutesToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            TimeLeft += new TimeSpan(0, 40, 0);
        }

        private void minutesToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            TimeLeft += new TimeSpan(0, 50, 0);
        }

        private void hourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TimeLeft += new TimeSpan(1, 0, 0);
        }

        private void hourToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            beginTimer(new TimeSpan(1, 0, 0));
        }

        private AboutForm m_aboutForm;
        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("zTimer version 0.01", "zTimer");
            m_aboutForm = new AboutForm();
            m_aboutForm.Show();
        }

        public void _translateMenu()
        {
            //doing this in a hurry, that's why this code is so bad
            int lang = ParamsServ.Inst.GetLanguage(1);

            this.toolStripMenuItem5.Text = lang == 1 ? "3 minutes" : "3 минуты";
            this.toolStripMenuItem4.Text = lang == 1 ? "5 minutes" : "5 минут";
            this.test1ToolStripMenuItem.Text = lang == 1 ? "10 minutes" : "10 минут";
            this.toolStripMenuItem2.Text = lang == 1 ? "20 minutes" : "20 минут";
            this.minutesToolStripMenuItem.Text = lang == 1 ? "40 minutes" : "40 минут";
            this.hourToolStripMenuItem1.Text = lang == 1 ? "1 hour" : "1 час";
            this.toolStripMenuItem8.Text = lang == 1 ? "Add time" : "Добавить";
            this.minutesToolStripMenuItem1.Text = lang == 1 ? "5 minutes" : "5 минут";
            this.toolStripMenuItem9.Text = lang == 1 ? "10 minutes" : "10 минут";
            this.minutesToolStripMenuItem2.Text = lang == 1 ? "20 minutes" : "20 минут";
            this.minutesToolStripMenuItem3.Text = lang == 1 ? "30 minutes" : "30 минут";
            this.minutesToolStripMenuItem4.Text = lang == 1 ? "40 minutes" : "40 минут";
            this.minutesToolStripMenuItem5.Text = lang == 1 ? "50 minutes" : "50 минут";
            this.hourToolStripMenuItem.Text = lang == 1 ? "1 hour" : "1 час";
            this.exitToolStripMenuItem.Text = lang == 1 ? "Exit" : "Выход";
            this.toolStripMenuItem10.Text = lang == 1 ? "About" : "О программе";
            this.toolStripMenuItem11.Text = lang == 1 ? "Language" : "Язык (language)";
            this.ReminderMenuItem.Text = lang == 1 ? "Reminders" : "Напоминания";
            this.addReminderToolStripMenuItem.Text = lang == 1 ? "Add reminder" : "Добавить напоминание";
            this.toolStripMenuItem13.Text = lang == 1 ? "Sound" : "Звук";
            this.noneToolStripMenuItem.Text = lang == 1 ? "Off" : "Выкл.";
            this.loudToolStripMenuItem.Text = lang == 1 ? "On (may be loud!)" : "Вкл. (может быть громко!)";
            this.testSoundToolStripMenuItem.Text = lang == 1 ? "Test sound" : "Тест звука";

            this.toolStripMenuItem14.Text = lang == 1 ? "Window size" : "Размер окна";
            this.classicToolStripMenuItem.Text = lang == 1 ? "Classic" : "Классический";
            this.smallToolStripMenuItem.Text = lang == 1 ? "Small" : "Мелкий";
        }

        public void _checkMenuItemsFromSettings()
        {
            int lang = ParamsServ.Inst.GetLanguage(1);

            this.englishToolStripMenuItem.Checked = (lang == 1);
            this.russianРусскийToolStripMenuItem.Checked = (lang != 1);

            //
            int sound = ParamsServ.Inst.GetSound(2);

            this.noneToolStripMenuItem.Checked = (sound == 0);
            this.loudToolStripMenuItem.Checked = (sound != 0);

            //
            int buf = ParamsServ.Inst.GetWndSizeType();
            this.smallToolStripMenuItem.Checked = (buf == 0); ;
            this.classicToolStripMenuItem.Checked = (buf != 0);
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ParamsServ.Inst.SetLanguage(1);
        }

        private void russianРусскийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ParamsServ.Inst.SetLanguage(2);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
        }

        private void noneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ParamsServ.Inst.SetSound(0);
            if (m_form2 != null)
                m_form2.stop_sound();
        }

        private void loudToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ParamsServ.Inst.SetSound(3);
        }

        private void addReminderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_form2.show_addreminder_form();
        }

        private void pauseReminder_Click(object sender, EventArgs e)
        {
            int ind = (int)(sender as ToolStripMenuItem).Tag;
            if (ind >= 0 && ind < m_reminders.Count)
            {
                m_reminders[ind].Paused = !m_reminders[ind].Paused;
                refresh_reminders_menu_texts();
            }
        }

        private void delReminder_Click(object sender, EventArgs e)
        {
            int ind = (int)(sender as ToolStripMenuItem).Tag;
            _delete_Reminder(ind);
        }

        private void resetReminder_Click(object sender, EventArgs e)
        {
            int ind = (int)(sender as ToolStripMenuItem).Tag;
            if (ind >= 0 && ind < m_reminders.Count)
            {
                m_reminders[ind].ResetReminderTime();
                refresh_reminders_menu_texts();
            }
        }

        private void _delete_Reminder(int ind)
        {
            if (ind >= 0 && ind < m_reminders.Count)
            {
                if (m_reminders[ind].reminder_form != null)
                    m_reminders[ind].reminder_form.Close();
                m_reminders.RemoveAt(ind);
                _refresh_reminders_menu();
                save_reminders_to_registry();
            }
        }

        public void delete_Reminder(REMINDER_STRUCT r)
        {
            for (int i = 0; i < m_reminders.Count; i++)
            {
                if (m_reminders[i] == r)
                {
                    _delete_Reminder(i);
                    break;
                }
            }
        }

        int m_iSolidReminderMenuItems = 1;
        private void _refresh_reminders_menu()
        {
            //with or without separator
            if(m_reminders.Count > 0)
                m_iSolidReminderMenuItems = 2;
            else
                m_iSolidReminderMenuItems = 1;

            int cur_size = ReminderMenuItem.DropDownItems.Count;

            //add or remove separator
            if (m_iSolidReminderMenuItems == 1 && cur_size >= 2 && (ReminderMenuItem.DropDownItems[1] as ToolStripSeparator) != null)
                ReminderMenuItem.DropDownItems.RemoveAt(1);
            if (m_iSolidReminderMenuItems == 2 && (cur_size < 2 || (ReminderMenuItem.DropDownItems[1] as ToolStripSeparator) == null))
                ReminderMenuItem.DropDownItems.Insert(1, new ToolStripSeparator());
            //
            int need_size = m_iSolidReminderMenuItems + m_reminders.Count;

            //delete unneeded
            while (need_size < ReminderMenuItem.DropDownItems.Count)
                ReminderMenuItem.DropDownItems.RemoveAt(need_size);

            //create new
            for (int i = ReminderMenuItem.DropDownItems.Count; i < need_size; i++)
            {
                ToolStripMenuItem item = new ToolStripMenuItem("aaa");
                ReminderMenuItem.DropDownItems.Add(item);

                ToolStripMenuItem pause_item = new ToolStripMenuItem("Pause");
                ToolStripMenuItem del_item = new ToolStripMenuItem("Delete");
                ToolStripMenuItem reset_item = new ToolStripMenuItem("Reset");

                pause_item.Click += new System.EventHandler(this.pauseReminder_Click);
                del_item.Click += new System.EventHandler(this.delReminder_Click);
                reset_item.Click += new System.EventHandler(this.resetReminder_Click);

                item.DropDownItems.Add(del_item);
                item.DropDownItems.Add(pause_item);
                item.DropDownItems.Add(reset_item);
            }

            refresh_reminders_menu_texts();
        }

        public void refresh_reminders_menu_texts()
        {
            //set texts
            for (int i = 0; i < m_reminders.Count; i++)
            {
                if (m_iSolidReminderMenuItems + i >= ReminderMenuItem.DropDownItems.Count)
                    break;  //error

                ToolStripItem item = ReminderMenuItem.DropDownItems[m_iSolidReminderMenuItems + i];

                TimeSpan dt = m_reminders[i].ReminderTimeLeft;
                string text = string.Format("{0:00}:{1:00}:{2:00}", dt.Hours, dt.Minutes, dt.Seconds);

                text += " \"";
                text += m_reminders[i].m_text;
                text += "\"";

                item.Text = text;

                ToolStripMenuItem menuitem = (item as ToolStripMenuItem);
                if (menuitem != null)
                {
                    for (int ii = 0; ii < menuitem.DropDownItems.Count; ii++)
                    {
                        menuitem.DropDownItems[ii].Tag = i;
                        if (ii == 1)//pause
                            (menuitem.DropDownItems[ii] as ToolStripMenuItem).Checked = m_reminders[i].Paused;
                    }
                }
            }
        }

        public void save_reminders_to_registry()
        {
            ParamsServ.Inst.SetRemindersCount(m_reminders.Count);
            for (int i = 0; i < m_reminders.Count; i++)
            {
                ParamsServ.Inst.SaveReminderParam(i, m_reminders[i]);
            }
        }

        public void load_reminders_from_registry()
        {
            int cnt = ParamsServ.Inst.GetRemindersCount();
            m_reminders.Clear();
            for (int i = 0; i < cnt; i++)
            {
                REMINDER_STRUCT r = new REMINDER_STRUCT();
                ParamsServ.Inst.GetReminderParam(i, r);
                m_reminders.Add(r);
            }
        }

        private void testSoundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_form2 != null)
                m_form2.play_sound();
        }

        private void smallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_form2 != null)
                m_form2.reset_wnd_size(Form2.WINDOW_SIZE_TYPE.WSZ_SMALL);
        }

        private void classicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_form2 != null)
                m_form2.reset_wnd_size(Form2.WINDOW_SIZE_TYPE.WSZ_CLASSIC);
        }

    }
}

