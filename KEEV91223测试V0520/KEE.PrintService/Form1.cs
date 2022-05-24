using HY.Redis.Service;
using RegalPrinter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KEE.PrintService
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitParam();
        }
        string printX = "0";
        string printY = "0";
        string bConfigFilePath = @"D:\Data\Config\Config.ini";
        public delegate void CbDelegate<T1>(T1 obj1);

        static bool areaContinuePrintFlag = false;//区间连续打印功能
        private void btnSavePrintParam_Click(object sender, EventArgs e)
        {
            printX = txtPrintX.Text;
            printY = txtPrintY.Text;
            ClassINI.INI.INIWriteValue(bConfigFilePath, "打印机", "X方向起点", printX);
            ClassINI.INI.INIWriteValue(bConfigFilePath, "打印机", "Y方向起点", printY);
        }

        private void InitParam()
        {
            printX = ClassINI.INI.INIGetStringValue(bConfigFilePath, "打印机", "X方向起点", null);
            printY = ClassINI.INI.INIGetStringValue(bConfigFilePath, "打印机", "Y方向起点", null);
            txtPrintX.Text = printX;
            txtPrintY.Text = printY;
        }

        private void ShowMessage(string msg)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new CbDelegate<string>(this.ShowMessage), msg);
            }
            else
            {
                clearLsvRevicedMsg();
                ListViewItem item = new ListViewItem(new string[] { DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff"), msg });
                this.lsvRevicedMsg.Items.Insert(0, item);
            }
        }

        private void clearLsvRevicedMsg()
        {
            if (this.lsvRevicedMsg.Items.Count > 1000)
            {
                this.lsvRevicedMsg.Items.Clear();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txBoxStartSn.Text) && !string.IsNullOrEmpty(txBoxEndSn.Text) && !string.IsNullOrEmpty(tbPrintContext.Text))
            {
                int startSn = 0;
                int endSn = 0;
                if (!int.TryParse(txBoxStartSn.Text, out startSn))
                {
                    MessageBox.Show("请输入有效整数值");
                }
                if (!int.TryParse(txBoxEndSn.Text, out endSn))
                {
                    MessageBox.Show("请输入有效整数值");
                }
                
                if (endSn < startSn)
                {
                    MessageBox.Show("结束值应大于或等于起始值");
                }
                else
                {
                    btnPrint.Enabled = false;
                    int totalPrintSn = endSn - startSn + 1;
                    Task.Factory.StartNew(new Action(() =>
                    {
                        int i = 0;
                        
                        this.Invoke(new Action(() => {
                            while (i < totalPrintSn)
                            {
                                string sn = tbPrintContext.Text + startSn.ToString("00000");
                                ActionPrinttest(sn);
                                i++;
                                Application.DoEvents();
                            }
                            btnPrint.Enabled = true; 
                        }));
                    }));
                }
            }
            else
            {
                ActionPrinttest(tbPrintContext.Text);
            }
        }

        private void ActionPrinttest(string finalBarcode)
        {
            Ring r = new Ring();
            if (!r.PrinterWorkOffline("Ring 4012PIM #4"))
            {
                r.StartPrinter("Ring 4012PIM #4", "ring");
                r.FMT(1, "30", "8", false, 0, 1);//设定标签纸的长和宽
                r.DMD(1);
                r.DPD(1);
                r.ACL();
                r.FAG(2);
                r.DMX(printX, printY, 24, 24, 3, 6, 3, 0, finalBarcode);
                r.PRT(1, 0, 1);
                Thread.Sleep(10);
                r.EndPrinter();
            }
        }

        #region 打印服务
        object mesObj = new object();
        static object pollObj = new object();
        List<string> messageList = new List<string>();
        bool isStartListen = false;
        Thread thMain = null;
        /// <summary>
        /// 格式:Request:设备编号:OnOrOff
        /// </summary>
        private void PrintFinabarcode()
        {
            using (RedisListService service = new RedisListService())
            {
                service.Subscribe("PrintServices", (c, message, iRedisSubscription) =>
                {
                    if (message.Equals("exit"))
                        iRedisSubscription.UnSubscribeFromChannels("PrintServices");
                    lock (mesObj)
                    {
                        messageList.Add(message);
                    }
                });//blocking
            }
        }
        #endregion


        private void ListenMyPrintServices()
        {
            while (isStartListen)
            {
                try
                {
                    lock (pollObj)
                    {
                        if (messageList.Count == 0)
                        {
                            continue;
                        }
                        ShowMessage(messageList[0]);
                        ActionPrinttest(messageList[0]);
                        string[] messageArr = messageList[0].Split(':');
                        messageList.RemoveAt(0);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Application.DoEvents();
                    Thread.Sleep(100);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            isStartListen = true;
            thMain = new Thread(new ThreadStart(ListenMyPrintServices));
            thMain.IsBackground = true;
            thMain.Start();
            Task.Factory.StartNew(new Action(PrintFinabarcode));
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            isStartListen = false;
            if (thMain!=null)
            {
                thMain.Abort();
                thMain = null;
            }

        }
    }
}
