using ClassINI;
using csIOC0640;
using csLTDMC;
using KEE.Renault.MyMotion;
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

namespace KEE.Renault.MenuForm
{
    public partial class FrmAxiasSetting : Form
    {
        bool injectHome = false;
        bool feedHome = false;
        bool feedRHome = false;
        bool takeRHome = false;
        List<PictureBox> picList = new List<PictureBox>();
        CancellationTokenSource tokenSource = null;
        bool isTimeOut = true;
        bool isClickRotate = false;
        public FrmAxiasSetting()
        {
            InitializeComponent();
            InitThreeAxiasSpeed();
            InitThreeAxiasHomeParams();
            InitAllPictures();
            InitUIData();
            chkBoxSafeDoorIsClose.Checked = GlobalVar.isCloseSafeDoor;
        }

        #region 窗体事件
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                TraversalAllPic();
                picInjectPLim.Image = GlobalVar.lsFirAxiasSensorDIs[(int.Parse(GlobalVar.feedFromInjectPLimP.Split(',')[1]))].ToString() == "0" ? KEE.Renault.Properties.Resources.RedLed : KEE.Renault.Properties.Resources.GrayLed;
                picInjectNLim.Image = GlobalVar.lsFirAxiasSensorDIs[(int.Parse(GlobalVar.feedFromInjectPLimN.Split(',')[1]))].ToString() == "0" ? KEE.Renault.Properties.Resources.RedLed : KEE.Renault.Properties.Resources.GrayLed;
                picInjectHLim.Image = GlobalVar.lsSecAxiasSensorDIs[(int.Parse(GlobalVar.feedFromInjectPLimHome.Split(',')[1]))].ToString() == "0" ? KEE.Renault.Properties.Resources.RedLed : KEE.Renault.Properties.Resources.GrayLed;
                picFeedPLim.Image = GlobalVar.lsFirAxiasSensorDIs[(int.Parse(GlobalVar.feedDiPLimP.Split(',')[1]))].ToString() == "0" ? KEE.Renault.Properties.Resources.RedLed : KEE.Renault.Properties.Resources.GrayLed;
                picFeedNLim.Image = GlobalVar.lsFirAxiasSensorDIs[(int.Parse(GlobalVar.feedDiNLimP.Split(',')[1]))].ToString() == "0" ? KEE.Renault.Properties.Resources.RedLed : KEE.Renault.Properties.Resources.GrayLed;
                picFeedHLim.Image = GlobalVar.lsSecAxiasSensorDIs[(int.Parse(GlobalVar.feedDiHLimP.Split(',')[1]))].ToString() == "0" ? KEE.Renault.Properties.Resources.RedLed : KEE.Renault.Properties.Resources.GrayLed;
                picFeedRHLim.Image = GlobalVar.lsSecAxiasSensorDIs[(int.Parse(GlobalVar.feedRDiHLimP.Split(',')[1]))].ToString() == "0" ? KEE.Renault.Properties.Resources.RedLed : KEE.Renault.Properties.Resources.GrayLed;
                picTakePLim.Image = GlobalVar.lsFirAxiasSensorDIs[(int.Parse(GlobalVar.takeDiPLimP.Split(',')[1]))].ToString() == "0" ? KEE.Renault.Properties.Resources.RedLed : KEE.Renault.Properties.Resources.GrayLed;
                picTakeNLim.Image = GlobalVar.lsFirAxiasSensorDIs[(int.Parse(GlobalVar.takeDiNLimP.Split(',')[1]))].ToString() == "0" ? KEE.Renault.Properties.Resources.RedLed : KEE.Renault.Properties.Resources.GrayLed;
                picTakeHLim.Image = GlobalVar.lsSecAxiasSensorDIs[(int.Parse(GlobalVar.takeDiHLimP.Split(',')[1]))].ToString() == "0" ? KEE.Renault.Properties.Resources.RedLed : KEE.Renault.Properties.Resources.GrayLed;
                staInjectCurSpeed.Text = LTDMC.dmc_read_current_speed(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber).ToString();
                staFeedCurSpeed.Text = LTDMC.dmc_read_current_speed(GlobalVar.CardId, GlobalVar.FeedAxiasNumber).ToString();
                staFeedRCurSpeed.Text = LTDMC.dmc_read_current_speed(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber).ToString();
                txtBoxInjectFeed.Text = LTDMC.dmc_get_position(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber).ToString();
                txtBoxFeed.Text = LTDMC.dmc_get_position(GlobalVar.CardId, GlobalVar.FeedAxiasNumber).ToString();
                txtBoxFeedR.Text = LTDMC.dmc_get_position(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber).ToString();
                txtBoxTake.Text = LTDMC.dmc_get_position(GlobalVar.CardId, GlobalVar.TakeAxiasNumber).ToString();
                if (LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber) == 0) // 读取指定轴运动状态
                {
                    staInjectFeed.Text = "运行中";
                }
                else
                {
                    staInjectFeed.Text = "停止中";
                    ushort homeRes = 2;
                    if (injectHome)
                    {
                        LTDMC.dmc_get_home_result(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, ref homeRes);
                        if (homeRes == 1)
                        {
                            staInjectFeed.Text = "回零完成";
                            //LTDMC.dmc_set_position(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, 0);
                            //LTDMC.dmc_set_encoder(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, 0);
                        }
                        else if (homeRes == 0)
                        {
                            staInjectFeed.Text = "回零未完成";
                        }
                        injectHome = false;
                    }
                }
                if (LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.FeedAxiasNumber) == 0) // 读取指定轴运动状态
                {
                    staFeed.Text = "运行中";
                }
                else
                {
                    staFeed.Text = "停止中";
                    ushort homeRes = 2;
                    if (feedHome)
                    {
                        LTDMC.dmc_get_home_result(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, ref homeRes);
                        if (homeRes == 1)
                        {
                            staFeed.Text = "回零完成";
                            //LTDMC.dmc_set_position(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, 0);
                            //LTDMC.dmc_set_encoder(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, 0);
                        }
                        else if (homeRes == 0)
                        {
                            staFeed.Text = "回零未完成";
                        }
                        feedHome = false;
                    }
                }
                if (LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber) == 0) // 读取指定轴运动状态
                {
                    staFeedR.Text = "运行中";
                }
                else
                {
                    staFeedR.Text = "停止中";
                    ushort homeRes = 2;
                    if (feedRHome)
                    {
                        LTDMC.dmc_get_home_result(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, ref homeRes);
                        if (homeRes == 1)
                        {
                            staFeedR.Text = "回零完成";
                            //LTDMC.dmc_set_position(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, 0);
                            //LTDMC.dmc_set_encoder(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, 0);
                        }
                        else if (homeRes == 0)
                        {
                            staFeedR.Text = "回零未完成";
                        }
                        feedRHome = false;
                    }
                }
                Application.DoEvents();
            }));
        }

        private void FrmAxiasSetting_Enter(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void FrmAxiasSetting_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            //恢复各轴自动运行速度
            LTDMC.dmc_set_profile(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, GlobalVar.injectFeedStartSpeed, GlobalVar.injectFeedMotionSpeed, GlobalVar.injectFeedAccTime, GlobalVar.injectFeedDccTime, GlobalVar.injectFeedStopSpeed);//设置速度参数
            LTDMC.dmc_set_s_profile(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, 0, GlobalVar.injectFeedSTime);//设置S段速度参数
            LTDMC.dmc_set_profile(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, GlobalVar.feedStartSpeed, GlobalVar.feedMotionSpeed, GlobalVar.feedAccTime, GlobalVar.feedDccTime, GlobalVar.feedStopSpeed);//设置速度参数
            LTDMC.dmc_set_s_profile(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, 0, GlobalVar.feedSTime);//设置S段速度参数
            LTDMC.dmc_set_profile(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, GlobalVar.feedRStartSpeed, GlobalVar.feedRMotionSpeed, GlobalVar.feedRAccTime, GlobalVar.feedRDccTime, GlobalVar.feedRStopSpeed);//设置速度参数
            LTDMC.dmc_set_s_profile(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, 0, GlobalVar.feedRSTime);//设置S段速度参数
            LTDMC.dmc_set_profile(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, GlobalVar.takeStartSpeed, GlobalVar.takeMotionSpeed, GlobalVar.takeAccTime, GlobalVar.takeDccTime, GlobalVar.takeStopSpeed);//设置速度参数
            LTDMC.dmc_set_s_profile(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, 0, GlobalVar.takeSTime);//设置S段速度参数
            LTDMC.dmc_set_profile(GlobalVar.CardId, GlobalVar.LabelingAxiasNumber, GlobalVar.labelStartSpeed, GlobalVar.labelMotionSpeed, GlobalVar.labelAccTime, GlobalVar.labelDccTime, GlobalVar.labelStopSpeed);//设置速度参数
            LTDMC.dmc_set_s_profile(GlobalVar.CardId, GlobalVar.LabelingAxiasNumber, 0, GlobalVar.labelSTime);//设置S段速度参数
        }

        private void btnCylinderElecValve_Click_1(object sender, EventArgs e)
        {
            if (!GlobalVar.lsAxiasDIs[44].CurIOStatus)
            {
                MessageBox.Show("急停信号按下，禁止操作");
            }
            if (!GlobalVar.totalRunFlag)
            {
                if (LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber) == 1 && LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.FeedAxiasNumber) == 1 && LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber) == 1)
                {
                    ushort ioIndex = Convert.ToUInt16(((Button)sender).Tag.ToString().Substring(2));
                    IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[ioIndex].Card - 1), GlobalVar.lsAxiasDOs[ioIndex].PinDefinition, GlobalVar.lsAxiasDOs[ioIndex].CurIOStatus ? (ushort)1 : (ushort)0);
                }
                else
                {
                    MessageBox.Show("轴正在运动中，禁止操作");
                }
            }
            else
            {
                MessageBox.Show("设备正在全自动模式，禁止操作");
            }
        }

        private void btnVacSorb_Click(object sender, EventArgs e)
        {
            if (!GlobalVar.lsAxiasDIs[44].CurIOStatus)
            {
                MessageBox.Show("急停信号按下，禁止操作");
            }
            ushort ioIndex = Convert.ToUInt16(((Button)sender).Tag.ToString().Substring(2));
            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[ioIndex].Card - 1), GlobalVar.lsAxiasDOs[ioIndex].PinDefinition, GlobalVar.lsAxiasDOs[ioIndex].CurIOStatus ? (ushort)1 : (ushort)0);
        }

        private void btn_MouseUp(object sender, MouseEventArgs e)
        {
            switch ((sender as Button).Tag.ToString().Split(',')[0])
            {
                case "1":
                    {
                        LTDMC.dmc_stop(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, Convert.ToUInt16(GlobalVar.injectFeedStopModel));
                    }
                    break;
                case "2":
                    {
                        LTDMC.dmc_stop(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, Convert.ToUInt16(GlobalVar.feedStopModel));
                    }
                    break;
                case "3":
                    {
                        LTDMC.dmc_stop(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, Convert.ToUInt16(GlobalVar.feedRStopModel));
                    }
                    break;
                case "4":
                    {
                        LTDMC.dmc_stop(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, Convert.ToUInt16(GlobalVar.takeStopModel));
                    }
                    break;
            }
        }

        private void btn_MouseDown(object sender, MouseEventArgs e)
        {
            if (!GlobalVar.lsAxiasDIs[44].CurIOStatus)
            {
                MessageBox.Show("急停信号按下，禁止操作");
            }
            if (!GlobalVar.totalRunFlag)
            {
                if (GlobalVar.lsAxiasDIs[14].CurIOStatus && !GlobalVar.lsAxiasDIs[15].CurIOStatus && GlobalVar.lsAxiasDIs[24].CurIOStatus && !GlobalVar.lsAxiasDIs[25].CurIOStatus)
                {
                    string[] arr = (sender as Button).Tag.ToString().Split(',');
                    switch (arr[0])
                    {
                        case "1":
                            {
                                LTDMC.dmc_vmove(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, (arr[1] == "1") ? (ushort)1 : (ushort)0);
                            }
                            break;
                        case "2":
                            {
                                LTDMC.dmc_vmove(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, (arr[1] == "1") ? (ushort)1 : (ushort)0);
                            }
                            break;
                        case "3":
                            {
                                LTDMC.dmc_vmove(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, (arr[1] == "1") ? (ushort)1 : (ushort)0);
                            }
                            break;
                        case "4":
                            {
                                LTDMC.dmc_vmove(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, (arr[1] == "1") ? (ushort)1 : (ushort)0);
                            }
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("气缸处于下降状态，禁止移动");
                }
            }
            else
            {
                MessageBox.Show("设备正在全自动模式，禁止操作");
            }
            
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            if (!GlobalVar.lsAxiasDIs[44].CurIOStatus)
            {
                MessageBox.Show("急停信号按下，禁止操作");
            }
            if (!GlobalVar.totalRunFlag)
            {
                if (GlobalVar.lsAxiasDIs[14].CurIOStatus && !GlobalVar.lsAxiasDIs[15].CurIOStatus && GlobalVar.lsAxiasDIs[24].CurIOStatus && !GlobalVar.lsAxiasDIs[25].CurIOStatus)
                {
                    string tag = (sender as Button).Tag.ToString();

                    switch (tag)
                    {
                        case "A":
                            {
                                LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, -30000, (ushort)1);
                                while (LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber) == 0)
                                {
                                    Application.DoEvents();
                                }
                                LTDMC.dmc_set_homemode(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, 1, (double)(HomeSpeedModel)GlobalVar.injectFeedHomeSpeedModel, (ushort)(HomeModel)GlobalVar.injectFeedHomeModel, 0);
                                LTDMC.dmc_set_home_position(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, 2, 0);
                                LTDMC.dmc_home_move(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber); injectHome = true;
                            }
                            break;
                        case "B":
                            {
                                LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, -30000, (ushort)1);
                                while (LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.FeedAxiasNumber) == 0)
                                {
                                    Application.DoEvents();
                                }
                                LTDMC.dmc_set_homemode(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, 1, (double)(HomeSpeedModel)GlobalVar.feedHomeSpeedModel, (ushort)(HomeModel)GlobalVar.feedHomeModel, 0);
                                LTDMC.dmc_set_home_position(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, 2, 0);
                                LTDMC.dmc_home_move(GlobalVar.CardId, GlobalVar.FeedAxiasNumber); feedHome = true;
                            }
                            break;
                        case "C":
                            {
                                if (LTDMC.dmc_get_position(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber) > 0)
                                {
                                    LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, -4000, (ushort)1);
                                    while (LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber) == 0)
                                    {
                                        Application.DoEvents();
                                    }
                                }
                                LTDMC.dmc_set_homemode(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, (ushort)1, (double)(HomeSpeedModel)GlobalVar.feedRHomeSpeedModel, (ushort)(HomeModel)GlobalVar.feedRHomeModel, 0);
                                LTDMC.dmc_set_home_position(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, 2, 0);
                                LTDMC.dmc_home_move(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber); feedRHome = true;
                            }
                            break;
                        case "D":
                            {
                                LTDMC.dmc_set_homemode(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, 0, (double)(HomeSpeedModel)GlobalVar.takeHomeSpeedModel, (ushort)(HomeModel)GlobalVar.takeHomeModel, 0);
                                LTDMC.dmc_set_home_position(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, 2, 0);
                                LTDMC.dmc_home_move(GlobalVar.CardId, GlobalVar.TakeAxiasNumber); takeRHome = true;
                            }
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("气缸处于下降状态，禁止移动");
                }
            }
            else
            {
                MessageBox.Show("设备正在全自动模式，禁止操作");
            }

        }

        private void btnWritePluesValueToINI_Click(object sender, EventArgs e)
        {
            if (!GlobalVar.lsAxiasDIs[44].CurIOStatus)
            {
                MessageBox.Show("急停信号按下，禁止操作");
            }
            if (!GlobalVar.totalRunFlag)
            {
                if (MessageBox.Show("确定是否写入？","提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information)== DialogResult.Yes)
                {
                    string tag = (sender as Button).Tag.ToString();
                    switch (tag)
                    {
                        case "W1":
                            {
                                txtBoxInjectTakePro.Text = txtBoxInjectFeed.Text;
                                GlobalVar.injectTakeProPos = Convert.ToInt32(txtBoxInjectFeed.Text);
                                INI.INIWriteValue(GlobalVar.bConfigFilePath, "来自有信机械手上料轴参数配置", "放料位置", GlobalVar.injectTakeProPos.ToString());
                            }
                            break;
                        case "W2":
                            {
                                txtBoxInjectPro1.Text = txtBoxInjectFeed.Text;
                                GlobalVar.injectFeedProPos1 = Convert.ToInt32(txtBoxInjectFeed.Text);
                                INI.INIWriteValue(GlobalVar.bConfigFilePath, "来自有信机械手上料轴参数配置", "取料位置1", GlobalVar.injectFeedProPos1.ToString());
                                txtBoxFeedPro1.Text = txtBoxFeed.Text;
                                GlobalVar.feedProPos1 = Convert.ToInt32(txtBoxFeed.Text);
                                INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡上料轴参数配置", "取料位置1", GlobalVar.feedProPos1.ToString());
                                txtBoxFeedRPro1.Text = txtBoxFeedR.Text;
                                GlobalVar.feedRProPos1 = Convert.ToInt32(txtBoxFeedR.Text);
                                INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡上料R轴参数配置", "取料位置1", GlobalVar.feedRProPos1.ToString());
                            }
                            break;
                        case "W3":
                            {
                                txtBoxInjectPro2.Text = txtBoxInjectFeed.Text;
                                GlobalVar.injectFeedProPos2 = Convert.ToInt32(txtBoxInjectFeed.Text);
                                INI.INIWriteValue(GlobalVar.bConfigFilePath, "来自有信机械手上料轴参数配置", "取料位置2", GlobalVar.injectFeedProPos2.ToString());
                                txtBoxFeedPro2.Text = txtBoxFeed.Text;
                                GlobalVar.feedProPos2 = Convert.ToInt32(txtBoxFeed.Text);
                                INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡上料轴参数配置", "取料位置2", GlobalVar.feedProPos2.ToString());
                                txtBoxFeedRPro2.Text = txtBoxFeedR.Text;
                                GlobalVar.feedRProPos2 = Convert.ToInt32(txtBoxFeedR.Text);
                                INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡上料R轴参数配置", "取料位置2", GlobalVar.feedRProPos2.ToString());
                            }
                            break;
                        case "W4":
                            {
                                txtBoxInjectPro3.Text = txtBoxInjectFeed.Text;
                                GlobalVar.injectFeedProPos3 = Convert.ToInt32(txtBoxInjectFeed.Text);
                                INI.INIWriteValue(GlobalVar.bConfigFilePath, "来自有信机械手上料轴参数配置", "取料位置3", GlobalVar.injectFeedProPos3.ToString());
                                txtBoxFeedPro3.Text = txtBoxFeed.Text;
                                GlobalVar.feedProPos3 = Convert.ToInt32(txtBoxFeed.Text);
                                INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡上料轴参数配置", "取料位置3", GlobalVar.feedProPos3.ToString());
                                txtBoxFeedRPro3.Text = txtBoxFeedR.Text;
                                GlobalVar.feedRProPos3 = Convert.ToInt32(txtBoxFeedR.Text);
                                INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡上料R轴参数配置", "取料位置3", GlobalVar.feedRProPos3.ToString());
                            }
                            break;
                        case "W5":
                            {
                                txtBoxInjectPro4.Text = txtBoxInjectFeed.Text;
                                GlobalVar.injectFeedProPos4 = Convert.ToInt32(txtBoxInjectFeed.Text);
                                INI.INIWriteValue(GlobalVar.bConfigFilePath, "来自有信机械手上料轴参数配置", "取料位置4", GlobalVar.injectFeedProPos4.ToString());
                                txtBoxFeedPro4.Text = txtBoxFeed.Text;
                                GlobalVar.feedProPos4 = Convert.ToInt32(txtBoxFeed.Text);
                                INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡上料轴参数配置", "取料位置4", GlobalVar.feedProPos4.ToString());
                                txtBoxFeedRPro4.Text = txtBoxFeedR.Text;
                                GlobalVar.feedRProPos4 = Convert.ToInt32(txtBoxFeedR.Text);
                                INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡上料R轴参数配置", "取料位置4", GlobalVar.feedRProPos4.ToString());
                            }
                            break;
                        case "W6":
                            {
                                txtBoxFeedTakePro.Text = txtBoxFeed.Text;
                                GlobalVar.feedTakeProPos = Convert.ToInt32(txtBoxFeed.Text);
                                INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡上料轴参数配置", "放料位置", GlobalVar.feedTakeProPos.ToString());
                                txtBoxFeedRTakePro.Text = txtBoxFeedR.Text;
                                GlobalVar.feedRTakeProPos = Convert.ToInt32(txtBoxFeedR.Text);
                                INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡上料R轴参数配置", "放料位置", GlobalVar.feedRTakeProPos.ToString());
                            }
                            break;
                        case "W7":
                            {
                                txtBoxTakeAxiasFeedPos.Text = txtBoxTake.Text;
                                GlobalVar.takeFeedProPos = Convert.ToInt32(txtBoxTake.Text);
                                INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡丢料轴参数配置", "取料位置", GlobalVar.takeFeedProPos.ToString());
                            }
                            break;
                        case "W8":
                            {
                                txtBoxTakeAxiasNGPos.Text = txtBoxTake.Text;
                                GlobalVar.takeNGProPos = Convert.ToInt32(txtBoxTake.Text);
                                INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡丢料轴参数配置", "放料NG位置", GlobalVar.takeNGProPos.ToString());
                            }
                            break;
                        case "W9":
                            {
                                txtBoxTakeAxiasOKPos.Text = txtBoxTake.Text;
                                GlobalVar.takeOKProPos = Convert.ToInt32(txtBoxTake.Text);
                                INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡丢料轴参数配置", "放料OK位置", GlobalVar.takeOKProPos.ToString());
                            }
                            break;
                    }
                }
            }
            else
            {
                MessageBox.Show("设备正在全自动模式，禁止操作");
            }
        }

        private void btnMoveToPos_Click(object sender, EventArgs e)
        {
            if (!GlobalVar.lsAxiasDIs[44].CurIOStatus)
            {
                MessageBox.Show("急停信号按下，禁止操作");
            }
            if (!GlobalVar.totalRunFlag)
            {
                if (GlobalVar.lsAxiasDIs[14].CurIOStatus && !GlobalVar.lsAxiasDIs[15].CurIOStatus && GlobalVar.lsAxiasDIs[24].CurIOStatus && !GlobalVar.lsAxiasDIs[25].CurIOStatus)
                {
                    string tag = (sender as Button).Tag.ToString();
                    switch (tag)
                    {
                        case "M1":
                            {
                                LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, GlobalVar.injectTakeProPos, (ushort)CoordModelEnum.绝对坐标);
                            }
                            break;
                        case "M2":
                            {
                                LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, GlobalVar.injectFeedProPos1, (ushort)CoordModelEnum.绝对坐标);
                                LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, GlobalVar.feedProPos1, (ushort)CoordModelEnum.绝对坐标);
                                LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, GlobalVar.feedRProPos1, (ushort)CoordModelEnum.绝对坐标);
                            }
                            break;
                        case "M3":
                            {
                                LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, GlobalVar.injectFeedProPos2, (ushort)CoordModelEnum.绝对坐标);
                                LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, GlobalVar.feedProPos2, (ushort)CoordModelEnum.绝对坐标);
                                LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, GlobalVar.feedRProPos2, (ushort)CoordModelEnum.绝对坐标);
                            }
                            break;
                        case "M4":
                            {
                                LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, GlobalVar.injectFeedProPos3, (ushort)CoordModelEnum.绝对坐标);
                                LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, GlobalVar.feedProPos3, (ushort)CoordModelEnum.绝对坐标);
                                LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, GlobalVar.feedRProPos3, (ushort)CoordModelEnum.绝对坐标);
                            }
                            break;
                        case "M5":
                            {
                                LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, GlobalVar.injectFeedProPos4, (ushort)CoordModelEnum.绝对坐标);
                                LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, GlobalVar.feedProPos4, (ushort)CoordModelEnum.绝对坐标);
                                LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, GlobalVar.feedRProPos4, (ushort)CoordModelEnum.绝对坐标);
                            }
                            break;
                        case "M6":
                            {
                                LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, GlobalVar.feedTakeProPos, (ushort)CoordModelEnum.绝对坐标);
                                LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, GlobalVar.feedRTakeProPos, (ushort)CoordModelEnum.绝对坐标);
                            }
                            break;
                        case "M7":
                            {
                                LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, GlobalVar.takeFeedProPos, (ushort)CoordModelEnum.绝对坐标);
                            }
                            break;
                        case "M8":
                            {
                                LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, GlobalVar.takeNGProPos, (ushort)CoordModelEnum.绝对坐标);
                            }
                            break;
                        case "M9":
                            {
                                LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, GlobalVar.takeOKProPos, (ushort)CoordModelEnum.绝对坐标);
                            }
                            break;
                    }
                }
                else
                { MessageBox.Show("请退回气缸再点击移动"); }
            }
            else
            { MessageBox.Show("设备正在全自动模式，禁止操作"); }
        }

        private void btnMoveSelectedAxias_Click(object sender, EventArgs e)
        {
            if (!GlobalVar.totalRunFlag)
            {
                if (GlobalVar.lsAxiasDIs[14].CurIOStatus && !GlobalVar.lsAxiasDIs[15].CurIOStatus && GlobalVar.lsAxiasDIs[24].CurIOStatus && !GlobalVar.lsAxiasDIs[25].CurIOStatus)
                {
                    if (JudgeTextBoxIsNullOrSpace(txtBoxCurSelectedAxiasV)) return;
                    int disP = 0;
                    if (!int.TryParse(txtBoxCurSelectedAxiasV.Text, out disP)) { MessageBox.Show("请输入有效值"); }

                    ushort curAxiasNumber = GlobalVar.InjectFeedAxiasNumber;
                    if (rBtnInject.Checked)
                    {
                        curAxiasNumber = GlobalVar.InjectFeedAxiasNumber;
                    }
                    if (rBtnFeed.Checked)
                    {
                        curAxiasNumber = GlobalVar.FeedAxiasNumber;
                    }
                    if (rBtnFeedR.Checked)
                    {
                        curAxiasNumber = GlobalVar.FeedRAxiasNumber;
                    }
                    LTDMC.dmc_pmove(GlobalVar.CardId, curAxiasNumber, disP, (ushort)CoordModelEnum.绝对坐标);
                }
                else
                {
                    MessageBox.Show("轴正在运动中，禁止操作");
                }
            }
            else
            { MessageBox.Show("设备正在全自动模式，禁止操作"); }
        }
        #endregion 

        #region 初始化各轴速度
        private void InitThreeAxiasSpeed()
        {
            double injectSpeed;
            double feedSpeed;
            double feedRSpeed;
            double takeSpeed;
            double labelSpeed;
            if (!double.TryParse(txtboxInjectSpeed.Text, out injectSpeed)) { MessageBox.Show("请输入有效值"); }
            if (!double.TryParse(txtboxFeedSpeed.Text, out feedSpeed)) { MessageBox.Show("请输入有效值"); }
            if (!double.TryParse(txtboxFeedRSpeed.Text, out feedRSpeed)) { MessageBox.Show("请输入有效值"); }
            if (!double.TryParse(txtboxTakeSpeed.Text, out takeSpeed)) { MessageBox.Show("请输入有效值"); }
            if (!double.TryParse(txtboxLabelSpeed.Text, out labelSpeed)) { MessageBox.Show("请输入有效值"); }
            LTDMC.dmc_set_profile(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, GlobalVar.injectFeedStartSpeed, injectSpeed, GlobalVar.injectFeedAccTime, GlobalVar.injectFeedDccTime, GlobalVar.injectFeedStopSpeed);//设置速度参数
            LTDMC.dmc_set_s_profile(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, 0, GlobalVar.injectFeedSTime);//设置S段速度参数
            LTDMC.dmc_set_profile(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, GlobalVar.feedStartSpeed, feedSpeed, GlobalVar.feedAccTime, GlobalVar.feedDccTime, GlobalVar.feedStopSpeed);//设置速度参数
            LTDMC.dmc_set_s_profile(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, 0, GlobalVar.feedSTime);//设置S段速度参数
            LTDMC.dmc_set_profile(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, GlobalVar.feedRStartSpeed, feedRSpeed, GlobalVar.feedRAccTime, GlobalVar.feedRDccTime, GlobalVar.feedRStopSpeed);//设置速度参数
            LTDMC.dmc_set_s_profile(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, 0, GlobalVar.feedRSTime);//设置S段速度参数
            LTDMC.dmc_set_profile(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, GlobalVar.takeStartSpeed, takeSpeed, GlobalVar.takeAccTime, GlobalVar.takeDccTime, GlobalVar.takeStopSpeed);//设置速度参数
            LTDMC.dmc_set_s_profile(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, 0, GlobalVar.takeSTime);//
            LTDMC.dmc_set_profile(GlobalVar.CardId, GlobalVar.LabelingAxiasNumber, GlobalVar.labelStartSpeed, labelSpeed, GlobalVar.labelAccTime, GlobalVar.labelDccTime, GlobalVar.labelStopSpeed);//设置速度参数
            LTDMC.dmc_set_s_profile(GlobalVar.CardId, GlobalVar.LabelingAxiasNumber, 0, GlobalVar.labelSTime);//设置S段速度参数
        }
        private void InitThreeAxiasHomeParams()
        {
            LTDMC.dmc_set_homemode(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, 1, (double)(HomeSpeedModel)GlobalVar.injectFeedHomeSpeedModel, (ushort)(HomeModel)GlobalVar.injectFeedHomeModel, 0);
            LTDMC.dmc_set_home_position(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, 2, 0);
            LTDMC.dmc_set_homemode(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, 1, (double)(HomeSpeedModel)GlobalVar.feedHomeSpeedModel, (ushort)(HomeModel)GlobalVar.feedHomeModel, 0);
            LTDMC.dmc_set_home_position(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, 2, 0);
            LTDMC.dmc_set_homemode(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, 1, (double)(HomeSpeedModel)GlobalVar.feedRHomeSpeedModel, (ushort)(HomeModel)GlobalVar.feedRHomeModel, 0);
            LTDMC.dmc_set_home_position(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, 2, 0);
            LTDMC.dmc_set_homemode(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, 1, (double)(HomeSpeedModel)GlobalVar.takeHomeSpeedModel, (ushort)(HomeModel)GlobalVar.takeHomeModel, 0);
            LTDMC.dmc_set_home_position(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, 2, 0);
        }
        #endregion 

        #region 其他方法
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
        private void InitAllPictures()
        {
            foreach (var pic in groupBox6.Controls)
            {
                if (pic is PictureBox)
                {
                    picList.Add((PictureBox)pic);
                }
            }
            foreach (var pic in groupBox25.Controls)
            {
                if (pic is PictureBox)
                {
                    picList.Add((PictureBox)pic);
                }

            }
            foreach (var pic in groupBox27.Controls)
            {
                if (pic is PictureBox)
                {
                    picList.Add((PictureBox)pic);
                }

            }
            foreach (var pic in groupBox8.Controls)
            {
                if (pic is PictureBox)
                {
                    picList.Add((PictureBox)pic);
                }

            }
            foreach (var pic in groupBox20.Controls)
            {
                if (pic is PictureBox)
                {
                    picList.Add((PictureBox)pic);
                }

            }
            foreach (var pic in groupBox21.Controls)
            {
                if (pic is PictureBox)
                {
                    picList.Add((PictureBox)pic);
                }

            }
            foreach (var pic in groupBox22.Controls)
            {
                if (pic is PictureBox)
                {
                    picList.Add((PictureBox)pic);
                }

            }
            foreach (var pic in groupBox24.Controls)
            {
                if (pic is PictureBox)
                {
                    picList.Add((PictureBox)pic);
                }

            }
        }
        private void InitUIData()
        {
            txtBoxInjectTakePro.Text = GlobalVar.injectTakeProPos.ToString();
            txtBoxInjectPro1.Text = GlobalVar.injectFeedProPos1.ToString();
            txtBoxInjectPro2.Text = GlobalVar.injectFeedProPos1.ToString();
            txtBoxInjectPro3.Text = GlobalVar.injectFeedProPos2.ToString();
            txtBoxInjectPro4.Text = GlobalVar.injectFeedProPos2.ToString();
            txtBoxFeedPro1.Text = GlobalVar.feedProPos1.ToString();
            txtBoxFeedPro2.Text = GlobalVar.feedProPos2.ToString();
            txtBoxFeedPro3.Text = GlobalVar.feedProPos3.ToString();
            txtBoxFeedPro4.Text = GlobalVar.feedProPos4.ToString();
            txtBoxFeedRPro1.Text = GlobalVar.feedRProPos1.ToString();
            txtBoxFeedRPro2.Text = GlobalVar.feedRProPos2.ToString();
            txtBoxFeedRPro3.Text = GlobalVar.feedRProPos3.ToString();
            txtBoxFeedRPro4.Text = GlobalVar.feedRProPos4.ToString();
            txtBoxFeedTakePro.Text = GlobalVar.feedTakeProPos.ToString();
            txtBoxFeedRTakePro.Text = GlobalVar.feedRTakeProPos.ToString();
            txtBoxTakeAxiasFeedPos.Text = GlobalVar.takeFeedProPos.ToString();
            txtBoxTakeAxiasNGPos.Text = GlobalVar.takeNGProPos.ToString();
            txtBoxTakeAxiasOKPos.Text = GlobalVar.takeOKProPos.ToString();
            tbVacuumDelayTime.Text = GlobalVar.feedVaccDelayTime.ToString();
            txtBoxContinueLabelPluse.Text = GlobalVar.labelContinuePlues.ToString();
            chkBoxIsLabelContinue.Checked = GlobalVar.labelIsContinue;
        }

        #endregion

        private void chkBoxSafeDoorIsClose_CheckedChanged(object sender, EventArgs e)
        {
            GlobalVar.isCloseSafeDoor = chkBoxSafeDoorIsClose.Checked;
        }

        private void btnChangeAxiasSpeed_Click(object sender, EventArgs e)
        {
            if (!GlobalVar.lsAxiasDIs[44].CurIOStatus)
            {
                MessageBox.Show("急停信号按下，禁止操作");
            }
            InitThreeAxiasSpeed();
        }

        private void btnCylinderTakeElecValve_Click(object sender, EventArgs e)
        {
            if (!GlobalVar.lsAxiasDIs[44].CurIOStatus)
            {
                MessageBox.Show("急停信号按下，禁止操作");
            }
            if (!GlobalVar.totalRunFlag)
            {
                if (LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.TakeAxiasNumber) == 1)
                {
                    ushort ioIndex = Convert.ToUInt16(((Button)sender).Tag.ToString().Substring(2));
                    IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[ioIndex].Card - 1), GlobalVar.lsAxiasDOs[ioIndex].PinDefinition, GlobalVar.lsAxiasDOs[ioIndex].CurIOStatus ? (ushort)1 : (ushort)0);
                }
                else
                {
                    MessageBox.Show("轴正在运动中，禁止操作");
                }
            }
            else
            {
                MessageBox.Show("设备正在全自动模式，禁止操作");
            }
        }

        private void btnTakeVacSorb_Click(object sender, EventArgs e)
        {
            if (!GlobalVar.lsAxiasDIs[44].CurIOStatus)
            {
                MessageBox.Show("急停信号按下，禁止操作");
            }
            ushort ioIndex = Convert.ToUInt16(((Button)sender).Tag.ToString().Substring(2));
            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[ioIndex].Card - 1), GlobalVar.lsAxiasDOs[ioIndex].PinDefinition, GlobalVar.lsAxiasDOs[ioIndex].CurIOStatus ? (ushort)1 : (ushort)0);
        }

        private void btnLabelPressCylinder_Click(object sender, EventArgs e)
        {
            if (!GlobalVar.lsAxiasDIs[44].CurIOStatus)
            {
                MessageBox.Show("急停信号按下，禁止操作");
            }
            if (!GlobalVar.totalRunFlag)
            {
                if (LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.LabelingAxiasNumber) == 1)
                {
                    ushort ioIndex = Convert.ToUInt16(((Button)sender).Tag.ToString().Substring(2));
                    IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[ioIndex].Card - 1), GlobalVar.lsAxiasDOs[ioIndex].PinDefinition, GlobalVar.lsAxiasDOs[ioIndex].CurIOStatus ? (ushort)1 : (ushort)0);
                }
                else
                {
                    MessageBox.Show("轴正在运动中，禁止操作"); 
                }
            }
            else
            {
                MessageBox.Show("设备正在全自动模式，禁止操作");
            }
        }

        private void btnLabelOutCylinder_Click(object sender, EventArgs e)
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

        private void btnFixLenMotion_MouseDown(object sender, MouseEventArgs e)
        {
            if (!GlobalVar.totalRunFlag)
            {
                if (GlobalVar.lsAxiasDIs[16].CurIOStatus && !GlobalVar.lsAxiasDIs[17].CurIOStatus)
                {
                    LTDMC.dmc_vmove(GlobalVar.CardId, GlobalVar.LabelingAxiasNumber, 0);
                }
                else
                {
                    MessageBox.Show("气缸处于下降状态，禁止移动");
                }
            }
            else
            {
                MessageBox.Show("设备正在全自动模式，禁止操作");
            }
            
        }

        private void btnFixLenMotion_MouseUp(object sender, MouseEventArgs e)
        {
            LTDMC.dmc_stop(GlobalVar.CardId, GlobalVar.LabelingAxiasNumber, Convert.ToUInt16(GlobalVar.labelStopModel));
        }

        private void btnResetRobot_Click(object sender, EventArgs e)
        {
            if (!GlobalVar.lsAxiasDIs[44].CurIOStatus)
            {
                MessageBox.Show("急停信号按下，禁止操作");
            }
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

        private void chkBoxIsPrintNGBarcode_CheckedChanged(object sender, EventArgs e)
        {
            GlobalVar.isPrintNGBarcode = chkBoxIsPrintNGBarcode.Checked;
        }

        private void btnRotateCam_Click(object sender, EventArgs e)
        {
            if (!GlobalVar.totalRunFlag)
            {
                if (GlobalVar.lsAxiasDIs[20].CurIOStatus && !GlobalVar.lsAxiasDIs[21].CurIOStatus && GlobalVar.lsAxiasDIs[22].CurIOStatus && !GlobalVar.lsAxiasDIs[23].CurIOStatus && GlobalVar.lsAxiasDIs[14].CurIOStatus && !GlobalVar.lsAxiasDIs[15].CurIOStatus && GlobalVar.lsAxiasDIs[24].CurIOStatus && !GlobalVar.lsAxiasDIs[25].CurIOStatus && GlobalVar.lsAxiasDIs[26].CurIOStatus && !GlobalVar.lsAxiasDIs[27].CurIOStatus)
                {
                    if (!isClickRotate)
                    {
                        isClickRotate = true;
                        tokenSource = new CancellationTokenSource(3000);
                        tokenSource.Token.Register(new Action(() => { isClickRotate = false; DialogResult = isTimeOut ? MessageBox.Show("轮盘正向转动超时") : MessageBox.Show("轮盘正向转动到位"); }));
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

        private void btnSaveVaccDelayTime_Click(object sender, EventArgs e)
        {
            if (JudgeTextBoxIsNullOrSpace(tbVacuumDelayTime))
            {
                return;
            }
            int a = 100;
            if (!int.TryParse(tbVacuumDelayTime.Text.Trim(), out a)) { MessageBox.Show("请输入有效数值"); return; }
            GlobalVar.feedVaccDelayTime = a;
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "System", "上料真空建立时间", tbVacuumDelayTime.Text);
        }

        private void btnSaveLabelPluse_Click(object sender, EventArgs e)
        {
            if (JudgeTextBoxIsNullOrSpace(txtBoxContinueLabelPluse))
            {
                return;
            }
            int a = 100;
            if (!int.TryParse(txtBoxContinueLabelPluse.Text.Trim(), out a)) { MessageBox.Show("请输入有效数值"); return; }
            GlobalVar.labelContinuePlues = a;
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡出标轴参数配置", "气缸下压后继续走的脉冲", txtBoxContinueLabelPluse.Text);
        }

        private void chkBoxIsLabelContinue_CheckedChanged(object sender, EventArgs e)
        {
            GlobalVar.labelIsContinue = chkBoxIsLabelContinue.Checked;
            ClassINI.INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡出标轴参数配置", "是否开启出标完继续出标固定脉冲", GlobalVar.labelIsContinue ? "true" : "false");
        }
    }
}
