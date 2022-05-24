using ClassINI;
using HY_Data;
using KEE.Renault.Common;
using KEE.Renault.Data;
using KEE.Renault.MyLog;
using KEE.Renault.MyMotion;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KEE.Renault.Utility
{
    public class GlobalVar
    {
        #region  轴卡变量
        /// <summary>
        /// 雷赛轴卡ID
        /// </summary>
        public static ushort CardId = 0;
        /// <summary>
        /// 与有信机械手关联的轴号
        /// </summary>
        public static ushort InjectFeedAxiasNumber = 0;
        /// <summary>
        /// 上料轴轴号
        /// </summary>
        public static ushort FeedAxiasNumber = 1;
        /// <summary>
        /// 上料R轴轴号
        /// </summary>
        public static ushort FeedRAxiasNumber = 2;
        /// <summary>
        /// 出标轴轴号
        /// </summary>
        public static ushort LabelingAxiasNumber = 4;
        /// <summary>
        /// 丢料轴
        /// </summary>
        public static ushort TakeAxiasNumber = 3;
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
        /// 图片保存路径
        /// </summary>
        public static string bImageSavePath = @"D:\Data\Image\";
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

        public static bool isFirAndSecIsShotOK = false;
        /// <summary>
        /// 第一工位和第二工位拍照NG允许的次数
        /// </summary>
        public static int firAndSecShotNGResetCount = 0;
        /// <summary>
        /// 第一工位和第二工位拍照NG是否拍过照
        /// </summary>
        public static bool firAndSecIsShot = false;
        /// <summary>
        /// 第一工位和第二工位拍照NG是否拍过照
        /// </summary>
        public static bool firAndSecIsAllowShot = false;
        
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
        /// 贴背胶前背胶工位X方向比例尺
        /// </summary>
        public static double noGumRatioX = 0;
        /// <summary>
        /// 贴背胶前背胶工位Y方向比例尺
        /// </summary>
        public static double noGumRatioY = 0;
        /// <summary>
        /// 贴背胶前背胶工位特征X坐标值
        /// </summary>
        public static double noGumProCenterX = 0;
        /// <summary>
        /// 贴背胶前背胶工位特征Y坐标值
        /// </summary>
        public static double noGumProCenterY = 0;
        /// <summary>
        /// 贴背胶前背胶工位特征角度值
        /// </summary>
        public static double noGumProDeg = 0;
        /// <summary>
        /// 贴背胶前背胶工位特征示教X坐标值
        /// </summary>
        public static double noGumProTeachCenterX = 0;
        /// <summary>
        /// 贴背胶前背胶工位特征示教Y坐标值
        /// </summary>
        public static double noGumProTeachCenterY = 0;
        /// <summary>
        /// 贴背胶前背胶工位特征示教角度值
        /// </summary>
        public static double noGumProTeachDeg = 0;
        /// <summary>
        /// 贴背胶前背胶工位标定特征是否寻找到
        /// </summary>
        public static bool noGumIsCalMatchNG = false;
        /// <summary>
        /// 贴背胶前背胶工位测试特征是否寻找到
        /// </summary>
        public static bool noGumIsMatchNG = false;
        /// <summary>
        /// 发送给机器人的X方向补偿值
        /// </summary>
        public static double noGumRobotOffsetX = 0;
        /// <summary>
        /// 发送给机器人的Y方向补偿值
        /// </summary>
        public static double noGumRobotOffsetY = 0;
        #endregion

        #region 第三工位视觉与机器人交互变量
        /// <summary>
        /// 贴背胶后Logo拍照工位标定或者正常测试的标记
        /// </summary>
        public static bool isCam3CalOrTest = false;
        /// <summary>
        /// 贴背胶后Logo拍照工位9点标定次数
        /// </summary>
        public static int labeledLogoNineCnt = 0;
        /// <summary>
        /// 贴背胶后Logo拍照工位机器人X坐标值
        /// </summary>
        public static double labeledLogoRobotX = 0;
        /// <summary>
        /// 贴背胶后Logo拍照工位机器人Y坐标值
        /// </summary>
        public static double labeledLogoRobotY = 0;
        /// <summary>
        /// 贴背胶后Logo拍照工位X方向比例尺
        /// </summary>
        public static double labeledLogoRatioX = 0;
        /// <summary>
        /// 贴背胶后Logo拍照工位Y方向比例尺
        /// </summary>
        public static double labeledLogoRatioY = 0;
        /// <summary>
        /// 贴背胶后Logo拍照工位特征X坐标值
        /// </summary>
        public static double labeledLogoProCenterX = 0;
        /// <summary>
        /// 贴背胶后Logo拍照工位特征Y坐标值
        /// </summary>
        public static double labeledLogoProCenterY = 0;
        /// <summary>
        /// 贴背胶后Logo拍照工位特征角度值
        /// </summary>
        public static double labeledLogoProDeg = 0;
        /// <summary>
        /// 贴背胶后Logo拍照工位示教特征X坐标值
        /// </summary>
        public static double labeledLogoProTeachCenterX = 0;
        /// <summary>
        /// 贴背胶后Logo拍照工位示教特征Y坐标值
        /// </summary>
        public static double labeledLogoProTeachCenterY = 0;
        /// <summary>
        /// 贴背胶后Logo拍照工位示教特征角度值
        /// </summary>
        public static double labeledLogoProTeachDeg = 0;
        /// <summary>
        /// 贴背胶后Logo拍照标定特征是否寻找到
        /// </summary>
        public static bool labeledLogoIsCalMatchNG = false;
        /// <summary>
        /// 贴背胶后Logo拍照测试特征是否寻找到
        /// </summary>
        public static bool labeledLogoIsMatchNG = false;
        #endregion

        #region 第四工位视觉与机器人交互变量
        /// <summary>
        /// 第三工位和第四工位拍照NG允许的次数
        /// </summary>
        //public static int thrAndFourShotNGResetCount = 0;
        /// <summary>
        /// 注塑件拍照工位标定或者正常测试的标记
        /// </summary>
        public static bool isCam4CalOrTest = false;
        /// <summary>
        /// 注塑件拍照拍照9点标定次数
        /// </summary>
        public static int gumedNineCnt = 0;
        /// <summary>
        ///  注塑件拍照工位机器人X坐标值
        /// </summary>
        public static double gumedRobotX = 0;
        /// <summary>
        /// 注塑件拍照工位机器人Y坐标值
        /// </summary>
        public static double gumedRobotY = 0;
        /// <summary>
        /// 注塑件拍照工位机器人Y坐标值
        /// </summary>
        public static double gumedRobotU = 0;
        /// <summary>
        /// 注塑件拍照工位机器人工具坐标系示教X坐标值
        /// </summary>
        public static double gumedTeachRobotTX = 0;
        /// <summary>
        /// 注塑件拍照工位机器人工具坐标系示教Y坐标值
        /// </summary>
        public static double gumedTeachRobotTY = 0;
        /// <summary>
        /// 注塑件拍照工位机器人工具坐标系示教U轴坐标值
        /// </summary>
        public static double gumedTeachRobotTU = 0;
        /// <summary>
        /// 注塑件拍照工位X方向比例尺
        /// </summary>
        public static double gumedRatioX = 0;
        /// <summary>
        /// 注塑件拍照工位Y方向比例尺
        /// </summary>
        public static double gumedRatioY = 0;
        /// <summary>
        /// 注塑件拍照工位特征X坐标值
        /// </summary>
        public static double gumedProCenterX = 0;
        /// <summary>
        /// 注塑件拍照工位特征Y坐标值
        /// </summary>
        public static double gumedProCenterY = 0;
        /// <summary>
        /// 注塑件拍照工位特征角度值
        /// </summary>
        public static double gumedProDeg = 0;
        /// <summary>
        /// 注塑件拍照工位特征示教X坐标值
        /// </summary>
        public static double gumedProTeachCenterX = 0;
        /// <summary>
        /// 注塑件拍照工位特征示教Y坐标值
        /// </summary>
        public static double gumedProTeachCenterY = 0;
        /// <summary>
        /// 注塑件拍照工位特征示教角度值
        /// </summary>
        public static double gumedProTeachDeg = 0;
        /// <summary>
        /// 注塑件拍照标定特征是否寻找到
        /// </summary>
        public static bool gumedIsCalMatchNG = false;
        /// <summary>
        /// 注塑件拍照测试特征是否寻找到
        /// </summary>
        public static bool gumedIsMatchNG = false;
        /// <summary>
        /// 发送给再塑胶件贴Logo位置机器人的X方向补偿值
        /// </summary>
        public static double gumedRobotOffsetX = 0;
        /// <summary>
        /// 发送给再塑胶件贴Logo位置机器人的Y方向补偿值
        /// </summary>
        public static double gumedRobotOffsetY = 0;
        /// <summary>
        /// 发送给再塑胶件贴Logo位置机器人的X方向补偿值
        /// </summary>
        public static double gumedProOffsetX = 0;
        /// <summary>
        /// 发送给再塑胶件贴Logo位置机器人的Y方向补偿值
        /// </summary>
        public static double gumedProOffsetY = 0;
        #endregion

        #region AOI视觉交互变量

        /// <summary>
        /// 数据库CRUD操作类
        /// </summary>
        public static DbUtility mySqlDb = new DbUtility(GetConnectionStringsConfig("ConnectionString"), DbProviderType.SqlServer);
        private static string GetConnectionStringsConfig(string connectionName)
        {
            string connectionString =
            ConfigurationManager.ConnectionStrings[connectionName].ConnectionString.ToString();

            return connectionString;
        }
        /// <summary>
        /// 最终测试结果
        /// </summary>
        public static List<bool> finalHighResAndAoiRes = new List<bool>();
        #region AOI变量
        /// <summary>
        /// X中心偏移最小值
        /// </summary>
        public static double aoiProCenterXOffsetVmin = 0;
        /// <summary>
        /// X中心偏移最大值
        /// </summary>
        public static double aoiProCenterXOffsetVmax = 0;
        /// <summary>
        /// Y中心偏移最小值
        /// </summary>
        public static double aoiProCenterYOffsetVmin = 0;
        /// <summary>
        /// Y中心偏移最大值
        /// </summary>
        public static double aoiProCenterYOffsetVmax = 0;
        /// <summary>
        /// θ中心偏移最小值
        /// </summary>
        public static double aoiProDegOffsetVmin = 0;
        /// <summary>
        /// θ中心偏移最大值
        /// </summary>
        public static double aoiProDegOffsetVmax = 0;
        /// <summary>
        /// X中心偏移补偿值
        /// </summary>
        public static double aoiProCenterOffsetX = 0;
        /// <summary>
        /// Y中心偏移补偿值
        /// </summary>
        public static double aoiProCenterOffsetY = 0;
        /// <summary>
        /// θ中心偏移补偿值
        /// </summary>
        public static double aoiProCenterOffsetDeg = 0;
        /// <summary>
        /// AOI是否测试
        /// </summary>
        public static bool isAoiStartTest = false;
        /// <summary>
        /// 是否测高工站测试过
        /// </summary>
        public static bool isAoiLastTestPro = false;
        #endregion


        #endregion

        #region 系统变量
        /// <summary>
        /// 是否记录系统Trace日志
        /// </summary>
        public static bool isRecordTraceLog = false;
        /// <summary>
        /// 操作员
        /// </summary>
        public static string userName = "19B0131";
        /// <summary>
        /// AOI数据集合
        /// </summary>
        public static List<AoiData> aoiDatas = new List<AoiData>();
        /// <summary>
        /// 测高数据
        /// </summary>
        public static List<HighData> highDatas = new List<HighData>();
        /// <summary>
        /// 凸轮分割器是否在旋转中
        /// </summary>
        public static bool isDivCamIsRotating = false;
        /// <summary>
        /// 设备全自动运行标记
        /// </summary>
        public static bool totalRunFlagVision = false;
        /// <summary>
        /// Aoi连续运行运行标记
        /// </summary>
        public static bool totalRunFlag = false;
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
        /// 当前班别
        /// </summary>
        public static string curClass = "白班";
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
        /// <summary>
        /// 测试产品良品数
        /// </summary>
        public static int okCnt = 0;
        /// <summary>
        /// 测试产品不良数
        /// </summary>
        public static int ngCnt = 0;
        public enum Level
        {
            工程师 = 4,
            生技 = 3,
            品质=2,
            操作员 = 1

        }
        /// <summary>
        /// 登录权限
        /// </summary>
        public static Level temLevel;
        /// <summary>
        /// 上传服务器路径
        /// </summary>
        public static string fileServerPath = @"\\10.65.4.200\Renault";
        /// <summary>
        /// 上料真空建立时间，默认100毫秒
        /// </summary>
        public static int feedVaccDelayTime = 100;

        #endregion

        #region 测高模组变量
        /// <summary>
        /// 子窗体是否在使用测高
        /// </summary>
        public static bool isChildFormUseHighMoudle = false;
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
        /// <summary>
        /// 测高实时值
        /// </summary>
        public static double highIndRealHighVal = 0.0;

        #endregion

        #region 打印机变量
        public static string printX = "";
        public static string printY = "";
        #endregion

        #region 频闪控制器变量
        /// <summary>
        /// 光源控制器串口
        /// </summary>
        public static string lightContrCom = "Com2";
        /// <summary>
        /// 未贴背胶的Logo通道1打开命令
        /// </summary>
        public static string lightNoGumLogoCH1OpenCmd = "";
        /// <summary>
        /// 未贴背胶的Logo通道1关闭命令
        /// </summary>
        public static string lightNoGumLogoCH1CloseCmd = "";
        /// <summary>
        /// 贴背胶的Logo通道1打开命令
        /// </summary>
        public static string lightLabeledGumCH1OpenCmd = "";
        /// <summary>
        /// 贴背胶的Logo通道1关闭命令
        /// </summary>
        public static string lightLabeledGumCH1CloseCmd = "";
        /// <summary>
        /// 拍背胶的通道2打开命令
        /// </summary>
        public static string lightGumedCH2OpenCmd = "";
        /// <summary>
        /// 拍背胶的通道2关闭命令
        /// </summary>
        public static string lightGumedCH2CloseCmd = "";
        /// <summary>
        /// 拍塑胶件的通道2打开命令
        /// </summary>
        public static string lightLabeledLogoCH2OpenCmd = "";
        /// <summary>
        /// 拍塑胶件的通道2关闭命令
        /// </summary>
        public static string lightLabeledLogoCH2CloseCmd = "";
        /// <summary>
        /// AOI偏位测试的通道3打开命令
        /// </summary>
        public static string lightCH3OpenCmd = "";
        /// <summary>
        /// AOI偏位测试的通道3关闭命令
        /// </summary>
        public static string lightCH3CloseCmd = "";

        #endregion

        #region  供料轴变量
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
        public static int injectTakeProPos = 1;
        /// <summary>
        /// 供料轴上料位置1
        /// </summary>
        public static int injectFeedProPos1 = 2;
        /// <summary>
        /// 供料轴上料位置2
        /// </summary>
        public static int injectFeedProPos2 = 3;
        /// <summary>
        /// 供料轴上料位置3
        /// </summary>
        public static int injectFeedProPos3 = 4;
        /// <summary>
        /// 供料轴上料位置4
        /// </summary>
        public static int injectFeedProPos4 = 5;
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
        /// 上料轴取料位置1
        /// </summary>
        public static int feedProPos1 = 1;
        /// <summary>
        /// 上料轴取料位置2
        /// </summary>
        public static int feedProPos2 = 2;
        /// <summary>
        /// 上料轴取料位置3
        /// </summary>
        public static int feedProPos3 = 3;
        /// <summary>
        /// 上料轴取料位置4
        /// </summary>
        public static int feedProPos4 = 4;
        /// <summary>
        /// 放料位置
        /// </summary>
        public static int feedTakeProPos = 3;

        #endregion

        #region 上料R轴变量
        /// <summary>
        /// 上料R轴每次转动之后的状态
        /// </summary>
        public static bool curFeedRAxiasDirPos = false;
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
        /// R轴取料位置1
        /// </summary>
        public static int feedRProPos1 = 1;
        /// <summary>
        /// R轴取料位置2
        /// </summary>
        public static int feedRProPos2 = 1;
        /// <summary>
        /// R轴取料位置3
        /// </summary>
        public static int feedRProPos3 = 1;
        /// <summary>
        /// R轴取料位置4
        /// </summary>
        public static int feedRProPos4 = 1;
        /// <summary>
        /// R轴放料位置
        /// </summary>
        public static int feedRTakeProPos = 2;
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
        public static int takeFeedProPos = 1;
        /// <summary>
        /// 丢OK产品位置
        /// </summary>
        public static int takeOKProPos = 2;
        /// <summary>
        /// 丢NG产品位置
        /// </summary>
        public static int takeNGProPos = 3;
        #endregion

        #region 出标轴变量
        /// <summary>
        /// 出标料感上升沿信号
        /// </summary>
        public static RisingTrig labelSensorRisingEdge = new RisingTrig();
        /// <summary>
        /// 出标料感上升沿信号
        /// </summary>
        public static FallTrig labelSensorFallEdge = new FallTrig();
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
        /// 出标轴定长距离
        /// </summary>
        public static int labelFixedStepLength = 1000;
        /// <summary>
        /// 出标气缸下压后继续走的脉冲值
        /// </summary>
        public static int labelContinuePlues = -800;
        /// <summary>
        /// 出标气缸下压后是否继续走脉冲
        /// </summary>
        public static bool labelIsContinue = false;

        #endregion

        #region 保压工位变量
        /// <summary>
        /// 保压时间
        /// </summary>
        public static int pressDelayTime = 0;
        #endregion 

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
        /// <summary>
        /// 出标感应器
        /// </summary>
        public static string labelOutMarkSensor = "2,11";
        /// <summary>
        /// 凸轮分割器感应信号
        /// </summary>
        public static string divCamSensor = "2,12";
        /// <summary>
        /// 设备安全门信号
        /// </summary>
        public static string deviceSafeDoor = "1,13";
        /// <summary>
        /// 机器人真空未建立
        /// </summary>
        public static string robotVaccNoArrve = "2,13";
        /// <summary>
        /// 急停信号
        /// </summary>
        public static string deviceEStop = "2,14";
        /// <summary>
        /// 机器人是否安全到达HOME
        /// </summary>
        public static string robotIsArriveHomeOk = "2,15";
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
        /// <summary>
        /// 允许凸轮分割器旋转
        /// </summary>
        public static string allowDivCAMToRun = "2,2";
        /// <summary>
        /// 控制机器人安全门打开
        /// </summary>
        public static string allowOpenRobotSafeDorr = "1,31";
        /// <summary>
        /// 允许凸轮分割器反旋转
        /// </summary>
        public static string allowDivCAMToNRun = "1,30";
        /// <summary>
        /// 允许打开AOI光源
        /// </summary>
        public static string allowAoiLightSensorOpen = "1,32";
        #endregion

        #endregion

        #region 雷赛轴卡IO变量
        /// <summary>
        /// 与注塑机关联的取料轴正极限
        /// </summary>
        public static string feedFromInjectPLimP = "0,16";
        /// <summary>
        /// 与注塑机关联的取料轴负极限
        /// </summary>
        public static string feedFromInjectPLimN = "0,24";
        /// <summary>
        /// 与注塑机关联的取料轴原点
        /// </summary>
        public static string feedFromInjectPLimHome = "1,0";
        /// <summary>
        /// 取料轴正极限
        /// </summary>
        public static string feedDiPLimP = "0,17";
        /// <summary>
        /// 取料轴负极限
        /// </summary>
        public static string feedDiNLimP = "0,25";
        /// <summary>
        /// 取料轴原点
        /// </summary>
        public static string feedDiHLimP = "1,1";
        /// <summary>
        /// 取料R轴原点
        /// </summary>
        public static string feedRDiHLimP = "1,2"; 
        /// <summary>
        /// 丢料轴正极限
        /// </summary>
        public static string takeDiPLimP = "0,18";
        /// <summary>
        /// 丢料轴负极限
        /// </summary>
        public static string takeDiNLimP = "0,26";
        /// <summary>
        /// 丢料轴原点
        /// </summary>
        public static string takeDiHLimP = "1,3";
        #endregion

        #region 每个治具补偿
        /// <summary>
        /// 编号1治具补偿X值
        /// </summary>
        public static double firToolXOffset = 0;
        /// <summary>
        /// 编号1治具补偿Y值
        /// </summary>
        public static double firToolYOffset = 0;
        /// <summary>
        /// 编号1治具补偿角度值
        /// </summary>
        public static double firToolDegOffset = 0;
        /// <summary>
        /// 编号2治具补偿X值
        /// </summary>
        public static double secToolXOffset = 0;
        /// <summary>
        /// 编号2治具补偿Y值
        /// </summary>
        public static double secToolYOffset = 0;
        /// <summary>
        /// 编号2治具补偿角度值
        /// </summary>
        public static double secToolDegOffset = 0;
        /// <summary>
        /// 编号3治具补偿X值
        /// </summary>
        public static double thrToolXOffset = 0;
        /// <summary>
        /// 编号3治具补偿Y值
        /// </summary>
        public static double thrToolYOffset = 0;
        /// <summary>
        /// 编号3治具补偿角度值
        /// </summary>
        public static double thrToolDegOffset = 0;
        /// <summary>
        /// 编号4治具补偿X值
        /// </summary>
        public static double fourToolXOffset = 0;
        /// <summary>
        /// 编号4治具补偿Y值
        /// </summary>
        public static double fourToolYOffset = 0;
        /// <summary>
        /// 编号4治具补偿角度值
        /// </summary>
        public static double fourToolDegOffset = 0;
        /// <summary>
        /// 编号5治具补偿X值
        /// </summary>
        public static double fiveToolXOffset = 0;
        /// <summary>
        /// 编号5治具补偿Y值
        /// </summary>
        public static double fiveToolYOffset = 0;
        /// <summary>
        /// 编号5治具补偿角度值
        /// </summary>
        public static double fiveToolDegOffset = 0;
        /// <summary>
        /// 编号6治具补偿X值
        /// </summary>
        public static double sixToolXOffset = 0;
        /// <summary>
        /// 编号6治具补偿X值
        /// </summary>
        public static double sixToolYOffset = 0;
        /// <summary>
        /// 编号6治具补偿角度值
        /// </summary>
        public static double sixToolDegOffset = 0;

        #endregion

        #region 电镀件偏位识别 By 薛姣奎
        /// <summary>
        /// 电镀件外轮廓 - 内轮廓X方向最大偏差值
        /// </summary>
        public static double logoXmax = 0;
        /// <summary>
        /// 电镀件外轮廓 - 内轮廓X方向最小偏差值
        /// </summary>
        public static double logoXmin = 0;
        /// <summary>
        /// 电镀件外轮廓 - 内轮廓Y方向最大偏差值
        /// </summary>
        public static double logoYmax = 0;
        /// <summary>
        /// 电镀件外轮廓 - 内轮廓Y方向最小偏差值
        /// </summary>
        public static double logoYmin = 0;
        #endregion

        /// <summary>
        /// 识别治具编号Flag
        /// </summary>
        //public static bool stationIsShot = false;
        /// <summary>
        /// 站号
        /// </summary>
        //public static string staticonSN = "1";
        /// <summary>
        /// 点击停止按钮结束生产并收尾轮盘上的每个工站
        /// </summary>
        public static bool totalRunIsEnded = false;
        /// <summary>
        /// 机器人连接状态
        /// </summary>
        public static bool isEpsonConnected = false;
        /// <summary>
        /// 是否走回原点运动
        /// </summary>
        public static bool isHomeMotion = false;
        /// <summary>
        /// 声明一个全局日志记录器
        /// </summary>
        public static Log4NetLogger myLog = new Log4NetLogger(typeof(GlobalVar));
        /// <summary>
        /// IO卡输入点集合
        /// </summary>
        public static List<LsIODIPinDefinition> lsAxiasDIs = new List<LsIODIPinDefinition>();
        /// <summary>
        /// IO卡输出点集合
        /// </summary>
        public static List<LsIODOPinDefinition> lsAxiasDOs = new List<LsIODOPinDefinition>();
        /// <summary>
        /// 获取轴卡所有第一组输入全部信号
        /// </summary>
        public static List<char> lsFirAxiasSensorDIs = new List<char>();
        /// <summary>
        /// 获取轴卡所有第二组输入全部信号
        /// </summary>
        public static List<char> lsSecAxiasSensorDIs = new List<char>();
        /// <summary>
        /// 与EpsonSocket通讯变量
        /// </summary>
        public static MyAppServer tcpServerEngine;
        /// <summary>
        /// 存储机器人通讯handle
        /// </summary>
        public static ConcurrentDictionary<string, MyAppSession> mOnLineConnections = new ConcurrentDictionary<string, MyAppSession>();
        /// <summary>
        /// 设备安全门是否打开
        /// </summary>
        public static bool deviceSafeDoorIsOpen = false;
        /// <summary>
        /// 设备是否报警
        /// </summary>
        public static bool deviceAlarmIsHappen = false;
        /// <summary>
        /// 给每工站的多线程池
        /// </summary>
        public static List<Task> taskList = new List<Task>();
        /// <summary>
        /// 主线程
        /// </summary>
        public static Thread mainThread = null;
        /// <summary>
        /// 是否屏蔽安全门
        /// </summary>
        public static bool isCloseSafeDoor = false;
        /// <summary>
        /// 是否保存用来分析数据的图片
        /// </summary>
        public static bool isAnalysisPic = false;
        /// <summary>
        /// 是否打印不良条码
        /// </summary>
        public static bool isPrintNGBarcode = false;
        #region 工位是否允许生产
        /// <summary>
        /// 第一工位是否允许生产
        /// </summary>
        public static bool isAllowFirStaAction = false;
        /// <summary>
        /// 第二工位是否允许生产
        /// </summary>
        public static bool isAllowSecStaAction = false;
        /// <summary>
        /// 第三工位是否允许生产
        /// </summary>
        public static bool isAllowThrStaAction = false;
        /// <summary>
        /// 第四工位是否允许生产
        /// </summary>
        public static bool isAllowFourStaAction = false;
        /// <summary>
        /// 第六工位是否允许生产
        /// </summary>
        public static bool isAllowSixStaAction = false;
        #endregion 
        /// <summary>
        /// 初始化总配置文件
        /// </summary>
        public static void InitConfigPara()
        {
            try
            {
                if (File.Exists(bConfigFilePath))  //如果文件存在
                {
                    powerIsOnOrOff = Convert.ToBoolean(INI.INIGetStringValue(bConfigFilePath, "System", "程序是否开机启动", null));
                    isRecordTraceLog = Convert.ToBoolean(INI.INIGetStringValue(bConfigFilePath, "System", "是否记录Trace日志", null));
                    isCloseSafeDoor = Convert.ToBoolean(INI.INIGetStringValue(bConfigFilePath, "System", "是否屏蔽安全门", null));
                    isAnalysisPic = Convert.ToBoolean(INI.INIGetStringValue(bConfigFilePath, "System", "是否追踪分析图片", null));
                    serverPort = Convert.ToInt32(INI.INIGetStringValue(bConfigFilePath, "System", "服务器端口", null));
                    curProName = INI.INIGetStringValue(bConfigFilePath, "System", "产品选择", null);
                    fileServerPath = INI.INIGetStringValue(bConfigFilePath, "System", "文件服务器路径", null);
                    feedVaccDelayTime = Convert.ToInt32(INI.INIGetStringValue(bConfigFilePath, "System", "上料真空建立时间", null));
                    noLabelLogoRatioX = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "第一工位比例尺", "X", null));
                    noLabelLogoRatioY = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "第一工位比例尺", "Y", null));
                    noGumRatioX = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "第二工位比例尺", "X", null));
                    noGumRatioY = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "第二工位比例尺", "Y", null));
                    noGumTeachRobotTX = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "第二工位Robot工具示教点", "X", null));
                    noGumTeachRobotTY = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "第二工位Robot工具示教点", "Y", null));
                    noGumTeachRobotTU = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "第二工位Robot工具示教点", "U", null));
                    noGumRobotOffsetX = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "第二工位Robot工具补偿值", "OffsetX", null));
                    noGumRobotOffsetY = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "第二工位Robot工具补偿值", "OffsetY", null));
                    labeledLogoRatioX = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "第三工位比例尺", "X", null));
                    labeledLogoRatioY = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "第三工位比例尺", "Y", null));
                    gumedRatioX = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "第四工位比例尺", "X", null));
                    gumedRatioY = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "第四工位比例尺", "Y", null));
                    gumedTeachRobotTX = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "第四工位Robot工具示教点", "X", null));
                    gumedTeachRobotTY = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "第四工位Robot工具示教点", "Y", null));
                    gumedTeachRobotTU = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "第四工位Robot工具示教点", "U", null));
                    gumedRobotOffsetX = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "第四工位Robot工具补偿值", "OffsetX", null));
                    gumedRobotOffsetY = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "第四工位Robot工具补偿值", "OffsetY", null));
                    gumedProOffsetX = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "第四工位Robot工具补偿值", "OffsetProX", null));
                    gumedProOffsetY = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "第四工位Robot工具补偿值", "OffsetProY", null));
                    highIndCom = INI.INIGetStringValue(bConfigFilePath, "测高模组", "串口号", null);
                    highIndInitDelayTime = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "测高模组", "延迟时间", null));
                    highIndInitHighVal = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "测高模组", "初始值", null));
                    printX = INI.INIGetStringValue(bConfigFilePath, "打印机", "X方向起点", null);
                    printY = INI.INIGetStringValue(bConfigFilePath, "打印机", "Y方向起点", null);
                    lightContrCom = INI.INIGetStringValue(bConfigFilePath, "频闪控制器", "串口号", null);
                    feedStopModel = (StopModelEnum)Enum.Parse(typeof(StopModelEnum), INI.INIGetStringValue(bConfigFilePath, "轴卡上料轴参数配置", "停止模式", null));
                    feedCoordModel = (CoordModelEnum)Enum.Parse(typeof(CoordModelEnum), INI.INIGetStringValue(bConfigFilePath, "轴卡上料轴参数配置", "坐标模式", null));
                    feedStartSpeed = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "轴卡上料轴参数配置", "起始速度", null));
                    feedMotionSpeed = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "轴卡上料轴参数配置", "正常运行速度", null));
                    feedStopSpeed = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "轴卡上料轴参数配置", "停止速度", null));
                    feedAccTime = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "轴卡上料轴参数配置", "加速时间", null));
                    feedDccTime = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "轴卡上料轴参数配置", "减速时间", null));
                    feedSTime = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "轴卡上料轴参数配置", "S段时间", null));
                    feedHomeLowSpeed = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "轴卡上料轴参数配置", "回零低速", null));
                    feedHomeHighSpeed = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "轴卡上料轴参数配置", "回零高速", null));
                    feedHomeModel = (HomeModel)Enum.Parse(typeof(HomeModel), INI.INIGetStringValue(bConfigFilePath, "轴卡上料轴参数配置", "回零模式", null));
                    feedHomeSpeedModel = (HomeSpeedModel)Enum.Parse(typeof(HomeSpeedModel), INI.INIGetStringValue(bConfigFilePath, "轴卡上料轴参数配置", "回零速度模式", null));
                    feedTakeProPos = Convert.ToInt32(INI.INIGetStringValue(bConfigFilePath, "轴卡上料轴参数配置", "放料位置", null));
                    feedProPos1 = Convert.ToInt32(INI.INIGetStringValue(bConfigFilePath, "轴卡上料轴参数配置", "取料位置1", null));
                    feedProPos2 = Convert.ToInt32(INI.INIGetStringValue(bConfigFilePath, "轴卡上料轴参数配置", "取料位置2", null));
                    feedProPos3 = Convert.ToInt32(INI.INIGetStringValue(bConfigFilePath, "轴卡上料轴参数配置", "取料位置3", null));
                    feedProPos4 = Convert.ToInt32(INI.INIGetStringValue(bConfigFilePath, "轴卡上料轴参数配置", "取料位置4", null));
                    feedRStopModel = (StopModelEnum)Enum.Parse(typeof(StopModelEnum), INI.INIGetStringValue(bConfigFilePath, "轴卡上料R轴参数配置", "停止模式", null));
                    feedRCoordModel = (CoordModelEnum)Enum.Parse(typeof(CoordModelEnum), INI.INIGetStringValue(bConfigFilePath, "轴卡上料R轴参数配置", "坐标模式", null));
                    feedRStartSpeed = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "轴卡上料R轴参数配置", "起始速度", null));
                    feedRMotionSpeed = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "轴卡上料R轴参数配置", "正常运行速度", null));
                    feedRStopSpeed = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "轴卡上料R轴参数配置", "停止速度", null));
                    feedRAccTime = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "轴卡上料R轴参数配置", "加速时间", null));
                    feedRDccTime = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "轴卡上料R轴参数配置", "减速时间", null));
                    feedRSTime = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "轴卡上料R轴参数配置", "S段时间", null));
                    feedRHomeLowSpeed = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "轴卡上料R轴参数配置", "回零低速", null));
                    feedRHomeHighSpeed = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "轴卡上料R轴参数配置", "回零高速", null));
                    feedRHomeModel = (HomeModel)Enum.Parse(typeof(HomeModel), INI.INIGetStringValue(bConfigFilePath, "轴卡上料R轴参数配置", "回零模式", null));
                    feedRHomeSpeedModel = (HomeSpeedModel)Enum.Parse(typeof(HomeSpeedModel), INI.INIGetStringValue(bConfigFilePath, "轴卡上料R轴参数配置", "回零速度模式", null));
                    feedRProPos1 = Convert.ToInt32(INI.INIGetStringValue(bConfigFilePath, "轴卡上料R轴参数配置", "取料位置1", null));
                    feedRProPos2 = Convert.ToInt32(INI.INIGetStringValue(bConfigFilePath, "轴卡上料R轴参数配置", "取料位置2", null));
                    feedRProPos3 = Convert.ToInt32(INI.INIGetStringValue(bConfigFilePath, "轴卡上料R轴参数配置", "取料位置3", null));
                    feedRProPos4 = Convert.ToInt32(INI.INIGetStringValue(bConfigFilePath, "轴卡上料R轴参数配置", "取料位置4", null));
                    feedRTakeProPos = Convert.ToInt32(INI.INIGetStringValue(bConfigFilePath, "轴卡上料R轴参数配置", "放料位置", null));
                    curFeedRAxiasDirPos = Convert.ToBoolean(INI.INIGetStringValue(bConfigFilePath, "轴卡上料R轴参数配置", "当前轴方向位置", null));
                    injectFeedStopModel = (StopModelEnum)Enum.Parse(typeof(StopModelEnum), INI.INIGetStringValue(bConfigFilePath, "来自有信机械手上料轴参数配置", "停止模式", null));
                    injectFeedCoordModel = (CoordModelEnum)Enum.Parse(typeof(CoordModelEnum), INI.INIGetStringValue(bConfigFilePath, "来自有信机械手上料轴参数配置", "坐标模式", null));
                    injectFeedStartSpeed = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "来自有信机械手上料轴参数配置", "起始速度", null));
                    injectFeedMotionSpeed = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "来自有信机械手上料轴参数配置", "正常运行速度", null));
                    injectFeedStopSpeed = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "来自有信机械手上料轴参数配置", "停止速度", null));
                    injectFeedAccTime = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "来自有信机械手上料轴参数配置", "加速时间", null));
                    injectFeedDccTime = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "来自有信机械手上料轴参数配置", "减速时间", null));
                    injectFeedSTime = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "来自有信机械手上料轴参数配置", "S段时间", null));
                    injectFeedHomeLowSpeed = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "来自有信机械手上料轴参数配置", "回零低速", null));
                    injectFeedHomeHighSpeed = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "来自有信机械手上料轴参数配置", "回零高速", null));
                    injectFeedHomeModel = (HomeModel)Enum.Parse(typeof(HomeModel), INI.INIGetStringValue(bConfigFilePath, "来自有信机械手上料轴参数配置", "回零模式", null));
                    injectFeedHomeSpeedModel = (HomeSpeedModel)Enum.Parse(typeof(HomeSpeedModel), INI.INIGetStringValue(bConfigFilePath, "来自有信机械手上料轴参数配置", "回零速度模式", null));
                    injectTakeProPos = Convert.ToInt32(INI.INIGetStringValue(bConfigFilePath, "来自有信机械手上料轴参数配置", "放料位置", null));
                    injectFeedProPos1 = Convert.ToInt32(INI.INIGetStringValue(bConfigFilePath, "来自有信机械手上料轴参数配置", "取料位置1", null));
                    injectFeedProPos2 = Convert.ToInt32(INI.INIGetStringValue(bConfigFilePath, "来自有信机械手上料轴参数配置", "取料位置2", null));
                    injectFeedProPos3 = Convert.ToInt32(INI.INIGetStringValue(bConfigFilePath, "来自有信机械手上料轴参数配置", "取料位置3", null));
                    injectFeedProPos4 = Convert.ToInt32(INI.INIGetStringValue(bConfigFilePath, "来自有信机械手上料轴参数配置", "取料位置4", null));
                    labelStopModel = (StopModelEnum)Enum.Parse(typeof(StopModelEnum), INI.INIGetStringValue(bConfigFilePath, "轴卡出标轴参数配置", "停止模式", null));
                    labelCoordModel = (CoordModelEnum)Enum.Parse(typeof(CoordModelEnum), INI.INIGetStringValue(bConfigFilePath, "轴卡出标轴参数配置", "坐标模式", null));
                    labelStartSpeed = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "轴卡出标轴参数配置", "起始速度", null));
                    labelMotionSpeed = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "轴卡出标轴参数配置", "正常运行速度", null));
                    labelStopSpeed = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "轴卡出标轴参数配置", "停止速度", null));
                    labelAccTime = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "轴卡出标轴参数配置", "加速时间", null));
                    labelDccTime = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "轴卡出标轴参数配置", "减速时间", null));
                    labelSTime = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "轴卡出标轴参数配置", "S段时间", null));
                    labelFixedStepLength = Convert.ToInt32(INI.INIGetStringValue(bConfigFilePath, "轴卡出标轴参数配置", "定长距离", null)??"-800");
                    labelContinuePlues = Convert.ToInt32(INI.INIGetStringValue(bConfigFilePath, "轴卡出标轴参数配置", "气缸下压后继续走的脉冲", null)??"-800");
                    labelIsContinue = Convert.ToBoolean(INI.INIGetStringValue(bConfigFilePath, "轴卡出标轴参数配置", "是否开启出标完继续出标固定脉冲", null)??"false");
                    takeStopModel = (StopModelEnum)Enum.Parse(typeof(StopModelEnum), INI.INIGetStringValue(bConfigFilePath, "轴卡丢料轴参数配置", "停止模式", null));
                    takeCoordModel = (CoordModelEnum)Enum.Parse(typeof(CoordModelEnum), INI.INIGetStringValue(bConfigFilePath, "轴卡丢料轴参数配置", "坐标模式", null));
                    takeStartSpeed = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "轴卡丢料轴参数配置", "起始速度", null));
                    takeMotionSpeed = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "轴卡丢料轴参数配置", "正常运行速度", null));
                    takeStopSpeed = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "轴卡丢料轴参数配置", "停止速度", null));
                    takeAccTime = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "轴卡丢料轴参数配置", "加速时间", null));
                    takeDccTime = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "轴卡丢料轴参数配置", "减速时间", null));
                    takeSTime = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "轴卡丢料轴参数配置", "S段时间", null));
                    takeHomeLowSpeed = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "轴卡丢料轴参数配置", "回零低速", null));
                    takeHomeHighSpeed = Convert.ToDouble(INI.INIGetStringValue(bConfigFilePath, "轴卡丢料轴参数配置", "回零高速", null));
                    takeHomeModel = (HomeModel)Enum.Parse(typeof(HomeModel), INI.INIGetStringValue(bConfigFilePath, "轴卡丢料轴参数配置", "回零模式", null));
                    takeHomeSpeedModel = (HomeSpeedModel)Enum.Parse(typeof(HomeSpeedModel), INI.INIGetStringValue(bConfigFilePath, "轴卡丢料轴参数配置", "回零速度模式", null));
                    takeFeedProPos = Convert.ToInt32(INI.INIGetStringValue(bConfigFilePath, "轴卡丢料轴参数配置", "取料位置", null));
                    takeNGProPos = Convert.ToInt32(INI.INIGetStringValue(bConfigFilePath, "轴卡丢料轴参数配置", "放料NG位置", null));
                    takeOKProPos = Convert.ToInt32(INI.INIGetStringValue(bConfigFilePath, "轴卡丢料轴参数配置", "放料OK位置", null));
                    InjectFeedAxiasNumber = Convert.ToUInt16(INI.INIGetStringValue(bConfigFilePath, "轴卡配置", "有信取料轴号", null));
                    FeedAxiasNumber = Convert.ToUInt16(INI.INIGetStringValue(bConfigFilePath, "轴卡配置", "上料轴号", null));
                    FeedRAxiasNumber = Convert.ToUInt16(INI.INIGetStringValue(bConfigFilePath, "轴卡配置", "上料R轴号", null));
                    LabelingAxiasNumber = Convert.ToUInt16(INI.INIGetStringValue(bConfigFilePath, "轴卡配置", "出标轴号", null));
                    TakeAxiasNumber = Convert.ToUInt16(INI.INIGetStringValue(bConfigFilePath, "轴卡配置", "丢料轴号", null));
                   
                }
                else
                {
                    FileStream fs = new FileStream(bConfigFilePath, FileMode.CreateNew, FileAccess.ReadWrite);
                    fs.Close();
                    //写入一批键值  通过引用的ClassINI文件内建置的方法完成如下操作；
                    INI.INIWriteItems(bConfigFilePath, "System", "程序是否开机启动=true\0操作权限=0\0服务器端口=9000\0产品选择=Renault\0");
                    INI.INIWriteItems(bConfigFilePath, "测高模组", "初始值=0\0延迟时间=0\0串口号=Com1\0");
                    INI.INIWriteItems(bConfigFilePath, "第一工位比例尺", "X=0\0Y=0\0");
                    INI.INIWriteItems(bConfigFilePath, "第二工位Robot工具补偿值", "OffsetX=0\0OffsetY=0\0");
                    INI.INIWriteItems(bConfigFilePath, "第二工位Robot工具示教点", "X=0\0Y=0\0U=0\0");
                    INI.INIWriteItems(bConfigFilePath, "第二工位比例尺", "X=0\0Y=0\0");
                    INI.INIWriteItems(bConfigFilePath, "第三工位比例尺", "X=0\0Y=0\0");
                    INI.INIWriteItems(bConfigFilePath, "第四工位比例尺", "X=0\0Y=0\0");
                    INI.INIWriteItems(bConfigFilePath, "第四工位Robot工具示教点", "X=0\0Y=0\0U=0\0");
                    INI.INIWriteItems(bConfigFilePath, "第四工位Robot工具补偿值", "OffsetX=0\0OffsetY=0\0");
                    INI.INIWriteItems(bConfigFilePath, "打印机", "X方向起点=0\0Y方向起点=0\0");
                    INI.INIWriteItems(bConfigFilePath, "频闪控制器", "串口号=Com6\0");
                    INI.INIWriteItems(bConfigFilePath, "轴卡出标轴参数配置 ", "停止模式=立即停止\0坐标模式=绝对坐标\0起始速度=0\0正常运行速度=0\0停止速度=0\0加速时间=0\0减速时间=0\0S段时间=0\0定长距离=0\0");
                    INI.INIWriteItems(bConfigFilePath, "轴卡上料轴参数配置 ", "停止模式=立即停止\0坐标模式=绝对坐标\0起始速度=0\0正常运行速度=0\0停止速度=0\0加速时间=0\0减速时间=0\0S段时间=0\0回零低速=0\0回零高速=0\0回零模式=一次回零\0回零速度模式=低速回零\0取料位置1=0\0取料位置2=0\0放料位置=0\0");
                    INI.INIWriteItems(bConfigFilePath, "轴卡丢料轴参数配置 ", "停止模式=立即停止\0坐标模式=绝对坐标\0起始速度=0\0正常运行速度=0\0停止速度=0\0加速时间=0\0减速时间=0\0S段时间=0\0回零低速=0\0回零高速=0\0回零模式=一次回零\0回零速度模式=低速回零\0取料位置=0\0放料NG位置=0\0放料OK位置=0\0");
                    INI.INIWriteItems(bConfigFilePath, "轴卡上料R轴参数配置 ", "停止模式=立即停止\0坐标模式=绝对坐标\0起始速度=0\0正常运行速度=0\0停止速度=0\0加速时间=0\0减速时间=0\0S段时间=0\0回零低速=0\0回零高速=0\0回零模式=一次回零\0回零速度模式=低速回零\0取料位置=0\0放料位置=0\0");
                    INI.INIWriteItems(bConfigFilePath, "来自有信机械手上料轴参数配置 ", "停止模式=立即停止\0坐标模式=绝对坐标\0起始速度=0\0正常运行速度=0\0停止速度=0\0加速时间=0\0减速时间=0\0S段时间=0\0回零低速=0\0回零高速=0\0回零模式=一次回零\0回零速度模式=低速回零\0取料位置1=0\0取料位置2=0\0放料位置=0\0");
                    INI.INIWriteItems(bConfigFilePath, "轴卡配置", "有信取料轴号=0\0上料轴号=1\0上料R轴号=2\0出标轴号=3\0丢料轴号=4\0");
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// 初始化IO配置文件
        /// </summary>
        public static void InitIOPara()
        {
            try
            {
                if (File.Exists(bDeviceIOFilePath))  //如果文件存在
                {
                    #region IO卡输入变量
                    epsonStandbying = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "机器人待机中", null);
                    epsonRunning = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "机器人运行中", null);
                    epsonPausing = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "机器人暂停中", null);
                    epsonControllerErr = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "机器人控制器一般错误", null);
                    epsonEMGOutput = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "机器人急停输出", null);
                    epsonSafeDoor = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "机器人安全门打开", null);
                    epsonControllerFatalErr = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "机器人控制器严重错误", null);
                    epsonAlarm = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "机器人报警", null);
                    yuShinBlowingFinished = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "有信放料完成", null);
                    deviceBoot = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "设备启动按钮", null);
                    deviceStop = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "设备停止按钮", null);
                    logo4FeedFinished = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "电镀件补料完成", null);
                    rotatingCylinderLeftLim = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "旋转气缸左极限", null);
                    rotatingCylinderRightLim = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "旋转气缸右极限", null);
                    feedCylinderHighLim = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "上料气缸上极限", null);
                    feedCylinderLowLim = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "上料气缸下极限", null);
                    labelCylinderHighLim = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "出标轴下压气缸上极限", null);
                    labelCylinderLowLim = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "出标轴下压气缸下极限", null);
                    labelCylinderLeftLim = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "出标轴出标气缸左极限", null);
                    labelCylinderRightLim = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "出标轴出标气缸右极限", null);
                    pressCylinderHighLim = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "压合气缸上极限", null);
                    pressCylinderLowLim = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "压合气缸下极限", null);
                    highTestCylinderHighLim = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "测高气缸上极限", null);
                    highTestCylinderLowLim = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "测高气缸下极限", null);
                    takeCylinerHighLim = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "下料气缸上极限", null);
                    takeCylinerLowLim = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "下料气缸下极限", null);
                    pushCylinerHighLim = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "贴Logo按压气缸上极限", null);
                    pushCylinerLowLim = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "贴Logo按压气缸下极限", null);
                    feedVacuumLim = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "上料真空负压表阈值", null);
                    takeVacuumLim = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "下料真空负压表阈值", null);
                    feedStationSensor = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "上料工站料感信号", null);
                    labelStationSensor = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "贴标工站料感信号", null);
                    pressStationSensor = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "压合工站料感信号", null);
                    highTestStationSensor = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "测高工站料感信号", null);
                    aoiTestStationSensor = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "AOI偏位测试工站料感信号", null);
                    takeStationSensor = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "下料工站料感信号", null);
                    feed4PcsLogoFinished = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "Epson通知取完4片Logo信号", null);
                    tearLabelStart = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "Epson通知撕标开始信号", null);
                    newLabelStart = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "Epson通知出标开始信号", null);
                    labelLogoToPlasticFinished = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "贴电镀+背胶完成", null);
                    labelOutMarkSensor = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "出标感应器", null);
                    divCamSensor = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "凸轮分割器旋转到位", null);
                    deviceSafeDoor = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "设备安全门信号", null);
                    robotVaccNoArrve  = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "机器人真空未建立", null);
                    deviceEStop = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "设备急停信号", null);//robotIsArriveHomeOk
                    robotIsArriveHomeOk = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "机器人是否回到Home", null);
                    #endregion
                    #region IO卡输出变量
                    bootEpson = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "启动机器人程序", null);
                    prog1Selected = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "机器人程序1", null);
                    prog2Selected = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "机器人程序2", null);
                    prog3Selected = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "机器人程序3", null);
                    stopEpson = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "停止机器人", null);
                    pausedEpson = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "暂停机器人", null);
                    continuedEpson = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "继续机器人", null);
                    resetEpson = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "复位机器人", null);
                    allowYuShinTakePro = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "允许有信放料", null);
                    redAlarmLight = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "三色灯-红灯", null);
                    yellowAlarmLight = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "三色灯-黄灯", null);
                    greenAlarmLight = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "三色灯-绿灯", null);
                    buzzingAlarm = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "三色灯-蜂鸣", null);
                    feedFinishedOut = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "补料完成->按钮绿灯亮", null);
                    feedStandbyingOut = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "待补料->按钮红灯亮", null);
                    rotatingCylinderElecValve = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "旋转气缸电磁阀", null);
                    feedCylinderElecValve = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "上料气缸电磁阀", null);
                    feedVacuumSolenoid = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "上料真空电磁阀", null);
                    labelPressCylinderElecValve = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "出标轴下压气缸电磁阀", null);
                    labelOutCylinderElecValve = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "出标轴出标气缸电磁阀", null);
                    pressCylinderElecValve = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "压合气缸电磁阀", null);
                    highTestCylinderElecValve = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "测高气缸电磁阀", null);
                    takeCylinderElecValve = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "下料气缸电磁阀", null);
                    takeVacuumSolenoid = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "下料真空电磁阀", null);
                    pushCylinderElecValve = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "贴Logo按压气缸电磁阀", null);
                    allowEpsonFeedLogo = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "允许epson取Logo", null);
                    allowEpsonLabelGum = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "允许epson贴背胶", null);
                    allowEpsonLabelPosMove = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "允许epson在出标气缸回退到位后从贴标位置离开", null);
                    allowEpsonLabelLogoToPlastic = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "允许epson贴Logo到塑胶件", null);
                    allowDivCAMToRun = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "允许凸轮分割器旋转", null);
                    allowDivCAMToNRun = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "允许凸轮分割器反旋转", null);
                    allowOpenRobotSafeDorr = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "允许打开机器人安全门", null);
                    allowAoiLightSensorOpen = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "允许打开AOI光源", null);
                    allowAoiLightSensorOpen = INI.INIGetStringValue(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "允许打开AOI光源", null);
                    #endregion
                    #region 轴卡输入信号变量
                    feedFromInjectPLimP = INI.INIGetStringValue(bDeviceIOFilePath, "轴卡输入信号配置", "与注塑机关联的取料轴正极限", null);
                    feedFromInjectPLimN = INI.INIGetStringValue(bDeviceIOFilePath, "轴卡输入信号配置", "与注塑机关联的取料轴负极限", null);
                    feedFromInjectPLimHome = INI.INIGetStringValue(bDeviceIOFilePath, "轴卡输入信号配置", "与注塑机关联的取料轴原点", null);
                    feedDiPLimP = INI.INIGetStringValue(bDeviceIOFilePath, "轴卡输入信号配置", "取料轴正极限", null);
                    feedDiNLimP = INI.INIGetStringValue(bDeviceIOFilePath, "轴卡输入信号配置", "取料轴负极限", null);
                    feedDiHLimP = INI.INIGetStringValue(bDeviceIOFilePath, "轴卡输入信号配置", "取料轴原点", null);
                    feedRDiHLimP = INI.INIGetStringValue(bDeviceIOFilePath, "轴卡输入信号配置", "取料R轴原点", null);
                    takeDiPLimP = INI.INIGetStringValue(bDeviceIOFilePath, "轴卡输入信号配置", "丢料轴正极限", null);
                    takeDiNLimP = INI.INIGetStringValue(bDeviceIOFilePath, "轴卡输入信号配置", "丢料轴负极限", null);
                    takeDiHLimP = INI.INIGetStringValue(bDeviceIOFilePath, "轴卡输入信号配置", "丢料轴原点", null);
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
                    INI.INIWriteItems(bDeviceIOFilePath, "雷赛IO卡输入信号配置", "机器人待机中=1,1\0机器人运行中=1,2\0机器人暂停中=1,3\0机器人控制器一般错误=1,4\0机器人急停输出=1,5\0机器人安全门打开=1,6\0机器人控制器严重错误=1,7\0机器人报警=1,8\0有信放料完成=1,9\0设备启动按钮=1,10\0设备停止按钮=1,11\0电镀件补料完成=1,14\0旋转气缸左极限=1,15\0旋转气缸右极限=1,16\0上料气缸上极限=1,17\0上料气缸下极限=1,18\0出标轴下压气缸上极限=1,19\0出标轴下压气缸下极限=1,20\0出标轴出标气缸左极限=1,21\0出标轴出标气缸右极限=1,22\0压合气缸上极限=1,23\0压合气缸下极限=1,24\0测高气缸上极限=1,25\0测高气缸下极限=1,26\0下料气缸上极限=1,27\0下料气缸下极限=1,28\0贴Logo按压气缸上极限=1,29\0贴Logo按压气缸下极限=1,30\0上料真空负压表阈值=1,31\0下料真空负压表阈值=1,32\0上料工站料感信号=2,1\0贴标工站料感信号=2,2\0压合工站料感信号=2,3\0测高工站料感信号=2,4\0AOI偏位测试工站料感信号=2,5\0下料工站料感信号=2,6\0Epson通知取完4片Logo信号=2,7\0Epson通知撕标开始信号=2,8\0Epson通知出标开始信号=2,9\0贴电镀+背胶完成=2,10\0出标感应器=2,11\0凸轮分割器旋转到位=2,12\0设备安全门信号=2,13\0设备急停信号=2,14\0机器人是否回到Home=2,15\0");
                    INI.INIWriteItems(bDeviceIOFilePath, "雷赛IO卡输出信号配置", "启动机器人程序=1,1\0机器人程序1=1,2\0机器人程序2=1,3\0机器人程序3=1,4\0停止机器人=1,5\0暂停机器人=1,6\0继续机器人=1,7\0复位机器人=1,8\0允许有信放料=1,9\0三色灯-红灯=1,10\0三色灯-黄灯=1,11\0三色灯-绿灯=1,12\0三色灯-蜂鸣=1,13\0补料完成->按钮绿灯亮=1,14\0待补料->按钮红灯亮=1,15\0旋转气缸电磁阀=1,16\0上料气缸电磁阀=1,17\0上料真空电磁阀=1,18\0出标轴下压气缸电磁阀=1,19\0出标轴出标气缸电磁阀=1,20\0压合气缸电磁阀=1,21\0测高气缸电磁阀=1,22\0下料气缸电磁阀=1,23\0下料真空电磁阀=1,24\0贴Logo按压气缸电磁阀=1,25\0允许epson取Logo=1,26\0允许epson贴背胶=1,27\0允许epson在出标气缸回退到位后从贴标位置离开=1,28\0允许epson贴Logo到塑胶件=1,29\0允许凸轮分割器旋转=1,30\0允许打开机器人安全门=1,31\0");
                    INI.INIWriteItems(bDeviceIOFilePath, "轴卡输入信号配置", "与注塑机关联的取料轴正极限=0,16\0与注塑机关联的取料轴负极限=0,24\0与注塑机关联的取料轴原点=1,0\0取料轴正极限=0,17\0取料轴负极限=0,25\0取料轴原点=1,1\0取料R轴原点=1,2\0丢料轴正极限=0,18\0丢料轴负极限=0,26\0丢料轴原点=1,3\0");
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// 根据总配置文件的当前产品名称来配置不同的Recipe数据
        /// </summary>
        public static void InitRecipePara()
        {
            try
            {
                switch (GlobalVar.curProName)
                {
                    case "Renault":
                        {
                            if (File.Exists(bRecipeRenaultFilePath))  //如果文件存在
                            {
                                okCnt = Convert.ToInt32(INI.INIGetStringValue(bRecipeRenaultFilePath, "System", "良品数量", null));
                                ngCnt = Convert.ToInt32(INI.INIGetStringValue(bRecipeRenaultFilePath, "System", "不良品数量", null));
                                curWo = INI.INIGetStringValue(bRecipeRenaultFilePath, "System", "当前工单", null);
                                curCusNumber = INI.INIGetStringValue(bRecipeRenaultFilePath, "System", "当前料号", null);
                                snCnt = Convert.ToInt32(INI.INIGetStringValue(bRecipeRenaultFilePath, "System", "当前流水号", null));
                                highIndSpaceHigh = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "测高模组", "空载具高度", null));
                                highIndMaxVal = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "测高模组", "模组最大值", null));
                                highIndMinVal = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "测高模组", "模组最小值", null));
                                noLabelLogoProTeachCenterX = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "第一工位示教点", "X", null));
                                noLabelLogoProTeachCenterY = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "第一工位示教点", "Y", null));
                                noLabelLogoProTeachDeg = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "第一工位示教点", "角度", null));
                                noGumProTeachCenterX = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "第二工位示教点", "X", null));
                                noGumProTeachCenterY = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "第二工位示教点", "Y", null));
                                noGumProTeachDeg = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "第二工位示教点", "角度", null));
                                labeledLogoProTeachCenterX = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "第三工位示教点", "X", null));
                                labeledLogoProTeachCenterY = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "第三工位示教点", "Y", null));
                                labeledLogoProTeachDeg = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "第三工位示教点", "角度", null));
                                gumedProTeachCenterX = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "第四工位示教点", "X", null));
                                gumedProTeachCenterY = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "第四工位示教点", "Y", null));
                                gumedProTeachDeg = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "第四工位示教点", "角度", null));
                                lightNoGumLogoCH1OpenCmd = INI.INIGetStringValue(bRecipeRenaultFilePath, "频闪控制器", "未贴背胶的Logo通道1打开命令", null);
                                lightNoGumLogoCH1CloseCmd = INI.INIGetStringValue(bRecipeRenaultFilePath, "频闪控制器", "未贴背胶的Logo通道1关闭命令", null);
                                lightLabeledGumCH1OpenCmd = INI.INIGetStringValue(bRecipeRenaultFilePath, "频闪控制器", "贴背胶的Logo通道1打开命令", null);
                                lightLabeledGumCH1CloseCmd = INI.INIGetStringValue(bRecipeRenaultFilePath, "频闪控制器", "贴背胶的Logo通道1关闭命令", null);
                                lightGumedCH2OpenCmd = INI.INIGetStringValue(bRecipeRenaultFilePath, "频闪控制器", "拍背胶的通道2打开命令", null);
                                lightGumedCH2CloseCmd = INI.INIGetStringValue(bRecipeRenaultFilePath, "频闪控制器", "拍背胶的通道2关闭命令", null);
                                lightLabeledLogoCH2OpenCmd = INI.INIGetStringValue(bRecipeRenaultFilePath, "频闪控制器", "拍塑胶件的通道2打开命令", null);
                                lightLabeledLogoCH2CloseCmd = INI.INIGetStringValue(bRecipeRenaultFilePath, "频闪控制器", "拍塑胶件的通道2关闭命令", null);
                                lightCH3OpenCmd = INI.INIGetStringValue(bRecipeRenaultFilePath, "频闪控制器", "AOI偏位测试的通道3打开命令", null);
                                lightCH3CloseCmd = INI.INIGetStringValue(bRecipeRenaultFilePath, "频闪控制器", "AOI偏位测试的通道3关闭命令", null);
                                aoiProCenterXOffsetVmin = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "AOI偏位检测", "X中心偏移最小值", null));
                                aoiProCenterXOffsetVmax = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "AOI偏位检测", "X中心偏移最大值", null));
                                aoiProCenterYOffsetVmin = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "AOI偏位检测", "Y中心偏移最小值", null));
                                aoiProCenterYOffsetVmax = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "AOI偏位检测", "Y中心偏移最大值", null));
                                aoiProDegOffsetVmin = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "AOI偏位检测", "θ中心偏移最小值", null));
                                aoiProDegOffsetVmax = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "AOI偏位检测", "θ中心偏移最大值", null));
                                aoiProCenterOffsetX = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "AOI偏位检测", "X中心偏移补偿值", null));
                                aoiProCenterOffsetY = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "AOI偏位检测", "Y中心偏移补偿值", null));
                                aoiProCenterOffsetDeg = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "AOI偏位检测", "θ中心偏移补偿值", null));
                                pressDelayTime = Convert.ToInt32(INI.INIGetStringValue(bRecipeRenaultFilePath, "保压工位参数", "保压时间", null));
                                firToolXOffset = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "治具补偿参数", "Tool1X", null));
                                firToolYOffset = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "治具补偿参数", "Tool1Y", null));
                                firToolDegOffset = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "治具补偿参数", "Tool1Deg", null));
                                secToolXOffset = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "治具补偿参数", "Tool2X", null));
                                secToolYOffset = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "治具补偿参数", "Tool2Y", null));
                                secToolDegOffset = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "治具补偿参数", "Tool2Deg", null));
                                thrToolXOffset = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "治具补偿参数", "Tool3X", null));
                                thrToolYOffset = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "治具补偿参数", "Tool3Y", null));
                                thrToolDegOffset = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "治具补偿参数", "Tool3Deg", null));
                                fourToolXOffset = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "治具补偿参数", "Tool4X", null));
                                fourToolYOffset = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "治具补偿参数", "Tool4Y", null));
                                fourToolDegOffset = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "治具补偿参数", "Tool4Deg", null));
                                fiveToolXOffset = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "治具补偿参数", "Tool5X", null));
                                fiveToolYOffset = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "治具补偿参数", "Tool5Y", null));
                                fiveToolDegOffset = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "治具补偿参数", "Tool5Deg", null));
                                sixToolXOffset = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "治具补偿参数", "Tool6X", null));
                                sixToolYOffset = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "治具补偿参数", "Tool6Y", null));
                                sixToolDegOffset = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "治具补偿参数", "Tool6Deg", null));
                                // 2022-3-18 薛骄奎 增加第一工站电镀件偏位判断功能 - 
                                logoXmax = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "电镀件内外轮廓偏位判断值", "logoXmax", null));
                                logoXmin = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "电镀件内外轮廓偏位判断值", "logoXmin", null));
                                logoYmax = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "电镀件内外轮廓偏位判断值", "logoYmax", null));
                                logoYmin = Convert.ToDouble(INI.INIGetStringValue(bRecipeRenaultFilePath, "电镀件内外轮廓偏位判断值", "logoYmin", null));

                            }
                            else
                            {
                                FileStream fs = new FileStream(bRecipeRenaultFilePath, FileMode.CreateNew, FileAccess.ReadWrite);
                                fs.Close();
                                //写入一批键值  通过引用的ClassINI文件内建置的方法完成如下操作；
                                INI.INIWriteItems(bRecipeRenaultFilePath, "System", "当前工单=ABC002\0当前流水号=0\0当前料号=BCD323\0当前班别=0\0");
                                INI.INIWriteItems(bRecipeRenaultFilePath, "测高模组", "空载具高度=0\0模组最大值=0\0模组最小值=0\0");
                                INI.INIWriteItems(bRecipeRenaultFilePath, "第一工位示教点", "X=0\0Y=0\0角度=0\0");
                                INI.INIWriteItems(bRecipeRenaultFilePath, "第二工位示教点", "X=0\0Y=0\0角度=0\0");
                                INI.INIWriteItems(bRecipeRenaultFilePath, "第三工位示教点", "X=0\0Y=0\0角度=0\0");
                                INI.INIWriteItems(bRecipeRenaultFilePath, "第四工位示教点", "X=0\0Y=0\0角度=0\0");
                                INI.INIWriteItems(bRecipeRenaultFilePath, "频闪控制器", "未贴背胶的Logo通道1打开命令=#1234\0未贴背胶的Logo通道1关闭命令=#232\0贴背胶的Logo通道1打开命令=#232\0贴背胶的Logo通道1关闭命令=#232\0拍背胶的通道2打开命令=#232\0拍背胶的通道2关闭命令=#232\0拍塑胶件的通道2打开命令=#232\0拍塑胶件的通道2关闭命令=#232\0AOI偏位测试的通道3打开命令=#232\0AOI偏位测试的通道3关闭命令=#232\0");
                                INI.INIWriteItems(bRecipeRenaultFilePath, "AOI偏位检测", "X中心偏移最小值=0\0X中心偏移最大值=0\0Y中心偏移最小值=0\0Y中心偏移最大值=0\0θ中心偏移最小值=0\0θ中心偏移最大值=0\0X中心偏移补偿值=0\0Y中心偏移补偿值=0\0θ中心偏移补偿值=0\0");
                                INI.INIWriteItems(bRecipeRenaultFilePath, "保压工位参数", "保压时间=0\0");
                            }
                        }
                        break;
                    case "Lada":
                        {

                        }
                        break;
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
            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(labelOutMarkSensor.Split(',')[0]), PinDefinition = ushort.Parse(labelOutMarkSensor.Split(',')[1]), PinDefinitionName = "出标感应器" });
            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(divCamSensor.Split(',')[0]), PinDefinition = ushort.Parse(divCamSensor.Split(',')[1]), PinDefinitionName = "凸轮分割器旋转到位" });
            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(deviceSafeDoor.Split(',')[0]), PinDefinition = ushort.Parse(deviceSafeDoor.Split(',')[1]), PinDefinitionName = "安全门感应" });
            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(robotVaccNoArrve.Split(',')[0]), PinDefinition = ushort.Parse(robotVaccNoArrve.Split(',')[1]), PinDefinitionName = "机器人真空未建立" });
            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(deviceEStop.Split(',')[0]), PinDefinition = ushort.Parse(deviceEStop.Split(',')[1]), PinDefinitionName = "设备急停信号" });
            lsAxiasDIs.Add(new LsIODIPinDefinition() { Card = ushort.Parse(robotIsArriveHomeOk.Split(',')[0]), PinDefinition = ushort.Parse(robotIsArriveHomeOk.Split(',')[1]), PinDefinitionName = "机器人是否回到Home" });
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
            lsAxiasDOs.Add(new LsIODOPinDefinition() { Card = ushort.Parse(allowDivCAMToRun.Split(',')[0]), PinDefinition = ushort.Parse(allowDivCAMToRun.Split(',')[1]), PinDefinitionName = "允许凸轮分割器旋转" });
            lsAxiasDOs.Add(new LsIODOPinDefinition() { Card = ushort.Parse(allowOpenRobotSafeDorr.Split(',')[0]), PinDefinition = ushort.Parse(allowOpenRobotSafeDorr.Split(',')[1]), PinDefinitionName = "允许打开机器人安全门" });
            lsAxiasDOs.Add(new LsIODOPinDefinition() { Card = ushort.Parse(allowDivCAMToNRun.Split(',')[0]), PinDefinition = ushort.Parse(allowDivCAMToNRun.Split(',')[1]), PinDefinitionName = "允许凸轮分割器反旋转" });
            lsAxiasDOs.Add(new LsIODOPinDefinition() { Card = ushort.Parse(allowAoiLightSensorOpen.Split(',')[0]), PinDefinition = ushort.Parse(allowAoiLightSensorOpen.Split(',')[1]), PinDefinitionName = "允许打开AOI光源" });//
        }
        public static string SendData(string datas)
        {
            try
            {
                string returnMsg = "SendOk";
                IList<MyAppSession> list = tcpServerEngine.GetSessions(s => s.Connected == true).ToList();
                if (tcpServerEngine != null)
                {
                    if (list.Count > 0)
                    {
                        MyAppSession appSession = list[0];
                        if (appSession == null)
                        {
                            returnMsg = "没有选中任何在线客户端！";
                            return returnMsg;
                        }
                        if (!appSession.Connected)
                        {
                            returnMsg = "目标客户端不在线！";
                            return returnMsg;
                        }
                        string msg = string.Format("{0}", datas + Environment.NewLine);//一定要加上分隔符
                        byte[] bMsg = System.Text.Encoding.UTF8.GetBytes(msg);//消息使用UTF-8编码
                                                                              //this.tcpServerEngine.GetSessionByID(appSession.SessionID).Send(new ArraySegment<byte>(bMsg, 0, bMsg.Length));
                        appSession.Send(new ArraySegment<byte>(bMsg, 0, bMsg.Length));
                        isEpsonConnected = true;
                    }
                    else
                    {
                        isEpsonConnected = false;
                    }
                }
                else
                {
                    returnMsg = "初始化通讯失败";
                }
                return returnMsg;
            }
            catch (Exception ee)
            {
                myLog.Error(ee.Message);
                return ee.Message;
            }
        }

    }
}
