using RegalPrinter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KEE.NavMenu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        List<WorkOrder> woList = new List<WorkOrder>();
        private void GetWoData()
        {
            woList = new List<WorkOrder>();
            woList.Add(new WorkOrder());
            woList.Add(new WorkOrder() {  Class="夜班", Item="8"});
            dataGridView1.DataSource = woList;
            dataGridView1.Columns[0].HeaderText = "日期";
            dataGridView1.Columns[1].HeaderText = "班别";
            dataGridView1.Columns[1].Width = 40;
            dataGridView1.Columns[2].HeaderText = "料号";
            dataGridView1.Columns[2].Width = 150;
            dataGridView1.Columns[3].HeaderText = "工单号";
            dataGridView1.Columns[3].Width = 120;
            dataGridView1.Columns[4].HeaderText = "排程单号";
            dataGridView1.Columns[4].Width = 120;
            dataGridView1.Columns[5].HeaderText = "项次";
            dataGridView1.Columns[5].Width = 40;
            dataGridView1.Columns[6].HeaderText = "选我你就点我吧";
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbClass.Items.Add("白班");
            cmbClass.Items.Add("夜班");
            GetWoData();
        }
        bool isClick = false;
        private void iBtnHomePage_Click(object sender, EventArgs e)
        {
           
        }

        private void iBtnSysSetting_Click(object sender, EventArgs e)
        {
           
        }

        private void iBtnStaTest_Click(object sender, EventArgs e)
        {
           
        }

        private void iBtnUserManage_Click(object sender, EventArgs e)
        {
            
        }

        private void iBtnMotionSetting_Click(object sender, EventArgs e)
        {
            
        }

        private void iBtnIOMonitor_Click(object sender, EventArgs e)
        {
        }

        private void iBtnVisionSetting_Click(object sender, EventArgs e)
        {
            
        }

        static bool test = false;
        bool isTimeOut = true;
        CancellationTokenSource tokenSource = null;

        private void tnFeedFromInjectAxias_Click(object sender, EventArgs e)
        {
            test = false;
            tokenSource = new CancellationTokenSource(10000);
            tokenSource.Token.Register(new Action(() => { DialogResult = isTimeOut? MessageBox.Show("1"): MessageBox.Show("2"); }));
            Task t = new Task(new Action(() =>
            {
                while (!tokenSource.Token.IsCancellationRequested && !test)
                {
                    Thread.Sleep(5);
                    Application.DoEvents();
                    if (test)
                    {
                        isTimeOut = false;
                        tokenSource.Cancel();
                    }
                }
            }), tokenSource.Token);
            t.Start();
        }

        private void tnFeedAxias_Click(object sender, EventArgs e)
        {
            
            tokenSource.Cancel();
        }

        private void tnFeedRAxias_Click(object sender, EventArgs e)
        {
            test = true;
        }

        static string ceshi ="1";
        private void button1_Click(object sender, EventArgs e)
        {
            while (true)
            {
                if (isClick)
                {
                    if (istest)
                    {
                        return;
                    }
                    
                }
                Console.WriteLine("haha");
                Thread.Sleep(500);
                Application.DoEvents();
            }
            //int a = Convert.ToInt32(textBox1.Text);
            //textBox2.Text = Convert.ToString(a, 2).PadLeft(32, '0');
            //List<char> bb = Convert.ToString(a, 2).PadLeft(32, '0').Reverse().ToList();

            //new MotionCommon().DoMyAction(10000,"超时1", null, "1->2", null, null, () => ceshi == "2");
            //new MotionCommon().DoMyAction(10000,"超时2", null, "2->3", null, null, () => ceshi == "3");
            //new MotionCommon().DoMyAction(10000,"超时3", null, "3->4", null, null, () => ceshi == "4");
            //new MotionCommon().DoMyAction(10000,"超时4", null, "4->5", null, null, () => ceshi == "5");
            //new MotionCommon().DoMyAction(10000,"超时5", null, "5->6", null, null, () => ceshi == "6");
            //new MotionCommon().DoMyAction(10000,"超时6", null, "6->7", null, null, () => ceshi == "7");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            isClick = true;
            ceshi = textBox1.Text;
        }
        bool istest = false;
        bool istest1 = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invoke(new Action(() => 
            {
                if (!istest)
                {
                    istest = true;
                    btnPause.BackColor = Color.FromKnownColor(KnownColor.Control);
                }
                else 
                {
                    istest = false;
                    btnPause.BackColor = Color.Red;
                }
               
                Application.DoEvents();

            }));
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                if (!istest1)
                {
                    istest1 = true;
                    btnClearAlarm.BackColor = Color.FromKnownColor(KnownColor.Control);
                }
                else
                {
                    istest1 = false;
                    btnClearAlarm.BackColor = Color.MediumSeaGreen;
                }
                Application.DoEvents();

            }));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Ring r = new Ring();
            if (!r.PrinterWorkOffline("Ring 4012PIM"))
            {
                r.StartPrinter("Ring 4012PIM", "ring");
                r.FMT(1, "30", "8", false, 0, 1);//设定标签纸的长和宽
                r.DMD(1);
                r.DPD(1);
                r.ACL();
                r.FAG(2);
                r.DMX(txtBoxXOffset.Text.Trim(), txtBoxYOffset.Text.Trim(), 24, 24, 3, 6, 3, 0, textBox3.Text);

                r.PRT(1, 0, 1);
                //Thread.Sleep(300);
                r.EndPrinter();
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
            if (dataGridView1.SelectedRows == null)
            {
                return;
            }
            for (int i = 0; i < woList.Count; i++)
            {
                if (i == e.RowIndex)
                {
                    woList[i].IsChecked = true;
                    txtBoxSN.Text = woList[i].PartNo;
                    txtBoxWO.Text = woList[i].Wo;
                    cmbClass.SelectedItem = woList[i].Class;
                }
                else
                {
                    woList[i].IsChecked = false;
                }
            }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = woList;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmUserLogin frmUserLogin = new FrmUserLogin();
            frmUserLogin.ShowDialog();
            if (frmUserLogin.mUserID == "" || frmUserLogin.mPassword == "")
            {
                MessageBox.Show("用户名或密码为空，无法保存到局域网共享盘");
                return;
            }
            bool status = false;

            //连接
            status = TransFileClass.ConnectState(@"\\10.65.4.200\Renault", frmUserLogin.mUserID, frmUserLogin.mPassword);
            if (status)
            {
                //共享文件夹的目录
                DirectoryInfo theFolder = new DirectoryInfo(@"\\10.65.4.200\Renault\");
                string filename = theFolder.ToString();
                //执行方法
                TransFileClass.TransportLocalToRemote(@"D:\SourceCode\EMS.APICaller.dll", filename, "EMS.APICaller.dll");  //实现将本地文件写入到远程服务器
                //TransFileClass.TransportRemoteToLocal(@"D:\readme.txt", filename, "readme.txt");    //实现将远程服务器文件写入到本地
            }
            else
            {
                Console.WriteLine("未能连接！");
            }
            Console.WriteLine("成功");
            //Console.ReadKey();
            //string path = txtUploadFolder.Text + @"\test.xls";
            //if (TransFileClass.ConnectState(path, "Administrator", "Ab123456"))
            //{
                
            //}
            //else
            //{
            //    MessageBox.Show("登录局域网共享盘失败");
            //}
        }

        Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
        int cnt = 0;

        private void button5_Click(object sender, EventArgs e)
        {
            string data = $"按钮按下了{cnt+1}次";
            keyValuePairs.Add($"测试{cnt}", data);
            cnt++;
            listBox1.Items.Add(data);
        }
    }
}
