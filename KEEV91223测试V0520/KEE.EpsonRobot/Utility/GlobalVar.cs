using ClassINI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KEE.EpsonRobot.Utility
{
    public class GlobalVar
    {
        /// <summary>
        /// 是否正确加载所有参数的标记
        /// </summary>
        public static bool isLoadAllParam = false;
        /// <summary>
        /// 程序是否随电脑开启一起启动:true：是;false：否
        /// </summary>
        public static bool powerIsOnOrOff = false;
        
        

        #region 上位机发送给Robot的数据定义
        /// <summary>
        /// 1:表示通知Robot走正常测试点位; 2: 表示通知Robot走标定动作点位
        /// </summary>
        public static int firstSendDataToRobot = 0;
        /// <summary>
        /// 1: 表示通知Robot图像识别OK;  2: 表示通知Robot图像识别NG
        /// </summary>
        public static int secondSendDataToRobot = 0;
        /// <summary>
        /// 表示通知Robot X方向走的绝对偏移量
        /// </summary>
        public static double threeSendDataToRobot = 0.0;
        /// <summary>
        /// 表示通知Robot Y方向走的绝对偏移量
        /// </summary>
        public static double fourSendDataToRobot = 0.0;
        /// <summary>
        /// 表示通知Robot U轴绝对偏移量
        /// </summary>
        public static double fiveSendDataToRobot = 0.0;
        #endregion 

        #region 第一工位变量
        /// <summary>
        /// 没有贴背胶前取料工位标定或者正常测试的标记
        /// </summary>
        public static bool isCam1CalOrTest = false;
        /// <summary>
        /// 没有贴背胶前取料工位9点标定次数
        /// </summary>
        public static int noLabelLogoNineCnt = 0;
        /// <summary>
        /// 没有贴背胶前取料工位工具路径
        /// </summary>
        public static string bFileNoLabelLogoToolPath = Application.StartupPath + @"\Tool\TB1.vpp";
        /// <summary>
        /// 没有贴背胶前取料工位5点标定次数
        /// </summary>
        public static int noLabelLogoFiveCnt = 0;
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
        /// 没有贴背胶前取料工位机器人工具坐标系示教X坐标值
        /// </summary>
        //public static double noLabelLogoTeachRobotTX = 0;
        /// <summary>
        /// 没有贴背胶前取料工位机器人工具坐标系示教Y坐标值
        /// </summary>
       // public static double noLabelLogoTeachRobotTY = 0;
        /// <summary>
        /// 没有贴背胶前取料工位机器人工具坐标系示教U轴坐标值
        /// </summary>
        //public static double noLabelLogoTeachRobotTU = 0;
        /// <summary>
        /// 标定特征是否寻找到
        /// </summary>
        public static bool noLabelLogoIsCalMatchNG = false;
        /// <summary>
        /// 测试特征是否寻找到
        /// </summary>
        public static bool noLabelLogoIsMatchNG = false;
        #endregion

        #region 第二工位变量
        /// <summary>
        /// 贴背胶前背胶工位标定或者正常测试的标记
        /// </summary>
        public static bool isCam2CalOrTest = false;
        /// <summary>
        /// 贴背胶前背胶工位工具路径
        /// </summary>
        public static string bFileNoGumToolPath = Application.StartupPath + @"\Tool\TB2.vpp";
        /// <summary>
        /// 贴背胶前背胶拍照9点标定次数
        /// </summary>
        public static int noGumNineCnt = 0;
        /// <summary>
        /// 贴背胶前背胶拍照5点标定次数
        /// </summary>
        //public static int noGumFiveCnt = 0;
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
        /// 轴中心在该相机下的坐标X值
        /// </summary>
        //public static double noGumRaxiasCenterX = 0;
        /// <summary>
        /// 轴中心在该相机下的坐标Y值
        /// </summary>
        //public static double noGumRaxiasCenterY = 0;
        /// <summary>
        /// 标定特征是否寻找到
        /// </summary>
        public static bool noGumIsCalMatchNG = false;
        /// <summary>
        /// 测试特征是否寻找到
        /// </summary>
        public static bool noGumIsMatchNG = false;
        #endregion

        #region 第三工位变量
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

        #region 第四工位变量
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

        /// <summary>
        /// 总配置路径
        /// </summary>       
        public static string bFilePath = Application.StartupPath + @"\Default.ini";

        public static void Initpara()
        {
            try
            {
                if (File.Exists(bFilePath))  //如果文件存在
                {
                    powerIsOnOrOff = Convert.ToBoolean(INI.INIGetStringValue(bFilePath, "System", "程序是否开启启动", null));
                    noLabelLogoRatioX = Convert.ToDouble(INI.INIGetStringValue(bFilePath, "第一工位比例尺", "X", null));
                    noLabelLogoRatioY = Convert.ToDouble(INI.INIGetStringValue(bFilePath, "第一工位比例尺", "Y", null));
                    noLabelLogoProTeachCenterX = Convert.ToDouble(INI.INIGetStringValue(bFilePath, "第一工位示教点", "X", null));
                    noLabelLogoProTeachCenterY = Convert.ToDouble(INI.INIGetStringValue(bFilePath, "第一工位示教点", "Y", null));
                    noLabelLogoProTeachDeg = Convert.ToDouble(INI.INIGetStringValue(bFilePath, "第一工位示教点", "角度", null));
                    noGumRatioX = Convert.ToDouble(INI.INIGetStringValue(bFilePath, "第二工位比例尺", "X", null));
                    noGumRatioY = Convert.ToDouble(INI.INIGetStringValue(bFilePath, "第二工位比例尺", "Y", null));
                    noGumProTeachCenterX = Convert.ToDouble(INI.INIGetStringValue(bFilePath, "第二工位示教点", "X", null));
                    noGumProTeachCenterY = Convert.ToDouble(INI.INIGetStringValue(bFilePath, "第二工位示教点", "Y", null));
                    noGumProTeachDeg = Convert.ToDouble(INI.INIGetStringValue(bFilePath, "第二工位示教点", "角度", null));
                    noGumTeachRobotTX = Convert.ToDouble(INI.INIGetStringValue(bFilePath, "第二工位Robot工具示教点", "X", null));
                    noGumTeachRobotTY = Convert.ToDouble(INI.INIGetStringValue(bFilePath, "第二工位Robot工具示教点", "Y", null));
                    noGumTeachRobotTU = Convert.ToDouble(INI.INIGetStringValue(bFilePath, "第二工位Robot工具示教点", "U", null));
                    labeledLogoRatioX = Convert.ToDouble(INI.INIGetStringValue(bFilePath, "第三工位比例尺", "X", null));
                    labeledLogoRatioY = Convert.ToDouble(INI.INIGetStringValue(bFilePath, "第三工位比例尺", "Y", null));
                    labeledLogoProTeachCenterX = Convert.ToDouble(INI.INIGetStringValue(bFilePath, "第三工位示教点", "X", null));
                    labeledLogoProTeachCenterY = Convert.ToDouble(INI.INIGetStringValue(bFilePath, "第三工位示教点", "Y", null));
                    labeledLogoProTeachDeg = Convert.ToDouble(INI.INIGetStringValue(bFilePath, "第三工位示教点", "角度", null));
                    gumedRatioX = Convert.ToDouble(INI.INIGetStringValue(bFilePath, "第四工位比例尺", "X", null));
                    gumedRatioY = Convert.ToDouble(INI.INIGetStringValue(bFilePath, "第四工位比例尺", "Y", null));
                    gumedProTeachCenterX = Convert.ToDouble(INI.INIGetStringValue(bFilePath, "第四工位示教点", "X", null));
                    gumedProTeachCenterY = Convert.ToDouble(INI.INIGetStringValue(bFilePath, "第四工位示教点", "Y", null));
                    gumedProTeachDeg = Convert.ToDouble(INI.INIGetStringValue(bFilePath, "第四工位示教点", "角度", null));
                    gumedTeachRobotTX = Convert.ToDouble(INI.INIGetStringValue(bFilePath, "第四工位Robot工具示教点", "X", null));
                    gumedTeachRobotTY = Convert.ToDouble(INI.INIGetStringValue(bFilePath, "第四工位Robot工具示教点", "Y", null));
                    gumedTeachRobotTU = Convert.ToDouble(INI.INIGetStringValue(bFilePath, "第四工位Robot工具示教点", "U", null));
                }
                else
                {
                    FileStream fs = new FileStream(bFilePath, FileMode.CreateNew, FileAccess.ReadWrite);
                    fs.Close();
                    //写入一批键值  通过引用的ClassINI文件内建置的方法完成如下操作；
                    INI.INIWriteItems(bFilePath, "System", "程序是否开启启动=true\0操作权限=0\0产品选择=false\0");
                    INI.INIWriteItems(bFilePath, "第一工位比例尺", "X=0\0Y=0\0");
                    INI.INIWriteItems(bFilePath, "第一工位示教点", "X=0\0Y=0\0角度=0\0");
                    //INI.INIWriteItems(bFilePath, "第一工位Robot工具示教点", "X=0\0Y=0\0U=0\0");
                    INI.INIWriteItems(bFilePath, "第一工位轴中心点", "X=0\0Y=0\0");
                    INI.INIWriteItems(bFilePath, "第二工位比例尺", "X=0\0Y=0\0");
                    INI.INIWriteItems(bFilePath, "第二工位示教点", "X=0\0Y=0\0角度=0\0");
                    INI.INIWriteItems(bFilePath, "第二工位Robot工具示教点", "X=0\0Y=0\0U=0\0");
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
