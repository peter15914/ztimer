using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace zTimer
{
    public partial class ReminderAdd : Form
    {
        private Form1 m_form1 = null;
        private Form2 m_form2 = null;
        public bool m_bClosed = true;

        public ReminderAdd(Form1 form1, Form2 form2)
        {
            InitializeComponent();
            m_form1 = form1;
            m_form2 = form2;
        }

        public void set_default_values()
        {
            m_bClosed = false;
            this.textBox1.Text = ParamsServ.Inst.GetLastRmndrText("Reminder!");
            this.checkBox1.Checked = (ParamsServ.Inst.GetLastRmndrSound(0) != 0);

            int val = ParamsServ.Inst.GetLastRmndrMin(60);
            if(val >= this.numericUpDown1.Minimum && val <= this.numericUpDown1.Maximum)
                this.numericUpDown1.Value = val;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_bClosed = true;
            _add_reminder();
            this.Hide();
        }

        private void _add_reminder()
        {
            int minutes = (int)this.numericUpDown1.Value;
            string text = this.textBox1.Text;
            bool sound = this.checkBox1.Checked;

            ParamsServ.Inst.SetLastRmndrMin(minutes);
            ParamsServ.Inst.SetLastRmndrText(text);
            ParamsServ.Inst.SetLastRmndrSound(sound ? 1 : 0);

            m_form1.add_reminder(minutes, text, sound);
            
            if(m_form2 != null)
                m_form2.move_reminder_forms();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            m_bClosed = true;
            if (m_form2 != null)
                m_form2.move_reminder_forms();
        }
    }
}
