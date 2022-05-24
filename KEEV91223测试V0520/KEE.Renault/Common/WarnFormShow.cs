using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KEE.Renault.Common
{
    public static  class WarnFormShow
    {
        public static frmNotify ctrlMsg = null;
        /// <summary>
        /// 警告框
        /// </summary>
        /// <param name="msg"></param>
        public static void ShowPromptMessage(Form thisForm, string msg)
        {
            if (thisForm != null)
            {
                thisForm.Invoke(new Action(() =>
                {
                    if (ctrlMsg != null)
                    {
                        ctrlMsg.Close();
                        Thread.Sleep(500);
                        Application.DoEvents();
                    }
                    ctrlMsg = new frmNotify();
                    ctrlMsg.SetMsg(msg);
                    ctrlMsg.TopMost = true;
                    ctrlMsg.Show();
                }));
            }
        }
    }
}
