using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinApp_SessionTimeOut
{
    class AutoLogOffHelper
    {
        public static int LogOffTime { get; set; }
        public static int Wait { get; set; }
        public static Timer timer = null;
        public AutoLogOffHelper()
        {

        }
        public delegate void MakeAutoLogOff();
        public static event MakeAutoLogOff MakeAutoLogOffEvent;
        public static void StartAutoLogOffOption()
        {
            Application.Idle += Application_Idle;
        }

        private static void Application_Idle(object sender, EventArgs e)
        {
            if (timer == null)
            {
                timer = new Timer();
                timer.Interval = LogOffTime * 60 * 1000;
                timer.Tick += Timer_Tick;
                timer.Enabled = true;
            }
            else
            {
                if (timer.Enabled == false)
                {   
                    timer.Enabled = true;
                }
            }
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            if (timer != null)
            {
                timer.Stop();
                timer = null;
                if (MakeAutoLogOffEvent != null)
                {
                    MakeAutoLogOffEvent();
                }
            }
        }
        public static void ResetLogOffSettings()
        {
            if (timer != null)
            {
                timer.Enabled = false;
                timer.Enabled = true;
            }
        }
    }
}
