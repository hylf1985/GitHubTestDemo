using ClassINI;
using Cognex.VisionPro;
using Cognex.VisionPro.Display;
using Cognex.VisionPro.Exceptions;
using csIOC0640;
using csLTDMC;
using HY.Redis.Service;
using KEE.Renault.CamOperator;
using KEE.Renault.Common;
using KEE.Renault.Data;
using KEE.Renault.Step;
using KEE.Renault.Utility;
using RegalPrinter;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace KEE.Renault.MenuForm
{
    public partial class FrmHomePage : Form
    {
        public FrmHomePage()
        {
            InitializeComponent();
            thisFrm = this;
            InitialzeCamDisplay();
            InitSocketEvent();
            InitCfgfile();
            InitMESData();
        }

        #region 变量声明

        #region 图片以字典方式保存测试变量
        /// <summary>
        /// 
        /// </summary>
        //static Dictionary<string, CogImage8Grey> picDicList = new Dictionary<string, CogImage8Grey>();
        static Dictionary<string, CogRecordDisplay> picDicList = new Dictionary<string, CogRecordDisplay>();
        #endregion
        /// <summary>
        /// 程序刚开机设备是否回原点
        /// </summary>
        static bool isFirstGoHome = false;
        static double finThrX = 0;
        static double finThrY = 0;
        static double finThrDeg = 0;
        static bool isStart = false;//启动或停止状态
        static string aoiFinalBarcode = "NoBarcode";

        /// <summary>
        /// 第三相机是否拍完照
        /// </summary>
        //static bool isThrCamShot = false;
        /// <summary>
        /// 第四相机是否拍完照
        /// </summary>
        //static bool isFourCamShot = false;
        /// <summary>
        /// 初始化绑定默认关键词（此数据源可以从数据库取）
        /// </summary>
        List<string> listOnit = new List<string>();
        /// <summary>
        /// 输入key之后，返回的关键词
        /// </summary>
        List<string> listNew = new List<string>();
        /// <summary>
        /// 存储日志信息
        /// </summary>
        List<string> curMsg = new List<string>();

        DataTable dtSelect = null;
        static FrmHomePage thisFrm = null;
        bool isTeach = false;
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
        /// 急停是否按下
        /// </summary>
        static bool isEStopIsOn = false;
        /// <summary>
        /// 因报警产生的暂停
        /// </summary>
        //static bool isAlarmPause = false;
        /// <summary>
        /// 正常需求的暂停
        /// </summary>
        static bool isManualPause = false;
        /// <summary>
        /// excel导出路径
        /// </summary>
        string excelPath = "";
        /// <summary>
        /// 是否按下停止或者急停后或者第一次开启程序重置了步骤
        /// </summary>
        static bool isFirStaStepReset = false;
        #region 界面按钮闪烁信号
        /// <summary>
        /// 开始按钮状态
        /// </summary>
        bool isStartButton = false;
        /// <summary>
        /// 暂停按钮状态
        /// </summary>
        bool isPauseButton = false;
        /// <summary>
        /// 停止按钮状态
        /// </summary>
        bool isStopButton = false;
        /// <summary>
        /// 报警状态
        /// </summary>
        bool isAlarmButton = false;
        /// <summary>
        /// 结束生产状态
        /// </summary>
        bool isEndManuProButton = false;
        /// <summary>
        /// AOI是否拍照完成
        /// </summary>
        static bool isAOIShotFinished = false;
        /// <summary>
        /// 第二工站测试的Flag
        /// </summary>
        static bool isSecStaTest = false;

        #endregion

        #region 按钮以及安全门相关信号变量
        static bool isStartBtnResponse = false;
        static bool isStopBtnResponse = false;
        /// <summary>
        /// 安全门信号是否触发
        /// </summary>
        static bool isSafeDoorTrig = false;
        /// <summary>
        /// 报警触发信号
        /// </summary>
        static bool isAlarmTrig = false;
        /// <summary>
        /// 机器人真空为建立触发信号
        /// </summary>
        static bool isRobotAlarmTrig = false;
        /// <summary>
        /// 工单信息
        /// </summary>
        List<WorkOrder> woList = new List<WorkOrder>();
        #endregion
        #region 分析图片数据使用的变量

        static int picAnalysisCnt = 0;

        static List<int> picAoiAnaylistCnt = new List<int>();

        #endregion

        #region 丢料轴用来判断丢到哪个区域的变量
        static List<bool> finalTakeHighResAndAoiRes = new List<bool>();
        #endregion 

        #region 测试电镀件内边距中心-外边距中心变量定义
        private double noLabelLogoProMCenterX = 0;
        private double noLabelLogoProMCenterY = 0;
        /// <summary>
        /// 内中心-外中心的X差值
        /// </summary>
        double noLabelLogoProOffsetX = 0;
        /// <summary>
        /// 内中心-外中心的X差值
        /// </summary>
        double noLabelLogoProOffsetY = 0;
        #endregion

        /// <summary>
        /// 如果机器人贴背胶或者贴标超出范围的标记
        /// </summary>
        static bool isRobotOverHappen = false;
        #endregion

        private void InitialzeCamDisplay()
        {
            InspectionClass.resultDisplay1 = thisFrm.CamDisplay1;
            InspectionClass.resultDisplay2 = thisFrm.CamDisplay2;
            InspectionClass.resultDisplay3 = thisFrm.CamDisplay3;
            InspectionClass.resultDisplay4 = thisFrm.CamDisplay4;
            InspectionClass.resultDisplayAOI = thisFrm.FrmHome;
            GlobalVar.isCam1CalOrTest = false;
            GlobalVar.isCam2CalOrTest = false;
            GlobalVar.isCam3CalOrTest = false;
            GlobalVar.isCam4CalOrTest = false;
            LoadSettingToTBS(0, "isCalOrTest");
            LoadSettingToTBS(1, "isCalOrTest");
            LoadSettingToTBS(2, "isCalOrTest");
            LoadSettingToTBS(3, "isCalOrTest");
            LoadAoiSettingToTBS();
            isTeach = false;
        }

        #region 初始化sokcet事件
        private void InitSocketEvent()
        {
            if (GlobalVar.tcpServerEngine != null)
            {
                GlobalVar.tcpServerEngine.NewSessionConnected += tcpServerEngine_NewSessionConnected;
                GlobalVar.tcpServerEngine.NewRequestReceived += tcpServerEngine_NewRequestReceived;
                GlobalVar.tcpServerEngine.SessionClosed += tcpServerEngine_SessionClosed;

            }
            else
                GlobalVar.myLog.Error("请检查配置文件中是否有可用的服务信息！");
        }

        void tcpServerEngine_SessionClosed(MyAppSession session, global::SuperSocket.SocketBase.CloseReason value)
        {
            LogMessage(session.RemoteEndPoint + " -> 下线");
            MyAppSession outAppSession;
            GlobalVar.mOnLineConnections.TryRemove(session.SessionID, out outAppSession);
            this.Invoke(new Action(() => { lalEpsonSocketStatus.ForeColor = Color.Red; lalEpsonSocketStatus.Text = "连线断开"; }));

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

                                //GlobalVar.noLabelLogoRobotX = Convert.ToDouble(datas[1]);
                                //GlobalVar.noLabelLogoRobotY = Convert.ToDouble(datas[2]);
                                RunTBTools(false, 0);
                                ControlLightOn(0, false);
                                break;
                            case "2"://出标位置上相机拍照
                                //ControlLightOn(1, true);
                                GlobalVar.noGumRobotX = Convert.ToDouble(datas[1]);
                                GlobalVar.noGumRobotY = Convert.ToDouble(datas[2]);
                                GlobalVar.noGumRobotU = Convert.ToDouble(datas[3]);
                                RunTBTools(false, 1);
                                ControlLightOn(1, false);
                                break;
                            case "3"://贴完背胶后Logo下相机拍照
                                //ControlLightOn(2, true);
                                GlobalVar.labeledLogoRobotX = Convert.ToDouble(datas[1]);
                                GlobalVar.labeledLogoRobotY = Convert.ToDouble(datas[2]);
                                RunTBTools(false, 2);
                                ControlLightOn(2, false);
                                break;
                            case "4"://上相机塑胶件拍照
                                //ControlLightOn(3, true);
                                GlobalVar.gumedRobotX = Convert.ToDouble(datas[1]);
                                GlobalVar.gumedRobotY = Convert.ToDouble(datas[2]);
                                GlobalVar.gumedRobotU = Convert.ToDouble(datas[3]);
                                RunTBTools(false, 3);
                                ControlLightOn(3, false);
                                break;
                        }
                    }
                    LogMessage(session.RemoteEndPoint + " ->" + requestInfo.Body);
                    break;
                case "heartbeat":
                    LogMessage(session.RemoteEndPoint + " ->" + requestInfo.Body);
                    string msg = string.Format("push {0}", requestInfo.Body + Environment.NewLine);//一定要加上分隔符
                    byte[] bMsg = System.Text.Encoding.UTF8.GetBytes(msg);//消息使用UTF-8编码
                    session.Send(new ArraySegment<byte>(bMsg, 0, bMsg.Length));
                    break;
                default:
                    LogMessage(session.RemoteEndPoint + " ->未知的指令（error unknow command）");
                    break;
            }
        }

        void tcpServerEngine_NewSessionConnected(MyAppSession session)
        {
            this.Invoke(new Action(() => { lalEpsonSocketStatus.ForeColor = Color.Green; lalEpsonSocketStatus.Text = "连线成功"; }));

            GlobalVar.mOnLineConnections.TryAdd(session.SessionID, session);

        }

        #endregion

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
                RunTool(station);
                RunToolTestfFinished(station);
            }
            catch (CogException e)
            {
                GlobalVar.myLog.Error("运行工具出错： " + e.Message);
            }
        }

        void RunToolTestfFinished(int station)
        {
            if (station==0)
            {
                GetFromTBDataS(station, "ProMCenterX");
                GetFromTBDataS(station, "ProMCenterY");
            }
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
                            GlobalVar.noLabelLogoProTeachDeg = GlobalVar.noLabelLogoProDeg;
                            ClassINI.INI.INIWriteValue(GlobalVar.curProName == "Renault" ? GlobalVar.bRecipeRenaultFilePath : GlobalVar.bRecipeLadaFilePath, "第一工位示教点", "X", GlobalVar.noLabelLogoProTeachCenterX.ToString("F3"));
                            ClassINI.INI.INIWriteValue(GlobalVar.curProName == "Renault" ? GlobalVar.bRecipeRenaultFilePath : GlobalVar.bRecipeLadaFilePath, "第一工位示教点", "Y", GlobalVar.noLabelLogoProTeachCenterY.ToString("F3"));
                            ClassINI.INI.INIWriteValue(GlobalVar.curProName == "Renault" ? GlobalVar.bRecipeRenaultFilePath : GlobalVar.bRecipeLadaFilePath, "第一工位示教点", "角度", GlobalVar.noLabelLogoProTeachDeg.ToString("F3"));
                        }
                        else
                        {

                            if (!GlobalVar.isCam1CalOrTest)
                            {
                                if (GlobalVar.noLabelLogoIsMatchNG)
                                {
                                    GlobalVar.SendData($"1,1,2,0,0,0");
                                    //IAsyncResult ia_TBImgSave = GraphicsImgSave.BeginInvoke(InspectionClass.resultDisplay1, @"未贴背胶电镀件\NG\" + DateTime.Now.ToString("HH-mm-ss"), null, ImgSave);
                                    IAsyncResult ia_TBImgSave = GraphicsImgSave.BeginInvoke(InspectionClass.resultDisplay1, @"未贴背胶电镀件\NG\" + DateTime.Now.ToString("HH-mm-ss"), null, GraphicsImgSave);
                                    IAsyncResult ia_TBImgSave1 = ImgSave.BeginInvoke(InspectionClass.global1308GreyImage1, @"未贴背胶电镀件\NG1\" + DateTime.Now.ToString("HH-mm-ss"), null, ImgSave);
                                }
                                else
                                {
                                    #region 继续采用工具坐标系
                                    firstStaOffsetDeg = GlobalVar.noLabelLogoProDeg - GlobalVar.noLabelLogoProTeachDeg;
                                    firstStaOffsetX = (GlobalVar.noLabelLogoProCenterX - GlobalVar.noLabelLogoProTeachCenterX) / GlobalVar.noLabelLogoRatioX;
                                    firstStaOffsetY = (GlobalVar.noLabelLogoProCenterY - GlobalVar.noLabelLogoProTeachCenterY) / GlobalVar.noLabelLogoRatioY;
                                    noLabelLogoProOffsetX = (noLabelLogoProMCenterX - GlobalVar.noLabelLogoProCenterX)/ GlobalVar.noLabelLogoRatioX;
                                    noLabelLogoProOffsetY = (noLabelLogoProMCenterY - GlobalVar.noLabelLogoProCenterY)/ GlobalVar.noLabelLogoRatioY;
                                    #region  增加电镀件内外轮廓偏位判断功能 by 薛姣奎
                                    if (noLabelLogoProOffsetX > GlobalVar.logoXmin && noLabelLogoProOffsetX < GlobalVar.logoXmax && noLabelLogoProOffsetY > GlobalVar.logoYmin && noLabelLogoProOffsetY < GlobalVar.logoYmax)
                                    {
                                        //第一位：1 表示请机器人正常测试走点；2 表示请机器人走9点标定 ； 3 表示示教 
                                        //第二位：1 表示取料位logo下相机牌照；2 表示出表位置上相机拍照；3 表示贴背胶后logo下相机拍照；4 表示下相机塑胶件拍照；5 表示AOI相机拍照
                                        //第三位：1 表示图像识别OK; 2 表示图像识别NG
                                        //第四位：1 表示X轴绝对位置，此位置可以为空数据
                                        //第五位：1 表示Y轴绝对位置，此位置可以为空数据
                                        //第六位：1 表示U轴绝对位置，此位置可以为空数据
                                        GlobalVar.SendData($"1,1,1,0,0,0");
                                    }
                                    else
                                    {
                                        GlobalVar.SendData($"1,1,2,0,0,0");
                                    }
                                    //try
                                    //{
                                    //    GlobalVar.mySqlDb.ExecuteNonQuery($"insert into LogoOffsetData  values('{noLabelLogoProMCenterX}','{GlobalVar.noLabelLogoProCenterX}','{noLabelLogoProMCenterY}','{GlobalVar.noLabelLogoProCenterY}','{noLabelLogoProOffsetX}','{noLabelLogoProMCenterY}'", null);
                                    //}
                                    //catch (Exception)
                                    //{

                                    //}
                                    #endregion
                                    Debug.WriteLine($"第一工位当前的中心值：{GlobalVar.noLabelLogoProCenterX / GlobalVar.noLabelLogoRatioX},{GlobalVar.noLabelLogoProCenterY / GlobalVar.noLabelLogoRatioY},{GlobalVar.noLabelLogoProDeg}");
                                    Debug.WriteLine($"第一工位示教点值：{GlobalVar.noLabelLogoProTeachCenterX / GlobalVar.noLabelLogoRatioX},{GlobalVar.noLabelLogoProTeachCenterY / GlobalVar.noLabelLogoRatioY},{GlobalVar.noLabelLogoProTeachDeg}");
                                    //正常测试产品X-R轴X
                                    LogMessage("第一工位机器人工具坐标系下，X值偏移量" + firstStaOffsetX);
                                    LogMessage("第一工位机器人工具坐标系下，Y值偏移量" + firstStaOffsetY);
                                    LogMessage("第一工位机器人工具坐标系下，U值偏移量" + firstStaOffsetDeg);
                                    
                                    #endregion
                                    if (GlobalVar.isAnalysisPic)
                                    {
                                        if (!picDicList.ContainsKey($"{GlobalVar.curWo}-未贴背胶电镀件-{picAnalysisCnt}"))
                                        {
                                            picDicList.Add($"{GlobalVar.curWo}-未贴背胶电镀件-{picAnalysisCnt}", InspectionClass.resultDisplay1);
                                        }
                                        else
                                        {
                                            picDicList[$"{GlobalVar.curWo}-未贴背胶电镀件-{picAnalysisCnt}"] = InspectionClass.resultDisplay1;
                                        }
                                    }
                                    IAsyncResult ia_TBImgSave = GraphicsImgSave.BeginInvoke(InspectionClass.resultDisplay1, @"未贴背胶电镀件\OK\" + DateTime.Now.ToString("HH-mm-ss"), null, GraphicsImgSave);
                                    IAsyncResult ia_TBImgSave1 = ImgSave.BeginInvoke(InspectionClass.global1308GreyImage1, @"未贴背胶电镀件\OK1\" + DateTime.Now.ToString("HH-mm-ss"), null, ImgSave);
                                }
                            }
                        }
                        ControlLightOn(1, true);
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
                                ClassINI.INI.INIWriteValue(GlobalVar.bConfigFilePath, "第二工位Robot工具示教点", "X", GlobalVar.noGumTeachRobotTX.ToString("F3"));
                                ClassINI.INI.INIWriteValue(GlobalVar.bConfigFilePath, "第二工位Robot工具示教点", "Y", GlobalVar.noGumTeachRobotTY.ToString("F3"));
                                ClassINI.INI.INIWriteValue(GlobalVar.bConfigFilePath, "第二工位Robot工具示教点", "U", GlobalVar.noGumTeachRobotTU.ToString("F3"));
                                GlobalVar.SendData($"3,2,1,0,0,0");
                            }
                            else
                            {
                                GlobalVar.noGumProTeachCenterX = GlobalVar.noGumProCenterX;
                                GlobalVar.noGumProTeachCenterY = GlobalVar.noGumProCenterY;
                                GlobalVar.noGumProTeachDeg = GlobalVar.noGumProDeg;
                                ClassINI.INI.INIWriteValue(GlobalVar.curProName == "Renault" ? GlobalVar.bRecipeRenaultFilePath : GlobalVar.bRecipeLadaFilePath, "第二工位示教点", "X", GlobalVar.noGumProTeachCenterX.ToString("F3"));
                                ClassINI.INI.INIWriteValue(GlobalVar.curProName == "Renault" ? GlobalVar.bRecipeRenaultFilePath : GlobalVar.bRecipeLadaFilePath, "第二工位示教点", "Y", GlobalVar.noGumProTeachCenterY.ToString("F3"));
                                ClassINI.INI.INIWriteValue(GlobalVar.curProName == "Renault" ? GlobalVar.bRecipeRenaultFilePath : GlobalVar.bRecipeLadaFilePath, "第二工位示教点", "角度", GlobalVar.noGumProTeachDeg.ToString("F3"));
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
                                double finX = compensaXVal + firstStaOffsetX + GlobalVar.noGumRobotOffsetX;
                                double finY = compensaYVal + firstStaOffsetY + GlobalVar.noGumRobotOffsetY;
                                double finDeg = GlobalVar.noGumTeachRobotTU - degOffset - firstStaOffsetDeg;
                                if (GlobalVar.noGumIsMatchNG)
                                {
                                    GlobalVar.firAndSecShotNGResetCount++;
                                    if (GlobalVar.firAndSecShotNGResetCount == 4)
                                    {
                                        //GlobalVar.SendData($"1,2,2,0,0,0");
                                        #region 修改成机器人暂停 ,后续量产后再视情况修改动作
                                        LogAlarmError("背胶拍照失败，机器人暂停中，请检查出标是否正常");
                                        //IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[5].Card - 1), GlobalVar.lsAxiasDOs[5].PinDefinition, (ushort)0);
                                        //Thread.Sleep(100);
                                        //IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[5].Card - 1), GlobalVar.lsAxiasDOs[5].PinDefinition, (ushort)1);
                                        GlobalVar.deviceAlarmIsHappen = true;
                                        #endregion 
                                        GlobalVar.firAndSecShotNGResetCount = 0;
                                        firstStaOffsetX = 0;
                                        firstStaOffsetY = 0;
                                        firstStaOffsetDeg = 0;
                                    }
                                    
                                    //if (GlobalVar.isAnalysisPic)
                                    //{
                                    //    string name = $"NG{ DateTime.Now.ToString("HHmmss")}";
                                    //    if (!picDicList.ContainsKey($"{GlobalVar.curWo}-背胶-{picAnalysisCnt}-{name}"))
                                    //    {
                                    //        picDicList.Add($"{GlobalVar.curWo}-背胶-{picAnalysisCnt}-{name}", InspectionClass.resultDisplay2);
                                    //    }
                                    //    else
                                    //    {
                                    //        picDicList[$"{GlobalVar.curWo}-背胶-{picAnalysisCnt}-{name}"] = InspectionClass.resultDisplay2;
                                    //    }
                                    //}
                                    IAsyncResult ia_TBImgSave = GraphicsImgSave.BeginInvoke(InspectionClass.resultDisplay2, @"背胶\NG\" + DateTime.Now.ToString("HH-mm-ss"), null, GraphicsImgSave);
                                    IAsyncResult ia_TBImgSave1 = ImgSave.BeginInvoke(InspectionClass.global1308GreyImage2, @"背胶\NG1\" + DateTime.Now.ToString("HH-mm-ss"), null, ImgSave);
                                    
                                }
                                else
                                {
                                    GlobalVar.firAndSecShotNGResetCount = 0;
                                    if (finX < 29.576 && finX > 9.576 && finY < 323 && finY > 303 && finDeg > 258 && finDeg < 278)
                                    {
                                        GlobalVar.SendData($"1,2,1,{finX},{finY},{finDeg}");
                                        LogMessage("机器人工具坐标系下，第二工位自己补偿X值" + compensaXVal);
                                        LogMessage("机器人工具坐标系下，第二工位自己补偿Y值" + compensaYVal);
                                        LogMessage("机器人工具坐标系下，第二工位自己补偿U值" + compensaDegVal);
                                        LogMessage("机器人工具坐标系下，第一二工位一起补偿X值" + finX);
                                        LogMessage("机器人工具坐标系下，第一二工位一起补偿Y值" + finY);
                                        LogMessage("机器人工具坐标系下，第一二工位一起补偿U值" + finDeg);
                                        Debug.WriteLine($"发送给机器人的值：{finX},{finY},{finDeg}");
                                        firstStaOffsetX = 0;
                                        firstStaOffsetY = 0;
                                        firstStaOffsetDeg = 0;
                                    }
                                    else
                                    {
                                        //LogError("机器人暂停中");
                                        LogAlarmError("贴背胶视觉补正超出机器人安全范围，机器人暂停运行,请停止设备运行");
                                        thisFrm.lalEpsonSocketStatus.Text = thisFrm.lalEpsonSocketStatus.Text + "超过机器人允许范围，机器人暂停运行";
                                        //IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[5].Card - 1), GlobalVar.lsAxiasDOs[5].PinDefinition, (ushort)0);
                                        //Thread.Sleep(100);
                                        //IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[5].Card - 1), GlobalVar.lsAxiasDOs[5].PinDefinition, (ushort)1);
                                        #region 将机器人暂停加入报警
                                        GlobalVar.deviceAlarmIsHappen = true;
                                        isRobotOverHappen = true;
                                        #endregion
                                    }
                                    if (GlobalVar.isAnalysisPic)
                                    {
                                        if (!picDicList.ContainsKey($"{GlobalVar.curWo}-背胶-{picAnalysisCnt}"))
                                        {
                                            picDicList.Add($"{GlobalVar.curWo}-背胶-{picAnalysisCnt}", InspectionClass.resultDisplay2);
                                        }
                                        else
                                        {
                                            picDicList[$"{GlobalVar.curWo}-背胶-{picAnalysisCnt}"] = InspectionClass.resultDisplay2;
                                        }
                                    }
                                    IAsyncResult ia_TBImgSave = GraphicsImgSave.BeginInvoke(InspectionClass.resultDisplay2, @"背胶\ok\" + DateTime.Now.ToString("HH-mm-ss"), null, GraphicsImgSave);
                                    IAsyncResult ia_TBImgSave1 = ImgSave.BeginInvoke(InspectionClass.global1308GreyImage2, @"背胶\ok1\" + DateTime.Now.ToString("HH-mm-ss"), null, ImgSave);

                                    //IAsyncResult ia_TBImgSave = GraphicsImgSave.BeginInvoke(InspectionClass.resultDisplay2, @"背胶\OK\" + DateTime.Now.ToString("HH-mm-ss"), null, ImgSave);
                                }
                                //正常测试产品X-R轴X


                                #endregion
                            }
                        }
                        ControlLightOn(2, true);
                    }
                    break;
                case 2:
                    {
                        if (isTeach)
                        {
                            GlobalVar.labeledLogoProTeachCenterX = GlobalVar.labeledLogoProCenterX;
                            GlobalVar.labeledLogoProTeachCenterY = GlobalVar.labeledLogoProCenterY;
                            GlobalVar.labeledLogoProTeachDeg = GlobalVar.labeledLogoProDeg;
                            ClassINI.INI.INIWriteValue(GlobalVar.curProName == "Renault" ? GlobalVar.bRecipeRenaultFilePath : GlobalVar.bRecipeLadaFilePath, "第三工位示教点", "X", GlobalVar.labeledLogoProTeachCenterX.ToString("F3"));
                            ClassINI.INI.INIWriteValue(GlobalVar.curProName == "Renault" ? GlobalVar.bRecipeRenaultFilePath : GlobalVar.bRecipeLadaFilePath, "第三工位示教点", "Y", GlobalVar.labeledLogoProTeachCenterY.ToString("F3"));
                            ClassINI.INI.INIWriteValue(GlobalVar.curProName == "Renault" ? GlobalVar.bRecipeRenaultFilePath : GlobalVar.bRecipeLadaFilePath, "第三工位示教点", "角度", GlobalVar.labeledLogoProTeachDeg.ToString("F3"));
                        }
                        else
                        {
                            if (!GlobalVar.isCam3CalOrTest)
                            {
                                GlobalVar.firAndSecShotNGResetCount = 0;
                                if (GlobalVar.labeledLogoIsMatchNG)
                                {
                                    GlobalVar.SendData($"1,3,2,0,0,0");
                                    //IAsyncResult ia_TBImgSave = GraphicsImgSave.BeginInvoke(InspectionClass.resultDisplay3, @"已贴背胶电镀件\NG\" + DateTime.Now.ToString("HH-mm-ss"), null, ImgSave);
                                    IAsyncResult ia_TBImgSave = GraphicsImgSave.BeginInvoke(InspectionClass.resultDisplay3, @"已贴背胶电镀件\NG\" + DateTime.Now.ToString("HH-mm-ss"), null, GraphicsImgSave);
                                    IAsyncResult ia_TBImgSave1 = ImgSave.BeginInvoke(InspectionClass.global1308GreyImage3, @"已贴背胶电镀件\NG1\" + DateTime.Now.ToString("HH-mm-ss"), null, ImgSave);

                                    //if (GlobalVar.isAnalysisPic)
                                    //{
                                    //    string name = $"NG{ DateTime.Now.ToString("HHmmss")}";
                                    //    if (!picDicList.ContainsKey($"{GlobalVar.curWo}-已贴背胶电镀件-{picAnalysisCnt}-{name}"))
                                    //    {
                                    //        picDicList.Add($"{GlobalVar.curWo}-已贴背胶电镀件-{picAnalysisCnt}-{name}", InspectionClass.resultDisplay3);
                                    //    }
                                    //    else
                                    //    {
                                    //        picDicList[$"{GlobalVar.curWo}-已贴背胶电镀件-{picAnalysisCnt}-{name}"] = InspectionClass.resultDisplay3;
                                    //    }
                                    //}
                                }
                                else
                                {
                                    finThrDeg = GlobalVar.labeledLogoProDeg - GlobalVar.labeledLogoProTeachDeg;
                                    finThrX = (GlobalVar.labeledLogoProCenterX - GlobalVar.labeledLogoProTeachCenterX) / GlobalVar.labeledLogoRatioX;
                                    finThrY = (GlobalVar.labeledLogoProCenterY - GlobalVar.labeledLogoProTeachCenterY) / GlobalVar.labeledLogoRatioY;
                                    Debug.WriteLine($"第三工位当前的中心值：{GlobalVar.labeledLogoProCenterX/ GlobalVar.labeledLogoRatioX},{GlobalVar.labeledLogoProCenterY/ GlobalVar.labeledLogoRatioY},{GlobalVar.labeledLogoProDeg}");
                                    Debug.WriteLine($"第三工位示教点值：{GlobalVar.labeledLogoProTeachCenterX / GlobalVar.labeledLogoRatioX},{GlobalVar.labeledLogoProTeachCenterY / GlobalVar.labeledLogoRatioY},{GlobalVar.labeledLogoProTeachDeg}");
                                    GlobalVar.SendData($"1,3,1,0,0,0");
                                    if (GlobalVar.isAnalysisPic)
                                    {
                                        if (!picDicList.ContainsKey($"{GlobalVar.curWo}-已贴背胶电镀件-{picAnalysisCnt}"))
                                        {
                                            picDicList.Add($"{GlobalVar.curWo}-已贴背胶电镀件-{picAnalysisCnt}", InspectionClass.resultDisplay3);
                                        }
                                        else
                                        {
                                            picDicList[$"{GlobalVar.curWo}-已贴背胶电镀件-{picAnalysisCnt}"] = InspectionClass.resultDisplay3;
                                        }
                                    }
                                    IAsyncResult ia_TBImgSave = GraphicsImgSave.BeginInvoke(InspectionClass.resultDisplay3, @"已贴背胶电镀件\ok\" + DateTime.Now.ToString("HH-mm-ss"), null, GraphicsImgSave);
                                    IAsyncResult ia_TBImgSave1 = ImgSave.BeginInvoke(InspectionClass.global1308GreyImage3, @"已贴背胶电镀件\ok1\" + DateTime.Now.ToString("HH-mm-ss"), null, ImgSave);
                                    //IAsyncResult ia_TBImgSave = GraphicsImgSave.BeginInvoke(InspectionClass.resultDisplay3, @"已贴背胶电镀件\OK\" + DateTime.Now.ToString("HH-mm-ss"), null, ImgSave);
                                }
                                #region 验证一次旋转补正算法
                                ControlLightOn(3, true);


                                #endregion
                            }
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
                                ClassINI.INI.INIWriteValue(GlobalVar.bConfigFilePath, "第四工位Robot工具示教点", "X", GlobalVar.gumedTeachRobotTX.ToString("F3"));
                                ClassINI.INI.INIWriteValue(GlobalVar.bConfigFilePath, "第四工位Robot工具示教点", "Y", GlobalVar.gumedTeachRobotTY.ToString("F3"));
                                ClassINI.INI.INIWriteValue(GlobalVar.bConfigFilePath, "第四工位Robot工具示教点", "U", GlobalVar.gumedTeachRobotTU.ToString("F3"));
                                GlobalVar.SendData($"3,4,1,0,0,0");
                            }
                            else
                            {
                                GlobalVar.gumedProTeachCenterX = GlobalVar.gumedProCenterX;
                                GlobalVar.gumedProTeachCenterY = GlobalVar.gumedProCenterY;
                                GlobalVar.gumedProTeachDeg = GlobalVar.gumedProDeg;
                                ClassINI.INI.INIWriteValue(GlobalVar.curProName == "Renault" ? GlobalVar.bRecipeRenaultFilePath : GlobalVar.bRecipeLadaFilePath, "第四工位示教点", "X", GlobalVar.gumedProTeachCenterX.ToString("F3"));
                                ClassINI.INI.INIWriteValue(GlobalVar.curProName == "Renault" ? GlobalVar.bRecipeRenaultFilePath : GlobalVar.bRecipeLadaFilePath, "第四工位示教点", "Y", GlobalVar.gumedProTeachCenterY.ToString("F3"));
                                ClassINI.INI.INIWriteValue(GlobalVar.curProName == "Renault" ? GlobalVar.bRecipeRenaultFilePath : GlobalVar.bRecipeLadaFilePath, "第四工位示教点", "角度", GlobalVar.gumedProTeachDeg.ToString("F3"));
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
                                Debug.WriteLine($"第四工位当前的中心值：{GlobalVar.gumedProCenterX / GlobalVar.gumedRatioX},{GlobalVar.gumedProCenterY / GlobalVar.gumedRatioY},{GlobalVar.gumedProDeg}");
                                Debug.WriteLine($"第四工位示教点值：{GlobalVar.gumedProTeachCenterX / GlobalVar.gumedRatioX},{GlobalVar.gumedProTeachCenterY / GlobalVar.gumedRatioY},{GlobalVar.gumedProTeachDeg}");
                                double finX=0;
                                double finY=0;
                                finX = GlobalVar.gumedTeachRobotTX + offsetX + finThrX - noLabelLogoProOffsetY;
                                finY = GlobalVar.gumedTeachRobotTY - offsetY + finThrY + noLabelLogoProOffsetX;
                                //finX = GlobalVar.gumedTeachRobotTX + offsetX + finThrX;
                                //finY = GlobalVar.gumedTeachRobotTY - offsetY + finThrY;
                                double finDeg = GlobalVar.gumedTeachRobotTU - degOffset - finThrDeg;
                                Debug.WriteLine($"第三和第四次的综合偏差值：{finX},{finY},{finDeg},");
                                isLabelActionFinished = true;
                                if (GlobalVar.gumedIsMatchNG)//
                                {
                                    //GlobalVar.SendData($"1,4,2,0,0,0");
                                    #region 修改成机器人暂停 ,后续量产后再视情况修改动作
                                    LogAlarmError("注塑件拍照失败，机器人暂停中，请检查出标是否正常");
                                    //IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[5].Card - 1), GlobalVar.lsAxiasDOs[5].PinDefinition, (ushort)0);
                                    //Thread.Sleep(100);
                                    //IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[5].Card - 1), GlobalVar.lsAxiasDOs[5].PinDefinition, (ushort)1);

                                    GlobalVar.deviceAlarmIsHappen = true;
                                    #endregion
                                    //IAsyncResult ia_TBImgSave = GraphicsImgSave.BeginInvoke(InspectionClass.resultDisplay4, @"注塑件\NG\" + DateTime.Now.ToString("HH-mm-ss"), null, ImgSave);
                                    IAsyncResult ia_TBImgSave = GraphicsImgSave.BeginInvoke(InspectionClass.resultDisplay4, @"注塑件\NG\" + DateTime.Now.ToString("HH-mm-ss"), null, GraphicsImgSave);
                                    IAsyncResult ia_TBImgSave1 = ImgSave.BeginInvoke(InspectionClass.global1308GreyImage4, @"注塑件\NG1\" + DateTime.Now.ToString("HH-mm-ss"), null, ImgSave);
                                    //if (GlobalVar.isAnalysisPic)
                                    //{
                                    //    string name = $"NG{ DateTime.Now.ToString("HHmmss")}";
                                    //    if (!picDicList.ContainsKey($"{GlobalVar.curWo}-注塑件-{picAnalysisCnt}-{name}"))
                                    //    {
                                    //        picDicList.Add($"{GlobalVar.curWo}-注塑件-{picAnalysisCnt}-{name}", InspectionClass.resultDisplay4);
                                    //    }
                                    //    else
                                    //    {
                                    //        picDicList[$"{GlobalVar.curWo}-注塑件-{picAnalysisCnt}-{name}"] = InspectionClass.resultDisplay4;
                                    //    }
                                    //    picAoiAnaylistCnt.Add(picAnalysisCnt++);
                                    //}
                                }
                                else
                                {
                                    if (finX < -192 && finX > -212 && finY < 286 && finY > 265 && finDeg > 381 && finDeg < 411)
                                    {
                                        if (noLabelLogoProOffsetX > 0)
                                        {
                                            GlobalVar.SendData($"1,4,1,{finX + GlobalVar.gumedRobotOffsetX-0.1},{finY + GlobalVar.gumedRobotOffsetY-0.01},{finDeg}");
                                        }
                                        else
                                        {
                                            GlobalVar.SendData($"1,4,1,{finX + GlobalVar.gumedRobotOffsetX},{finY + +GlobalVar.gumedRobotOffsetY},{finDeg}");
                                        }
                                        //GlobalVar.SendData($"1,4,1,{finX + GlobalVar.gumedRobotOffsetX},{finY + GlobalVar.gumedRobotOffsetY},{finDeg}");
                                        Debug.WriteLine($"RobotX补偿值：{GlobalVar.gumedRobotOffsetX}--RobotY补偿值：{GlobalVar.gumedRobotOffsetY}");
                                        Debug.WriteLine($"1,4,1,{finX + GlobalVar.gumedRobotOffsetX},{finY + GlobalVar.gumedRobotOffsetY},{finDeg}");
                                        noLabelLogoProOffsetX = 0;
                                        noLabelLogoProOffsetY = 0;
                                        finThrX = 0;
                                        finThrY = 0;
                                        finThrDeg = 0;
                                    }
                                    else
                                    {
                                        #region 将机器人暂停加入报警
                                        LogAlarmError("贴LOGO视觉补正超出机器人安全范围，机器人暂停运行,请停止设备运行");
                                        GlobalVar.deviceAlarmIsHappen = true;
                                        isRobotOverHappen = true;
                                        #endregion
                                        thisFrm.lalEpsonSocketStatus.Text = thisFrm.lalEpsonSocketStatus.Text + "超过机器人允许范围，机器人暂停运行";
                                        //IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[5].Card - 1), GlobalVar.lsAxiasDOs[5].PinDefinition, (ushort)0);
                                        //Thread.Sleep(100);
                                        //IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[5].Card - 1), GlobalVar.lsAxiasDOs[5].PinDefinition, (ushort)1);
                                    }
                                    if (GlobalVar.isAnalysisPic)
                                    {
                                        if (!picDicList.ContainsKey($"{GlobalVar.curWo}-注塑件-{picAnalysisCnt}"))
                                        {
                                            picDicList.Add($"{GlobalVar.curWo}-注塑件-{picAnalysisCnt}", InspectionClass.resultDisplay4);
                                        }
                                        else
                                        {
                                            picDicList[$"{GlobalVar.curWo}-注塑件-{picAnalysisCnt}"] = InspectionClass.resultDisplay4;
                                        }
                                        picAoiAnaylistCnt.Add(picAnalysisCnt++);
                                    }
                                    IAsyncResult ia_TBImgSave = GraphicsImgSave.BeginInvoke(InspectionClass.resultDisplay4, @"注塑件\ok\" + DateTime.Now.ToString("HH-mm-ss"), null, GraphicsImgSave);
                                    IAsyncResult ia_TBImgSave1 = ImgSave.BeginInvoke(InspectionClass.global1308GreyImage4, @"注塑件\ok1\" + DateTime.Now.ToString("HH-mm-ss"), null, ImgSave);
                                    //IAsyncResult ia_TBImgSave = GraphicsImgSave.BeginInvoke(InspectionClass.resultDisplay4, @"注塑件\OK\" + DateTime.Now.ToString("HH-mm-ss"), null, ImgSave);
                                }
                                ControlLightOn(0, true);

                                //正常测试产品X-R轴X
                                //LogMessage("机器人工具坐标系下，第三四工位一起补偿X值" + finX);
                                //LogMessage("机器人工具坐标系下，第三四工位一起补偿Y值" + finY);
                                //LogMessage("机器人工具坐标系下，第三四工位一起补偿U值" + finDeg);
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
                    case "FiveCalCount":
                        switch (station)
                        {
                            //case 0:
                            //    InspectionClass.tb1.Inputs[myParams].Value = GlobalVar.noLabelLogoFiveCnt;
                            //    break;
                            //case 1:
                            //    //InspectionClass.tb2.Inputs[myParams].Value = GlobalVar.noGumFiveCnt;
                            //    break;
                            //case 2:
                            //    InspectionClass.tb2.Inputs[myParams].Value = GlobalVar.isCam2CalOrTest;
                            //    break;
                            //case 3:
                            //    InspectionClass.tb2.Inputs[myParams].Value = GlobalVar.isCam2CalOrTest;
                            //break;
                        }
                        break;
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
                GlobalVar.myLog.Error("错误信息是： " + e.Message);
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
                    case "ProMCenterX":  //产品中心X坐标
                        { noLabelLogoProMCenterX = Convert.ToDouble(InspectionClass.tb1.Outputs[myParams].Value); }
                        break;
                    case "ProMCenterY":  //产品中心X坐标
                        { noLabelLogoProMCenterY = Convert.ToDouble(InspectionClass.tb1.Outputs[myParams].Value); }
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
                GlobalVar.myLog.Error("错误信息是： " + e.Message);
                return;
            }
        }

        /// <summary>
        /// 加载配置到toolblock
        /// </summary>
        /// <param name="station">工位</param>
        /// <param name="myParams">需要填充到TB的数据名称</param>
        private void LoadAoiSettingToTBS()
        {
            try
            {
                InspectionClass.tb5.Inputs["CurProName"].Value = GlobalVar.curProName;
                InspectionClass.tb5.Inputs["XVCentDisMax"].Value = GlobalVar.aoiProCenterXOffsetVmax;
                InspectionClass.tb5.Inputs["XVCentDisMin"].Value = GlobalVar.aoiProCenterXOffsetVmin;
                InspectionClass.tb5.Inputs["YVCentDisMax"].Value = GlobalVar.aoiProCenterYOffsetVmax;
                InspectionClass.tb5.Inputs["YVCentDisMin"].Value = GlobalVar.aoiProCenterYOffsetVmin;
                InspectionClass.tb5.Inputs["AngleRangeMax"].Value = GlobalVar.aoiProDegOffsetVmax;
                InspectionClass.tb5.Inputs["AngleRangeMin"].Value = GlobalVar.aoiProDegOffsetVmin;
                InspectionClass.tb5.Inputs["XVCentDisOffset"].Value = GlobalVar.aoiProCenterOffsetX;
                InspectionClass.tb5.Inputs["YVCentDisOffset"].Value = GlobalVar.aoiProCenterOffsetY;
                InspectionClass.tb5.Inputs["AngleRangeOffset"].Value = GlobalVar.aoiProCenterOffsetDeg;
            }
            catch (Exception e)
            {
                GlobalVar.myLog.Error("错误信息是： " + e.Message);
                return;
            }
        }

        /// <summary>
        /// 从toolblock获取数据
        /// </summary>
        /// <param name="station">工位</param>
        /// <param name="myParams">TB的数据名称</param>
        private void GetFromAoiTBDataS()
        {
            try
            {
                GlobalVar.aoiDatas.Add(new AoiData()
                {
                    XVCentDis = InspectionClass.tb5.Outputs["XVCentDisVal"].Value.ToString(),
                    YVCentDis = InspectionClass.tb5.Outputs["YVCentDisVal"].Value.ToString(),
                    Deg = InspectionClass.tb5.Outputs["AngleRangeVal"].Value.ToString(),
                    AOIRes = Convert.ToBoolean(InspectionClass.tb5.Outputs["finalRes"].Value) ? "PASS" : "FAIL",
                    AOITestDt = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                });
            }
            catch (Exception e)
            {
                GlobalVar.myLog.Error("错误信息是： " + e.Message);
                return;
            }
        }

        private void AOIFuncDo()
        {
            try
            {
                thisFrm.Invoke(new Action(() =>
                {
                    isAOIShotFinished = true;
                    //thisFrm.ControlLightOn(4, true);
                    IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[32].Card - 1), GlobalVar.lsAxiasDOs[32].PinDefinition, (ushort)1);
                    Thread.Sleep(500);
                    InspectionClass.tb5.Run();
                    thisFrm.GetFromAoiTBDataS();
                    //thisFrm.ControlLightOn(4, false);
                    IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[32].Card - 1), GlobalVar.lsAxiasDOs[32].PinDefinition, (ushort)0);
                    bool aoiThisRes = GlobalVar.aoiDatas[0].AOIRes == "PASS";
                    string aoiResPath = aoiThisRes ? "OK" : "NG";
                    JudgeTakePose();
                    IAsyncResult ia_TBImgSave = GraphicsImgSave.BeginInvoke(InspectionClass.resultDisplayAOI, @"AOI\" + aoiResPath + @"\" + aoiFinalBarcode, null, ImgSave);
                    if (GlobalVar.isAnalysisPic)
                    {
                        if (picAoiAnaylistCnt.Count > 0)
                        {
                            if (!picDicList.ContainsKey($"{GlobalVar.curWo}-AOI-{picAoiAnaylistCnt[0]}"))
                            {
                                picDicList.Add($"{GlobalVar.curWo}-AOI-{picAoiAnaylistCnt[0]}", InspectionClass.resultDisplayAOI);
                                picAoiAnaylistCnt.RemoveAt(0);
                            }
                        }
                    }
                    aoiFinalBarcode = "NoBarcode";
                }));
            }
            catch (Exception ex)
            {
                GlobalVar.myLog.Error("执行方法AOIFuncDo()出现未知错误:" + ex.Message);
            }
        }

        #endregion

        #region 光源亮暗控制
        private void ControlLightOn(int station, bool onOroff)
        {
            using (SerialPort sp = new SerialPort(GlobalVar.lightContrCom))
            {
                if (!sp.IsOpen)
                {
                    sp.Open();
                }
                switch (station)
                {
                    case 0:
                        {
                            sp.Write(onOroff ? GlobalVar.lightNoGumLogoCH1OpenCmd : GlobalVar.lightNoGumLogoCH1CloseCmd);
                        }
                        break;
                    case 1:
                        {
                            sp.Write(onOroff ? GlobalVar.lightGumedCH2OpenCmd : GlobalVar.lightGumedCH2CloseCmd);
                        }
                        break;
                    case 2:
                        {
                            sp.Write(onOroff ? GlobalVar.lightLabeledGumCH1OpenCmd : GlobalVar.lightLabeledGumCH1CloseCmd);
                        }
                        break;
                    case 3:
                        {
                            sp.Write(onOroff ? GlobalVar.lightLabeledLogoCH2OpenCmd : GlobalVar.lightLabeledLogoCH2CloseCmd);
                        }
                        break;
                    case 4:
                        {
                            sp.Write(onOroff ? GlobalVar.lightCH3OpenCmd : GlobalVar.lightCH3CloseCmd);
                        }
                        break;

                }
            }
        }
        #endregion

        #region 主线程
        private static void MainTotalRunThread(object state)
        {
            while (GlobalVar.totalRunFlagVision)
            {
                try
                {
                    thisFrm.Invoke(new Action(() => { thisFrm.timeClick.Text = DateTime.Now.ToString("HH:mm:ss"); }));
                    if (GlobalVar.lsAxiasDIs[9].CurIOStatus && !isStart)//设备启动按钮
                    {
                        if (!isFirstGoHome)
                        {
                            thisFrm.LogError("程序刚启动请先做回原点动作");
                            continue;
                        }
                        if (GlobalVar.lsAxiasDIs[30].CurIOStatus || GlobalVar.lsAxiasDIs[31].CurIOStatus || GlobalVar.lsAxiasDIs[32].CurIOStatus || GlobalVar.lsAxiasDIs[33].CurIOStatus || GlobalVar.lsAxiasDIs[34].CurIOStatus || GlobalVar.lsAxiasDIs[35].CurIOStatus)
                        {
                            //thisFrm.LogError("请先点击'生产收尾按钮'");
                            thisFrm.LogError("检测到轮盘有料感信号,请手动将轮盘上的产品收拾干净...");
                            continue;
                        }
                        //设备启动，线判断工单是否录入
                        thisFrm.Invoke(new Action(() =>
                        {
                            if (thisFrm.JudgeTextBoxIsNullOrSpace(thisFrm.txtBoxSN)) { return; }
                            if (thisFrm.JudgeTextBoxIsNullOrSpace(thisFrm.txtBoxWO)) { return; }
                            if (thisFrm.JudgeComboBoxIsNullOrSpace(thisFrm.cmbClass)) { return; }
                            //机器人开始
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[0].Card - 1), GlobalVar.lsAxiasDOs[0].PinDefinition, (ushort)0);
                            Thread.Sleep(50);
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[0].Card - 1), GlobalVar.lsAxiasDOs[0].PinDefinition, (ushort)1);
                            //改成IO更加安全可靠
                            while (true)//机器人通讯是否联通
                            {
                                if (GlobalVar.isEpsonConnected || GlobalVar.lsAxiasDIs[45].CurIOStatus || GlobalVar.lsAxiasDIs[10].CurIOStatus || !GlobalVar.lsAxiasDIs[44].CurIOStatus)
                                {
                                    break;
                                }
                                Application.DoEvents();
                            }
                            if (!GlobalVar.lsAxiasDIs[10].CurIOStatus && GlobalVar.lsAxiasDIs[44].CurIOStatus)//设备停止或者急停
                            {
                                //判断轮盘上气缸状态
                                if (GlobalVar.lsAxiasDIs[20].CurIOStatus && !GlobalVar.lsAxiasDIs[21].CurIOStatus && GlobalVar.lsAxiasDIs[22].CurIOStatus && !GlobalVar.lsAxiasDIs[23].CurIOStatus && GlobalVar.lsAxiasDIs[14].CurIOStatus && !GlobalVar.lsAxiasDIs[15].CurIOStatus && GlobalVar.lsAxiasDIs[24].CurIOStatus && !GlobalVar.lsAxiasDIs[25].CurIOStatus && GlobalVar.lsAxiasDIs[26].CurIOStatus && !GlobalVar.lsAxiasDIs[27].CurIOStatus)
                                {
                                    IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[29].Card - 1), GlobalVar.lsAxiasDOs[29].PinDefinition, 0); //凸轮先旋转一圈
                                }
                                else
                                {
                                    IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[4].Card - 1), GlobalVar.lsAxiasDOs[4].PinDefinition, (ushort)0);
                                    Thread.Sleep(10);
                                    IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[4].Card - 1), GlobalVar.lsAxiasDOs[4].PinDefinition, (ushort)1);
                                    MessageBox.Show("轮盘上有气缸下降状态");
                                    return;
                                }
                                isStart = true;
                                GlobalVar.totalRunFlag = true;
                                //红灯
                                IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[9].Card - 1), GlobalVar.lsAxiasDOs[9].PinDefinition, (ushort)1);
                                //蜂鸣
                                IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[12].Card - 1), GlobalVar.lsAxiasDOs[12].PinDefinition, (ushort)1);
                                //绿灯
                                IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[11].Card - 1), GlobalVar.lsAxiasDOs[11].PinDefinition, (ushort)0);
                                //黄灯
                                IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[10].Card - 1), GlobalVar.lsAxiasDOs[10].PinDefinition, (ushort)1);
                                isStartBtnResponse = true;
                                isStopBtnResponse = false;
                            }
                        }));
                    }
                    if (GlobalVar.lsAxiasDIs[9].CurIOStatus)
                    {
                        if (isStart)
                        {
                            thisFrm.LogError("设备已经处于全自动运行状态");
                        }
                    }
                    //点击停止按钮动作
                    if (GlobalVar.lsAxiasDIs[10].CurIOStatus)//设备停止
                    {
                        if (MessageBox.Show("请确认是否需要先点击“生产收尾”按钮", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            continue;
                        }
                        if (GlobalVar.lsAxiasDIs[30].CurIOStatus || GlobalVar.lsAxiasDIs[31].CurIOStatus || GlobalVar.lsAxiasDIs[32].CurIOStatus || GlobalVar.lsAxiasDIs[33].CurIOStatus || GlobalVar.lsAxiasDIs[34].CurIOStatus || GlobalVar.lsAxiasDIs[35].CurIOStatus)
                        {
                            //数据清空
                            picAnalysisCnt = 0;
                            picAoiAnaylistCnt.Clear();
                            picDicList.Clear();
                            GlobalVar.aoiDatas.Clear();
                            GlobalVar.highDatas.Clear();
                            GlobalVar.finalHighResAndAoiRes.Clear();
                            finalTakeHighResAndAoiRes.Clear();
                        }
                        isStopBtnResponse = true;
                        isStartBtnResponse = false;
                        isLabelOut = false;
                        GlobalVar.totalRunFlag = false;
                        GlobalVar.deviceSafeDoorIsOpen = false;
                        GlobalVar.isEpsonConnected = false;
                        GlobalVar.isFirAndSecIsShotOK = false;
                        thisFrm.isRotatePorN = true;
                        isCurStepAction = false;
                        firStationIsReadyOk = false;
                        secStationIsReadyOk = false;
                        thrStationIsReadyOk = false;
                        fourStationIsReadyOk = false;
                        sixStationisReadyOk = false;
                        isCurSecStepAction = false;
                        isCurThrStepAction = false;
                        isCurFourStepAction = false;
                        isCurSixStepAction = false;
                        isOtherStationAllowCylinderDown = false;
                        isRotateCylinder = false;
                        isRobotOverHappen = false;
                        isInjectSignalIsOnOver = false;
                        //停止凸轮旋转
                        IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[29].Card - 1), GlobalVar.lsAxiasDOs[29].PinDefinition, (ushort)1);
                        LTDMC.dmc_stop(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, 0);
                        LTDMC.dmc_stop(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, 0);
                        LTDMC.dmc_stop(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, 0);
                        LTDMC.dmc_stop(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, 0);
                        LTDMC.dmc_stop(GlobalVar.CardId, GlobalVar.LabelingAxiasNumber, 0);
                        //红灯
                        IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[9].Card - 1), GlobalVar.lsAxiasDOs[9].PinDefinition, (ushort)1);
                        //蜂鸣
                        IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[12].Card - 1), GlobalVar.lsAxiasDOs[12].PinDefinition, (ushort)1);
                        //绿灯
                        IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[11].Card - 1), GlobalVar.lsAxiasDOs[11].PinDefinition, (ushort)1);
                        //黄灯
                        IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[10].Card - 1), GlobalVar.lsAxiasDOs[10].PinDefinition, (ushort)0);

                        //停止机器人动作
                        IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[4].Card - 1), GlobalVar.lsAxiasDOs[4].PinDefinition, (ushort)0);
                        Thread.Sleep(10);
                        IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[4].Card - 1), GlobalVar.lsAxiasDOs[4].PinDefinition, (ushort)1);
                        //复位各自气缸
                        IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[15].Card - 1), GlobalVar.lsAxiasDOs[15].PinDefinition, (ushort)1);//旋转
                        IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[16].Card - 1), GlobalVar.lsAxiasDOs[16].PinDefinition, (ushort)1);//上料
                        IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[17].Card - 1), GlobalVar.lsAxiasDOs[17].PinDefinition, (ushort)1);//上料真空
                        IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[18].Card - 1), GlobalVar.lsAxiasDOs[18].PinDefinition, (ushort)1);//出标轴下压
                        IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[19].Card - 1), GlobalVar.lsAxiasDOs[19].PinDefinition, (ushort)1);//出标轴出标
                        IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[20].Card - 1), GlobalVar.lsAxiasDOs[20].PinDefinition, (ushort)1);//压合气缸
                        IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[21].Card - 1), GlobalVar.lsAxiasDOs[21].PinDefinition, (ushort)1);//测高
                        IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[22].Card - 1), GlobalVar.lsAxiasDOs[22].PinDefinition, (ushort)1);//下料
                        IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[23].Card - 1), GlobalVar.lsAxiasDOs[23].PinDefinition, (ushort)1);//下料真空
                        IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[24].Card - 1), GlobalVar.lsAxiasDOs[24].PinDefinition, (ushort)1);//贴logo按压气缸
                        IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[25].Card - 1), GlobalVar.lsAxiasDOs[25].PinDefinition, (ushort)1);//允许取LOGO
                        IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[26].Card - 1), GlobalVar.lsAxiasDOs[26].PinDefinition, (ushort)1);//允许贴背胶
                        IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[27].Card - 1), GlobalVar.lsAxiasDOs[27].PinDefinition, (ushort)1);//通知回退
                        IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[28].Card - 1), GlobalVar.lsAxiasDOs[28].PinDefinition, (ushort)1);//允许贴标
                        Console.WriteLine("test3");
                        IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[30].Card - 1), GlobalVar.lsAxiasDOs[30].PinDefinition, (ushort)1);//安全门
                        IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[8].Card - 1), GlobalVar.lsAxiasDOs[8].PinDefinition, (ushort)1);//给注塑机放疗信号

                        //复位各自步骤
                        Thread.Sleep(10);
                        thisFrm.firStep = EnumFirstStation.MoveToInjectTakeProPos;
                        thisFrm.secStep = EnumSecStation.SenseAllSensor;
                        thisFrm.thrStep = EnumThrStation.SenseAllSensor;
                        thisFrm.fourStep = EnumFourStation.SenseAllSensor;
                        thisFrm.sixStep = EnumSixStation.MoveToFeedPos;
                        isFirStaStepReset = false;
                        isInjectSignalArrived = false;
                        isFeedVaccIsAlarm = false;
                        isStart = false;
                    }
                    if (!GlobalVar.lsAxiasDIs[44].CurIOStatus)//急停
                    {
                        if (!isEStopIsOn)
                        {
                            isEStopIsOn = true;
                            isStopBtnResponse = true;
                            isStartBtnResponse = false;
                            isLabelOut = false;
                            GlobalVar.totalRunFlag = false;
                            GlobalVar.deviceSafeDoorIsOpen = false;
                            GlobalVar.isEpsonConnected = false;
                            GlobalVar.isFirAndSecIsShotOK = false;
                            thisFrm.isRotatePorN = true;
                            isCurStepAction = false;
                            firStationIsReadyOk = false;
                            secStationIsReadyOk = false;
                            thrStationIsReadyOk = false;
                            fourStationIsReadyOk = false;
                            sixStationisReadyOk = false;
                            isCurSecStepAction = false;
                            isCurThrStepAction = false;
                            isCurFourStepAction = false;
                            isCurSixStepAction = false;
                            isOtherStationAllowCylinderDown = false;
                            isRotateCylinder = false;
                            //停止凸轮旋转
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[29].Card - 1), GlobalVar.lsAxiasDOs[29].PinDefinition, (ushort)1);
                            LTDMC.dmc_stop(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, 0);
                            LTDMC.dmc_stop(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, 0);
                            LTDMC.dmc_stop(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, 0);
                            LTDMC.dmc_stop(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, 0);
                            LTDMC.dmc_stop(GlobalVar.CardId, GlobalVar.LabelingAxiasNumber, 0);
                            //红灯
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[9].Card - 1), GlobalVar.lsAxiasDOs[9].PinDefinition, (ushort)1);
                            //蜂鸣
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[12].Card - 1), GlobalVar.lsAxiasDOs[12].PinDefinition, (ushort)1);
                            //绿灯
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[11].Card - 1), GlobalVar.lsAxiasDOs[11].PinDefinition, (ushort)1);
                            //黄灯
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[10].Card - 1), GlobalVar.lsAxiasDOs[10].PinDefinition, (ushort)0);

                            //停止机器人动作
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[4].Card - 1), GlobalVar.lsAxiasDOs[4].PinDefinition, (ushort)0);
                            Thread.Sleep(10);
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[4].Card - 1), GlobalVar.lsAxiasDOs[4].PinDefinition, (ushort)1);
                            //复位各自气缸
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[15].Card - 1), GlobalVar.lsAxiasDOs[15].PinDefinition, (ushort)1);//旋转
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[16].Card - 1), GlobalVar.lsAxiasDOs[16].PinDefinition, (ushort)1);//上料
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[17].Card - 1), GlobalVar.lsAxiasDOs[17].PinDefinition, (ushort)1);//上料真空
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[18].Card - 1), GlobalVar.lsAxiasDOs[18].PinDefinition, (ushort)1);//出标轴下压
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[19].Card - 1), GlobalVar.lsAxiasDOs[19].PinDefinition, (ushort)1);//出标轴出标
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[20].Card - 1), GlobalVar.lsAxiasDOs[20].PinDefinition, (ushort)1);//压合气缸
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[21].Card - 1), GlobalVar.lsAxiasDOs[21].PinDefinition, (ushort)1);//测高
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[22].Card - 1), GlobalVar.lsAxiasDOs[22].PinDefinition, (ushort)1);//下料
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[23].Card - 1), GlobalVar.lsAxiasDOs[23].PinDefinition, (ushort)1);//下料真空
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[24].Card - 1), GlobalVar.lsAxiasDOs[24].PinDefinition, (ushort)1);//贴logo按压气缸
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[25].Card - 1), GlobalVar.lsAxiasDOs[25].PinDefinition, (ushort)1);//允许取LOGO
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[26].Card - 1), GlobalVar.lsAxiasDOs[26].PinDefinition, (ushort)1);//允许贴背胶
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[27].Card - 1), GlobalVar.lsAxiasDOs[27].PinDefinition, (ushort)1);//通知回退
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[28].Card - 1), GlobalVar.lsAxiasDOs[28].PinDefinition, (ushort)1);//允许贴标
                            Console.WriteLine("test4");
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[30].Card - 1), GlobalVar.lsAxiasDOs[30].PinDefinition, (ushort)1);//安全门
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[8].Card - 1), GlobalVar.lsAxiasDOs[8].PinDefinition, (ushort)1);//给注塑机放疗信号

                            //复位各自步骤
                            Thread.Sleep(10);
                            thisFrm.firStep = EnumFirstStation.MoveToInjectTakeProPos;
                            thisFrm.secStep = EnumSecStation.SenseAllSensor;
                            thisFrm.thrStep = EnumThrStation.SenseAllSensor;
                            thisFrm.fourStep = EnumFourStation.SenseAllSensor;
                            thisFrm.sixStep = EnumSixStation.MoveToFeedPos;
                            isFirStaStepReset = false;
                            isInjectSignalArrived = false;
                            isFeedVaccIsAlarm = false;
                            isRobotOverHappen = false;
                            isInjectSignalIsOnOver = false;
                            //MessageBox.Show("设备急停打开中");

                        }
                    }
                    if (GlobalVar.lsAxiasDIs[44].CurIOStatus)//
                    {
                        if (isEStopIsOn)
                        {
                            isEStopIsOn = false;
                            thisFrm.LogError("急停已解除");
                        }
                    }
                    //查询机器人状态
                    thisFrm.Invoke(new Action(() =>
                    {
                        if (GlobalVar.lsAxiasDIs[0].CurIOStatus)
                        {
                            thisFrm.lalEpsonStatus.Text = "待机中";
                        }
                        if (GlobalVar.lsAxiasDIs[1].CurIOStatus)
                        {
                            thisFrm.lalEpsonStatus.Text = "运行中";
                        }
                        if (GlobalVar.lsAxiasDIs[2].CurIOStatus)
                        {
                            thisFrm.lalEpsonStatus.Text = "暂停中";
                        }
                        if (GlobalVar.lsAxiasDIs[3].CurIOStatus)
                        {
                            thisFrm.lalEpsonStatus.Text = "控制器一般错误";
                        }
                        if (GlobalVar.lsAxiasDIs[4].CurIOStatus)
                        {
                            thisFrm.lalEpsonStatus.Text = "急停输出";
                        }
                        if (GlobalVar.lsAxiasDIs[5].CurIOStatus)
                        {
                            thisFrm.lalEpsonStatus.Text = "安全门打开";
                        }
                        if (GlobalVar.lsAxiasDIs[6].CurIOStatus)
                        {
                            thisFrm.lalEpsonStatus.Text = "控制器严重错误";
                        }
                        if (GlobalVar.lsAxiasDIs[7].CurIOStatus)
                        {
                            thisFrm.lalEpsonStatus.Text = "报警";
                        }
                    }));
                    if (GlobalVar.totalRunFlag)
                    {
                        //查询安全门状态
                        if (!GlobalVar.lsAxiasDIs[42].CurIOStatus && !GlobalVar.isCloseSafeDoor && !isSafeDoorTrig)//设备安全门打开
                        {
                            isSafeDoorTrig = true;
                            thisFrm.LogAlarmError("安全门打开");
                            //设备所有动作暂停
                            GlobalVar.deviceSafeDoorIsOpen = true;
                            GlobalVar.deviceAlarmIsHappen = true;
                            //绿灯
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[11].Card - 1), GlobalVar.lsAxiasDOs[11].PinDefinition, (ushort)1);
                            //黄灯
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[10].Card - 1), GlobalVar.lsAxiasDOs[10].PinDefinition, (ushort)1);
                            //机器人安全门打开
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[30].Card - 1), GlobalVar.lsAxiasDOs[30].PinDefinition, (ushort)0);
                            Thread.Sleep(50);
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[30].Card - 1), GlobalVar.lsAxiasDOs[30].PinDefinition, (ushort)1);
                            //isAlarmPause = true;
                        }
                        if (!GlobalVar.lsAxiasDIs[41].CurIOStatus && (GlobalVar.deviceSafeDoorIsOpen || GlobalVar.deviceAlarmIsHappen))//如果凸轮分割器在打开安全门后在旋转，当感应到信号后，停止继续旋转
                        {
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[29].Card - 1), GlobalVar.lsAxiasDOs[29].PinDefinition, (ushort)1);
                        }
                        if (GlobalVar.lsAxiasDIs[43].CurIOStatus && !isRobotAlarmTrig && !GlobalVar.deviceSafeDoorIsOpen)//机器人真空报警
                        {
                            isRobotAlarmTrig = true;
                            thisFrm.LogAlarmError("机器人真空吸标报警");
                            GlobalVar.deviceAlarmIsHappen = true;
                        }
                        if (GlobalVar.deviceAlarmIsHappen && !GlobalVar.deviceSafeDoorIsOpen && !isAlarmTrig)//除安全门的其他报警信号
                        {
                            isAlarmTrig = true;
                            //绿灯
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[11].Card - 1), GlobalVar.lsAxiasDOs[11].PinDefinition, (ushort)1);
                            //黄灯
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[10].Card - 1), GlobalVar.lsAxiasDOs[10].PinDefinition, (ushort)1);
                            //机器人安全门打开
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[5].Card - 1), GlobalVar.lsAxiasDOs[5].PinDefinition, (ushort)0);
                            Thread.Sleep(50);
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[5].Card - 1), GlobalVar.lsAxiasDOs[5].PinDefinition, (ushort)1);
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[8].Card - 1), GlobalVar.lsAxiasDOs[8].PinDefinition, (ushort)1);//给注塑机放疗信号
                            //isAlarmPause = true;
                        }
                    }
                    if (GlobalVar.totalRunFlag && !GlobalVar.deviceSafeDoorIsOpen && !GlobalVar.deviceAlarmIsHappen)
                    {
                        if (!GlobalVar.lsAxiasDIs[41].CurIOStatus && !isAOIShotFinished && GlobalVar.isAoiLastTestPro && GlobalVar.lsAxiasDIs[34].CurIOStatus)
                        {
                            GlobalVar.isAoiLastTestPro = false;
                            do_Aoi.BeginInvoke(null, null);
                        }
                        if (GlobalVar.firAndSecShotNGResetCount > 0 && GlobalVar.firAndSecIsAllowShot)
                        {
                            GlobalVar.firAndSecIsAllowShot = false;
                            thisFrm.ControlLightOn(1, true);
                            thisFrm.RunTBTools(false, 1);
                            thisFrm.ControlLightOn(1, false);
                            thisFrm.LogMessage($"NG拍照了{GlobalVar.firAndSecShotNGResetCount}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    GlobalVar.myLog.Error($"主线程异常错误：{ex.Message}");
                }
                finally
                {
                    Thread.Sleep(20);
                    Application.DoEvents();
                }
            }
        }
        #endregion

        #region 窗体事件
        private void FrmHomePage_Load(object sender, EventArgs e)
        {
            cmbClass.Items.Add("白班");
            cmbClass.Items.Add("夜班");
            GlobalVar.totalRunFlagVision = true;
            GlobalVar.mainThread = new Thread(new ParameterizedThreadStart(MainTotalRunThread));//主线程
            GlobalVar.taskList.Add(new Task(DoDeviceTotalRun));
            GlobalVar.taskList.Add(new Task(DoSecStaCycleAction));
            GlobalVar.taskList.Add(new Task(DoFirStaCycleAction));
            GlobalVar.taskList.Add(new Task(DoThirdStaCycleAction));
            GlobalVar.taskList.Add(new Task(DoFourStaCycleAction));
            GlobalVar.taskList.Add(new Task(DoSixStaCycleAction));
            GlobalVar.taskList.Add(new Task(DevieLedChangeStatus));
            GlobalVar.taskList.Add(new Task(AnalysisiAllStaPicData));
            GlobalVar.mainThread.Start();
            foreach (Task task in GlobalVar.taskList)
            {
                task.Start();
            }
            do_Aoi = new Do_AoiAction(thisFrm.AOIFuncDo);
            IOC0640.ioc_write_outbit(Convert.ToUInt16(GlobalVar.lsAxiasDOs[14].Card - 1), GlobalVar.lsAxiasDOs[14].PinDefinition,0);//待补料关闭
        }

        private void FrmHomePage_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        private void btnExport2Excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (GlobalVar.totalRunFlag)
                {
                    LogError("正在全自动运行中，不允许导出数据");
                    return;
                }
                if (dtSelect == null)
                {
                    MessageBox.Show("请先产生报表，再输出Excel");
                    return;
                }
                if (dtSelect.Rows.Count == 0)
                {
                    MessageBox.Show("产生报表行数为0，无法输出Excel");
                    return;
                }
                if (!string.IsNullOrWhiteSpace(txtBoxWO.Text))
                {
                    excelPath = GlobalVar.bTestDataFilePath + cmbBatchID.SelectedItem.ToString() + @".xls";
                    if (ExportExcel(excelPath))
                    {
                        MessageBox.Show("EXCEL资料导出成功!");
                        LogPass("EXCEL资料导出成功!\n导出地址:" + excelPath);
                    }
                    else
                    {
                        MessageBox.Show("Excel导出到本地失败");
                    }
                }
                else
                {
                    txtBoxWO.Focus();
                }
            }
            catch (Exception ex)
            {
                //LogAlarmError("EXCEL资料导出异常!");
                GlobalVar.myLog.Error("EXCEL资料导出异常:" + ex.Message);
            }
        }

        private void btnSaveWoInfo_Click(object sender, EventArgs e)
        {
            if (JudgeTextBoxIsNullOrSpace(txtBoxSN)) { return; }
            if (JudgeTextBoxIsNullOrSpace(txtBoxWO)) { return; }
            if (JudgeComboBoxIsNullOrSpace(cmbClass)) { return; }
            GlobalVar.curCusNumber = txtBoxSN.Text.ToString();
            GlobalVar.curWo = txtBoxWO.Text.ToString();
            GlobalVar.curClass = cmbClass.SelectedItem.ToString();
            switch (GlobalVar.curProName)
            {
                case "Renault":
                    {
                        ClassINI.INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "System", "当前料号", GlobalVar.curCusNumber);
                        ClassINI.INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "System", "当前工单", GlobalVar.curWo);
                        ClassINI.INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "System", "当前班别", GlobalVar.curClass);
                    }
                    break;
                case "Lada":
                    { }
                    break;
            }


        }

        private void FrmHomePage_Enter(object sender, EventArgs e)
        {
            thisFrm.userID.Text = GlobalVar.userName;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Bindcmd_BatchID();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            QueryRealTestData();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgvData.Columns.Clear();
        }

        private void cmbBatchID_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvData.Columns.Clear();
        }

        private void cmbBatchID_TextUpdate(object sender, EventArgs e)
        {
            //清空combobox
            this.cmbBatchID.Items.Clear();
            //清空listNew
            listNew.Clear();
            //遍历全部备查数据
            foreach (var item in listOnit)
            {
                if (item.Contains(this.cmbBatchID.Text))
                {
                    //符合，插入ListNew
                    listNew.Add(item);
                }
            }
            //combobox添加已经查到的关键词
            this.cmbBatchID.Items.AddRange(listNew.ToArray());
            //设置光标位置，否则光标位置始终保持在第一列，造成输入关键词的倒序排列
            this.cmbBatchID.SelectionStart = this.cmbBatchID.Text.Length;
            //保持鼠标指针原来状态，有时候鼠标指针会被下拉框覆盖，所以要进行一次设置。
            Cursor = Cursors.Default;
            cmbBatchID.DroppedDown = true;
        }

        private void cmbBatchID_MouseDown(object sender, MouseEventArgs e)
        {
            cmbBatchID.DroppedDown = true;
        }

        private void btnEndManufa_Click(object sender, EventArgs e)
        {
            if (GlobalVar.lsAxiasDIs[30].CurIOStatus)
            {
                MessageBox.Show("当前上料工站还有料，请等待上料工站上料结束或者在安全情况下手动拿走再继续");
                return;
            }
            if (!GlobalVar.lsAxiasDIs[26].CurIOStatus && GlobalVar.lsAxiasDIs[27].CurIOStatus)
            {
                if (MessageBox.Show("贴标工位当前有产品处于气缸压住状态，请确认是否继续生产'收尾动作'", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[24].Card - 1), GlobalVar.lsAxiasDOs[24].PinDefinition, (ushort)1);//贴logo按压气缸
                    LogError("贴标工位轮盘气缸回退");
                    Thread.Sleep(500);
                    secStationIsReadyOk = false;
                    firStationIsReadyOk = false;
                    GlobalVar.totalRunIsEnded = true;
                }
                else
                { return; }
            }
            else
            {
                GlobalVar.totalRunIsEnded = true;
                secStationIsReadyOk = false;
                firStationIsReadyOk = false;
            }
        }

        private void btnClearAlarm_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(isFeedVaccIsAlarm? "若有未吸起来的产品请先拿走再清除报警" : "请确认所有异常都解决", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                isManualPause = true;
                if (GlobalVar.deviceAlarmIsHappen)
                {
                    GlobalVar.deviceAlarmIsHappen = false;
                }
                if (isRobotAlarmTrig)
                {
                    isRobotAlarmTrig = false;
                }
                if (isFeedVaccIsAlarm)
                {
                    if (GlobalVar.lsAxiasDIs[15].CurIOStatus && !GlobalVar.lsAxiasDIs[14].CurIOStatus)
                    {
                        IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[16].Card - 1), GlobalVar.lsAxiasDOs[16].PinDefinition, (ushort)1);//上料气缸
                        Thread.Sleep(100);
                    }
                    IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[17].Card - 1), GlobalVar.lsAxiasDOs[17].PinDefinition, (ushort)1);//关闭上料真空发生器
                }
                if (firstLunPanSignalError)
                {
                    firstLPSinalAlarmIsClear = true;
                    firstLunPanSignalError = false;
                }
                //红灯
                IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[9].Card - 1), GlobalVar.lsAxiasDOs[9].PinDefinition, (ushort)1);
                //蜂鸣
                IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[12].Card - 1), GlobalVar.lsAxiasDOs[12].PinDefinition, (ushort)1);
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (!GlobalVar.totalRunFlag)
            {
                MessageBox.Show("设备不在全自动运行状态，点击此按钮无效");
                return;
            }
            if (GlobalVar.deviceAlarmIsHappen || isRobotAlarmTrig)
            {
                MessageBox.Show("请先点击清除报警按钮");
                return;
            }
            if (btnPause.Text == "继 续")
            {
                if (MessageBox.Show("是否继续正常运行设备", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (isRobotOverHappen)
                    {
                        MessageBox.Show("机器人贴标超出范围，请停止后再重新启动");
                        return;
                    }
                    if (isFeedVaccIsAlarm)
                    {
                        switch (firStep)
                        {
                            case EnumFirstStation.OpenFeedVacuumSolenoid:
                                {
                                    if (GlobalVar.lsAxiasDIs[14].CurIOStatus && !GlobalVar.lsAxiasDIs[15].CurIOStatus)
                                    {
                                        LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, GlobalVar.injectFeedProPos2, (ushort)GlobalVar.injectFeedCoordModel);
                                        LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, GlobalVar.feedProPos2, (ushort)GlobalVar.feedCoordModel);
                                        LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, GlobalVar.feedRProPos2, (ushort)GlobalVar.feedRCoordModel);
                                        while (LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber) == 0 || LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.FeedAxiasNumber) == 0 || LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber) == 0)
                                        {
                                            Application.DoEvents();
                                        }
                                        firStep = EnumFirstStation.MoveToFeedPos2;
                                    }
                                    else
                                    {
                                        LogAlarmError("上料气缸状态错误"); GlobalVar.deviceAlarmIsHappen = true;return;
                                    }
                                }
                                break;
                            case EnumFirstStation.OpenFeedVacuumSolenoid1:
                                {
                                    if (GlobalVar.lsAxiasDIs[14].CurIOStatus && !GlobalVar.lsAxiasDIs[15].CurIOStatus)
                                    {
                                        LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, GlobalVar.injectFeedProPos3, (ushort)GlobalVar.injectFeedCoordModel);
                                        LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, GlobalVar.feedProPos3, (ushort)GlobalVar.feedCoordModel);
                                        LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, GlobalVar.feedRProPos3, (ushort)GlobalVar.feedRCoordModel);
                                        while (LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber) == 0 || LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.FeedAxiasNumber) == 0 || LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber) == 0)
                                        {
                                            Application.DoEvents();
                                        }
                                        firStep = EnumFirstStation.MoveToFeedPos3;
                                    }
                                    else
                                    {
                                        LogAlarmError("上料气缸状态错误"); GlobalVar.deviceAlarmIsHappen = true; ; return;
                                    }
                                }
                                break;
                            case EnumFirstStation.OpenFeedVacuumSolenoid2:
                                {
                                    if (GlobalVar.lsAxiasDIs[14].CurIOStatus && !GlobalVar.lsAxiasDIs[15].CurIOStatus)
                                    {
                                        LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, GlobalVar.injectFeedProPos4, (ushort)GlobalVar.injectFeedCoordModel);
                                        LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, GlobalVar.feedProPos4, (ushort)GlobalVar.feedCoordModel);
                                        LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, GlobalVar.feedRProPos4, (ushort)GlobalVar.feedRCoordModel);
                                        while (LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber) == 0 || LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.FeedAxiasNumber) == 0 || LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber) == 0)
                                        {
                                            Application.DoEvents();
                                        }
                                        firStep = EnumFirstStation.MoveToFeedPos4;
                                    }
                                    else
                                    {
                                        LogAlarmError("上料气缸状态错误"); GlobalVar.deviceAlarmIsHappen = true; ; return;
                                    }
                                }
                                break;
                            case EnumFirstStation.OpenFeedVacuumSolenoid3:
                                {
                                    if (GlobalVar.lsAxiasDIs[14].CurIOStatus && !GlobalVar.lsAxiasDIs[15].CurIOStatus)
                                    {
                                        isFirStaStepReset = false;
                                        isInjectSignalArrived = false;
                                        firStep = EnumFirstStation.MoveToInjectTakeProPos;
                                    }
                                    else
                                    {
                                        LogAlarmError("上料气缸状态错误"); GlobalVar.deviceAlarmIsHappen = true; ; return;
                                    }
                                }
                                break;
                        }
                    }
                    if (isInjectSignalArrived)
                    {
                        IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[8].Card - 1), GlobalVar.lsAxiasDOs[8].PinDefinition, 0);//正常做的过程中执行此步骤
                    }
                    if (isManualPause)
                    {
                        GlobalVar.deviceSafeDoorIsOpen = false;

                        if (isSafeDoorTrig)
                        {
                            isSafeDoorTrig = false;
                        }
                        if (isAlarmTrig)
                        {
                            isAlarmTrig = false;
                        }
                        if (GlobalVar.totalRunFlag)
                        {
                            isStartBtnResponse = true;
                        }
                        if (!isStopBtnResponse)
                        {
                            //绿灯
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[11].Card - 1), GlobalVar.lsAxiasDOs[11].PinDefinition, (ushort)0);
                            //黄灯
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[10].Card - 1), GlobalVar.lsAxiasDOs[10].PinDefinition, (ushort)1);
                            //机器人继续
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[6].Card - 1), GlobalVar.lsAxiasDOs[6].PinDefinition, (ushort)0);
                            Thread.Sleep(100);
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[6].Card - 1), GlobalVar.lsAxiasDOs[6].PinDefinition, (ushort)1);
                        }
                        isManualPause = false;
                        isFeedVaccIsAlarm = false;
                    }
                }
            }
            else
            {
                if (!isManualPause)
                {
                    if (MessageBox.Show("是否让设备暂停", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        isManualPause = true;
                        //机器人继续
                        IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[6].Card - 1), GlobalVar.lsAxiasDOs[5].PinDefinition, (ushort)0);
                        Thread.Sleep(100);
                        IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[6].Card - 1), GlobalVar.lsAxiasDOs[5].PinDefinition, (ushort)1);
                    }
                }
            }
        }

        private void btnClearData_Click(object sender, EventArgs e)
        {
            if (GlobalVar.totalRunFlag)
            {
                LogError("正在全自动运行中，不允许清除数据");
                return;
            }
            GlobalVar.okCnt = 0;
            GlobalVar.ngCnt = 0;
            txtBoxTotalCnt.Text = "0";
            txtBoxOkCnt.Text = "0";
            txtBoxNgCnt.Text = "0";
            txtBoxOkYeild.Text = "0%";
            ClassINI.INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "System", "不良品数量", txtBoxNgCnt.Text);
            ClassINI.INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "System", "良品数量", txtBoxOkCnt.Text);
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            if (GlobalVar.lsAxiasDIs[14].CurIOStatus && !GlobalVar.lsAxiasDIs[15].CurIOStatus && GlobalVar.lsAxiasDIs[24].CurIOStatus && !GlobalVar.lsAxiasDIs[25].CurIOStatus && GlobalVar.lsAxiasDIs[20].CurIOStatus && !GlobalVar.lsAxiasDIs[21].CurIOStatus && GlobalVar.lsAxiasDIs[22].CurIOStatus && !GlobalVar.lsAxiasDIs[23].CurIOStatus && GlobalVar.lsAxiasDIs[26].CurIOStatus && !GlobalVar.lsAxiasDIs[27].CurIOStatus)
            {
                //RunTBTools(false, 0);
                lblHomeStatus.ForeColor = Color.DeepSkyBlue;
                lblHomeStatus.Text = "正在回原点";
                InitAxiasHomeParamsAndGoHome();
                ControlLightOn(0, true);
                isFirstGoHome = true;
                lblHomeStatus.ForeColor = Color.Green;
                lblHomeStatus.Text = "回原点动作完成,可以启动设备";
            }
            else
            {
                LogError("回原点前各气缸初始状态不对");
                MessageBox.Show("气缸初始状态不正确,请检查上料，放料，测高，压合以及分割器的气缸感应器");
            }
        }

        private void dgvSelectWoID_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgvSelectWoID.SelectedRows == null)
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
        }

        private void btnExportShareExcel_Click(object sender, EventArgs e)
        {
            if (GlobalVar.totalRunFlag)
            {
                LogError("正在全自动运行中，不允许导出数据");
                return;
            }
            try
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
                status = TransFileClass.ConnectState(GlobalVar.fileServerPath, frmUserLogin.mUserID, frmUserLogin.mPassword);
                if (status)
                {
                    //共享文件夹的目录
                    DirectoryInfo theFolder = new DirectoryInfo(GlobalVar.fileServerPath + @"\");
                    string filename = theFolder.ToString();
                    //执行方法
                    TransFileClass.TransportLocalToRemote(excelPath, filename, cmbBatchID.SelectedItem.ToString() + @".xls");  //实现将本地文件写入到远程服务器
                }
                else
                {
                    LogError("未能连接！");
                }
                MessageBox.Show("数据上传成功");
                LogPass("数据上传成功");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            isSecStaTest = true;
            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[0].Card - 1), GlobalVar.lsAxiasDOs[0].PinDefinition, (ushort)0);
            Thread.Sleep(50);
            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[0].Card - 1), GlobalVar.lsAxiasDOs[0].PinDefinition, (ushort)1);
            //改成IO更加安全可靠
            while (true)//机器人通讯是否联通
            {
                if (GlobalVar.isEpsonConnected || GlobalVar.lsAxiasDIs[45].CurIOStatus || GlobalVar.lsAxiasDIs[10].CurIOStatus || !GlobalVar.lsAxiasDIs[44].CurIOStatus)
                {
                    break;
                }
                Application.DoEvents();
            }
        }
        #endregion

        #region 报警日志记录
        public delegate void LogAlarmAppendDelegate(Color color, string text);
        /// <summary> 
        /// 追加显示文本 
        /// </summary> 
        /// <param name="color">文本颜色</param> 
        /// <param name="text">显示文本</param> 
        public void LogAlarmAppend(Color color, string text)
        {
            richTxtBox_Alarm.SelectionColor = color;
            if (!GlobalVar.deviceAlarmIsHappen || !isFirstGoHome)
            {
                richTxtBox_Alarm.AppendText(DateTime.Now.ToString("【HH:mm:ss】") + "-->" + text + "\r\n");
                richTxtBox_Alarm.ScrollToCaret();//richTxtLogRecv.AppendText("\r\n");
                GlobalVar.myLog.Warn(text);
            }
        }
        /// <summary> 
        /// 显示fail信息 
        /// </summary> 
        /// <param name="text"></param> 
        public void LogAlarmError(string text)
        {
            LogAlarmAppendDelegate la = new LogAlarmAppendDelegate(LogAlarmAppend);
            richTxtBox_Alarm.Invoke(la, Color.Red, text);
        }
        #endregion

        #region 生产日志记录
        public delegate void LogAppendDelegate(Color color, string text);
        /// <summary> 
        /// 追加显示文本 
        /// </summary> 
        /// <param name="color">文本颜色</param> 
        /// <param name="text">显示文本</param> 
        public void LogAppend(Color color, string text)
        {
            richTxtBox_Info.SelectionColor = color;
            if (curMsg.Count == 0)      //A ,B,C,A,
            {
                curMsg.Add(text);
            }
            else
            {
                if (curMsg[0] != text)
                {
                    curMsg.Add(text);
                }
                else
                {
                    if (curMsg.Count > 0)
                    {
                        curMsg.RemoveAt(0);
                    }
                }
                if (curMsg.Count >= 1)
                {
                    richTxtBox_Info.AppendText(DateTime.Now.ToString("【HH:mm:ss】") + " -->" + curMsg[curMsg.Count - 1] + "\r\n");
                }
                else
                {
                    richTxtBox_Info.AppendText(DateTime.Now.ToString("【HH:mm:ss】") + "-->" + text + "\r\n");
                }
                if (GlobalVar.isRecordTraceLog)
                {
                    GlobalVar.myLog.Info(text);
                }
            }
            richTxtBox_Info.ScrollToCaret();//richTxtLogRecv.AppendText("\r\n");
        }
        /// <summary> 
        /// 显示pass信息 
        /// </summary> 
        /// <param name="text"></param> 
        public void LogPass(string text)
        {
            LogAppendDelegate la = new LogAppendDelegate(LogAppend);
            richTxtBox_Info.Invoke(la, Color.Green, text);
        }
        /// <summary> 
        /// 显示正常信息 
        /// </summary> 
        /// <param name="text"></param> 
        public void LogMessage(string text)
        {
            LogAppendDelegate la = new LogAppendDelegate(LogAppend);
            richTxtBox_Info.Invoke(la, Color.Black, text);
        }
        public void LogError(string text)
        {
            LogAppendDelegate la = new LogAppendDelegate(LogAppend);
            richTxtBox_Info.Invoke(la, Color.Red, text);
        }
        #endregion

        #region 报表数据添加
        private void QueryRealTestData()
        {
            try
            {
                dtSelect = null;
                string myWO = cmbBatchID.SelectedItem.ToString();
                if (string.IsNullOrEmpty(cmbBatchID.SelectedItem.ToString()))
                {
                    LogMessage("请输入批号");
                    return;
                }
                StringBuilder sb = new StringBuilder();

                sb.Append("select row_number() over(order by AOITestDt desc) as 'Index', UserId as 'UserId',SN as 'P/N',WO as 'WOID', Class as 'Class',FinalBarcode as 'Final Barcode',FinalTestRes as 'Final Result', ");
                sb.Append(" AOITestDt as 'Test Create Date(GMT+0)', HightValue as 'Hight_Value',HightRes as 'Hight_Result', XVCentDis as 'XVCentDisValue',YVCentDis as 'YVCentDisValue',Deg as 'Deg', ");
                sb.Append("AOIRes as 'AOIRes' from TestData  " + string.Format("where IsDel = 0 and WO = '{0}'", myWO));
                sb.Append(!chkGetNgRrt.Checked ? " and HightRes = 'PASS' and AOIRes = 'PASS' and FinalTestRes = 'PASS'" : "");

                dtSelect = GlobalVar.mySqlDb.ExecuteDataTable(sb.ToString(), null);
                if (dtSelect.Rows.Count > 0)
                {
                    dgvData.DataSource = dtSelect;
                    dgvData.AutoResizeColumns();
                }
                else
                {
                    LogMessage("资料库没有记录");
                }
            }
            catch (Exception ex)
            {
                GlobalVar.myLog.Error(ex.Message);
            }
        }
        #endregion

        #region 导出Excel表格
        private bool ExportExcel(string path)
        {
            try
            {
                //用來存放最後一個欄位的名稱.
                string EndCellName = "Q1";

                //引用Excel Application類別
                Excel._Application myExcel = null;
                //引用活頁簿類別
                Excel._Workbook myBook = null;
                //引用工作表類別
                Excel._Worksheet mySheet = null;
                //引用Range類別
                //Excel.Range myRange = null;

                //開啟一個新的應用程式
                myExcel = new Microsoft.Office.Interop.Excel.Application();
                //加入新的活頁簿
                myExcel.Workbooks.Add(true);
                //停用警告訊息
                myExcel.DisplayAlerts = false;
                //讓Excel文件可見 
                myExcel.Visible = false;
                //引用第一個活頁簿
                myBook = myExcel.Workbooks[1];
                //設定活頁簿焦點
                myBook.Activate();
                //引用第一個工作表
                mySheet = (Excel._Worksheet)myBook.Worksheets[1];
                //命名工作表的名稱為 "Array"
                mySheet.Name = "Testing File";
                //設工作表焦點
                mySheet.Activate();

                int a = 0;
                int CellCurrPosit = 0;

                int UpBound1 = dgvData.Rows.Count - 1; //二維陣列數上限
                int UpBound2 = dgvData.Columns.Count - 1; //二維陣列數上限

                //UpBound1= 


                //寫入header

                for (int i = 0; i <= UpBound2; i++)
                {
                    myExcel.Cells[1, i + 1] = dgvData.Columns[i].HeaderText.ToString();
                }


                //以下的Select方法可省略，加速Excel運行，但VBA有些功能必須要用到Select方法。
                //逐行寫入數據
                for (int i = 0; i <= UpBound1; i++)
                {
                    //for (int j = 0; j <= UpBound2 - 1; j++)
                    for (int j = 0; j <= UpBound2; j++)
                    {
                        //以單引號開頭，表示該單元格為純文字
                        Boolean mmi = false;
                        if (dgvData[0, i].Value != null)
                        { mmi = true; }
                        if (mmi == true)
                        {
                            a++;
                            //用offset寫入陣列資料
                            //myRange = mySheet.get_Range("A2", Type.Missing);
                            //myRange.get_Offset(i, j).Select();
                            //myRange.Value2 =  dgvTmp[i,j].Value;
                            //用Cells寫入陣列資料
                            //myRange.get_Range(myExcel.Cells[2 + i, 1 + j], myExcel.Cells[2 + i, 1 + j]).Select();
                            myExcel.Cells[2 + CellCurrPosit, 1 + j] = dgvData[j, i].Value;

                        }
                    }
                    CellCurrPosit += 1;
                }


                // Cells.Select
                //Cells.EntireColumn.AutoFit
                myExcel.Range["A1:" + EndCellName].Select();
                myExcel.Selection.Font.Bold = true;
                myExcel.Selection.HorizontalAlignment = Excel.Constants.xlCenter;
                myExcel.Selection.VerticalAlignment = Excel.Constants.xlCenter;
                myExcel.Cells.Select();
                myExcel.Cells.EntireColumn.AutoFit();
                myExcel.Range["A1"].Select();
                myExcel.Selection.End[Excel.XlDirection.xlToRight].Select();
                myExcel.Selection.End[Excel.XlDirection.xlDown].Select();
                myExcel.Selection.End[Excel.XlDirection.xlDown].Select();
                myExcel.Range[myExcel.Selection, myExcel.Selection.End[Excel.XlDirection.xlToLeft]].Select();
                myExcel.Range[myExcel.Selection, myExcel.Selection.End[Excel.XlDirection.xlToLeft]].Select();
                myExcel.Range[myExcel.Selection, myExcel.Selection.End[Excel.XlDirection.xlUp]].Select();
                myExcel.Range[myExcel.Selection, myExcel.Selection.End[Excel.XlDirection.xlUp]].Select();
                myExcel.Selection.Borders[Excel.XlBordersIndex.xlDiagonalDown].LineStyle = Excel.Constants.xlNone;
                myExcel.Selection.Borders[Excel.XlBordersIndex.xlDiagonalUp].LineStyle = Excel.Constants.xlNone;

                myExcel.Selection.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                //myExcel.Selection.LineStyle = Excel.XlLineStyle.xlContinuous;
                myExcel.Selection.Borders[Excel.XlBordersIndex.xlEdgeLeft].ColorIndex = Excel.Constants.xlAutomatic;
                //myExcel.Selection.Borders[Excel.XlBordersIndex.xlEdgeLeft]. = 0;
                myExcel.Selection.Borders[Excel.XlBordersIndex.xlEdgeLeft].Weight = Excel.XlBorderWeight.xlThin;

                myExcel.Selection.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
                myExcel.Selection.Borders[Excel.XlBordersIndex.xlEdgeTop].ColorIndex = 0;
                //myExcel.Selection.Borders[Excel.XlBordersIndex.xlEdgeTop].TintAndShade = 0;
                myExcel.Selection.Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = Excel.XlBorderWeight.xlThin;

                myExcel.Selection.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                myExcel.Selection.Borders[Excel.XlBordersIndex.xlEdgeBottom].ColorIndex = 0;
                //myExcel.Selection.Borders[Excel.XlBordersIndex.xlEdgeBottom].TintAndShade = 0;
                myExcel.Selection.Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = Excel.XlBorderWeight.xlThin;

                myExcel.Selection.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
                myExcel.Selection.Borders[Excel.XlBordersIndex.xlEdgeRight].ColorIndex = 0;
                //myExcel.Selection.Borders[Excel.XlBordersIndex.xlEdgeRight].TintAndShade = 0;
                myExcel.Selection.Borders[Excel.XlBordersIndex.xlEdgeRight].Weight = Excel.XlBorderWeight.xlThin;

                myExcel.Selection.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;
                myExcel.Selection.Borders[Excel.XlBordersIndex.xlInsideVertical].ColorIndex = 0;
                //myExcel.Selection.Borders[Excel.XlBordersIndex.xlInsideVertical].TintAndShade = 0;
                myExcel.Selection.Borders[Excel.XlBordersIndex.xlInsideVertical].Weight = Excel.XlBorderWeight.xlThin;

                myExcel.Selection.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous;
                myExcel.Selection.Borders[Excel.XlBordersIndex.xlInsideHorizontal].ColorIndex = 0;
                //myExcel.Selection.Borders[Excel.XlBordersIndex.xlInsideHorizontal].TintAndShade = 0;
                myExcel.Selection.Borders[Excel.XlBordersIndex.xlInsideHorizontal].Weight = Excel.XlBorderWeight.xlThin;


                myExcel.Range[EndCellName].Select();
                myExcel.Range[myExcel.Selection, myExcel.Selection.End[Excel.XlDirection.xlToLeft]].Select();

                myExcel.Range["A1:" + EndCellName].Select();
                myExcel.Selection.Interior.Pattern = Excel.Constants.xlSolid;
                myExcel.Selection.Interior.PatternColorIndex = Excel.Constants.xlAutomatic;
                myExcel.Selection.Interior.Color = 5296274;

                //myExcel.Range["D1:D1"].Select();
                //myExcel.Selection.Interior.ColorIndex = 6;
                //myExcel.Range["H1:H1"].Select();
                //myExcel.Selection.Interior.ColorIndex = 6;
                //myExcel.Selection.Interior.TintAndShade = 0;
                //myExcel.Selection.Interior.PatternTintAndShade = 0;
                myExcel.Range["A1:" + EndCellName].Select();
                myExcel.Cells.Select();
                myExcel.Cells.EntireColumn.AutoFit();
                mySheet.Protect("zidonghua", false, true, true, true, false, false, false, false, false, false, false, false, false, false, false);

                ////加入新的工作表在第1張工作表之後
                //myBook.Sheets.Add(Type.Missing, myBook.Worksheets[1], 1, Type.Missing);
                ////引用第2個工作表
                //mySheet = (_Worksheet)myBook.Worksheets[2];
                ////命名工作表的名稱為 "Array"
                //mySheet.Name = "Array";
                ////Console.WriteLine(mySheet.Name);
                ////寫入報表名稱 
                //myExcel.Cells[1, 4] = "普通報表";
                ////設定範圍
                //myRange = (Range)mySheet.get_Range(myExcel.Cells[2, 1], myExcel.Cells[UpBound1 + 1, UpBound2 + 1]);

                //myRange.Select();


                ////用陣列一次寫入資料
                //myRange.Value2 = "'" + dgvTmp;
                //設定儲存路徑
                string PathFile = path;
                //设置打开密码
                //myBook.Password = "zidonghua";

                //另存活頁簿
                myBook.SaveAs(PathFile, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing
                                , Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                //關閉活頁簿
                myBook.Close(false, Type.Missing, Type.Missing);
                //關閉Excel
                myExcel.Quit();
                //釋放Excel資源
                System.Runtime.InteropServices.Marshal.ReleaseComObject(myExcel);
                myBook = null;
                mySheet = null;
                //myRange = null;
                myExcel = null;
                GC.Collect();
                return true;
            }
            catch (Exception ex)
            {
                LogMessage("EXCEL资料导出失败");
                GlobalVar.myLog.Error("EXCEL资料导出失败:" + ex.Message);
                return false;
            }
        }
        #endregion

        #region Aoi测试完成后的判断最早一笔数据，并给出结果让第六工位去根据结果判断丢料位置
        private void JudgeTakePose()
        {
            if (GlobalVar.aoiDatas.Count > 0 && GlobalVar.highDatas.Count > 0)
            {
                if (GlobalVar.aoiDatas[0].AOIRes == "PASS" && GlobalVar.highDatas[0].HightRes == "PASS")
                {
                    GlobalVar.finalHighResAndAoiRes.Add(true);
                    finalTakeHighResAndAoiRes.Add(true);
                    GlobalVar.okCnt++;
                }
                else
                {
                    GlobalVar.finalHighResAndAoiRes.Add(false);
                    finalTakeHighResAndAoiRes.Add(false);
                    GlobalVar.ngCnt++;
                }

                this.Invoke(new Action(() =>
                {
                    picTotalRes.Image = GlobalVar.finalHighResAndAoiRes[0] ? Properties.Resources.tpass : Properties.Resources.tFail;
                    txtBoxTotalCnt.Text = (GlobalVar.okCnt + GlobalVar.ngCnt).ToString();
                    txtBoxOkCnt.Text = GlobalVar.okCnt.ToString();
                    txtBoxNgCnt.Text = GlobalVar.ngCnt.ToString();
                    txtBoxOkYeild.Text = (GlobalVar.okCnt + GlobalVar.ngCnt) == 0 ? "0%" : $"{(GlobalVar.okCnt * 1.0 / (GlobalVar.okCnt + GlobalVar.ngCnt) * 100.0).ToString("f1")}%";
                    ClassINI.INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "System", "不良品数量", txtBoxNgCnt.Text);
                    ClassINI.INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "System", "良品数量", txtBoxOkCnt.Text);
                }));
                ///存储结果值
                GeneraFinalBarcode();
                if (GlobalVar.aoiDatas.Count > 0)
                {
                    GlobalVar.aoiDatas.RemoveAt(0);
                }
                if (GlobalVar.highDatas.Count > 0)
                {
                    GlobalVar.highDatas.RemoveAt(0);
                }
                if (GlobalVar.finalHighResAndAoiRes.Count > 0)
                {
                    GlobalVar.finalHighResAndAoiRes.RemoveAt(0);
                }
            }
        }
        #endregion

        #region 生成FinalBarcode
        /// <summary>
        /// 打印条码
        /// </summary>
        /// <param name="finalBarcode"></param>
        private void ActionPrint(string finalBarcode)
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
                r.DMX(GlobalVar.printX, GlobalVar.printY, 24, 24, 3, 6, 3, 0, finalBarcode);
                r.PRT(1, 0, 1);
                Thread.Sleep(100);
                r.EndPrinter();
            }
        }
        /// <summary>
        /// 生成最终条码
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        private void GeneraFinalBarcode()
        {
            TestData data = new TestData();
            string finalBarcode = "";
            try
            {
                finalBarcode += GlobalVar.curCusNumber;
                finalBarcode += DateTime.Now.Year.ToString("00");
                finalBarcode += DateTime.Now.Month.ToString("00");
                finalBarcode += DateTime.Now.Day.ToString("00");
                if (GlobalVar.curProName == "Lada")
                {
                    finalBarcode += GlobalVar.curClass == "白班" ? "L" : "R";
                }
                else
                {
                    finalBarcode += GlobalVar.curClass == "白班" ? "A" : "B";
                }
                //ActionPrint(finalBarcode);

                finalBarcode += (++GlobalVar.snCnt).ToString("00000");
                if (GlobalVar.finalHighResAndAoiRes[0])
                {
                    using (RedisListService service = new RedisListService())
                    {
                        service.Publish("PrintServices", $"{finalBarcode}");
                    }
                }
                data.FinalBarcode = finalBarcode;
                aoiFinalBarcode = finalBarcode;
                data.WO = GlobalVar.curWo;
                data.SN = GlobalVar.curCusNumber;
                data.Class = GlobalVar.curClass;
                data.HightValue = GlobalVar.highDatas[0].HightValue;
                data.HightRes = GlobalVar.highDatas[0].HightRes;
                data.HightTestDt = GlobalVar.highDatas[0].HightTestDt;
                data.XVCentDis = GlobalVar.aoiDatas[0].XVCentDis;
                data.YVCentDis = GlobalVar.aoiDatas[0].YVCentDis;
                data.Deg = GlobalVar.aoiDatas[0].Deg;
                data.AOIRes = GlobalVar.aoiDatas[0].AOIRes;
                data.AOITestDt = GlobalVar.aoiDatas[0].AOITestDt;
                data.FinalTestRes = GlobalVar.finalHighResAndAoiRes.Count > 0 ? GlobalVar.finalHighResAndAoiRes[0] ? "PASS" : "FAIL" : "FAIL";
                data.UserId = GlobalVar.userName;
                string sql = string.Format("insert into TestData values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')", data.FinalBarcode, data.SN, data.WO,
                   data.Class, data.HightValue, data.HightRes, data.HightTestDt, data.XVCentDis, data.YVCentDis, data.Deg, data.AOIRes, data.AOITestDt, data.FinalTestRes, data.UserId, 0);
                GlobalVar.mySqlDb.ExecuteNonQuery(sql, null);
                LogMessage("资料存储成功");
                ClassINI.INI.INIWriteValue(GlobalVar.curProName == "Renault" ? GlobalVar.bRecipeRenaultFilePath : GlobalVar.bRecipeLadaFilePath, "System", "当前流水号", GlobalVar.snCnt.ToString());
            }
            catch (Exception ex)
            {
                GlobalVar.myLog.Error("生成最终条码保存资料出现未知错误 : " + ex.Message);
            }
        }
        #endregion

        #region 刷新工单方法
        /// <summary>
        /// 存储所有BatchID
        /// </summary>  
        Dictionary<string, int> dic_WoID = new Dictionary<string, int>();
        public void Bindcmd_BatchID()
        {
            try
            {
                listOnit.Clear();
                dic_WoID.Clear();
                this.cmbBatchID.Items.Clear();
                txtBoxSN.Text = GlobalVar.curCusNumber;
                txtBoxWO.Text = GlobalVar.curWo;
                DataTable dt = GlobalVar.mySqlDb.ExecuteDataTable("proc_GetWoIDandWODataList", null, CommandType.StoredProcedure);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dic_WoID.Add(dt.Rows[i][0].ToString(), i);
                }
                listOnit = dic_WoID.Keys.ToList();
                this.cmbBatchID.Items.AddRange(listOnit.ToArray());
            }
            catch (Exception ex)
            {
                GlobalVar.isLoadAllParam = false;
            }
        }
        #endregion

        private bool JudgeTextBoxIsNullOrSpace(TextBox tb)
        {
            if (string.IsNullOrEmpty(tb.Text.Trim()))
            {
                MessageBox.Show("数据不能为空!!");
                return true;
            }
            else
            { return false; }
        }

        private bool JudgeComboBoxIsNullOrSpace(ComboBox cmb)
        {
            if (string.IsNullOrEmpty(cmb.Text))
            {
                MessageBox.Show("数据不能为空!!");
                return true;
            }
            else
            { return false; }
        }

        #region 全自动运行所有动作
        #region 全自动运行需要的变量
        /// <summary>
        /// 第一工位轮盘到位信号异常
        /// </summary>
        static bool firstLunPanSignalError = false;
        /// <summary>
        /// 第一工位轮盘到位信号异常清除标记
        /// </summary>
        static bool firstLPSinalAlarmIsClear = false;
        static bool logo4Finished = false;
        static bool firStationIsReadyOk = false;
        static bool secStationIsReadyOk = false;
        static bool thrStationIsReadyOk = false;
        static bool fourStationIsReadyOk = false;
        static bool sixStationisReadyOk = false;
        static bool lastFeedInjectPosArrivated = false;
        static bool lastFeedAndFeedRPosArrivated = false;
        //是否允许旋转平台贴Logo气缸下降
        static bool isOtherStationAllowCylinderDown = false;
        static bool isAllowThrStaTest = false;
        static bool isAllowFourStaTest = false;
        static bool isAllowSixStaTest = false;
        EnumFirstStation firStep = EnumFirstStation.MoveToInjectTakeProPos;
        static bool isCurStepAction = false;
        /// <summary>
        /// 旋转气缸是正旋还是反旋，默认正旋
        /// </summary>
        bool isRotatePorN = true;
        static bool isCurSecStepAction = false;
        EnumSecStation secStep = EnumSecStation.SenseAllSensor;
        static bool isLabelOut = false;//下降沿信号
        static bool isCurThrStepAction = false;
        EnumThrStation thrStep = EnumThrStation.SenseAllSensor;
        bool startPress = false;//持续保压时间标记
        int pressCnt = 0;
        static bool isCurFourStepAction = false;
        EnumFourStation fourStep = EnumFourStation.SenseAllSensor;
        bool startHighPress = false;//持续保压时间标记
        int pressHighCnt = 0;
        /// <summary>
        /// 存储采集值
        /// </summary>
        List<double> dataList = new List<double>();
        static bool isCurSixStepAction = false;
        EnumSixStation sixStep = EnumSixStation.MoveToFeedPos;
        private int cylinderSensorTimeOut = 3000;
        private int axiasMoveTimeOut = 5000;
        private bool isRobot1 = false;
        private bool isRobot2 = false;
        /// <summary>
        /// 是否第一次旋转气缸
        /// </summary>
        private static bool isRotateCylinder = false;
        /// <summary>
        /// 射出机是否已经给了响应信号
        /// </summary>
        private static bool isInjectSignalArrived = false;
        /// <summary>
        /// 上料真空是否报警
        /// </summary>
        static bool isFeedVaccIsAlarm = false;
        /// <summary>
        /// 有信是否给过放料完成信号
        /// </summary>
        static bool isInjectSignalIsOnOver = false;
        /// <summary>
        /// 出标动作是否完成
        /// </summary>
        static bool isLabelActionFinished = false;
        #endregion

        #region 开始运行用到的方法

        #region 允许设备启动
        private void DoDeviceTotalRun()
        {
            while (GlobalVar.totalRunFlagVision)
            {
                try
                {
                    if (GlobalVar.totalRunFlag && !GlobalVar.deviceSafeDoorIsOpen && !GlobalVar.deviceAlarmIsHappen)
                    {
                        if (!thisFrm.startHighPress && thisFrm.pressHighCnt > 10)
                        {
                            thisFrm.dataList.Add(thisFrm.GatherDataing());
                        }
                        if (firStationIsReadyOk && !secStationIsReadyOk && !thrStationIsReadyOk && !fourStationIsReadyOk && !sixStationisReadyOk && !GlobalVar.lsAxiasDIs[41].CurIOStatus && GlobalVar.lsAxiasDIs[26].CurIOStatus && !GlobalVar.lsAxiasDIs[27].CurIOStatus && GlobalVar.lsAxiasDIs[22].CurIOStatus && !GlobalVar.lsAxiasDIs[23].CurIOStatus && GlobalVar.lsAxiasDIs[20].CurIOStatus && !GlobalVar.lsAxiasDIs[21].CurIOStatus && GlobalVar.lsAxiasDIs[24].CurIOStatus && !GlobalVar.lsAxiasDIs[25].CurIOStatus && GlobalVar.lsAxiasDIs[14].CurIOStatus && !GlobalVar.lsAxiasDIs[15].CurIOStatus && GlobalVar.lsAxiasDIs[30].CurIOStatus)
                        {
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[29].Card - 1), GlobalVar.lsAxiasDOs[29].PinDefinition, 0);

                            firStationIsReadyOk = false;
                        }
                        else if (GlobalVar.totalRunIsEnded && !GlobalVar.lsAxiasDIs[30].CurIOStatus && !secStationIsReadyOk && !thrStationIsReadyOk && !fourStationIsReadyOk && !sixStationisReadyOk && !GlobalVar.lsAxiasDIs[41].CurIOStatus && GlobalVar.lsAxiasDIs[26].CurIOStatus && !GlobalVar.lsAxiasDIs[27].CurIOStatus && GlobalVar.lsAxiasDIs[22].CurIOStatus && !GlobalVar.lsAxiasDIs[23].CurIOStatus && GlobalVar.lsAxiasDIs[20].CurIOStatus && !GlobalVar.lsAxiasDIs[21].CurIOStatus && GlobalVar.lsAxiasDIs[24].CurIOStatus && !GlobalVar.lsAxiasDIs[25].CurIOStatus && GlobalVar.lsAxiasDIs[14].CurIOStatus && !GlobalVar.lsAxiasDIs[15].CurIOStatus)
                        {
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[29].Card - 1), GlobalVar.lsAxiasDOs[29].PinDefinition, 0);
                        }
                        else
                        {
                            if (!GlobalVar.lsAxiasDIs[41].CurIOStatus && GlobalVar.isDivCamIsRotating)
                            {
                                IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[29].Card - 1), GlobalVar.lsAxiasDOs[29].PinDefinition, 1);
                                GlobalVar.isDivCamIsRotating = false;
                                isOtherStationAllowCylinderDown = true;
                                Thread.Sleep(1000);
                            }
                            if (GlobalVar.lsAxiasDIs[41].CurIOStatus && !GlobalVar.isDivCamIsRotating)
                            {
                                GlobalVar.isDivCamIsRotating = true;
                                isAOIShotFinished = false;
                                isAllowThrStaTest = true;
                                isAllowFourStaTest = true;
                                isAllowSixStaTest = true;
                            }
                        }
                        //if (GlobalVar.lsAxiasDIs[25].CurIOStatus || GlobalVar.lsAxiasDIs[21].CurIOStatus || GlobalVar.lsAxiasDIs[23].CurIOStatus || GlobalVar.lsAxiasDIs[27].CurIOStatus)
                        if (GlobalVar.lsAxiasDIs[21].CurIOStatus || GlobalVar.lsAxiasDIs[23].CurIOStatus || GlobalVar.lsAxiasDIs[27].CurIOStatus)
                        {
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[29].Card - 1), GlobalVar.lsAxiasDOs[29].PinDefinition, 1);
                        }
                        //当工位都清空后，复位该信号，停止轮盘转动
                        if (GlobalVar.totalRunIsEnded && !GlobalVar.isDivCamIsRotating && !GlobalVar.lsAxiasDIs[30].CurIOStatus && !GlobalVar.lsAxiasDIs[31].CurIOStatus && !GlobalVar.lsAxiasDIs[32].CurIOStatus && !GlobalVar.lsAxiasDIs[33].CurIOStatus && !GlobalVar.lsAxiasDIs[34].CurIOStatus && !GlobalVar.lsAxiasDIs[35].CurIOStatus)
                        {
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[29].Card - 1), GlobalVar.lsAxiasDOs[29].PinDefinition, 1);
                            GlobalVar.totalRunIsEnded = false;
                            //红灯
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[9].Card - 1), GlobalVar.lsAxiasDOs[9].PinDefinition, (ushort)1);
                            //蜂鸣
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[12].Card - 1), GlobalVar.lsAxiasDOs[12].PinDefinition, (ushort)1);
                            //绿灯
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[11].Card - 1), GlobalVar.lsAxiasDOs[11].PinDefinition, (ushort)1);
                            //黄灯
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[10].Card - 1), GlobalVar.lsAxiasDOs[10].PinDefinition, (ushort)0);
                        }
                        if (lastFeedInjectPosArrivated && lastFeedAndFeedRPosArrivated && isFirStaStepReset)
                        {
                            lastFeedInjectPosArrivated = false;
                            lastFeedAndFeedRPosArrivated = false;
                            if (isCurStepAction)
                            {
                                isCurStepAction = false;
                            }
                            firStep = EnumFirstStation.MoveToFeedPos1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    GlobalVar.myLog.Error($"第一工位运行发生未知错误:{ex.Message}");
                }
                finally
                {
                    Thread.Sleep(10);
                    Application.DoEvents();
                }
            }
        }

        #endregion 

        #region  第一工位
        private void DoFirStaCycleAction()
        {
            while (GlobalVar.totalRunFlagVision)
            {
                try
                {
                    if (GlobalVar.totalRunFlag && !GlobalVar.totalRunIsEnded && !GlobalVar.deviceSafeDoorIsOpen && !GlobalVar.deviceAlarmIsHappen && !isManualPause && !GlobalVar.totalRunIsEnded)
                    {
                        switch (firStep)
                        {
                            case EnumFirstStation.MoveToInjectTakeProPos:
                                {
                                    if (!isCurStepAction)
                                    {
                                        isCurStepAction = true;
                                        if (!isFirStaStepReset)
                                        {
                                            FirstStep();
                                        }
                                    }
                                }
                                break;
                            case EnumFirstStation.WaitYunshinFinishedOk:
                                {
                                    if (!isCurStepAction)
                                    {
                                        isCurStepAction = true;
                                        SecStep();
                                    }
                                }
                                break;
                            case EnumFirstStation.MoveToFeedPos1:
                                {
                                    if (!isCurStepAction)
                                    {
                                        isCurStepAction = true;
                                        TheSameStep1(EnumFirstStation.MoveToFeedPos1, "运动到上料位置1超时");
                                    }
                                }
                                break;
                            case EnumFirstStation.JudgeFeedSensorAndCylinderSensor:
                                {
                                    if (!isCurStepAction)
                                    {
                                        isCurStepAction = true;
                                        TheSameStep2(EnumFirstStation.JudgeFeedSensorAndCylinderSensor);
                                    }
                                }
                                break;
                            case EnumFirstStation.OpenFeedVacuumSolenoid:
                                {
                                    if (!isCurStepAction)
                                    {
                                        isCurStepAction = true;
                                        TheSameStep3(EnumFirstStation.OpenFeedVacuumSolenoid);
                                    }
                                }
                                break;
                            case EnumFirstStation.MoveToTakeProPos:
                                {
                                    if (!isCurStepAction)
                                    {
                                        isCurStepAction = true;
                                        TheSameStep4(EnumFirstStation.MoveToTakeProPos);
                                    }
                                }
                                break;
                            case EnumFirstStation.JudgeCylinderSensorAndControlDown:
                                {
                                    if (!isCurStepAction)
                                    {
                                        isCurStepAction = true;
                                        TheSameStep5(EnumFirstStation.JudgeCylinderSensorAndControlDown);
                                    }
                                }
                                break;
                            case EnumFirstStation.JudgeCloseFeedSensorAndCylinderSensor:
                                {
                                    if (!isCurStepAction)
                                    {
                                        isCurStepAction = true;
                                        TheSameStep6(EnumFirstStation.JudgeCloseFeedSensorAndCylinderSensor, GlobalVar.injectFeedProPos2, GlobalVar.feedProPos2, GlobalVar.feedRProPos2, 2);
                                    }
                                }
                                break;
                            case EnumFirstStation.MoveToFeedPos2:
                                {
                                    if (!isCurStepAction)
                                    {
                                        isCurStepAction = true;
                                        TheSameStep1(EnumFirstStation.MoveToFeedPos2, "运动到上料位置2超时");
                                    }
                                }
                                break;
                            case EnumFirstStation.JudgeFeedSensorAndCylinderSensor1:
                                {
                                    if (!isCurStepAction)
                                    {
                                        isCurStepAction = true;
                                        TheSameStep2(EnumFirstStation.JudgeFeedSensorAndCylinderSensor1);
                                    }
                                }
                                break;
                            case EnumFirstStation.OpenFeedVacuumSolenoid1:
                                {
                                    if (!isCurStepAction)
                                    {
                                        isCurStepAction = true;
                                        TheSameStep3(EnumFirstStation.OpenFeedVacuumSolenoid1);
                                    }
                                }
                                break;
                            case EnumFirstStation.MoveToTakeProPos1:
                                {
                                    if (!isCurStepAction)
                                    {
                                        isCurStepAction = true;
                                        TheSameStep4(EnumFirstStation.MoveToTakeProPos1);
                                    }
                                }
                                break;
                            case EnumFirstStation.JudgeCylinderSensorAndControlDown1:
                                {
                                    if (!isCurStepAction)
                                    {
                                        isCurStepAction = true;
                                        TheSameStep5(EnumFirstStation.JudgeCylinderSensorAndControlDown1);
                                    }
                                }
                                break;
                            case EnumFirstStation.JudgeCloseFeedSensorAndCylinderSensor1:
                                {
                                    if (!isCurStepAction)
                                    {
                                        isCurStepAction = true;
                                        TheSameStep6(EnumFirstStation.JudgeCloseFeedSensorAndCylinderSensor1, GlobalVar.injectFeedProPos3, GlobalVar.feedProPos3, GlobalVar.feedRProPos3, 3);
                                    }
                                }
                                break;
                            case EnumFirstStation.MoveToFeedPos3:
                                {
                                    if (!isCurStepAction)
                                    {
                                        isCurStepAction = true;
                                        TheSameStep1(EnumFirstStation.MoveToFeedPos3, "运动到上料位置3超时");
                                    }
                                }
                                break;
                            case EnumFirstStation.JudgeFeedSensorAndCylinderSensor2:
                                {
                                    if (!isCurStepAction)
                                    {
                                        isCurStepAction = true;
                                        TheSameStep2(EnumFirstStation.JudgeFeedSensorAndCylinderSensor2);
                                    }
                                }
                                break;
                            case EnumFirstStation.OpenFeedVacuumSolenoid2:
                                {
                                    if (!isCurStepAction)
                                    {
                                        isCurStepAction = true;
                                        TheSameStep3(EnumFirstStation.OpenFeedVacuumSolenoid2);
                                    }
                                }
                                break;
                            case EnumFirstStation.MoveToTakeProPos2:
                                {
                                    if (!isCurStepAction)
                                    {
                                        isCurStepAction = true;
                                        TheSameStep4(EnumFirstStation.MoveToTakeProPos2);
                                    }
                                }
                                break;
                            case EnumFirstStation.JudgeCylinderSensorAndControlDown2:
                                {
                                    if (!isCurStepAction)
                                    {
                                        isCurStepAction = true;
                                        TheSameStep5(EnumFirstStation.MoveToTakeProPos2);
                                    }
                                }
                                break;
                            case EnumFirstStation.JudgeCloseFeedSensorAndCylinderSensor2:
                                {
                                    if (!isCurStepAction)
                                    {
                                        isCurStepAction = true;
                                        TheSameStep6(EnumFirstStation.JudgeCloseFeedSensorAndCylinderSensor2, GlobalVar.injectFeedProPos4, GlobalVar.feedProPos4, GlobalVar.feedRProPos4, 4);
                                    }
                                }
                                break;
                            case EnumFirstStation.MoveToFeedPos4:
                                {
                                    if (!isCurStepAction)
                                    {
                                        isCurStepAction = true;
                                        TheSameStep1(EnumFirstStation.MoveToFeedPos4, "运动到上料位置4超时");
                                    }
                                }
                                break;
                            case EnumFirstStation.JudgeFeedSensorAndCylinderSensor3:
                                {
                                    if (!isCurStepAction)
                                    {
                                        isCurStepAction = true;
                                        TheSameStep2(EnumFirstStation.JudgeFeedSensorAndCylinderSensor3);
                                    }
                                }
                                break;
                            case EnumFirstStation.OpenFeedVacuumSolenoid3:
                                {
                                    if (!isCurStepAction)
                                    {
                                        isCurStepAction = true;
                                        TheSameStep3(EnumFirstStation.OpenFeedVacuumSolenoid3);
                                    }
                                }
                                break;
                            case EnumFirstStation.MoveToTakeProPos3:
                                {
                                    if (!isCurStepAction)
                                    {
                                        isCurStepAction = true;
                                        TheSameStep4(EnumFirstStation.MoveToTakeProPos3);
                                    }
                                }
                                break;
                            case EnumFirstStation.JudgeCylinderSensorAndControlDown3:
                                {
                                    if (!isCurStepAction)
                                    {
                                        isCurStepAction = true;
                                        TheSameStep5(EnumFirstStation.JudgeCylinderSensorAndControlDown3);
                                    }
                                }
                                break;
                            case EnumFirstStation.JudgeCloseFeedSensorAndCylinderSensor3:
                                {
                                    if (!isCurStepAction)
                                    {
                                        isCurStepAction = true;
                                        if (!lastFeedAndFeedRPosArrivated)
                                        {
                                            TheSameStep6(EnumFirstStation.JudgeCloseFeedSensorAndCylinderSensor3, 0, GlobalVar.feedProPos1, GlobalVar.feedRProPos1, 1);
                                        }
                                        //DoFirstMyAction(cylinderSensorTimeOut, "检测气缸退回状态超时",
                                        //null,
                                        //"上料气缸退回到位,开始移动到有信放料位置",
                                        //new Action(() =>
                                        //{
                                        //    if (isFirStaStepReset)
                                        //    {
                                        //        firStep = EnumFirstStation.MoveToFeedPos1;
                                        //    }
                                        //    else
                                        //    {
                                        //        firStep = EnumFirstStation.WaitYunshinFinishedOk;
                                        //    }
                                        //    firStationIsReadyOk = true;
                                        //}),
                                        //null,
                                        //() => GlobalVar.lsAxiasDIs[14].CurIOStatus && !GlobalVar.lsAxiasDIs[15].CurIOStatus);
                                    }
                                }
                                break;
                        }
                    }
                    if (GlobalVar.lsAxiasDIs[8].CurIOStatus && !isInjectSignalIsOnOver && GlobalVar.totalRunFlag)
                    {
                        isInjectSignalIsOnOver = true;
                        Thread.Sleep(100);
                        IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[8].Card - 1), GlobalVar.lsAxiasDOs[8].PinDefinition, 1);
                        isInjectSignalArrived = false;
                    }
                }
                catch (Exception ex)
                {
                    GlobalVar.myLog.Error($"第一工位运行发生未知错误:{ex.Message}");
                }
                finally
                {
                    Thread.Sleep(10);
                    Application.DoEvents();
                }
            }
        }
        private void FirstStep()
        {
            #region 初始化第一步是先移动到放料位置，再发送允许放料信号
            DoFirstMyAction(cylinderSensorTimeOut, "到有信放料位置超时",
            new Action(() => { firStep = EnumFirstStation.MoveToInjectTakeProPos; }),
            "指定位置到达,发送指令给射出机允许放料",
            new Action(() =>
            {
                Thread.Sleep(10); IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[8].Card - 1), GlobalVar.lsAxiasDOs[8].PinDefinition, 0); isInjectSignalArrived = true; firStep++;
            }),
            new Action(() => { LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, GlobalVar.injectTakeProPos, (ushort)GlobalVar.injectFeedCoordModel); }),
            () => LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber) == 1);
            #endregion
        }
        private void SecStep()
        {
            #region 第二步是等待有信放料完成，下一步移动到上料位置1
            DoFirstMyAction(axiasMoveTimeOut, "等待有信放料信号超时",
            null,
            "有信放料完成,三轴开始移动到上料位置",
            new Action(() =>
            {
                if (GlobalVar.lsAxiasDIs[14].CurIOStatus && !GlobalVar.lsAxiasDIs[15].CurIOStatus)
                {
                    //三轴同动
                    if (!isFirStaStepReset)
                    {
                        isFirStaStepReset = true;
                        LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, GlobalVar.injectFeedProPos1, (ushort)GlobalVar.injectFeedCoordModel);
                    }
                    LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, GlobalVar.feedProPos1, (ushort)GlobalVar.feedCoordModel);
                    LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, GlobalVar.feedRProPos1, (ushort)GlobalVar.feedRCoordModel);
                    Thread.Sleep(100);
                    firStep++;
                }
                else
                {
                    LogAlarmError("供料轴气缸状态不对,请停止设备检查"); GlobalVar.deviceAlarmIsHappen = true;
                }
            }),
            null,
            () => isInjectSignalIsOnOver);
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="curStep"></param>
        /// <param name="msg"></param>
        private void TheSameStep1(EnumFirstStation curStep, string msg)
        {
            #region 第三步是判断是否移动到上料位置1，下一步判断料感信号以及气缸初始状态是否正常，
            DoFirstMyAction(axiasMoveTimeOut, msg,
            new Action(() =>
            {
                if (GlobalVar.lsAxiasDIs[15].CurIOStatus && !GlobalVar.lsAxiasDIs[14].CurIOStatus && !GlobalVar.deviceAlarmIsHappen)
                {
                    LogAlarmError("上料气缸感应器初始状态不对");
                    GlobalVar.deviceAlarmIsHappen = true;
                }
                //LogError("移动到上料位置1超时,请检查供料3个轴是否正常");
            }),
            "指定位置到达,开始检查料感信号以及气缸初始状态是否正常",
            new Action(() =>
            {
                LogMessage("分割器到位信号和料感信号以及气缸初始状态正常,上料气缸打出");
                if (LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber) == 1 && LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.FeedAxiasNumber) == 1 && LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber) == 1)
                {
                    //isInjectSignalArrived = false;
                    IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[16].Card - 1), GlobalVar.lsAxiasDOs[16].PinDefinition, (ushort)0);//上料气缸
                    firStep++;
                }
                if (isInjectSignalIsOnOver)
                {
                    isInjectSignalIsOnOver = false;
                }
            }),
            null,
            () =>
            #region xjk 2022.5.20  判断模组是否移动到位，雷赛模组移动到位的反馈脉冲会有1~2个脉冲的误差值，此处只要在10个脉冲范围内都认为移动到位了。
            {
            switch (firStep)
            {
                case EnumFirstStation.MoveToFeedPos1:
                    {
                        return LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber) == 1
          && LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.FeedAxiasNumber) == 1
          && LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber) == 1
           // X轴参数设定
           && (LTDMC.dmc_get_position(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber) < (GlobalVar.injectFeedProPos1 + 10))
          && ((GlobalVar.injectFeedProPos1 - 10) < LTDMC.dmc_get_position(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber))
          // Y轴参数设定
          && (LTDMC.dmc_get_position(GlobalVar.CardId, GlobalVar.FeedAxiasNumber) < (GlobalVar.feedProPos1 + 10))
          && ((GlobalVar.feedProPos1 - 10) < LTDMC.dmc_get_position(GlobalVar.CardId, GlobalVar.FeedAxiasNumber))
          // R轴参数设定
          && (LTDMC.dmc_get_position(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber) < (GlobalVar.feedRProPos1 + 10))
          && ((GlobalVar.feedRProPos1 - 10) < LTDMC.dmc_get_position(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber));
                    }
                    break;

                case EnumFirstStation.MoveToFeedPos2:
                    {
                        return LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber) == 1
          && LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.FeedAxiasNumber) == 1
          && LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber) == 1
          // X轴参数设定
          && (LTDMC.dmc_get_position(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber) < (GlobalVar.injectFeedProPos2 + 10))
          && ((GlobalVar.injectFeedProPos2 - 10) < LTDMC.dmc_get_position(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber))
          // Y轴参数设定
          && (LTDMC.dmc_get_position(GlobalVar.CardId, GlobalVar.FeedAxiasNumber) < (GlobalVar.feedProPos2 + 10))
          && ((GlobalVar.feedProPos2 - 10) < LTDMC.dmc_get_position(GlobalVar.CardId, GlobalVar.FeedAxiasNumber))          
          // R轴参数设定
          && (LTDMC.dmc_get_position(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber) < (GlobalVar.feedRProPos2 + 10))
          && ((GlobalVar.feedRProPos2 - 10) < LTDMC.dmc_get_position(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber));
                    }
                    break;

                case EnumFirstStation.MoveToFeedPos3:
                    {
                        return LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber) == 1
          && LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.FeedAxiasNumber) == 1
          && LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber) == 1
           // X轴参数设定
           && (LTDMC.dmc_get_position(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber) < (GlobalVar.injectFeedProPos3 + 10))
          && ((GlobalVar.injectFeedProPos3 - 10) < LTDMC.dmc_get_position(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber))
          // Y轴参数设定
          && (LTDMC.dmc_get_position(GlobalVar.CardId, GlobalVar.FeedAxiasNumber) < (GlobalVar.feedProPos3 + 10))
          && ((GlobalVar.feedProPos3 - 10) < LTDMC.dmc_get_position(GlobalVar.CardId, GlobalVar.FeedAxiasNumber))
          // R轴参数设定
          && (LTDMC.dmc_get_position(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber) < (GlobalVar.feedRProPos3 + 10))
          && ((GlobalVar.feedRProPos3 - 10) < LTDMC.dmc_get_position(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber));
                    }
                    break;

                case EnumFirstStation.MoveToFeedPos4:
                    {
                        return LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber) == 1
          && LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.FeedAxiasNumber) == 1
          && LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber) == 1
          // X轴参数设定
          && (LTDMC.dmc_get_position(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber) < (GlobalVar.injectFeedProPos4 + 10))
          && ((GlobalVar.injectFeedProPos4 - 10) < LTDMC.dmc_get_position(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber))
          // Y轴参数设定
          && (LTDMC.dmc_get_position(GlobalVar.CardId, GlobalVar.FeedAxiasNumber) < (GlobalVar.feedProPos4 + 10))
          && ((GlobalVar.feedProPos4 - 10) < LTDMC.dmc_get_position(GlobalVar.CardId, GlobalVar.FeedAxiasNumber))
          // R轴参数设定
          && (LTDMC.dmc_get_position(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber) < (GlobalVar.feedRProPos4 + 10))
          && ((GlobalVar.feedRProPos4 - 10) < LTDMC.dmc_get_position(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber));
                    }
                    break;
                default:
                    return true;
                    break;
            }
        }
            );

            #endregion
            #endregion
        }
        private void TheSameStep2(EnumFirstStation curStep)
        {
            #region 第四步是先气缸打出状态是否正常,下一步打开真空发生器，并退回气缸
            DoFirstMyAction(cylinderSensorTimeOut, "上料气缸打出超时",
            new Action(() =>
            {
                if (!GlobalVar.lsAxiasDIs[15].CurIOStatus && GlobalVar.lsAxiasDIs[14].CurIOStatus && !GlobalVar.deviceAlarmIsHappen)
                {
                    //控制气缸退回
                    LogAlarmError("上料气缸打出后上下极限状态不对");
                    GlobalVar.deviceAlarmIsHappen = true;
                }
                //LogError("上料气缸打出超时");
            }),//复位气缸 
            "上料气缸打出到位，打开真空发生器,并退回气缸",
            new Action(() =>
            {
                IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[17].Card - 1), GlobalVar.lsAxiasDOs[17].PinDefinition, (ushort)0);//上料真空发生器
                Thread.Sleep(GlobalVar.feedVaccDelayTime<100?100: GlobalVar.feedVaccDelayTime);//建立真空时间
                IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[16].Card - 1), GlobalVar.lsAxiasDOs[16].PinDefinition, (ushort)1);//上料气缸
                firStep++;
            }),
            null,
            () => GlobalVar.lsAxiasDIs[15].CurIOStatus && !GlobalVar.lsAxiasDIs[14].CurIOStatus);
            #endregion
        }
        private void TheSameStep3(EnumFirstStation curStep)
        {
            #region 第五步检测气缸是否退回到位，下一步移动到放料位置
            DoFirstMyAction(cylinderSensorTimeOut, "上料气缸退回超时",
            new Action(() =>
            {
                if (GlobalVar.lsAxiasDIs[15].CurIOStatus && !GlobalVar.lsAxiasDIs[14].CurIOStatus && !GlobalVar.deviceAlarmIsHappen)
                {
                    LogAlarmError("上料气缸退回状态不对"); GlobalVar.deviceAlarmIsHappen = true;return;
                }
                if (!GlobalVar.lsAxiasDIs[28].CurIOStatus && !GlobalVar.deviceAlarmIsHappen)
                {
                    LogAlarmError("上料真空发生器状态不对"); GlobalVar.deviceAlarmIsHappen = true; isFeedVaccIsAlarm = true;return;
                }
                //LogError("上料气缸退回超时");
            }),
            "开始移动到放料位置",
            new Action(() =>
            {
                if (!GlobalVar.lsAxiasDIs[15].CurIOStatus && GlobalVar.lsAxiasDIs[14].CurIOStatus)
                {
                    LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, GlobalVar.feedTakeProPos, (ushort)GlobalVar.feedCoordModel);
                    LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, GlobalVar.feedRTakeProPos, (ushort)GlobalVar.feedRCoordModel);//移动到轮盘放料位
                    //20210916 增加以下代码优化的动作是：最后一次取注塑件，气缸退回后就发送给供料轴去下料位置等待 byHy
                    Thread.Sleep(100);
                    if (curStep == EnumFirstStation.OpenFeedVacuumSolenoid3)//允许移动到有信机械手下料位置
                    {
                        LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, GlobalVar.injectTakeProPos, (ushort)GlobalVar.injectFeedCoordModel);
                        Thread.Sleep(200);
                        while (LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber) == 0)
                        {
                            Application.DoEvents();
                        }
                        if (isFirStaStepReset)
                        {
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[8].Card - 1), GlobalVar.lsAxiasDOs[8].PinDefinition, 0);//正常做的过程中执行此步骤
                            isInjectSignalArrived = true;
                            DoFirstMyAction1(3600000, "等待有信放料信号超时",
           null,
           "有信放料完成,供料轴开始移动到上料位置1",
           new Action(() =>
           {
               if (GlobalVar.lsAxiasDIs[14].CurIOStatus && !GlobalVar.lsAxiasDIs[15].CurIOStatus)
               {
                   //IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[8].Card - 1), GlobalVar.lsAxiasDOs[8].PinDefinition, 1);
                   isInjectSignalArrived = false;
                   LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, GlobalVar.injectFeedProPos1, (ushort)GlobalVar.injectFeedCoordModel);
                   Thread.Sleep(300);
                   while (LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber) == 0)
                   {
                       Application.DoEvents();
                   }
                   lastFeedInjectPosArrivated = true;
               }
               else
               {
                   LogAlarmError("供料轴气缸状态不对,请停止设备检查"); GlobalVar.deviceAlarmIsHappen = true;
               }
           }),
           null,
           () => isInjectSignalIsOnOver && GlobalVar.lsAxiasDIs[14].CurIOStatus && !GlobalVar.lsAxiasDIs[15].CurIOStatus && !GlobalVar.deviceAlarmIsHappen && !isManualPause);//ON 2秒
                        }
                    }
                    firStep++;
                }
            }),
            null,
            () => !GlobalVar.lsAxiasDIs[15].CurIOStatus && GlobalVar.lsAxiasDIs[14].CurIOStatus && GlobalVar.lsAxiasDIs[28].CurIOStatus);//检测真空负压表是否达标
            #endregion
        }
        private void TheSameStep4(EnumFirstStation curStep)
        {
            #region 第六步检查气缸状态以及是否移动到放料位置，下一步控制气缸
            DoFirstMyAction(30000, "移动到放料位置超时",
            new Action(() =>
            {
               // Thread.Sleep(800);
                //Application.DoEvents();
                if (GlobalVar.lsAxiasDIs[41].CurIOStatus && !GlobalVar.deviceAlarmIsHappen && !firstLunPanSignalError)//
                {
                    LogAlarmError("检测到轮盘停止位置信号错误"); GlobalVar.deviceAlarmIsHappen = true; firstLunPanSignalError = true; return;
                }
                //LogError("移动到放料位置超时");
            }),
            "放料位置到达，上料气缸开始下降",
            new Action(() =>
            {
                if (!GlobalVar.lsAxiasDIs[41].CurIOStatus || firstLPSinalAlarmIsClear)
                {
                    firstLPSinalAlarmIsClear = false;
                    IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[16].Card - 1), GlobalVar.lsAxiasDOs[16].PinDefinition, (ushort)0);//上料气缸
                    firStep++;
                }
            }),
            null,
            () => LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.FeedAxiasNumber) == 1 && LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber) == 1 && !GlobalVar.lsAxiasDIs[30].CurIOStatus && (!GlobalVar.lsAxiasDIs[41].CurIOStatus || firstLPSinalAlarmIsClear));
            #endregion
        }
        private void TheSameStep5(EnumFirstStation curStep)
        {
            #region 第七步检查气缸退回是的状态
            DoFirstMyAction(cylinderSensorTimeOut, "检测上料气缸打出状态超时",
            new Action(() =>
            {
                //LogError("检测上料气缸打出状态超时");
                if (GlobalVar.lsAxiasDIs[14].CurIOStatus && !GlobalVar.deviceAlarmIsHappen)
                {
                    LogAlarmError("上料气缸上极限状态错误"); GlobalVar.deviceAlarmIsHappen = true;return;
                }
                if (!GlobalVar.lsAxiasDIs[15].CurIOStatus && !GlobalVar.deviceAlarmIsHappen)
                {
                    LogAlarmError("上料气缸下极限状态错误"); GlobalVar.deviceAlarmIsHappen = true;return;
                }
            }),
           "上料气缸打出到位,关闭真空发生器，并退回气缸",
           new Action(() =>
           {
               IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[17].Card - 1), GlobalVar.lsAxiasDOs[17].PinDefinition, (ushort)1);//上料真空
               Thread.Sleep(50);//关闭真空吸附
               IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[16].Card - 1), GlobalVar.lsAxiasDOs[16].PinDefinition, (ushort)1);//上料气缸
               //Thread.Sleep(150);
               firStep++;
           }),
           null,
           () => !GlobalVar.lsAxiasDIs[14].CurIOStatus && GlobalVar.lsAxiasDIs[15].CurIOStatus);//不检测真空状态
            #endregion
        }
        private void TheSameStep6(EnumFirstStation curStep, int injectPos, int feedPos, int feedRPos, int posIndex)
        {
            #region 第八步
            DoFirstMyAction(cylinderSensorTimeOut, "检测上料气缸退回状态超时",
            new Action(() =>
            {
                //LogError("检测上料气缸退回状态超时");
                if (!GlobalVar.lsAxiasDIs[14].CurIOStatus && !GlobalVar.deviceAlarmIsHappen)
                {
                    LogAlarmError("上料气缸上极限状态不对"); GlobalVar.deviceAlarmIsHappen = true;return;
                }
                if (GlobalVar.lsAxiasDIs[15].CurIOStatus && !GlobalVar.deviceAlarmIsHappen)
                {
                    LogAlarmError("上料气缸下极限状态不对"); GlobalVar.deviceAlarmIsHappen = true; return;
                }
                if (!GlobalVar.lsAxiasDIs[30].CurIOStatus && !GlobalVar.deviceAlarmIsHappen )
                {
                    LogAlarmError("产品未放到位"); GlobalVar.deviceAlarmIsHappen = true;
                }
            }),
            "上料气缸退回到位,开始移动到上料位置" + posIndex.ToString(),
            new Action(() =>
            {
                if (curStep != EnumFirstStation.JudgeCloseFeedSensorAndCylinderSensor3)
                {
                    if (GlobalVar.lsAxiasDIs[14].CurIOStatus && !GlobalVar.lsAxiasDIs[15].CurIOStatus)
                    {
                        firStationIsReadyOk = true;//凸轮分割器可以转动标记
                        LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, injectPos, (ushort)GlobalVar.injectFeedCoordModel);
                        LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, feedPos, (ushort)GlobalVar.feedCoordModel);
                        LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, feedRPos, (ushort)GlobalVar.feedRCoordModel);
                        Thread.Sleep(100);
                        firStep++;
                    }
                }
                else
                {
                    if (isFirStaStepReset)
                    {
                        if (GlobalVar.lsAxiasDIs[14].CurIOStatus && !GlobalVar.lsAxiasDIs[15].CurIOStatus)
                        {
                           LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, feedPos, (ushort)GlobalVar.feedCoordModel);
                           LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, feedRPos, (ushort)GlobalVar.feedRCoordModel);
                           Thread.Sleep(100);
                           while (LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.FeedAxiasNumber) == 0 && LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber) == 0)
                            {
                                Application.DoEvents();
                            }
                            lastFeedAndFeedRPosArrivated = true;
                            //firStep = EnumFirstStation.MoveToFeedPos1; 
                        }
                    }
                    //else
                    //{
                    //    firStep = EnumFirstStation.WaitYunshinFinishedOk;
                    //}
                    firStationIsReadyOk = true;
                }
            }),
            null,
            () => GlobalVar.lsAxiasDIs[14].CurIOStatus && !GlobalVar.lsAxiasDIs[15].CurIOStatus && GlobalVar.lsAxiasDIs[30].CurIOStatus);// 14气缸上极限 15 气缸下极限 30 料感感应器
            #endregion
        }
        private void DoFirstMyAction(int timeout, string timeoutMessage, Action timeoutAct, string nextStepMessage, Action nextAct, Action curAct, Func<bool> breakWhile)
        {
            bool myTimeOut = true;
            CancellationTokenSource myToken = new CancellationTokenSource(timeout);
            myToken.Token.Register(new Action(() =>
            {
                if (myTimeOut)
                {
                    //LogError(timeoutMessage);
                    if (timeoutAct != null)
                    {
                        timeoutAct.Invoke();
                    }
                }
                else
                {
                    LogMessage(nextStepMessage);
                    if (nextAct != null && GlobalVar.totalRunFlag && !GlobalVar.deviceSafeDoorIsOpen && !GlobalVar.deviceAlarmIsHappen && !isManualPause && !GlobalVar.totalRunIsEnded)
                    {
                        nextAct.Invoke();
                        //if (firStep == EnumFirstStation.JudgeCloseFeedSensorAndCylinderSensor3)
                        //{
                        //    firStep = EnumFirstStation.MoveToInjectTakeProPos;
                        //}
                        //else
                        //{ firStep++; }
                    }
                }
                isCurStepAction = false;
            }));
            if (curAct != null && GlobalVar.totalRunFlag && !GlobalVar.deviceSafeDoorIsOpen && !GlobalVar.deviceAlarmIsHappen && !isManualPause && !GlobalVar.totalRunIsEnded)
            {
                curAct.Invoke();
            }
            Task.Factory.StartNew(new Action(() =>
            {
                while (!myToken.Token.IsCancellationRequested)
                {
                    Thread.Sleep(5);
                    Application.DoEvents();
                    if (breakWhile.Invoke() || !GlobalVar.totalRunFlag)
                    {
                        myTimeOut = false; myToken.Cancel();
                    }
                }
            }), myToken.Token);
        }
        private void DoFirstMyAction1(int timeout, string timeoutMessage, Action timeoutAct, string nextStepMessage, Action nextAct, Action curAct, Func<bool> breakWhile)
        {
            bool myTimeOut = true;
            CancellationTokenSource myToken = new CancellationTokenSource(timeout);
            myToken.Token.Register(new Action(() =>
            {
                if (myTimeOut)
                {
                    //LogError(timeoutMessage);
                    if (timeoutAct != null)
                    {
                        timeoutAct.Invoke();
                    }
                }
                else
                {
                    LogMessage(nextStepMessage);
                    if (nextAct != null && GlobalVar.totalRunFlag && !GlobalVar.deviceSafeDoorIsOpen && !GlobalVar.deviceAlarmIsHappen && !isManualPause && !GlobalVar.totalRunIsEnded)
                    {
                        nextAct.Invoke();
                    }
                }
            }));
            if (curAct != null && GlobalVar.totalRunFlag && !GlobalVar.deviceSafeDoorIsOpen && !GlobalVar.deviceAlarmIsHappen && !isManualPause && !GlobalVar.totalRunIsEnded)
            {
                curAct.Invoke();
            }
            Task.Factory.StartNew(new Action(() =>
            {
                while (!myToken.Token.IsCancellationRequested)
                {
                    Thread.Sleep(5);
                    Application.DoEvents();
                    if (breakWhile.Invoke() || !GlobalVar.totalRunFlag)
                    {
                        myTimeOut = false; myToken.Cancel();
                    }
                }
            }), myToken.Token);
        }
        #endregion

        #region 第二工位
        private void DoSecStaCycleAction()
        {
            while (GlobalVar.totalRunFlagVision)
            {
                try
                {
                    if ((GlobalVar.totalRunFlag || isSecStaTest) && !GlobalVar.totalRunIsEnded && !GlobalVar.deviceSafeDoorIsOpen && !GlobalVar.deviceAlarmIsHappen && !isManualPause && !GlobalVar.totalRunIsEnded)
                    {
                        if (GlobalVar.mOnLineConnections.Count == 1)
                        {
                            if (!GlobalVar.isEpsonConnected)
                            {
                                GlobalVar.SendData("1,1,0,0,0,0");
                            }
                        }
                        //补料完成按钮的信号
                        if (GlobalVar.lsAxiasDIs[11].CurIOStatus && !isRobot2)
                        {
                            //Epson取完4片Logo
                            if (!GlobalVar.lsAxiasDIs[36].CurIOStatus && isRotateCylinder)
                            {
                                LogError("电镀件还未取完，请取完后再双手启动");
                                continue;
                            }
                            if (isRobotAlarmTrig)
                            {
                                LogError("机器人真空报警中");
                                continue;
                            }
                            isRobot2 = true;
                            //旋转气缸
                            RotateCylinderStepMethod();
                            if (!isRotateCylinder)
                            {
                                isRotateCylinder = true;
                            }
                        }
                        //Epson允许出标信号
                        if (GlobalVar.lsAxiasDIs[38].CurIOStatus && secStep == EnumSecStation.UpDownCylinderBack)
                        {
                            secStep = EnumSecStation.SenseAllSensor;
                        }
                        //贴标工站料感信号以及分割器旋转到位信号
                        if (!GlobalVar.isDivCamIsRotating && GlobalVar.lsAxiasDIs[31].CurIOStatus && !GlobalVar.lsAxiasDIs[41].CurIOStatus && !secStationIsReadyOk && isOtherStationAllowCylinderDown )
                        {
                            if (isRobotAlarmTrig)//内部自检
                            {
                                LogError("机器人真空报警中");
                                continue;
                            }
                            IOC0640.ioc_write_outbit(Convert.ToUInt16(GlobalVar.lsAxiasDOs[24].Card - 1), GlobalVar.lsAxiasDOs[24].PinDefinition, 0);//贴logo按压气缸下降
                            LogMessage("贴Logo按压气缸下降");
                            isOtherStationAllowCylinderDown = false;
                            //检测贴标气缸初始状态，并打下去
                            LabelPressCylinderDownMethod();//发送允许贴Logo信号给机器人
                        }
                        //贴标工站料感信号以及分割器旋转到位信号
                        if (!GlobalVar.lsAxiasDIs[31].CurIOStatus && GlobalVar.lsAxiasDIs[41].CurIOStatus && isLabelActionFinished)
                        {
                            isLabelActionFinished = false;
                            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[28].Card - 1), GlobalVar.lsAxiasDOs[28].PinDefinition, 1);//复位允许Epson贴标信号
                            Thread.Sleep(10);
                            Console.WriteLine("test1");
                        }
                        //贴背胶完成信号
                        if (GlobalVar.lsAxiasDIs[39].CurIOStatus && !isRobot1)//机器人给的贴标整个动作完成信号
                        {
                            isRobot1 = true;
                            LabelPressCylinderUPMethod();
                        }
                        //Epson取完4片Logo
                        if (GlobalVar.lsAxiasDIs[36].CurIOStatus)//Epson取完4片LOGO
                        {
                            if (!logo4Finished)
                            {
                                logo4Finished = true;
                                IOC0640.ioc_write_outbit(Convert.ToUInt16(GlobalVar.lsAxiasDOs[25].Card - 1), GlobalVar.lsAxiasDOs[25].PinDefinition, 1);//发送允许取LOgo
                                Thread.Sleep(10);
                                IOC0640.ioc_write_outbit(Convert.ToUInt16(GlobalVar.lsAxiasDOs[13].Card - 1), GlobalVar.lsAxiasDOs[13].PinDefinition, 1);//补料完成灯
                                Thread.Sleep(10);
                                IOC0640.ioc_write_outbit(Convert.ToUInt16(GlobalVar.lsAxiasDOs[14].Card - 1), GlobalVar.lsAxiasDOs[14].PinDefinition, 0);//待补料灯
                                Thread.Sleep(10);
                            }
                        }
                        else
                        { logo4Finished = false; }
                        switch (secStep)
                        {
                            case EnumSecStation.SenseAllSensor:
                                {
                                    if (!isCurSecStepAction)
                                    {
                                        isCurSecStepAction = true;
                                        SecStaFisrtStep();
                                    }
                                }
                                break;
                            case EnumSecStation.Labeling:
                                {
                                    if (!isCurSecStepAction)
                                    {
                                        isCurSecStepAction = true;
                                        SecStaSecStep();
                                    }
                                }
                                break;
                            case EnumSecStation.UpDownCylinderOut:
                                {
                                    if (!isCurSecStepAction)
                                    {
                                        isCurSecStepAction = true;
                                        SecStaThrStep();
                                    }
                                }
                                break;
                            case EnumSecStation.NotifyEpsonStickGum:
                                {
                                    if (!isCurSecStepAction)
                                    {
                                        isCurSecStepAction = true;
                                        SecStaFourStep();
                                    }
                                }
                                break;
                            case EnumSecStation.LeftRightCylinderBack:
                                {
                                    if (!isCurSecStepAction)
                                    {
                                        isCurSecStepAction = true;
                                        SecStaFiveStep();
                                    }
                                }
                                break;
                            case EnumSecStation.NotifyEpsonLRCylinderBackIsOk:
                                {
                                    if (!isCurSecStepAction)
                                    {
                                        isCurSecStepAction = true;
                                        SecStaSixStep();
                                    }
                                }
                                break;
                            case EnumSecStation.LeftRightCylinderForce:
                                {
                                    if (!isCurSecStepAction)
                                    {
                                        isCurSecStepAction = true;
                                        SecStaSevenStep();
                                    }
                                }
                                break;
                            case EnumSecStation.UpDownCylinderBack:
                                {
                                    if (!isCurSecStepAction)
                                    {
                                        isCurSecStepAction = true;
                                        SecStaEightStep();
                                    }
                                }
                                break;
                        }
                    }
                    //if (GlobalVar.deviceAlarmIsHappen && LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.LabelingAxiasNumber) == 0)
                    //{
                    //    LTDMC.dmc_stop(GlobalVar.CardId, GlobalVar.LabelingAxiasNumber, 0);
                    //    secStep = secStep - 1;
                    //}
                }
                catch (Exception ex)
                {
                    GlobalVar.myLog.Error($"第二工位运行发生未知错误:{ex.Message}");
                }
                finally
                {
                    Thread.Sleep(10);
                    Application.DoEvents();
                }
            }
        }

        private void RotateCylinderStepMethod()
        {
            bool myTimeOut2 = true;
            CancellationTokenSource myToken2 = new CancellationTokenSource(10000);
            if (isRotatePorN)//正转
            {
                if (!GlobalVar.lsAxiasDIs[12].CurIOStatus && GlobalVar.lsAxiasDIs[13].CurIOStatus)
                {
                    LogAlarmError("旋转气缸正转极限感应器错误");
                    GlobalVar.deviceAlarmIsHappen = true;
                }
                else
                {
                    myToken2.Token.Register(new Action(() =>
                    {
                        if (myTimeOut2)
                        {
                            LogAlarmError("气缸正旋转超时"); GlobalVar.deviceAlarmIsHappen = true;
                        }
                        else
                        {
                            if ((GlobalVar.totalRunFlag || isSecStaTest) && !GlobalVar.deviceSafeDoorIsOpen && !GlobalVar.deviceAlarmIsHappen)
                            {
                                LogMessage("气缸正旋转到位,开始发送信号给EPSON");
                                IOC0640.ioc_write_outbit(Convert.ToUInt16(GlobalVar.lsAxiasDOs[13].Card - 1), GlobalVar.lsAxiasDOs[13].PinDefinition, 0);//补料完成
                                Thread.Sleep(10);
                                IOC0640.ioc_write_outbit(Convert.ToUInt16(GlobalVar.lsAxiasDOs[14].Card - 1), GlobalVar.lsAxiasDOs[14].PinDefinition, 1);//待补料关闭
                                Thread.Sleep(1500);
                                IOC0640.ioc_write_outbit(Convert.ToUInt16(GlobalVar.lsAxiasDOs[25].Card - 1), GlobalVar.lsAxiasDOs[25].PinDefinition, 0);//发送允许取Logo
                                isRotatePorN = false;
                            }
                        }
                        isRobot2 = false;
                    }));
                    IOC0640.ioc_write_outbit(Convert.ToUInt16(GlobalVar.lsAxiasDOs[15].Card - 1), GlobalVar.lsAxiasDOs[15].PinDefinition, 0);
                    Task.Factory.StartNew(new Action(() =>
                    {
                        while (!myToken2.Token.IsCancellationRequested)
                        {
                            Thread.Sleep(5);
                            Application.DoEvents();
                            if (!GlobalVar.lsAxiasDIs[12].CurIOStatus && GlobalVar.lsAxiasDIs[13].CurIOStatus || !(GlobalVar.totalRunFlag || isSecStaTest))
                            { myTimeOut2 = false; myToken2.Cancel(); }
                        }
                    }), myToken2.Token);
                }
            }
            else //反转
            {
                if (GlobalVar.lsAxiasDIs[12].CurIOStatus && !GlobalVar.lsAxiasDIs[13].CurIOStatus)
                {
                    LogAlarmError("旋转气缸反转极限感应器错误");
                    GlobalVar.deviceAlarmIsHappen = true;
                }
                else
                {
                    myToken2.Token.Register(new Action(() =>
                    {
                        if (myTimeOut2)
                        {
                            LogAlarmError("气缸反旋转超时"); GlobalVar.deviceAlarmIsHappen = true;
                        }
                        else
                        {
                            if ((GlobalVar.totalRunFlag || isSecStaTest) && !GlobalVar.deviceSafeDoorIsOpen && !GlobalVar.deviceAlarmIsHappen)
                            {
                                LogMessage("气缸反旋转到位,开始发送信号给EPSON");
                                IOC0640.ioc_write_outbit(Convert.ToUInt16(GlobalVar.lsAxiasDOs[13].Card - 1), GlobalVar.lsAxiasDOs[13].PinDefinition, 0);
                                Thread.Sleep(10);
                                IOC0640.ioc_write_outbit(Convert.ToUInt16(GlobalVar.lsAxiasDOs[14].Card - 1), GlobalVar.lsAxiasDOs[14].PinDefinition, 1);
                                Thread.Sleep(1500);
                                IOC0640.ioc_write_outbit(Convert.ToUInt16(GlobalVar.lsAxiasDOs[25].Card - 1), GlobalVar.lsAxiasDOs[25].PinDefinition, 0);
                                isRotatePorN = true;
                            }
                        }
                        isRobot2 = false;
                    }));
                    IOC0640.ioc_write_outbit(Convert.ToUInt16(GlobalVar.lsAxiasDOs[15].Card - 1), GlobalVar.lsAxiasDOs[15].PinDefinition, 1);
                    Task.Factory.StartNew(new Action(() =>
                    {
                        while (!myToken2.Token.IsCancellationRequested)
                        {
                            Thread.Sleep(5);
                            Application.DoEvents();
                            if (GlobalVar.lsAxiasDIs[12].CurIOStatus && !GlobalVar.lsAxiasDIs[13].CurIOStatus || !(GlobalVar.totalRunFlag || isSecStaTest))
                            { myTimeOut2 = false; myToken2.Cancel(); }
                        }
                    }), myToken2.Token);
                }
            }
        }

        private void LabelPressCylinderDownMethod()
        {
            bool myTimeOut2 = true;
            CancellationTokenSource myToken2 = new CancellationTokenSource(10000);
            myToken2.Token.Register(new Action(() =>
            {
                if (myTimeOut2)
                {
                    IOC0640.ioc_write_outbit(Convert.ToUInt16(GlobalVar.lsAxiasDOs[24].Card - 1), GlobalVar.lsAxiasDOs[24].PinDefinition, 1);
                    if (GlobalVar.lsAxiasDIs[26].CurIOStatus)
                    {
                        LogAlarmError("贴Logo按压气缸上极限状态错误"); GlobalVar.deviceAlarmIsHappen = true;
                    }
                    if (!GlobalVar.lsAxiasDIs[27].CurIOStatus)
                    {
                        LogAlarmError("贴Logo按压气缸到位下极限状态错误"); GlobalVar.deviceAlarmIsHappen = true;
                    }
                }
                else
                {
                    if ((GlobalVar.totalRunFlag || isSecStaTest) && !GlobalVar.deviceSafeDoorIsOpen && !GlobalVar.deviceAlarmIsHappen)
                    {
                        LogMessage("贴Logo按压气缸到位,开始发送信号允许EPSON过来贴标");
                        // GlobalVar.stationIsShot = true;
                        secStationIsReadyOk = true;
                        Thread.Sleep(50);
                        IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[28].Card - 1), GlobalVar.lsAxiasDOs[28].PinDefinition, 0);//允许贴表信号ON
                    }
                }
            }));

            Task.Factory.StartNew(new Action(() =>
            {
                while (!myToken2.Token.IsCancellationRequested)
                {
                    Thread.Sleep(5);
                    Application.DoEvents();
                    if (!GlobalVar.lsAxiasDIs[26].CurIOStatus && GlobalVar.lsAxiasDIs[27].CurIOStatus || !(GlobalVar.totalRunFlag || isSecStaTest))
                    { myTimeOut2 = false; myToken2.Cancel(); }
                }
            }), myToken2.Token);
            //}
        }

        private void LabelPressCylinderUPMethod()
        {
            bool myTimeOut2 = true;
            CancellationTokenSource myToken2 = new CancellationTokenSource(10000);
            myToken2.Token.Register(new Action(() =>
            {
                if (myTimeOut2)
                {
                    IOC0640.ioc_write_outbit(Convert.ToUInt16(GlobalVar.lsAxiasDOs[24].Card - 1), GlobalVar.lsAxiasDOs[24].PinDefinition, 1);
                    if (!GlobalVar.lsAxiasDIs[26].CurIOStatus)
                    {
                        LogAlarmError("贴Logo按压气缸上极限状态错误"); GlobalVar.deviceAlarmIsHappen = true;
                    }
                    if (GlobalVar.lsAxiasDIs[27].CurIOStatus)
                    {
                        LogAlarmError("贴Logo按压气缸到位下极限状态错误"); GlobalVar.deviceAlarmIsHappen = true;
                    }
                }
                else
                {
                    LogMessage("允许凸轮旋转"); secStationIsReadyOk = false;
                }
                isRobot1 = false;
            }));
            IOC0640.ioc_write_outbit(Convert.ToUInt16(GlobalVar.lsAxiasDOs[24].Card - 1), GlobalVar.lsAxiasDOs[24].PinDefinition, 1);//tieLogo气缸复位
            Thread.Sleep(10);
            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[28].Card - 1), GlobalVar.lsAxiasDOs[28].PinDefinition, 1);//允许Epson贴标信号复位
            Console.WriteLine("test2");
            Task.Factory.StartNew(new Action(() =>
            {
                while (!myToken2.Token.IsCancellationRequested)
                {
                    Thread.Sleep(5);
                    Application.DoEvents();
                    if (GlobalVar.lsAxiasDIs[26].CurIOStatus && !GlobalVar.lsAxiasDIs[27].CurIOStatus || !(GlobalVar.totalRunFlag || isSecStaTest))
                    { myTimeOut2 = false; myToken2.Cancel(); }
                }
            }), myToken2.Token);
        }

        private void DoSecMyAction(int timeout, string timeoutMessage, Action timeoutAct, string nextStepMessage, Action nextAct, Action curAct, Func<bool> breakWhile)
        {
            bool myTimeOut = true;
            CancellationTokenSource myToken = new CancellationTokenSource(timeout);
            myToken.Token.Register(new Action(() =>
            {
                if (myTimeOut)
                {
                    //LogError(timeoutMessage);
                    if (timeoutAct != null)
                    {
                        timeoutAct.Invoke();
                    }
                }
                else
                {
                    LogMessage(nextStepMessage);
                    if (nextAct != null)
                    {
                        if (secStep == EnumSecStation.Labeling)
                        {
                            secStep++;
                            nextAct.Invoke();
                        }
                        else
                        {
                            if ((GlobalVar.totalRunFlag || isSecStaTest) && !GlobalVar.deviceSafeDoorIsOpen && !GlobalVar.deviceAlarmIsHappen && !isManualPause && !GlobalVar.totalRunIsEnded)
                            {
                                secStep++;
                                nextAct.Invoke();
                            }
                        }
                    }
                }
                isCurSecStepAction = false;
            }));
            if (curAct != null && (GlobalVar.totalRunFlag || isSecStaTest) && !GlobalVar.deviceSafeDoorIsOpen && !GlobalVar.deviceAlarmIsHappen && !isManualPause && !GlobalVar.totalRunIsEnded)
            {
                curAct.Invoke();
            }
            Task.Factory.StartNew(new Action(() =>
            {
                while (!myToken.Token.IsCancellationRequested)
                {
                    Thread.Sleep(5);
                    Application.DoEvents();

                    if (breakWhile.Invoke() || !(GlobalVar.totalRunFlag || isSecStaTest))
                    {
                        myTimeOut = false; myToken.Cancel();
                    }
                }
            }), myToken.Token);
        }

        #region 出标机步骤
        private void SecStaSecStep()
        {
            #region 第二步
            DoSecMyAction(10000, "出标信号状态侦测超时",
            new Action(() => { 
                //secStep = secStep - 1;
                if (isLabelOut)
                {
                    isLabelOut = false;
                    LTDMC.dmc_stop(GlobalVar.CardId, GlobalVar.LabelingAxiasNumber, 0);
                    LogAlarmError("出标轴出标异常，请检查出标轴是否能转动");
                    GlobalVar.deviceAlarmIsHappen = true;
                }
            }),//视后续是否回出现一直出标，如果出现就再此步骤加上停止出标
            "侦测到出标信号，停止出标，下一步控制下气缸下降",
            new Action(() =>
            {
                LTDMC.dmc_stop(GlobalVar.CardId, GlobalVar.LabelingAxiasNumber, 0);

                //出标上下气缸动作，第一步已经侦测在此不需要了
                IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[18].Card - 1), GlobalVar.lsAxiasDOs[18].PinDefinition, 0);
            }),
            null,
            () =>
            {
                if ((GlobalVar.lsAxiasDIs[38].CurIOStatus || GlobalVar.firAndSecIsShot) && !isLabelOut && isStartBtnResponse)//侦测允许出标信号
                {
                    LTDMC.dmc_vmove(GlobalVar.CardId, GlobalVar.LabelingAxiasNumber, 0);
                    Thread.Sleep(300);
                    isLabelOut = true;
                    return false;
                }
                if (isLabelOut)
                {
                    GlobalVar.labelSensorFallEdge.CLK = GlobalVar.lsAxiasDIs[40].CurIOStatus;
                }
                if (GlobalVar.labelSensorFallEdge.Q && isLabelOut && LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.LabelingAxiasNumber) == 0)// 【2021.12.02】 用出标感应器信号的上升沿信号
                {
                    isLabelOut = false;
                    return true;
                }
                else
                { return false; }
            }
            );
            #endregion
        }
        private void SecStaFisrtStep()
        {
            #region 第一步
            DoSecMyAction(cylinderSensorTimeOut, "上下左右气缸初始状态侦测超时",
            new Action(() =>
            {
                secStep = EnumSecStation.SenseAllSensor;

                if (!GlobalVar.lsAxiasDIs[16].CurIOStatus && GlobalVar.lsAxiasDIs[17].CurIOStatus)//如果上电磁阀处于下状态，复位
                {
                    IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[18].Card - 1), GlobalVar.lsAxiasDOs[18].PinDefinition, 1);
                }
                if (!GlobalVar.lsAxiasDIs[18].CurIOStatus && GlobalVar.lsAxiasDIs[19].CurIOStatus)//如果上电磁阀处于下状态，复位
                {
                    IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[19].Card - 1), GlobalVar.lsAxiasDOs[19].PinDefinition, 1);
                }
                if (GlobalVar.lsAxiasDOs[27].CurIOStatus)//复位给Epson气缸回退到位信号
                {
                    IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[27].Card - 1), GlobalVar.lsAxiasDOs[27].PinDefinition, 1);
                }
                if (GlobalVar.lsAxiasDOs[26].CurIOStatus)//复位给Epson允许贴背胶信号
                {
                    IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[26].Card - 1), GlobalVar.lsAxiasDOs[26].PinDefinition, 1);
                }
                GlobalVar.isFirAndSecIsShotOK = false;
            }),
            "初始出标2个气缸状态正确,下一步等待Epson允许出标信号",
            new Action(() => { GlobalVar.isFirAndSecIsShotOK = false; }),
            null,
            () => GlobalVar.lsAxiasDIs[16].CurIOStatus && !GlobalVar.lsAxiasDIs[17].CurIOStatus && GlobalVar.lsAxiasDIs[18].CurIOStatus && !GlobalVar.lsAxiasDIs[19].CurIOStatus);
            #endregion
        }
        private void SecStaThrStep()
        {
            #region 第三步
            DoSecMyAction(axiasMoveTimeOut, "气缸打出到位超时",
            new Action(() => { 
                //IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[18].Card - 1), GlobalVar.lsAxiasDOs[18].PinDefinition, 1);
                //secStep = secStep - 1; 
            }),
            "气缸下降到位，下一步发送允许贴背胶信号给机器人",
            new Action(() =>
            {
                if (!GlobalVar.isFirAndSecIsShotOK)
                {
                    GlobalVar.isFirAndSecIsShotOK = true;
                    if (GlobalVar.labelIsContinue)
                    {
                        LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.LabelingAxiasNumber, GlobalVar.labelContinuePlues, 0);
                        while (LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.LabelingAxiasNumber) == 0)
                        {
                            Application.DoEvents();
                            Thread.Sleep(10);
                        }
                    }
                }
                if (GlobalVar.firAndSecShotNGResetCount > 0 && GlobalVar.firAndSecIsShot && !GlobalVar.firAndSecIsAllowShot)
                {
                    GlobalVar.firAndSecIsShot = false;
                    GlobalVar.firAndSecIsAllowShot = true;
                }
                //发送允许贴背胶信号,机器人过来先拍照
                IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[26].Card - 1), GlobalVar.lsAxiasDOs[26].PinDefinition, 0);
            }),
            null,
            () => !GlobalVar.lsAxiasDIs[16].CurIOStatus && GlobalVar.lsAxiasDIs[17].CurIOStatus && LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.LabelingAxiasNumber) == 1);
            #endregion
        }
        private void SecStaFourStep()
        {
            #region 第四步
            DoSecMyAction(10000, "侦测Epsono给出的撕标信号超时",
            new Action(() =>
            {
                if (GlobalVar.firAndSecShotNGResetCount > 0 && !GlobalVar.firAndSecIsShot && !GlobalVar.firAndSecIsAllowShot)
                {
                    GlobalVar.firAndSecIsShot = true;
                    IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[18].Card - 1), GlobalVar.lsAxiasDOs[18].PinDefinition, 1);//下压气缸退回
                    Thread.Sleep(500);
                    secStep = EnumSecStation.SenseAllSensor;
                    GlobalVar.isFirAndSecIsShotOK = true;
                }
                else
                {
                    //secStep = secStep - 1;
                }
            }),
            "收到Epson撕标信号，下一步发送通知气缸后退",
            new Action(() =>
            {
                if (GlobalVar.lsAxiasDOs[27].CurIOStatus)
                {
                    IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[27].Card - 1), GlobalVar.lsAxiasDOs[27].PinDefinition, 1);
                }
                if ((GlobalVar.totalRunFlag || isSecStaTest) && !GlobalVar.deviceSafeDoorIsOpen && !GlobalVar.deviceAlarmIsHappen)
                {
                    //关闭允许贴背胶信号
                    IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[26].Card - 1), GlobalVar.lsAxiasDOs[26].PinDefinition, 1);
                    Thread.Sleep(10);
                    //撕标前进后退气缸动作
                    IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[19].Card - 1), GlobalVar.lsAxiasDOs[19].PinDefinition, 0);
                }
            }),
           null,
            //() => GlobalVar.lsAxiasDIs[37].CurIOStatus || secStep == EnumSecStation.SenseAllSensor);//侦测Epson给出的撕标命令
            () => GlobalVar.lsAxiasDIs[37].CurIOStatus);//侦测Epson给出的撕标命令
            #endregion
        }
        private void SecStaFiveStep()
        {
            #region 第五步
            DoSecMyAction(cylinderSensorTimeOut, "撕标气缸后退超时",
            new Action(() => { 
                //IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[19].Card - 1), GlobalVar.lsAxiasDOs[19].PinDefinition, 1);
                //secStep = secStep - 1;
            }),
            "撕标气缸后退到位，下一步通知Epson撕标完成",
            new Action(() =>
            {
                //通知Epson撕标完成，Epson可以去往下一工位
                Thread.Sleep(100);
                //通知Epson出标气缸回退到位(告知机器人贴背胶完成)
                IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[27].Card - 1), GlobalVar.lsAxiasDOs[27].PinDefinition, 0);
            }),
            null,
            () => !GlobalVar.lsAxiasDIs[18].CurIOStatus && GlobalVar.lsAxiasDIs[19].CurIOStatus && !GlobalVar.lsAxiasDIs[16].CurIOStatus && GlobalVar.lsAxiasDIs[17].CurIOStatus);//侦测Epson给出的撕标命令
            #endregion
        }
        private void SecStaSixStep()
        {
            #region 第六步
            DoSecMyAction(cylinderSensorTimeOut, "通知Espson撕标信号异常",
            new Action(() => { 
                //IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[19].Card - 1), GlobalVar.lsAxiasDOs[19].PinDefinition, 1); secStep = secStep - 1; 
            }),
            "通知Espson撕标信号OK，下一步左右气缸向前退回",
            new Action(() =>
            {
                Thread.Sleep(10);
                IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[27].Card - 1), GlobalVar.lsAxiasDOs[27].PinDefinition, 1);//复位通知Epson撕标完成
                Thread.Sleep(10);
                IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[19].Card - 1), GlobalVar.lsAxiasDOs[19].PinDefinition, 1);//出标气缸复位退回
                Thread.Sleep(300);
            }),
            null,
            () => !GlobalVar.lsAxiasDIs[18].CurIOStatus && GlobalVar.lsAxiasDIs[19].CurIOStatus && GlobalVar.lsAxiasDIs[38].CurIOStatus);//侦测Epson给出的撕标命令
            //() => !GlobalVar.lsAxiasDIs[18].CurIOStatus && GlobalVar.lsAxiasDIs[19].CurIOStatus);//侦测Epson给出的撕标命令
            #endregion
        }
        private void SecStaSevenStep()
        {
            #region 第七步
            DoSecMyAction(cylinderSensorTimeOut, "左右气缸向前打出超时",
            new Action(() => { 
                //IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[19].Card - 1), GlobalVar.lsAxiasDOs[19].PinDefinition, 1); 
                //secStep = secStep - 1;
            }),
            "左右气缸向前打出到位，下一步上下气缸退回",
            new Action(() =>
            {
                //下压气缸退回
                IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[18].Card - 1), GlobalVar.lsAxiasDOs[18].PinDefinition, 1);
            }),
            null,
            () => GlobalVar.lsAxiasDIs[18].CurIOStatus && !GlobalVar.lsAxiasDIs[19].CurIOStatus);//出标气缸左右极限状态
            #endregion
        }
        private void SecStaEightStep()
        {
            #region 第八步
            DoSecMyAction(cylinderSensorTimeOut, "上下气缸退回超时",
            new Action(() => { 
                //IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[18].Card - 1), GlobalVar.lsAxiasDOs[18].PinDefinition, 1); secStep = secStep - 1;
            }),
            "上下气缸退回到位，下一步开始出标",
            null,
            null,
            () => GlobalVar.lsAxiasDIs[16].CurIOStatus && !GlobalVar.lsAxiasDIs[17].CurIOStatus);//出标上下极限状态
            #endregion
        }
        #endregion 

        #endregion

        #region 第三工位
        private void DoThirdStaCycleAction()
        {
            while (GlobalVar.totalRunFlagVision)
            {
                try
                {
                    if (GlobalVar.totalRunFlag && !GlobalVar.deviceSafeDoorIsOpen && !GlobalVar.deviceAlarmIsHappen && !isManualPause)
                    {
                        if (GlobalVar.lsAxiasDIs[32].CurIOStatus && !GlobalVar.lsAxiasDIs[41].CurIOStatus && !thrStationIsReadyOk && isAllowThrStaTest)
                        {
                            thrStationIsReadyOk = true;
                            isAllowThrStaTest = false;
                        }
                        if (thrStep == EnumThrStation.CylinderUp && isAllowThrStaTest)
                        {
                            thrStep = EnumThrStation.SenseAllSensor;
                        }
                        switch (thrStep)
                        {
                            case EnumThrStation.SenseAllSensor:
                                {
                                    if (!isCurThrStepAction)
                                    {
                                        isCurThrStepAction = true;
                                        ThrStaFisrtStep();
                                    }
                                }
                                break;
                            case EnumThrStation.CylinderDown:
                                {
                                    if (!isCurThrStepAction)
                                    {
                                        isCurThrStepAction = true;
                                        ThrStaSecStep();
                                    }
                                }
                                break;
                            case EnumThrStation.PressIsArrived:
                                {
                                    if (!isCurThrStepAction)
                                    {
                                        isCurThrStepAction = true;
                                        ThrStaThrStep();
                                    }
                                }
                                break;
                            case EnumThrStation.CylinderUp:
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    GlobalVar.myLog.Error($"第三工位运行发生未知错误:{ex.Message}");
                }
                finally
                {
                    Thread.Sleep(10);
                    Application.DoEvents();
                }
            }
        }
        private void ThrStaFisrtStep()
        {
            #region 第一步
            DoThrMyAction(cylinderSensorTimeOut, "上下压合气缸初始状态侦测超时",
            new Action(() =>
            {
                
                #region 2022-03-30 薛 增加压合气缸未到位的报警提示功能
                if(GlobalVar.lsAxiasDIs[20].CurIOStatus && GlobalVar.lsAxiasDIs[21].CurIOStatus)
                {
                    LogAlarmError("压合气缸上下极限感应器同时为ON，信号异常请排查");
                    GlobalVar.deviceAlarmIsHappen = true;
                }

                if (!GlobalVar.lsAxiasDIs[20].CurIOStatus && !GlobalVar.lsAxiasDIs[21].CurIOStatus)
                {
                    LogAlarmError("压合气缸上下极限感应器同时为OFF，信号异常请排查");
                    GlobalVar.deviceAlarmIsHappen = true;
                }

                //if (!GlobalVar.lsAxiasDIs[32].CurIOStatus && !GlobalVar.totalRunIsEnded )
                //{
                //    LogAlarmError("压合工位料感信号未感应到，信号异常请排查");
                //    GlobalVar.deviceAlarmIsHappen = true;
                //}
                #endregion

                if (!GlobalVar.lsAxiasDIs[20].CurIOStatus && GlobalVar.lsAxiasDIs[21].CurIOStatus)//如果电磁阀处于下状态，复位
                {
                    IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[20].Card - 1), GlobalVar.lsAxiasDOs[20].PinDefinition, 1);
                }
                thrStep = EnumThrStation.SenseAllSensor;

            }),
            "压合气缸初始状态正确,下一步开始打出气缸，并保压",
            new Action(() =>
            {
                if (!GlobalVar.lsAxiasDIs[41].CurIOStatus)
                {
                    IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[20].Card - 1), GlobalVar.lsAxiasDOs[20].PinDefinition, 0);
                }
            }),
            null,
            () => GlobalVar.lsAxiasDIs[20].CurIOStatus && !GlobalVar.lsAxiasDIs[21].CurIOStatus && GlobalVar.lsAxiasDIs[32].CurIOStatus && !GlobalVar.lsAxiasDIs[41].CurIOStatus);
            #endregion
        }
        private void ThrStaSecStep()
        {
            #region 第二步
            DoThrMyAction(cylinderSensorTimeOut, "上下压合气缸打出超时",
            new Action(() =>
            {
                #region 2022-03-30 薛 增加压合气缸未到位的报警提示功能
                if (GlobalVar.lsAxiasDIs[20].CurIOStatus && GlobalVar.lsAxiasDIs[21].CurIOStatus)
                {
                    LogAlarmError("压合气缸上下极限感应器同时为ON，信号异常请排查");
                    GlobalVar.deviceAlarmIsHappen = true;
                }

                if (!GlobalVar.lsAxiasDIs[20].CurIOStatus && !GlobalVar.lsAxiasDIs[21].CurIOStatus)
                {
                    LogAlarmError("压合气缸上下极限感应器同时为OFF，信号异常请排查");
                    GlobalVar.deviceAlarmIsHappen = true;
                }
                #endregion

                IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[20].Card - 1), GlobalVar.lsAxiasDOs[20].PinDefinition, 1);
                pressCnt = 0;
                startPress = false;
                thrStep = thrStep - 1;
            }),
            "保压时间到,下一步开始退回气缸",
            new Action(() =>
            {
                pressCnt = 0;
                startPress = false;
                IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[20].Card - 1), GlobalVar.lsAxiasDOs[20].PinDefinition, 1);
            }),
            null,
            () =>
            {
                if (!GlobalVar.isDivCamIsRotating && startPress && !GlobalVar.lsAxiasDIs[20].CurIOStatus && GlobalVar.lsAxiasDIs[21].CurIOStatus && !GlobalVar.lsAxiasDIs[41].CurIOStatus)
                { return true; }
                else
                {
                    //持续保压
                    if (pressCnt >= GlobalVar.pressDelayTime * 1000 / 5)
                    {
                        startPress = true;
                    }
                    pressCnt++;
                    return false;
                }
            });
            #endregion
        }
        private void ThrStaThrStep()
        {
            #region 第三步
            DoThrMyAction(cylinderSensorTimeOut, "上下压合气缸退回超时",
            new Action(() =>
            {
                if (!GlobalVar.lsAxiasDIs[20].CurIOStatus)//如果电磁阀处于下状态，复位
                {
                    LogAlarmError("压合气缸上极限感应器状态错误"); GlobalVar.deviceAlarmIsHappen = true;
                }
                if (GlobalVar.lsAxiasDIs[21].CurIOStatus)
                {
                    LogAlarmError("压合气缸下极限感应器状态错误"); GlobalVar.deviceAlarmIsHappen = true;
                }
                IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[20].Card - 1), GlobalVar.lsAxiasDOs[20].PinDefinition, 1);
                Thread.Sleep(500);
                thrStationIsReadyOk = false;
                thrStep = thrStep - 1;
            }),
            "压合气缸退回到位,下一步提示系统，可以旋转凸轮分割器",
            new Action(() =>
            {
                thrStationIsReadyOk = false;
            }),
            null,
            () => GlobalVar.lsAxiasDIs[20].CurIOStatus && !GlobalVar.lsAxiasDIs[21].CurIOStatus && GlobalVar.lsAxiasDIs[32].CurIOStatus && !GlobalVar.lsAxiasDIs[41].CurIOStatus);
            #endregion
        }
        private void DoThrMyAction(int timeout, string timeoutMessage, Action timeoutAct, string nextStepMessage, Action nextAct, Action curAct, Func<bool> breakWhile)
        {
            bool myTimeOut = true;
            CancellationTokenSource myToken = new CancellationTokenSource(timeout);
            myToken.Token.Register(new Action(() =>
            {
                if (myTimeOut)
                {
                    //LogError(timeoutMessage);
                    if (timeoutAct != null)
                    {
                        timeoutAct.Invoke();
                    }
                }
                else
                {
                    LogMessage(nextStepMessage);
                    if (nextAct != null && GlobalVar.totalRunFlag && !GlobalVar.deviceSafeDoorIsOpen && !GlobalVar.deviceAlarmIsHappen && !isManualPause)
                    {
                        nextAct.Invoke();
                        thrStep++;
                    }
                }
                isCurThrStepAction = false;
            }));
            if (curAct != null && GlobalVar.totalRunFlag && !GlobalVar.deviceSafeDoorIsOpen && !GlobalVar.deviceAlarmIsHappen && !isManualPause)
            {
                curAct.Invoke();
            }
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

        #endregion

        #region 第四工位
        private void DoFourStaCycleAction()
        {
            while (GlobalVar.totalRunFlagVision)
            {
                try
                {
                    if (GlobalVar.totalRunFlag && !GlobalVar.deviceSafeDoorIsOpen && !GlobalVar.deviceAlarmIsHappen && !isManualPause)
                    {
                        if (GlobalVar.lsAxiasDIs[33].CurIOStatus && !GlobalVar.lsAxiasDIs[41].CurIOStatus && !fourStationIsReadyOk && isAllowFourStaTest)
                        {
                            isAllowFourStaTest = false;
                            fourStationIsReadyOk = true;
                        }
                        if (fourStep == EnumFourStation.CylinderUp && fourStationIsReadyOk)
                        {
                            fourStep = EnumFourStation.SenseAllSensor;
                        }
                        switch (fourStep)
                        {
                            case EnumFourStation.SenseAllSensor:
                                {
                                    if (!isCurFourStepAction)
                                    {
                                        isCurFourStepAction = true;
                                        FourStaFisrtStep();
                                    }
                                }
                                break;
                            case EnumFourStation.CylinderDown:
                                {
                                    if (!isCurFourStepAction)
                                    {
                                        isCurFourStepAction = true;
                                        FourStaSecStep();
                                    }
                                }
                                break;
                            case EnumFourStation.PressIsArrived:
                                {
                                    if (!isCurFourStepAction)
                                    {
                                        isCurFourStepAction = true;
                                        FourStaThrStep();
                                    }
                                }
                                break;
                            case EnumFourStation.CylinderUp:
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    GlobalVar.myLog.Error($"第四工位运行发生未知错误:{ex.Message}");
                }
                finally
                {
                    Thread.Sleep(10);
                    Application.DoEvents();
                }
            }
        }

        private void FourStaFisrtStep()
        {
            #region 第一步
            DoFourMyAction(cylinderSensorTimeOut, "上下测高气缸初始状态侦测超时",
            new Action(() =>
            {
                
                #region 2022-03-30 薛 增加测高气缸未到位的报警提示功能
                if (GlobalVar.lsAxiasDIs[22].CurIOStatus && GlobalVar.lsAxiasDIs[23].CurIOStatus)
                {
                    LogAlarmError("测高气缸上下极限感应器同时为ON，信号异常请排查");
                    GlobalVar.deviceAlarmIsHappen = true;
                }

                if (!GlobalVar.lsAxiasDIs[22].CurIOStatus && !GlobalVar.lsAxiasDIs[23].CurIOStatus)
                {
                    LogAlarmError("测高气缸上下极限感应器同时为OFF，信号异常请排查");
                    GlobalVar.deviceAlarmIsHappen = true;
                }

                //if (!GlobalVar.lsAxiasDIs[33].CurIOStatus && !GlobalVar.totalRunIsEnded)
                //{
                //    LogAlarmError("测高工位料感信号未感应到，信号异常请排查");
                //    GlobalVar.deviceAlarmIsHappen = true;
                //}

                #endregion

                if (!GlobalVar.lsAxiasDIs[22].CurIOStatus && GlobalVar.lsAxiasDIs[23].CurIOStatus)//如果电磁阀处于下状态，复位
                {
                    IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[21].Card - 1), GlobalVar.lsAxiasDOs[21].PinDefinition, 1);
                }


            }),
            "初始测高气缸状态正确,下一步开始打出气缸，并保压",
            new Action(() =>
            {
                if (!GlobalVar.lsAxiasDIs[41].CurIOStatus)
                {
                    IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[21].Card - 1), GlobalVar.lsAxiasDOs[21].PinDefinition, 0);
                }
            }),
            null,
            () => GlobalVar.lsAxiasDIs[22].CurIOStatus && !GlobalVar.lsAxiasDIs[23].CurIOStatus && GlobalVar.lsAxiasDIs[33].CurIOStatus && !GlobalVar.lsAxiasDIs[41].CurIOStatus);
            #endregion
        }

        private void FourStaSecStep()
        {
            #region 第二步
            DoFourMyAction(cylinderSensorTimeOut, "上下测高气缸打出超时",
            new Action(() =>
            {
                #region 2022-03-30 薛 增加测高气缸未到位的报警提示功能
                if (GlobalVar.lsAxiasDIs[22].CurIOStatus && GlobalVar.lsAxiasDIs[23].CurIOStatus)
                {
                    LogAlarmError("压合气缸上下极限感应器同时为ON，信号异常请排查");
                    GlobalVar.deviceAlarmIsHappen = true;
                }

                if (!GlobalVar.lsAxiasDIs[22].CurIOStatus && !GlobalVar.lsAxiasDIs[23].CurIOStatus)
                {
                    LogAlarmError("压合气缸上下极限感应器同时为OFF，信号异常请排查");
                    GlobalVar.deviceAlarmIsHappen = true;
                }
                #endregion

                IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[21].Card - 1), GlobalVar.lsAxiasDOs[21].PinDefinition, 1);
                pressHighCnt = 0;
                startHighPress = false;
                fourStep = fourStep - 1;
            }),
            "测高时间到,下一步开始退回气缸",
            new Action(() =>
            {
                pressHighCnt = 0;
                startHighPress = false;
                IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[21].Card - 1), GlobalVar.lsAxiasDOs[21].PinDefinition, 1);
                GlobalVar.isAoiLastTestPro = true;
            }),
            null,
            () =>
            {
                if (!GlobalVar.isDivCamIsRotating && startHighPress && !GlobalVar.lsAxiasDIs[22].CurIOStatus && GlobalVar.lsAxiasDIs[23].CurIOStatus && !GlobalVar.lsAxiasDIs[41].CurIOStatus)
                { return true; }
                else
                {
                    //持续保压
                    if (pressHighCnt >= GlobalVar.highIndInitDelayTime * 1000 / 10)
                    {
                        if (dataList.Count == 0)
                        {
                            LogError("模组数据没有采集到");
                        }
                        startHighPress = true;
                    }
                    else
                    {
                        pressHighCnt++;
                    }
                    return false;
                }
            });
            #endregion
        }

        private void FourStaThrStep()
        {
            #region 第三步
            DoFourMyAction(cylinderSensorTimeOut, "上下测高气缸退回超时",
            new Action(() =>
            {
                if (!GlobalVar.lsAxiasDIs[22].CurIOStatus)//如果电磁阀处于下状态，复位
                {
                    LogAlarmError("测高气缸上极限感应器状态错误"); GlobalVar.deviceAlarmIsHappen = true;
                }
                if (GlobalVar.lsAxiasDIs[23].CurIOStatus)
                {
                    LogAlarmError("测高气缸下极限感应器状态错误"); GlobalVar.deviceAlarmIsHappen = true;
                }
                IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[21].Card - 1), GlobalVar.lsAxiasDOs[21].PinDefinition, 1);
                Thread.Sleep(500);
                fourStationIsReadyOk = false;
                fourStep = fourStep - 1;
            }),
            "测高气缸退回到位,下一步提示系统，可以旋转凸轮分割器",
            new Action(() =>
            {
                fourStationIsReadyOk = false;
                //凸轮分割器开始旋转
                LogMessage("凸轮分割器等待旋转命令");
                try
                {
                    while (dataList.Count==0)
                    {
                        Application.DoEvents();
                    }
                    GlobalVar.highIndRealHighVal = dataList.Max() - GlobalVar.highIndInitHighVal + GlobalVar.highIndSpaceHigh;
                    dataList.Clear();
                    LogMessage(GlobalVar.highIndRealHighVal.ToString("f4"));
                    if (GlobalVar.highIndRealHighVal < GlobalVar.highIndMaxVal && GlobalVar.highIndRealHighVal > GlobalVar.highIndMinVal)
                    {
                        //OK
                        GlobalVar.highDatas.Add(new HighData()
                        {
                            HightValue = GlobalVar.highIndRealHighVal.ToString(),
                            HightRes = "PASS",
                            HightTestDt = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                        });
                        this.Invoke(new Action(() =>
                        {
                            txtBoxHighVal.Text = GlobalVar.highIndRealHighVal.ToString();
                            picHighRes.Image = Properties.Resources.OK;
                        }));
                    }
                    else
                    {
                        //NG
                        GlobalVar.highDatas.Add(new HighData()
                        {
                            HightValue = GlobalVar.highIndRealHighVal.ToString(),
                            HightRes = "FAIL",
                            HightTestDt = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                        });
                        this.Invoke(new Action(() =>
                        {
                            txtBoxHighVal.Text = GlobalVar.highIndRealHighVal.ToString();
                            picHighRes.Image = Properties.Resources.NG;
                        }));
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }),
            null,
            () => GlobalVar.lsAxiasDIs[22].CurIOStatus && !GlobalVar.lsAxiasDIs[23].CurIOStatus && GlobalVar.lsAxiasDIs[33].CurIOStatus && !GlobalVar.lsAxiasDIs[41].CurIOStatus);
            #endregion
        }

        private double GatherDataing()
        {
            double resD = 0;
            ushort[] res = HighTest.ModbusSerialRtuMasterReadRegisters(GlobalVar.highIndCom);//目前只采集一次看数据是否准确
            if (res.Length == 2)
            {
                if (res[1] != 65535)
                {
                    resD = res[0] / 10000d;
                }
                else
                {
                    resD = (res[0] - 65535) / 10000d;
                }
                LogMessage(resD.ToString("f4"));
            }
            return resD;
        }

        private void DoFourMyAction(int timeout, string timeoutMessage, Action timeoutAct, string nextStepMessage, Action nextAct, Action curAct, Func<bool> breakWhile)
        {
            bool myTimeOut = true;
            CancellationTokenSource myToken = new CancellationTokenSource(timeout);
            myToken.Token.Register(new Action(() =>
            {
                if (myTimeOut)
                {
                    //LogError(timeoutMessage);
                    if (timeoutAct != null)
                    {
                        timeoutAct.Invoke();
                    }
                }
                else
                {
                    LogMessage(nextStepMessage);
                    if (nextAct != null && GlobalVar.totalRunFlag && !GlobalVar.deviceSafeDoorIsOpen && !GlobalVar.deviceAlarmIsHappen && !isManualPause)
                    {
                        nextAct.Invoke();
                        fourStep++;
                    }
                }
                isCurFourStepAction = false;
            }));
            if (curAct != null && GlobalVar.totalRunFlag && !GlobalVar.deviceSafeDoorIsOpen && !GlobalVar.deviceAlarmIsHappen && !isManualPause)
            {
                curAct.Invoke();
            }
            Task.Factory.StartNew(new Action(() =>
            {
                while (!myToken.Token.IsCancellationRequested)
                {
                    Thread.Sleep(10);
                    Application.DoEvents();

                    if (breakWhile.Invoke())
                    {
                        myTimeOut = false; myToken.Cancel();
                    }
                }
            }), myToken.Token);
        }

        #endregion

        #region 第六工位
        private void DoSixStaCycleAction()
        {
            while (GlobalVar.totalRunFlagVision)
            {
                try
                {
                    if (GlobalVar.totalRunFlag && !GlobalVar.deviceSafeDoorIsOpen && !GlobalVar.deviceAlarmIsHappen && !isManualPause)
                    {
                        if (GlobalVar.lsAxiasDIs[35].CurIOStatus && !GlobalVar.lsAxiasDIs[41].CurIOStatus && !sixStationisReadyOk && isAllowSixStaTest)
                        {
                            isAllowSixStaTest = false;
                            sixStationisReadyOk = true;
                        }
                        switch (sixStep)
                        {
                            case EnumSixStation.MoveToFeedPos:
                                {
                                    if (!isCurSixStepAction)
                                    {
                                        isCurSixStepAction = true;
                                        SixStaFisrtStep();
                                    }
                                }
                                break;
                            case EnumSixStation.CylinderDown:
                                {
                                    if (!isCurSixStepAction)
                                    {
                                        isCurSixStepAction = true;
                                        SixStaSecStep();
                                    }
                                }
                                break;
                            case EnumSixStation.DetectVaccSensor:
                                {
                                    if (!isCurSixStepAction)
                                    {
                                        isCurSixStepAction = true;
                                        SixStaThrStep();
                                    }
                                }
                                break;
                            case EnumSixStation.CylinderUp:
                                {
                                    if (!isCurSixStepAction)
                                    {
                                        isCurSixStepAction = true;
                                        SixStaFourStep();
                                    }
                                }
                                break;
                            case EnumSixStation.MoveToOkOrNgPos:
                                {
                                    if (!isCurSixStepAction)
                                    {
                                        isCurSixStepAction = true;
                                        SixStaFiveStep();
                                    }
                                }
                                break;
                            case EnumSixStation.CylinderTakeDown:
                                {
                                    if (!isCurSixStepAction)
                                    {
                                        isCurSixStepAction = true;
                                        SixStaSixStep();
                                    }
                                }
                                break;
                            case EnumSixStation.CylinderTakeUp:
                                {
                                    if (GlobalVar.lsAxiasDIs[24].CurIOStatus && !GlobalVar.lsAxiasDIs[25].CurIOStatus)
                                    {
                                        sixStep = EnumSixStation.MoveToFeedPos;
                                    }
                                }
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    GlobalVar.myLog.Error($"第六工位运行发生未知错误:{ex.Message}");
                }
                finally
                {
                    Thread.Sleep(10);
                    Application.DoEvents();
                }
            }
        }
        private void DoSixMyAction(int timeout, string timeoutMessage, Action timeoutAct, string nextStepMessage, Action nextAct, Action curAct, Func<bool> breakWhile)
        {
            bool myTimeOut = true;
            CancellationTokenSource myToken = new CancellationTokenSource(timeout);
            myToken.Token.Register(new Action(() =>
            {
                if (myTimeOut)
                {
                    //LogError(timeoutMessage);
                    if (timeoutAct != null)
                    {
                        timeoutAct.Invoke();
                    }
                }
                else
                {
                    LogMessage(nextStepMessage);
                    if (nextAct != null && GlobalVar.totalRunFlag && !GlobalVar.deviceSafeDoorIsOpen && !GlobalVar.deviceAlarmIsHappen && !isManualPause)
                    {
                        nextAct.Invoke();
                        sixStep++;
                    }
                }
                isCurSixStepAction = false;
            }));
            if (curAct != null && GlobalVar.totalRunFlag && !GlobalVar.deviceSafeDoorIsOpen && !GlobalVar.deviceAlarmIsHappen && !isManualPause)
            {
                curAct.Invoke();
            }
            Task.Factory.StartNew(new Action(() =>
            {
                while (!myToken.Token.IsCancellationRequested)
                {
                    Thread.Sleep(5);
                    Application.DoEvents();

                    if (breakWhile.Invoke() || !GlobalVar.totalRunFlag)
                    {
                        myTimeOut = false; myToken.Cancel();
                    }
                }
            }), myToken.Token);
        }
        private void SixStaFisrtStep()
        {
            #region 第一步
            DoSixMyAction(axiasMoveTimeOut, "移动到位置以及下料气缸或者真空电磁阀初始状态侦测超时",
            new Action(() =>
            {
                if (!GlobalVar.lsAxiasDIs[24].CurIOStatus)//如果上电磁阀处于下状态，复位
                {
                    LogAlarmError("下料气缸上极限状态错误"); GlobalVar.deviceAlarmIsHappen = true;
                }
                if (GlobalVar.lsAxiasDIs[25].CurIOStatus)
                {
                    LogAlarmError("下料气缸下极限状态错误"); GlobalVar.deviceAlarmIsHappen = true;
                }
                if (GlobalVar.lsAxiasDIs[29].CurIOStatus)//复位真空电磁阀
                {
                    LogAlarmError("下料真空负压表状态错误"); GlobalVar.deviceAlarmIsHappen = true;
                }
            }),
            "已经移动到取料位置,气缸状态和真空阀正确,下一步打出气缸",
            new Action(() =>
            {
                if (!GlobalVar.lsAxiasDIs[41].CurIOStatus)
                {
                    IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[22].Card - 1), GlobalVar.lsAxiasDOs[22].PinDefinition, 0);//下料气缸
                }
            }),
            new Action(() =>
            {
                LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, GlobalVar.takeFeedProPos, (ushort)GlobalVar.takeCoordModel);
            }),
            () => GlobalVar.lsAxiasDIs[24].CurIOStatus && !GlobalVar.lsAxiasDIs[25].CurIOStatus && !GlobalVar.lsAxiasDIs[41].CurIOStatus && LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.TakeAxiasNumber) == 1 && GlobalVar.lsAxiasDIs[35].CurIOStatus);
            #endregion
        }
        private void SixStaSecStep()
        {
            #region 第2步
            DoSixMyAction(cylinderSensorTimeOut, "下料气缸打出超时",
           new Action(() =>
           {
               if (GlobalVar.lsAxiasDIs[24].CurIOStatus)//如果上电磁阀处于下状态，复位
               {
                   LogAlarmError("下料气缸上极限状态错误"); GlobalVar.deviceAlarmIsHappen = true; return;//IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[22].Card - 1), GlobalVar.lsAxiasDOs[22].PinDefinition, 1); sixStep = sixStep - 1;
               }
               if (!GlobalVar.lsAxiasDIs[25].CurIOStatus)
               {
                   LogAlarmError("下料气缸下极限状态错误"); GlobalVar.deviceAlarmIsHappen = true; return;//IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[22].Card - 1), GlobalVar.lsAxiasDOs[22].PinDefinition, 1); sixStep = sixStep - 1;
               }
           }),
            "下料气缸打出到位,下一步打开真空阀",
            new Action(() =>
            {
                IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[23].Card - 1), GlobalVar.lsAxiasDOs[23].PinDefinition, 0);//下料真空打开
                Thread.Sleep(GlobalVar.feedVaccDelayTime<100?100 : GlobalVar.feedVaccDelayTime);
            }),
            null,
            () => !GlobalVar.lsAxiasDIs[24].CurIOStatus && GlobalVar.lsAxiasDIs[25].CurIOStatus && !GlobalVar.lsAxiasDIs[41].CurIOStatus);
            //() => !GlobalVar.lsAxiasDIs[24].CurIOStatus && GlobalVar.lsAxiasDIs[25].CurIOStatus && GlobalVar.lsAxiasDIs[29].CurIOStatus && !GlobalVar.lsAxiasDIs[41].CurIOStatus && GlobalVar.lsAxiasDIs[35].CurIOStatus);
            #endregion
        }
        private void SixStaThrStep()
        {
            #region 第3步
            DoSixMyAction(cylinderSensorTimeOut, "真空检测超时",
            new Action(() =>
            {
                if (!GlobalVar.lsAxiasDIs[29].CurIOStatus)
                {
                    LogAlarmError("下料真空负压表状态错误"); GlobalVar.deviceAlarmIsHappen = true; return;
                }
            }),
            "真空检测OK,下一步退回下料气缸",
            new Action(() =>
            {
                IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[22].Card - 1), GlobalVar.lsAxiasDOs[22].PinDefinition, 1);//下料气缸退回
            }),
            null,
            () => !GlobalVar.lsAxiasDIs[24].CurIOStatus && GlobalVar.lsAxiasDIs[25].CurIOStatus && GlobalVar.lsAxiasDIs[29].CurIOStatus);
            //() => GlobalVar.lsAxiasDIs[29].CurIOStatus);//检测真空负压值，测试暂时不需要检测
            #endregion
        }
        private void SixStaFourStep()
        {
            #region 第4步
            DoSixMyAction(cylinderSensorTimeOut, "下料退回超时",
            new Action(() =>
            {
                IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[22].Card - 1), GlobalVar.lsAxiasDOs[22].PinDefinition, 1);
                if (!GlobalVar.lsAxiasDIs[24].CurIOStatus)
                {
                    LogAlarmError("下料气缸上极限状态错误"); GlobalVar.deviceAlarmIsHappen = true; return;
                }
                if (GlobalVar.lsAxiasDIs[25].CurIOStatus)
                {
                    LogAlarmError("下料气缸下极限状态错误"); GlobalVar.deviceAlarmIsHappen = true; return;
                }
            }),
            "退回到位,下一步允许凸轮分割旋转，同时可以移动到相应位置",
            new Action(() =>
            {
                LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, (finalTakeHighResAndAoiRes.Count > 0 ? finalTakeHighResAndAoiRes[0] : false) ? GlobalVar.takeOKProPos : GlobalVar.takeNGProPos, (ushort)GlobalVar.takeCoordModel);
                Thread.Sleep(300);
                while (LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.TakeAxiasNumber) == 0)
                {
                    Application.DoEvents();
                }
                if (finalTakeHighResAndAoiRes.Count > 0)
                {
                    finalTakeHighResAndAoiRes.RemoveAt(0);
                }
            }),
            null,
            () => GlobalVar.lsAxiasDIs[24].CurIOStatus && !GlobalVar.lsAxiasDIs[25].CurIOStatus);
            // () => GlobalVar.lsAxiasDIs[24].CurIOStatus && !GlobalVar.lsAxiasDIs[25].CurIOStatus && GlobalVar.lsAxiasDIs[29].CurIOStatus);
            #endregion
        }
        private void SixStaFiveStep()
        {
            #region 第5步
            DoSixMyAction(axiasMoveTimeOut, "移动到相应位置超时",
            null,
            "相应位置已到达,下一步气缸开始下降，并同时关闭电池阀",
            new Action(() =>
            {
                IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[22].Card - 1), GlobalVar.lsAxiasDOs[22].PinDefinition, 0);
                Thread.Sleep(100);
                IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[23].Card - 1), GlobalVar.lsAxiasDOs[23].PinDefinition, 1);
                sixStationisReadyOk = false;
            }),
            null,
            () => GlobalVar.lsAxiasDIs[24].CurIOStatus && !GlobalVar.lsAxiasDIs[25].CurIOStatus && LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.TakeAxiasNumber) == 1);
            //() => GlobalVar.lsAxiasDIs[24].CurIOStatus && !GlobalVar.lsAxiasDIs[25].CurIOStatus  && !GlobalVar.lsAxiasDIs[29].CurIOStatus);
            #endregion
        }
        private void SixStaSixStep()
        {
            #region 第6步
            DoSixMyAction(cylinderSensorTimeOut, "气缸下降状态超时",
            new Action(() =>
            {
                IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[22].Card - 1), GlobalVar.lsAxiasDOs[22].PinDefinition, 1);
            }),
            "气缸下降到位完成,下一步气缸退回",
            new Action(() =>
            {
                IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[22].Card - 1), GlobalVar.lsAxiasDOs[22].PinDefinition, 1);
            }),
            null,
            () => !GlobalVar.lsAxiasDIs[24].CurIOStatus && GlobalVar.lsAxiasDIs[25].CurIOStatus);
            #endregion
        }
        #endregion

        #endregion

        #endregion

        #region 原始图片异步保存
        /// <summary>
        /// 良品图片保存
        /// </summary>
        private delegate void P_ImgSave(CogImage8Grey Img, string imageName);
        /// <summary>
        /// 声明并初始化一个代理
        /// </summary>
        private static P_ImgSave ImgSave = new P_ImgSave(ImgSave_Method);
        /// <summary>
        /// 图片保存方法
        /// </summary>
        private static void ImgSave_Method(CogImage8Grey Img, string imageName)
        {
            if (Img != null)
            {
                try
                {
                    string dirPath = "";
                    dirPath = GlobalVar.bImageSavePath + GlobalVar.curProName + @"\" + DateTime.Now.ToString("yyyy-MM-dd") + @"\" + imageName;

                    string path = Directory.GetParent(dirPath).FullName;
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    Img.ToBitmap().Save(dirPath + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                catch (Exception e)
                {
                    //thisFrm.LogError(e.Message);
                }
            }
        }

        #endregion

        #region 带Graphics图片异步保存
        /// <summary>
        /// 良品图片保存
        /// </summary>
        private delegate void P_GraphicsImgSave(CogRecordDisplay recordDis, string imageName);
        /// <summary>
        /// 声明并初始化一个代理
        /// </summary>
        private static P_GraphicsImgSave GraphicsImgSave = new P_GraphicsImgSave(GraphicsImgSave_Method);
        /// <summary>
        /// 图片保存方法
        /// </summary>
        private static void GraphicsImgSave_Method(CogRecordDisplay recordDis, string imageName)
        {
            if (recordDis != null)
            {
                try
                {
                    string dirPath = "";
                    dirPath = GlobalVar.bImageSavePath + GlobalVar.curProName + @"\" + DateTime.Now.ToString("yyyy-MM-dd") + @"\" + imageName;
                    string path = Directory.GetParent(dirPath).FullName;
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    CogDisplayContentBitmapConstants content = new CogDisplayContentBitmapConstants();
                    CogRectangle reg = new CogRectangle();
                    Bitmap Image = recordDis.CreateContentBitmap(content, reg, 0) as Bitmap;
                    Image.Save(dirPath + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                catch (Exception e)
                {
                    //thisFrm.LogError(e.Message);
                }
            }
        }

        #endregion

        #region AOI异步处理
        private delegate void Do_AoiAction();
        static Do_AoiAction do_Aoi = null;

        #endregion

        #region 其他方法
        private void InitCfgfile()
        {
            txtBoxSN.Text = GlobalVar.curCusNumber;
            txtBoxWO.Text = GlobalVar.curWo;
            txtBoxTotalCnt.Text = (GlobalVar.okCnt + GlobalVar.ngCnt).ToString();
            txtBoxOkCnt.Text = GlobalVar.okCnt.ToString();
            txtBoxNgCnt.Text = GlobalVar.ngCnt.ToString();
            //cmbClass.SelectedItem = GlobalVar.curClass;
            txtBoxOkYeild.Text = (GlobalVar.okCnt + GlobalVar.ngCnt) == 0 ? "0%" : $"{(GlobalVar.okCnt * 1.0 / (GlobalVar.okCnt + GlobalVar.ngCnt) * 100.0).ToString("f1")}%";
        }

        private void InitMESData()
        {
            woList.Add(new WorkOrder());
            woList.Add(new WorkOrder() { Class = "夜班", Item = "8" });
            dgvSelectWoID.DataSource = woList;
            dgvSelectWoID.Columns[0].HeaderText = "日期";
            dgvSelectWoID.Columns[1].HeaderText = "班别";
            dgvSelectWoID.Columns[1].Width = 40;
            dgvSelectWoID.Columns[2].HeaderText = "料号";
            dgvSelectWoID.Columns[2].Width = 150;
            dgvSelectWoID.Columns[3].HeaderText = "工单号";
            dgvSelectWoID.Columns[3].Width = 120;
            dgvSelectWoID.Columns[4].HeaderText = "排程单号";
            dgvSelectWoID.Columns[4].Width = 120;
            dgvSelectWoID.Columns[5].HeaderText = "项次";
            dgvSelectWoID.Columns[5].Width = 40;
            dgvSelectWoID.Columns[6].HeaderText = "选我你就点我吧";
        }

        private void DevieLedChangeStatus()
        {
            while (GlobalVar.totalRunFlagVision)
            {
                try
                {
                    thisFrm.Invoke(new Action(() =>
                    {
                        //当设备启动按钮启动后设备开始闪烁
                        if (isStartBtnResponse)
                        {
                            if (!isStartButton)
                            {
                                isStartButton = true;
                                btnStart.BackColor = Color.FromKnownColor(KnownColor.Control);
                            }
                            else
                            {
                                isStartButton = false;
                                btnStart.BackColor = Color.MediumSeaGreen;
                            }
                        }
                        else
                        { btnStart.BackColor = Color.FromKnownColor(KnownColor.Control); isStartButton = false; }
                        //安全门打开
                        if (GlobalVar.deviceSafeDoorIsOpen || isAlarmTrig || isManualPause)
                        {
                            isStartBtnResponse = false;
                            btnPause.Text = "继 续";
                            if (!isPauseButton)
                            {
                                isPauseButton = true;
                                btnPause.BackColor = Color.FromKnownColor(KnownColor.Control);
                            }
                            else
                            {
                                isPauseButton = false;
                                btnPause.BackColor = Color.DarkGreen;
                            }
                        }
                        else
                        {
                            btnPause.Text = "暂 停";
                            btnPause.BackColor = Color.FromKnownColor(KnownColor.Control);
                            isPauseButton = false;
                        }
                        //停止按钮开启
                        if (isStopBtnResponse)
                        {
                            if (!isStopButton)
                            {
                                isStopButton = true;
                                btnStop.BackColor = Color.FromKnownColor(KnownColor.Control);
                            }
                            else
                            {
                                isStopButton = false;
                                btnStop.BackColor = Color.Red;
                            }
                        }
                        else
                        { isStopButton = false; btnStop.BackColor = Color.FromKnownColor(KnownColor.Control); }
                        //设备报警
                        if (GlobalVar.deviceAlarmIsHappen)
                        {
                            if (!isAlarmButton)
                            {
                                isAlarmButton = true;
                                btnClearAlarm.BackColor = Color.FromKnownColor(KnownColor.Control);
                                //蜂鸣
                                IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[12].Card - 1), GlobalVar.lsAxiasDOs[12].PinDefinition, (ushort)0);
                                //红灯
                                IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[9].Card - 1), GlobalVar.lsAxiasDOs[9].PinDefinition, (ushort)0);
                            }
                            else
                            {
                                isAlarmButton = false;
                                btnClearAlarm.BackColor = Color.Red;
                                //蜂鸣
                                IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[12].Card - 1), GlobalVar.lsAxiasDOs[12].PinDefinition, (ushort)1);
                                //红灯
                                IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[9].Card - 1), GlobalVar.lsAxiasDOs[9].PinDefinition, (ushort)1);
                            }
                        }
                        else
                        { isAlarmButton = false; btnClearAlarm.BackColor = Color.FromKnownColor(KnownColor.Control); }
                        //生产结束收尾
                        if (GlobalVar.totalRunIsEnded)
                        {
                            if (!isEndManuProButton)
                            {
                                isEndManuProButton = true;
                                btnEndManufa.BackColor = Color.FromKnownColor(KnownColor.Control);
                            }
                            else
                            {
                                isEndManuProButton = false;
                                btnEndManufa.BackColor = Color.DarkSlateGray;
                            }
                        }
                        else
                        { isEndManuProButton = false; btnEndManufa.BackColor = Color.FromKnownColor(KnownColor.Control); }
                    }));
                }
                catch (Exception ex)
                {
                    GlobalVar.myLog.Error($"设备状态灯显示异常错误：{ex.Message}");
                }
                finally
                {
                    Application.DoEvents();
                    Thread.Sleep(500);
                }
            }
        }

        private void AnalysisiAllStaPicData()
        {
            while (GlobalVar.totalRunFlagVision)
            {
                try
                {
                    if (GlobalVar.isAnalysisPic)
                    {
                        if (picDicList.Count > 0)
                        {
                            string[] paths = picDicList.ElementAt(0).Key.Split('-');
                            if (GlobalVar.curWo.Contains('-'))
                            {
                                IAsyncResult ia_TBImgSave = GraphicsImgSave.BeginInvoke(picDicList.ElementAt(0).Value, @"图片分析\" + paths[1] + @"\流水号" + paths[3] + @"\" + paths[2], null, ImgSave);
                            }
                            else
                            {
                                IAsyncResult ia_TBImgSave = GraphicsImgSave.BeginInvoke(picDicList.ElementAt(0).Value, @"图片分析\" + paths[0] + @"\流水号" + paths[2] + @"\" + paths[1], null, ImgSave);
                            }

                            picDicList.Remove(picDicList.ElementAt(0).Key);
                        }
                    }
                }
                catch (Exception ex)
                {
                    GlobalVar.myLog.Error($"分析图片数据功能出现未知错误：{ex.Message}");
                }
                finally
                {
                    Application.DoEvents();
                    Thread.Sleep(100);
                }
            }
        }

        private void InitAxiasHomeParamsAndGoHome()
        {
            LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, -30000, (ushort)1);
            while (LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber) == 0)
            {
                Application.DoEvents();
            }
            LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, -30000, (ushort)1);
            while (LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.FeedAxiasNumber) == 0)
            {
                Application.DoEvents();
            }
            LTDMC.dmc_set_homemode(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, 1, (double)GlobalVar.injectFeedHomeSpeedModel, (ushort)GlobalVar.injectFeedHomeModel, 0);
            LTDMC.dmc_set_home_position(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, 2, 0);
            LTDMC.dmc_set_homemode(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, 1, (double)GlobalVar.feedHomeSpeedModel, (ushort)GlobalVar.feedHomeModel, 0);
            LTDMC.dmc_set_home_position(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, 2, 0);
            if (LTDMC.dmc_get_position(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber) > 0)
            {
                LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, -4000, (ushort)1);
                while (LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber) == 0)
                {
                    Application.DoEvents();
                }
            }
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


        #endregion

    }
}
