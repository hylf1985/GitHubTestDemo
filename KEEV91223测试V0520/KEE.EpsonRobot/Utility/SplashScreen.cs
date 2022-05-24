using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KEE.EpsonRobot.Utility
{
    public class SplashScreen
    {
        private static object _obj = new object();

        private static Form _SplashForm = null;

        private static Label _SplashLabel = new Label();
        
        private static Thread _SplashThread = null;

        private delegate void changeFormTextDelegate(string s);

        #region 创建splash窗体实例
        public static void Show(Type splashFormType)
        {
            if (_SplashThread != null)
                return;
            if (splashFormType == null)
            {
                throw (new Exception());
            }
            _SplashThread = new Thread(new ThreadStart(delegate()
            {
                CreateInstance(splashFormType);
                Application.Run(_SplashForm);
            }));
            _SplashThread.IsBackground = true;
            _SplashThread.SetApartmentState(ApartmentState.STA);
            _SplashThread.Start();
                      
        }

        private static void CreateInstance(Type FormType)
        {
            if (_SplashForm == null)
            {
                lock (_obj)
                {
                    object obj = FormType.InvokeMember(null,
                        BindingFlags.DeclaredOnly |
                        BindingFlags.Public |
                        BindingFlags.NonPublic|
                        BindingFlags.Instance |
                        BindingFlags.CreateInstance,null,null,null);
                    _SplashForm = obj as Form;
                    _SplashForm.TopMost = true;
                    _SplashForm.ShowInTaskbar = false;
                    _SplashForm.BringToFront();
                    _SplashForm.Size = new System.Drawing.Size(378, 310);
                    _SplashForm.StartPosition = FormStartPosition.CenterScreen;
                    _SplashForm.Controls.Add(SplashScreen._SplashLabel);
                    _SplashLabel.Location = new System.Drawing.Point(25,286);
                    _SplashLabel.Name = "lable1";
                    _SplashLabel.AutoSize = false;
                    _SplashLabel.Size = new System.Drawing.Size(500,15);
                    _SplashLabel.TabIndex = 1;
                    _SplashLabel.Text = "界面正在加载中....";
                    if (_SplashForm == null)
                    {
                        throw (new Exception());
                    }                        
               
                }
            }
        }
        #endregion

        public static void ChangeTitle(string title)
        {
            _SplashLabel.Text = title.ToString();
        }

        ////public static void ChangeLabelTitle(string status)
        //{
        //    changeFormTextDelegate de = new changeFormTextDelegate(ChangeLabelText);
        //    _SplashLabel.Invoke(de, status);
        //}

        //private static void ChangeLabelText(string title)
        //{
        //    _SplashLabel.Text = title.ToString();
        //}

        public static void Close()
        {
            if (_SplashThread == null || _SplashForm == null) return;
            try
            {
                _SplashForm.Invoke(new MethodInvoker(_SplashForm.Close));
            }
            catch (Exception)
            { 
            }
            _SplashThread = null;
            _SplashForm = null;
        }

    }
}
