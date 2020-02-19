using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Media;

namespace zTimer
{
	public partial class Form2 : Form
	{
		private Form1 m_form1 = null;

        enum DRAG_TYPE
        {
            DT_NONE,  //not drag
            DT_BEGIN, //begin drag
            DT_DRAG   //real drag
        };

        private DRAG_TYPE m_eDragging = DRAG_TYPE.DT_NONE;
        private Point m_LMDown;
        private Point m_LMDownScreen;

		public Form2(Form1 form1)
		{
			InitializeComponent();
			m_form1 = form1;

            this.MouseWheel += new MouseEventHandler(MouseWheelEvent);

            toolTip1.SetToolTip(button5, "Toggle automatic pause on computer lock");
            toolTip1.SetToolTip(button1, "Toggle pause");
            toolTip1.SetToolTip(button4, "Hide to system tray");

            //make rounded angles of the form
            /*GraphicsPath path = new GraphicsPath();
            //int w = this.Width;
            //int h = this.Height;
            Rectangle bnds = Bounds;            // Rectangle.Inflate(Bounds, -1, -1);

            int CornerRadius = 10;

            path.AddArc(bnds.X, bnds.Y, CornerRadius, CornerRadius, 180, 90);
            path.AddArc(bnds.X + bnds.Width - CornerRadius, bnds.Y, CornerRadius, CornerRadius, 270, 90);
            path.AddArc(bnds.X + bnds.Width - CornerRadius-1, bnds.Y + bnds.Height - CornerRadius-2, CornerRadius, CornerRadius, 0, 90);
            path.AddArc(bnds.X, bnds.Y + bnds.Height - CornerRadius-2, CornerRadius, CornerRadius, 90, 90);
            path.CloseAllFigures();

            //path.AddEllipse(-(w - this.Width) / 2, 0, w, h);

            Region = new Region(path);*/
		}

        class LabelNoInput : Label
        {
            const int WM_NCHITTEST = 0x0084;
            const int HTTRANSPARENT = -1;
            protected override void WndProc(ref Message m)
            {
                if (m.Msg == WM_NCHITTEST)
                    m.Result = (IntPtr)HTTRANSPARENT;
                else
                    base.WndProc(ref m);
            }

            private int _iSelectedVal = -1; //0 - hours, 1 - minutes, 2 - seconds
            public int SelectedVal
            {
                get { return _iSelectedVal; }
                set
                {
                    if (_iSelectedVal != value)
                        this.Invalidate();
                    _iSelectedVal = value;
                }
            }

            protected override void OnPaint(PaintEventArgs pe)
            {
                base.OnPaint(pe);
                //if (SelectedVal != -1)
                {
                    System.Drawing.Pen SelPen, NormPen;
                    SelPen = new System.Drawing.Pen(System.Drawing.Color.GreenYellow);
                    NormPen = new System.Drawing.Pen(System.Drawing.Color.Gray);

                    /*if (SelectedVal == 0)
                        pe.Graphics.DrawRectangle(myPen, 1, 4, 36, 25);
                    else
                    if (SelectedVal == 1)
                        pe.Graphics.DrawRectangle(myPen, 43, 4, 36, 25);
                    else
                        pe.Graphics.DrawRectangle(myPen, 84, 4, 36, 25);*/

                    int dd = 2;
                    System.Drawing.Pen bufPen1, bufPen2, bufPen3;
                    bufPen1 = bufPen2 = bufPen3 = NormPen;
                    if (SelectedVal == 0) bufPen1 = SelPen;
                    if (SelectedVal == 1) bufPen2 = SelPen;
                    if (SelectedVal == 2) bufPen3 = SelPen;

                    if (m_cur_wnd_size_type == WINDOW_SIZE_TYPE.WSZ_SMALL)
                    {
                        pe.Graphics.DrawLine(bufPen1, 1 + dd, 18, 20 - dd, 18);
                        pe.Graphics.DrawLine(bufPen2, 25 + dd, 18, 44 - dd, 18);
                        pe.Graphics.DrawLine(bufPen3, 48 + dd, 18, 67 - dd, 18);
                    }
                    else
                    {
                        pe.Graphics.DrawLine(bufPen1, 1 + dd, 29, 37 - dd, 29);
                        pe.Graphics.DrawLine(bufPen2, 44 + dd, 29, 80 - dd, 29);
                        pe.Graphics.DrawLine(bufPen3, 84 + dd, 29, 120 - dd, 29);
                    }


                    SelPen.Dispose();
                    NormPen.Dispose();
                }
 
            }   
        }

