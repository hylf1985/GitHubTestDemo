using ClassINI;
using csIOC0640;
using csLTDMC;
using KEE.Renault.Common;
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
    public partial class FrmSplitter : Form
    {
        public FrmSplitter()
        {
            InitializeComponent();
            InitCfgUIData();
            InitAllPictures();
        }

        #region 变量
        List<PictureBox> picList = new List<PictureBox>();
        double realV = 0;
        
        CancellationTokenSource tokenSource = new CancellationTokenSource();
        #endregion 

        #region 窗体事件
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                TraversalAllPic();
                try
                {
                    this.Invoke(new Action(() => {
                        ushort[] res = HighTest.ModbusSerialRtuMasterReadRegisters(GlobalVar.highIndCom);
                        if (res.Length == 2)
                        {
                            if (res[1] != 65535)
                            {
                                realV = res[0] / 10000d;
                            }
                            else
                            {
                                realV = (res[0] - 65535) / 10000d;
                            }
                            txtBoxRealV.Text = realV.ToString();
                        }
                    }));
                }
                catch (Exception ex)
                {
                    GlobalVar.myLog.Error($"测高产生异常错误：{ex.Message}");
                }
                //Application.DoEvents();
            }));
        }

        private void FrmSplitter_Enter(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void FrmSplitter_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void btnSaveHighPara_Click(object sender, EventArgs e)
        {
            if (JudgeComboBoxIsNullOrSpace(cmbCom)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxInitHighV)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxSpaceHigh)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxDelay)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxMaxV)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxMinV)) return;
            GlobalVar.highIndCom = cmbCom.SelectedItem.ToString();
            double a, b, c, d, f = 0;
            if (!double.TryParse(txtBoxInitHighV.Text.Trim(), out a)) { MessageBox.Show("请输入有效数值"); return; }
            if (!double.TryParse(txtBoxSpaceHigh.Text.Trim(), out b)) { MessageBox.Show("请输入有效数值"); return; }
            if (!double.TryParse(txtBoxDelay.Text.Trim(), out c)) { MessageBox.Show("请输入有效数值"); return; }
            if (!double.TryParse(txtBoxMaxV.Text.Trim(), out d)) { MessageBox.Show("请输入有效数值"); return; }
            if (!double.TryParse(txtBoxMinV.Text.Trim(), out f)) { MessageBox.Show("请输入有效数值"); return; }
            GlobalVar.highIndInitHighVal = a;
            GlobalVar.highIndSpaceHigh = b;
            GlobalVar.highIndInitDelayTime = c;
            GlobalVar.highIndMaxVal = d;
            GlobalVar.highIndMinVal = f;
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "测高模组", "串口号", cmbCom.SelectedItem.ToString());
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "测高模组", "初始值", a.ToString());
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "测高模组", "延迟时间", c.ToString());
            switch (GlobalVar.curProName)
            {
                case "Renault":
                    INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "测高模组", "空载具高度", b.ToString());
                    INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "测高模组", "模组最大值", d.ToString());
                    INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "测高模组", "模组最小值", f.ToString());
                    break;
                case "Lada":
                    INI.INIWriteValue(GlobalVar.bRecipeLadaFilePath, "测高模组", "空载具高度", b.ToString());
                    INI.INIWriteValue(GlobalVar.bRecipeLadaFilePath, "测高模组", "模组最大值", d.ToString());
                    INI.INIWriteValue(GlobalVar.bRecipeLadaFilePath, "测高模组", "模组最小值", f.ToString());
                    break;
            }

        }

        private void btnSaveIndDiP_Click(object sender, EventArgs e)
        {
            if (JudgeComboBoxIsNullOrSpace(cmbLowLim)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbHighLim)) return;
            GlobalVar.highTestCylinderLowLim = cmbLowLim.SelectedItem.ToString();
            GlobalVar.highTestCylinderHighLim = cmbHighLim.SelectedItem.ToString();
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输入信号配置", "测高气缸上极限", GlobalVar.highTestCylinderHighLim.ToString());
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输入信号配置", "测高气缸下极限", GlobalVar.highTestCylinderLowLim.ToString());

        }

        private void btnSaveIndDoP_Click(object sender, EventArgs e)
        {
            if (JudgeComboBoxIsNullOrSpace(cmbCylinder)) return;
            GlobalVar.highTestCylinderElecValve = cmbCylinder.SelectedItem.ToString();
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输出信号配置", "测高气缸电磁阀", GlobalVar.highTestCylinderElecValve.ToString());
        }

        private void btnSavePressDiV_Click(object sender, EventArgs e)
        {
            if (JudgeComboBoxIsNullOrSpace(cmbPressLowLim)) return;
            if (JudgeComboBoxIsNullOrSpace(cmbPressHighLim)) return;
            GlobalVar.pushCylinerLowLim = cmbPressLowLim.SelectedItem.ToString();
            GlobalVar.pushCylinerHighLim = cmbPressHighLim.SelectedItem.ToString();
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输入信号配置", "压合气缸上极限", GlobalVar.pushCylinerHighLim.ToString());
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输入信号配置", "压合气缸下极限", GlobalVar.pushCylinerLowLim.ToString());
        }

        private void btnSavePressDoV_Click(object sender, EventArgs e)
        {
            if (JudgeComboBoxIsNullOrSpace(cmbPressDoV)) return;
            GlobalVar.pushCylinderElecValve = cmbPressDoV.SelectedItem.ToString();
            INI.INIWriteValue(GlobalVar.bDeviceIOFilePath, "雷赛IO卡输出信号配置", "压合气缸电磁阀", GlobalVar.pushCylinderElecValve.ToString());
        }

        private void FrmSplitter_Load(object sender, EventArgs e)
        {
            //TestLeftHightValue();
        }

        private void btnSavePressDelayTime_Click(object sender, EventArgs e)
        {
            if (JudgeTextBoxIsNullOrSpace(txtBoxPressDelayTime)) return;
            int a = 0;
            if (!int.TryParse(txtBoxPressDelayTime.Text.Trim(), out a)) { MessageBox.Show("请输入有效数值"); return; };
            GlobalVar.pressDelayTime = a;
            switch (GlobalVar.curProName)
            {
                case "Renault":
                    INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "保压工位参数", "保压时间", a.ToString());
                    break;
                case "Lada":
                    INI.INIWriteValue(GlobalVar.bRecipeLadaFilePath, "保压工位参数", "保压时间", a.ToString());
                    break;
            }

        }

        private void btnCylinderOut_Click(object sender, EventArgs e)
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

        private void btnPressOut_Click(object sender, EventArgs e)
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
            cmbCom.SelectedItem = GlobalVar.highIndCom;
            txtBoxInitHighV.Text = GlobalVar.highIndInitHighVal.ToString();
            txtBoxSpaceHigh.Text = GlobalVar.highIndSpaceHigh.ToString();
            txtBoxDelay.Text = GlobalVar.highIndInitDelayTime.ToString();
            txtBoxMaxV.Text = GlobalVar.highIndMaxVal.ToString();
            txtBoxMinV.Text = GlobalVar.highIndMinVal.ToString();
            cmbLowLim.SelectedItem = GlobalVar.highTestCylinderLowLim;
            cmbHighLim.SelectedItem = GlobalVar.highTestCylinderHighLim;
            cmbCylinder.SelectedItem = GlobalVar.highTestCylinderElecValve;
            cmbPressLowLim.SelectedItem = GlobalVar.pressCylinderLowLim;
            cmbPressHighLim.SelectedItem = GlobalVar.pressCylinderHighLim;
            cmbPressDoV.SelectedItem = GlobalVar.pressCylinderElecValve;
            txtBoxPressDelayTime.Text = GlobalVar.pressDelayTime.ToString();
        }
        private void InitAllPictures()
        {
            foreach (var pic in groupBox3.Controls)
            {
                if (pic is PictureBox)
                {
                    picList.Add((PictureBox)pic);
                }

            }
            foreach (var pic in groupBox4.Controls)
            {
                if (pic is PictureBox)
                {
                    picList.Add((PictureBox)pic);
                }

            }
            foreach (var pic in panel1.Controls)
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

        private void TestLeftHightValue()
        {
            CancellationToken token = tokenSource.Token;
            var task = Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    if (token.IsCancellationRequested)
                    {
                        return;
                    }
                    try
                    {
                        ushort[] res = HighTest.ModbusSerialRtuMasterReadRegisters(GlobalVar.highIndCom);
                        if (res.Length == 2)
                        {
                            if (res[1] != 65535)
                            {
                                realV = res[0] / 10000d;
                            }
                            else
                            {
                                realV = (res[0] - 65535) / 10000d;
                            }
                            this.Invoke(new Action(() =>
                            {
                                txtBoxRealV.Text = realV.ToString();
                            }));
                        }
                    }
                    catch (Exception ex)
                    {
                        GlobalVar.myLog.Error($"测高页面显示异常错误：{ex.Message}");
                    }
                    Application.DoEvents();
                    await Task.Delay(100);
                }
            });
        }


        #endregion

      
    }
}
