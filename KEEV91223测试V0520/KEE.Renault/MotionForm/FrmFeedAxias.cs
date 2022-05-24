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

namespace KEE.Renault.MotionForm
{
    public partial class FrmFeedAxias : Form
    {
        public FrmFeedAxias()
        {
            InitializeComponent();
            cmbCoordModel.DataSource = LSParamCommonInit.coordModels;
            cmbStopModel.DataSource = LSParamCommonInit.stopModels;
            cmbHomeModel.DataSource = LSParamCommonInit.homeModels;
            cmbHomeSpeedModel.DataSource = LSParamCommonInit.homeSpeeds;
            InitAllPictures();
            InitCfgUIData();
        }

        #region 变量
        List<PictureBox> picList = new List<PictureBox>();
        CancellationTokenSource tokenSource = null;
        bool isTimeOut = true;
        #endregion 

        #region 窗体事件
        private void btnSaveDiVal_Click(object sender, EventArgs e)
        {
            if (JudgeComboBoxIsNullOrSpace(cmbPLimP)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbNLimP)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbHomeP)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbFeedCylinderHighLim)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbFeedCylinderLowLim)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbFeedVacuumLim)) return;
            GlobalVar.feedDiPLimP = cmbPLimP.SelectedItem.ToString();
            GlobalVar.feedDiNLimP = cmbNLimP.SelectedItem.ToString();
            GlobalVar.feedDiHLimP = cmbHomeP.SelectedItem.ToString();
            GlobalVar.feedCylinderHighLim = cmbFeedCylinderHighLim.SelectedItem.ToString();
            GlobalVar.feedCylinderLowLim = cmbFeedCylinderLowLim.SelectedItem.ToString();
            GlobalVar.feedVacuumLim = cmbFeedVacuumLim.SelectedItem.ToString();
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "轴卡输入信号配置", "取料轴正极限", GlobalVar.feedDiPLimP.ToString());
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "轴卡输入信号配置", "取料轴负极限", GlobalVar.feedDiNLimP.ToString());
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "轴卡输入信号配置", "取料轴原点", GlobalVar.feedDiHLimP.ToString());
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输入信号配置", "上料气缸上极限", GlobalVar.feedCylinderHighLim.ToString());
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输入信号配置", "上料气缸下极限", GlobalVar.feedCylinderLowLim.ToString());
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输入信号配置", "上料真空负压表阈值", GlobalVar.feedVacuumLim.ToString());
        }

        private void btnSaveModel_Click(object sender, EventArgs e)
        {
            if (JudgeComboBoxIsNullOrSpace(cmbStopModel)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbCoordModel)) return;
            GlobalVar.feedStopModel = (StopModelEnum)cmbStopModel.SelectedItem;
            GlobalVar.feedCoordModel = (CoordModelEnum)cmbStopModel.SelectedItem;
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡上料轴参数配置", "停止模式", GlobalVar.feedStopModel.ToString());
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡上料轴参数配置", "坐标模式", GlobalVar.feedCoordModel.ToString());
        }

        private void btnSavePara_Click(object sender, EventArgs e)
        {
            if (JudgeTextBoxIsNullOrSpace(txtBoxStartSpeed)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxNormalSpeed)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxStopSpeed)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxAccTime)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxDccTime)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxSTime)) return;
            GlobalVar.feedStartSpeed = Convert.ToDouble(txtBoxStartSpeed.Text);
            GlobalVar.feedMotionSpeed = Convert.ToDouble(txtBoxNormalSpeed.Text);
            GlobalVar.feedStopSpeed = Convert.ToDouble(txtBoxStopSpeed.Text);
            GlobalVar.feedAccTime = Convert.ToDouble(txtBoxAccTime.Text);
            GlobalVar.feedDccTime = Convert.ToDouble(txtBoxDccTime.Text);
            GlobalVar.feedSTime = Convert.ToDouble(txtBoxSTime.Text);
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡上料轴参数配置", "起始速度", GlobalVar.feedStartSpeed.ToString());
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡上料轴参数配置", "正常运行速度", GlobalVar.feedMotionSpeed.ToString());
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡上料轴参数配置", "停止速度", GlobalVar.feedStopSpeed.ToString());
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡上料轴参数配置", "加速时间", GlobalVar.feedAccTime.ToString());
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡上料轴参数配置", "减速时间", GlobalVar.feedDccTime.ToString());
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡上料轴参数配置", "S段时间", GlobalVar.feedSTime.ToString());
//设置S段速度参数
            LTDMC.dmc_set_profile(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, GlobalVar.feedStartSpeed, GlobalVar.feedMotionSpeed, GlobalVar.feedAccTime, GlobalVar.feedDccTime, GlobalVar.feedStopSpeed);//设置速度参数
            LTDMC.dmc_set_s_profile(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, 0, GlobalVar.feedSTime);//设置S段速度参数
        }

        private void btnSaveCurOtherDIP_Click(object sender, EventArgs e)
        {
            if (JudgeComboBoxIsNullOrSpace(cmbFeedCylinderElecValve)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbFeedVacuumSolenoid)) return;
            GlobalVar.feedCylinderElecValve = cmbFeedCylinderElecValve.SelectedItem.ToString();
            GlobalVar.feedVacuumSolenoid = cmbFeedVacuumSolenoid.SelectedItem.ToString();
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输出信号配置", "上料气缸电磁阀", GlobalVar.feedCylinderElecValve.ToString());
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输出信号配置", "上料真空电磁阀", GlobalVar.feedVacuumSolenoid.ToString());
        }

        private void btnSaveHomePara_Click(object sender, EventArgs e)
        {
            if (JudgeComboBoxIsNullOrSpace(cmbHomeModel)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbHomeSpeedModel)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxHomeLowSpeed)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxHomeHighSpeed)) return;
            GlobalVar.feedHomeLowSpeed = Convert.ToDouble(txtBoxHomeLowSpeed.Text);
            GlobalVar.feedHomeHighSpeed = Convert.ToDouble(txtBoxHomeHighSpeed.Text);
            GlobalVar.feedHomeModel = (HomeModel)Convert.ToDouble(cmbHomeModel.SelectedItem);
            GlobalVar.feedHomeSpeedModel = (HomeSpeedModel)Convert.ToDouble(cmbHomeSpeedModel.SelectedItem);
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡上料轴参数配置", "回零低速", GlobalVar.feedHomeLowSpeed.ToString());
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡上料轴参数配置", "回零高速", GlobalVar.feedHomeHighSpeed.ToString());
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡上料轴参数配置", "回零模式", GlobalVar.feedHomeModel.ToString());
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡上料轴参数配置", "回零速度模式", GlobalVar.feedHomeSpeedModel.ToString());
        }

        private void btnCylinderElecValve_Click(object sender, EventArgs e)
        {
            if (!GlobalVar.lsAxiasDIs[44].CurIOStatus)
            {
                MessageBox.Show("急停信号按下，禁止操作");
            }
            if (!GlobalVar.totalRunFlag)
            {
                if (LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.FeedAxiasNumber) == 1 && LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber) == 1 && LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber) == 1)
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
                    MessageBox.Show("轴正在运动中，禁止操作");
                }
            }
            else
            {
                MessageBox.Show("设备正在全自动模式，禁止操作");
            }
        }

        private void btnSaveFirPos_Click(object sender, EventArgs e)
        {
            if (JudgeTextBoxIsNullOrSpace(txtBoxFirFeedPos)) return;
            GlobalVar.feedProPos1 = Convert.ToInt32(txtBoxFirFeedPos.Text);
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡上料轴参数配置", "取料位置1", GlobalVar.feedProPos1.ToString());
        }

        private void btnSaveSecPos_Click(object sender, EventArgs e)
        {
            if (JudgeTextBoxIsNullOrSpace(txtBoxSecFeedPos)) return;
            GlobalVar.feedProPos2 = Convert.ToInt32(txtBoxSecFeedPos.Text);
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡上料轴参数配置", "取料位置2", GlobalVar.feedProPos2.ToString());
        }

        private void btnSaveTakePlaPos_Click(object sender, EventArgs e)
        {
            if (JudgeTextBoxIsNullOrSpace(txtBoxTakePlasticsPos)) return;
            GlobalVar.feedTakeProPos = Convert.ToInt32(txtBoxTakePlasticsPos.Text);
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡上料轴参数配置", "放料位置", GlobalVar.feedTakeProPos.ToString());
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            if (!GlobalVar.lsAxiasDIs[44].CurIOStatus)
            {
                MessageBox.Show("急停信号按下，禁止操作");
            }
            if (!GlobalVar.totalRunFlag)
            {
                if (GlobalVar.lsAxiasDIs[14].CurIOStatus && !GlobalVar.lsAxiasDIs[15].CurIOStatus)
                {
                    LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, -30000, (ushort)1);
                    while (LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.FeedAxiasNumber) == 0)
                    {
                        Application.DoEvents();
                    }
                    GlobalVar.isHomeMotion = true;
                    double dStartVel;//起始速度
                    double dMaxVel;//运行速度
                    double dTacc;//加速时间
                    double dTdec;//减速时间
                    double dStopVel;//停止速度
                    double dS_para;//S段时间
                    if (!double.TryParse(txtBoxHomeLowSpeed.Text, out dStartVel)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxHomeHighSpeed.Text, out dMaxVel)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxAccTime.Text, out dTacc)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxDccTime.Text, out dTdec)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxStopSpeed.Text, out dStopVel)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxSTime.Text, out dS_para)) { MessageBox.Show("请输入有效值"); }
                    LTDMC.dmc_set_profile(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, dStartVel, dMaxVel, dTacc, dTdec, dStopVel);  //设置速度参数
                    LTDMC.dmc_set_s_profile(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, 0, dS_para);//设置S段速度参数
                    LTDMC.dmc_set_homemode(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, 1, (ushort)(HomeSpeedModel)cmbHomeSpeedModel.SelectedItem, (ushort)(HomeModel)cmbHomeModel.SelectedItem, 0);
                    LTDMC.dmc_set_home_position(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, 2, 0);
                    LTDMC.dmc_home_move(GlobalVar.CardId, GlobalVar.FeedAxiasNumber);
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

        private void btnStop_Click(object sender, EventArgs e)
        {

            LTDMC.dmc_stop(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, (ushort)(StopModelEnum)cmbStopModel.SelectedItem);
        }

        private void btnRunFirPos_Click(object sender, EventArgs e)
        {
            if (!GlobalVar.totalRunFlag)
            {
                if (GlobalVar.lsAxiasDIs[14].CurIOStatus && !GlobalVar.lsAxiasDIs[15].CurIOStatus)
                {
                    double dStartVel;//起始速度
                    double dMaxVel;//运行速度
                    double dTacc;//加速时间
                    double dTdec;//减速时间
                    double dStopVel;//停止速度
                    double dS_para;//S段时间
                    int disP;//目标位置
                    isTimeOut = true;//默认超时
                    if (!int.TryParse(txtBoxFirFeedPos.Text, out disP)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxStartSpeed.Text, out dStartVel)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxNormalSpeed.Text, out dMaxVel)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxAccTime.Text, out dTacc)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxDccTime.Text, out dTdec)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxStopSpeed.Text, out dStopVel)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxSTime.Text, out dS_para)) { MessageBox.Show("请输入有效值"); }
                    LTDMC.dmc_set_profile(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, dStartVel, dMaxVel, dTacc, dTdec, dStopVel);  //设置速度参数
                    LTDMC.dmc_set_s_profile(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, 0, dS_para);//设置S段速度参数
                    tokenSource = new CancellationTokenSource(20000);
                    tokenSource.Token.Register(new Action(() => { DialogResult = isTimeOut ? MessageBox.Show("电机走动超时") : MessageBox.Show("指定位置到达"); }));
                    LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, disP, (ushort)CoordModelEnum.绝对坐标);
                    Task.Factory.StartNew(new Action(() =>
                    {
                        while (!tokenSource.Token.IsCancellationRequested && LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.FeedAxiasNumber) == 0)
                        {
                            Thread.Sleep(5);
                            Application.DoEvents();
                            if (LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.FeedAxiasNumber) == 1)
                            { isTimeOut = false; tokenSource.Cancel(); }
                        }
                    }), tokenSource.Token);
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

        private void btnRunSecPos_Click(object sender, EventArgs e)
        {
            if (!GlobalVar.totalRunFlag)
            {
                if (GlobalVar.lsAxiasDIs[14].CurIOStatus && !GlobalVar.lsAxiasDIs[15].CurIOStatus)
                {
                    double dStartVel;//起始速度
                    double dMaxVel;//运行速度
                    double dTacc;//加速时间
                    double dTdec;//减速时间
                    double dStopVel;//停止速度
                    double dS_para;//S段时间
                    int disP;//目标位置
                    isTimeOut = true;//默认超时
                    if (!int.TryParse(txtBoxSecFeedPos.Text, out disP)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxStartSpeed.Text, out dStartVel)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxNormalSpeed.Text, out dMaxVel)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxAccTime.Text, out dTacc)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxDccTime.Text, out dTdec)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxStopSpeed.Text, out dStopVel)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxSTime.Text, out dS_para)) { MessageBox.Show("请输入有效值"); }
                    LTDMC.dmc_set_profile(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, dStartVel, dMaxVel, dTacc, dTdec, dStopVel);  //设置速度参数
                    LTDMC.dmc_set_s_profile(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, 0, dS_para);//设置S段速度参数
                    LTDMC.dmc_set_profile(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, dStartVel, dMaxVel, dTacc, dTdec, dStopVel);  //设置速度参数
                    LTDMC.dmc_set_s_profile(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, 0, dS_para);//设置S段速度参数
                    tokenSource = new CancellationTokenSource(20000);
                    tokenSource.Token.Register(new Action(() => { DialogResult = isTimeOut ? MessageBox.Show("电机走动超时") : MessageBox.Show("指定位置到达"); }));
                    LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, disP, (ushort)CoordModelEnum.绝对坐标);
                    Task.Factory.StartNew(new Action(() =>
                    {
                        while (!tokenSource.Token.IsCancellationRequested && LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.FeedAxiasNumber) == 0)
                        {
                            Thread.Sleep(5);
                            Application.DoEvents();
                            if (LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.FeedAxiasNumber) == 1)
                            { isTimeOut = false; tokenSource.Cancel(); }
                        }
                    }), tokenSource.Token);
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

        private void btnRunTakePlaPos_Click(object sender, EventArgs e)
        {
            if (!GlobalVar.totalRunFlag)
            {
                if (GlobalVar.lsAxiasDIs[14].CurIOStatus && !GlobalVar.lsAxiasDIs[15].CurIOStatus)
                {
                    double dStartVel;//起始速度
                    double dMaxVel;//运行速度
                    double dTacc;//加速时间
                    double dTdec;//减速时间
                    double dStopVel;//停止速度
                    double dS_para;//S段时间
                    int disP;//目标位置
                    isTimeOut = true;//默认超时
                    if (!int.TryParse(txtBoxTakePlasticsPos.Text, out disP)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxStartSpeed.Text, out dStartVel)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxNormalSpeed.Text, out dMaxVel)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxAccTime.Text, out dTacc)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxDccTime.Text, out dTdec)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxStopSpeed.Text, out dStopVel)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxSTime.Text, out dS_para)) { MessageBox.Show("请输入有效值"); }
                    LTDMC.dmc_set_profile(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, dStartVel, dMaxVel, dTacc, dTdec, dStopVel);  //设置速度参数
                    LTDMC.dmc_set_s_profile(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, 0, dS_para);//设置S段速度参数
                    tokenSource = new CancellationTokenSource(20000);
                    tokenSource.Token.Register(new Action(() => { DialogResult = isTimeOut ? MessageBox.Show("电机走动超时") : MessageBox.Show("指定位置到达"); }));
                    LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, disP, (ushort)CoordModelEnum.绝对坐标);
                    Task.Factory.StartNew(new Action(() =>
                    {
                        while (!tokenSource.Token.IsCancellationRequested && LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.FeedAxiasNumber) == 0)
                        {
                            Thread.Sleep(5);
                            Application.DoEvents();
                            if (LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.FeedAxiasNumber) == 1)
                            { isTimeOut = false; tokenSource.Cancel(); }
                        }
                    }), tokenSource.Token);
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

        private void chkBoxAxiasEnable_CheckedChanged(object sender, EventArgs e)
        {
            //if (LTDMC.dmc_read_sevon_pin(GlobalVar.CardId, GlobalVar.FeedAxiasNumber)==0)
            //{

            //}

            LTDMC.dmc_write_sevon_pin(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, (ushort)(chkBoxAxiasEnable.Checked ? 0 : 1));
        }

        private void btnVacSorb_Click(object sender, EventArgs e)
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

        private void btnBackRun_MouseUp(object sender, MouseEventArgs e)
        {
            LTDMC.dmc_stop(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, Convert.ToUInt16((StopModelEnum)cmbStopModel.SelectedItem));
        }

        private void btnForwardRun_MouseDown(object sender, MouseEventArgs e)
        {
            if (!GlobalVar.lsAxiasDIs[44].CurIOStatus)
            {
                MessageBox.Show("急停信号按下，禁止操作");
            }
            if (!GlobalVar.totalRunFlag)
            {
                if (GlobalVar.lsAxiasDIs[14].CurIOStatus && !GlobalVar.lsAxiasDIs[15].CurIOStatus)
                {
                    double dStartVel;//起始速度
                    double dMaxVel;//运行速度
                    double dTacc;//加速时间
                    double dTdec;//减速时间
                    double dStopVel;//停止速度
                    double dS_para;//S段时间
                    if (!double.TryParse(txtBoxStartSpeed.Text, out dStartVel)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxNormalSpeed.Text, out dMaxVel)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxAccTime.Text, out dTacc)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxDccTime.Text, out dTdec)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxStopSpeed.Text, out dStopVel)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxSTime.Text, out dS_para)) { MessageBox.Show("请输入有效值"); }
                    LTDMC.dmc_set_profile(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, dStartVel, dMaxVel, dTacc, dTdec, dStopVel);//设置速度参数

                    LTDMC.dmc_set_s_profile(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, 0, dS_para);//设置S段速度参数

                    LTDMC.dmc_vmove(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, 1);
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

        private void btnForwardRun_MouseUp(object sender, MouseEventArgs e)
        {
            LTDMC.dmc_stop(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, Convert.ToUInt16((StopModelEnum)cmbStopModel.SelectedItem));
        }

        private void btnBackRun_MouseDown(object sender, MouseEventArgs e)
        {
            if (!GlobalVar.lsAxiasDIs[44].CurIOStatus)
            {
                MessageBox.Show("急停信号按下，禁止操作");
            }
            if (!GlobalVar.totalRunFlag)
            {
                if (GlobalVar.lsAxiasDIs[14].CurIOStatus && !GlobalVar.lsAxiasDIs[15].CurIOStatus)
                {
                    double dStartVel;//起始速度
                    double dMaxVel;//运行速度
                    double dTacc;//加速时间
                    double dTdec;//减速时间
                    double dStopVel;//停止速度
                    double dS_para;//S段时间
                    if (!double.TryParse(txtBoxStartSpeed.Text, out dStartVel)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxNormalSpeed.Text, out dMaxVel)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxAccTime.Text, out dTacc)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxDccTime.Text, out dTdec)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxStopSpeed.Text, out dStopVel)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxSTime.Text, out dS_para)) { MessageBox.Show("请输入有效值"); }
                    LTDMC.dmc_set_profile(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, dStartVel, dMaxVel, dTacc, dTdec, dStopVel);//设置速度参数

                    LTDMC.dmc_set_s_profile(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, 0, dS_para);//设置S段速度参数

                    LTDMC.dmc_vmove(GlobalVar.CardId, GlobalVar.FeedAxiasNumber, 0);
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                TraversalAllPic();
                picPLim.Image = GlobalVar.lsFirAxiasSensorDIs[(int.Parse(GlobalVar.feedDiPLimP.Split(',')[1]))].ToString() == "0" ? KEE.Renault.Properties.Resources.RedLed : KEE.Renault.Properties.Resources.GrayLed;
                picNLim.Image = GlobalVar.lsFirAxiasSensorDIs[(int.Parse(GlobalVar.feedDiNLimP.Split(',')[1]))].ToString() == "0" ? KEE.Renault.Properties.Resources.RedLed : KEE.Renault.Properties.Resources.GrayLed;
                picHLim.Image = GlobalVar.lsSecAxiasSensorDIs[(int.Parse(GlobalVar.feedDiHLimP.Split(',')[1]))].ToString() == "0" ? KEE.Renault.Properties.Resources.RedLed : KEE.Renault.Properties.Resources.GrayLed;
                //Application.DoEvents();
            }));
        }

        private void FrmFeedAxias_Enter(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void FrmFeedAxias_Leave(object sender, EventArgs e)
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
            cmbStopModel.SelectedItem = GlobalVar.feedStopModel;
            cmbCoordModel.SelectedItem = GlobalVar.feedCoordModel;
            txtBoxStartSpeed.Text = GlobalVar.feedStartSpeed.ToString();
            txtBoxNormalSpeed.Text = GlobalVar.feedMotionSpeed.ToString();
            txtBoxStopSpeed.Text = GlobalVar.feedStopSpeed.ToString();
            txtBoxAccTime.Text = GlobalVar.feedAccTime.ToString();
            txtBoxDccTime.Text = GlobalVar.feedDccTime.ToString();
            txtBoxSTime.Text = GlobalVar.feedSTime.ToString();
            txtBoxHomeLowSpeed.Text = GlobalVar.feedHomeLowSpeed.ToString();
            txtBoxHomeHighSpeed.Text = GlobalVar.feedHomeHighSpeed.ToString();
            cmbHomeModel.SelectedItem = GlobalVar.feedHomeModel;
            cmbHomeSpeedModel.SelectedItem = GlobalVar.feedHomeSpeedModel;
            txtBoxTakePlasticsPos.Text = GlobalVar.feedTakeProPos.ToString();
            txtBoxFirFeedPos.Text = GlobalVar.feedProPos1.ToString();
            txtBoxSecFeedPos.Text = GlobalVar.feedProPos2.ToString();
            cmbPLimP.SelectedItem = GlobalVar.feedDiPLimP;
            cmbNLimP.SelectedItem = GlobalVar.feedDiNLimP;
            cmbHomeP.SelectedItem = GlobalVar.feedDiHLimP;
            cmbFeedCylinderHighLim.SelectedItem = GlobalVar.feedCylinderHighLim;
            cmbFeedCylinderLowLim.SelectedItem = GlobalVar.feedCylinderLowLim;
            cmbFeedVacuumLim.SelectedItem = GlobalVar.feedVacuumLim;
            cmbFeedCylinderElecValve.SelectedItem = GlobalVar.feedCylinderElecValve;
            cmbFeedVacuumSolenoid.SelectedItem = GlobalVar.feedVacuumSolenoid;
            chkBoxAxiasEnable.Checked = (LTDMC.dmc_read_sevon_pin(GlobalVar.CardId, GlobalVar.FeedAxiasNumber) == 0);
        }
        private void InitAllPictures()
        {
            foreach (var pic in groupBox8.Controls)
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