        //
        private bool m_bPrevNeedBlink = false;
        public SoundPlayer m_notification;

		private void _refresh_Label()
		{
			if (m_form1 != null)
			{
                bool bNeedBlink = false;
                bool bNeedBlink2 = false;
                TimeSpan dt = m_form1.TimeLeft;
                TimeSpan dt2 = default(TimeSpan);
                if(m_AttentionBlinkBegin != default(DateTime))
                    dt2 = DateTime.Now - m_AttentionBlinkBegin;

                Color clr = System.Drawing.Color.White;

                if (dt < TimeSpan.Zero)
                {
                    bNeedBlink = true;
                }
                if (m_form1.Paused && dt2 != default(TimeSpan) && dt2 >= TimeSpan.Zero)// && dt2 <= new TimeSpan(0, 0, 15))
                {
                    bNeedBlink2 = true;
                }

                //start sound on end of timer
                if (bNeedBlink && !m_bPrevNeedBlink)
                {
                    try
                    {
                        int sound = ParamsServ.Inst.GetSound(1);

                        if (sound > 0)
                            play_sound();
                        else
                            stop_sound();
                    }
                    catch (Exception)
                    {
                    }
                }

                //stop sound if needed
                if (!bNeedBlink && m_bPrevNeedBlink)
                {
                    if (m_notification != null)
                        m_notification.Stop();
                }
                m_bPrevNeedBlink = bNeedBlink;

                //find label's color
                if (bNeedBlink && bNeedBlink2)
                    clr = Color.Orange;
                else if (bNeedBlink)
                    clr = Color.Red;
                else if (bNeedBlink2)
                    clr = Color.Yellow;

                //
                if (dt < TimeSpan.Zero)
                    dt = -dt;
                else
                    dt.Add(new TimeSpan(0, 0, 1));

                //set text and color
                this.label1.ForeColor = clr;
                this.label1.Text = string.Format("{0:00}:{1:00}:{2:00}", dt.Hours, dt.Minutes, dt.Seconds);

                //process blinking
                bool bHide = false;
                if (bNeedBlink && !m_form1.Paused)
                    bHide = (DateTime.Now.Millisecond % 500 < 250);
                else if (bNeedBlink2)
                    bHide = (DateTime.Now.Millisecond % 500 < 100);

                if (bHide)
                    this.label1.Hide();
                else
                    this.label1.Show();

                TimeSpan dt3 = m_form1.TimeSpend;
                this.label2.Text = string.Format("{0:00}:{1:00}:{2:00}:{3:000}", dt3.Hours, dt3.Minutes, dt3.Seconds, dt3.Milliseconds);

                TimeSpan dt4 = default(TimeSpan);
                if (m_form1.GlobalStartTime != default(DateTime))
                    dt4 =(DateTime.Now - m_form1.GlobalStartTime);

                int hours = dt4.Hours;
                hours += dt4.Days * 24;
                this.label3.Text = string.Format("{0:00}:{1:00}:{2:00}", hours, dt4.Minutes, dt4.Seconds);
            }
		}

        DateTime m_AttentionBlinkBegin;
        public void blink_OnAttention(bool bBlink = true)
        {
            m_AttentionBlinkBegin = bBlink ? DateTime.Now : default(DateTime);
        }

		private void Form2_Activated(object sender, EventArgs e)
		{
        }

		private void timer1_Tick(object sender, EventArgs e)
		{
			_refresh_Label();
            CheckReminders();
            m_form1.refresh_reminders_menu_texts();
        }

        //move to default position
        public void move_DefaultPos()
        {
            int buf = ParamsServ.Inst.GetWndSizeType();
            WINDOW_SIZE_TYPE wnd_size_type_from_reg = (buf == 0) ? WINDOW_SIZE_TYPE.WSZ_SMALL : WINDOW_SIZE_TYPE.WSZ_CLASSIC;
            reset_wnd_size(wnd_size_type_from_reg);

            //move_RightBtm();
            //move_CenterTop();
            move_FromRegistry();
            /*Screen scn = Screen.FromPoint(this.Location);
            Point p = new Point((scn.WorkingArea.Right - this.Width) / 2, (scn.WorkingArea.Bottom - this.Height) / 2);
            this.Location = p;*/
        }

