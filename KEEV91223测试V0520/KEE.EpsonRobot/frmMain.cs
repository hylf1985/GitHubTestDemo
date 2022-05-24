using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Linq;
using System.IO;
using System.Collections.Concurrent;
using SuperSocket.SocketBase;
using SuperSocket.SocketEngine;
using SuperSocket.SocketBase.Config;
using KEE.EpsonRobot.Utility;
using KEE.EpsonRobot.CamOperator;
using System.Threading;
using Cognex.VisionPro;
using Cognex.VisionPro.Exceptions;
using Cognex.VisionPro.ToolBlock;
using System.Runtime.Serialization.Formatters.Binary;

namespace KEE.EpsonRobot
{
    public partial class frmMain : Form
    {
        InspectionClass inspection = null;
        public delegate void CbDelegate();
        public delegate void CbDelegate<T1>(T1 obj1);
        public delegate void CbDelegate<T1, T2>(T1 obj1, T2 obj2);
        private MyAppServer tcpServerEngine;
        //bool isCalOrTest = false;
        bool isTeach = false;
        /// <summary>
        /// 第一工位偏移量X
        /// </summary>
        double firstStaOffsetX = 0;
        /// <summary>
        /// 第一工位偏移量Y
        /// </summary>
        double firstStaOffsetY = 0;
        /// <summary>
        /// 第一工位偏移量Deg
        /// </summary>
        double firstStaOffsetDeg = 0;
        /// <summary>
        /// 第三工位偏移量X
        /// </summary>
        double thirdStaOffsetX = 0;
        /// <summary>
        /// 第三工位偏移量Y
        /// </summary>
        double thirdStaOffsetY = 0;
        /// <summary>
        /// 第三工位偏移量Deg
        /// </summary>
        double thirdStaOffsetDeg = 0;
        public static ConcurrentDictionary<string, MyAppSession> mOnLineConnections = new ConcurrentDictionary<string, MyAppSession>();
        public frmMain()
        {
            SplashScreen.Show(typeof(SplashForm)); // 实例化SplashForm窗体，并展示运行
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            #region 初始化一系列参数
            #region  相机加载流程
            SplashScreen.ChangeTitle("-->开始加载相机");
            //相机加载方法
            GlobalVar.Initpara();
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
            if (InspectionClass.isLoadAllParam)  //如果isLoadAllParam为True
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
            Thread.Sleep(300);   //加载成功关闭窗体
            #endregion
            #region  VisionPro加载流程
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
            SplashScreen.ChangeTitle("-->开始加载 UI数据");
            LoadUiData();
            Thread.Sleep(300);
            if (GlobalVar.isLoadAllParam)
            {
                SplashScreen.ChangeTitle("-->加载 UI数据完成...");
            }
            else
            {
                SplashScreen.ChangeTitle("-->加载 UI数据,错误信息");
                DisposeAllCam();
                Thread.Sleep(3000);
                SplashScreen.Close();   //通讯失败关闭窗体
                System.Environment.Exit(System.Environment.ExitCode);   //
            }
            SplashScreen.Close();
            #endregion
            #endregion
        }

