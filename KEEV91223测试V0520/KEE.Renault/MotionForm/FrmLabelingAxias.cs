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
    public partial class FrmLabelingAxias : Form
    {
        public FrmLabelingAxias()
        {
            InitializeComponent();
            cmbCoordModel.DataSource = LSParamCommonInit.coordModels;
            cmbStopModel.DataSource = LSParamCommonInit.stopModels;
            InitAllPictures();
            InitCfgUIData();
        }

        #region 变量
        List<PictureBox> picList = new List<PictureBox>();
        CancellationTokenSource tokenSource = null;
        bool isTimeOut = true;
        bool isLabelOut = false;//下降沿信号
        #endregion 

        #region 窗体事件
        private void FrmLabelingAxias_Enter(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void FrmLabelingAxias_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                TraversalAllPic();
                // Application.DoEvents();
            }));
        }

        private void btnSaveModel_Click(object sender, EventArgs e)
        {
            if (!GlobalVar.totalRunFlag)
            {
                if (JudgeComboBoxIsNullOrSpace(cmbStopModel)) return;
                if (JudgeComboBoxIsNullOrSpace(cmbCoordModel)) return;
                GlobalVar.labelStopModel = (StopModelEnum)cmbStopModel.SelectedItem;
                GlobalVar.labelCoordModel = (CoordModelEnum)cmbStopModel.SelectedItem;
                INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡出标轴参数配置", "停止模式", GlobalVar.labelStopModel.ToString());
                INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡出标轴参数配置", "坐标模式", GlobalVar.labelCoordModel.ToString());
            }
            else
            { MessageBox.Show("设备正在全自动模式，禁止操作"); }
        }

        private void btnClearPluse_Click(object sender, EventArgs e)
        {
            if (!GlobalVar.totalRunFlag)
            {
                LTDMC.dmc_set_position(GlobalVar.CardId, GlobalVar.LabelingAxiasNumber, 0);
            }
            else
            { MessageBox.Show("设备正在全自动模式，禁止操作"); }
           
        }

        private void btnSavePara_Click(object sender, EventArgs e)
        {
            if (JudgeTextBoxIsNullOrSpace(txtBoxStartSpeed)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxNormalSpeed)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxStopSpeed)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxAccTime)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxDccTime)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxSTime)) return;
            GlobalVar.labelStartSpeed = Convert.ToDouble(txtBoxStartSpeed.Text);
            GlobalVar.labelMotionSpeed = Convert.ToDouble(txtBoxNormalSpeed.Text);
            GlobalVar.labelStopSpeed = Convert.ToDouble(txtBoxStopSpeed.Text);
            GlobalVar.labelAccTime = Convert.ToDouble(txtBoxAccTime.Text);
            GlobalVar.labelDccTime = Convert.ToDouble(txtBoxDccTime.Text);
            GlobalVar.labelSTime = Convert.ToDouble(txtBoxSTime.Text);
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡出标轴参数配置", "起始速度", GlobalVar.labelStartSpeed.ToString());
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡出标轴参数配置", "正常运行速度", GlobalVar.labelMotionSpeed.ToString());
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡出标轴参数配置", "停止速度", GlobalVar.labelStopSpeed.ToString());
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡出标轴参数配置", "加速时间", GlobalVar.labelAccTime.ToString());
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡出标轴参数配置", "减速时间", GlobalVar.labelDccTime.ToString());
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡出标轴参数配置", "S段时间", GlobalVar.labelSTime.ToString());
        }

        private void btnSaveDiVal_Click(object sender, EventArgs e)
        {
            if (JudgeComboBoxIsNullOrSpace(cmbLabelCylinderLowLim)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbLabelCylinderHighLim)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbLabelCylinderLeftLim)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbLabelCylinderRightLim)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbLabingOutP)) return;
            GlobalVar.labelCylinderLowLim = cmbLabelCylinderLowLim.SelectedItem.ToString();
            GlobalVar.labelCylinderHighLim = cmbLabelCylinderHighLim.SelectedItem.ToString();
            GlobalVar.labelCylinderLeftLim = cmbLabelCylinderLeftLim.SelectedItem.ToString();
            GlobalVar.labelCylinderRightLim = cmbLabelCylinderRightLim.SelectedItem.ToString();
            GlobalVar.labelOutMarkSensor = cmbLabingOutP.SelectedItem.ToString();
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输入信号配置", "出标轴下压气缸上极限", GlobalVar.labelCylinderHighLim.ToString());
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输入信号配置", "出标轴下压气缸下极限", GlobalVar.labelCylinderLowLim.ToString());
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输入信号配置", "出标轴出标气缸左极限", GlobalVar.labelCylinderLeftLim.ToString());
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输入信号配置", "出标轴出标气缸右极限", GlobalVar.labelCylinderRightLim.ToString());
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输入信号配置", "出标感应器", GlobalVar.labelOutMarkSensor.ToString());
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            LTDMC.dmc_stop(GlobalVar.CardId, GlobalVar.LabelingAxiasNumber, (ushort)(StopModelEnum)cmbStopModel.SelectedItem);
        }

        private void btnFixLenMotion_Click(object sender, EventArgs e)
        {
            if (!GlobalVar.totalRunFlag)
            {
                if (GlobalVar.lsAxiasDIs[16].CurIOStatus && !GlobalVar.lsAxiasDIs[17].CurIOStatus)
                {
                    double dStartVel;//起始速度
                    double dMaxVel;//运行速度
                    double dTacc;//加速时间
                    double dTdec;//减速时间
                    double dStopVel;//停止速度
                    double dS_para;//S段时间
                    int disP;//目标位置
                    isTimeOut = true;//默认超时
                    if (!int.TryParse(txtBoxFixLength.Text, out disP)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxStartSpeed.Text, out dStartVel)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxNormalSpeed.Text, out dMaxVel)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxAccTime.Text, out dTacc)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxDccTime.Text, out dTdec)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxStopSpeed.Text, out dStopVel)) { MessageBox.Show("请输入有效值"); }
                    if (!double.TryParse(txtBoxSTime.Text, out dS_para)) { MessageBox.Show("请输入有效值"); }
                    LTDMC.dmc_set_profile(GlobalVar.CardId, GlobalVar.LabelingAxiasNumber, dStartVel, dMaxVel, dTacc, dTdec, dStopVel);  //设置速度参数
                    LTDMC.dmc_set_s_profile(GlobalVar.CardId, GlobalVar.LabelingAxiasNumber, 0, dS_para);//设置S段速度参数
                    LTDMC.dmc_vmove(GlobalVar.CardId, GlobalVar.LabelingAxiasNumber, 0);
                    Thread.Sleep(500);
                    tokenSource = new CancellationTokenSource(20000);
                    tokenSource.Token.Register(new Action(() => { LTDMC.dmc_stop(GlobalVar.CardId, GlobalVar.LabelingAxiasNumber, 0); }));
                    Task.Factory.StartNew(new Action(() =>
                    {
                        while (!tokenSource.Token.IsCancellationRequested)
                        {
                            Application.DoEvents();
                            if (GlobalVar.lsAxiasDIs[40].CurIOStatus && !isLabelOut)
                            {
                                isLabelOut = true;
                                LTDMC.dmc_vmove(GlobalVar.CardId, GlobalVar.LabelingAxiasNumber, 0);
                            }
                            if (!GlobalVar.lsAxiasDIs[40].CurIOStatus && isLabelOut)
                            {
                                isLabelOut = false;
                                isTimeOut = false; tokenSource.Cancel();
                            }

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
                LTDMC.dmc_write_sevon_pin(GlobalVar.CardId, GlobalVar.LabelingAxiasNumber, (ushort)(chkBoxAxiasEnable.Checked ? 0 : 1));
            }
            else
            { MessageBox.Show("设备正在全自动模式，禁止操作"); }
        }

        private void btnBackRun_MouseUp(object sender, MouseEventArgs e)
        {
            LTDMC.dmc_stop(GlobalVar.CardId, GlobalVar.LabelingAxiasNumber, Convert.ToUInt16((StopModelEnum)cmbStopModel.SelectedItem));
        }

        private void btnBackRun_MouseDown(object sender, MouseEventArgs e)
        {
            if (!GlobalVar.lsAxiasDIs[44].CurIOStatus)
            {
                MessageBox.Show("急停信号按下，禁止操作");
            }
            if (!GlobalVar.totalRunFlag)
            {
                if (GlobalVar.lsAxiasDIs[16].CurIOStatus && !GlobalVar.lsAxiasDIs[17].CurIOStatus)
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
                    LTDMC.dmc_set_profile(GlobalVar.CardId, GlobalVar.LabelingAxiasNumber, dStartVel, dMaxVel, dTacc, dTdec, dStopVel);//设置速度参数

                    LTDMC.dmc_set_s_profile(GlobalVar.CardId, GlobalVar.LabelingAxiasNumber, 0, dS_para);//设置S段速度参数

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

        private void btnForwardRun_MouseUp(object sender, MouseEventArgs e)
        {
            LTDMC.dmc_stop(GlobalVar.CardId, GlobalVar.LabelingAxiasNumber, Convert.ToUInt16((StopModelEnum)cmbStopModel.SelectedItem));
        }

        private void btnForwardRun_MouseDown(object sender, MouseEventArgs e)
        {
            if (!GlobalVar.lsAxiasDIs[44].CurIOStatus)
            {
                MessageBox.Show("急停信号按下，禁止操作");
            }
            if (!GlobalVar.totalRunFlag)
            {
                if (GlobalVar.lsAxiasDIs[16].CurIOStatus && !GlobalVar.lsAxiasDIs[17].CurIOStatus)
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
                    LTDMC.dmc_set_profile(GlobalVar.CardId, GlobalVar.LabelingAxiasNumber, dStartVel, dMaxVel, dTacc, dTdec, dStopVel);//设置速度参数

                    LTDMC.dmc_set_s_profile(GlobalVar.CardId, GlobalVar.LabelingAxiasNumber, 0, dS_para);//设置S段速度参数

                    LTDMC.dmc_vmove(GlobalVar.CardId, GlobalVar.LabelingAxiasNumber, 1);
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

        private void btnSaveDOVal_Click(object sender, EventArgs e)
        {
            if (JudgeComboBoxIsNullOrSpace(cmbLabelPressCylinderElecValve)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbLabelOutCylinderElecValve)) return;
            GlobalVar.labelPressCylinderElecValve = cmbLabelPressCylinderElecValve.SelectedItem.ToString();
            GlobalVar.labelOutCylinderElecValve = cmbLabelOutCylinderElecValve.SelectedItem.ToString();
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输出信号配置", "出标轴下压气缸电磁阀", GlobalVar.labelPressCylinderElecValve.ToString());
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输出信号配置", "出标轴出标气缸电磁阀", GlobalVar.labelOutCylinderElecValve.ToString());
        }

        private void btnLabelPressCylinder_Click(object sender, EventArgs e)
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
            { MessageBox.Show("设备正在全自动模式，禁止操作"); }
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
            { MessageBox.Show("设备正在全自动模式，禁止操作"); }
        }

        private void btnSaveFixLength_Click(object sender, EventArgs e)
        {
            if (JudgeTextBoxIsNullOrSpace(txtBoxFixLength)) return;
            GlobalVar.labelFixedStepLength = Convert.ToInt32(txtBoxFixLength.Text);
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "轴卡出标轴参数配置", "定长距离", GlobalVar.labelFixedStepLength.ToString());
        }
        #endregion

        #region 其他方法

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
            cmbStopModel.SelectedItem = GlobalVar.labelStopModel;
            cmbCoordModel.SelectedItem = GlobalVar.labelCoordModel;
            txtBoxStartSpeed.Text = GlobalVar.labelStartSpeed.ToString();
            txtBoxNormalSpeed.Text = GlobalVar.labelMotionSpeed.ToString();
            txtBoxStopSpeed.Text = GlobalVar.labelStopSpeed.ToString();
            txtBoxAccTime.Text = GlobalVar.labelAccTime.ToString();
            txtBoxDccTime.Text = GlobalVar.labelDccTime.ToString();
            txtBoxSTime.Text = GlobalVar.labelSTime.ToString();
            cmbLabelCylinderLowLim.SelectedItem = GlobalVar.labelCylinderLowLim;
            cmbLabelCylinderHighLim.SelectedItem = GlobalVar.labelCylinderHighLim;
            cmbLabelCylinderLeftLim.SelectedItem = GlobalVar.labelCylinderLeftLim;
            cmbLabelCylinderRightLim.SelectedItem = GlobalVar.labelCylinderRightLim;
            cmbLabingOutP.SelectedItem = GlobalVar.labelOutMarkSensor;
            txtBoxFixLength.Text = GlobalVar.labelFixedStepLength.ToString();
            cmbLabelPressCylinderElecValve.SelectedItem = GlobalVar.labelPressCylinderElecValve.ToString();
            cmbLabelOutCylinderElecValve.SelectedItem = GlobalVar.labelOutCylinderElecValve.ToString();
            chkBoxAxiasEnable.Checked = (LTDMC.dmc_read_sevon_pin(GlobalVar.CardId, GlobalVar.LabelingAxiasNumber) == 0);
        }

        #endregion


    }
}
