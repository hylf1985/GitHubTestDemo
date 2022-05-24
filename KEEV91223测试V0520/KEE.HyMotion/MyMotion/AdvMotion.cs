using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advantech.Motion;

namespace KEE.HyMotion.MyMotion
{
    public class AdvMotion
    {
        #region 变量声明
        /// <summary>
        ///  轴卡反馈状态代码
        /// </summary>
        private int axiasReturnStatusCode;
        /// <summary>
        /// 轴卡反馈状态信息
        /// </summary>
        private string axiasReturnStatusMessage;
        /// <summary>
        /// 定义轴卡列表
        /// </summary>
        DEV_LIST[] curAvailableDevs = new DEV_LIST[Motion.MAX_DEVICES];
        /// <summary>
        /// 轴卡有效句柄
        /// </summary>
        IntPtr m_DeviceHandle = IntPtr.Zero;
        /// <summary>
        /// 轴卡数量
        /// </summary>
        uint deviceCount = 0;
        /// <summary>
        /// 轴卡轴数
        /// </summary>
        uint axiasCountPerDev = 0;
        /// <summary>
        /// 对应轴卡的轴数句柄
        /// </summary>
        IntPtr[] m_Axishand = new IntPtr[4];
        /// <summary>
        /// 轴卡初始化OK状态
        /// </summary>
        bool axiasInitStatus = false;
        /// <summary>
        /// 对应4轴使能信号状态
        /// </summary>
        bool[] sVOnStatus ;
        /// <summary>
        /// 对应4轴Ready信号状态
        /// </summary>
        bool[] redyOkStatus;
        /// <summary>
        /// 对应4轴Alarm信号状态
        /// </summary>
        bool[] almOnStatus;
        /// <summary>
        /// 对应4轴正硬件限位信号状态
        /// </summary>
        bool[] lmtPStatus;
        /// <summary>
        /// 对应4轴负极限位信号状态
        /// </summary>
        bool[] lmtNStatus;
        /// <summary>
        /// 对应4轴原点信号状态
        /// </summary>
        bool[] orgStatus ;
        /// <summary>
        /// 对应4轴Dir输出状态
        /// </summary>
        bool[] dirStatus ;
        /// <summary>
        /// 对应4轴紧急信号输入状态
        /// </summary>
        bool[] emgStatus ;
        /// <summary>
        /// 对应4轴输出偏转计数器清除信号至伺服电机驱动(out7)
        /// </summary>
        bool[] ercStatus ;
        /// <summary>
        /// 对应4轴编码器Z信号
        /// </summary>
        bool[] ezStatus ;
        /// <summary>
        /// 对应4轴到位信号输入
        /// </summary>
        bool[] inpOnStatus ;
        /// <summary>
        /// 对应4轴报警复位输出状态
        /// </summary>
        bool[] alarmsOnStatus;
        #endregion

