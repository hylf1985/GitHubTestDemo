using Cognex.VisionPro;
using Cognex.VisionPro.ToolBlock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cognex.VisionPro.FGGigE;
using KEE.EpsonRobot.Utility;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Cognex.VisionPro.Exceptions;

namespace KEE.EpsonRobot.CamOperator
{
    public class InspectionClass  //加载相机操作
    {
        #region 定义全局变量
        /// <summary>
        /// 定义主显示窗体
        /// </summary>
        //public static frmMain frmMain;
        /// <summary>
        /// 定义主显示Cogdisplay1
        /// </summary>
        //public static CogRecordDisplay resultDisplay1;
        /// <summary>
        /// 定义主显示Cogdisplay2
        /// </summary>
        //public static CogRecordDisplay resultDisplay2;
        /// <summary>
        /// 定义主显示Cogdisplay3
        /// </summary>
        //public static CogRecordDisplay resultDisplay3;
        /// <summary>
        /// 取料位置图像
        /// </summary>
        public static CogImage8Grey global1308GreyImage1;
        /// <summary>
        /// 放料位置图像
        /// </summary>
        public static CogImage8Grey global1308GreyImage2;
        /// <summary>
        /// CogBlockTool工具1
        /// </summary>
        public static CogToolBlock tb1 = new CogToolBlock();
        /// <summary>
        /// CogBlockTool工具2
        /// </summary>
        public static CogToolBlock tb2 = new CogToolBlock();
        /// <summary>
        /// CogBlockTool工具3
        /// </summary>
        public static CogToolBlock tb3 = new CogToolBlock();
        /// <summary>
        /// CogBlockTool工具3
        /// </summary>
        public static CogToolBlock tb4 = new CogToolBlock();
        /// <summary>
        /// 定义变量是否找到相机
        /// </summary>
        public static bool camIsFind = false;
        /// <summary>
        /// 加载参数是否OK
        /// </summary>
        public static bool isLoadAllParam = false;
        /// <summary>
        /// 相机1和软件是否匹配，相机与软件绑定使用
        /// </summary>
        public static bool cameraIsMatch1 = true;
        /// <summary>
        /// 相机2和软件是否匹配，相机与软件绑定使用
        /// </summary>
        public static bool cameraIsMatch2 = true;
        #endregion

        #region 私有变量
        CogFrameGrabberGigEs mGigEGrabber;   //VisionPro 调用相机的私有变量指令
        #endregion

        #region 定义构造函数和公用以及私用方法方法
        public InspectionClass() 
        {
            mGigEGrabber = new CogFrameGrabberGigEs();  

            //定义相机的数量
            if (mGigEGrabber.Count == 2)   //判断相机数量是否为设定值
            {
                camIsFind = true;
                foreach (ICogFrameGrabber item in mGigEGrabber)  //分别遍历mGigEGrabber内的每一个相机的信息  
                {
                    if (item.SerialNumber.Contains("00F25942548"))   //与相机1的ID绑定，如果更换相机将无法运行程序
                    {
                        cameraIsMatch1 = true;
                        isLoadAllParam = true;
                    }
                    else if (item.SerialNumber.Contains("22969400"))   //与相机2的ID绑定，如果更换相机将无法运行程序
                    {
                        cameraIsMatch2 = true;
                        isLoadAllParam = true;
                    }
                    else
                    {
                        isLoadAllParam = false;   //未找到序列号为23686762的相机时退出
                    }
                }
            }
            else
	        {
                isLoadAllParam = false;  //未找到相机时退出
	        }

        }
            
        #endregion

        #region 公用方法
        /// <summary>
        /// 加载所有工具
        /// </summary>
        public string LoadAllTools()
        {
            return LoadNoGumTool() + LoadNoLabelLogoTool() + LoadLabeledLogoTool() +LoadGumedTool();  //加载所有工具，取料+放料
        }

        /// <summary>
        /// 关闭窗体之前注销掉TB1事件
        /// </summary>
        public void takeTB1_EventHandler()
        {
            tb1.Ran -= new EventHandler(TB1_Ran);
        }
        /// <summary>
        /// 关闭窗体之前注销掉TB2事件
        /// </summary>
        public void takeTB2_EventHandler()
        {
            tb2.Ran -= new EventHandler(TB2_Ran);
        }