        private void button_StartListen_Click(object sender, EventArgs e)
        {
            try
            {
                //方法一、采用当前应用程序中的【App.config】文件。
                //var bootstrap = BootstrapFactory.CreateBootstrap();

                //=>方法二、采用自定义独立【SuperSocket.config】配置文件
                var bootstrap = BootstrapFactory.CreateBootstrapFromConfigFile("SuperSocket.config");
                if (!bootstrap.Initialize())
                {
                    ShowMessage("Failed to initialize!");
                    return;
                }
                StartResult startResult = bootstrap.Start();
                if (startResult == StartResult.Success)
                {
                    this.ShowMessage("服务启动成功！");
                    tcpServerEngine = bootstrap.AppServers.Cast<MyAppServer>().FirstOrDefault();
                    if (tcpServerEngine != null)
                    {
                        tcpServerEngine.NewSessionConnected += tcpServerEngine_NewSessionConnected;
                        tcpServerEngine.NewRequestReceived += tcpServerEngine_NewRequestReceived;
                        tcpServerEngine.SessionClosed += tcpServerEngine_SessionClosed;
                        this.ShowListenStatus();
                    }
                    else
                        this.ShowMessage("请检查配置文件中是否有可用的服务信息！");
                }
                else
                    this.ShowMessage("服务启动失败！");
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (InspectionClass.tb1 != null && cogToolBlockEditV21.Subject != null && radioButton1.Checked)
            {
                InspectionClass.tb1 = cogToolBlockEditV21.Subject;
                CogSerializer.SaveObjectToFile(InspectionClass.tb1, GlobalVar.bFileNoLabelLogoToolPath, typeof(BinaryFormatter), CogSerializationOptionsConstants.Minimum);
                System.Threading.Thread.Sleep(300);
            }
            if (InspectionClass.tb2 != null && cogToolBlockEditV22.Subject != null && radioButton2.Checked)
            {
                InspectionClass.tb2 = cogToolBlockEditV22.Subject;
                CogSerializer.SaveObjectToFile(InspectionClass.tb2, GlobalVar.bFileNoGumToolPath, typeof(BinaryFormatter), CogSerializationOptionsConstants.Minimum);
                System.Threading.Thread.Sleep(300);
            }
            if (InspectionClass.tb3 != null && cogToolBlockEditV23.Subject != null && radioButton3.Checked)
            {
                InspectionClass.tb3 = cogToolBlockEditV23.Subject;
                CogSerializer.SaveObjectToFile(InspectionClass.tb3, GlobalVar.bFileNoGumToolPath, typeof(BinaryFormatter), CogSerializationOptionsConstants.Minimum);
                System.Threading.Thread.Sleep(300);
            }
            if (InspectionClass.tb4 != null && cogToolBlockEditV24.Subject != null && radioButton4.Checked)
            {
                InspectionClass.tb4 = cogToolBlockEditV24.Subject;
                CogSerializer.SaveObjectToFile(InspectionClass.tb4, GlobalVar.bFileNoGumToolPath, typeof(BinaryFormatter), CogSerializationOptionsConstants.Minimum);
                System.Threading.Thread.Sleep(300);
            }
        }

        private void button_Close_Click(object sender, EventArgs e)
        {
            if (tcpServerEngine == null) return;
            this.tcpServerEngine.Stop();
            this.ShowListenStatus();
            this.textBox_port.ReadOnly = false;
            this.textBox_port.SelectAll();
            this.textBox_port.Focus();
            this.tcpServerEngine.Dispose();
            this.tcpServerEngine = null;
            this.button_StartListen.Enabled = true;
        }

        void tcpServerEngine_SessionClosed(MyAppSession session, global::SuperSocket.SocketBase.CloseReason value)
        {
            this.ShowMessage(session.RemoteEndPoint, "下线");
            MyAppSession outAppSession;
            mOnLineConnections.TryRemove(session.SessionID, out outAppSession);
            this.ShowConnectionCount(mOnLineConnections.Count);
            //this.ShowConnectionCount(this.tcpServerEngine.SessionCount);
        }

        void tcpServerEngine_NewRequestReceived(MyAppSession session, global::SuperSocket.SocketBase.Protocol.StringRequestInfo requestInfo)
        {
            switch (requestInfo.Key)
            {
                case "echo":
                    string[] datas = requestInfo.Body.Split(',');
                    if (datas.Length == 4)
                    {
                        switch (datas[0]) //根据Body的长度来定义是正常测试还是标定1,107.853,-247.316
                        {
                            case "1":  //表示取料位Logo下相机拍照
                                GlobalVar.noLabelLogoRobotX = Convert.ToDouble(datas[1]);
                                GlobalVar.noLabelLogoRobotY = Convert.ToDouble(datas[2]);
                                if (GlobalVar.isCam1CalOrTest)
                                {
                                    RunTBTools(true, 0);
                                }
                                else
                                {
                                    RunTBTools(false, 0);
                                }
                                break;
                            case "2"://出标位置上相机拍照
                                GlobalVar.noGumRobotX = Convert.ToDouble(datas[1]);
                                GlobalVar.noGumRobotY = Convert.ToDouble(datas[2]);
                                GlobalVar.noGumRobotU = Convert.ToDouble(datas[3]);
                                if (GlobalVar.isCam2CalOrTest)
                                {
                                    RunTBTools(true, 1);
                                }
                                else
                                {
                                    RunTBTools(false, 1);
                                }
                                break;
                            case "3"://贴完背胶后Logo下相机拍照
                                GlobalVar.labeledLogoRobotX = Convert.ToDouble(datas[1]);
                                GlobalVar.labeledLogoRobotY = Convert.ToDouble(datas[2]);
                                if (GlobalVar.isCam3CalOrTest)
                                {
                                    RunTBTools(true, 2);
                                }
                                else
                                {
                                    RunTBTools(false, 2);
                                }
                                break;
                            case "4"://上相机塑胶件拍照
                                GlobalVar.gumedRobotX = Convert.ToDouble(datas[1]);
                                GlobalVar.gumedRobotY = Convert.ToDouble(datas[2]);
                                GlobalVar.gumedRobotU = Convert.ToDouble(datas[3]);
                                if (GlobalVar.isCam4CalOrTest)
                                {
                                    RunTBTools(true, 3);
                                }
                                else
                                {
                                    RunTBTools(false, 3);
                                }
                                break;
                        }
                    }
                    this.ShowMessage(session.RemoteEndPoint, requestInfo.Body);
                    break;
                case "heartbeat":
                    this.ShowMessage(session.RemoteEndPoint, requestInfo.Body);
                    string msg = string.Format("push {0}", requestInfo.Body + Environment.NewLine);//一定要加上分隔符
                    byte[] bMsg = System.Text.Encoding.UTF8.GetBytes(msg);//消息使用UTF-8编码
                    session.Send(new ArraySegment<byte>(bMsg, 0, bMsg.Length));
                    break;
                default:
                    this.ShowMessage(session.RemoteEndPoint, "未知的指令（error unknow command）");
                    break;
            }
        }

        void tcpServerEngine_NewSessionConnected(MyAppSession session)
        {
            this.ShowMessage(session.RemoteEndPoint, "上线");
            mOnLineConnections.TryAdd(session.SessionID, session);
            this.ShowConnectionCount(mOnLineConnections.Count);
            //this.ShowConnectionCount(this.tcpServerEngine.SessionCount);
        }

        private void ShowListenStatus()
        {
            this.button_StartListen.Enabled = !(this.tcpServerEngine.State == ServerState.Running);
            this.button_Close.Enabled = (this.tcpServerEngine.State == ServerState.Running);
            this.textBox_port.ReadOnly = !(this.tcpServerEngine.State == ServerState.Running);
            this.button_Send.Enabled = (this.tcpServerEngine.State == ServerState.Running);
        }

        private void button_Send_Click(object sender, EventArgs e)
        {
            try
            {
                MyAppSession appSession = (MyAppSession)this.cboClients.SelectedItem;
                if (appSession == null)
                {
                    ShowMessage("没有选中任何在线客户端！");
                    return;
                }
                if (!appSession.Connected)
                {
                    ShowMessage("目标客户端不在线！");
                    return;
                }
                string msg = string.Format("{0}", this.textBox_msg.Text + Environment.NewLine);//一定要加上分隔符
                byte[] bMsg = System.Text.Encoding.UTF8.GetBytes(msg);//消息使用UTF-8编码
                //this.tcpServerEngine.GetSessionByID(appSession.SessionID).Send(new ArraySegment<byte>(bMsg, 0, bMsg.Length));
                appSession.Send(new ArraySegment<byte>(bMsg, 0, bMsg.Length));
            }
            catch (Exception ee)
            {
                ShowMessage(ee.Message);
            }
        }

        private void cboClients_DropDown(object sender, EventArgs e)
        {
            if (tcpServerEngine == null)
            {
                this.cboClients.DataSource = null;
                return;
            }
            //IList<MyAppSession> list = this.tcpServerEngine.GetAllSessions().ToList();
            IList<MyAppSession> list = this.tcpServerEngine.GetSessions(s => s.Connected == true).ToList();
            this.cboClients.DisplayMember = "RemoteEndPoint";
            this.cboClients.ValueMember = "SessionID";
            this.cboClients.DataSource = list;
        }

        private void ShowMessage(string msg)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new CbDelegate<string>(this.ShowMessage), msg);
            }
            else
            {
                ListViewItem item = new ListViewItem(new string[] { DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff"), msg });
                this.lsvRevicedMsg.Items.Insert(0, item);
            }
        }

        private void ShowMessage(IPEndPoint client, string msg)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new CbDelegate<IPEndPoint, string>(this.ShowMessage), client, msg);
            }
            else
            {
                ListViewItem item = new ListViewItem(new string[] { DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff"), client.ToString(), msg });
                this.lsvRevicedMsg.Items.Insert(0, item);
            }
        }

        private void ShowConnectionCount(int clientCount)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new CbDelegate<int>(this.ShowConnectionCount), clientCount);
            }
            else
            {
                this.toolStripLabel_clientCount.Text = "在线数量： " + clientCount.ToString();
            }
        }

        #region 初始化相机  
        private void InitializeAcqusition()
        {
            inspection = new InspectionClass();
            //InspectionClass.resultDisplay1 = thisFrm.CamDisplay1;  //CamDisplay1主窗体上绘制的VP控件
            //InspectionClass.resultDisplay2 = thisFrm.CamDisplay2;
            //InspectionClass.resultDisplay3 = thisFrm.CamDisplay3;
            //InspectionClass.frmMain = this;  //？？
        }
        #endregion
        private static void DisposeAllCam()
        {
            CogFrameGrabbers frameGrabbers = new CogFrameGrabbers();
            foreach (ICogFrameGrabber fg in frameGrabbers)
                fg.Disconnect(false);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisposeAllCam();
        }

        #region VP运行方法
        /// <summary>
        /// visionpro工具运行方法
        /// </summary>
        /// <param name="isCaOrTst">是标定还是正常测试</param>
        /// <param name="isFeedOrTake">是取料还是放料</param>
        void RunTBTools(bool isCaOrTst, int station)
        {
            try
            {
                if (isCaOrTst)
                {
                    switch (station)
                    {
                        case 0:
                            {
                                if (GlobalVar.noLabelLogoNineCnt < 8)
                                {
                                    ++GlobalVar.noLabelLogoNineCnt;
                                }
                                else
                                {
                                    if (GlobalVar.noLabelLogoNineCnt == 8)
                                    {
                                        ++GlobalVar.noLabelLogoNineCnt;
                                    }
                                    //else
                                    //{
                                    //    ++GlobalVar.noLabelLogoFiveCnt;
                                    //}
                                }
                            }
                            break;
                        case 1:
                            {
                                if (GlobalVar.noGumNineCnt < 8)
                                {
                                    ++GlobalVar.noGumNineCnt;
                                }
                                else
                                {
                                    if (GlobalVar.noGumNineCnt == 8)
                                    {
                                        ++GlobalVar.noGumNineCnt;
                                    }
                                    //else
                                    //{
                                    //    ++GlobalVar.noGumFiveCnt;
                                    //}
                                }
                            }
                            break;
                        case 2:
                            {
                                if (GlobalVar.labeledLogoNineCnt < 8)
                                {
                                    ++GlobalVar.labeledLogoNineCnt;
                                }
                                else
                                {
                                    if (GlobalVar.labeledLogoNineCnt == 8)
                                    {
                                        ++GlobalVar.labeledLogoNineCnt;
                                    }
                                    //else
                                    //{
                                    //    ++GlobalVar.labeledLogoFiveCnt;
                                    //}
                                }
                            }
                            break;
                        case 3:
                            {
                                if (GlobalVar.gumedNineCnt < 8)
                                {
                                    ++GlobalVar.gumedNineCnt;
                                }
                                else
                                {
                                    if (GlobalVar.gumedNineCnt == 8)
                                    {
                                        ++GlobalVar.gumedNineCnt;
                                    }
                                    //else
                                    //{
                                    //    ++GlobalVar.noGumFiveCnt;
                                    //}
                                }
                            }
                            break;
                    }
                }
                if (isCaOrTst)
                {
                    NineCaling(station);
                    //if (station == 0 || station == 2)//第一工位和第三工位做5点标定
                    //{
                    //    FiveCaling(station);//根据实际需求看是否需要判断该工位做5点标定的动作
                    //}
                }
                RunTool(station);
                if (isCaOrTst)
                {
                    NineCaliedFinished(station);
                }
                else
                {
                    RunToolTestfFinished(station);
                }
            }
            catch (CogException e)
            {
                ShowMessage("运行工具出错： " + e.Message);
            }
        }

        void RunToolTestfFinished(int station)
        {
            GetFromTBDataS(station, "ProCenterX");
            GetFromTBDataS(station, "ProCenterY");
            GetFromTBDataS(station, "ProDeg");
            GetFromTBDataS(station, "IsMatchNG");
            RunToolFinishedAfterData(station);
        }

        void RunToolFinishedAfterData(int station)
        {
            switch (station)
            {
                case 0:
                    {
                        if (isTeach)
                        {
                            GlobalVar.noLabelLogoProTeachCenterX = GlobalVar.noLabelLogoProCenterX;
                            GlobalVar.noLabelLogoProTeachCenterY = GlobalVar.noLabelLogoProCenterY;
                            GlobalVar.noLabelLogoProTeachDeg =     GlobalVar.noLabelLogoProDeg;
                            ClassINI.INI.INIWriteValue(GlobalVar.bFilePath, "第一工位示教点", "X",    GlobalVar.noLabelLogoProTeachCenterX.ToString("F3"));
                            ClassINI.INI.INIWriteValue(GlobalVar.bFilePath, "第一工位示教点", "Y",    GlobalVar.noLabelLogoProTeachCenterY.ToString("F3"));
                            ClassINI.INI.INIWriteValue(GlobalVar.bFilePath, "第一工位示教点", "角度", GlobalVar.noLabelLogoProTeachDeg.ToString("F3"));
                        }
                        else
                        {
                            #region 1次补正算法 旋转中心算法需要考虑工具坐标系的转换，在此先不用，继续用工具坐标系
                            //角度差= 示教产品角度-正常测试产品角度=
                            //double degOffset;
                            //degOffset = GlobalVar.noLabelLogoProTeachDeg - GlobalVar.noLabelLogoProDeg;
                            //double mySinDeg = Math.Sin(Math.PI * degOffset / 180);//sin(角度)
                            //double myCosDeg = Math.Cos(Math.PI * degOffset / 180);//cos(角度)
                            //double offsetX = GlobalVar.noLabelLogoProCenterX - GlobalVar.noLabelLogoRaxiasCenterX;
                            ////正常测试产品Y-R轴X
                            //double offsetY = GlobalVar.noLabelLogoProCenterY - GlobalVar.noLabelLogoRaxiasCenterY;
                            //double compensaXVal;
                            //double compensaYVal;
                            ////测试产品绕R轴旋转角度后X'=COS(角度差)*(测试产品X-R轴X)-SIN(角度差)*(测试产品Y-R轴Y)+R轴X
                            //compensaXVal = myCosDeg * offsetX + GlobalVar.noLabelLogoRaxiasCenterX - mySinDeg * offsetY;
                            ////测试产品绕R轴旋转角度后Y'=COS(角度差)*(测试产品Y-R轴Y)-SIN(角度差)*(测试产品X-R轴X)+R轴Y
                            //compensaYVal = myCosDeg * offsetY + GlobalVar.noLabelLogoRaxiasCenterY + mySinDeg * offsetX;
                            //double finalXOffset = -Convert.ToInt32((compensaXVal - GlobalVar.noLabelLogoProTeachCenterX) / GlobalVar.noLabelLogoRatioX);
                            //double finalYOffset = -Convert.ToInt32((compensaYVal - GlobalVar.noLabelLogoProTeachCenterY) / GlobalVar.noLabelLogoRatioY);
                            //double finalDegOffset = Convert.ToInt32(degOffset);
                            ////正常测试产品X-R轴X
                            //ShowMessage("旋转R轴补偿值：" + finalDegOffset);
                            //ShowMessage("X轴补偿脉值：" + finalXOffset);
                            //ShowMessage("Y轴补偿脉值：" + finalYOffset); 
                            #endregion

                            if (!GlobalVar.isCam1CalOrTest)
                            {
                                #region 继续采用工具坐标系
                                firstStaOffsetDeg = GlobalVar.noLabelLogoProDeg - GlobalVar.noLabelLogoProTeachDeg;
                                firstStaOffsetX = (GlobalVar.noLabelLogoProCenterX - GlobalVar.noLabelLogoProTeachCenterX) / GlobalVar.noLabelLogoRatioX;
                                firstStaOffsetY = (GlobalVar.noLabelLogoProCenterY - GlobalVar.noLabelLogoProTeachCenterY) / GlobalVar.noLabelLogoRatioY;
                                if (GlobalVar.noLabelLogoIsMatchNG)
                                {
                                    SendData($"1,1,2,0,0,0");
                                }
                                else
                                {
                                    SendData($"1,1,1,0,0,0");
                                }
                                //正常测试产品X-R轴X
                                ShowMessage("第一工位机器人工具坐标系下，X值偏移量" + firstStaOffsetX);
                                ShowMessage("第一工位机器人工具坐标系下，Y值偏移量" + firstStaOffsetY);
                                ShowMessage("第一工位机器人工具坐标系下，U值偏移量" + firstStaOffsetDeg);
                                #endregion
                            }

                        }
                    }
                    break;
                case 1:
                    {
                        if (isTeach)
                        {
                            if (GlobalVar.noGumRobotU != 0)
                            {
                                GlobalVar.noGumTeachRobotTX = GlobalVar.noGumRobotX;
                                GlobalVar.noGumTeachRobotTY = GlobalVar.noGumRobotY;
                                GlobalVar.noGumTeachRobotTU = GlobalVar.noGumRobotU;
                                ClassINI.INI.INIWriteValue(GlobalVar.bFilePath, "第二工位Robot工具示教点", "X", GlobalVar.noGumTeachRobotTX.ToString("F3"));
                                ClassINI.INI.INIWriteValue(GlobalVar.bFilePath, "第二工位Robot工具示教点", "Y", GlobalVar.noGumTeachRobotTY.ToString("F3"));
                                ClassINI.INI.INIWriteValue(GlobalVar.bFilePath, "第二工位Robot工具示教点", "U", GlobalVar.noGumTeachRobotTU.ToString("F3"));
                                SendData($"3,2,1,0,0,0");
                            }
                            else
                            {
                                GlobalVar.noGumProTeachCenterX = GlobalVar.noGumProCenterX;
                                GlobalVar.noGumProTeachCenterY = GlobalVar.noGumProCenterY;
                                GlobalVar.noGumProTeachDeg = GlobalVar.noGumProDeg;
                                ClassINI.INI.INIWriteValue(GlobalVar.bFilePath, "第二工位示教点", "X", GlobalVar.noGumProTeachCenterX.ToString("F3"));
                                ClassINI.INI.INIWriteValue(GlobalVar.bFilePath, "第二工位示教点", "Y", GlobalVar.noGumProTeachCenterY.ToString("F3"));
                                ClassINI.INI.INIWriteValue(GlobalVar.bFilePath, "第二工位示教点", "角度", GlobalVar.noGumProTeachDeg.ToString("F3"));
                            }
                        }
                        else
                        {
                            if (!GlobalVar.isCam2CalOrTest)
                            {
                                #region 1次补正算法
                                //角度差= 示教产品角度-正常测试产品角度
                                double degOffset;
                                degOffset = GlobalVar.noGumProDeg - GlobalVar.noGumProTeachDeg;
                                double offsetX = (GlobalVar.noGumProCenterX - GlobalVar.noGumProTeachCenterX) / GlobalVar.noGumRatioX;
                                double offsetY = (GlobalVar.noGumProCenterY - GlobalVar.noGumProTeachCenterY) / GlobalVar.noGumRatioY;
                                double compensaXVal;
                                double compensaYVal;
                                double compensaDegVal;
                                compensaXVal = GlobalVar.noGumTeachRobotTX + offsetX;
                                compensaYVal = GlobalVar.noGumTeachRobotTY - offsetY;
                                compensaDegVal = GlobalVar.noGumTeachRobotTU - degOffset;
                                double finX = GlobalVar.noGumTeachRobotTX + offsetX + firstStaOffsetX;
                                double finY = GlobalVar.noGumTeachRobotTY - offsetY + firstStaOffsetY;
                                double finDeg = GlobalVar.noGumTeachRobotTU - degOffset - firstStaOffsetDeg;
                                if (GlobalVar.noGumIsMatchNG)
                                {
                                    SendData($"1,2,2,0,0,0");
                                }
                                else
                                {
                                    SendData($"1,2,1,{finX},{finY},{finDeg}");
                                }
                                //正常测试产品X-R轴X
                                ShowMessage("机器人工具坐标系下，第二工位自己补偿X值" + compensaXVal);
                                ShowMessage("机器人工具坐标系下，第二工位自己补偿Y值" + compensaYVal);
                                ShowMessage("机器人工具坐标系下，第二工位自己补偿U值" + compensaDegVal);
                                ShowMessage("机器人工具坐标系下，第一二工位一起补偿X值" + finX);
                                ShowMessage("机器人工具坐标系下，第一二工位一起补偿Y值" + finY);
                                ShowMessage("机器人工具坐标系下，第一二工位一起补偿U值" + finDeg);
                                firstStaOffsetX = 0;
                                firstStaOffsetY = 0;
                                firstStaOffsetDeg = 0;
                                #endregion
                            }

                        }
                    }
                    break;
                case 2:
                    {
                        if (isTeach)
                        {
                            GlobalVar.labeledLogoProTeachCenterX = GlobalVar.labeledLogoProCenterX;
                            GlobalVar.labeledLogoProTeachCenterY = GlobalVar.labeledLogoProCenterY;
                            GlobalVar.labeledLogoProTeachDeg     = GlobalVar.labeledLogoProDeg;
                            ClassINI.INI.INIWriteValue(GlobalVar.bFilePath, "第三工位示教点", "X",    GlobalVar.labeledLogoProTeachCenterX.ToString("F3"));
                            ClassINI.INI.INIWriteValue(GlobalVar.bFilePath, "第三工位示教点", "Y",    GlobalVar.labeledLogoProTeachCenterY.ToString("F3"));
                            ClassINI.INI.INIWriteValue(GlobalVar.bFilePath, "第三工位示教点", "角度", GlobalVar.labeledLogoProTeachDeg.ToString("F3"));
                        }
                        else
                        {
                            if (!GlobalVar.isCam3CalOrTest)
                            {
                                #region 继续采用工具坐标系
                                thirdStaOffsetDeg = GlobalVar.labeledLogoProDeg - GlobalVar.labeledLogoProTeachDeg;
                                thirdStaOffsetX = (GlobalVar.labeledLogoProCenterX - GlobalVar.labeledLogoProTeachCenterX) / GlobalVar.labeledLogoRatioX;
                                thirdStaOffsetY = (GlobalVar.labeledLogoProCenterY - GlobalVar.labeledLogoProTeachCenterY) / GlobalVar.labeledLogoRatioY;
                                if (GlobalVar.labeledLogoIsMatchNG)
                                {
                                    SendData($"1,3,2,0,0,0");
                                }
                                else
                                {
                                    SendData($"1,3,1,0,0,0");
                                }
                                //正常测试产品X-R轴X
                                ShowMessage("第三工位机器人工具坐标系下，X值偏移量" + thirdStaOffsetX);
                                ShowMessage("第三工位机器人工具坐标系下，Y值偏移量" + thirdStaOffsetY);
                                ShowMessage("第三工位机器人工具坐标系下，U值偏移量" + thirdStaOffsetDeg);
                            }
                            
                            #endregion 
                        }
                    }
                    break;
                case 3:
                    {
                        if (isTeach)
                        {
                            if (GlobalVar.gumedRobotU != 0)
                            {
                                GlobalVar.gumedTeachRobotTX = GlobalVar.gumedRobotX;
                                GlobalVar.gumedTeachRobotTY = GlobalVar.gumedRobotY;
                                GlobalVar.gumedTeachRobotTU = GlobalVar.gumedRobotU;
                                ClassINI.INI.INIWriteValue(GlobalVar.bFilePath, "第四工位Robot工具示教点", "X", GlobalVar.gumedTeachRobotTX.ToString("F3"));
                                ClassINI.INI.INIWriteValue(GlobalVar.bFilePath, "第四工位Robot工具示教点", "Y", GlobalVar.gumedTeachRobotTY.ToString("F3"));
                                ClassINI.INI.INIWriteValue(GlobalVar.bFilePath, "第四工位Robot工具示教点", "U", GlobalVar.gumedTeachRobotTU.ToString("F3"));
                                SendData($"3,4,1,0,0,0");
                            }
                            else
                            {
                                GlobalVar.gumedProTeachCenterX = GlobalVar.gumedProCenterX;
                                GlobalVar.gumedProTeachCenterY = GlobalVar.gumedProCenterY;
                                GlobalVar.gumedProTeachDeg =     GlobalVar.gumedProDeg;
                                ClassINI.INI.INIWriteValue(GlobalVar.bFilePath, "第四工位示教点", "X",    GlobalVar.gumedProTeachCenterX.ToString("F3"));
                                ClassINI.INI.INIWriteValue(GlobalVar.bFilePath, "第四工位示教点", "Y",    GlobalVar.gumedProTeachCenterY.ToString("F3"));
                                ClassINI.INI.INIWriteValue(GlobalVar.bFilePath, "第四工位示教点", "角度", GlobalVar.gumedProTeachDeg.ToString("F3"));
                            }
                        }
                        else
                        {
                            if (!GlobalVar.isCam4CalOrTest)
                            {
                                #region 1次补正算法
                                //角度差= 示教产品角度-正常测试产品角度
                                double degOffset;
                                degOffset = GlobalVar.gumedProDeg - GlobalVar.gumedProTeachDeg;
                                double offsetX = (GlobalVar.gumedProCenterX - GlobalVar.gumedProTeachCenterX) / GlobalVar.gumedRatioX;
                                double offsetY = (GlobalVar.gumedProCenterY - GlobalVar.gumedProTeachCenterY) / GlobalVar.gumedRatioY;
                                double compensaXVal;
                                double compensaYVal;
                                double compensaDegVal;
                                compensaXVal = GlobalVar.gumedTeachRobotTX + offsetX;
                                compensaYVal = GlobalVar.gumedTeachRobotTY - offsetY;
                                compensaDegVal = GlobalVar.gumedTeachRobotTU - degOffset;
                                double finX = GlobalVar.gumedTeachRobotTX + offsetX + thirdStaOffsetX;
                                double finY = GlobalVar.gumedTeachRobotTY - offsetY + thirdStaOffsetY;
                                double finDeg = GlobalVar.gumedTeachRobotTU - degOffset - thirdStaOffsetDeg;
                                if (GlobalVar.gumedIsMatchNG)
                                {
                                    SendData($"1,4,2,0,0,0");
                                }
                                else
                                {
                                    SendData($"1,4,1,{finX},{finY},{finDeg}");
                                }
                                //正常测试产品X-R轴X
                                ShowMessage("机器人工具坐标系下，第四工位自己补偿X值" + compensaXVal);
                                ShowMessage("机器人工具坐标系下，第四工位自己补偿Y值" + compensaYVal);
                                ShowMessage("机器人工具坐标系下，第四工位自己补偿U值" + compensaDegVal);
                                ShowMessage("机器人工具坐标系下，第三四工位一起补偿X值" + finX);
                                ShowMessage("机器人工具坐标系下，第三四工位一起补偿Y值" + finY);
                                ShowMessage("机器人工具坐标系下，第三四工位一起补偿U值" + finDeg);
                                thirdStaOffsetX = 0;
                                thirdStaOffsetY = 0;
                                thirdStaOffsetDeg = 0;
                                #endregion
                            }

                        }
                    }
                    break;
            }
            //发送数据给Robot
        }

        void RunTool(int station)
        {
            switch (station)
            {
                case 0:
                    InspectionClass.tb1.Run();
                    break;
                case 1:
                    InspectionClass.tb2.Run();
                    break;
                case 2:
                    InspectionClass.tb3.Run();
                    break;
                case 3:
                    InspectionClass.tb4.Run();
                    break;
            }
        }

        void NineCaliedFinished(int station)
        {
            switch (station)
            {
                case 0:
                    {
                        #region 9点加5点的写法
                        //if (GlobalVar.noLabelLogoNineCnt >= 9 && GlobalVar.noLabelLogoFiveCnt >= 5)
                        //{
                        //    GlobalVar.noLabelLogoNineCnt = 0;
                        //    GlobalVar.noLabelLogoFiveCnt = 0;
                        //    GlobalVar.isCam1CalOrTest = false;
                        //    ShowMessage("Logo取料工位五点标定动作完成");
                        //    //更新相关界面UI
                        //    //发送数据给Robot
                        //    GetFromTBDataS(station, "RatioX");
                        //    GetFromTBDataS(station, "RatioY");
                        //    ShowMessage($"第{station + 1}工站X轴脉冲比例尺: " + GlobalVar.noLabelLogoRatioX + ";Y轴脉冲比例尺: " + GlobalVar.noLabelLogoRatioY);
                        //    GetFromTBDataS(station, "RaxiasCenterX");
                        //    GetFromTBDataS(station, "RaxiasCenterY");
                        //    ClassINI.INI.INIWriteValue(GlobalVar.bFilePath, "第一工位比例尺", "X", GlobalVar.noLabelLogoRatioX.ToString("F3"));
                        //    ClassINI.INI.INIWriteValue(GlobalVar.bFilePath, "第一工位比例尺", "Y", GlobalVar.noLabelLogoRatioY.ToString("F3"));
                        //    //ClassINI.INI.INIWriteValue(GlobalVar.bFilePath, "第一工位轴中心点", "X", GlobalVar.noLabelLogoRaxiasCenterX.ToString("F3"));
                        //    //ClassINI.INI.INIWriteValue(GlobalVar.bFilePath, "第一工位轴中心点", "Y", GlobalVar.noLabelLogoRaxiasCenterY.ToString("F3"));
                        //}
                        //else
                        //{
                        //    if (GlobalVar.noLabelLogoNineCnt >= 9 && !GlobalVar.noLabelLogoIsCalMatchNG && GlobalVar.noLabelLogoFiveCnt == 0)
                        //    {
                        //        ShowMessage("Logo取料工位九点标定动作完成");
                        //        ShowMessage("Logo取料工位5点标定动作开始");
                        //    }
                        //    GetFromTBDataS(station, "IsCalMatchNG");
                        //    if (GlobalVar.noLabelLogoIsCalMatchNG)//如果标定匹配NG
                        //    {
                        //        //数据先清空
                        //        GlobalVar.noLabelLogoNineCnt = 0;
                        //        GlobalVar.noLabelLogoFiveCnt = 0;
                        //        GlobalVar.isCam1CalOrTest = false;
                        //        ShowMessage("Logo取料工位九点标定匹配NG");
                        //    }
                        //    //更新相关界面UI
                        //    //发送数据给Robot
                        //    if (GlobalVar.noLabelLogoIsCalMatchNG)
                        //    {
                        //        SendData("2,1,2,0,0,0");
                        //    }
                        //    else
                        //    {
                        //        SendData("2,1,1,0,0,0");
                        //    }
                        //}
                        #endregion 

                        if (GlobalVar.noLabelLogoNineCnt == 9 && !GlobalVar.noLabelLogoIsCalMatchNG)
                        {
                            GlobalVar.noLabelLogoNineCnt = 0;
                            GlobalVar.isCam1CalOrTest = false;
                            ShowMessage("Logo取料工位九点标定动作完成");
                            GetFromTBDataS(station, "RatioX");
                            GetFromTBDataS(station, "RatioY");
                            ShowMessage($"第{station + 1}工站X轴脉冲比例尺: " + GlobalVar.noLabelLogoRatioX + ";Y轴脉冲比例尺: " + GlobalVar.noLabelLogoRatioY);
                            ClassINI.INI.INIWriteValue(GlobalVar.bFilePath, "第一工位比例尺", "X", GlobalVar.noLabelLogoRatioX.ToString("F3"));
                            ClassINI.INI.INIWriteValue(GlobalVar.bFilePath, "第一工位比例尺", "Y", GlobalVar.noLabelLogoRatioY.ToString("F3"));
                        }
                        GetFromTBDataS(station, "IsCalMatchNG");
                        if (GlobalVar.noLabelLogoIsCalMatchNG)//如果标定匹配NG
                        {
                            //数据先清空
                            GlobalVar.noLabelLogoNineCnt = 0;
                            GlobalVar.isCam1CalOrTest = false;
                            ShowMessage("Logo取料工位九点标定匹配NG");
                        }
                        if (GlobalVar.noLabelLogoIsCalMatchNG)
                        {
                            SendData("2,1,2,0,0,0");
                        }
                        else
                        {
                            SendData("2,1,1,0,0,0");
                        }
                    }
                    break;
                case 1:
                    {
                        if (GlobalVar.noGumNineCnt == 9 && !GlobalVar.noGumIsCalMatchNG)
                        {
                            GlobalVar.noGumNineCnt = 0;
                            GlobalVar.isCam2CalOrTest = false;
                            GetFromTBDataS(station, "RatioX");
                            GetFromTBDataS(station, "RatioY");
                            ShowMessage($"第{station + 1}工站X轴脉冲比例尺: " + GlobalVar.noGumRatioX + ";Y轴脉冲比例尺: " + GlobalVar.noGumRatioY);
                            ClassINI.INI.INIWriteValue(GlobalVar.bFilePath, "第二工位比例尺", "X", GlobalVar.noGumRatioX.ToString("F3"));
                            ClassINI.INI.INIWriteValue(GlobalVar.bFilePath, "第二工位比例尺", "Y", GlobalVar.noGumRatioY.ToString("F3"));
                            ShowMessage("Logo取料工位九点标定动作完成");
                        }
                        GetFromTBDataS(station, "IsCalMatchNG");
                        if (GlobalVar.noGumIsCalMatchNG)//如果标定匹配NG
                        {
                            //数据先清空
                            GlobalVar.noGumNineCnt = 0;
                            GlobalVar.isCam2CalOrTest = false;
                            ShowMessage("撕标拍照工位九点标定匹配NG");
                        }
                        if (GlobalVar.noGumIsCalMatchNG)
                        {
                            SendData("2,2,2,0,0,0");
                        }
                        else
                        {
                            SendData("2,2,1,0,0,0");
                        }
                    }
                    break;
                case 2:
                    {
                        if (GlobalVar.labeledLogoNineCnt == 9 && !GlobalVar.labeledLogoIsCalMatchNG)
                        {
                            GlobalVar.labeledLogoNineCnt = 0;
                            GlobalVar.isCam3CalOrTest = false;
                            ShowMessage("Logo贴标后工位九点标定动作完成");
                            GetFromTBDataS(station, "RatioX");
                            GetFromTBDataS(station, "RatioY");
                            ShowMessage($"第{station + 1}工站X轴脉冲比例尺: " + GlobalVar.labeledLogoRatioX + ";Y轴脉冲比例尺: " + GlobalVar.labeledLogoRatioY);
                            ClassINI.INI.INIWriteValue(GlobalVar.bFilePath, "第三工位比例尺", "X", GlobalVar.labeledLogoRatioX.ToString("F3"));
                            ClassINI.INI.INIWriteValue(GlobalVar.bFilePath, "第三工位比例尺", "Y", GlobalVar.labeledLogoRatioY.ToString("F3"));
                        }
                        GetFromTBDataS(station, "IsCalMatchNG");
                        if (GlobalVar.labeledLogoIsCalMatchNG)//如果标定匹配NG
                        {
                            //数据先清空
                            GlobalVar.labeledLogoNineCnt = 0;
                            GlobalVar.isCam3CalOrTest = false;
                            ShowMessage("Logo贴标后工位九点标定匹配NG");
                        }
                        if (GlobalVar.labeledLogoIsCalMatchNG)
                        {
                            SendData("2,3,2,0,0,0");
                        }
                        else
                        {
                            SendData("2,3,1,0,0,0");
                        }
                    }
                    break;
                case 3:
                    {
                        if (GlobalVar.gumedNineCnt == 9 && !GlobalVar.gumedIsCalMatchNG)
                        {
                            GlobalVar.gumedNineCnt = 0;
                            GlobalVar.isCam4CalOrTest = false;
                            GetFromTBDataS(station, "RatioX");
                            GetFromTBDataS(station, "RatioY");
                            ShowMessage($"第{station + 1}工站X轴脉冲比例尺: " + GlobalVar.gumedRatioX + ";Y轴脉冲比例尺: " + GlobalVar.gumedRatioY);
                            ClassINI.INI.INIWriteValue(GlobalVar.bFilePath, "第四工位比例尺", "X", GlobalVar.gumedRatioX.ToString("F3"));
                            ClassINI.INI.INIWriteValue(GlobalVar.bFilePath, "第四工位比例尺", "Y", GlobalVar.gumedRatioY.ToString("F3"));
                            ShowMessage("Logo压合到塑胶件工位九点标定动作完成");
                        }
                        GetFromTBDataS(station, "IsCalMatchNG");
                        if (GlobalVar.gumedIsCalMatchNG)//如果标定匹配NG
                        {
                            //数据先清空
                            GlobalVar.gumedNineCnt = 0;
                            GlobalVar.isCam4CalOrTest = false;
                            ShowMessage("Logo压合到塑胶件工位九点标定匹配NG");
                        }
                        if (GlobalVar.gumedIsCalMatchNG)
                        {
                            SendData("2,4,2,0,0,0");
                        }
                        else
                        {
                            SendData("2,4,1,0,0,0");
                        }
                    }
                    break;
            }
        }

        void NineCaling(int station)
        {
            LoadSettingToTBS(station, "CalibratingCount");
            LoadSettingToTBS(station, "RobotX");
            LoadSettingToTBS(station, "RobotY");
        }

        void FiveCaling(int station)
        {
            LoadSettingToTBS(station, "FiveCalCount");
        }

        #endregion

        #region 加载配置到2个TB工具的输入参数里
        /// <summary>
        /// 加载配置到toolblock
        /// </summary>
        /// <param name="station">工位</param>
        /// <param name="myParams">需要填充到TB的数据名称</param>
        private void LoadSettingToTBS(int station, string myParams)
        {
            try
            {
                switch (myParams)
                {
                    case "isCalOrTest":
                        switch (station)
                        {
                            case 0:
                                InspectionClass.tb1.Inputs[myParams].Value = GlobalVar.isCam1CalOrTest;
                                break;
                            case 1:
                                InspectionClass.tb2.Inputs[myParams].Value = GlobalVar.isCam2CalOrTest;
                                break;
                            case 2:
                                InspectionClass.tb3.Inputs[myParams].Value = GlobalVar.isCam3CalOrTest;
                                break;
                            case 3:
                                InspectionClass.tb4.Inputs[myParams].Value = GlobalVar.isCam4CalOrTest;
                                break;
                        }
                        break;
                    case "CalibratingCount":
                        switch (station)
                        {
                            case 0:
                                InspectionClass.tb1.Inputs[myParams].Value = GlobalVar.noLabelLogoNineCnt;
                                break;
                            case 1:
                                InspectionClass.tb2.Inputs[myParams].Value = GlobalVar.noGumNineCnt;
                                break;
                            case 2:
                                InspectionClass.tb3.Inputs[myParams].Value = GlobalVar.labeledLogoNineCnt;
                                break;
                            case 3:
                                InspectionClass.tb4.Inputs[myParams].Value = GlobalVar.gumedNineCnt;
                                break;
                        }
                        break;
                    //case "FiveCalCount":
                    //    switch (station)
                    //    {
                    //        case 0:
                    //            InspectionClass.tb1.Inputs[myParams].Value = GlobalVar.noLabelLogoFiveCnt;
                    //            break;
                    //        case 1:
                    //            //InspectionClass.tb2.Inputs[myParams].Value = GlobalVar.noGumFiveCnt;
                    //            break;
                    //        case 2:
                    //            InspectionClass.tb2.Inputs[myParams].Value = GlobalVar.isCam2CalOrTest;
                    //            break;
                    //        case 3:
                    //            InspectionClass.tb2.Inputs[myParams].Value = GlobalVar.isCam2CalOrTest;
                    //            break;
                    //    }
                    //    break;
                    case "RobotX":
                        switch (station)
                        {
                            case 0:
                                InspectionClass.tb1.Inputs[myParams].Value = GlobalVar.noLabelLogoRobotX;
                                break;
                            case 1:
                                InspectionClass.tb2.Inputs[myParams].Value = GlobalVar.noGumRobotX;
                                break;
                            case 2:
                                InspectionClass.tb3.Inputs[myParams].Value = GlobalVar.labeledLogoRobotX;
                                break;
                            case 3:
                                InspectionClass.tb4.Inputs[myParams].Value = GlobalVar.gumedRobotX;
                                break;
                        }
                        break;
                    case "RobotY":
                        switch (station)
                        {
                            case 0:
                                InspectionClass.tb1.Inputs[myParams].Value = GlobalVar.noLabelLogoRobotY;
                                break;
                            case 1:
                                InspectionClass.tb2.Inputs[myParams].Value = GlobalVar.noGumRobotY;
                                break;
                            case 2:
                                InspectionClass.tb3.Inputs[myParams].Value = GlobalVar.labeledLogoRobotY;
                                break;
                            case 3:
                                InspectionClass.tb4.Inputs[myParams].Value = GlobalVar.gumedRobotY;
                                break;
                        }
                        break;
                }
            }
            catch (Exception e)
            {
                ShowMessage("错误信息是： " + e.Message);
                return;
            }
        }

        /// <summary>
        /// 从toolblock获取数据
        /// </summary>
        /// <param name="station">工位</param>
        /// <param name="myParams">TB的数据名称</param>
        private void GetFromTBDataS(int station, string myParams)
        {
            try
            {
                switch (myParams)
                {
                    case "RatioX":    //X轴像素脉冲比
                        switch (station)
                        {
                            case 0:
                                GlobalVar.noLabelLogoRatioX = Convert.ToDouble(InspectionClass.tb1.Outputs[myParams].Value);
                                break;
                            case 1:
                                GlobalVar.noGumRatioX = Convert.ToDouble(InspectionClass.tb2.Outputs[myParams].Value);
                                break;
                            case 2:
                                GlobalVar.labeledLogoRatioX = Convert.ToDouble(InspectionClass.tb3.Outputs[myParams].Value);
                                break;
                            case 3:
                                GlobalVar.gumedRatioX = Convert.ToDouble(InspectionClass.tb4.Outputs[myParams].Value);
                                break;
                        }
                        break;
                    case "RatioY":   //Y轴像素脉冲比
                        switch (station)
                        {
                            case 0:
                                GlobalVar.noLabelLogoRatioY = Convert.ToDouble(InspectionClass.tb1.Outputs[myParams].Value);
                                break;
                            case 1:
                                GlobalVar.noGumRatioY = Convert.ToDouble(InspectionClass.tb2.Outputs[myParams].Value);
                                break;
                            case 2:
                                GlobalVar.labeledLogoRatioY = Convert.ToDouble(InspectionClass.tb3.Outputs[myParams].Value);
                                break;
                            case 3:
                                GlobalVar.gumedRatioY = Convert.ToDouble(InspectionClass.tb4.Outputs[myParams].Value);
                                break;
                        }
                        break;
                    case "ProCenterX":  //产品中心X坐标
                        switch (station)
                        {
                            case 0:
                                GlobalVar.noLabelLogoProCenterX = Convert.ToDouble(InspectionClass.tb1.Outputs[myParams].Value);
                                break;
                            case 1:
                                GlobalVar.noGumProCenterX = Convert.ToDouble(InspectionClass.tb2.Outputs[myParams].Value);
                                break;
                            case 2:
                                GlobalVar.labeledLogoProCenterX = Convert.ToDouble(InspectionClass.tb3.Outputs[myParams].Value);
                                break;
                            case 3:
                                GlobalVar.gumedProCenterX = Convert.ToDouble(InspectionClass.tb4.Outputs[myParams].Value);
                                break;
                        }
                        break;
                    case "ProCenterY":  //产品中心Y坐标
                        switch (station)
                        {
                            case 0:
                                GlobalVar.noLabelLogoProCenterY = Convert.ToDouble(InspectionClass.tb1.Outputs[myParams].Value);
                                break;
                            case 1:
                                GlobalVar.noGumProCenterY = Convert.ToDouble(InspectionClass.tb2.Outputs[myParams].Value);
                                break;
                            case 2:
                                GlobalVar.labeledLogoProCenterY = Convert.ToDouble(InspectionClass.tb3.Outputs[myParams].Value);
                                break;
                            case 3:
                                GlobalVar.gumedProCenterY = Convert.ToDouble(InspectionClass.tb4.Outputs[myParams].Value);
                                break;
                        }
                        break;
                    case "ProDeg":  //产品夹角
                        switch (station)
                        {
                            case 0:
                                GlobalVar.noLabelLogoProDeg = Convert.ToDouble(InspectionClass.tb1.Outputs[myParams].Value);
                                break;
                            case 1:
                                GlobalVar.noGumProDeg = Convert.ToDouble(InspectionClass.tb2.Outputs[myParams].Value);
                                break;
                            case 2:
                                GlobalVar.labeledLogoProDeg = Convert.ToDouble(InspectionClass.tb3.Outputs[myParams].Value);
                                break;
                            case 3:
                                GlobalVar.gumedProDeg = Convert.ToDouble(InspectionClass.tb4.Outputs[myParams].Value);
                                break;
                        }
                        break;
                    case "IsMatchNG":  //示教匹配NG
                        switch (station)
                        {
                            case 0:
                                GlobalVar.noLabelLogoIsMatchNG = Convert.ToBoolean(InspectionClass.tb1.Outputs[myParams].Value);
                                break;
                            case 1:
                                GlobalVar.noGumIsMatchNG = Convert.ToBoolean(InspectionClass.tb2.Outputs[myParams].Value);
                                break;
                            case 2:
                                GlobalVar.labeledLogoIsMatchNG = Convert.ToBoolean(InspectionClass.tb3.Outputs[myParams].Value);
                                break;
                            case 3:
                                GlobalVar.gumedIsMatchNG = Convert.ToBoolean(InspectionClass.tb4.Outputs[myParams].Value);
                                break;
                        }
                        break;
                    case "IsCalMatchNG":  //标定匹配NG
                        switch (station)
                        {
                            case 0:
                                GlobalVar.noLabelLogoIsCalMatchNG = Convert.ToBoolean(InspectionClass.tb1.Outputs[myParams].Value);
                                break;
                            case 1:
                                GlobalVar.noGumIsCalMatchNG = Convert.ToBoolean(InspectionClass.tb2.Outputs[myParams].Value);
                                break;
                            case 2:
                                GlobalVar.labeledLogoIsCalMatchNG = Convert.ToBoolean(InspectionClass.tb3.Outputs[myParams].Value);
                                break;
                            case 3:
                                GlobalVar.gumedIsCalMatchNG = Convert.ToBoolean(InspectionClass.tb4.Outputs[myParams].Value);
                                break;
                        }
                        break;
                    case "RaxiasCenterX":  //5点标定后中心坐标X
                        switch (station)
                        {
                            case 0:
                                //GlobalVar.noLabelLogoRaxiasCenterX = Convert.ToDouble(InspectionClass.tb1.Outputs[myParams].Value);
                                break;
                            case 1:
                                //GlobalVar.noGumRaxiasCenterX = Convert.ToDouble(InspectionClass.tb2.Outputs[myParams].Value);
                                break;
                            case 2:

                                break;
                            case 3:

                                break;
                        }
                        break;
                    case "RaxiasCenterY":  //5点标定后中心坐标Y
                        switch (station)
                        {
                            case 0:
                                //GlobalVar.noLabelLogoRaxiasCenterY = Convert.ToDouble(InspectionClass.tb1.Outputs[myParams].Value);
                                break;
                            case 1:
                                //GlobalVar.noGumRaxiasCenterY = Convert.ToDouble(InspectionClass.tb2.Outputs[myParams].Value);
                                break;
                            case 2:

                                break;
                            case 3:

                                break;
                        }
                        break;
                }
            }
            catch (Exception e)
            {
                ShowMessage("错误信息是： " + e.Message);
                return;
            }
        }

        #endregion

        #region 更新UI状态
        private void UpdateMenuEnableStatus(bool enableStatus)
        {
            this.Invoke(new Action(() =>
            {

            }));
        }

        private void LoadUiData()
        {
            try
            {
                cogToolBlockEditV21.Subject = InspectionClass.tb1;
                cogToolBlockEditV22.Subject = InspectionClass.tb2;
                cogToolBlockEditV23.Subject = InspectionClass.tb3;
                cogToolBlockEditV24.Subject = InspectionClass.tb4;
                cogToolBlockEditV21.Visible = true;
                cogToolBlockEditV22.Visible = false;
                cogToolBlockEditV23.Visible = false;
                cogToolBlockEditV24.Visible = false;
                radioButton1.Checked = true;
                radioButton2.Checked = false;
                GlobalVar.isLoadAllParam = true;
            }
            catch (Exception)
            {
                GlobalVar.isLoadAllParam = false;
            }

        }

        #endregion

        private void SendData(string datas)
        {
            try
            {
                IList<MyAppSession> list = this.tcpServerEngine.GetSessions(s => s.Connected == true).ToList();
                if (list.Count > 0)
                {
                    MyAppSession appSession = (MyAppSession)list[0];
                    if (appSession == null)
                    {
                        ShowMessage("没有选中任何在线客户端！");
                        return;
                    }
                    if (!appSession.Connected)
                    {
                        ShowMessage("目标客户端不在线！");
                        return;
                    }
                    string msg = string.Format("{0}", datas + Environment.NewLine);//一定要加上分隔符
                    byte[] bMsg = System.Text.Encoding.UTF8.GetBytes(msg);//消息使用UTF-8编码
                                                                          //this.tcpServerEngine.GetSessionByID(appSession.SessionID).Send(new ArraySegment<byte>(bMsg, 0, bMsg.Length));
                    appSession.Send(new ArraySegment<byte>(bMsg, 0, bMsg.Length));
                }
            }
            catch (Exception ee)
            {
                ShowMessage(ee.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GlobalVar.isCam1CalOrTest = true;
            LoadSettingToTBS(0, "isCalOrTest");
            isTeach = false;
            textBox_msg.Text = "2,1,0,0,0,0";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GlobalVar.isCam2CalOrTest = true;
            LoadSettingToTBS(1, "isCalOrTest");
            isTeach = false;
            textBox_msg.Text = "2,2,0,0,0,0";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GlobalVar.isCam3CalOrTest = true;
            LoadSettingToTBS(2, "isCalOrTest");
            isTeach = false;
            textBox_msg.Text = "2,3,0,0,0,0";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            GlobalVar.isCam4CalOrTest = true;
            LoadSettingToTBS(3, "isCalOrTest");
            isTeach = false;
            textBox_msg.Text = "2,4,0,0,0,0";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            GlobalVar.isCam1CalOrTest = false;
            LoadSettingToTBS(0, "isCalOrTest");
            isTeach = true;
            textBox_msg.Text = "3,1,0,0,0,0";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            GlobalVar.isCam2CalOrTest = false;
            LoadSettingToTBS(1, "isCalOrTest");
            isTeach = true;
            textBox_msg.Text = "3,2,0,0,0,0";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            GlobalVar.isCam3CalOrTest = false;
            LoadSettingToTBS(2, "isCalOrTest");
            isTeach = true;
            textBox_msg.Text = "3,3,0,0,0,0";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            GlobalVar.isCam4CalOrTest = false;
            LoadSettingToTBS(3, "isCalOrTest");
            isTeach = true;
            textBox_msg.Text = "3,4,0,0,0,0";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            GlobalVar.isCam1CalOrTest = false;
            LoadSettingToTBS(0, "isCalOrTest");
            isTeach = false;
            textBox_msg.Text = "1,1,0,0,0,0";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            GlobalVar.isCam2CalOrTest = false;
            LoadSettingToTBS(1, "isCalOrTest");
            isTeach = false;
            textBox_msg.Text = "1,2,0,0,0,0";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            GlobalVar.isCam3CalOrTest = false;
            LoadSettingToTBS(2, "isCalOrTest");
            isTeach = false;
            textBox_msg.Text = "1,3,0,0,0,0";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            GlobalVar.isCam4CalOrTest = false;
            LoadSettingToTBS(3, "isCalOrTest");
            isTeach = false;
            textBox_msg.Text = "1,4,0,0,0,0";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            cogToolBlockEditV21.Visible = radioButton1.Checked;
            cogToolBlockEditV22.Visible = radioButton2.Checked;
            cogToolBlockEditV23.Visible = radioButton3.Checked;
            cogToolBlockEditV24.Visible = radioButton4.Checked;
        }
    }
}
