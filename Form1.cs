using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinApp_SessionTimeOut
{
    public partial class Form1 : Form, IMessageFilter
    {
        public Form1()
        {
            SetSessionConfigSettings();
            InitializeComponent();
        }

        private void SetSessionConfigSettings()
        {
            AutoLogOffHelper.LogOffTime = 1;
            AutoLogOffHelper.MakeAutoLogOff eventHandler = new AutoLogOffHelper.MakeAutoLogOff(AutoLogOff_MakeAutoLogOff);
            AutoLogOffHelper.MakeAutoLogOffEvent += eventHandler;
            AutoLogOffHelper.StartAutoLogOffOption();
            Application.AddMessageFilter(this);
        }

        private void AutoLogOff_MakeAutoLogOff()
        {
            label1.Text = "Session closed.";
        }

        public bool PreFilterMessage(ref Message m)
        {
            if ((m.Msg >= 0x0200 && m.Msg <= 0x020A) || (m.Msg <= 0x0106 && m.Msg >= 0x00A0) || (m.Msg == 0x00211))
            {
                AutoLogOffHelper.ResetLogOffSettings();
                string time = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss fff tt");
                label2.Text = "Main " + time;
                label3.Text = "Timer reset on the user activity at " + time;
                label4.Text = "Last user activity type OS message 0x" + m.Msg.ToString("X");
            }
                return false;
        }
    }
}