        /// <summary>
        /// 关闭窗体之前注销掉TB1事件
        /// </summary>
        public void takeTB3_EventHandler()
        {
            tb3.Ran -= new EventHandler(TB3_Ran);
        }
        /// <summary>
        /// 关闭窗体之前注销掉TB2事件
        /// </summary>
        public void takeTB4_EventHandler()
        {
            tb4.Ran -= new EventHandler(TB4_Ran);
        }

        #endregion

        #region 私用方法

        /// <summary>
        /// 加载取料定位工具
        /// </summary>
        string LoadNoLabelLogoTool()
        {
            try
            {
                string VppPath = GlobalVar.bFileNoLabelLogoToolPath;  //调用GlobalVar中声明的TB1的路径文件
                if (File.Exists(VppPath))     //检验查询是否存在TB1文件
                {
                    tb1 = (CogToolBlock)CogSerializer.LoadObjectFromFile(VppPath, typeof(BinaryFormatter), CogSerializationOptionsConstants.Minimum);  //VP固定加载语法
                    runTB1_EventHandler();  //注册TB1的运行事件，注册完之后写TB1的运行方法
                    GlobalVar.isLoadAllParam = true;  //加载成功
                    return "";  
                }
                else   //当TB1文件不存在时
                {
                    //show 错误代码
                    //frmMain.LogError("视觉检测1的工具不存在，请检查");
                    GlobalVar.isLoadAllParam = false;  //加载失败
                    return "视觉检测1的工具不存在，请检查";
                }
            }
            catch (CogException e)
            {
                GlobalVar.isLoadAllParam = false;
                //frmMain.LogError("加载工具错误： "+ e.Message);
                return "加载工具错误： " + e.Message;
            }
        }

        /// <summary>
        /// 加载放料定位工具
        /// </summary>
        string LoadNoGumTool() 
        {
            try
            {
                string VppPath = GlobalVar.bFileNoGumToolPath;  //调用GlobalVar中声明的TB2的路径文件
                if (File.Exists(VppPath))
                {
                    tb2 = (CogToolBlock)CogSerializer.LoadObjectFromFile(VppPath, typeof(BinaryFormatter), CogSerializationOptionsConstants.Minimum);
                    runTB2_EventHandler();
                    GlobalVar.isLoadAllParam = true;
                    return "";
                }
                else
                {
                    //show 错误代码
                    //frmMain.LogError("视觉检测1的工具不存在，请检查");
                    GlobalVar.isLoadAllParam = false;
                    return "视觉检测2的工具不存在，请检查";
                }

            }
            catch (CogException e)
            {
                GlobalVar.isLoadAllParam = false;
                //frmMain.LogError("加载工具错误： "+ e.Message);
                return "加载工具错误： " + e.Message;
            }
        }

        /// <summary>
        /// 加载取料定位工具
        /// </summary>
        string LoadLabeledLogoTool()
        {
            try
            {
                string VppPath = GlobalVar.bFileLabeledLogoToolPath;  //调用GlobalVar中声明的TB1的路径文件
                if (File.Exists(VppPath))     //检验查询是否存在TB1文件
                {
                    tb3 = (CogToolBlock)CogSerializer.LoadObjectFromFile(VppPath, typeof(BinaryFormatter), CogSerializationOptionsConstants.Minimum);  //VP固定加载语法
                    runTB3_EventHandler();  //注册TB1的运行事件，注册完之后写TB1的运行方法
                    GlobalVar.isLoadAllParam = true;  //加载成功
                    return "";
                }
                else   //当TB1文件不存在时
                {
                    //show 错误代码
                    //frmMain.LogError("视觉检测1的工具不存在，请检查");
                    GlobalVar.isLoadAllParam = false;  //加载失败
                    return "视觉检测3的工具不存在，请检查";
                }
            }
            catch (CogException e)
            {
                GlobalVar.isLoadAllParam = false;
                //frmMain.LogError("加载工具错误： "+ e.Message);
                return "加载工具错误： " + e.Message;
            }
        }

