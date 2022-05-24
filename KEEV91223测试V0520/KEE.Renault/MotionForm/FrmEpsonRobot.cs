using ClassINI;
using csIOC0640;
using KEE.Renault.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KEE.Renault.MotionForm
{
    public partial class FrmEpsonRobot : Form
    {
        public FrmEpsonRobot()
        {
            InitializeComponent();
            InitAllPictures();
            InitCfgToComboBox();
        }

        #region  变量定义
        List<PictureBox> picList = new List<PictureBox>();

        #endregion 

        #region 窗体事件

        private void FrmEpsonRobot_Enter(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void FrmEpsonRobot_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void btnControlEpsonIO_Click(object sender, EventArgs e)
        {
            if (!GlobalVar.lsAxiasDIs[44].CurIOStatus)
            {
                MessageBox.Show("急停信号按下，禁止操作");
            }
            if (!GlobalVar.totalRunFlag)
            {
                int index = Convert.ToInt32(((Button)sender).Tag.ToString().Substring(2));
                if (GlobalVar.lsAxiasDOs[index].CurIOStatus)
                {
                    IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[index].Card - 1), GlobalVar.lsAxiasDOs[index].PinDefinition, 1);
                }
                else
                {
                    IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[index].Card - 1), GlobalVar.lsAxiasDOs[index].PinDefinition, 0);
                }
            }
            else
            { MessageBox.Show("设备正在全自动模式，禁止操作"); }
             
        }

        private void btnControlRotateIO_Click(object sender, EventArgs e)
        {
            if (!GlobalVar.lsAxiasDIs[44].CurIOStatus)
            {
                MessageBox.Show("急停信号按下，禁止操作");
            }
            if (!GlobalVar.totalRunFlag)
            {
                int index = Convert.ToInt32(((Button)sender).Tag.ToString().Substring(2));
                if (GlobalVar.lsAxiasDOs[index].CurIOStatus)
                {
                    IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[index].Card - 1), GlobalVar.lsAxiasDOs[index].PinDefinition, 1);
                }
                else
                {
                    IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[index].Card - 1), GlobalVar.lsAxiasDOs[index].PinDefinition, 0);
                }
            }
            else
            {
                MessageBox.Show("设备正在全自动模式，禁止操作");
            }
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                TraversalAllPic();
               // Application.DoEvents();
            }));
        }
        private void btnSaveIODI_Click(object sender, EventArgs e)
        {
            if (JudgeComboBoxIsNullOrSpace(cmbEpsonStandbying)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbEpsonRunning)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbEpsonPausing)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbEpsonControllerErr)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbEpsonEMGOutput)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbEpsonSafeDoor)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbEpsonControllerFatalErr)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbEpsonAlarm)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbFeed4PcsLogoFinished)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbTearLabelStart)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbNewLabelStart)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbLabelLogoToPlasticFinished)) return;
            GlobalVar.epsonStandbying = cmbEpsonStandbying.SelectedItem.ToString();
            GlobalVar.epsonRunning = cmbEpsonRunning.SelectedItem.ToString();
            GlobalVar.epsonPausing = cmbEpsonPausing.SelectedItem.ToString();
            GlobalVar.epsonControllerErr = cmbEpsonControllerErr.SelectedItem.ToString();
            GlobalVar.epsonEMGOutput = cmbEpsonEMGOutput.SelectedItem.ToString();
            GlobalVar.epsonSafeDoor = cmbEpsonSafeDoor.SelectedItem.ToString();
            GlobalVar.epsonControllerFatalErr = cmbEpsonControllerFatalErr.SelectedItem.ToString();
            GlobalVar.epsonAlarm = cmbEpsonAlarm.SelectedItem.ToString();
            GlobalVar.feed4PcsLogoFinished = cmbFeed4PcsLogoFinished.SelectedItem.ToString();
            GlobalVar.tearLabelStart = cmbTearLabelStart.SelectedItem.ToString();
            GlobalVar.newLabelStart = cmbNewLabelStart.SelectedItem.ToString();
            GlobalVar.labelLogoToPlasticFinished = cmbLabelLogoToPlasticFinished.SelectedItem.ToString();
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输入信号配置", "机器人待机中", GlobalVar.epsonStandbying);
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输入信号配置", "机器人运行中", GlobalVar.epsonRunning);
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输入信号配置", "机器人暂停中", GlobalVar.epsonPausing);
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输入信号配置", "机器人控制器一般错误 ", GlobalVar.epsonControllerErr);
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输入信号配置", "机器人急停输出", GlobalVar.epsonEMGOutput);
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输入信号配置", "机器人安全门打开", GlobalVar.epsonSafeDoor);
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输入信号配置", "机器人控制器严重错误 ", GlobalVar.epsonControllerFatalErr);
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输入信号配置", "机器人报警", GlobalVar.epsonAlarm);
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输入信号配置", "Epson通知取完4片Logo信号", GlobalVar.feed4PcsLogoFinished);
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输入信号配置", "Epson通知撕标开始信号", GlobalVar.tearLabelStart);
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输入信号配置", "Epson通知出标开始信号", GlobalVar.newLabelStart);
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输入信号配置", "贴电镀+背胶完成", GlobalVar.labelLogoToPlasticFinished);


        }
        private void btnSaveIODO_Click(object sender, EventArgs e)
        {
            if (JudgeComboBoxIsNullOrSpace(cmbBootEpson)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbProg1Selected)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbProg2Selected)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbProg3Selected)) return;
            if (JudgeComboBoxIsNullOrSpace(cmdStopEpson)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbPausedEpson)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbContinuedEpson)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbResetEpson)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbAllowEpsonFeedLogo)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbAllowEpsonLabelGum)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbAllowEpsonLabelPosMove)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbAllowEpsonLabelLogoToPlastic)) return;
            GlobalVar.bootEpson = cmbBootEpson.SelectedItem.ToString();
            GlobalVar.prog1Selected = cmbProg1Selected.SelectedItem.ToString();
            GlobalVar.prog2Selected = cmbProg2Selected.SelectedItem.ToString();
            GlobalVar.prog3Selected = cmbProg3Selected.SelectedItem.ToString();
            GlobalVar.stopEpson = cmdStopEpson.SelectedItem.ToString();
            GlobalVar.pausedEpson = cmbPausedEpson.SelectedItem.ToString();
            GlobalVar.continuedEpson = cmbContinuedEpson.SelectedItem.ToString();
            GlobalVar.resetEpson = cmbResetEpson.SelectedItem.ToString();
            GlobalVar.allowEpsonFeedLogo = cmbAllowEpsonFeedLogo.SelectedItem.ToString();
            GlobalVar.allowEpsonLabelGum = cmbAllowEpsonLabelGum.SelectedItem.ToString();
            GlobalVar.allowEpsonLabelPosMove = cmbAllowEpsonLabelPosMove.SelectedItem.ToString();
            GlobalVar.allowEpsonLabelLogoToPlastic = cmbAllowEpsonLabelLogoToPlastic.SelectedItem.ToString();
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输出信号配置", "启动机器人程序", GlobalVar.bootEpson);
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输出信号配置", "机器人程序1", GlobalVar.prog1Selected);
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输出信号配置", "机器人程序2", GlobalVar.prog2Selected);
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输出信号配置", "机器人程序3", GlobalVar.prog3Selected);
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输出信号配置", "停止机器人", GlobalVar.stopEpson);
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输出信号配置", "暂停机器人", GlobalVar.pausedEpson);
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输出信号配置", "继续机器人", GlobalVar.continuedEpson);
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输出信号配置", "复位机器人", GlobalVar.resetEpson);
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输出信号配置", "允许epson取Logo", GlobalVar.allowEpsonFeedLogo);
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输出信号配置", "允许epson贴背胶", GlobalVar.allowEpsonLabelGum);
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输出信号配置", "允许epson在出标气缸回退到位后从贴标位置离开", GlobalVar.allowEpsonLabelPosMove);
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输出信号配置", "允许epson贴Logo到塑胶件", GlobalVar.allowEpsonLabelLogoToPlastic);
        }

        private void btnSaveFeedDI_Click(object sender, EventArgs e)
        {
            if (JudgeComboBoxIsNullOrSpace(cmbRotatingCylinderLeftLim)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbRotatingCylinderRightLim)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbLogo4FeedFinished)) return;
            GlobalVar.rotatingCylinderLeftLim = cmbRotatingCylinderLeftLim.SelectedItem.ToString();
            GlobalVar.rotatingCylinderRightLim = cmbRotatingCylinderRightLim.SelectedItem.ToString();
            GlobalVar.logo4FeedFinished = cmbLogo4FeedFinished.SelectedItem.ToString();
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输入信号配置", "旋转气缸左极限", GlobalVar.rotatingCylinderLeftLim);
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输入信号配置", "旋转气缸右极限", GlobalVar.rotatingCylinderRightLim);
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输入信号配置", "电镀件补料完成", GlobalVar.logo4FeedFinished);
        }

        private void btnSaveFeedDO_Click(object sender, EventArgs e)
        {
            if (JudgeComboBoxIsNullOrSpace(cmbRotatingCylinderElecValve)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbFeedFinishedOut)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbFeedStandbyingOut)) return;
            GlobalVar.rotatingCylinderElecValve = cmbRotatingCylinderElecValve.SelectedItem.ToString();
            GlobalVar.feedFinishedOut = cmbFeedFinishedOut.SelectedItem.ToString();
            GlobalVar.feedStandbyingOut = cmbFeedStandbyingOut.SelectedItem.ToString();
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输出信号配置", "旋转气缸电磁阀", GlobalVar.rotatingCylinderElecValve);
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输出信号配置", "补料完成->按钮绿灯亮", GlobalVar.feedFinishedOut);
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输出信号配置", "待补料->按钮红灯亮", GlobalVar.feedStandbyingOut);
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
        private void InitAllPictures()
        {
            foreach (var pic in groupBox10.Controls)
            {
                if (pic is PictureBox)
                {
                    picList.Add((PictureBox)pic);
                }

            }
            foreach (var pic in groupBox7.Controls)
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
        private void InitCfgToComboBox()
        {
            cmbEpsonStandbying.SelectedItem = GlobalVar.epsonStandbying;
            cmbEpsonRunning.SelectedItem = GlobalVar.epsonRunning;
            cmbEpsonPausing.SelectedItem = GlobalVar.epsonPausing;
            cmbEpsonControllerErr.SelectedItem = GlobalVar.epsonControllerErr;
            cmbEpsonEMGOutput.SelectedItem = GlobalVar.epsonEMGOutput;
            cmbEpsonSafeDoor.SelectedItem = GlobalVar.epsonSafeDoor;
            cmbEpsonControllerFatalErr.SelectedItem = GlobalVar.epsonControllerFatalErr;
            cmbEpsonAlarm.SelectedItem = GlobalVar.epsonAlarm;
            cmbFeed4PcsLogoFinished.SelectedItem = GlobalVar.feed4PcsLogoFinished;
            cmbTearLabelStart.SelectedItem = GlobalVar.tearLabelStart;
            cmbNewLabelStart.SelectedItem = GlobalVar.newLabelStart;
            cmbLabelLogoToPlasticFinished.SelectedItem = GlobalVar.labelLogoToPlasticFinished;
            cmbRotatingCylinderLeftLim.SelectedItem = GlobalVar.rotatingCylinderLeftLim;
            cmbRotatingCylinderRightLim.SelectedItem = GlobalVar.rotatingCylinderRightLim;
            cmbLogo4FeedFinished.SelectedItem = GlobalVar.logo4FeedFinished;
            cmbBootEpson.SelectedItem = GlobalVar.bootEpson;
            cmbProg1Selected.SelectedItem = GlobalVar.prog1Selected;
            cmbProg2Selected.SelectedItem = GlobalVar.prog2Selected;
            cmbProg3Selected.SelectedItem = GlobalVar.prog3Selected;
            cmdStopEpson.SelectedItem = GlobalVar.stopEpson;
            cmbPausedEpson.SelectedItem = GlobalVar.pausedEpson;
            cmbContinuedEpson.SelectedItem = GlobalVar.continuedEpson;
            cmbResetEpson.SelectedItem = GlobalVar.resetEpson;
            cmbAllowEpsonFeedLogo.SelectedItem = GlobalVar.allowEpsonFeedLogo;
            cmbAllowEpsonLabelGum.SelectedItem = GlobalVar.allowEpsonLabelGum;
            cmbAllowEpsonLabelPosMove.SelectedItem = GlobalVar.allowEpsonLabelPosMove;
            cmbAllowEpsonLabelLogoToPlastic.SelectedItem = GlobalVar.allowEpsonLabelLogoToPlastic;
            cmbRotatingCylinderElecValve.SelectedItem = GlobalVar.rotatingCylinderElecValve;
            cmbFeedFinishedOut.SelectedItem = GlobalVar.feedFinishedOut;
            cmbFeedStandbyingOut.SelectedItem = GlobalVar.feedStandbyingOut;
        }

        #endregion


    }
}







