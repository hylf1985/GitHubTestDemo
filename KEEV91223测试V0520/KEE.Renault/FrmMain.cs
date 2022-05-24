using Cognex.VisionPro;
using csIOC0640;
using csLTDMC;
using FontAwesome.Sharp;
using KEE.Renault.CamOperator;
using KEE.Renault.Common;
using KEE.Renault.MyMotion;
using KEE.Renault.Utility;
using SuperSocket.SocketBase;
using SuperSocket.SocketEngine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KEE.Renault
{
    public partial class MainFrm : Form
    {
        public MainFrm()
        {
            SplashScreen.Show(typeof(SplashForm));
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            #region 配置文件加载
            SplashScreen.ChangeTitle("-->开始执行加载配置文件");  //显示文字内容
            //加载方法
            GlobalVar.InitConfigPara();
            GlobalVar.InitIOPara();
            GlobalVar.InitRecipePara();
            Thread.Sleep(300);     
            SplashScreen.ChangeTitle("-->配置加载完成...");
            Thread.Sleep(300);
            #endregion 相机加载流程

            #region 相机加载
            SplashScreen.ChangeTitle("-->开始加载相机");
            //相机加载方法
            InitializeAcqusition();
            Thread.Sleep(300);
            if (!InspectionClass.camIsFind)  //如果相机没找到
            {
                SplashScreen.ChangeTitle("未找到相机.....");
                Thread.Sleep(300);
            }
            if (!InspectionClass.cameraIsMatch1)  //如果相机1序列号不匹配
            {
                SplashScreen.ChangeTitle("相机1序列号不匹配.....");
                Thread.Sleep(300);
            }
            if (!InspectionClass.cameraIsMatch2)  //如果相机2序列号不匹配
            {
                SplashScreen.ChangeTitle("相机2序列号不匹配.....");
                Thread.Sleep(300);
            }
            if (GlobalVar.isLoadAllParam)  //如果isLoadAllParam为True
            {
                SplashScreen.ChangeTitle("加载相机完成......");
                Thread.Sleep(300);
            }
            else    //如果isLoadAllParam不为True
            {
                SplashScreen.ChangeTitle("加载相机失败......");
                Thread.Sleep(1000);
                SplashScreen.Close();   //加载失败关闭窗体
                System.Environment.Exit(System.Environment.ExitCode);
            }
            Thread.Sleep(300);
            SplashScreen.ChangeTitle("-->相机加载完成...");
            Thread.Sleep(300);   //加载成功关闭窗体
            #endregion

            #region Visionpro加载流程
            SplashScreen.ChangeTitle("-->开始加载 VisionPro工具");
            string visionproErr = inspection.LoadAllTools();   //inspection中定义的相机加载的方法
            Thread.Sleep(300);
            if (GlobalVar.isLoadAllParam)
            {
                SplashScreen.ChangeTitle("-->加载visionpro工具完成...");
            }
            else
            {
                SplashScreen.ChangeTitle("-->加载visionpro工具失败,错误信息：" + visionproErr);
                DisposeAllCam();
                Thread.Sleep(3000);
                SplashScreen.Close();   //通讯失败关闭窗体
                System.Environment.Exit(System.Environment.ExitCode);   //
            }
            Thread.Sleep(300);
            #endregion

            #region 初始化IO卡
            SplashScreen.ChangeTitle("-->开始加载 IO卡驱动");
            string err = InitIO640();
            
            if (GlobalVar.isLoadAllParam)
            {
                SplashScreen.ChangeTitle($"-->{err}...");
            }
            else
            {
                SplashScreen.ChangeTitle($"-->{err},IO卡初始化失败...");
                Thread.Sleep(3000);
                SplashScreen.Close();   //通讯失败关闭窗体
                System.Environment.Exit(System.Environment.ExitCode);   //
            }
            Thread.Sleep(300);
            #endregion

            #region 初始化轴卡
            SplashScreen.ChangeTitle("-->开始加载 轴卡驱动");
            string err1 = InitLSAxiasCard();   //inspection中定义的相机加载的方法
            Thread.Sleep(300);
            if (GlobalVar.isLoadAllParam)
            {
                LoadLsAllAxiasParams();
                InitThreeAxiasSpeed();
                SplashScreen.ChangeTitle($"-->{err1}...");
            }
            else
            {
                SplashScreen.ChangeTitle($"-->{err1}");
                Thread.Sleep(3000);
                SplashScreen.Close();   //通讯失败关闭窗体
                System.Environment.Exit(System.Environment.ExitCode);   //
            }
            Thread.Sleep(300);
            #endregion

            #region 各轴再开机时回原点 ,主界面由人工回原点
            //SplashScreen.ChangeTitle("-->各轴开始回Home点...");
            //InitAxiasHomeParamsAndGoHome();
            //Thread.Sleep(300);
            //if (GlobalVar.isLoadAllParam)
            //{
            //    SplashScreen.ChangeTitle("-->各轴开始回Home点完成...");
            //}
            //else
            //{
            //    SplashScreen.ChangeTitle("-->回Home点失败，请检查");
            //    Thread.Sleep(3000);
            //    SplashScreen.Close();   //通讯失败关闭窗体
            //    System.Environment.Exit(System.Environment.ExitCode);   //
            //}
            //Thread.Sleep(300);
            #endregion

            #region 初始化Sokcet通讯
            SplashScreen.ChangeTitle("-->初始化Socket通讯");
            InitEpsonSocket();
            Thread.Sleep(300);
            if (GlobalVar.isLoadAllParam)
            {
                SplashScreen.ChangeTitle("-->Socket通讯初始化完成...");
            }
            else
            {
                SplashScreen.ChangeTitle("-->Socket通讯初始化失败");
                Thread.Sleep(3000);
                SplashScreen.Close();   //通讯失败关闭窗体
                System.Environment.Exit(System.Environment.ExitCode);   //
            }
            Thread.Sleep(300);
            #endregion 

            #region 加载初始界面信息数据
            SplashScreen.ChangeTitle("-->开始加载主界面数据...");
            InitLeftButton();
            GlobalVar.InitAxiasDI();
            GlobalVar.InitAxiasDO();
            LSParamCommonInit.InitAxiasEnums();
            LoadUser();
            LoginUserShowButton(GlobalVar.Level.操作员);
            cmbProList.SelectedItem = GlobalVar.curProName;
            lblVersion.Text = "版本 : " +  Application.ProductVersion.ToString();//System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() //System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString()
            Thread.Sleep(300);
            if (GlobalVar.isLoadAllParam)
            {
                SplashScreen.ChangeTitle("-->加载数据完成...");
            }
            Thread.Sleep(500);
            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[32].Card - 1), GlobalVar.lsAxiasDOs[32].PinDefinition, (ushort)0);
            SplashScreen.Close();
            #endregion
        }

        #region 变量声明
        private bool mainStart = false;
        private Panel leftBorderBtn;
        private IconButton currentBtn;
        List<string> listUsers = new List<string>() { };
        Thread MyIoListenThread = null;
        #endregion

        #region 工具栏按钮切换状态变换的方法
        private struct RGBColors  //panelMenu工具栏图标、字体颜色
        {
            public static Color color1 = Color.FromArgb(172, 126, 241);//第一个按钮
            public static Color color2 = Color.FromArgb(249, 118, 176);//第二个按钮......
            public static Color color3 = Color.FromArgb(253, 138, 114);
            public static Color color4 = Color.FromArgb(95, 77, 221);
            public static Color color5 = Color.FromArgb(150, 133, 200);
            public static Color color6 = Color.FromArgb(223, 220, 150);
            public static Color color7 = Color.FromArgb(187, 110, 119);
            public static Color color8 = Color.FromArgb(61, 120, 120);
        }
        //方法
        private void ActicateButton(object senderBtn, Color color)
        {
            if (senderBtn != null) //判断按钮是否已被点击过
            {
                DisableButton(); //复位动作
                //按钮
                currentBtn = (IconButton)senderBtn; //标记按钮已被点击
                currentBtn.BackColor = Color.FromArgb(31, 30, 68);//背景色置位
                currentBtn.ForeColor = color;//前景色置位（字体）
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;//文字位置
                currentBtn.IconColor = color;//图标颜色置位
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;//文字在图片之前
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;//对齐方式
                //点击按钮后左边呈现彩色边框
                leftBorderBtn.BackColor = color;//左侧彩色边框颜色
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);//左侧彩色变宽个位置，X值越大越靠右
                leftBorderBtn.Visible = true; //true显示；false不显示
                leftBorderBtn.BringToFront();//置顶显示
                //顶部显示栏图标跟随左侧工具栏图标变化 
                iconCurrentChildForm.IconChar = currentBtn.IconChar;//顶部图标与按钮图标保持一致
                iconCurrentChildForm.IconColor = color;//顶部图标颜色与按钮图标颜色保持一致                
            }
        }
        //切换按钮时复位之前点击的按钮显示状态
        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(31, 30, 68); //背景色复位（底色）
                currentBtn.ForeColor = Color.Gainsboro; // 前景色复位(字体颜色)
                currentBtn.TextAlign = ContentAlignment.MiddleLeft; //文字位置复位
                currentBtn.IconColor = Color.Gainsboro; ;//图标颜色复位
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;//图片在文字之前
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;//对齐方式
            }
        }
        //切换窗体显示方法
        private void InitLeftButton()
        {
            leftBorderBtn = new Panel(); //实例化一个容器
            leftBorderBtn.Size = new Size(7, 60); //点击工具栏按钮时左侧条框长宽尺寸 
            panelMenu.Controls.Add(leftBorderBtn);
            //Form
            //隐藏系统自带缩小、放大、关闭功能键及出题名称标题
            Text = string.Empty;
            ControlBox = false;
            DoubleBuffered = true; //解决按钮切换时的显示闪烁问题
            //使窗口最大化时可以显示任务栏状态，同时窗体上下左右各预留出可调整间隙
            //this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea; 
        }
        #endregion

        private void FormDisplay(string frmName)
        {
            Form form = FormFactory.CreateForm(frmName);
            form.MdiParent = this;
            form.Parent = panelDesktop;
            form.Dock = DockStyle.Fill;
            form.WindowState = FormWindowState.Maximized;
            form.TopLevel = false;
            form.BringToFront();
            form.Show();
            lblTitleChildForm.Text = form.Text;
        }

        #region 窗体事件


        private void MainFrm_Load(object sender, EventArgs e)
        {
            mainStart = true;
            MyIoListenThread = new Thread(new ThreadStart(ListenIOStatus));
            MyIoListenThread.IsBackground = true;
            MyIoListenThread.Start();
            
            //黄灯
            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[10].Card - 1), GlobalVar.lsAxiasDOs[10].PinDefinition, (ushort)0);//三色灯黄灯
        }
        private void btnExit_MouseHover(object sender, EventArgs e)
        {
            btnExit.BackColor = Color.DarkSlateGray;
        }

        private void btnExit_MouseLeave(object sender, EventArgs e)
        {
            btnExit.BackColor = Color.Transparent;
        }

        private void btnMaximize_MouseHover(object sender, EventArgs e)
        {
            btnMaximize.BackColor = Color.DarkSlateGray;
        }

        private void btnMaximize_MouseLeave(object sender, EventArgs e)
        {
            btnMaximize.BackColor = Color.Transparent;
        }

        private void btnMinmize_MouseHover(object sender, EventArgs e)
        {
            btnMinmize.BackColor = Color.DarkSlateGray;
        }

        private void btnMinmize_MouseLeave(object sender, EventArgs e)
        {
            btnMinmize.BackColor = Color.Transparent;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnHome_Click(object sender, EventArgs e)
        {
            FormFactory.HideFormAll();
            cmbUserList.Focus();
            DisableButton();
            leftBorderBtn.Visible = false;
            iconCurrentChildForm.IconChar = IconChar.Home;
            iconCurrentChildForm.IconColor = Color.MediumPurple;
            lblTitleChildForm.Text = "首    页";

            LoginUserShowButton(GlobalVar.temLevel);
        }
        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void btnMinmize_Click(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }

        private void iBtnHomePage_Click(object sender, EventArgs e)
        {
            ActicateButton(sender, RGBColors.color1);
            FormDisplay(((IconButton)sender).Tag.ToString());
        }

        private void iBtnVisionSetting_Click(object sender, EventArgs e)
        {
            ActicateButton(sender, RGBColors.color2);
            FormDisplay(((IconButton)sender).Tag.ToString());
        }

        private void iBtnMotionSetting_Click(object sender, EventArgs e)
        {
            ActicateButton(sender, RGBColors.color3);
            FormDisplay(((IconButton)sender).Tag.ToString());
        }

        private void iBtnSportSetting_Click(object sender, EventArgs e)
        {
            ActicateButton(sender, RGBColors.color8);
            FormDisplay(((IconButton)sender).Tag.ToString());
        }

        private void iBtnIOMonitor_Click(object sender, EventArgs e)
        {
            ActicateButton(sender, RGBColors.color4);
            FormDisplay(((IconButton)sender).Tag.ToString());
        }

        private void iBtnUserManage_Click(object sender, EventArgs e)
        {
            ActicateButton(sender, RGBColors.color5);
            FormDisplay(((IconButton)sender).Tag.ToString());
        }

        private void iBtnStaTest_Click(object sender, EventArgs e)
        {
            ActicateButton(sender, RGBColors.color6);
            FormDisplay(((IconButton)sender).Tag.ToString());
        }

        private void iBtnSysSetting_Click(object sender, EventArgs e)
        {
            ActicateButton(sender, RGBColors.color7);
            FormDisplay(((IconButton)sender).Tag.ToString());
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void MainFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            mainStart = false;
            GlobalVar.totalRunFlagVision = false;
            if (GlobalVar.tcpServerEngine!=null)
            {
                GlobalVar.tcpServerEngine.Stop();
            }
            
            IOC0640.ioc_board_close();
            if (InspectionClass.camIsFind)
            {
                CogFrameGrabbers frameGrabbers = new CogFrameGrabbers();
                foreach (ICogFrameGrabber fg in frameGrabbers)
                    fg.Disconnect(false);
            }
            if (GlobalVar.mainThread!=null)
            {
                GlobalVar.mainThread.Abort();
                GlobalVar.mainThread = null;
            }

            foreach (Task task in GlobalVar.taskList)
            {
                task.Wait(500);
                task.Dispose();
            }
            if (FormFactory.forms.Count>0)
            {
                foreach (Form frm in FormFactory.forms)
                {
                    if (!frm.IsDisposed)
                    {
                        frm.Close();
                    }
                }
            }
            if (MyIoListenThread!=null)
            {
                MyIoListenThread.Abort();
                MyIoListenThread = null;
            }
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cmbProList.Text))
                {
                    cmbUserList.Focus();
                    MessageBox.Show("产品名称为空，请输入或选择");
                    return;
                }
                if (string.IsNullOrWhiteSpace(cmbUserList.Text))
                {
                    cmbUserList.Focus();
                    MessageBox.Show("用户名输入为空，请输入或选择");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtPassWord.Text))
                {
                    txtPassWord.Focus();
                    MessageBox.Show("密码输入为空，请输入或选择");
                    return;
                }
                //验证用户名是否存在
                if (!listUsers.Contains(cmbUserList.Text.Trim()) && GlobalVar.temLevel> GlobalVar.Level.操作员)
                {
                    if (MessageBox.Show("该用户名【" + cmbUserList.Text.Trim() + "】不存在，是否添加此用户", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {//弹出超级管理员登录密码
                        //FormFactory.HideFormAll();
                        iBtnUserManage_Click(iBtnUserManage, null);
                    }
                    return;
                }
                //验证密码是否正确
                string pwd_input = txtPassWord.Text;
                GlobalVar.userName = cmbUserList.SelectedItem?.ToString();
                string sql = "select top 1 UserPwd from dbo.UserData where UserName='" + GlobalVar.userName + "'";
                string pwd_db = GlobalVar.mySqlDb.ExecuteScalar(sql, null).ToString();
                if (pwd_input != pwd_db)
                {
                    txtPassWord.Text = "";
                    MessageBox.Show("输入密码不正确，请重新输入");
                    return;
                }
                string sql1 = "select top 1 UserPower from dbo.UserData where UserName='" + GlobalVar.userName + "'";
                GlobalVar.temLevel = (GlobalVar.Level)Convert.ToInt32(GlobalVar.mySqlDb.ExecuteScalar(sql1, null));
                LoginUserShowButton(GlobalVar.temLevel); 
                GlobalVar.curProName = cmbProList.SelectedItem.ToString();
                ClassINI.INI.INIWriteValue(GlobalVar.bConfigFilePath, "System", "产品选择", GlobalVar.curProName);
                FormFactory.HideFormAll();
                txtPassWord.Clear();
                ActicateButton(iBtnHomePage, RGBColors.color1);
                FormDisplay("FrmHomePage");
            }
            catch (Exception ex)
            {
                GlobalVar.myLog.Warn("登录主界面异常 : " + ex.Message);
            }
        }

        private void cmbUserList_Enter(object sender, EventArgs e)
        {
            LoadUser();
        }

        private void cmbUserList_TextUpdate(object sender, EventArgs e)
        {
            // 输入key之后，返回的关键词
            List<string> listNew = new List<string>();

            //清空combobox
            this.cmbUserList.Items.Clear();
            //清空listNew
            listNew.Clear();
            //遍历全部备查数据
            foreach (var item in listUsers)
            {
                if (item.Contains(this.cmbUserList.Text))
                {
                    //符合，插入ListNew
                    listNew.Add(item);
                }
            }
            //combobox添加已经查到的关键词
            this.cmbUserList.Items.AddRange(listNew.ToArray());
            //设置光标位置，否则光标位置始终保持在第一列，造成输入关键词的倒序排列
            this.cmbUserList.SelectionStart = this.cmbUserList.Text.Length;
            //保持鼠标指针原来状态，有时候鼠标指针会被下拉框覆盖，所以要进行一次设置。
            Cursor = Cursors.Default;
            cmbUserList.DroppedDown = true;
        }

        private void cmbUserList_MouseDown(object sender, MouseEventArgs e)
        {
            cmbUserList.DroppedDown = true;
        }
        #endregion

        #region 鼠标拖动Panel方法

        //Drag Form  使鼠标可以拖动相应panel
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        #endregion

        #region 初始化相机  
        private void InitializeAcqusition()
        {
            inspection = new InspectionClass();
        }
        #endregion 

        #region 视觉部分

        #region  声明变量
        InspectionClass inspection = null;
        #endregion

        private static void DisposeAllCam()
        {
            CogFrameGrabbers frameGrabbers = new CogFrameGrabbers();
            foreach (ICogFrameGrabber fg in frameGrabbers)
                fg.Disconnect(false);
        }
        #endregion

        #region IO卡
        private string InitIO640()
        {
            if (IOC0640.ioc_board_init() <= 0)
            {
                GlobalVar.isLoadAllParam = false;
                return "没有找到IO640卡";
            }
            else
            {
                GlobalVar.isLoadAllParam = true;
                return "IO640初始化完成";
            }
           
        }

        private void ListenIoStatus()
        {
            if (GlobalVar.lsAxiasDIs[0].IoPinStatus) { }
            if (GlobalVar.lsAxiasDIs[1].IoPinStatus) { }
            if (GlobalVar.lsAxiasDIs[2].IoPinStatus) { }
            if (GlobalVar.lsAxiasDIs[3].IoPinStatus) { }
            if (GlobalVar.lsAxiasDIs[4].IoPinStatus) { }
            if (GlobalVar.lsAxiasDIs[5].IoPinStatus) { }
            if (GlobalVar.lsAxiasDIs[6].IoPinStatus) { }
            if (GlobalVar.lsAxiasDIs[7].IoPinStatus) { }
            if (GlobalVar.lsAxiasDIs[8].IoPinStatus) { }
            if (GlobalVar.lsAxiasDIs[9].IoPinStatus) { }
            if (GlobalVar.lsAxiasDIs[10].IoPinStatus) { }
            if (GlobalVar.lsAxiasDIs[11].IoPinStatus) { }
            if (GlobalVar.lsAxiasDIs[12].IoPinStatus) { }
            if (GlobalVar.lsAxiasDIs[13].IoPinStatus) { }
            if (GlobalVar.lsAxiasDIs[14].IoPinStatus) { }
            if (GlobalVar.lsAxiasDIs[15].IoPinStatus) { }
            if (GlobalVar.lsAxiasDIs[16].IoPinStatus) { }
            if (GlobalVar.lsAxiasDIs[17].IoPinStatus) { }
            if (GlobalVar.lsAxiasDIs[18].IoPinStatus) { }
            if (GlobalVar.lsAxiasDIs[19].IoPinStatus) { }
            if (GlobalVar.lsAxiasDIs[20].IoPinStatus) { }
            if (GlobalVar.lsAxiasDIs[21].IoPinStatus) { }
            if (GlobalVar.lsAxiasDIs[22].IoPinStatus) { }
            if (GlobalVar.lsAxiasDIs[23].IoPinStatus) { }
            if (GlobalVar.lsAxiasDIs[24].IoPinStatus) { }
            if (GlobalVar.lsAxiasDIs[25].IoPinStatus) { }
            if (GlobalVar.lsAxiasDIs[26].IoPinStatus) { }
            if (GlobalVar.lsAxiasDIs[27].IoPinStatus) { }
            if (GlobalVar.lsAxiasDIs[28].IoPinStatus) { }
            if (GlobalVar.lsAxiasDIs[29].IoPinStatus) { }
            if (GlobalVar.lsAxiasDIs[30].IoPinStatus) { }
            if (GlobalVar.lsAxiasDIs[31].IoPinStatus) { }
            if (GlobalVar.lsAxiasDIs[32].IoPinStatus) { }
            if (GlobalVar.lsAxiasDIs[33].IoPinStatus) { }
            if (GlobalVar.lsAxiasDIs[34].IoPinStatus) { }
            if (GlobalVar.lsAxiasDIs[35].IoPinStatus) { }
            if (GlobalVar.lsAxiasDIs[36].IoPinStatus) { }
            if (GlobalVar.lsAxiasDIs[37].IoPinStatus) { }
            if (GlobalVar.lsAxiasDIs[38].IoPinStatus) { }
            if (GlobalVar.lsAxiasDIs[39].IoPinStatus) { }
            if (GlobalVar.lsAxiasDIs[40].IoPinStatus) { }
            if (GlobalVar.lsAxiasDIs[41].IoPinStatus) { }
            if (GlobalVar.lsAxiasDIs[42].IoPinStatus) { }
            if (GlobalVar.lsAxiasDIs[43].IoPinStatus) { }
            if (GlobalVar.lsAxiasDIs[44].IoPinStatus) { }
            if (GlobalVar.lsAxiasDIs[45].IoPinStatus) { }
            if (GlobalVar.lsAxiasDOs[0].IoPinStatus) { }
            if (GlobalVar.lsAxiasDOs[1].IoPinStatus) { }
            if (GlobalVar.lsAxiasDOs[2].IoPinStatus) { }
            if (GlobalVar.lsAxiasDOs[3].IoPinStatus) { }
            if (GlobalVar.lsAxiasDOs[4].IoPinStatus) { }
            if (GlobalVar.lsAxiasDOs[5].IoPinStatus) { }
            if (GlobalVar.lsAxiasDOs[6].IoPinStatus) { }
            if (GlobalVar.lsAxiasDOs[7].IoPinStatus) { }
            if (GlobalVar.lsAxiasDOs[8].IoPinStatus) { }
            if (GlobalVar.lsAxiasDOs[9].IoPinStatus) { }
            if (GlobalVar.lsAxiasDOs[10].IoPinStatus) { }
            if (GlobalVar.lsAxiasDOs[11].IoPinStatus) { }
            if (GlobalVar.lsAxiasDOs[12].IoPinStatus) { }
            if (GlobalVar.lsAxiasDOs[13].IoPinStatus) { }
            if (GlobalVar.lsAxiasDOs[14].IoPinStatus) { }
            if (GlobalVar.lsAxiasDOs[15].IoPinStatus) { }
            if (GlobalVar.lsAxiasDOs[16].IoPinStatus) { }
            if (GlobalVar.lsAxiasDOs[17].IoPinStatus) { }
            if (GlobalVar.lsAxiasDOs[18].IoPinStatus) { }
            if (GlobalVar.lsAxiasDOs[19].IoPinStatus) { }
            if (GlobalVar.lsAxiasDOs[20].IoPinStatus) { }
            if (GlobalVar.lsAxiasDOs[21].IoPinStatus) { }
            if (GlobalVar.lsAxiasDOs[22].IoPinStatus) { }
            if (GlobalVar.lsAxiasDOs[23].IoPinStatus) { }
            if (GlobalVar.lsAxiasDOs[24].IoPinStatus) { }
            if (GlobalVar.lsAxiasDOs[25].IoPinStatus) { }
            if (GlobalVar.lsAxiasDOs[26].IoPinStatus) { }
            if (GlobalVar.lsAxiasDOs[27].IoPinStatus) { }
            if (GlobalVar.lsAxiasDOs[28].IoPinStatus) { }
            if (GlobalVar.lsAxiasDOs[29].IoPinStatus) { }
            if (GlobalVar.lsAxiasDOs[30].IoPinStatus) { }
            if (GlobalVar.lsAxiasDOs[31].IoPinStatus) { }
            if (GlobalVar.lsAxiasDOs[32].IoPinStatus) { }
        }

        #endregion

        #region 轴卡初始化
        private string InitLSAxiasCard()
        {
            short num = LTDMC.dmc_board_init();
            if (num <= 0 || num > 8)
            {
                GlobalVar.myLog.Warn("初始卡失败!");
                GlobalVar.isLoadAllParam = false;
                return "没有找到轴卡";
            }
            else
            {
                ushort _num = 0;
                ushort[] cardids = new ushort[8];
                uint[] cardtypes = new uint[8];
                short res = LTDMC.dmc_get_CardInfList(ref _num, cardtypes, cardids);
                if (res != 0)
                {
                    GlobalVar.myLog.Warn("获取卡信息失败!");
                    GlobalVar.isLoadAllParam = false;
                    return "获取卡信息失败";
                }
                else
                {
                    GlobalVar.CardId = cardids[0];
                    GlobalVar.isLoadAllParam = true;
                    return "轴卡初始化成功";
                }
            }
           
            
        }

        private void LoadLsAllAxiasParams()
        {
            if (GlobalVar.isLoadAllParam)
            {
                {
                    ushort enabled = 0;
                    ushort alm_logic = 0;
                    ushort alm_action = 0;
                    LTDMC.dmc_get_alm_mode(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, ref enabled, ref alm_logic, ref alm_action);
                    if (alm_logic == 0)
                    {
                        LTDMC.dmc_set_alm_mode(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, enabled, 1, alm_action);
                    }
                }
                {
                    ushort enabled = 0;
                    ushort alm_logic = 0;
                    ushort alm_action = 0;
                    LTDMC.dmc_get_alm_mode(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, ref enabled, ref alm_logic, ref alm_action);
                    if (alm_logic == 0)
                    {
                        LTDMC.dmc_set_alm_mode(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, enabled, 1, alm_action);
                    }
                }
                {
                    ushort enabled = 0;
                    ushort alm_logic = 0;
                    ushort alm_action = 0;
                    LTDMC.dmc_get_alm_mode(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, ref enabled, ref alm_logic, ref alm_action);
                    if (alm_logic == 0)
                    {
                        LTDMC.dmc_set_alm_mode(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, enabled, 1, alm_action);
                    }
                }
                {
                    ushort enabled = 0;
                    ushort alm_logic = 0;
                    ushort alm_action = 0;
                    LTDMC.dmc_get_alm_mode(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, ref enabled, ref alm_logic, ref alm_action);
                    if (alm_logic == 0)
                    {
                        LTDMC.dmc_set_alm_mode(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, enabled, 1, alm_action);
                    }
                }
                if (LTDMC.dmc_read_sevon_pin(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber) == 1)
                {
                    LTDMC.dmc_write_sevon_pin(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, 0);
                }
                if (LTDMC.dmc_read_sevon_pin(GlobalVar.CardId, GlobalVar.FeedAxiasNumber)==1)
                {
                    LTDMC.dmc_write_sevon_pin(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, 0);
                }
                if (LTDMC.dmc_read_sevon_pin(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber) == 1)
                {
                    LTDMC.dmc_write_sevon_pin(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, 0);
                }
                if (LTDMC.dmc_read_sevon_pin(GlobalVar.CardId, GlobalVar.TakeAxiasNumber) == 1)
                {
                    LTDMC.dmc_write_sevon_pin(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, 0);
                }
                if (LTDMC.dmc_read_sevon_pin(GlobalVar.CardId, GlobalVar.LabelingAxiasNumber) == 1)
                {
                    LTDMC.dmc_write_sevon_pin(GlobalVar.CardId, GlobalVar.LabelingAxiasNumber, 0);
                }
                //出标轴
                LTDMC.dmc_set_alm_mode(GlobalVar.CardId, GlobalVar.LabelingAxiasNumber, 1, 0, 0);
            }
        }

        private void InitAxiasHomeParamsAndGoHome()
        {
            LTDMC.dmc_set_homemode(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, 1, (double)GlobalVar.injectFeedHomeSpeedModel, (ushort)GlobalVar.injectFeedHomeModel, 0);
            LTDMC.dmc_set_home_position(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, 2, 0);
            LTDMC.dmc_set_homemode(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, 1, (double)GlobalVar.feedHomeSpeedModel, (ushort)GlobalVar.feedHomeModel, 0);
            LTDMC.dmc_set_home_position(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, 2, 0);
            LTDMC.dmc_set_homemode(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, 1, (double)GlobalVar.feedRHomeSpeedModel, (ushort)GlobalVar.feedRHomeModel, 0);
            LTDMC.dmc_set_home_position(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, 2, 0);
            LTDMC.dmc_set_homemode(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, 0, (double)GlobalVar.takeHomeSpeedModel, (ushort)GlobalVar.takeHomeModel, 0);
            LTDMC.dmc_set_home_position(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, 2, 0);

            LTDMC.dmc_home_move(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber);
            LTDMC.dmc_home_move(GlobalVar.CardId, GlobalVar.FeedAxiasNumber);
            LTDMC.dmc_home_move(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber);
            LTDMC.dmc_home_move(GlobalVar.CardId, GlobalVar.TakeAxiasNumber);
            while (LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.TakeAxiasNumber) == 0 || LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.FeedAxiasNumber) == 0 || LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber) == 0 || LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber) == 0)
            { Application.DoEvents(); }
        }

        private void InitThreeAxiasSpeed()
        {
            LTDMC.dmc_set_profile(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, GlobalVar.injectFeedStartSpeed, GlobalVar.injectFeedMotionSpeed, GlobalVar.injectFeedAccTime, GlobalVar.injectFeedDccTime, GlobalVar.injectFeedStopSpeed);//设置速度参数
            LTDMC.dmc_set_s_profile(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, 0, GlobalVar.injectFeedSTime);//设置S段速度参数
            LTDMC.dmc_set_profile(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, GlobalVar.feedStartSpeed, GlobalVar.feedMotionSpeed, GlobalVar.feedAccTime, GlobalVar.feedDccTime, GlobalVar.feedStopSpeed);//设置速度参数
            LTDMC.dmc_set_s_profile(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, 0, GlobalVar.feedSTime);//设置S段速度参数
            LTDMC.dmc_set_profile(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, GlobalVar.feedRStartSpeed, GlobalVar.feedRMotionSpeed, GlobalVar.feedRAccTime, GlobalVar.feedRDccTime, GlobalVar.feedRStopSpeed);//设置速度参数
            LTDMC.dmc_set_s_profile(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, 0, GlobalVar.feedRSTime);//设置S段速度参数
            LTDMC.dmc_set_profile(GlobalVar.CardId,   GlobalVar.TakeAxiasNumber,    GlobalVar.takeStartSpeed, GlobalVar.takeMotionSpeed, GlobalVar.takeAccTime, GlobalVar.takeDccTime, GlobalVar.takeStopSpeed);//设置速度参数
            LTDMC.dmc_set_s_profile(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, 0, GlobalVar.takeSTime);//设置S段速度参数
            LTDMC.dmc_set_profile(GlobalVar.CardId, GlobalVar.LabelingAxiasNumber, GlobalVar.labelStartSpeed, GlobalVar.labelMotionSpeed, GlobalVar.labelAccTime, GlobalVar.labelDccTime, GlobalVar.labelStopSpeed);//设置速度参数
            LTDMC.dmc_set_s_profile(GlobalVar.CardId, GlobalVar.LabelingAxiasNumber, 0, GlobalVar.labelSTime);//设置S段速度参数
        }
        #endregion

        #region 解析轴卡感应器信号
        private void AnalysisDMCSensorLimData()
        {
            GlobalVar.lsFirAxiasSensorDIs = Convert.ToString(LTDMC.dmc_read_inport(GlobalVar.CardId, 0), 2).PadLeft(32, '0').Reverse().ToList();
            GlobalVar.lsSecAxiasSensorDIs = Convert.ToString(LTDMC.dmc_read_inport(GlobalVar.CardId, 1), 2).PadLeft(32, '0').Reverse().ToList();
        }
        #endregion

        #region epson机器人Socket通讯以及相关事件
        private void InitEpsonSocket()
        {
            try
            {
                //方法一、采用当前应用程序中的【App.config】文件。
                //var bootstrap = BootstrapFactory.CreateBootstrap();

                //=>方法二、采用自定义独立【SuperSocket.config】配置文件
                var bootstrap = BootstrapFactory.CreateBootstrapFromConfigFile("SuperSocket.config");
                if (!bootstrap.Initialize())
                {
                    GlobalVar.myLog.Error("Failed to initialize!");
                    GlobalVar.isLoadAllParam = false;
                    return;
                }
                StartResult startResult = bootstrap.Start();
                if (startResult == StartResult.Success)
                {
                    GlobalVar.myLog.Info("服务启动成功！");
                    GlobalVar.tcpServerEngine = bootstrap.AppServers.Cast<MyAppServer>().FirstOrDefault();
                    GlobalVar.isLoadAllParam = true;
                }
                else
                {
                    GlobalVar.isLoadAllParam = false;
                    GlobalVar.myLog.Error("服务启动失败！");
                }
            }
            catch (Exception ex)
            {
                GlobalVar.isLoadAllParam = false;
                GlobalVar.myLog.Error("服务器启动出现位置错误 :" + ex.Message);
            }
        }

        #endregion

        #region 不同权限控制的不同按钮
        public void LoginUserShowButton(GlobalVar.Level userLevel)
        {
            switch (userLevel)
            {
                case GlobalVar.Level.工程师:
                    {
                        iBtnVisionSetting.Enabled = true;
                        iBtnMotionSetting.Enabled = true;
                        iBtnSportSetting.Enabled = true;
                        iBtnIOMonitor.Enabled = true;
                        iBtnUserManage.Enabled = true;
                        iBtnStaTest.Enabled = true;
                        iBtnSysSetting.Enabled = true;
                    }
                    break;
                case GlobalVar.Level.品质:
                    {
                        iBtnVisionSetting.Enabled = false;
                        iBtnMotionSetting.Enabled = false;
                        iBtnSportSetting.Enabled = false;
                        iBtnIOMonitor.Enabled = true;
                        iBtnUserManage.Enabled = true;
                        iBtnStaTest.Enabled = true;
                        iBtnSysSetting.Enabled = false;
                    }
                    break;
                case GlobalVar.Level.生技:
                    {
                        iBtnVisionSetting.Enabled = false;
                        iBtnMotionSetting.Enabled = false;
                        iBtnSportSetting.Enabled = true;
                        iBtnIOMonitor.Enabled = true;
                        iBtnUserManage.Enabled = true;
                        iBtnStaTest.Enabled = false;
                        iBtnSysSetting.Enabled = false;
                    }
                    break;
                case GlobalVar.Level.操作员:
                    {
                        iBtnVisionSetting.Enabled = false;
                        iBtnMotionSetting.Enabled = false;
                        iBtnSportSetting.Enabled = false;
                        iBtnIOMonitor.Enabled = true;
                        iBtnUserManage.Enabled = false;
                        iBtnStaTest.Enabled = false;
                        iBtnSysSetting.Enabled = false;
                    }
                    break;
                default:
                    {
                        iBtnVisionSetting.Enabled = false;
                        iBtnMotionSetting.Enabled = false;
                        iBtnSportSetting.Enabled = false;
                        iBtnIOMonitor.Enabled = true;
                        iBtnUserManage.Enabled = false;
                        iBtnStaTest.Enabled = false;
                        iBtnSysSetting.Enabled = false;
                    }
                    break;
            }
        }
        #endregion

        #region 登录方法
        /// <summary>
        /// 用户列表载入
        /// </summary>
        public void LoadUser()
        {
            DataTable dt1 = GlobalVar.mySqlDb.ExecuteDataTable("select distinct(UserName) from UserData", null);
            listUsers.Clear();
            cmbUserList.Items.Clear();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                cmbUserList.Items.Add(dt1.Rows[i][0].ToString());
                listUsers.Add(dt1.Rows[i][0].ToString());
            }
            cmbUserList.Focus();
        }

        #endregion

        #region IO扫描

        private void ListenIOStatus()
        {
            while (mainStart)
            {
                try
                {
                    if (GlobalVar.lsAxiasDIs[9].CurIOStatus)
                    {
                        this.Invoke(new Action(() => {
                            ActicateButton(iBtnHomePage, RGBColors.color1);
                            FormDisplay("FrmHomePage");
                        }));
                    }
                    if (!GlobalVar.lsAxiasDIs[44].CurIOStatus)//急停
                    {
                        if (WarnFormShow.ctrlMsg == null)
                        {
                            this.Enabled = false;
                            WarnFormShow.ShowPromptMessage(this, "设备急停打开中");
                        }
                    }
                    else
                    {
                        if (WarnFormShow.ctrlMsg != null)
                        {
                            WarnFormShow.ctrlMsg.Close();
                            WarnFormShow.ctrlMsg = null;
                            this.Enabled = true;
                        }
                    }
                    ListenIoStatus();
                    AnalysisDMCSensorLimData();
                }
                catch (Exception ex)
                {
                    GlobalVar.myLog.Error($"主线程IO扫描出现异常错误：{ex.Message}");
                }
                finally
                {
                    Application.DoEvents();
                    Thread.Sleep(50);
                }
            }
        }
        #endregion

    }
}