        public void move_CenterTop()
        {
            Screen scn = Screen.FromPoint(this.Location);
            Point p = new Point((scn.WorkingArea.Right - this.Width) / 2, scn.WorkingArea.Top);
            this.Location = p;
        }

		public void move_RightBtm()
		{
			Screen scn = Screen.FromPoint(this.Location);
            Point p = new Point(scn.WorkingArea.Right - this.Width, scn.WorkingArea.Bottom - this.Height);
			this.Location = p;
		}

        public void move_FromRegistry()
        {
            int x = ParamsServ.Inst.GetWindosPosX(-999);
            int y = ParamsServ.Inst.GetWindosPosY(-999);
            if (y == -999 || x == -999)
                move_CenterTop();
            else
                this.Location = new Point(x, y);
        }

        public void refresh_BtnImage()
        {
            if (m_cur_wnd_size_type == WINDOW_SIZE_TYPE.WSZ_CLASSIC)
            {
                if (m_form1.Paused)
                    button1.BackgroundImage = zTimer.Properties.Resources.play;
                else
                    button1.BackgroundImage = zTimer.Properties.Resources.pause;
            }
            else
            {
                if (m_form1.Paused)
                    button1.BackgroundImage = zTimer.Properties.Resources.play_small;
                else
                    button1.BackgroundImage = zTimer.Properties.Resources.pause_small;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_form1.Paused = !m_form1.Paused;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (label1.SelectedVal == -1 || label1.SelectedVal == 1)
                m_form1.TimeLeft += new TimeSpan(0, 1, 0);
            else
            if (label1.SelectedVal == 0)
                m_form1.TimeLeft += new TimeSpan(1, 0, 0);
            else
                m_form1.TimeLeft += new TimeSpan(0, 0, 1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (label1.SelectedVal == -1 || label1.SelectedVal == 1)
                m_form1.TimeLeft -= new TimeSpan(0, 1, 0);
            else
                if (label1.SelectedVal == 0)
                    m_form1.TimeLeft -= new TimeSpan(1, 0, 0);
                else
                    m_form1.TimeLeft -= new TimeSpan(0, 0, 1);
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void MouseWheelEvent(object sender, MouseEventArgs e)
        {
            double d = e.Delta > 0 ? 0.1 : -0.1;
            if (this.Opacity >= 0.3 && d < 0 || this.Opacity <= 0.9 && d > 0)
                this.Opacity += d;
        }

        private void Form2_Shown(object sender, EventArgs e)
        {
            _refresh_Label();
            //move_RightBtm();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form2_MouseDown(object sender, MouseEventArgs e)
        {
            if (m_eDragging == DRAG_TYPE.DT_NONE && e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                m_eDragging = DRAG_TYPE.DT_BEGIN;
                m_LMDown = e.Location;
                m_LMDownScreen = this.PointToScreen(m_LMDown);
            }

            if (e.Button == System.Windows.Forms.MouseButtons.Middle)
                this.Hide();
        }

        private void Form2_MouseUp(object sender, MouseEventArgs e)
        {
            m_eDragging = DRAG_TYPE.DT_NONE;

            //
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                m_form1.show_ContextMenu(this.PointToScreen(e.Location));
            }
        }

        private void Form2_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_eDragging != DRAG_TYPE.DT_NONE)
            {
                Point cur = this.PointToScreen(e.Location);
                int dx = cur.X - m_LMDownScreen.X;
                int dy = cur.Y - m_LMDownScreen.Y;
                int dd = (int)Math.Sqrt(dx * dx + dy * dy);

                cur.X -= m_LMDown.X;
                cur.Y -= m_LMDown.Y;

                if (this.Location != cur && (dd >= 10 || m_eDragging == DRAG_TYPE.DT_DRAG))
                {
                    this.Location = cur;
                    m_eDragging = DRAG_TYPE.DT_DRAG;
                    move_reminder_forms();
                }
            }
        }

        private void Form2_MouseLeave(object sender, EventArgs e)
        {
            m_eDragging = DRAG_TYPE.DT_NONE;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            m_form1.PauseOnLock = !m_form1.PauseOnLock;
            if(m_form1.PauseOnLock)
                button5.BackgroundImage = zTimer.Properties.Resources.lockpause;
            else
                button5.BackgroundImage = zTimer.Properties.Resources.lockpause_dis;
        }

        private void Form2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //!!!TEST!!!!
            //if (e.Button == System.Windows.Forms.MouseButtons.Left)
            //{
            //    _show_reminder(0);
            //    return;
            //}


            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                m_form1.Paused = !m_form1.Paused;
                label1.SelectedVal = -1;
            }
        }