        /// <summary>
        /// 加载放料定位工具
        /// </summary>
        string LoadGumedTool()
        {
            try
            {
                string VppPath = GlobalVar.bFileGumedToolPath;  //调用GlobalVar中声明的TB2的路径文件
                if (File.Exists(VppPath))
                {
                    tb4 = (CogToolBlock)CogSerializer.LoadObjectFromFile(VppPath, typeof(BinaryFormatter), CogSerializationOptionsConstants.Minimum);
                    runTB4_EventHandler();
                    GlobalVar.isLoadAllParam = true;
                    return "";
                }
                else
                {
                    //show 错误代码
                    //frmMain.LogError("视觉检测1的工具不存在，请检查");
                    GlobalVar.isLoadAllParam = false;
                    return "视觉检测4的工具不存在，请检查";
                }

            }
            catch (CogException e)
            {
                GlobalVar.isLoadAllParam = false;
                //frmMain.LogError("加载工具错误： "+ e.Message);
                return "加载工具错误： " + e.Message;
            }
        }


        /// <summary>
        /// 运行TB1工具
        /// </summary>
        void runTB1_EventHandler()
        {
            tb1.Ran += new EventHandler(TB1_Ran);  //VP固定触发方法，运行TB1文件
        }

        /// <summary>
        /// 运行TB1工具
        /// </summary>
        void runTB2_EventHandler()
        {
            tb2.Ran += new EventHandler(TB2_Ran);    //VP固定触发方法，运行TB2文件
        }

        void runTB3_EventHandler()
        {
            tb3.Ran += new EventHandler(TB3_Ran);    //VP固定触发方法，运行TB2文件
        }
        void runTB4_EventHandler()
        {
            tb4.Ran += new EventHandler(TB4_Ran);    //VP固定触发方法，运行TB2文件
        }
        #endregion

        #region 事件方法
        void TB1_Ran(object sender, EventArgs e)   //获取TB1运行的结果数据
        {
            try
            {
                //resultDisplay1.Record = GlobalVar.isLoadAllParam ? tb1.CreateLastRunRecord().SubRecords[1] : tb1.CreateLastRunRecord().SubRecords[9];//IPOneITool1.CreateLastRunRecord();
                //resultDisplay1.Image = tb1.Outputs["InspectionImage"].Value as CogImage8Grey;
                //global1308GreyImage1 = tb1.Outputs["InspectionImage"].Value as CogImage8Grey; 
                //resultDisplay1.Fit(true);
            }
            catch (CogException e1)
            {
                //frmMain.LogWarning("TB1 错误信息： " + e1.Message);
            }
        }

        void TB2_Ran(object sender, EventArgs e)   //获取TB2运行的结果数据
        {
            try
            {
                //resultDisplay2.Record = GlobalVar.isLoadAllParam ? tb2.CreateLastRunRecord().SubRecords[1] : tb2.CreateLastRunRecord().SubRecords[9];//IPOneITool1.CreateLastRunRecord();
                //resultDisplay2.Image = tb2.Outputs["InspectionImage"].Value as CogImage8Grey;
                //global1308GreyImage2 = tb2.Outputs["InspectionImage"].Value as CogImage8Grey;
                //resultDisplay2.Fit(true);
            }
            catch (CogException e1)
            {
                //frmMain.LogWarning("TB2 错误信息： " + e1.Message);
            }
        }

        void TB3_Ran(object sender, EventArgs e)   //获取TB3运行的结果数据
        {
            try
            {
                //resultDisplay3.Record = tb3.CreateLastRunRecord().SubRecords[1];
                //resultDisplay3.Image = global1308GreyImage2;
                //resultDisplay3.Fit(true);
            }
            catch (CogException e1)
            {
                //frmMain.LogWarning("TB3 错误信息： " + e1.Message);
            }
        }

        void TB4_Ran(object sender, EventArgs e)   //获取TB3运行的结果数据
        {
            try
            {
                //resultDisplay3.Record = tb3.CreateLastRunRecord().SubRecords[1];
                //resultDisplay3.Image = global1308GreyImage2;
                //resultDisplay3.Fit(true);
            }
            catch (CogException e1)
            {
                //frmMain.LogWarning("TB3 错误信息： " + e1.Message);
            }
        }

        #endregion

    }
}