        #region 属性
        /// <summary>
        /// 轴卡反馈状态代码
        /// </summary>
        public int AxiasReturnStatusCode
        {
            get { return axiasReturnStatusCode; }
            set { axiasReturnStatusCode = value; }
        }
        /// <summary>
        /// 轴卡反馈状态信息
        /// </summary>
        public string AxiasReturnStatusMessage
        {
            get { return axiasReturnStatusMessage; }
            set { axiasReturnStatusMessage = value; }
        }
        /// <summary>
        /// 轴卡初始化状态：true或者false
        /// </summary>
        public bool AxiasInitStatus
        {
            get { return axiasInitStatus; }
            set { axiasInitStatus = value; }
        }
        /// <summary>
        /// 对应4轴控制句柄
        /// </summary>
        public IntPtr[] AxiasHand
        {
            get { return m_Axishand; }
            set { m_Axishand = value; }
        }
        /// <summary>
        /// 对应4轴使能信号状态
        /// </summary>
        public bool[] SVOnStatus
        {
            get
            {
                sVOnStatus = new bool[4] { false, false, false, false };
                return sVOnStatus;
            }
            set { sVOnStatus = value; }
        }
        public bool[] RedyOkStatus
        {
            get
            {
                redyOkStatus = new bool[4] { false, false, false, false };
                return redyOkStatus;
            }
            set { redyOkStatus = value; }
        }
        public bool[] AlmOnStatus
        {
            get
            {
                almOnStatus = new bool[4] { false, false, false, false };
                return almOnStatus;
            }
            set { almOnStatus = value; }
        }
        public bool[] LMTPStatus
        {
            get
            {
                lmtPStatus = new bool[4] { false, false, false, false };
                return lmtPStatus;
            }
            set { lmtPStatus = value; }
        }
        public bool[] LMTNStatus
        {
            get
            {
                lmtNStatus = new bool[4] { false, false, false, false };
                return lmtNStatus;
            }
            set { lmtNStatus = value; }
        }
        public bool[] OrgStatus
        {
            get
            {
                orgStatus = new bool[4] { false, false, false, false };
                return orgStatus;
            }
            set { orgStatus = value; }
        }
        public bool[] DirStatus
        {
            get
            {
                dirStatus = new bool[4] { false, false, false, false };
                return dirStatus;
            }
            set { dirStatus = value; }
        }
        public bool[] EMGStatus
        {
            get
            {
                emgStatus = new bool[4] { false, false, false, false };
                return emgStatus;
            }
            set { emgStatus = value; }
        }
        public bool[] ERCStatus
        {
            get
            {
                ercStatus = new bool[4] { false, false, false, false };
                return ercStatus;
            }
            set { ercStatus = value; }
        }
        public bool[] EZStatus
        {
            get
            {
                ezStatus = new bool[4] { false, false, false, false };
                return ezStatus;
            }
            set { ezStatus = value; }
        }
        public bool[] INPOnStatus
        {
            get
            {
                inpOnStatus = new bool[4] { false, false, false, false };
                return inpOnStatus;
            }
            set { inpOnStatus = value; }
        }
        public bool[] AlarmsResetOutStatus
        {
            get
            {
                alarmsOnStatus = new bool[4] { false, false, false, false };
                return alarmsOnStatus;
            }
            set { alarmsOnStatus = value; }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 判断是否有安装轴卡驱动
        /// </summary>
        /// <returns></returns>
        private bool GetDevCfgDllDrvVer() //Driver Version Number, this step is not necessary
        {
            string fileName = "";
            FileVersionInfo myFileVersionInfo;
            string FileVersion = "";
            fileName = Environment.SystemDirectory + "\\ADVMOT.dll";//SystemDirectory指System32 
            myFileVersionInfo = FileVersionInfo.GetVersionInfo(fileName);
            FileVersion = myFileVersionInfo.FileVersion;
            string[] strSplit = FileVersion.Split(',');
            if (Convert.ToUInt16(strSplit[0]) < 2)
            {
                axiasReturnStatusMessage = "The Driver Version  Is Too Low" + "\r\nYou can update the driver through the driver installation package ";
                axiasReturnStatusMessage = axiasReturnStatusMessage + "\r\nThe Current Driver Version Number is " + FileVersion;
                axiasReturnStatusMessage = axiasReturnStatusMessage + "\r\nYou need to update the driver to 2.0.0.0 version and above";
                return false;
            }
            return true;
        }
        /// <summary>
        /// 初始化轴卡
        /// </summary>
        /// <returns></returns>
        public bool InitAxiasCard()
        {
            try
            {
                if (!GetDevCfgDllDrvVer())
                {
                    axiasReturnStatusMessage = "请确认是否有安装研华轴卡驱动";
                    return false;
                }
                axiasReturnStatusCode = Motion.mAcm_GetAvailableDevs(curAvailableDevs, Motion.MAX_DEVICES, ref deviceCount);
                if (axiasReturnStatusCode != (int)ErrorCode.SUCCESS)
                {
                    axiasReturnStatusMessage = "Get Device Numbers Failed With Error Code: [0x" + Convert.ToString(axiasReturnStatusCode, 16) + "]";
                    return false;
                }
                if (deviceCount < 1)
                {
                    axiasReturnStatusMessage = "轴卡没有找到";
                    return false;
                }
                if (deviceCount > 1)
                {
                    axiasReturnStatusMessage = "有多张轴卡";
                    return false;
                }
                bool rescan = false;
                uint retry = 0;
                do
                {
                    axiasReturnStatusCode = (int)Motion.mAcm_DevOpen(curAvailableDevs[0].DeviceNum, ref m_DeviceHandle);
                    if (axiasReturnStatusCode != (uint)ErrorCode.SUCCESS)
                    {
                        axiasReturnStatusMessage = "Open Device Failed With Error Code[0x" + Convert.ToString(axiasReturnStatusCode, 16) + "]";
                        retry++;
                        rescan = true;
                        if (retry > 10)
                            return false;
                        System.Threading.Thread.Sleep(1000);
                    }
                } while (rescan == true);
                //获取轴数量
                axiasReturnStatusCode = (int)Motion.mAcm_GetU32Property(m_DeviceHandle, (uint)PropertyID.FT_DevAxesCount, ref axiasCountPerDev);
                if ((int)ErrorCode.SUCCESS != axiasReturnStatusCode)
                {
                    axiasReturnStatusMessage = "Get Axis Number Failed With Error Code: [0x" + Convert.ToString(axiasReturnStatusCode, 16) + "]";
                    return false;
                }
                for (int i = 0; i < axiasCountPerDev; i++)
                {
                    axiasReturnStatusCode = (int)Motion.mAcm_AxOpen(m_DeviceHandle, (UInt16)i, ref m_Axishand[i]);
                    if ((uint)ErrorCode.SUCCESS != axiasReturnStatusCode)
                    {
                        axiasReturnStatusMessage = "Open Axis Failed With Error Code: [0x" + Convert.ToString(axiasReturnStatusCode, 16) + "]";
                        return false;
                    }
                    //默认设置命令模式，初始值为0
                    axiasReturnStatusCode = (int)Motion.mAcm_AxSetCmdPosition(m_Axishand[i], 0);
                    if ((uint)ErrorCode.SUCCESS != axiasReturnStatusCode)
                    {
                        axiasReturnStatusMessage = "Set CmdPosition Mode With Error Code: [0x" + Convert.ToString(axiasReturnStatusCode, 16) + "]";
                        return false;
                    }
                }
                axiasReturnStatusCode = (int)ErrorCode.SUCCESS;
                axiasInitStatus = true;
                return true;
            }
            catch (Exception ex)
            {
                axiasReturnStatusMessage = $"初始化轴卡发生未知错误 : {ex.Message}";
                return false;
            }
        }
        /// <summary>
        /// 关闭释放轴卡信息
        /// </summary>
        public void CloseAxiasBoard()
        {
            try
            {
                UInt16[] usAxisState = new UInt16[64];
                uint AxisNum;
                //Stop Every Axes
                if (axiasInitStatus == true)
                {
                    for (AxisNum = 0; AxisNum < axiasCountPerDev; AxisNum++)
                    {
                        //Get the axis's current state
                        axiasReturnStatusCode = (int)Motion.mAcm_AxGetState(m_Axishand[AxisNum], ref usAxisState[AxisNum]);
                        if (usAxisState[AxisNum] == (uint)AxisState.STA_AX_ERROR_STOP)
                        {
                            // Reset the axis' state. If the axis is in ErrorStop state, the state will be changed to Ready after calling this function
                            Motion.mAcm_AxResetError(m_Axishand[AxisNum]);
                        }
                        //To command axis to decelerate to stop.
                        Motion.mAcm_AxStopDec(m_Axishand[AxisNum]);
                    }
                    //Close Axes
                    for (AxisNum = 0; AxisNum < axiasCountPerDev; AxisNum++)
                    {
                        Motion.mAcm_AxClose(ref m_Axishand[AxisNum]);
                    }
                    axiasCountPerDev = 0;
                    //Close Device
                    Motion.mAcm_DevClose(ref m_DeviceHandle);
                    m_DeviceHandle = IntPtr.Zero;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 4轴同时使能ON
        /// </summary>
        public void SetAxiasAllServerOn()
        {
            if (!axiasInitStatus)
            {
                return ;
            }
            try
            {
                for (UInt32 i = 0; i < axiasCountPerDev; i++)
                {
                    axiasReturnStatusCode = (int)Motion.mAcm_AxSetSvOn(m_Axishand[i], 1);
                    if ((int)ErrorCode.SUCCESS != axiasReturnStatusCode)
                    {
                        axiasReturnStatusMessage = "Servo On Failed With Error Code: [0x" + Convert.ToString(axiasReturnStatusCode, 16) + "]";
                        return ;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 4轴同时使能OFF
        /// </summary>
        public void SetAxiasAllServerOff()
        {
            if (!axiasInitStatus)
            {
                return;
            }
            try
            {
                for (UInt32 i = 0; i < axiasCountPerDev; i++)
                {
                    axiasReturnStatusCode = (int)Motion.mAcm_AxSetSvOn(m_Axishand[i], 0);
                    if ((int)ErrorCode.SUCCESS != axiasReturnStatusCode)
                    {
                        axiasReturnStatusMessage = $"第{i+1}轴Servo Off Failed With Error Code: [0x" + Convert.ToString(axiasReturnStatusCode, 16) + "]";
                        return;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 设置指定轴使能On或者Off
        /// </summary>
        /// <param name="axiasIndex"></param>
        /// <param name="OnOrOff"></param>
        public void SetSpecifiedAxiaServerOnOrOff(int axiasIndex,bool OnOrOff)
        {
            if (!axiasInitStatus)
            {
                return;
            }
            try
            {
                axiasReturnStatusCode = (int)Motion.mAcm_AxSetSvOn(m_Axishand[axiasIndex], OnOrOff ? (uint)1 : 0);
                if ((int)ErrorCode.SUCCESS != axiasReturnStatusCode)
                {
                    axiasReturnStatusMessage = $"Servo OnOrOff Failed With Error Code: [0x" + Convert.ToString(axiasReturnStatusCode, 16) + "]";
                    return;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取轴的理论位置
        /// </summary>
        /// <param name="axiaIndex">轴号</param>
        /// <returns></returns>
        public double GetCurAxiaCmdPosition(int axiaIndex)
        {
            double curCmd = 0;
            if (AxiasInitStatus)
            {
                axiasReturnStatusCode = (int)Motion.mAcm_AxGetCmdPosition(m_Axishand[axiaIndex], ref curCmd);
                if ((int)ErrorCode.SUCCESS!=axiasReturnStatusCode)
                {
                    axiasReturnStatusMessage = $"获取第{axiaIndex+1}轴的理论位置失败";
                    return curCmd;
                }
            }
            return curCmd;
        }
        /// <summary>
        /// 获取轴的实际位置
        /// </summary>
        /// <param name="axiaIndex">轴号</param>
        /// <returns></returns>
        public double GetCurAxiaActualPosition(int axiaIndex)
        {
            double curCmd = 0;
            if (AxiasInitStatus)
            {
                axiasReturnStatusCode = (int)Motion.mAcm_AxGetActualPosition(m_Axishand[axiaIndex], ref curCmd);
                if ((int)ErrorCode.SUCCESS != axiasReturnStatusCode)
                {
                    axiasReturnStatusMessage = $"获取第{axiaIndex + 1}轴的实际位置失败";
                    return curCmd;
                }
            }
            return curCmd;
        }
        /// <summary>
        /// 设置当前轴的理论位置值
        /// </summary>
        /// <param name="axiaIndex">轴号</param>
        /// <param name="cmdPosition">理论位置值</param>
        public void SetCurAxiaCmdPosition(int axiaIndex,double cmdPosition)
        {
            if (AxiasInitStatus)
            {
                axiasReturnStatusCode = (int)Motion.mAcm_AxSetCmdPosition(m_Axishand[axiaIndex], cmdPosition);
                if ((int)ErrorCode.SUCCESS != axiasReturnStatusCode)
                {
                    axiasReturnStatusMessage = $"设置第{axiaIndex + 1}轴的理论位置失败";
                }
            }
        }
        /// <summary>
        /// 设置当前轴的实际位置值
        /// </summary>
        /// <param name="axiaIndex">轴号</param>
        /// <param name="cmdPosition">实际位置值</param>
        public void SetCurAxiaActualPosition(int axiaIndex, double cmdPosition)
        {
            if (AxiasInitStatus)
            {
                axiasReturnStatusCode = (int)Motion.mAcm_AxSetActualPosition(m_Axishand[axiaIndex], cmdPosition);
                if ((int)ErrorCode.SUCCESS != axiasReturnStatusCode)
                {
                    axiasReturnStatusMessage = $"设置第{axiaIndex + 1}轴的绝对位置失败";
                }
            }
        }
        /// <summary>
        /// 复位所有轴错误
        /// </summary>
        /// <param name="axiaIndex">指定轴号</param>
        public void ResetSpecAxiaError(int axiaIndex)
        {
            if (AxiasInitStatus)
            {
                axiasReturnStatusCode = (int)Motion.mAcm_AxResetError(m_Axishand[axiaIndex]);
                if ((int)ErrorCode.SUCCESS != axiasReturnStatusCode)
                {
                    axiasReturnStatusMessage = $"复位第{axiaIndex + 1}轴失败";
                }
            }
        }
        /// <summary>
        /// 复位所有轴错误
        /// </summary>
        public void ResetAllAxiaError()
        {
            if (AxiasInitStatus)
            {
                for (int i = 0; i < 4; i++)
                {
                    axiasReturnStatusCode = (int)Motion.mAcm_AxResetError(m_Axishand[i]);
                    if ((int)ErrorCode.SUCCESS != axiasReturnStatusCode)
                    {
                        axiasReturnStatusMessage = $"复位第{i + 1}轴失败";
                    }
                }
            }
        }
        /// <summary>
        /// 获取指定轴的IO状态
        /// </summary>
        /// <param name="axiaIndex"></param>
        public void GetCurMotionCardIO(int axiaIndex)
        {
            if (axiasInitStatus)
            {
                UInt32 ioStatus = new UInt32();
                axiasReturnStatusCode = (int)Motion.mAcm_AxGetMotionIO(m_Axishand[axiaIndex],ref ioStatus);
                if ((int)ErrorCode.SUCCESS != axiasReturnStatusCode)
                {
                    axiasReturnStatusMessage = $"获取第{axiaIndex + 1}轴IO状态失败";
                }
                sVOnStatus[axiaIndex] = ((ioStatus & (uint)Ax_Motion_IO.AX_MOTION_IO_SVON) > 0);
                redyOkStatus[axiaIndex] = ((ioStatus & (uint)Ax_Motion_IO.AX_MOTION_IO_RDY) > 0);
                almOnStatus[axiaIndex] = ((ioStatus & (uint)Ax_Motion_IO.AX_MOTION_IO_ALM) > 0);
                lmtPStatus[axiaIndex] = ((ioStatus & (uint)Ax_Motion_IO.AX_MOTION_IO_LMTP) > 0);
                lmtNStatus[axiaIndex] = ((ioStatus & (uint)Ax_Motion_IO.AX_MOTION_IO_LMTN) > 0);
                orgStatus[axiaIndex] = ((ioStatus & (uint)Ax_Motion_IO.AX_MOTION_IO_ORG) > 0);
                dirStatus[axiaIndex] = ((ioStatus & (uint)Ax_Motion_IO.AX_MOTION_IO_DIR) > 0);
                emgStatus[axiaIndex] = ((ioStatus & (uint)Ax_Motion_IO.AX_MOTION_IO_EMG) > 0);
                ercStatus[axiaIndex] = ((ioStatus & (uint)Ax_Motion_IO.AX_MOTION_IO_ERC) > 0);
                ezStatus[axiaIndex] = ((ioStatus & (uint)Ax_Motion_IO.AX_MOTION_IO_EZ) > 0);
                inpOnStatus[axiaIndex] = ((ioStatus & (uint)Ax_Motion_IO.AX_MOTION_IO_INP) > 0);
                alarmsOnStatus[axiaIndex] = ((ioStatus & (uint)Ax_Motion_IO.AX_MOTION_IO_ALRM) > 0);
            }
        }
        /// <summary>
        /// 获取轴的当前状态
        /// </summary>
        /// <param name="axiaIndex">轴号</param>
        public string GetCurMotionCardAxiaState(int axiaIndex)
        {
            UInt16 AxState = new UInt16();
            string axStateString = "NA";
            if (axiasInitStatus)
            {
                axiasReturnStatusCode = (int)Motion.mAcm_AxGetState(m_Axishand[axiaIndex], ref AxState);
                if ((int)ErrorCode.SUCCESS != axiasReturnStatusCode)
                {
                    axiasReturnStatusMessage = $"获取第{axiaIndex + 1}轴状态失败";
                }
                switch ((AxisState)AxState)
                {
                    case AxisState.STA_AX_DISABLE:
                        axStateString = "轴被禁用，用户可打开并激活";
                        break;
                    case AxisState.STA_AX_READY:
                        axStateString = "轴已准备就绪，等待新的命令";
                        break;
                    case AxisState.STA_AX_STOPPING:
                        axStateString = "轴停止";
                        break;
                    case AxisState.STA_AX_ERROR_STOP:
                        axStateString = "出现错误，轴停止";
                        break;
                    case AxisState.STA_AX_HOMING:
                        axStateString = "轴正在执行返回原点运动";
                        break;
                    case AxisState.STA_AX_PTP_MOT:
                        axStateString = "轴正在执行PTP运动";
                        break;
                    case AxisState.STA_AX_CONTI_MOT:
                        axStateString = "轴正在执行连续运动";
                        break;
                    case AxisState.STA_AX_SYNC_MOT:
                        axStateString = "轴在一个群组中，群组正在执行插补运动：或轴是一个从轴，正在执行E-cam/E-gear/Gantry运动";
                        break;
                    case AxisState.STA_AX_EXT_JOG:
                        axStateString = "轴有外部信号控制，当外部信号激活时，轴将执行JOG模式运动";
                        break;
                    case AxisState.STA_AX_EXT_MPG:
                        axStateString = "轴有外部信号控制，当外部信号激活时，轴将执行MPG模式运动";
                        break;
                    case AxisState.STA_AX_PAUSE:
                        axStateString = "轴暂停运动";
                        break;
                    case AxisState.STA_AX_BUSY:
                        axStateString = "轴处于忙的状态";
                        break;
                    case AxisState.STA_AX_WAIT_DI:
                        axStateString = "轴等待输入信号";
                        break;
                    case AxisState.STA_AX_WAIT_PTP:
                        axStateString = "轴等待点到点状态";
                        break;
                    case AxisState.STA_AX_WAIT_VEL:
                        axStateString = "轴等待速度设定状态";
                        break;
                }
            }
            return ((AxisState)AxState).ToString();
        }

        #endregion
    }
}
