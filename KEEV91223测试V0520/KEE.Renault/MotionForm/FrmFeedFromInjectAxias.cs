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
    public partial class FrmFeedFromInjectAxias : Form
    {
        public FrmFeedFromInjectAxias()
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
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                TraversalAllPic();
                picPLim.Image = GlobalVar.lsFirAxiasSensorDIs[(int.Parse(GlobalVar.feedFromInjectPLimP.Split(',')[1]))].ToString() == "0" ? KEE.Renault.Properties.Resources.RedLed : KEE.Renault.Properties.Resources.GrayLed;
                picNLim.Image = GlobalVar.lsFirAxiasSensorDIs[(int.Parse(GlobalVar.feedFromInjectPLimN.Split(',')[1]))].ToString() == "0" ? KEE.Renault.Properties.Resources.RedLed : KEE.Renault.Properties.Resources.GrayLed;
                picHLim.Image = GlobalVar.lsSecAxiasSensorDIs[(int.Parse(GlobalVar.feedFromInjectPLimHome.Split(',')[1]))].ToString() == "0" ? KEE.Renault.Properties.Resources.RedLed : KEE.Renault.Properties.Resources.GrayLed;
                //Application.DoEvents();
            }));
        }

        private void btnSaveModel_Click(object sender, EventArgs e)
        {
            if (JudgeComboBoxIsNullOrSpace(cmbStopModel)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbCoordModel)) return;
            GlobalVar.injectFeedStopModel = (StopModelEnum)cmbStopModel.SelectedItem;
            GlobalVar.injectFeedCoordModel = (CoordModelEnum)cmbStopModel.SelectedItem;
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "来自有信机械手上料轴参数配置", "停止模式", GlobalVar.injectFeedStopModel.ToString());
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "来自有信机械手上料轴参数配置", "坐标模式", GlobalVar.injectFeedCoordModel.ToString());
        }

        private void btnSavePara_Click(object sender, EventArgs e)
        {
            if (JudgeTextBoxIsNullOrSpace(txtBoxStartSpeed)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxNormalSpeed)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxStopSpeed)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxAccTime)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxDccTime)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxSTime)) return;
            GlobalVar.injectFeedStartSpeed = Convert.ToDouble(txtBoxStartSpeed.Text);
            GlobalVar.injectFeedMotionSpeed = Convert.ToDouble(txtBoxNormalSpeed.Text);
            GlobalVar.injectFeedStopSpeed = Convert.ToDouble(txtBoxStopSpeed.Text);
            GlobalVar.injectFeedAccTime = Convert.ToDouble(txtBoxAccTime.Text);
            GlobalVar.injectFeedDccTime = Convert.ToDouble(txtBoxDccTime.Text);
            GlobalVar.injectFeedSTime = Convert.ToDouble(txtBoxSTime.Text);
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "来自有信机械手上料轴参数配置", "起始速度", GlobalVar.injectFeedStartSpeed.ToString());
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "来自有信机械手上料轴参数配置", "正常运行速度", GlobalVar.injectFeedMotionSpeed.ToString());
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "来自有信机械手上料轴参数配置", "停止速度", GlobalVar.injectFeedStopSpeed.ToString());
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "来自有信机械手上料轴参数配置", "加速时间", GlobalVar.injectFeedAccTime.ToString());
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "来自有信机械手上料轴参数配置", "减速时间", GlobalVar.injectFeedDccTime.ToString());
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "来自有信机械手上料轴参数配置", "S段时间", GlobalVar.injectFeedSTime.ToString());
            LTDMC.dmc_set_profile(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, GlobalVar.injectFeedStartSpeed, GlobalVar.injectFeedMotionSpeed, GlobalVar.injectFeedAccTime, GlobalVar.injectFeedDccTime, GlobalVar.injectFeedStopSpeed);//设置速度参数
            LTDMC.dmc_set_s_profile(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, 0, GlobalVar.injectFeedSTime);
        }

        private void btnSaveDiVal_Click(object sender, EventArgs e)
        {
            if (JudgeComboBoxIsNullOrSpace(cmbPLimP)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbNLimP)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbHomeP)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbYuShinBlowingFinished)) return;
            GlobalVar.feedFromInjectPLimP    = cmbPLimP.SelectedItem.ToString();
            GlobalVar.feedFromInjectPLimN    = cmbNLimP.SelectedItem.ToString();
            GlobalVar.feedFromInjectPLimHome = cmbHomeP.SelectedItem.ToString();
            GlobalVar.yuShinBlowingFinished = cmbYuShinBlowingFinished.SelectedItem.ToString();
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "轴卡输入信号配置", "与注塑机关联的取料轴正极限", GlobalVar.feedFromInjectPLimP.ToString());
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "轴卡输入信号配置", "与注塑机关联的取料轴负极限", GlobalVar.feedFromInjectPLimN.ToString());
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "轴卡输入信号配置", "与注塑机关联的取料轴原点", GlobalVar.feedFromInjectPLimHome.ToString());
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输入信号配置", "有信放料完成", GlobalVar.yuShinBlowingFinished.ToString());
        }

        private void btnSaveCurOtherDIP_Click(object sender, EventArgs e)
        {
            if (JudgeComboBoxIsNullOrSpace(cmbAllowYuShinTakePro)) return;
            GlobalVar.allowYuShinTakePro = cmbAllowYuShinTakePro.SelectedItem.ToString();
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输出信号配置", "允许有信放料", GlobalVar.allowYuShinTakePro.ToString());
        }

        private void btnAllowYuShinTakePro_Click(object sender, EventArgs e)
        {
            if (!GlobalVar.lsAxiasDIs[44].CurIOStatus)
            {
                MessageBox.Show("急停信号按下，禁止操作");
            }
            if (!GlobalVar.totalRunFlag)
            {
                if (LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber) == 1)
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

        private void btnSaveHomePara_Click(object sender, EventArgs e)
        {
            if (JudgeComboBoxIsNullOrSpace(cmbHomeModel)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbHomeSpeedModel)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxHomeLowSpeed)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxHomeHighSpeed)) return;
            GlobalVar.injectFeedHomeLowSpeed = Convert.ToDouble(txtBoxHomeLowSpeed.Text);
            GlobalVar.injectFeedHomeHighSpeed = Convert.ToDouble(txtBoxHomeHighSpeed.Text);
            GlobalVar.injectFeedHomeModel = (HomeModel)Convert.ToDouble(cmbHomeModel.SelectedItem);
            GlobalVar.injectFeedHomeSpeedModel = (HomeSpeedModel)Convert.ToDouble(cmbHomeSpeedModel.SelectedItem);
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "来自有信机械手上料轴参数配置", "回零低速", GlobalVar.injectFeedHomeLowSpeed.ToString());
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "来自有信机械手上料轴参数配置", "回零高速", GlobalVar.injectFeedHomeHighSpeed.ToString());
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "来自有信机械手上料轴参数配置", "回零模式", GlobalVar.injectFeedHomeModel.ToString());
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "来自有信机械手上料轴参数配置", "回零速度模式", GlobalVar.injectFeedHomeSpeedModel.ToString());
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
                    LTDMC.dmc_set_profile(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, dStartVel, dMaxVel, dTacc, dTdec, dStopVel);  //设置速度参数
                    LTDMC.dmc_set_s_profile(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, 0, dS_para);//设置S段速度参数
                    LTDMC.dmc_set_homemode(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, 1, (double)(HomeSpeedModel)cmbHomeSpeedModel.SelectedItem, (ushort)(HomeModel)cmbHomeModel.SelectedItem, 0);
                    LTDMC.dmc_set_home_position(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, 2, 0);
                    LTDMC.dmc_home_move(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber);
                }
                else
                {
                    MessageBox.Show("气缸处于下降状态，禁止移动");
                }
            }
            else
            { MessageBox.Show("设备正在全自动模式，禁止操作"); }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            LTDMC.dmc_stop(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, (ushort)(StopModelEnum)cmbStopModel.SelectedItem);
        }

        private void btnFirFeedFromInjectPos_Click(object sender, EventArgs e)
        {
            if (!GlobalVar.totalRunFlag)
            {
                if (GlobalVar.lsAxiasDIs[14].CurIOStatus && !GlobalVar.lsAxiasDIs[15].CurIOStatus)
                {

                }
                else
                {
                    MessageBox.Show("气缸处于下降状态，禁止移动");
                }
            }
            else
            { MessageBox.Show("设备正在全自动模式，禁止操作"); }
            double dStartVel;//起始速度
            double dMaxVel;//运行速度
            double dTacc;//加速时间
            double dTdec;//减速时间
            double dStopVel;//停止速度
            double dS_para;//S段时间
            int disP;//目标位置
            isTimeOut = true;//默认超时
            if (!int.TryParse(txtBoxFirFeedFromInjectPos.Text, out disP)) { MessageBox.Show("请输入有效值"); }
            if (!double.TryParse(txtBoxStartSpeed.Text, out dStartVel)) { MessageBox.Show("请输入有效值"); }
            if (!double.TryParse(txtBoxNormalSpeed.Text, out dMaxVel)) { MessageBox.Show("请输入有效值"); }
            if (!double.TryParse(txtBoxAccTime.Text, out dTacc)) { MessageBox.Show("请输入有效值"); }
            if (!double.TryParse(txtBoxDccTime.Text, out dTdec)) { MessageBox.Show("请输入有效值"); }
            if (!double.TryParse(txtBoxStopSpeed.Text, out dStopVel)) { MessageBox.Show("请输入有效值"); }
            if (!double.TryParse(txtBoxSTime.Text, out dS_para)) { MessageBox.Show("请输入有效值"); }
            LTDMC.dmc_set_profile(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, dStartVel, dMaxVel, dTacc, dTdec, dStopVel);  //设置速度参数
            LTDMC.dmc_set_s_profile(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, 0, dS_para);//设置S段速度参数
            tokenSource = new CancellationTokenSource(20000);
            tokenSource.Token.Register(new Action(() => { DialogResult = isTimeOut ? MessageBox.Show("电机走动超时") : MessageBox.Show("指定位置到达"); }));
            LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, disP, (ushort)CoordModelEnum.绝对坐标);
            Task.Factory.StartNew(new Action(() =>
            {
                while (!tokenSource.Token.IsCancellationRequested &&LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber) == 0)
                {
                    Thread.Sleep(5);
                    Application.DoEvents();
                    if (LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber) == 1)
                    { isTimeOut = false; tokenSource.Cancel(); }
                }
            }), tokenSource.Token);
        }

        private void btnSecFeedFromInjectPos_Click(object sender, EventArgs e)
        {
            if (!GlobalVar.totalRunFlag)
            {
                if (GlobalVar.lsAxiasDIs[14].CurIOStatus && !GlobalVar.lsAxiasDIs[15].CurIOStatus)
                {

                }
                else
                {
                    MessageBox.Show("气缸处于下降状态，禁止移动");
                }
            }
            else
            { MessageBox.Show("设备正在全自动模式，禁止操作"); }
            double dStartVel;//起始速度
            double dMaxVel;//运行速度
            double dTacc;//加速时间
            double dTdec;//减速时间
            double dStopVel;//停止速度
            double dS_para;//S段时间
            int disP;//目标位置
            isTimeOut = true;//默认超时
            if (!int.TryParse(txtBoxSecFeedFromInjectPos.Text, out disP)) { MessageBox.Show("请输入有效值"); }
            if (!double.TryParse(txtBoxStartSpeed.Text, out dStartVel)) { MessageBox.Show("请输入有效值"); }
            if (!double.TryParse(txtBoxNormalSpeed.Text, out dMaxVel)) { MessageBox.Show("请输入有效值"); }
            if (!double.TryParse(txtBoxAccTime.Text, out dTacc)) { MessageBox.Show("请输入有效值"); }
            if (!double.TryParse(txtBoxDccTime.Text, out dTdec)) { MessageBox.Show("请输入有效值"); }
            if (!double.TryParse(txtBoxStopSpeed.Text, out dStopVel)) { MessageBox.Show("请输入有效值"); }
            if (!double.TryParse(txtBoxSTime.Text, out dS_para)) { MessageBox.Show("请输入有效值"); }
            LTDMC.dmc_set_profile(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, dStartVel, dMaxVel, dTacc, dTdec, dStopVel);  //设置速度参数
            LTDMC.dmc_set_s_profile(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, 0, dS_para);//设置S段速度参数
            tokenSource = new CancellationTokenSource(20000);
            tokenSource.Token.Register(new Action(() => { DialogResult = isTimeOut ? MessageBox.Show("电机走动超时") : MessageBox.Show("指定位置到达"); }));
            LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, disP, (ushort)CoordModelEnum.绝对坐标);
            Task.Factory.StartNew(new Action(() =>
            {
                while (!tokenSource.Token.IsCancellationRequested && LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber) == 0)
                {
                    Thread.Sleep(5);
                    Application.DoEvents();
                    if (LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber) == 1)
                    { isTimeOut = false; tokenSource.Cancel(); }
                }
            }), tokenSource.Token);
        }

        private void btnSaveFromInjectPos_Click(object sender, EventArgs e)
        {
            if (JudgeTextBoxIsNullOrSpace(txtBoxFeedFromInjectPos)) return;
            GlobalVar.injectTakeProPos = Convert.ToInt32(txtBoxFeedFromInjectPos.Text);
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "来自有信机械手上料轴参数配置", "放料位置", GlobalVar.injectTakeProPos.ToString());
        }

        private void btnTakeProFromInjectPos_Click(object sender, EventArgs e)
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
                    if (!int.TryParse(txtBoxFeedFromInjectPos.Text, out disP)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxStartSpeed.Text, out dStartVel)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxNormalSpeed.Text, out dMaxVel)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxAccTime.Text, out dTacc)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxDccTime.Text, out dTdec)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxStopSpeed.Text, out dStopVel)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxSTime.Text, out dS_para)) { MessageBox.Show("请输入有效值"); }
                    LTDMC.dmc_set_profile(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, dStartVel, dMaxVel, dTacc, dTdec, dStopVel);  //设置速度参数
                    LTDMC.dmc_set_s_profile(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, 0, dS_para);//设置S段速度参数
                    tokenSource = new CancellationTokenSource(20000);
                    tokenSource.Token.Register(new Action(() => { DialogResult = isTimeOut ? MessageBox.Show("电机走动超时") : MessageBox.Show("指定位置到达"); }));
                    LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, disP, (ushort)CoordModelEnum.绝对坐标);
                    Task.Factory.StartNew(new Action(() =>
                    {
                        while (!tokenSource.Token.IsCancellationRequested && LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber) == 0)
                        {
                            Thread.Sleep(5);
                            Application.DoEvents();
                            if (LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber) == 1)
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
            { MessageBox.Show("设备正在全自动模式，禁止操作"); }
        }

        private void btnSaveFirPos_Click(object sender, EventArgs e)
        {
            if (JudgeTextBoxIsNullOrSpace(txtBoxFirFeedFromInjectPos)) return;
            GlobalVar.injectFeedProPos1 = Convert.ToInt32(txtBoxFirFeedFromInjectPos.Text);
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "来自有信机械手上料轴参数配置", "取料位置1", GlobalVar.injectFeedProPos1.ToString());
        }

        private void btnSaveSecPos_Click(object sender, EventArgs e)
        {
            if (JudgeTextBoxIsNullOrSpace(txtBoxSecFeedFromInjectPos)) return;
            GlobalVar.injectFeedProPos2 = Convert.ToInt32(txtBoxSecFeedFromInjectPos.Text);
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "来自有信机械手上料轴参数配置", "取料位置2", GlobalVar.injectFeedProPos2.ToString());
        }

        private void btnBackRun_MouseUp(object sender, MouseEventArgs e)
        {
            LTDMC.dmc_stop(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, Convert.ToUInt16((StopModelEnum)cmbStopModel.SelectedItem));
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
                    LTDMC.dmc_set_profile(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, dStartVel, dMaxVel, dTacc, dTdec, dStopVel);//设置速度参数

                    LTDMC.dmc_set_s_profile(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, 0, dS_para);//设置S段速度参数

                    LTDMC.dmc_vmove(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, 1);
                }
                else
                {
                    MessageBox.Show("气缸处于下降状态，禁止移动");
                }
            }
            else
            { MessageBox.Show("设备正在全自动模式，禁止操作"); }
           
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
                    LTDMC.dmc_set_profile(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, dStartVel, dMaxVel, dTacc, dTdec, dStopVel);//设置速度参数

                    LTDMC.dmc_set_s_profile(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, 0, dS_para);//设置S段速度参数

                    LTDMC.dmc_vmove(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, 0);
                }
                else
                {
                    MessageBox.Show("气缸处于下降状态，禁止移动");
                }
            }
            else
            { MessageBox.Show("设备正在全自动模式，禁止操作"); }
        }

        private void btnForwardRun_MouseUp(object sender, MouseEventArgs e)
        {
            LTDMC.dmc_stop(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, (ushort)(StopModelEnum)cmbStopModel.SelectedItem);
        }

        private void FrmFeedFromInjectAxias_Enter(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void FrmFeedFromInjectAxias_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void chkBoxAxiasEnable_CheckedChanged(object sender, EventArgs e)
        {
            LTDMC.dmc_write_sevon_pin(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber, (ushort)(chkBoxAxiasEnable.Checked ? 0 : 1));
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
            cmbStopModel.SelectedItem = GlobalVar.injectFeedStopModel;
            cmbCoordModel.SelectedItem = GlobalVar.injectFeedCoordModel;
            txtBoxStartSpeed.Text = GlobalVar.injectFeedStartSpeed.ToString();
            txtBoxNormalSpeed.Text = GlobalVar.injectFeedMotionSpeed.ToString();
            txtBoxStopSpeed.Text = GlobalVar.injectFeedStopSpeed.ToString();
            txtBoxAccTime.Text = GlobalVar.injectFeedAccTime.ToString();
            txtBoxDccTime.Text = GlobalVar.injectFeedDccTime.ToString();
            txtBoxSTime.Text = GlobalVar.injectFeedSTime.ToString();
            txtBoxHomeLowSpeed.Text = GlobalVar.injectFeedHomeLowSpeed.ToString();
            txtBoxHomeHighSpeed.Text = GlobalVar.injectFeedHomeHighSpeed.ToString();
            cmbHomeModel.SelectedItem = GlobalVar.injectFeedHomeModel;
            cmbHomeSpeedModel.SelectedItem = GlobalVar.injectFeedHomeSpeedModel;
            txtBoxFeedFromInjectPos.Text = GlobalVar.injectTakeProPos.ToString();
            txtBoxFirFeedFromInjectPos.Text = GlobalVar.injectFeedProPos1.ToString();
            txtBoxSecFeedFromInjectPos.Text = GlobalVar.injectFeedProPos2.ToString();
            cmbPLimP.SelectedItem = GlobalVar.feedFromInjectPLimP;
            cmbNLimP.SelectedItem = GlobalVar.feedFromInjectPLimN;
            cmbHomeP.SelectedItem = GlobalVar.feedFromInjectPLimHome;
            cmbYuShinBlowingFinished.SelectedItem = GlobalVar.yuShinBlowingFinished;
            cmbAllowYuShinTakePro.SelectedItem = GlobalVar.allowYuShinTakePro;
            chkBoxAxiasEnable.Checked = (LTDMC.dmc_read_sevon_pin(GlobalVar.CardId, GlobalVar.InjectFeedAxiasNumber) == 0);
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
