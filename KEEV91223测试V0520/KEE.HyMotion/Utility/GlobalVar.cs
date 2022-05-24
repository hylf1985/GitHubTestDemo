using ClassINI;
using KEE.HyMotion.MyMotion;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KEE.HyMotion.Utility
{
    public class GlobalVar
    {
        #region 来自射出机取料轴变量
        /// <summary>
        /// 轴停止模式
        /// </summary>
        public static StopModelEnum firAxiaStopModel = StopModelEnum.减速停止;
        /// <summary>
        /// 坐标模式
        /// </summary>
        public static CoordModelEnum firAxiaCoordModel = CoordModelEnum.绝对坐标;
        /// <summary>
        /// 起始速度
        /// </summary>
        public static double firStartSpeed = 1000;
        /// <summary>
        /// 运动速度
        /// </summary>
        public static double firMotionSpeed = 3000;
        /// <summary>
        /// 停止速度
        /// </summary>
        public static double firStopSpeed = 2000;
        /// <summary>
        /// 加速时间
        /// </summary>
        public static double firAccTime = 0.1;
        /// <summary>
        /// 减速时间
        /// </summary>
        public static double firDecTime = 0.1;
        /// <summary>
        /// S段时间
        /// </summary>
        public static double firStime = 0.01;

        #endregion

        #region 路径变量
        /// <summary>
        /// 视觉1路径
        /// </summary>       
        public static string bVpp1FilePath = @"D:\Data\Vpp\TB1.vpp";
        /// <summary>
        /// 视觉2路径
        /// </summary> 
        public static string bVpp2FilePath = @"D:\Data\Vpp\TB2.vpp";
        /// <summary>
        /// 视觉3路径
        /// </summary> 
        public static string bVpp3FilePath = @"D:\Data\Vpp\TB3.vpp";
        /// <summary>
        /// 视觉4路径
        /// </summary> 
        public static string bVpp4FilePath = @"D:\Data\Vpp\TB4.vpp";
        /// <summary>
        /// 视觉5路径
        /// </summary> 
        public static string bVpp5FilePath = @"D:\Data\Vpp\TB5.vpp";
        /// <summary>
        /// 总配置路径
        /// </summary>
        public static string bConfigFilePath = @"D:\Data\Config\Config.ini";
        /// <summary>
        /// 雷诺Recipe路径
        /// </summary>
        public static string bRecipeRenaultFilePath = @"D:\Data\Recipe\Renault.ini";
        /// <summary>
        /// LadaRecipe路径
        /// </summary>
        public static string bRecipeLadaFilePath = @"D:\Data\Recipe\Lada.ini";
        /// <summary>
        /// IO配置路径
        /// </summary>
        public static string bDeviceIOFilePath = @"D:\Data\IO\IO.ini";
        /// <summary>
        /// 测试数据路径
        /// </summary>
        public static string bTestDataFilePath = @"D:\Data\MyData\";
        /// <summary>
        /// 报警日志路径
        /// </summary>
        public static string bAlarmLogFilePath = @"D:\Data\Alarm\";
        /// <summary>
        /// 事件追踪日志路径
        /// </summary>
        public static string bTraceLogFilePath = @"D:\Data\Trace\";
        /// <summary>
        /// 图片保存路径
        /// </summary>
        public static string bImageSavePath = @"Data\Image\";
        #endregion

        #region 第一工位视觉与机器人交互变量
        /// <summary>
        /// 没有贴背胶前取料工位标定或者正常测试的标记
        /// </summary>
        public static bool isCam1CalOrTest = false;
        /// <summary>
        /// 没有贴背胶前取料工位9点标定次数
        /// </summary>
        public static int noLabelLogoNineCnt = 0;
        /// <summary>
        /// 没有贴背胶前取料工位机器人X坐标值
        /// </summary>
        public static double noLabelLogoRobotX = 0;
        /// <summary>
        /// 没有贴背胶前取料工位机器人Y坐标值
        /// </summary>
        public static double noLabelLogoRobotY = 0;
        /// <summary>
        /// 没有贴背胶前取料工位X方向比例尺
        /// </summary>
        public static double noLabelLogoRatioX = 0;
        /// <summary>
        /// 没有贴背胶前取料工位Y方向比例尺
        /// </summary>
        public static double noLabelLogoRatioY = 0;
        /// <summary>
        /// 没有贴背胶前取料工位特征X坐标值
        /// </summary>
        public static double noLabelLogoProCenterX = 0;
        /// <summary>
        /// 没有贴背胶前取料工位特征Y坐标值
        /// </summary>
        public static double noLabelLogoProCenterY = 0;
        /// <summary>
        /// 没有贴背胶前取料工位特征角度值
        /// </summary>
        public static double noLabelLogoProDeg = 0;
        /// <summary>
        /// 没有贴背胶前取料工位示教特征X坐标值
        /// </summary>
        public static double noLabelLogoProTeachCenterX = 0;
        /// <summary>
        /// 没有贴背胶前取料工位示教特征Y坐标值
        /// </summary>
        public static double noLabelLogoProTeachCenterY = 0;
        /// <summary>
        /// 没有贴背胶前取料工位示教特征角度值
        /// </summary>
        public static double noLabelLogoProTeachDeg = 0;
        /// <summary>
        /// 标定特征是否寻找到
        /// </summary>
        public static bool noLabelLogoIsCalMatchNG = false;
        /// <summary>
        /// 测试特征是否寻找到
        /// </summary>
        public static bool noLabelLogoIsMatchNG = false;
        #endregion

        #region 第二工位视觉与机器人交互变量
        /// <summary>
        /// 贴背胶前背胶工位标定或者正常测试的标记
        /// </summary>
        public static bool isCam2CalOrTest = false;
        /// <summary>
        /// 贴背胶前背胶拍照9点标定次数
        /// </summary>
        public static int noGumNineCnt = 0;
        /// <summary>
        ///  贴背胶前背胶工位机器人X坐标值
        /// </summary>
        public static double noGumRobotX = 0;
        /// <summary>
        /// 贴背胶前背胶工位机器人Y坐标值
        /// </summary>
        public static double noGumRobotY = 0;
        /// <summary>
        /// 贴背胶前背胶工位机器人Y坐标值
        /// </summary>
        public static double noGumRobotU = 0;
        /// <summary>
        /// 贴背胶前背胶工位机器人工具坐标系示教X坐标值
        /// </summary>
        public static double noGumTeachRobotTX = 0;
        /// <summary>
        /// 贴背胶前背胶工位机器人工具坐标系示教Y坐标值
        /// </summary>
        public static double noGumTeachRobotTY = 0;
        /// <summary>
        /// 贴背胶前背胶工位机器人工具坐标系示教U轴坐标值
        /// </summary>
        public static double noGumTeachRobotTU = 0;
        /// <summary>
        /// 没有贴背胶前取料工位X方向比例尺
        /// </summary>
        public static double noGumRatioX = 0;
        /// <summary>
        /// 没有贴背胶前取料工位Y方向比例尺
        /// </summary>
        public static double noGumRatioY = 0;
        /// <summary>
        /// 没有贴背胶前取料工位特征X坐标值
        /// </summary>
        public static double noGumProCenterX = 0;
        /// <summary>
        /// 没有贴背胶前取料工位特征Y坐标值
        /// </summary>
        public static double noGumProCenterY = 0;
        /// <summary>
        /// 没有贴背胶前取料工位特征角度值
        /// </summary>
        public static double noGumProDeg = 0;
        /// <summary>
        /// 没有贴背胶前取料工位特征示教X坐标值
        /// </summary>
        public static double noGumProTeachCenterX = 0;
        /// <summary>
        /// 没有贴背胶前取料工位特征示教Y坐标值
        /// </summary>
        public static double noGumProTeachCenterY = 0;
        /// <summary>
        /// 没有贴背胶前取料工位特征示教角度值
        /// </summary>
        public static double noGumProTeachDeg = 0;
        /// <summary>
        /// 标定特征是否寻找到
        /// </summary>
        public static bool noGumIsCalMatchNG = false;
        /// <summary>
        /// 测试特征是否寻找到
        /// </summary>
        public static bool noGumIsMatchNG = false;
        #endregion

        #region 第三工位视觉与机器人交互变量
        /// <summary>
        /// 没有贴背胶前取料工位标定或者正常测试的标记
        /// </summary>
        public static bool isCam3CalOrTest = false;
        /// <summary>
        /// 没有贴背胶前取料工位9点标定次数
        /// </summary>
        public static int labeledLogoNineCnt = 0;
        /// <summary>
        /// 没有贴背胶前取料工位工具路径
        /// </summary>
        public static string bFileLabeledLogoToolPath = Application.StartupPath + @"\Tool\TB3.vpp";
        /// <summary>
        /// 没有贴背胶前取料工位机器人X坐标值
        /// </summary>
        public static double labeledLogoRobotX = 0;
        /// <summary>
        /// 没有贴背胶前取料工位机器人Y坐标值
        /// </summary>
        public static double labeledLogoRobotY = 0;
        /// <summary>
        /// 没有贴背胶前取料工位X方向比例尺
        /// </summary>
        public static double labeledLogoRatioX = 0;
        /// <summary>
        /// 没有贴背胶前取料工位Y方向比例尺
        /// </summary>
        public static double labeledLogoRatioY = 0;
        /// <summary>
        /// 没有贴背胶前取料工位特征X坐标值
        /// </summary>
        public static double labeledLogoProCenterX = 0;
        /// <summary>
        /// 没有贴背胶前取料工位特征Y坐标值
        /// </summary>
        public static double labeledLogoProCenterY = 0;
        /// <summary>
        /// 没有贴背胶前取料工位特征角度值
        /// </summary>
        public static double labeledLogoProDeg = 0;
        /// <summary>
        /// 没有贴背胶前取料工位示教特征X坐标值
        /// </summary>
        public static double labeledLogoProTeachCenterX = 0;
        /// <summary>
        /// 没有贴背胶前取料工位示教特征Y坐标值
        /// </summary>
        public static double labeledLogoProTeachCenterY = 0;
        /// <summary>
        /// 没有贴背胶前取料工位示教特征角度值
        /// </summary>
        public static double labeledLogoProTeachDeg = 0;
        /// <summary>
        /// 轴中心在该相机下的坐标X值,通过5点标定获取
        /// </summary>
        public static double labeledLogoRaxiasCenterX = 0;
        /// <summary>
        /// 轴中心在该相机下的坐标Y值,通过5点标定获取
        /// </summary>
        public static double labeledLogoRaxiasCenterY = 0;
        /// <summary>
        /// 标定特征是否寻找到
        /// </summary>
        public static bool labeledLogoIsCalMatchNG = false;
        /// <summary>
        /// 测试特征是否寻找到
        /// </summary>
        public static bool labeledLogoIsMatchNG = false;
        #endregion

        #region 第四工位视觉与机器人交互变量
        /// <summary>
        /// 贴背胶后背胶工位标定或者正常测试的标记
        /// </summary>
        public static bool isCam4CalOrTest = false;
        /// <summary>
        /// 贴背胶前背胶工位工具路径
        /// </summary>
        public static string bFileGumedToolPath = Application.StartupPath + @"\Tool\TB4.vpp";
        /// <summary>
        /// 贴背胶前背胶拍照9点标定次数
        /// </summary>
        public static int gumedNineCnt = 0;
        /// <summary>
        ///  贴背胶前背胶工位机器人X坐标值
        /// </summary>
        public static double gumedRobotX = 0;
        /// <summary>
        /// 贴背胶前背胶工位机器人Y坐标值
        /// </summary>
        public static double gumedRobotY = 0;
        /// <summary>
        /// 贴背胶前背胶工位机器人Y坐标值
        /// </summary>
        public static double gumedRobotU = 0;
        /// <summary>
        /// 贴背胶前背胶工位机器人工具坐标系示教X坐标值
        /// </summary>
        public static double gumedTeachRobotTX = 0;
        /// <summary>
        /// 贴背胶前背胶工位机器人工具坐标系示教Y坐标值
        /// </summary>
        public static double gumedTeachRobotTY = 0;
        /// <summary>
        /// 贴背胶前背胶工位机器人工具坐标系示教U轴坐标值
        /// </summary>
        public static double gumedTeachRobotTU = 0;
        /// <summary>
        /// 没有贴背胶前取料工位X方向比例尺
        /// </summary>
        public static double gumedRatioX = 0;
        /// <summary>
        /// 没有贴背胶前取料工位Y方向比例尺
        /// </summary>
        public static double gumedRatioY = 0;
        /// <summary>
        /// 没有贴背胶前取料工位特征X坐标值
        /// </summary>
        public static double gumedProCenterX = 0;
        /// <summary>
        /// 没有贴背胶前取料工位特征Y坐标值
        /// </summary>
        public static double gumedProCenterY = 0;
        /// <summary>
        /// 没有贴背胶前取料工位特征角度值
        /// </summary>
        public static double gumedProDeg = 0;
        /// <summary>
        /// 没有贴背胶前取料工位特征示教X坐标值
        /// </summary>
        public static double gumedProTeachCenterX = 0;
        /// <summary>
        /// 没有贴背胶前取料工位特征示教Y坐标值
        /// </summary>
        public static double gumedProTeachCenterY = 0;
        /// <summary>
        /// 没有贴背胶前取料工位特征示教角度值
        /// </summary>
        public static double gumedProTeachDeg = 0;
        /// <summary>
        /// 标定特征是否寻找到
        /// </summary>
        public static bool gumedIsCalMatchNG = false;
        /// <summary>
        /// 测试特征是否寻找到
        /// </summary>
        public static bool gumedIsMatchNG = false;
        #endregion 

        #region 系统变量
        /// <summary>
        /// 服务器端口
        /// </summary>
        public static int serverPort = 9000;
        /// <summary>
        /// 当前选择的产品
        /// </summary>
        public static string curProName = "Renault";
        /// <summary>
        /// 工单号
        /// </summary>
        public static string curWo = "BNAA-0123456789";
        /// <summary>
        /// 料号
        /// </summary>
        public static string curCusNumber = "A3C0734480000";
        /// <summary>
        /// 程序是否随电脑开启一起启动:true：是;false：否
        /// </summary>
        public static bool powerIsOnOrOff = false;
        /// <summary>
        /// 是否正确加载所有参数的标记
        /// </summary>
        public static bool isLoadAllParam = false;
        /// <summary>
        /// 流水号计数器
        /// </summary>
        public static int snCnt = 0;
        #endregion

        #region 测高模组变量
        /// <summary>
        /// 测高模组485串口，采用RTU通讯
        /// </summary>
        public static string highIndCom = "COM5";
        /// <summary>
        /// 测高模组左空载治具高度
        /// </summary>
        public static double highIndSpaceHigh = 0.0;
        /// <summary>
        /// 测高模组初始值
        /// </summary>
        public static double highIndInitHighVal = 0.0;
        /// <summary>
        /// 测高模组延迟时间
        /// </summary>
        public static double highIndInitDelayTime = 0.0;
        /// <summary>
        /// 测高模组最大值
        /// </summary>
        public static double highIndMaxVal = 0.0;
        /// <summary>
        /// 测高模组最小值
        /// </summary>
        public static double highIndMinVal = 0.0;

        #endregion

        #region 打印机变量
        public static string printX = "";
        public static string printY = "";
        #endregion

        #region 频闪控制器变量
        public static string lightContrCom = "Com2";
        public static string lightCH1OpenCmd = "";
        public static string lightCH1CloseCmd = "";
        public static string lightCH2OpenCmd = "";
        public static string lightCH2CloseCmd = "";
        public static string lightCH3OpenCmd = "";
        public static string lightCH3CloseCmd = "";
        #endregion

        #region  射出取料轴变量
        /// <summary>
        /// 停止模式
        /// </summary>
        public static StopModelEnum injectFeedStopModel = StopModelEnum.减速停止;
        /// <summary>
        /// 坐标模式
        /// </summary>
        public static CoordModelEnum injectFeedCoordModel = CoordModelEnum.绝对坐标;
        /// <summary>
        /// 起始速度
        /// </summary>
        public static double injectFeedStartSpeed = 1000;
        /// <summary>
        /// 正常运行速度
        /// </summary>
        public static double injectFeedMotionSpeed = 3000;
        /// <summary>
        /// 停止速度
        /// </summary>
        public static double injectFeedStopSpeed = 2000;
        /// <summary>
        /// 加速时间
        /// </summary>
        public static double injectFeedAccTime = 0.1;
        /// <summary>
        /// 减速时间
        /// </summary>
        public static double injectFeedDccTime = 0.1;
        /// <summary>
        /// S段时间
        /// </summary>
        public static double injectFeedSTime = 0.01;
        /// <summary>
        /// 回零低速
        /// </summary>
        public static double injectFeedHomeLowSpeed = 1000;
        /// <summary>
        /// 回零高速
        /// </summary>
        public static double injectFeedHomeHighSpeed = 3000;
        /// <summary>
        /// 回零模式
        /// </summary>
        public static HomeModel injectFeedHomeModel = HomeModel.一次回零;
        /// <summary>
        /// 回零速度模式
        /// </summary>
        public static HomeSpeedModel injectFeedHomeSpeedModel = HomeSpeedModel.低速回零;
        /// <summary>
        /// 射出机放料位置
        /// </summary>
        public static double injectTakeProPos = 1;
        /// <summary>
        /// 上料位置1
        /// </summary>
        public static double injectFeedProPos1 = 2;
        /// <summary>
        /// 上料位置2
        /// </summary>
        public static double injectFeedProPos2 = 3;
        /// <summary>
        /// 正极限点位序号
        /// </summary>
        public static int injectFeedDiPLimP = 1;
        /// <summary>
        /// 负极限点位序号
        /// </summary>
        public static int injectFeedDiNLimP = 2;
        /// <summary>
        /// 原点位序号
        /// </summary>
        public static int injectFeedDiHLimP = 3;
        /// <summary>
        /// 注塑机放料完成点位序号
        /// </summary>
        public static int injectFeedDiInjectFinishedP = 4;
        /// <summary>
        /// 告知注塑机允许放料点位序号
        /// </summary>
        public static int injectFeedDoAllowInjectFinishedP = 1;
        #endregion

        #region 上料轴变量
        /// <summary>
        /// 停止模式
        /// </summary>
        public static StopModelEnum feedStopModel = StopModelEnum.减速停止;
        /// <summary>
        /// 坐标模式
        /// </summary>
        public static CoordModelEnum feedCoordModel = CoordModelEnum.绝对坐标;
        /// <summary>
        /// 起始速度
        /// </summary>
        public static double feedStartSpeed = 1000;
        /// <summary>
        /// 正常运行速度
        /// </summary>
        public static double feedMotionSpeed = 3000;
        /// <summary>
        /// 停止速度
        /// </summary>
        public static double feedStopSpeed = 2000;
        /// <summary>
        /// 加速时间
        /// </summary>
        public static double feedAccTime = 0.1;
        /// <summary>
        /// 减速时间
        /// </summary>
        public static double feedDccTime = 0.1;
        /// <summary>
        /// S段时间
        /// </summary>
        public static double feedSTime = 0.01;
        /// <summary>
        /// 回零低速
        /// </summary>
        public static double feedHomeLowSpeed = 1000;
        /// <summary>
        /// 回零高速
        /// </summary>
        public static double feedHomeHighSpeed = 3000;
        /// <summary>
        /// 回零模式
        /// </summary>
        public static HomeModel feedHomeModel = HomeModel.一次回零;
        /// <summary>
        /// 回零速度模式
        /// </summary>
        public static HomeSpeedModel feedHomeSpeedModel = HomeSpeedModel.低速回零;
        /// <summary>
        /// 取料位置1
        /// </summary>
        public static double feedProPos1 = 1;
        /// <summary>
        /// 取料位置2
        /// </summary>
        public static double feedProPos2 = 2;
        /// <summary>
        /// 放料位置
        /// </summary>
        public static double feedTakeProPos = 3;
        /// <summary>
        /// 正极限点位序号
        /// </summary>
        public static int feedDiPLimP = 1;
        /// <summary>
        /// 负极限点位序号
        /// </summary>
        public static int feedDiNLimP = 2;
        /// <summary>
        /// 原点位序号
        /// </summary>
        public static int feedDiHLimP = 3;
        /// <summary>
        /// 气缸打出到位
        /// </summary>
        public static int feedDiCylinderHitInPlace = 4;
        /// <summary>
        /// 气缸退回到位
        /// </summary>
        public static int feedDiCylinderBackInPlace = 5;
        /// <summary>
        /// 真空吸附检测信号
        /// </summary>
        public static int feedDiVacuumAdsorptionDetection = 6;
        #endregion

        #region 上料R轴变量
        /// <summary>
        /// 停止模式
        /// </summary>
        public static StopModelEnum feedRStopModel = StopModelEnum.减速停止;
        /// <summary>
        /// 坐标模式
        /// </summary>
        public static CoordModelEnum feedRCoordModel = CoordModelEnum.绝对坐标;
        /// <summary>
        /// 起始速度
        /// </summary>
        public static double feedRStartSpeed = 1000;
        /// <summary>
        /// 正常运行速度
        /// </summary>
        public static double feedRMotionSpeed = 3000;
        /// <summary>
        /// 停止速度
        /// </summary>
        public static double feedRStopSpeed = 2000;
        /// <summary>
        /// 加速时间
        /// </summary>
        public static double feedRAccTime = 0.1;
        /// <summary>
        /// 减速时间
        /// </summary>
        public static double feedRDccTime = 0.1;
        /// <summary>
        /// S段时间
        /// </summary>
        public static double feedRSTime = 0.01;
        /// <summary>
        /// 回零低速
        /// </summary>
        public static double feedRHomeLowSpeed = 1000;
        /// <summary>
        /// 回零高速
        /// </summary>
        public static double feedRHomeHighSpeed = 3000;
        /// <summary>
        /// 回零模式
        /// </summary>
        public static HomeModel feedRHomeModel = HomeModel.一次回零;
        /// <summary>
        /// 回零速度模式
        /// </summary>
        public static HomeSpeedModel feedRHomeSpeedModel = HomeSpeedModel.低速回零;
        /// <summary>
        /// 取料位置1
        /// </summary>
        public static double feedRProPos1 = 1;
        /// <summary>
        /// 放料位置
        /// </summary>
        public static double feedRTakeProPos = 2;
        /// <summary>
        /// 原点位序号
        /// </summary>
        public static int feedRDiHLimP = 3;
        #endregion

        #region 丢料轴变量
        /// <summary>
        /// 停止模式
        /// </summary>
        public static StopModelEnum takeStopModel = StopModelEnum.减速停止;
        /// <summary>
        /// 坐标模式
        /// </summary>
        public static CoordModelEnum takeCoordModel = CoordModelEnum.绝对坐标;
        /// <summary>
        /// 起始速度
        /// </summary>
        public static double takeStartSpeed = 1000;
        /// <summary>
        /// 正常运行速度
        /// </summary>
        public static double takeMotionSpeed = 3000;
        /// <summary>
        /// 停止速度
        /// </summary>
        public static double takeStopSpeed = 2000;
        /// <summary>
        /// 加速时间
        /// </summary>
        public static double takeAccTime = 0.1;
        /// <summary>
        /// 减速时间
        /// </summary>
        public static double takeDccTime = 0.1;
        /// <summary>
        /// S段时间
        /// </summary>
        public static double takeSTime = 0.01;
        /// <summary>
        /// 回零低速
        /// </summary>
        public static double takeHomeLowSpeed = 1000;
        /// <summary>
        /// 回零高速
        /// </summary>
        public static double takeHomeHighSpeed = 3000;
        /// <summary>
        /// 回零模式
        /// </summary>
        public static HomeModel takeHomeModel = HomeModel.一次回零;
        /// <summary>
        /// 回零速度模式
        /// </summary>
        public static HomeSpeedModel takeHomeSpeedModel = HomeSpeedModel.低速回零;
        /// <summary>
        /// 取料位置
        /// </summary>
        public static double takeFeedProPos = 1;
        /// <summary>
        /// 丢OK产品位置
        /// </summary>
        public static double takeOKProPos = 2;
        /// <summary>
        /// 丢NG产品位置
        /// </summary>
        public static double takeNGProPos = 3;
        /// <summary>
        /// 正极限点位序号
        /// </summary>
        public static int takeDiPLimP = 1;
        /// <summary>
        /// 负极限点位序号
        /// </summary>
        public static int takeDiNLimP = 2;
        /// <summary>
        /// 原点位序号
        /// </summary>
        public static int takeDiHLimP = 3;
        /// <summary>
        /// 气缸打出到位
        /// </summary>
        public static int takeDiCylinderHitInPlace = 4;
        /// <summary>
        /// 气缸退回到位
        /// </summary>
        public static int takeDiCylinderBackInPlace = 5;
        /// <summary>
        /// 真空吸附检测信号
        /// </summary>
        public static int takeDiVacuumAdsorptionDetection = 6;
        #endregion

        #region 出标轴变量
        /// <summary>
        /// 停止模式
        /// </summary>
        public static StopModelEnum labelStopModel = StopModelEnum.减速停止;
        /// <summary>
        /// 坐标模式
        /// </summary>
        public static CoordModelEnum labelCoordModel = CoordModelEnum.绝对坐标;
        /// <summary>
        /// 起始速度
        /// </summary>
        public static double labelStartSpeed = 1000;
        /// <summary>
        /// 正常运行速度
        /// </summary>
        public static double labelMotionSpeed = 3000;
        /// <summary>
        /// 停止速度
        /// </summary>
        public static double labelStopSpeed = 2000;
        /// <summary>
        /// 加速时间
        /// </summary>
        public static double labelAccTime = 0.1;
        /// <summary>
        /// 减速时间
        /// </summary>
        public static double labelDccTime = 0.1;
        /// <summary>
        /// S段时间
        /// </summary>
        public static double labelSTime = 0.01;
        /// <summary>
        /// 出标轴运动方向
        /// </summary>
        public static int labelDirect = 1;
        /// <summary>
        /// 出标轴定长距离
        /// </summary>
        public static double labelFixedStepLength = 1000;
        /// <summary>
        /// 压标气缸打出到位
        /// </summary>
        public static int labelPressMarkCylinderHitInPlace = 1;
        /// <summary>
        /// 压标气缸退回到位
        /// </summary>
        public static int labelPressMarkCylinderBackInPlace = 2;
        /// <summary>
        /// 撕标气缸打出到位
        /// </summary>
        public static int labelTearMarkCylinderHitInPlace = 3;
        /// <summary>
        /// 撕标气缸退出到位
        /// </summary>
        public static int labelTearMarkCylinderBackInPlace = 4;
        /// <summary>
        /// 出标感应器
        /// </summary>
        public static int labelOutMarkSensor = 5;
        #endregion

        #region 凸轮分割器变量
        /// <summary>
        /// 凸轮分割器输出信号
        /// </summary>
        public static int camDividerDoP = 1;
        /// <summary>
        /// 上料料感信号
        /// </summary>
        public static int materialSensorFeedDiP = 1;
        /// <summary>
        /// 贴标料感信号
        /// </summary>
        public static int materialSensorLabelDiP = 2;
        /// <summary>
        /// 压合料感信号
        /// </summary>
        public static int materialSensorPressDiP = 3;
        /// <summary>
        /// 测高料感信号
        /// </summary>
        public static int materialSensorHighTestDiP = 4;
        /// <summary>
        /// AOI料号信号
        /// </summary>
        public static int materialSensorAoiOffsetDiP = 5;
        /// <summary>
        /// 丢料料感信号
        /// </summary>
        public static int materialSensorTakeDiP = 6;
        #endregion
        public static void Initpara()
        {
            try
            {
                if (File.Exists(bConfigFilePath))  //如果文件存在
                {
                    powerIsOnOrOff = Convert.ToBoolean(INI.INIGetStringValue(bConfigFilePath, "System", "程序是否开机启动", null));
                    serverPort = Convert.ToInt32(INI.INIGetStringValue(bConfigFilePath, "System", "服务器端口", null));
                    curProName = INI.INIGetStringValue(bConfigFilePath, "System", "产品选择", null);
                    curWo = INI.INIGetStringValue(bConfigFilePath, "System", "当前工单", null);
                    curCusNumber = INI.INIGetStringValue(bConfigFilePath, "System", "当前料号", null);
                    snCnt = Convert.ToInt32(INI.INIGetStringValue(bConfigFilePath, "System", "当前序列号", null));
                    noLabelLogoRatioX = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "第一工位比例尺", "X", null));
                    noLabelLogoRatioY = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "第一工位比例尺", "Y", null));
                    noLabelLogoProTeachCenterX = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "第一工位示教点", "X", null));
                    noLabelLogoProTeachCenterY = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "第一工位示教点", "Y", null));
                    noLabelLogoProTeachDeg = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "第一工位示教点", "角度", null));
                    noGumRatioX = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "第二工位比例尺", "X", null));
                    noGumRatioY = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "第二工位比例尺", "Y", null));
                    noGumProTeachCenterX = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "第二工位示教点", "X", null));
                    noGumProTeachCenterY = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "第二工位示教点", "Y", null));
                    noGumProTeachDeg = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "第二工位示教点", "角度", null));
                    noGumTeachRobotTX = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "第二工位Robot工具示教点", "X", null));
                    noGumTeachRobotTY = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "第二工位Robot工具示教点", "Y", null));
                    noGumTeachRobotTU = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "第二工位Robot工具示教点", "U", null));
                    highIndCom = INI.INIGetStringValue(bConfigFilePath, "测高模组", "串口号", null);
                    highIndSpaceHigh = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "测高模组", "空载具高度", null));
                    highIndInitDelayTime = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "测高模组", "延迟时间", null));
                    highIndInitHighVal = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "测高模组", "初始值", null));
                    printX = INI.INIGetStringValue(bConfigFilePath, "打印机", "X方向起点", null);
                    printY = INI.INIGetStringValue(bConfigFilePath, "打印机", "Y方向起点", null);
                }
                else
                {
                    FileStream fs = new FileStream(bConfigFilePath, FileMode.CreateNew, FileAccess.ReadWrite);
                    fs.Close();
                    //写入一批键值  通过引用的ClassINI文件内建置的方法完成如下操作；
                    INI.INIWriteItems(bConfigFilePath, "System", "程序是否开机启动=true\0操作权限=0\0服务器端口=9000\0产品选择=Renault\0当前工单=ABC002\0当前流水号=0\0当前料号=BCD323\0");
                    INI.INIWriteItems(bConfigFilePath, "测高模组", "初始值=0\0延迟时间=0\0串口号=Com1\0空载具高度=0\0");
                    INI.INIWriteItems(bConfigFilePath, "第一工位比例尺", "X=0\0Y=0\0");
                    INI.INIWriteItems(bConfigFilePath, "第一工位示教点", "X=0\0Y=0\0角度=0\0");
                    INI.INIWriteItems(bConfigFilePath, "第一工位轴中心点", "X=0\0Y=0\0");
                    INI.INIWriteItems(bConfigFilePath, "第二工位比例尺", "X=0\0Y=0\0");
                    INI.INIWriteItems(bConfigFilePath, "第二工位示教点", "X=0\0Y=0\0角度=0\0");
                    INI.INIWriteItems(bConfigFilePath, "第二工位Robot工具示教点", "X=0\0Y=0\0U=0\0");
                    INI.INIWriteItems(bConfigFilePath, "打印机", "X方向起点=0\0Y方向起点=0\0");
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
