using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KEE.NavMenu
{
    public class MotionCommon
    {
        CancellationTokenSource myToken = null;
        public void DoMyAction(int timeout, string timeoutMessage, Action timeoutAct, string nextStepMessage, Action nextAct, Action curAct, Func<bool> breakWhile)
        {
            bool myTimeOut = true;
            myToken = new CancellationTokenSource(timeout);
            myToken.Token.Register(new Action(() =>
            {
                if (myTimeOut)
                {
                    Debug.WriteLine(timeoutMessage);
                    Debug.WriteLine(Thread.CurrentThread.ManagedThreadId.ToString());
                }
                else
                {
                    Debug.WriteLine(nextStepMessage);

                }
            }));
            Task.Factory.StartNew(new Action(() =>
            {
                while (!myToken.Token.IsCancellationRequested)
                {
                    Thread.Sleep(5);
                    Application.DoEvents();

                    if (breakWhile.Invoke())
                    {
                        myTimeOut = false; myToken.Cancel();
                    }
                }
            }), myToken.Token);

        }
    }
}
