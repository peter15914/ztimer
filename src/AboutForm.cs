using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace zTimer
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void goto_Url(string url)
        {
            try
            {
                System.Diagnostics.Process.Start(url);
            }
            catch (Exception)
            {
                //string s = String.Format("{0} Exception caught.", e);
                //s = e.GetType().ToString();
                //MessageBox.Show(s, "zTimer");
            }            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            goto_Url(linkLabel1.Text);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            goto_Url(linkLabel2.Text);
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            goto_Url(linkLabel3.Text);
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