        //private bool m_WasMouseClick = false;
        private void Form2_MouseClick(object sender, MouseEventArgs e)
        {
            //m_WasMouseClick = true;
            _on_MouseClick(e);
        }

        private void _on_MouseClick(MouseEventArgs e)
        {
            //reset blinking with yellow
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                m_AttentionBlinkBegin = default(DateTime);

            //stop alarm
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (m_notification != null)
                    m_notification.Stop();
            }

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Point clickScr = this.PointToScreen(new Point(e.X, e.Y));
                Point p = this.label1.PointToClient(clickScr);

                int dx = clickScr.X - m_LMDownScreen.X;
                int dy = clickScr.Y - m_LMDownScreen.Y;
                int dd = (int)Math.Sqrt(dx * dx + dy * dy);

                if (dd <= 5)       //если не двигали сильно
                {
                    //if (p.X >= 0 && p.X < label1.Width - 20 && p.Y >= label1.Height-10 && p.Y < label1.Height)
                    if (p.X >= 0 && p.X < label1.Width && p.Y >= label1.Height - 10 && p.Y < label1.Height)
                    {
                        int k = p.X / (label1.Width / 3);
                        if (k < 0)
                            k = 0;
                        if (k > 2)
                            k = 2;
                        if (label1.SelectedVal != k)
                            label1.SelectedVal = k;
                        else
                            label1.SelectedVal = -1;
                    }
                    else
                        label1.SelectedVal = -1;
                }
            }
        }

        private void _save_WindowPos()
        {
            ParamsServ.Inst.SetWindosPosX(this.Location.X);
            ParamsServ.Inst.SetWindosPosY(this.Location.Y);
        }

        private void Form2_Move(object sender, EventArgs e)
        {
            _save_WindowPos();
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
        }

        private DateTime m_PrevReminderInvld;
        private void CheckReminders()
        {
            if (m_form1 != null && this.Visible)
            {
                bool need_repaint = false;
                TimeSpan tt = new TimeSpan(0, 0, 0, 0, 200);
                if (m_PrevReminderInvld == default(DateTime) || (DateTime.Now - m_PrevReminderInvld) > tt)
                {
                    need_repaint = true;
                    m_PrevReminderInvld = DateTime.Now;
                }

                //
                for (int i = 0; i < m_form1.m_reminders.Count; i++)
                {
                    REMINDER_STRUCT r = m_form1.m_reminders[i];
                    if (r.reminder_form == null || !r.reminder_form.Visible)
                    {
                        if (!r.Paused)
                        {
                            TimeSpan dt = r.ReminderTimeLeft;
                            if (dt <= TimeSpan.Zero)
                                _show_reminder(i);
                        }
                    }
                    else
                    {
                        if (need_repaint)
                        {
                            r.reminder_form.RecalcColor();
                            r.reminder_form.Invalidate();
                            r.reminder_form.Update();
                        }
                    }
                }
            }
        }

        private void _show_reminder(int reminder_num)
        {
            if (reminder_num < 0 || reminder_num >= m_form1.m_reminders.Count)
                return;

            REMINDER_STRUCT r = m_form1.m_reminders[reminder_num];

            if (r.reminder_form == null)
                r.reminder_form = new ReminderForm(m_form1, this, r);
            r.reminder_form.m_Hidden = false;
            r.reminder_form.Show();

            move_reminder_forms();
        }

        public void move_reminder_forms()
        {
            int cur = 0;

            if (m_addreminder_form != null && m_addreminder_form.Visible && !m_addreminder_form.m_bClosed)
            {
                int x = this.Location.X + this.Width - m_addreminder_form.Width;
                int y = this.Location.Y + this.Height + cur;
                m_addreminder_form.Location = new Point(x, y);
                cur += m_addreminder_form.Height;
            }

            for (int i = 0; i < m_form1.m_reminders.Count; i++)
            {
                REMINDER_STRUCT r = m_form1.m_reminders[i];
                if (r.reminder_form != null && r.reminder_form.Visible)
                {
                    int x = this.Location.X + this.Width - r.reminder_form.Width;
                    int y = this.Location.Y + this.Height + cur;
                    r.reminder_form.Location = new Point(x, y);
                    cur += r.reminder_form.Height;
                }
            }
        }

        private void Form2_VisibleChanged(object sender, EventArgs e)
        {
            if (m_addreminder_form != null && !m_addreminder_form.m_bClosed)
                m_addreminder_form.Visible = this.Visible;
            for (int i = 0; i < m_form1.m_reminders.Count; i++)
            {
                if (m_form1.m_reminders[i].reminder_form != null && !m_form1.m_reminders[i].reminder_form.m_Hidden)
                    m_form1.m_reminders[i].reminder_form.Visible = this.Visible;
            }
        }

        public void show_addreminder_form()
        {
            if (m_addreminder_form == null)
                m_addreminder_form = new ReminderAdd(m_form1, this);

            m_addreminder_form.set_default_values();
            m_addreminder_form.Show();
            m_addreminder_form.Visible = true;

            move_reminder_forms();
        }

        //
        private ReminderAdd m_addreminder_form;

        private void Form2_Resize(object sender, EventArgs e)
        {
            int h = (m_cur_wnd_size_type == WINDOW_SIZE_TYPE.WSZ_SMALL) ? 19 : 42;
            if (this.Height != h)
                this.Height = h;
        }

        public void play_sound()
        {
            if (m_notification == null)
            {
                m_notification = new SoundPlayer();
                m_notification.Stream = Properties.Resources.ztBeepLoud;
            }

            if (m_notification != null)
                m_notification.PlayLooping();
        }
        public void stop_sound()
        {
            if (m_notification != null)
                m_notification.Stop();
        }

        public enum WINDOW_SIZE_TYPE
        {
            WSZ_SMALL,
            WSZ_CLASSIC
        };

        static WINDOW_SIZE_TYPE m_cur_wnd_size_type = WINDOW_SIZE_TYPE.WSZ_SMALL;

        public void reset_wnd_size(WINDOW_SIZE_TYPE wnd_size_type)
        {
            if (wnd_size_type == m_cur_wnd_size_type)
                return;

            m_cur_wnd_size_type = wnd_size_type;
            ParamsServ.Inst.SetWndSizeType(m_cur_wnd_size_type == WINDOW_SIZE_TYPE.WSZ_SMALL ? 0 : 1);

            int new_height = 0;
            int new_width = 0;
            if (m_cur_wnd_size_type == WINDOW_SIZE_TYPE.WSZ_SMALL)
            {
                new_height = 19;
                new_width = 250;
                this.label1.Location = new System.Drawing.Point(71, -1);
                this.label1.Size = new System.Drawing.Size(76, 21);
                this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                this.button5.Location = new System.Drawing.Point(223, 4);
                this.button4.Location = new System.Drawing.Point(233, 4);
                this.button3.Location = new System.Drawing.Point(199, 4);
                this.button2.Location = new System.Drawing.Point(212, 4);
                this.button1.Location = new System.Drawing.Point(186, 4);
                this.button1.Size = new System.Drawing.Size(10, 10);
                this.label2.Location = new System.Drawing.Point(0, 2);
                this.label3.Location = new System.Drawing.Point(138, 2);
                this.label3.Size = new System.Drawing.Size(49, 13);
            }
            else
            {
                new_height = 42;
                new_width = 170;
                this.label1.Location = new System.Drawing.Point(4, 9);
                this.label1.Size = new System.Drawing.Size(129, 32);
                this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                this.button5.Location = new System.Drawing.Point(158, 28);
                this.button4.Location = new System.Drawing.Point(158, 2);
                this.button3.Location = new System.Drawing.Point(129, 29);
                this.button2.Location = new System.Drawing.Point(129, 14);
                this.button1.Location = new System.Drawing.Point(139, 6);
                this.button1.Size = new System.Drawing.Size(20, 30);
                this.label2.Location = new System.Drawing.Point(0, 0);
                this.label3.Location = new System.Drawing.Point(90, 0);
                this.label3.Size = new System.Drawing.Size(70, 13);
            }

            if (this.Height != new_height || this.Width != new_width)
                this.ClientSize = new System.Drawing.Size(new_width, new_height);

            refresh_BtnImage();
        }
   }
}