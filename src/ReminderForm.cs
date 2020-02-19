using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace zTimer
{
    public partial class ReminderForm : Form
    {
        private Form1 m_form1 = null;
        private Form2 m_form2 = null;
        private REMINDER_STRUCT m_reminder = null;
        public bool m_Hidden = false;

        public ReminderForm(Form1 form1, Form2 form2, REMINDER_STRUCT reminder)
        {
            InitializeComponent();
            m_form1 = form1;
            m_form2 = form2;
            m_reminder = reminder;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_reminder.ResetReminderTime();
            m_Hidden = true;
            this.Hide();
            if (m_form2 != null)
            {
                m_form2.move_reminder_forms();
                if (m_reminder.m_sound)
                    m_form2.stop_sound();
            }
        }

        Random m_rand = new Random();
        private int m_color_red = -1;
        private int m_color_green = -1;
        private int m_color_blue = -1;

        public void RecalcColor()
        {
            int min_val = 80;
            m_color_red = min_val + m_rand.Next(255 - min_val);
            m_color_green = min_val + m_rand.Next(255 - min_val);
            m_color_blue = min_val + m_rand.Next(255 - min_val);
        }


        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            if (m_color_red == -1)
                RecalcColor();

            Color clr = Color.FromArgb(m_color_red, m_color_green, m_color_blue);
            Brush brush = new SolidBrush(clr);

            Rectangle rect = this.ClientRectangle;
            pe.Graphics.FillRectangle(brush, rect);

            try
            {
                using (Font font = new Font("Arial", 20))
                {
                    string s = m_reminder.m_text;
                    pe.Graphics.DrawString(s, font, Brushes.Black, new Point(5, 20));
                }
            }
            catch { }


            brush.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            m_form1.delete_Reminder(m_reminder);
            m_Hidden = true;
            if (m_form2 != null)
            {
                m_form2.move_reminder_forms();
                m_form2.stop_sound();
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void minuteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _skip_time(3);
        }

        private void minuteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _skip_time(1);
        }

        private void minutesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _skip_time(5);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Point p = button3.Location;
            p.X += button3.Width;
            p.Y += button3.Height;
            this.contextMenuStrip1.Show(this.PointToScreen(p));

            if (m_form2 != null && m_reminder.m_sound)
                m_form2.stop_sound();
        }

        private void _skip_time(int minutes)
        {
            if (m_reminder != null)
            {
                m_reminder.SkipTime = new TimeSpan(0, minutes, 0);

                m_Hidden = true;
                this.Hide();
                if (m_form2 != null)
                    m_form2.move_reminder_forms();
            }
        }

        private void minutesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _skip_time(10);
        }

        private void minutesToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            _skip_time(15);
        }

        private void minutesToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            _skip_time(20);
        }

        private void minutesToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            _skip_time(30);
        }

        private void hourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _skip_time(60);
        }

        private void ReminderForm_Shown(object sender, EventArgs e)
        {
            //if (m_form2 != null && m_reminder.m_sound)
              //  m_form2.play_sound();
        }

        private void ReminderForm_Load(object sender, EventArgs e)
        {

        }

        private void ReminderForm_Click(object sender, EventArgs e)
        {
            if (m_form2 != null && m_reminder.m_sound)
                m_form2.stop_sound();
        }

        private void ReminderForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                if (m_form2 != null && m_reminder.m_sound)
                    m_form2.play_sound();
            }
        }
    }
}
