using ClassINI;
using csIOC0640;
using csLTDMC;
using KEE.Renault.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KEE.Renault.MotionForm
{
    public partial class FrmPressAltimetry : Form
    {
        public FrmPressAltimetry()
        {
            InitializeComponent();
            InitCfgUIData();
            InitAllPictures();
        }

        #region 变量
        bool isClickRotate = false;
        bool isRotateSignalOn = false;
        List<PictureBox> picList = new List<PictureBox>();
        CancellationTokenSource tokenSource = null;
        bool isTimeOut = true;
        #endregion 

        #region 窗体事件
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                TraversalAllPic();
                //Application.DoEvents();
            }));
        }

        private void btnSaveDiVal_Click(object sender, EventArgs e)
        {
            if (JudgeComboBoxIsNullOrSpace(cmbFeedStationSensor)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbLabelStationSensor)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbPressStationSensor)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbHighTestStationSensor)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbAoiTestStationSensor)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbTakeStationSensor)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbPushCylinerHighLim)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbPushCylinerLowLim)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbDivCamSensor)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbDeviceStart)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbDeviceStop)) return;
            GlobalVar.feedStationSensor = cmbFeedStationSensor.SelectedItem.ToString();
            GlobalVar.labelStationSensor = cmbLabelStationSensor.SelectedItem.ToString();
            GlobalVar.pressStationSensor = cmbPressStationSensor.SelectedItem.ToString();
            GlobalVar.highTestStationSensor = cmbHighTestStationSensor.SelectedItem.ToString();
            GlobalVar.aoiTestStationSensor = cmbAoiTestStationSensor.SelectedItem.ToString();
            GlobalVar.takeStationSensor = cmbTakeStationSensor.SelectedItem.ToString();
            GlobalVar.pushCylinerHighLim = cmbPushCylinerHighLim.SelectedItem.ToString();
            GlobalVar.pushCylinerLowLim = cmbPushCylinerLowLim.SelectedItem.ToString();
            GlobalVar.divCamSensor = cmbDivCamSensor.SelectedItem.ToString();
            GlobalVar.deviceBoot = cmbDeviceStart.SelectedItem.ToString();
            GlobalVar.deviceStop = cmbDeviceStop.SelectedItem.ToString();
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输入信号配置", "上料工站料感信号", GlobalVar.feedStationSensor.ToString());
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输入信号配置", "贴标工站料感信号", GlobalVar.labelStationSensor.ToString());
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输入信号配置", "压合工站料感信号", GlobalVar.pressStationSensor.ToString());
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输入信号配置", "测高工站料感信号", GlobalVar.highTestStationSensor.ToString());
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输入信号配置", "AOI偏位测试工站料感信号", GlobalVar.aoiTestStationSensor.ToString());
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输入信号配置", "下料工站料感信号", GlobalVar.takeStationSensor.ToString());
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输入信号配置", "贴Logo按压气缸上极限", GlobalVar.pushCylinerHighLim.ToString());
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输入信号配置", "贴Logo按压气缸下极限", GlobalVar.pushCylinerLowLim.ToString());
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输入信号配置", "凸轮分割器旋转到位", GlobalVar.divCamSensor.ToString());
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输入信号配置", "设备启动按钮", GlobalVar.deviceBoot.ToString());
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输入信号配置", "设备停止按钮", GlobalVar.deviceStop.ToString());
        }

        private void btnSaveDoV_Click(object sender, EventArgs e)
        {
            if (JudgeComboBoxIsNullOrSpace(cmbPushCylinderElecValveP)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbAllowDivCamRotateP)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbRedLightP)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbYellowLightP)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbGreenLightP)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbBuzzerP)) return;
            GlobalVar.pushCylinderElecValve = cmbPushCylinderElecValveP.SelectedItem.ToString();
            GlobalVar.allowDivCAMToRun = cmbAllowDivCamRotateP.SelectedItem.ToString();
            GlobalVar.redAlarmLight = cmbRedLightP.SelectedItem.ToString();
            GlobalVar.yellowAlarmLight = cmbYellowLightP.SelectedItem.ToString();
            GlobalVar.greenAlarmLight = cmbGreenLightP.SelectedItem.ToString();
            GlobalVar.buzzingAlarm = cmbBuzzerP.SelectedItem.ToString();
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输出信号配置", "贴Logo按压气缸电磁阀", GlobalVar.pushCylinderElecValve.ToString());
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输出信号配置", "允许凸轮分割器旋转", GlobalVar.allowDivCAMToRun.ToString());
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输出信号配置", "三色灯-红灯", GlobalVar.redAlarmLight.ToString());
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输出信号配置", "三色灯-黄灯", GlobalVar.yellowAlarmLight.ToString());
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输出信号配置", "三色灯-绿灯", GlobalVar.greenAlarmLight.ToString());
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输出信号配置", "三色灯-蜂鸣", GlobalVar.buzzingAlarm.ToString());
        }

        private void btnPushCylinderElecValve_Click(object sender, EventArgs e)
        {
            if (!GlobalVar.lsAxiasDIs[44].CurIOStatus)
            {
                MessageBox.Show("急停信号按下，禁止操作");
            }
            if (!GlobalVar.totalRunFlag)
            {
                if (!GlobalVar.lsAxiasDIs[41].CurIOStatus)
                {
                    ushort ioIndex = Convert.ToUInt16(((Button)sender).Tag.ToString().Substring(2));
                    IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[ioIndex].Card - 1), GlobalVar.lsAxiasDOs[ioIndex].PinDefinition, GlobalVar.lsAxiasDOs[ioIndex].CurIOStatus ? (ushort)1 : (ushort)0);
                }
                else
                {
                    MessageBox.Show("轮盘停止位置信号处于未停止状态，请先将轮盘转到停止信号位置");
                }
            }
            else
            {
                MessageBox.Show("设备正在全自动模式，禁止操作");
            }
           
        }

        private void btnAllowDivCamRotate_Click(object sender, EventArgs e)
        {
            if (!GlobalVar.lsAxiasDIs[44].CurIOStatus)
            {
                MessageBox.Show("急停信号按下，禁止操作");
            }
            if (!GlobalVar.totalRunFlag)
            {
                if (GlobalVar.lsAxiasDIs[20].CurIOStatus && !GlobalVar.lsAxiasDIs[21].CurIOStatus && GlobalVar.lsAxiasDIs[22].CurIOStatus && !GlobalVar.lsAxiasDIs[23].CurIOStatus && GlobalVar.lsAxiasDIs[14].CurIOStatus && !GlobalVar.lsAxiasDIs[15].CurIOStatus && GlobalVar.lsAxiasDIs[24].CurIOStatus && !GlobalVar.lsAxiasDIs[25].CurIOStatus && GlobalVar.lsAxiasDIs[26].CurIOStatus && !GlobalVar.lsAxiasDIs[27].CurIOStatus)
                {
                    if (!isClickRotate)
                    {
                        isClickRotate = true;
                        tokenSource = new CancellationTokenSource(3000);
                        tokenSource.Token.Register(new Action(() => { isClickRotate = false; DialogResult = isTimeOut ? MessageBox.Show("轮盘转动超时") : MessageBox.Show("轮盘转动到位"); }));
                        ushort ioIndex = Convert.ToUInt16(((Button)sender).Tag.ToString().Substring(2));
                        IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[ioIndex].Card - 1), GlobalVar.lsAxiasDOs[ioIndex].PinDefinition, (ushort)0);
                        Thread.Sleep(800);
                        Task.Factory.StartNew(new Action(() =>
                        {
                            while (!tokenSource.Token.IsCancellationRequested)
                            {
                                Thread.Sleep(5);
                                Application.DoEvents();
                                if (!GlobalVar.lsAxiasDIs[41].CurIOStatus)
                                {
                                    IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[ioIndex].Card - 1), GlobalVar.lsAxiasDOs[ioIndex].PinDefinition, (ushort)1);
                                    isTimeOut = false;
                                    tokenSource.Cancel();
                                }
                            }
                        }), tokenSource.Token);
                    }
                }
                else
                {
                    MessageBox.Show("轮盘有气缸处于下降状态，禁止操作");
                    return;
                }
            }
            else
            {
                MessageBox.Show("设备正在全自动模式，禁止操作");
            }
        }

        private void btnRedLightOut_Click(object sender, EventArgs e)
        {
            if (!GlobalVar.lsAxiasDIs[44].CurIOStatus)
            {
                MessageBox.Show("急停信号按下，禁止操作");
            }
            if (!GlobalVar.totalRunFlag)
            {
                ushort ioIndex = Convert.ToUInt16(((Button)sender).Tag.ToString().Substring(2));
                IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[ioIndex].Card - 1), GlobalVar.lsAxiasDOs[ioIndex].PinDefinition, GlobalVar.lsAxiasDOs[ioIndex].CurIOStatus ? (ushort)1 : (ushort)0);
            }
            else
            {
                MessageBox.Show("设备正在全自动模式，禁止操作");
            }
            
        }

        private void btnYellowLightOut_Click(object sender, EventArgs e)
        {
            if (!GlobalVar.lsAxiasDIs[44].CurIOStatus)
            {
                MessageBox.Show("急停信号按下，禁止操作");
            }
            if (!GlobalVar.totalRunFlag)
            {
                ushort ioIndex = Convert.ToUInt16(((Button)sender).Tag.ToString().Substring(2));
                IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[ioIndex].Card - 1), GlobalVar.lsAxiasDOs[ioIndex].PinDefinition, GlobalVar.lsAxiasDOs[ioIndex].CurIOStatus ? (ushort)1 : (ushort)0);
            }
            else
            {
                MessageBox.Show("设备正在全自动模式，禁止操作");
            }
            
        }

        private void btnGreenLightOut_Click(object sender, EventArgs e)
        {
            if (!GlobalVar.lsAxiasDIs[44].CurIOStatus)
            {
                MessageBox.Show("急停信号按下，禁止操作");
            }
            if (!GlobalVar.totalRunFlag)
            {
                ushort ioIndex = Convert.ToUInt16(((Button)sender).Tag.ToString().Substring(2));
                IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[ioIndex].Card - 1), GlobalVar.lsAxiasDOs[ioIndex].PinDefinition, GlobalVar.lsAxiasDOs[ioIndex].CurIOStatus ? (ushort)1 : (ushort)0);
            }
            else
            {
                MessageBox.Show("设备正在全自动模式，禁止操作");
            }
           
        }

        private void btnBuzzerOut_Click(object sender, EventArgs e)
        {
            if (!GlobalVar.lsAxiasDIs[44].CurIOStatus)
            {
                MessageBox.Show("急停信号按下，禁止操作");
            }
            if (!GlobalVar.totalRunFlag)
            {
                ushort ioIndex = Convert.ToUInt16(((Button)sender).Tag.ToString().Substring(2));
                IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[ioIndex].Card - 1), GlobalVar.lsAxiasDOs[ioIndex].PinDefinition, GlobalVar.lsAxiasDOs[ioIndex].CurIOStatus ? (ushort)1 : (ushort)0);
            }
            else
            {
                MessageBox.Show("设备正在全自动模式，禁止操作");
            }
           
        }

        private void FrmPressAltimetry_Enter(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void FrmPressAltimetry_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }
        #endregion

        #region 其他方法
        private void TraversalAllPic()
        {
            foreach (PictureBox pic in picList)
            {
                if (pic.Tag == null)
                {
                    continue;
                }
                int index = Convert.ToInt32(pic.Tag.ToString().Substring(2));
                string name = pic.Tag.ToString().Substring(0, 2);
                switch (name)
                {
                    case "DO":
                        {
                            pic.Image = GlobalVar.lsAxiasDOs[index].PinStatus;
                        }
                        break;
                    case "DI":
                        {
                            pic.Image = GlobalVar.lsAxiasDIs[index].PinStatus;
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// 判断TextBox是否为空
        /// </summary>
        /// <param name="tb"></param>
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
        /// <summary>
        /// 判断TextBox是否为空
        /// </summary>
        /// <param name="tb"></param>
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
        private void InitCfgUIData()
        {
            cmbFeedStationSensor.SelectedItem = GlobalVar.feedStationSensor;
            cmbLabelStationSensor.SelectedItem = GlobalVar.labelStationSensor;
            cmbPressStationSensor.SelectedItem = GlobalVar.pressStationSensor;
            cmbHighTestStationSensor.SelectedItem = GlobalVar.highTestStationSensor;
            cmbAoiTestStationSensor.SelectedItem = GlobalVar.aoiTestStationSensor;
            cmbTakeStationSensor.SelectedItem = GlobalVar.takeStationSensor;
            cmbPushCylinerHighLim.SelectedItem = GlobalVar.pushCylinerHighLim;
            cmbPushCylinerLowLim.SelectedItem = GlobalVar.pushCylinerLowLim;
            cmbDivCamSensor.SelectedItem = GlobalVar.divCamSensor;
            cmbDeviceStart.SelectedItem = GlobalVar.deviceBoot;
            cmbDeviceStop.SelectedItem = GlobalVar.deviceStop;
            cmbPushCylinderElecValveP.SelectedItem = GlobalVar.pushCylinderElecValve;
            cmbAllowDivCamRotateP.SelectedItem = GlobalVar.allowDivCAMToRun;
            cmbRedLightP.SelectedItem = GlobalVar.redAlarmLight;
            cmbYellowLightP.SelectedItem = GlobalVar.yellowAlarmLight;
            cmbGreenLightP.SelectedItem = GlobalVar.greenAlarmLight;
            cmbBuzzerP.SelectedItem = GlobalVar.buzzingAlarm;

        }
        private void InitAllPictures()
        {
            foreach (var pic in groupBox2.Controls)
            {
                if (pic is PictureBox)
                {
                    picList.Add((PictureBox)pic);
                }

            }
            foreach (var pic in panel3.Controls)
            {
                if (pic is PictureBox)
                {
                    picList.Add((PictureBox)pic);
                }

            }

        }

        #endregion

    }
}
