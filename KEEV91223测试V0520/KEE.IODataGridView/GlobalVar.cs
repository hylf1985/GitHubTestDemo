using ClassINI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEE.IODataGridView
{
    public class GlobalVar
    {
        /// <summary>
        /// IO配置路径
        /// </summary>
        public static string bDeviceIOFilePath = @"D:\Data\IO\IO.ini";

        /// <summary>
        /// IO卡输入点集合
        /// </summary>
        public static List<LsIODIPinDefinition> lsAxiasDIs = new List<LsIODIPinDefinition>();
        /// <summary>
        /// IO卡输出点集合
        /// </summary>
        public static List<LsIODOPinDefinition> lsAxiasDOs = new List<LsIODOPinDefinition>();

        #region 雷赛IO卡变量

        #region 输入变量
        /// <summary>
        /// 机器人待机中
        /// </summary>
        public static string epsonStandbying = "1,1";
        /// <summary>
        /// 机器人运行中
        /// </summary>
        public static string epsonRunning = "1,2";
        /// <summary>
        /// 机器人暂停中
        /// </summary>
        public static string epsonPausing = "1,3";
        /// <summary>
        /// 机器人控制器一般错误
        /// </summary>
        public static string epsonControllerErr = "1,4";
        /// <summary>
        /// 机器人急停输出
        /// </summary>
        public static string epsonEMGOutput = "1,5";
        /// <summary>
        /// 机器人安全门打开
        /// </summary>
        public static string epsonSafeDoor = "1,6";
        /// <summary>
        /// 机器人控制器严重错误
        /// </summary>
        public static string epsonControllerFatalErr = "1,7";
        /// <summary>
        /// 机器人报警
        /// </summary>
        public static string epsonAlarm = "1,8";
        /// <summary>
        /// 有信放料完成
        /// </summary>
        public static string yuShinBlowingFinished = "1,9";
        /// <summary>
        /// 设备启动按钮
        /// </summary>
        public static string deviceBoot = "1,10";
        /// <summary>
        /// 设备停止按钮
        /// </summary>
        public static string deviceStop = "1,11";
        /// <summary>
        /// 电镀件补料完成
        /// </summary>
        public static string logo4FeedFinished = "1,14";
        /// <summary>
        /// 旋转气缸左极限
        /// </summary>
        public static string rotatingCylinderLeftLim = "1,15";
        /// <summary>
        /// 旋转气缸右极限
        /// </summary>
        public static string rotatingCylinderRightLim = "1,16";
        /// <summary>
        /// 上料气缸上极限
        /// </summary>
        public static string feedCylinderHighLim = "1,17";
        /// <summary>
        /// 上料气缸下极限
        /// </summary>
        public static string feedCylinderLowLim = "1,18";
        /// <summary>
        /// 出标轴下压气缸上极限
        /// </summary>
        public static string labelCylinderHighLim = "1,19";
        /// <summary>
        /// 出标轴下压气缸下极限
        /// </summary>
        public static string labelCylinderLowLim = "1,20";
        /// <summary>
        /// 出标轴出标气缸左极限
        /// </summary>
        public static string labelCylinderLeftLim = "1,21";
        /// <summary>
        /// 出标轴出标气缸右极限
        /// </summary>
        public static string labelCylinderRightLim = "1,22";
        /// <summary>
        /// 压合气缸上极限
        /// </summary>
        public static string pressCylinderHighLim = "1,23";
        /// <summary>
        /// 压合气缸下极限
        /// </summary>
        public static string pressCylinderLowLim = "1,24";
        /// <summary>
        /// 测高气缸上极限
        /// </summary>
        public static string highTestCylinderHighLim = "1,25";
        /// <summary>
        /// 测高气缸下极限
        /// </summary>
        public static string highTestCylinderLowLim = "1,26";
        /// <summary>
        /// 下料气缸上极限
        /// </summary>
        public static string takeCylinerHighLim = "1,27";
        /// <summary>
        /// 下料气缸下极限
        /// </summary>
        public static string takeCylinerLowLim = "1,28";
        /// <summary>
        /// 贴Logo按压气缸上极限
        /// </summary>
        public static string pushCylinerHighLim = "1,29";
        /// <summary>
        /// 贴Logo按压气缸下极限
        /// </summary>
        public static string pushCylinerLowLim = "1,30";
        /// <summary>
        /// 上料真空负压表阈值
        /// </summary>
        public static string feedVacuumLim = "1,31";
        /// <summary>
        /// 下料真空负压表阈值
        /// </summary>
        public static string takeVacuumLim = "1,32";
        /// <summary>
        /// 上料工站料感信号
        /// </summary>
        public static string feedStationSensor = "2,1";
        /// <summary>
        /// 贴标工站料感信号
        /// </summary>
        public static string labelStationSensor = "2,2";
        /// <summary>
        /// 压合工站料感信号
        /// </summary>
        public static string pressStationSensor = "2,3";
        /// <summary>
        /// 测高工站料感信号
        /// </summary>
        public static string highTestStationSensor = "2,4";
        /// <summary>
        /// AOI偏位测试工站料感信号
        /// </summary>
        public static string aoiTestStationSensor = "2,5";
        /// <summary>
        /// 下料工站料感信号
        /// </summary>
        public static string takeStationSensor = "2,6";
        /// <summary>
        /// Epson通知取完4片Logo信号
        /// </summary>
        public static string feed4PcsLogoFinished = "2,7";
        /// <summary>
        /// Epson通知撕标开始信号
        /// </summary>
        public static string tearLabelStart = "2,8";
        /// <summary>
        /// Epson通知出标开始信号
        /// </summary>
        public static string newLabelStart = "2,9";
        /// <summary>
        /// 贴电镀+背胶完成
        /// </summary>
        public static string labelLogoToPlasticFinished = "2,10";
        #endregion

        #region 输出变量
        /// <summary>
        /// 启动机器人程序
        /// </summary>
        public static string bootEpson = "1,1";
        /// <summary>
        /// 机器人程序1
        /// </summary>
        public static string prog1Selected = "1,2";
        /// <summary>
        /// 机器人程序2
        /// </summary>
        public static string prog2Selected = "1,3";
        /// <summary>
        /// 机器人程序3
        /// </summary>
        public static string prog3Selected = "1,4";
        /// <summary>
        /// 停止机器人
        /// </summary>
        public static string stopEpson = "1,5";
        /// <summary>
        /// 暂停机器人
        /// </summary>
        public static string pausedEpson = "1,6";
        /// <summary>
        /// 停止机器人
        /// </summary>
        public static string continuedEpson = "1,7";
        /// <summary>
        /// 复位机器人
        /// </summary>
        public static string resetEpson = "1,8";
        /// <summary>
        /// 允许有信放料
        /// </summary>
        public static string allowYuShinTakePro = "1,9";
        /// <summary>
        /// 三色灯-红灯
        /// </summary>
        public static string redAlarmLight = "1,10";
        /// <summary>
        /// 三色灯-黄灯
        /// </summary>
        public static string yellowAlarmLight = "1,11";
        /// <summary>
        /// 三色灯-绿灯
        /// </summary>
        public static string greenAlarmLight = "1,12";
        /// <summary>
        /// 三色灯-蜂鸣
        /// </summary>
        public static string buzzingAlarm = "1,13";
        /// <summary>
        /// 补料完成-->通知机器人和按钮绿灯亮
        /// </summary>
        public static string feedFinishedOut = "1,14";
        /// <summary>
        /// 待补料-->按钮红灯亮
        /// </summary>
        public static string feedStandbyingOut = "1,15";
        /// <summary>
        /// 旋转气缸电磁阀
        /// </summary>
        public static string rotatingCylinderElecValve = "1,16";
        /// <summary>
        /// 上料气缸电磁阀
        /// </summary>
        public static string feedCylinderElecValve = "1,17";
        /// <summary>
        /// 上料真空电磁阀
        /// </summary>
        public static string feedVacuumSolenoid = "1,18";
        /// <summary>
        /// 出标轴下压气缸电磁阀
        /// </summary>
        public static string labelPressCylinderElecValve = "1,19";
        /// <summary>
        /// 出标轴出标气缸电磁阀
        /// </summary>
        public static string labelOutCylinderElecValve = "1,20";
        /// <summary>
        /// 压合气缸电磁阀
        /// </summary>
        public static string pressCylinderElecValve = "1,21";
        /// <summary>
        /// 测高气缸电磁阀
        /// </summary>
        public static string highTestCylinderElecValve = "1,22";
        /// <summary>
        /// 下料气缸电磁阀
        /// </summary>
        public static string takeCylinderElecValve = "1,23";
        /// <summary>
        /// 下料真空电磁阀
        /// </summary>
        public static string takeVacuumSolenoid = "1,24";
        /// <summary>
        /// 贴Logo按压气缸电磁阀
        /// </summary>
        public static string pushCylinderElecValve = "1,25";
        /// <summary>
        /// 允许epson取Logo
        /// </summary>
        public static string allowEpsonFeedLogo = "1,26";
        /// <summary>
        /// 允许epson贴背胶
        /// </summary>
        public static string allowEpsonLabelGum = "1,27";
        /// <summary>
        /// 允许epson再出标气缸回退到位后从贴标位置离开
        /// </summary>
        public static string allowEpsonLabelPosMove = "1,28";
        /// <summary>
        /// 允许epson贴Logo到塑胶件
        /// </summary>
        public static string allowEpsonLabelLogoToPlastic = "1,29";
        #endregion

        #endregion

        #region 雷赛轴卡IO变量
        /// <summary>
        /// 与注塑机关联的取料轴正极限
        /// </summary>
        public static int feedFromInjectPLimP = 1;
        /// <summary>
        /// 与注塑机关联的取料轴负极限
        /// </summary>
        public static int feedFromInjectPLimN = 1;
        /// <summary>
        /// 与注塑机关联的取料轴原点
        /// </summary>
        public static int feedFromInjectPLimHome = 1;
        /// <summary>
        /// 取料轴正极限
        /// </summary>
        public static int feedPLimP = 1;
        /// <summary>
        /// 取料轴负极限
        /// </summary>
        public static int feedLimN = 1;
        /// <summary>
        /// 取料轴原点
        /// </summary>
        public static int feedPLimHome = 1;
        /// <summary>
        /// 取料R轴原点
        /// </summary>
        public static int feedRPLimHome = 1;
        /// <summary>
        /// 丢料轴正极限
        /// </summary>
        public static int takePLimP = 1;
        /// <summary>
        /// 丢料轴负极限
        /// </summary>
        public static int takePLimN = 1;
        /// <summary>
        /// 丢料轴原点
        /// </summary>
        public static int takePLimHome = 1;
        #endregion
        public static void Initpara()
        {
            try
            {
                if (File.Exists(bDeviceIOFilePath))  //如果文件存在
                {
                    #region IO卡输入变量
                    epsonStandbying            = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "机器人待机中"            , null);
                    epsonRunning               = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "机器人运行中"            , null);
                    epsonPausing               = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "机器人暂停中"            , null);
                    epsonControllerErr         = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "机器人控制器一般错误"    , null);
                    epsonEMGOutput             = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "机器人急停输出"          , null);
                    epsonSafeDoor              = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "机器人安全门打开"        , null);
                    epsonControllerFatalErr    = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "机器人控制器严重错误"    , null);
                    epsonAlarm                 = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "机器人报警"              , null);
                    yuShinBlowingFinished      = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "有信放料完成"            , null);
                    deviceBoot                 = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "设备启动按钮"            , null);
                    deviceStop                 = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "设备停止按钮"            , null);
                    logo4FeedFinished          = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "电镀件补料完成"          , null);
                    rotatingCylinderLeftLim    = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "旋转气缸左极限"          , null);
                    rotatingCylinderRightLim   = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "旋转气缸右极限"          , null);
                    feedCylinderHighLim        = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "上料气缸上极限"          , null);
                    feedCylinderLowLim         = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "上料气缸下极限"          , null);
                    labelCylinderHighLim       = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "出标轴下压气缸上极限"    , null);
                    labelCylinderLowLim        = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "出标轴下压气缸下极限"    , null);
                    labelCylinderLeftLim       = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "出标轴出标气缸左极限"    , null);
                    labelCylinderRightLim      = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "出标轴出标气缸右极限"    , null);
                    pressCylinderHighLim       = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "压合气缸上极限"          , null);
                    pressCylinderLowLim        = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "压合气缸下极限"          , null);
                    highTestCylinderHighLim    = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "测高气缸上极限"          , null);
                    highTestCylinderLowLim     = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "测高气缸下极限"          , null);
                    takeCylinerHighLim         = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "下料气缸上极限"          , null);
                    takeCylinerLowLim          = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "下料气缸下极限"          , null);
                    pushCylinerHighLim         = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "贴Logo按压气缸上极限"    , null);
                    pushCylinerLowLim          = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "贴Logo按压气缸下极限"    , null);
                    feedVacuumLim              = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "上料真空负压表阈值"      , null);
                    takeVacuumLim              = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "下料真空负压表阈值"      , null);
                    feedStationSensor          = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "上料工站料感信号"        , null);
                    labelStationSensor         = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "贴标工站料感信号"        , null);
                    pressStationSensor         = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "压合工站料感信号"        , null);
                    highTestStationSensor      = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "测高工站料感信号"        , null);
                    aoiTestStationSensor       = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "AOI偏位测试工站料感信号" , null);
                    takeStationSensor          = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "下料工站料感信号"        , null);
                    feed4PcsLogoFinished       = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "Epson通知取完4片Logo信号", null);
                    tearLabelStart             = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "Epson通知撕标开始信号"   , null);
                    newLabelStart              = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "Epson通知出标开始信号"   , null);
                    labelLogoToPlasticFinished = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "贴电镀+背胶完成"         , null);
                    #endregion
                    #region IO卡输出变量
                    bootEpson                    = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "启动机器人程序", null);
                    prog1Selected                = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "机器人程序1", null);
                    prog2Selected                = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "机器人程序2", null);
                    prog3Selected                = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "机器人程序3", null);
                    stopEpson                    = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "停止机器人", null);
                    pausedEpson                  = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "暂停机器人", null);
                    continuedEpson               = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "继续机器人", null);
                    resetEpson                   = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "复位机器人  ", null);
                    allowYuShinTakePro           = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "允许有信放料", null);
                    redAlarmLight                = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "三色灯-红灯", null);
                    yellowAlarmLight             = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "三色灯-黄灯", null);
                    greenAlarmLight              = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "三色灯-绿灯", null);
                    buzzingAlarm                 = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "三色灯-蜂鸣", null);
                    feedFinishedOut              = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "补料完成->按钮绿灯亮", null);
                    feedStandbyingOut            = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "待补料->按钮红灯亮", null);
                    rotatingCylinderElecValve    = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "旋转气缸电磁阀", null);
                    feedCylinderElecValve        = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "上料气缸电磁阀", null);
                    feedVacuumSolenoid           = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "上料真空电磁阀", null);
                    labelPressCylinderElecValve  = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "出标轴下压气缸电磁阀", null);
                    labelOutCylinderElecValve    = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "出标轴出标气缸电磁阀", null);
                    pressCylinderElecValve       = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "压合气缸电磁阀", null);
                    highTestCylinderElecValve    = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "测高气缸电磁阀", null);
                    takeCylinderElecValve        = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "下料气缸电磁阀", null);
                    takeVacuumSolenoid           = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "下料真空电磁阀", null);
                    pushCylinderElecValve        = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "贴Logo按压气缸电磁阀", null);
                    allowEpsonFeedLogo           = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "允许epson取Logo", null);
                    allowEpsonLabelGum           = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "允许epson贴背胶", null);
                    allowEpsonLabelPosMove       = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "允许epson在出标气缸回退到位后从贴标位置离开", null);
                    allowEpsonLabelLogoToPlastic = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "允许epson贴Logo到塑胶件", null);
                    #endregion
                    #region 轴卡输入信号变量
                    feedFromInjectPLimP = Convert.ToInt32(INI.INIGetStringValue(bDeviceIOFilePath, "轴卡输入信号配置", "与注塑机关联的取料轴正极限", null));
                    feedFromInjectPLimN = Convert.ToInt32(INI.INIGetStringValue(bDeviceIOFilePath, "轴卡输入信号配置", "与注塑机关联的取料轴负极限", null));
                    feedFromInjectPLimHome = Convert.ToInt32(INI.INIGetStringValue(bDeviceIOFilePath, "轴卡输入信号配置", "与注塑机关联的取料轴原点", null));
                    feedPLimP = Convert.ToInt32(INI.INIGetStringValue(bDeviceIOFilePath, "轴卡输入信号配置", "取料轴正极限", null));
                    feedLimN = Convert.ToInt32(INI.INIGetStringValue(bDeviceIOFilePath, "轴卡输入信号配置", "取料轴负极限", null));
                    feedPLimHome = Convert.ToInt32(INI.INIGetStringValue(bDeviceIOFilePath, "轴卡输入信号配置", "取料轴原点", null));
                    feedRPLimHome = Convert.ToInt32(INI.INIGetStringValue(bDeviceIOFilePath, "轴卡输入信号配置", "取料R轴原点", null));
                    takePLimP = Convert.ToInt32(INI.INIGetStringValue(bDeviceIOFilePath, "轴卡输入信号配置", "丢料轴正极限", null));
                    takePLimN = Convert.ToInt32(INI.INIGetStringValue(bDeviceIOFilePath, "轴卡输入信号配置", "丢料轴负极限", null));
                    takePLimHome = Convert.ToInt32(INI.INIGetStringValue(bDeviceIOFilePath, "轴卡输入信号配置", "丢料轴原点", null));
                    #endregion
                }
                else
                {
                    if (!Directory.Exists(bDeviceIOFilePath))
                    {
                        DirectoryInfo pathInfo = new DirectoryInfo(bDeviceIOFilePath);
                        Directory.CreateDirectory(pathInfo.Parent.FullName);
                    }
                    FileStream fs = new FileStream(bDeviceIOFilePath, FileMode.CreateNew, FileAccess.ReadWrite);
                    fs.Close();
                    //写入一批键值  通过引用的ClassINI文件内建置的方法完成如下操作；
                    INI.INIWriteItems(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "机器人待机中=1,1\0机器人运行中=1,2\0机器人暂停中=1,3\0机器人控制器一般错误=1,4\0机器人急停输出=1,5\0机器人安全门打开=1,6\0机器人控制器严重错误=1,7\0机器人报警=1,8\0有信放料完成=1,9\0设备启动按钮=1,10\0设备停止按钮=1,11\0电镀件补料完成=1,14\0旋转气缸左极限=1,15\0旋转气缸右极限=1,16\0上料气缸上极限=1,17\0上料气缸下极限=1,18\0出标轴下压气缸上极限=1,19\0出标轴下压气缸下极限=1,20\0出标轴出标气缸左极限=1,21\0出标轴出标气缸右极限=1,22\0压合气缸上极限=1,23\0压合气缸下极限=1,24\0测高气缸上极限=1,25\0测高气缸下极限=1,26\0下料气缸上极限=1,27\0下料气缸下极限=1,28\0贴Logo按压气缸上极限=1,29\0贴Logo按压气缸下极限=1,30\0上料真空负压表阈值=1,31\0下料真空负压表阈值=1,32\0上料工站料感信号=2,1\0贴标工站料感信号=2,2\0压合工站料感信号=2,3\0测高工站料感信号=2,4\0AOI偏位测试工站料感信号=2,5\0下料工站料感信号=2,6\0Epson通知取完4片Logo信号=2,7\0Epson通知撕标开始信号=2,8\0Epson通知出标开始信号=2,9\0贴电镀+背胶完成=2,10\0");
                    INI.INIWriteItems(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "启动机器人程序=1,1\0机器人程序1=1,2\0机器人程序2=1,3\0机器人程序3=1,4\0停止机器人=1,5\0暂停机器人=1,6\0继续机器人=1,7\0复位机器人=1,8\0允许有信放料=1,9\0三色灯-红灯=1,10\0三色灯-黄灯=1,11\0三色灯-绿灯=1,12\0三色灯-蜂鸣=1,13\0补料完成->按钮绿灯亮=1,14\0待补料->按钮红灯亮=1,15\0旋转气缸电磁阀=1,16\0上料气缸电磁阀=1,17\0上料真空电磁阀=1,18\0出标轴下压气缸电磁阀=1,19\0出标轴出标气缸电磁阀=1,20\0压合气缸电磁阀=1,21\0测高气缸电磁阀=1,22\0下料气缸电磁阀=1,23\0下料真空电磁阀=1,24\0贴Logo按压气缸电磁阀=1,25\0允许epson取Logo=1,26\0允许epson贴背胶=1,27\0允许epson在出标气缸回退到位后从贴标位置离开=1,28\0允许epson贴Logo到塑胶件=1,29\0");
                    INI.INIWriteItems(bDeviceIOFilePath, "轴卡输入信号配置", "与注塑机关联的取料轴正极限=1\0与注塑机关联的取料轴负极限=1\0与注塑机关联的取料轴原点=1\0取料轴正极限=1\0取料轴负极限=1\0取料轴原点=1\0取料R轴原点=1\0丢料轴正极限=1\0丢料轴负极限=1\0丢料轴原点=1\0");
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static void InitAxiasDI()
        {
            if (lsAxiasDIs.Count > 0)
            {
                lsAxiasDIs.Clear();
            }
            //lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = int.Parse(GlobalVar.epsonStandbying.Split(',')[0]), PinDefinition = int.Parse(GlobalVar.epsonStandbying.Split(',')[1]), PinDefinitionName = "机器人待机中", PinStatus = KEE.IODataGridView.Properties.Resources.RedBtn });
            //lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = int.Parse(GlobalVar.epsonRunning.Split(',')[0]), PinDefinition = int.Parse(GlobalVar.epsonRunning.Split(',')[1]), PinDefinitionName = "机器人运行中", PinStatus = KEE.IODataGridView.Properties.Resources.RedBtn });
            //lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = int.Parse(GlobalVar.epsonPausing.Split(',')[0]), PinDefinition = int.Parse(GlobalVar.epsonPausing.Split(',')[1]), PinDefinitionName = "机器人暂停中", PinStatus = KEE.IODataGridView.Properties.Resources.RedBtn });
            //lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = int.Parse(GlobalVar.epsonControllerErr.Split(',')[0]), PinDefinition = int.Parse(GlobalVar.epsonControllerErr.Split(',')[1]), PinDefinitionName = "机器人控制器一般错误", PinStatus = KEE.IODataGridView.Properties.Resources.RedBtn });
            //lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = int.Parse(GlobalVar.epsonEMGOutput.Split(',')[0]), PinDefinition = int.Parse(GlobalVar.epsonEMGOutput.Split(',')[1]), PinDefinitionName = "机器人急停输出", PinStatus = KEE.IODataGridView.Properties.Resources.RedBtn });
            //lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = int.Parse(GlobalVar.epsonSafeDoor.Split(',')[0]), PinDefinition = int.Parse(GlobalVar.epsonSafeDoor.Split(',')[1]), PinDefinitionName = "机器人安全门打开", PinStatus = KEE.IODataGridView.Properties.Resources.RedBtn });
            //lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = int.Parse(GlobalVar.epsonControllerFatalErr.Split(',')[0]), PinDefinition = int.Parse(GlobalVar.epsonControllerFatalErr.Split(',')[1]), PinDefinitionName = "机器人控制器严重错误", PinStatus = KEE.IODataGridView.Properties.Resources.RedBtn });
            //lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = int.Parse(GlobalVar.epsonAlarm.Split(',')[0]), PinDefinition = int.Parse(GlobalVar.epsonAlarm.Split(',')[1]), PinDefinitionName = "机器人报警", PinStatus = KEE.IODataGridView.Properties.Resources.RedBtn });
            //lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = int.Parse(GlobalVar.yuShinBlowingFinished.Split(',')[0]), PinDefinition = int.Parse(GlobalVar.yuShinBlowingFinished.Split(',')[1]), PinDefinitionName = "有信放料完成", PinStatus = KEE.IODataGridView.Properties.Resources.RedBtn });
            //lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = int.Parse(GlobalVar.deviceBoot.Split(',')[0]), PinDefinition = int.Parse(GlobalVar.deviceBoot.Split(',')[1]), PinDefinitionName = "设备启动按钮", PinStatus = KEE.IODataGridView.Properties.Resources.RedBtn });
            //lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = int.Parse(GlobalVar.deviceStop.Split(',')[0]), PinDefinition = int.Parse(GlobalVar.deviceStop.Split(',')[1]), PinDefinitionName = "设备停止按钮", PinStatus = KEE.IODataGridView.Properties.Resources.RedBtn });
            //lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = int.Parse(GlobalVar.logo4FeedFinished.Split(',')[0]), PinDefinition = int.Parse(GlobalVar.logo4FeedFinished.Split(',')[1]), PinDefinitionName = "电镀件补料完成", PinStatus = KEE.IODataGridView.Properties.Resources.RedBtn });
            //lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = int.Parse(GlobalVar.rotatingCylinderLeftLim.Split(',')[0]), PinDefinition = int.Parse(GlobalVar.rotatingCylinderLeftLim.Split(',')[1]), PinDefinitionName = "旋转气缸左极限", PinStatus = KEE.IODataGridView.Properties.Resources.RedBtn });
            //lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = int.Parse(GlobalVar.rotatingCylinderRightLim.Split(',')[0]), PinDefinition = int.Parse(GlobalVar.rotatingCylinderRightLim.Split(',')[1]), PinDefinitionName = "旋转气缸右极限", PinStatus = KEE.IODataGridView.Properties.Resources.RedBtn });
            //lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = int.Parse(GlobalVar.feedCylinderHighLim.Split(',')[0]), PinDefinition = int.Parse(GlobalVar.feedCylinderHighLim.Split(',')[1]), PinDefinitionName = "上料气缸上极限", PinStatus = KEE.IODataGridView.Properties.Resources.RedBtn });
            //lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = int.Parse(GlobalVar.feedCylinderLowLim.Split(',')[0]), PinDefinition = int.Parse(GlobalVar.feedCylinderLowLim.Split(',')[1]), PinDefinitionName = "上料气缸下极限", PinStatus = KEE.IODataGridView.Properties.Resources.RedBtn });
            //lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = int.Parse(GlobalVar.labelCylinderHighLim.Split(',')[0]), PinDefinition = int.Parse(GlobalVar.labelCylinderHighLim.Split(',')[1]), PinDefinitionName = "出标轴下压气缸上极限", PinStatus = KEE.IODataGridView.Properties.Resources.RedBtn });
            //lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = int.Parse(GlobalVar.labelCylinderLowLim.Split(',')[0]), PinDefinition = int.Parse(GlobalVar.labelCylinderLowLim.Split(',')[1]), PinDefinitionName = "出标轴下压气缸下极限", PinStatus = KEE.IODataGridView.Properties.Resources.RedBtn });
            //lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = int.Parse(GlobalVar.labelCylinderLeftLim.Split(',')[0]), PinDefinition = int.Parse(GlobalVar.labelCylinderLeftLim.Split(',')[1]), PinDefinitionName = "出标轴出标气缸左极限", PinStatus = KEE.IODataGridView.Properties.Resources.RedBtn });
            //lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = int.Parse(GlobalVar.labelCylinderRightLim.Split(',')[0]), PinDefinition = int.Parse(GlobalVar.labelCylinderRightLim.Split(',')[1]), PinDefinitionName = "出标轴出标气缸右极限", PinStatus = KEE.IODataGridView.Properties.Resources.RedBtn });
            //lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = int.Parse(GlobalVar.pressCylinderHighLim.Split(',')[0]), PinDefinition = int.Parse(GlobalVar.pressCylinderHighLim.Split(',')[1]), PinDefinitionName = "压合气缸上极限", PinStatus = KEE.IODataGridView.Properties.Resources.RedBtn });
            //lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = int.Parse(GlobalVar.pressCylinderLowLim.Split(',')[0]), PinDefinition = int.Parse(GlobalVar.pressCylinderLowLim.Split(',')[1]), PinDefinitionName = "压合气缸下极限", PinStatus = KEE.IODataGridView.Properties.Resources.RedBtn });
            //lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = int.Parse(GlobalVar.highTestCylinderHighLim.Split(',')[0]), PinDefinition = int.Parse(GlobalVar.highTestCylinderHighLim.Split(',')[1]), PinDefinitionName = "测高气缸上极限", PinStatus = KEE.IODataGridView.Properties.Resources.RedBtn });
            //lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = int.Parse(GlobalVar.highTestCylinderLowLim.Split(',')[0]), PinDefinition = int.Parse(GlobalVar.highTestCylinderLowLim.Split(',')[1]), PinDefinitionName = "测高气缸下极限", PinStatus = KEE.IODataGridView.Properties.Resources.RedBtn });
            //lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = int.Parse(GlobalVar.takeCylinerHighLim.Split(',')[0]), PinDefinition = int.Parse(GlobalVar.takeCylinerHighLim.Split(',')[1]), PinDefinitionName = "下料气缸上极限", PinStatus = KEE.IODataGridView.Properties.Resources.RedBtn });
            //lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = int.Parse(GlobalVar.takeCylinerLowLim.Split(',')[0]), PinDefinition = int.Parse(GlobalVar.takeCylinerLowLim.Split(',')[1]), PinDefinitionName = "下料气缸下极限", PinStatus = KEE.IODataGridView.Properties.Resources.RedBtn });
            //lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = int.Parse(GlobalVar.pushCylinerHighLim.Split(',')[0]), PinDefinition = int.Parse(GlobalVar.pushCylinerHighLim.Split(',')[1]), PinDefinitionName = "贴Logo按压气缸上极限", PinStatus = KEE.IODataGridView.Properties.Resources.RedBtn });
            //lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = int.Parse(GlobalVar.pushCylinerLowLim.Split(',')[0]), PinDefinition = int.Parse(GlobalVar.pushCylinerLowLim.Split(',')[1]), PinDefinitionName = "贴Logo按压气缸下极限", PinStatus = KEE.IODataGridView.Properties.Resources.RedBtn });
            //lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = int.Parse(GlobalVar.feedVacuumLim.Split(',')[0]), PinDefinition = int.Parse(GlobalVar.feedVacuumLim.Split(',')[1]), PinDefinitionName = "上料真空负压表阈值", PinStatus = KEE.IODataGridView.Properties.Resources.RedBtn });
            //lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = int.Parse(GlobalVar.takeVacuumLim.Split(',')[0]), PinDefinition = int.Parse(GlobalVar.takeVacuumLim.Split(',')[1]), PinDefinitionName = "下料真空负压表阈值", PinStatus = KEE.IODataGridView.Properties.Resources.RedBtn });
            //lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = int.Parse(GlobalVar.feedStationSensor.Split(',')[0]), PinDefinition = int.Parse(GlobalVar.feedStationSensor.Split(',')[1]), PinDefinitionName = "上料工站料感信号", PinStatus = KEE.IODataGridView.Properties.Resources.RedBtn });
            //lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = int.Parse(GlobalVar.labelStationSensor.Split(',')[0]), PinDefinition = int.Parse(GlobalVar.labelStationSensor.Split(',')[1]), PinDefinitionName = "贴标工站料感信号", PinStatus = KEE.IODataGridView.Properties.Resources.RedBtn });
            //lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = int.Parse(GlobalVar.pressStationSensor.Split(',')[0]), PinDefinition = int.Parse(GlobalVar.pressStationSensor.Split(',')[1]), PinDefinitionName = "压合工站料感信号", PinStatus = KEE.IODataGridView.Properties.Resources.RedBtn });
            //lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = int.Parse(GlobalVar.highTestStationSensor.Split(',')[0]), PinDefinition = int.Parse(GlobalVar.highTestStationSensor.Split(',')[1]), PinDefinitionName = "测高工站料感信号", PinStatus = KEE.IODataGridView.Properties.Resources.RedBtn });
            //lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = int.Parse(GlobalVar.aoiTestStationSensor.Split(',')[0]), PinDefinition = int.Parse(GlobalVar.aoiTestStationSensor.Split(',')[1]), PinDefinitionName = "AOI偏位测试工站料感信号", PinStatus = KEE.IODataGridView.Properties.Resources.RedBtn });
            //lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = int.Parse(GlobalVar.takeStationSensor.Split(',')[0]), PinDefinition = int.Parse(GlobalVar.takeStationSensor.Split(',')[1]), PinDefinitionName = "下料工站料感信号", PinStatus = KEE.IODataGridView.Properties.Resources.RedBtn });
            //lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = int.Parse(GlobalVar.feed4PcsLogoFinished.Split(',')[0]), PinDefinition = int.Parse(GlobalVar.feed4PcsLogoFinished.Split(',')[1]), PinDefinitionName = "Epson通知取完4片Logo信号", PinStatus = KEE.IODataGridView.Properties.Resources.RedBtn });
            //lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = int.Parse(GlobalVar.tearLabelStart.Split(',')[0]), PinDefinition = int.Parse(GlobalVar.tearLabelStart.Split(',')[1]), PinDefinitionName = "Epson通知撕标开始信号", PinStatus = KEE.IODataGridView.Properties.Resources.RedBtn });
            //lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = int.Parse(GlobalVar.newLabelStart.Split(',')[0]), PinDefinition = int.Parse(GlobalVar.newLabelStart.Split(',')[1]), PinDefinitionName = "Epson通知出标开始信号", PinStatus = KEE.IODataGridView.Properties.Resources.RedBtn });
            //lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = int.Parse(GlobalVar.labelLogoToPlasticFinished.Split(',')[0]), PinDefinition = int.Parse(GlobalVar.labelLogoToPlasticFinished.Split(',')[1]), PinDefinitionName = "贴电镀+背胶完成", PinStatus = KEE.IODataGridView.Properties.Resources.RedBtn });


            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(epsonStandbying.Split(',')[0]), PinDefinition = ushort.Parse(epsonStandbying.Split(',')[1]), PinDefinitionName = "机器人待机中" });
            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(epsonRunning.Split(',')[0]), PinDefinition = ushort.Parse(epsonRunning.Split(',')[1]), PinDefinitionName = "机器人运行中" });
            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(epsonPausing.Split(',')[0]), PinDefinition = ushort.Parse(epsonPausing.Split(',')[1]), PinDefinitionName = "机器人暂停中" });
            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(epsonControllerErr.Split(',')[0]), PinDefinition = ushort.Parse(epsonControllerErr.Split(',')[1]), PinDefinitionName = "机器人控制器一般错误" });
            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(epsonEMGOutput.Split(',')[0]), PinDefinition = ushort.Parse(epsonEMGOutput.Split(',')[1]), PinDefinitionName = "机器人急停输出" });
            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(epsonSafeDoor.Split(',')[0]), PinDefinition = ushort.Parse(epsonSafeDoor.Split(',')[1]), PinDefinitionName = "机器人安全门打开" });
            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(epsonControllerFatalErr.Split(',')[0]), PinDefinition = ushort.Parse(epsonControllerFatalErr.Split(',')[1]), PinDefinitionName = "机器人控制器严重错误" });
            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(epsonAlarm.Split(',')[0]), PinDefinition = ushort.Parse(epsonAlarm.Split(',')[1]), PinDefinitionName = "机器人报警" });
            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(yuShinBlowingFinished.Split(',')[0]), PinDefinition = ushort.Parse(yuShinBlowingFinished.Split(',')[1]), PinDefinitionName = "有信放料完成" });
            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(deviceBoot.Split(',')[0]), PinDefinition = ushort.Parse(deviceBoot.Split(',')[1]), PinDefinitionName = "设备启动按钮" });
            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(deviceStop.Split(',')[0]), PinDefinition = ushort.Parse(deviceStop.Split(',')[1]), PinDefinitionName = "设备停止按钮" });
            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(logo4FeedFinished.Split(',')[0]), PinDefinition = ushort.Parse(logo4FeedFinished.Split(',')[1]), PinDefinitionName = "电镀件补料完成" });
            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(rotatingCylinderLeftLim.Split(',')[0]), PinDefinition = ushort.Parse(rotatingCylinderLeftLim.Split(',')[1]), PinDefinitionName = "旋转气缸左极限" });
            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(rotatingCylinderRightLim.Split(',')[0]), PinDefinition = ushort.Parse(rotatingCylinderRightLim.Split(',')[1]), PinDefinitionName = "旋转气缸右极限" });
            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(feedCylinderHighLim.Split(',')[0]), PinDefinition = ushort.Parse(feedCylinderHighLim.Split(',')[1]), PinDefinitionName = "上料气缸上极限" });
            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(feedCylinderLowLim.Split(',')[0]), PinDefinition = ushort.Parse(feedCylinderLowLim.Split(',')[1]), PinDefinitionName = "上料气缸下极限" });
            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(labelCylinderHighLim.Split(',')[0]), PinDefinition = ushort.Parse(labelCylinderHighLim.Split(',')[1]), PinDefinitionName = "出标轴下压气缸上极限" });
            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(labelCylinderLowLim.Split(',')[0]), PinDefinition = ushort.Parse(labelCylinderLowLim.Split(',')[1]), PinDefinitionName = "出标轴下压气缸下极限" });
            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(labelCylinderLeftLim.Split(',')[0]), PinDefinition = ushort.Parse(labelCylinderLeftLim.Split(',')[1]), PinDefinitionName = "出标轴出标气缸左极限" });
            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(labelCylinderRightLim.Split(',')[0]), PinDefinition = ushort.Parse(labelCylinderRightLim.Split(',')[1]), PinDefinitionName = "出标轴出标气缸右极限" });
            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(pressCylinderHighLim.Split(',')[0]), PinDefinition = ushort.Parse(pressCylinderHighLim.Split(',')[1]), PinDefinitionName = "压合气缸上极限" });
            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(pressCylinderLowLim.Split(',')[0]), PinDefinition = ushort.Parse(pressCylinderLowLim.Split(',')[1]), PinDefinitionName = "压合气缸下极限" });
            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(highTestCylinderHighLim.Split(',')[0]), PinDefinition = ushort.Parse(highTestCylinderHighLim.Split(',')[1]), PinDefinitionName = "测高气缸上极限" });
            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(highTestCylinderLowLim.Split(',')[0]), PinDefinition = ushort.Parse(highTestCylinderLowLim.Split(',')[1]), PinDefinitionName = "测高气缸下极限" });
            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(takeCylinerHighLim.Split(',')[0]), PinDefinition = ushort.Parse(takeCylinerHighLim.Split(',')[1]), PinDefinitionName = "下料气缸上极限" });
            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(takeCylinerLowLim.Split(',')[0]), PinDefinition = ushort.Parse(takeCylinerLowLim.Split(',')[1]), PinDefinitionName = "下料气缸下极限" });
            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(pushCylinerHighLim.Split(',')[0]), PinDefinition = ushort.Parse(pushCylinerHighLim.Split(',')[1]), PinDefinitionName = "贴Logo按压气缸上极限" });
            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(pushCylinerLowLim.Split(',')[0]), PinDefinition = ushort.Parse(pushCylinerLowLim.Split(',')[1]), PinDefinitionName = "贴Logo按压气缸下极限" });
            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(feedVacuumLim.Split(',')[0]), PinDefinition = ushort.Parse(feedVacuumLim.Split(',')[1]), PinDefinitionName = "上料真空负压表阈值" });
            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(takeVacuumLim.Split(',')[0]), PinDefinition = ushort.Parse(takeVacuumLim.Split(',')[1]), PinDefinitionName = "下料真空负压表阈值" });
            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(feedStationSensor.Split(',')[0]), PinDefinition = ushort.Parse(feedStationSensor.Split(',')[1]), PinDefinitionName = "上料工站料感信号" });
            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(labelStationSensor.Split(',')[0]), PinDefinition = ushort.Parse(labelStationSensor.Split(',')[1]), PinDefinitionName = "贴标工站料感信号" });
            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(pressStationSensor.Split(',')[0]), PinDefinition = ushort.Parse(pressStationSensor.Split(',')[1]), PinDefinitionName = "压合工站料感信号" });
            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(highTestStationSensor.Split(',')[0]), PinDefinition = ushort.Parse(highTestStationSensor.Split(',')[1]), PinDefinitionName = "测高工站料感信号" });
            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(aoiTestStationSensor.Split(',')[0]), PinDefinition = ushort.Parse(aoiTestStationSensor.Split(',')[1]), PinDefinitionName = "AOI偏位测试工站料感信号" });
            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(takeStationSensor.Split(',')[0]), PinDefinition = ushort.Parse(takeStationSensor.Split(',')[1]), PinDefinitionName = "下料工站料感信号" });
            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(feed4PcsLogoFinished.Split(',')[0]), PinDefinition = ushort.Parse(feed4PcsLogoFinished.Split(',')[1]), PinDefinitionName = "Epson通知取完4片Logo信号" });
            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(tearLabelStart.Split(',')[0]), PinDefinition = ushort.Parse(tearLabelStart.Split(',')[1]), PinDefinitionName = "Epson通知撕标开始信号" });
            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(newLabelStart.Split(',')[0]), PinDefinition = ushort.Parse(newLabelStart.Split(',')[1]), PinDefinitionName = "Epson通知出标开始信号" });
            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(labelLogoToPlasticFinished.Split(',')[0]), PinDefinition = ushort.Parse(labelLogoToPlasticFinished.Split(',')[1]), PinDefinitionName = "贴电镀+背胶完成" });
        }
        public static void InitAxiasDO()
        {
            if (lsAxiasDOs.Count > 0)
            {
                lsAxiasDOs.Clear();
            }
            lsAxiasDOs.Add(new LsIODOPinDefinition() { Card = ushort.Parse(bootEpson.Split(',')[0]), PinDefinition = ushort.Parse(bootEpson.Split(',')[1]), PinDefinitionName = "启动机器人程序" });
            lsAxiasDOs.Add(new LsIODOPinDefinition() { Card = ushort.Parse(prog1Selected.Split(',')[0]), PinDefinition = ushort.Parse(prog1Selected.Split(',')[1]), PinDefinitionName = "机器人程序1" });
            lsAxiasDOs.Add(new LsIODOPinDefinition() { Card = ushort.Parse(prog2Selected.Split(',')[0]), PinDefinition = ushort.Parse(prog2Selected.Split(',')[1]), PinDefinitionName = "机器人程序2" });
            lsAxiasDOs.Add(new LsIODOPinDefinition() { Card = ushort.Parse(prog3Selected.Split(',')[0]), PinDefinition = ushort.Parse(prog3Selected.Split(',')[1]), PinDefinitionName = "机器人程序3" });
            lsAxiasDOs.Add(new LsIODOPinDefinition() { Card = ushort.Parse(stopEpson.Split(',')[0]), PinDefinition = ushort.Parse(stopEpson.Split(',')[1]), PinDefinitionName = "停止机器人" });
            lsAxiasDOs.Add(new LsIODOPinDefinition() { Card = ushort.Parse(pausedEpson.Split(',')[0]), PinDefinition = ushort.Parse(pausedEpson.Split(',')[1]), PinDefinitionName = "暂停机器人" });
            lsAxiasDOs.Add(new LsIODOPinDefinition() { Card = ushort.Parse(continuedEpson.Split(',')[0]), PinDefinition = ushort.Parse(continuedEpson.Split(',')[1]), PinDefinitionName = "继续机器人" });
            lsAxiasDOs.Add(new LsIODOPinDefinition() { Card = ushort.Parse(resetEpson.Split(',')[0]), PinDefinition = ushort.Parse(resetEpson.Split(',')[1]), PinDefinitionName = "复位机器人" });
            lsAxiasDOs.Add(new LsIODOPinDefinition() { Card = ushort.Parse(allowYuShinTakePro.Split(',')[0]), PinDefinition = ushort.Parse(allowYuShinTakePro.Split(',')[1]), PinDefinitionName = "允许有信放料" });
            lsAxiasDOs.Add(new LsIODOPinDefinition() { Card = ushort.Parse(redAlarmLight.Split(',')[0]), PinDefinition = ushort.Parse(redAlarmLight.Split(',')[1]), PinDefinitionName = "三色灯-红灯" });
            lsAxiasDOs.Add(new LsIODOPinDefinition() { Card = ushort.Parse(yellowAlarmLight.Split(',')[0]), PinDefinition = ushort.Parse(yellowAlarmLight.Split(',')[1]), PinDefinitionName = "三色灯-黄灯" });
            lsAxiasDOs.Add(new LsIODOPinDefinition() { Card = ushort.Parse(greenAlarmLight.Split(',')[0]), PinDefinition = ushort.Parse(greenAlarmLight.Split(',')[1]), PinDefinitionName = "三色灯-绿灯" });
            lsAxiasDOs.Add(new LsIODOPinDefinition() { Card = ushort.Parse(buzzingAlarm.Split(',')[0]), PinDefinition = ushort.Parse(buzzingAlarm.Split(',')[1]), PinDefinitionName = "三色灯-蜂鸣" });
            lsAxiasDOs.Add(new LsIODOPinDefinition() { Card = ushort.Parse(feedFinishedOut.Split(',')[0]), PinDefinition = ushort.Parse(feedFinishedOut.Split(',')[1]), PinDefinitionName = "补料完成->按钮绿灯亮" });
            lsAxiasDOs.Add(new LsIODOPinDefinition() { Card = ushort.Parse(feedStandbyingOut.Split(',')[0]), PinDefinition = ushort.Parse(feedStandbyingOut.Split(',')[1]), PinDefinitionName = "待补料->按钮红灯亮" });
            lsAxiasDOs.Add(new LsIODOPinDefinition() { Card = ushort.Parse(rotatingCylinderElecValve.Split(',')[0]), PinDefinition = ushort.Parse(rotatingCylinderElecValve.Split(',')[1]), PinDefinitionName = "旋转气缸电磁阀" });
            lsAxiasDOs.Add(new LsIODOPinDefinition() { Card = ushort.Parse(feedCylinderElecValve.Split(',')[0]), PinDefinition = ushort.Parse(feedCylinderElecValve.Split(',')[1]), PinDefinitionName = "上料气缸电磁阀" });
            lsAxiasDOs.Add(new LsIODOPinDefinition() { Card = ushort.Parse(feedVacuumSolenoid.Split(',')[0]), PinDefinition = ushort.Parse(feedVacuumSolenoid.Split(',')[1]), PinDefinitionName = "上料真空电磁阀" });
            lsAxiasDOs.Add(new LsIODOPinDefinition() { Card = ushort.Parse(labelPressCylinderElecValve.Split(',')[0]), PinDefinition = ushort.Parse(labelPressCylinderElecValve.Split(',')[1]), PinDefinitionName = "出标轴下压气缸电磁阀" });
            lsAxiasDOs.Add(new LsIODOPinDefinition() { Card = ushort.Parse(labelOutCylinderElecValve.Split(',')[0]), PinDefinition = ushort.Parse(labelOutCylinderElecValve.Split(',')[1]), PinDefinitionName = "出标轴出标气缸电磁阀" });
            lsAxiasDOs.Add(new LsIODOPinDefinition() { Card = ushort.Parse(pressCylinderElecValve.Split(',')[0]), PinDefinition = ushort.Parse(pressCylinderElecValve.Split(',')[1]), PinDefinitionName = "压合气缸电磁阀" });
            lsAxiasDOs.Add(new LsIODOPinDefinition() { Card = ushort.Parse(highTestCylinderElecValve.Split(',')[0]), PinDefinition = ushort.Parse(highTestCylinderElecValve.Split(',')[1]), PinDefinitionName = "测高气缸电磁阀" });
            lsAxiasDOs.Add(new LsIODOPinDefinition() { Card = ushort.Parse(takeCylinderElecValve.Split(',')[0]), PinDefinition = ushort.Parse(takeCylinderElecValve.Split(',')[1]), PinDefinitionName = "下料气缸电磁阀" });
            lsAxiasDOs.Add(new LsIODOPinDefinition() { Card = ushort.Parse(takeVacuumSolenoid.Split(',')[0]), PinDefinition = ushort.Parse(takeVacuumSolenoid.Split(',')[1]), PinDefinitionName = "下料真空电磁阀" });
            lsAxiasDOs.Add(new LsIODOPinDefinition() { Card = ushort.Parse(pushCylinderElecValve.Split(',')[0]), PinDefinition = ushort.Parse(pushCylinderElecValve.Split(',')[1]), PinDefinitionName = "贴Logo按压气缸电磁阀" });
            lsAxiasDOs.Add(new LsIODOPinDefinition() { Card = ushort.Parse(allowEpsonFeedLogo.Split(',')[0]), PinDefinition = ushort.Parse(allowEpsonFeedLogo.Split(',')[1]), PinDefinitionName = "允许epson取Logo" });
            lsAxiasDOs.Add(new LsIODOPinDefinition() { Card = ushort.Parse(allowEpsonLabelGum.Split(',')[0]), PinDefinition = ushort.Parse(allowEpsonLabelGum.Split(',')[1]), PinDefinitionName = "允许epson贴背胶" });
            lsAxiasDOs.Add(new LsIODOPinDefinition() { Card = ushort.Parse(allowEpsonLabelPosMove.Split(',')[0]), PinDefinition = ushort.Parse(allowEpsonLabelPosMove.Split(',')[1]), PinDefinitionName = "允许epson在出标气缸回退到位后从贴标位置离开" });
            lsAxiasDOs.Add(new LsIODOPinDefinition() { Card = ushort.Parse(allowEpsonLabelLogoToPlastic.Split(',')[0]), PinDefinition = ushort.Parse(allowEpsonLabelLogoToPlastic.Split(',')[1]), PinDefinitionName = "允许epson贴Logo到塑胶件" });
        }
    }
}
