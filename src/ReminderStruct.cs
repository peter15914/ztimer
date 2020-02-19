using System;
using System.Collections.Generic;
using System.Text;

namespace zTimer
{
    public class REMINDER_STRUCT
    {
        public int m_minutes = 0;
        public string m_text;
        public bool m_sound = false;
        private DateTime m_ReminderTime;    //time, when 00:00:00 will happen
        public ReminderForm reminder_form;
        private bool m_bPaused = false;
        private TimeSpan m_PausedTimeLeft;

        public REMINDER_STRUCT()
        {
        }

        public REMINDER_STRUCT(int minutes, string text, bool sound)
        {
            m_minutes = minutes;
            m_text = text;
            m_sound = sound;
        }

        public void ResetReminderTime()
        {
            TimeSpan dt = new TimeSpan(0, m_minutes, 0);
            //!!!TEST
            //dt = new TimeSpan(0, 10, 0);

            m_ReminderTime = DateTime.Now + dt;
            m_PausedTimeLeft = dt;
        }

        private DateTime ReminderTime
        {
            get
            {
                if (m_ReminderTime == default(DateTime))
                    ResetReminderTime();
                DateTime ret = m_ReminderTime;
                return ret;
            }
        }

        public TimeSpan ReminderTimeLeft
        {
            get
            {
                if (!m_bPaused)
                {
                    TimeSpan dt = ReminderTime - DateTime.Now;
                    if (dt < TimeSpan.Zero)
                        dt = TimeSpan.Zero;
                    return dt;
                }
                else
                    return m_PausedTimeLeft;
            }
        }

        public TimeSpan SkipTime
        {
            set
            {
                if (ReminderTimeLeft > TimeSpan.Zero)
                    m_ReminderTime += value;
                else
                {
                    m_ReminderTime = DateTime.Now + value;
                    //
                }
            }
        }

        public bool Paused
        {
            get { return m_bPaused; }
            set
            {
                if (value && !m_bPaused)
                    m_PausedTimeLeft = ReminderTimeLeft;
                else
                if (!value && m_bPaused)
                    m_ReminderTime = DateTime.Now + m_PausedTimeLeft;
                m_bPaused = value;
            }

        }

    };
}
