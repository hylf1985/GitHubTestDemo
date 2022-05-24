using ClassINI;
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
    public partial class FrmFeedRAxias : Form
    {
        public FrmFeedRAxias()
        {
            InitializeComponent();
            cmbCoordModel.DataSource = LSParamCommonInit.coordModels;
            cmbStopModel.DataSource = LSParamCommonInit.stopModels;
            cmbHomeModel.DataSource = LSParamCommonInit.homeModels;
            cmbHomeSpeedModel.DataSource = LSParamCommonInit.homeSpeeds;
            InitCfgUIData();
        }

        #region 变量
        CancellationTokenSource tokenSource = null;
        bool isTimeOut = true;
        #endregion

        #region 窗体事件
        private void btnSaveModel_Click(object sender, EventArgs e)
        {
            if (JudgeComboBoxIsNullOrSpace(cmbStopModel)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbCoordModel)) return;
            GlobalVar.feedRStopModel = (StopModelEnum)cmbStopModel.SelectedItem;
            GlobalVar.feedRCoordModel = (CoordModelEnum)cmbStopModel.SelectedItem;
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡上料R轴参数配置", "停止模式", GlobalVar.feedRStopModel.ToString());
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡上料R轴参数配置", "坐标模式", GlobalVar.feedRCoordModel.ToString());
        }

        private void btnSavePara_Click(object sender, EventArgs e)
        {
            if (JudgeTextBoxIsNullOrSpace(txtBoxStartSpeed)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxNormalSpeed)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxStopSpeed)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxAccTime)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxDccTime)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxSTime)) return;
            GlobalVar.feedRStartSpeed = Convert.ToDouble(txtBoxStartSpeed.Text);
            GlobalVar.feedRMotionSpeed = Convert.ToDouble(txtBoxNormalSpeed.Text);
            GlobalVar.feedRStopSpeed = Convert.ToDouble(txtBoxStopSpeed.Text);
            GlobalVar.feedRAccTime = Convert.ToDouble(txtBoxAccTime.Text);
            GlobalVar.feedRDccTime = Convert.ToDouble(txtBoxDccTime.Text);
            GlobalVar.feedRSTime = Convert.ToDouble(txtBoxSTime.Text);
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡上料R轴参数配置", "起始速度", GlobalVar.feedRStartSpeed.ToString());
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡上料R轴参数配置", "正常运行速度", GlobalVar.feedRMotionSpeed.ToString());
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡上料R轴参数配置", "停止速度", GlobalVar.feedRStopSpeed.ToString());
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡上料R轴参数配置", "加速时间", GlobalVar.feedRAccTime.ToString());
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡上料R轴参数配置", "减速时间", GlobalVar.feedRDccTime.ToString());
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡上料R轴参数配置", "S段时间", GlobalVar.feedRSTime.ToString());
   
            LTDMC.dmc_set_profile(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, GlobalVar.feedRStartSpeed, GlobalVar.feedRMotionSpeed, GlobalVar.feedRAccTime, GlobalVar.feedRDccTime, GlobalVar.feedRStopSpeed);//设置速度参数
            LTDMC.dmc_set_s_profile(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, 0, GlobalVar.feedRSTime);//设置S段速度参数
        }

        private void btnSaveHomePara_Click(object sender, EventArgs e)
        {
            if (JudgeComboBoxIsNullOrSpace(cmbHomeModel)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbHomeSpeedModel)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxHomeLowSpeed)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxHomeHighSpeed)) return;
            GlobalVar.feedRHomeLowSpeed = Convert.ToDouble(txtBoxHomeLowSpeed.Text);
            GlobalVar.feedRHomeHighSpeed = Convert.ToDouble(txtBoxHomeHighSpeed.Text);
            GlobalVar.feedRHomeModel = (HomeModel)Convert.ToDouble(cmbHomeModel.SelectedItem);
            GlobalVar.feedRHomeSpeedModel = (HomeSpeedModel)Convert.ToDouble(cmbHomeSpeedModel.SelectedItem);
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡上料R轴参数配置", "回零低速", GlobalVar.feedRHomeLowSpeed.ToString());
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡上料R轴参数配置", "回零高速", GlobalVar.feedRHomeHighSpeed.ToString());
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡上料R轴参数配置", "回零模式", GlobalVar.feedRHomeModel.ToString());
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡上料R轴参数配置", "回零速度模式", GlobalVar.feedRHomeSpeedModel.ToString());
        }

        private void btnSaveRAxiasFeedPos_Click(object sender, EventArgs e)
        {
            //if (JudgeTextBoxIsNullOrSpace(txtBoxRAxiasFeedDeg)) return;
            //GlobalVar.feedRProPos = Convert.ToInt32(txtBoxRAxiasFeedDeg.Text);
            //INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡上料R轴参数配置", "取料位置", GlobalVar.feedRProPos.ToString());
        }

        private void btnSaveRAxiasTakePos_Click(object sender, EventArgs e)
        {
            //if (JudgeTextBoxIsNullOrSpace(txtBoxRAxiasTakeDeg)) return;
            //GlobalVar.feedRTakeProPos = Convert.ToInt32(txtBoxRAxiasTakeDeg.Text);
            //INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡上料R轴参数配置", "放料位置", GlobalVar.feedRTakeProPos.ToString());
        }

        private void btnSaveDiVal_Click(object sender, EventArgs e)
        {
            if (JudgeComboBoxIsNullOrSpace(cmbRAxiasHomeP)) return;
            GlobalVar.feedRDiHLimP = cmbRAxiasHomeP.SelectedItem.ToString();
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "轴卡输入信号配置", "取料R轴原点", GlobalVar.feedRDiHLimP.ToString());
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
                    LTDMC.dmc_set_profile(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, dStartVel, dMaxVel, dTacc, dTdec, dStopVel);  //设置速度参数
                    LTDMC.dmc_set_s_profile(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, 0, dS_para);//设置S段速度参数
                    LTDMC.dmc_set_homemode(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, 1, (ushort)(HomeSpeedModel)cmbHomeSpeedModel.SelectedItem, (ushort)(HomeModel)cmbHomeModel.SelectedItem, 0);
                    LTDMC.dmc_set_home_position(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, 2, 0);
                    LTDMC.dmc_home_move(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber);
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
            LTDMC.dmc_stop(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, (ushort)(StopModelEnum)cmbStopModel.SelectedItem);
        }

        private void btnFeedPos_Click(object sender, EventArgs e)
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
                    if (!int.TryParse(txtBoxRAxiasFeedDeg.Text, out disP)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxStartSpeed.Text, out dStartVel)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxNormalSpeed.Text, out dMaxVel)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxAccTime.Text, out dTacc)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxDccTime.Text, out dTdec)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxStopSpeed.Text, out dStopVel)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxSTime.Text, out dS_para)) { MessageBox.Show("请输入有效值"); }
                    LTDMC.dmc_set_profile(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, dStartVel, dMaxVel, dTacc, dTdec, dStopVel);  //设置速度参数
                    LTDMC.dmc_set_s_profile(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, 0, dS_para);//设置S段速度参数
                    tokenSource = new CancellationTokenSource(20000);
                    tokenSource.Token.Register(new Action(() => { DialogResult = isTimeOut ? MessageBox.Show("电机走动超时") : MessageBox.Show("指定位置到达"); }));
                    LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, disP, (ushort)CoordModelEnum.绝对坐标);
                    Task.Factory.StartNew(new Action(() =>
                    {
                        while (!tokenSource.Token.IsCancellationRequested && LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber) == 0)
                        {
                            Thread.Sleep(5);
                            Application.DoEvents();
                            if (LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber) == 1)
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

        private void btnTakePos_Click(object sender, EventArgs e)
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
                    if (!int.TryParse(txtBoxRAxiasTakeDeg.Text, out disP)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxStartSpeed.Text, out dStartVel)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxNormalSpeed.Text, out dMaxVel)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxAccTime.Text, out dTacc)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxDccTime.Text, out dTdec)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxStopSpeed.Text, out dStopVel)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxSTime.Text, out dS_para)) { MessageBox.Show("请输入有效值"); }
                    LTDMC.dmc_set_profile(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, dStartVel, dMaxVel, dTacc, dTdec, dStopVel);  //设置速度参数
                    LTDMC.dmc_set_s_profile(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, 0, dS_para);//设置S段速度参数
                    tokenSource = new CancellationTokenSource(20000);
                    tokenSource.Token.Register(new Action(() => { DialogResult = isTimeOut ? MessageBox.Show("电机走动超时") : MessageBox.Show("指定位置到达"); }));
                    LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, disP, (ushort)CoordModelEnum.绝对坐标);
                    Task.Factory.StartNew(new Action(() =>
                    {
                        while (!tokenSource.Token.IsCancellationRequested && LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber) == 0)
                        {
                            Thread.Sleep(5);
                            Application.DoEvents();
                            if (LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber) == 1)
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

        private void btnBackRun_MouseUp(object sender, MouseEventArgs e)
        {
            LTDMC.dmc_stop(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, Convert.ToUInt16((StopModelEnum)cmbStopModel.SelectedItem));
        }

        private void btnForwardRun_MouseUp(object sender, MouseEventArgs e)
        {
            LTDMC.dmc_stop(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, Convert.ToUInt16((StopModelEnum)cmbStopModel.SelectedItem));
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
                    LTDMC.dmc_set_profile(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, dStartVel, dMaxVel, dTacc, dTdec, dStopVel);//设置速度参数

                    LTDMC.dmc_set_s_profile(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, 0, dS_para);//设置S段速度参数

                    LTDMC.dmc_vmove(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, 0);
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
                    LTDMC.dmc_set_profile(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, dStartVel, dMaxVel, dTacc, dTdec, dStopVel);//设置速度参数

                    LTDMC.dmc_set_s_profile(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, 1, dS_para);//设置S段速度参数

                    LTDMC.dmc_vmove(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, 1);
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

        private void FrmFeedRAxias_Enter(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void FrmFeedRAxias_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                picHLim.Image = GlobalVar.lsSecAxiasSensorDIs[(int.Parse(GlobalVar.feedRDiHLimP.Split(',')[1]))].ToString() == "0" ? KEE.Renault.Properties.Resources.RedLed : KEE.Renault.Properties.Resources.GrayLed;
                //Application.DoEvents();
            }));
        }

        private void chkBoxAxiasEnable_CheckedChanged(object sender, EventArgs e)
        {
            LTDMC.dmc_write_sevon_pin(GlobalVar.CardId, GlobalVar.FeedRAxiasNumber, (ushort)(chkBoxAxiasEnable.Checked ? 0 : 1));
        }
        #endregion

        #region 其他方法

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
            cmbStopModel.SelectedItem = GlobalVar.feedRStopModel;
            cmbCoordModel.SelectedItem = GlobalVar.feedRCoordModel;
            txtBoxStartSpeed.Text = GlobalVar.feedRStartSpeed.ToString();
            txtBoxNormalSpeed.Text = GlobalVar.feedRMotionSpeed.ToString();
            txtBoxStopSpeed.Text = GlobalVar.feedRStopSpeed.ToString();
            txtBoxAccTime.Text = GlobalVar.feedRAccTime.ToString();
            txtBoxDccTime.Text = GlobalVar.feedRDccTime.ToString();
            txtBoxSTime.Text = GlobalVar.feedRSTime.ToString();
            txtBoxHomeLowSpeed.Text = GlobalVar.feedRHomeLowSpeed.ToString();
            txtBoxHomeHighSpeed.Text = GlobalVar.feedRHomeHighSpeed.ToString();
            cmbHomeModel.SelectedItem = GlobalVar.feedRHomeModel;
            cmbHomeSpeedModel.SelectedItem = GlobalVar.feedRHomeSpeedModel;
            txtBoxRAxiasTakeDeg.Text = GlobalVar.feedRTakeProPos.ToString();
            //txtBoxRAxiasFeedDeg.Text = GlobalVar.feedRProPos.ToString();
            cmbRAxiasHomeP.SelectedItem = GlobalVar.feedRDiHLimP;
            chkBoxAxiasEnable.Checked = (LTDMC.dmc_read_sevon_pin(GlobalVar.CardId, GlobalVar.FeedAxiasNumber) == 0);
        }

        #endregion

       
    }
}
