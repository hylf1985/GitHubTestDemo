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
    public partial class FrmTakeAxias : Form
    {
        public FrmTakeAxias()
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
            cmbStopModel.SelectedItem =      GlobalVar.takeStopModel;
            cmbCoordModel.SelectedItem =     GlobalVar.takeCoordModel;
            txtBoxStartSpeed.Text =          GlobalVar.takeStartSpeed.ToString();
            txtBoxNormalSpeed.Text =         GlobalVar.takeMotionSpeed.ToString();
            txtBoxStopSpeed.Text =           GlobalVar.takeStopSpeed.ToString();
            txtBoxAccTime.Text =             GlobalVar.takeAccTime.ToString();
            txtBoxDccTime.Text =             GlobalVar.takeDccTime.ToString();
            txtBoxSTime.Text =               GlobalVar.takeSTime.ToString();
            txtBoxHomeLowSpeed.Text =        GlobalVar.takeHomeLowSpeed.ToString();
            txtBoxHomeHighSpeed.Text =       GlobalVar.takeHomeHighSpeed.ToString();
            cmbHomeModel.SelectedItem =      GlobalVar.takeHomeModel;
            cmbHomeSpeedModel.SelectedItem = GlobalVar.takeHomeSpeedModel;
            txtBoxFeedPro.Text = GlobalVar.takeFeedProPos.ToString();
            txtBoxTakeNGPro.Text = GlobalVar.takeNGProPos.ToString();
            txtBoxTakeOKPro.Text = GlobalVar.takeOKProPos.ToString();
            cmbPLimP.SelectedItem = GlobalVar.takeDiPLimP;
            cmbNLimP.SelectedItem = GlobalVar.takeDiNLimP;
            cmbHomeP.SelectedItem = GlobalVar.takeDiHLimP;
            cmbTakeCylinderHighLim.SelectedItem = GlobalVar.takeCylinerHighLim;
            cmbTakeCylinderLowLim.SelectedItem =  GlobalVar.takeCylinerLowLim;
            cmbTakeVacuumLim.SelectedItem =       GlobalVar.takeVacuumLim;
            cmbTakeCylinderElecValve.SelectedItem = GlobalVar.takeCylinderElecValve;
            cmbTakeVacuumSolenoid.SelectedItem = GlobalVar.takeVacuumSolenoid;
            chkBoxAxiasEnable.Checked = (LTDMC.dmc_read_sevon_pin(GlobalVar.CardId, GlobalVar.TakeAxiasNumber) == 0);
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

        #region 窗体事件
        private void btnSaveModel_Click(object sender, EventArgs e)
        {
            if (JudgeComboBoxIsNullOrSpace(cmbStopModel)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbCoordModel)) return;
            GlobalVar.takeStopModel = (StopModelEnum)cmbStopModel.SelectedItem;
            GlobalVar.takeCoordModel = (CoordModelEnum)cmbStopModel.SelectedItem;
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡丢料轴参数配置", "停止模式", GlobalVar.takeStopModel.ToString());
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡丢料轴参数配置", "坐标模式", GlobalVar.takeCoordModel.ToString());
        }

        private void btnSavePara_Click(object sender, EventArgs e)
        {
            if (JudgeTextBoxIsNullOrSpace(txtBoxStartSpeed)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxNormalSpeed)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxStopSpeed)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxAccTime)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxDccTime)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxSTime)) return;
            GlobalVar.takeStartSpeed = Convert.ToDouble(txtBoxStartSpeed.Text);
            GlobalVar.takeMotionSpeed = Convert.ToDouble(txtBoxNormalSpeed.Text);
            GlobalVar.takeStopSpeed = Convert.ToDouble(txtBoxStopSpeed.Text);
            GlobalVar.takeAccTime = Convert.ToDouble(txtBoxAccTime.Text);
            GlobalVar.takeDccTime = Convert.ToDouble(txtBoxDccTime.Text);
            GlobalVar.takeSTime = Convert.ToDouble(txtBoxSTime.Text);
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡丢料轴参数配置", "起始速度", GlobalVar.takeStartSpeed.ToString());
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡丢料轴参数配置", "正常运行速度", GlobalVar.takeMotionSpeed.ToString());
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡丢料轴参数配置", "停止速度", GlobalVar.takeStopSpeed.ToString());
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡丢料轴参数配置", "加速时间", GlobalVar.takeAccTime.ToString());
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡丢料轴参数配置", "减速时间", GlobalVar.takeDccTime.ToString());
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡丢料轴参数配置", "S段时间", GlobalVar.takeSTime.ToString());
        }

        private void btnSaveDiVal_Click(object sender, EventArgs e)
        {
            if (JudgeComboBoxIsNullOrSpace(cmbPLimP)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbNLimP)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbHomeP)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbTakeCylinderHighLim)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbTakeCylinderLowLim)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbTakeVacuumLim)) return;
            GlobalVar.takeDiPLimP = cmbPLimP.SelectedItem.ToString();
            GlobalVar.takeDiNLimP = cmbNLimP.SelectedItem.ToString();
            GlobalVar.takeDiHLimP = cmbHomeP.SelectedItem.ToString();
            GlobalVar.takeCylinerHighLim = cmbTakeCylinderHighLim.SelectedItem.ToString();
            GlobalVar.takeCylinerLowLim = cmbTakeCylinderLowLim.SelectedItem.ToString();
            GlobalVar.takeVacuumLim = cmbTakeVacuumLim.SelectedItem.ToString();
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "轴卡输入信号配置", "丢料轴正极限", GlobalVar.takeDiPLimP.ToString());
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "轴卡输入信号配置", "丢料轴负极限", GlobalVar.takeDiNLimP.ToString());
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "轴卡输入信号配置", "丢料轴原点", GlobalVar.takeDiHLimP.ToString());
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输入信号配置", "下料气缸上极限   ", GlobalVar.takeCylinerHighLim.ToString());
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输入信号配置", "下料气缸下极限", GlobalVar.takeCylinerLowLim.ToString());
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输入信号配置", "下料真空负压表阈值", GlobalVar.takeVacuumLim.ToString());
        }

        private void btnSaveDoVal_Click(object sender, EventArgs e)
        {
            if (JudgeComboBoxIsNullOrSpace(cmbTakeCylinderElecValve)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbTakeVacuumSolenoid)) return;
            GlobalVar.takeCylinderElecValve = cmbTakeCylinderElecValve.SelectedItem.ToString();
            GlobalVar.takeVacuumSolenoid =    cmbTakeVacuumSolenoid.SelectedItem.ToString();
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输出信号配置", "下料气缸电磁阀", GlobalVar.takeCylinderElecValve.ToString());
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输出信号配置", "下料真空电磁阀", GlobalVar.takeVacuumSolenoid.ToString());
        }

        private void btnSaveHomePara_Click(object sender, EventArgs e)
        {
            if (JudgeComboBoxIsNullOrSpace(cmbHomeModel)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbHomeSpeedModel)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxHomeLowSpeed)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxHomeHighSpeed)) return;
            GlobalVar.takeHomeLowSpeed = Convert.ToDouble(txtBoxHomeLowSpeed.Text);
            GlobalVar.takeHomeHighSpeed = Convert.ToDouble(txtBoxHomeHighSpeed.Text);
            GlobalVar.takeHomeModel = (HomeModel)Convert.ToDouble(cmbHomeModel.SelectedItem);
            GlobalVar.takeHomeSpeedModel = (HomeSpeedModel)Convert.ToDouble(cmbHomeSpeedModel.SelectedItem);
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡丢料轴参数配置", "回零低速",     GlobalVar.takeHomeLowSpeed.ToString());
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡丢料轴参数配置", "回零高速",     GlobalVar.takeHomeHighSpeed.ToString());
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡丢料轴参数配置", "回零模式",     GlobalVar.takeHomeModel.ToString());
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡丢料轴参数配置", "回零速度模式", GlobalVar.takeHomeSpeedModel.ToString());
        }

        private void btnSaveFinishedProPos_Click(object sender, EventArgs e)
        {
            if (JudgeTextBoxIsNullOrSpace(txtBoxFeedPro)) return;
            GlobalVar.takeFeedProPos = Convert.ToInt32(txtBoxFeedPro.Text);
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡丢料轴参数配置", "取料位置", GlobalVar.takeFeedProPos.ToString());
        }

        private void btnSaveNGPos_Click(object sender, EventArgs e)
        {
            if (JudgeTextBoxIsNullOrSpace(txtBoxTakeNGPro)) return;
            GlobalVar.takeNGProPos = Convert.ToInt32(txtBoxTakeNGPro.Text);
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡丢料轴参数配置", "放料NG位置", GlobalVar.takeNGProPos.ToString());
        }

        private void btnSaveOKPos_Click(object sender, EventArgs e)
        {
            if (JudgeTextBoxIsNullOrSpace(txtBoxTakeOKPro)) return;
            GlobalVar.takeOKProPos = Convert.ToInt32(txtBoxTakeOKPro.Text);
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡丢料轴参数配置", "放料OK位置", GlobalVar.takeOKProPos.ToString());
        }

        private void btnCylinderElecValve_Click(object sender, EventArgs e)
        {
            if (!GlobalVar.totalRunFlag)
            {
                if (LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.TakeAxiasNumber) == 1)
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

        private void btnVacSorb_Click(object sender, EventArgs e)
        {
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

        private void btnHome_Click(object sender, EventArgs e)
        {
            if (!GlobalVar.lsAxiasDIs[44].CurIOStatus)
            {
                MessageBox.Show("急停信号按下，禁止操作");
            }
            if (!GlobalVar.totalRunFlag)
            {
                if (GlobalVar.lsAxiasDIs[24].CurIOStatus && !GlobalVar.lsAxiasDIs[25].CurIOStatus)
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
                    LTDMC.dmc_set_profile(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, dStartVel, dMaxVel, dTacc, dTdec, dStopVel);  //设置速度参数
                    LTDMC.dmc_set_s_profile(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, 0, dS_para);//设置S段速度参数
                    LTDMC.dmc_set_homemode(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, 0, (ushort)(HomeSpeedModel)cmbHomeSpeedModel.SelectedItem, (ushort)(HomeModel)cmbHomeModel.SelectedItem, 0);
                    LTDMC.dmc_set_home_position(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, 2, 0);
                    LTDMC.dmc_home_move(GlobalVar.CardId, GlobalVar.TakeAxiasNumber);
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
            LTDMC.dmc_stop(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, (ushort)(StopModelEnum)cmbStopModel.SelectedItem);
        }

        private void btnFeedPro_Click(object sender, EventArgs e)
        {
            if (!GlobalVar.totalRunFlag)
            {
                if (GlobalVar.lsAxiasDIs[24].CurIOStatus && !GlobalVar.lsAxiasDIs[25].CurIOStatus)
                {
                    double dStartVel;//起始速度
                    double dMaxVel;//运行速度
                    double dTacc;//加速时间
                    double dTdec;//减速时间
                    double dStopVel;//停止速度
                    double dS_para;//S段时间
                    int disP;//目标位置
                    isTimeOut = true;//默认超时
                    if (!int.TryParse(txtBoxFeedPro.Text, out disP)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxStartSpeed.Text, out dStartVel)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxNormalSpeed.Text, out dMaxVel)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxAccTime.Text, out dTacc)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxDccTime.Text, out dTdec)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxStopSpeed.Text, out dStopVel)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxSTime.Text, out dS_para)) { MessageBox.Show("请输入有效值"); }
                    LTDMC.dmc_set_profile(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, dStartVel, dMaxVel, dTacc, dTdec, dStopVel);  //设置速度参数
                    LTDMC.dmc_set_s_profile(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, 0, dS_para);//设置S段速度参数
                    tokenSource = new CancellationTokenSource(20000);
                    tokenSource.Token.Register(new Action(() => { DialogResult = isTimeOut ? MessageBox.Show("电机走动超时") : MessageBox.Show("指定位置到达"); }));
                    LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, disP, (ushort)CoordModelEnum.绝对坐标);
                    Task.Factory.StartNew(new Action(() =>
                    {
                        while (!tokenSource.Token.IsCancellationRequested && LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.TakeAxiasNumber) == 0)
                        {
                            Thread.Sleep(5);
                            Application.DoEvents();
                            if (LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.TakeAxiasNumber) == 1)
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

        private void btnTakeNGPro_Click(object sender, EventArgs e)
        {
            if (!GlobalVar.totalRunFlag)
            {
                if (GlobalVar.lsAxiasDIs[24].CurIOStatus && !GlobalVar.lsAxiasDIs[25].CurIOStatus)
                {
                    double dStartVel;//起始速度
                    double dMaxVel;//运行速度
                    double dTacc;//加速时间
                    double dTdec;//减速时间
                    double dStopVel;//停止速度
                    double dS_para;//S段时间
                    int disP;//目标位置
                    isTimeOut = true;//默认超时
                    if (!int.TryParse(txtBoxTakeNGPro.Text, out disP)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxStartSpeed.Text, out dStartVel)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxNormalSpeed.Text, out dMaxVel)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxAccTime.Text, out dTacc)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxDccTime.Text, out dTdec)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxStopSpeed.Text, out dStopVel)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxSTime.Text, out dS_para)) { MessageBox.Show("请输入有效值"); }
                    LTDMC.dmc_set_profile(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, dStartVel, dMaxVel, dTacc, dTdec, dStopVel);  //设置速度参数
                    LTDMC.dmc_set_s_profile(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, 0, dS_para);//设置S段速度参数
                    tokenSource = new CancellationTokenSource(20000);
                    tokenSource.Token.Register(new Action(() => { DialogResult = isTimeOut ? MessageBox.Show("电机走动超时") : MessageBox.Show("指定位置到达"); }));
                    LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, disP, (ushort)CoordModelEnum.绝对坐标);
                    Task.Factory.StartNew(new Action(() =>
                    {
                        while (!tokenSource.Token.IsCancellationRequested && LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.TakeAxiasNumber) == 0)
                        {
                            Thread.Sleep(5);
                            Application.DoEvents();
                            if (LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.TakeAxiasNumber) == 1)
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

        private void btnTakeOKPro_Click(object sender, EventArgs e)
        {
            if (!GlobalVar.totalRunFlag)
            {
                if (GlobalVar.lsAxiasDIs[24].CurIOStatus && !GlobalVar.lsAxiasDIs[25].CurIOStatus)
                {
                    double dStartVel;//起始速度
                    double dMaxVel;//运行速度
                    double dTacc;//加速时间
                    double dTdec;//减速时间
                    double dStopVel;//停止速度
                    double dS_para;//S段时间
                    int disP;//目标位置
                    isTimeOut = true;//默认超时
                    if (!int.TryParse(txtBoxTakeOKPro.Text, out disP)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxStartSpeed.Text, out dStartVel)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxNormalSpeed.Text, out dMaxVel)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxAccTime.Text, out dTacc)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxDccTime.Text, out dTdec)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxStopSpeed.Text, out dStopVel)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxSTime.Text, out dS_para)) { MessageBox.Show("请输入有效值"); }
                    LTDMC.dmc_set_profile(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, dStartVel, dMaxVel, dTacc, dTdec, dStopVel);  //设置速度参数
                    LTDMC.dmc_set_s_profile(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, 0, dS_para);//设置S段速度参数
                    tokenSource = new CancellationTokenSource(20000);
                    tokenSource.Token.Register(new Action(() => { DialogResult = isTimeOut ? MessageBox.Show("电机走动超时") : MessageBox.Show("指定位置到达"); }));
                    LTDMC.dmc_pmove(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, disP, (ushort)CoordModelEnum.绝对坐标);
                    Task.Factory.StartNew(new Action(() =>
                    {
                        while (!tokenSource.Token.IsCancellationRequested && LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.TakeAxiasNumber) == 0)
                        {
                            Thread.Sleep(5);
                            Application.DoEvents();
                            if (LTDMC.dmc_check_done(GlobalVar.CardId, GlobalVar.TakeAxiasNumber) == 1)
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
            if (!GlobalVar.totalRunFlag)
            {
                LTDMC.dmc_write_sevon_pin(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, (ushort)(chkBoxAxiasEnable.Checked ? 0 : 1));
            }
            else
            { MessageBox.Show("设备正在全自动模式，禁止操作"); }
        }

        private void btnForwardRun_MouseUp(object sender, MouseEventArgs e)
        {
            LTDMC.dmc_stop(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, (ushort)(StopModelEnum)cmbStopModel.SelectedItem);
        }

        private void btnForwardRun_MouseDown(object sender, MouseEventArgs e)
        {
            if (!GlobalVar.lsAxiasDIs[44].CurIOStatus)
            {
                MessageBox.Show("急停信号按下，禁止操作");
            }
            if (!GlobalVar.totalRunFlag)
            {
                if (GlobalVar.lsAxiasDIs[24].CurIOStatus && !GlobalVar.lsAxiasDIs[25].CurIOStatus)
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
                    LTDMC.dmc_set_profile(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, dStartVel, dMaxVel, dTacc, dTdec, dStopVel);//设置速度参数

                    LTDMC.dmc_set_s_profile(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, 0, dS_para);//设置S段速度参数

                    LTDMC.dmc_vmove(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, 0);
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
            LTDMC.dmc_stop(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, (ushort)(StopModelEnum)cmbStopModel.SelectedItem);
        }

        private void btnBackRun_MouseDown(object sender, MouseEventArgs e)
        {
            if (!GlobalVar.lsAxiasDIs[44].CurIOStatus)
            {
                MessageBox.Show("急停信号按下，禁止操作");
            }
            if (!GlobalVar.totalRunFlag)
            {
                if (GlobalVar.lsAxiasDIs[24].CurIOStatus && !GlobalVar.lsAxiasDIs[25].CurIOStatus)
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
                    LTDMC.dmc_set_profile(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, dStartVel, dMaxVel, dTacc, dTdec, dStopVel);//设置速度参数

                    LTDMC.dmc_set_s_profile(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, 0, dS_para);//设置S段速度参数

                    LTDMC.dmc_vmove(GlobalVar.CardId, GlobalVar.TakeAxiasNumber, 1);
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
                picPLim.Image = GlobalVar.lsFirAxiasSensorDIs[(int.Parse(GlobalVar.takeDiPLimP.Split(',')[1]))].ToString() == "0" ? KEE.Renault.Properties.Resources.RedLed : KEE.Renault.Properties.Resources.GrayLed;
                picNLim.Image = GlobalVar.lsFirAxiasSensorDIs[(int.Parse(GlobalVar.takeDiNLimP.Split(',')[1]))].ToString() == "0" ? KEE.Renault.Properties.Resources.RedLed : KEE.Renault.Properties.Resources.GrayLed;
                picHLim.Image = GlobalVar.lsSecAxiasSensorDIs[(int.Parse(GlobalVar.takeDiHLimP.Split(',')[1]))].ToString() == "0" ? KEE.Renault.Properties.Resources.RedLed : KEE.Renault.Properties.Resources.GrayLed;
                //Application.DoEvents();
            }));
        }

        private void FrmTakeAxias_Enter(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void FrmTakeAxias_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }
        #endregion 
    }
}
