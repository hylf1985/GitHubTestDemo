using ClassINI;
using Cognex.VisionPro;
using KEE.Renault.CamOperator;
using KEE.Renault.LightControl;
using KEE.Renault.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KEE.Renault.MenuForm
{
    public partial class FrmVisionSetting : Form
    {
        public FrmVisionSetting()
        {
            InitializeComponent();
            InitLightCmb();
            InitLightTxtBoxVal();
            InitToolBlockDis();
            InitVisionCfgFile();
        }

        #region 光源控制器变量声明
        delegate void CbDelegate<T1, T2>(T1 obj1, T2 obj2);

        readonly string lightCtrPatternCmd = "#";

        string lightCtrCmd = "1";//默认是打开命令

        //string lightCtrCH = "1";//默认1通道

        string lightCtrData = "064";//默认100亮度值转16进制用3字节

        //string lightCtrXor = "00";//异或2字节

        List<CHEnum> cHs = new List<CHEnum>();

        List<CmdEnum> cmds = new List<CmdEnum>();

        List<WorkStation> stas = new List<WorkStation>();
        #endregion

        #region 初始化Combox组件方法
        private void InitLightCtrCMD()
        {
            cmds.Add(CmdEnum.打开通道);
            cmds.Add(CmdEnum.关闭通道);
            cmds.Add(CmdEnum.设置通道亮度);
            cmds.Add(CmdEnum.读出通道亮度);
        }
        private void InitLightCtrCH()
        {
            cHs.Add(CHEnum.通道1);
            cHs.Add(CHEnum.通道2);
            cHs.Add(CHEnum.通道3);
            cHs.Add(CHEnum.通道4);
        }
        private void InitLightWorkStation()
        {
            stas.Add(WorkStation.未贴背胶电镀件拍照位);
            stas.Add(WorkStation.背胶拍照位);
            stas.Add(WorkStation.贴背胶电镀件拍照位);
            stas.Add(WorkStation.贴电镀件到塑胶件拍照位);
            stas.Add(WorkStation.AOI偏位拍照位);
        }
        private void InitLightCmb()
        {
            InitLightCtrCMD();
            InitLightCtrCH();
            InitLightWorkStation();
            cmbLightCtrCMD.DataSource = cmds;
            cmbLightCtrCH.DataSource = cHs;
            cmbLightSta.DataSource = stas;
            serialPort1.PortName = GlobalVar.lightContrCom;
        }
        private void InitLightTxtBoxVal()
        {
            cmbLightCom.SelectedItem =GlobalVar.lightContrCom.ToUpper();
            txtBoxNoGumCH1Open.Text = GlobalVar.lightNoGumLogoCH1OpenCmd;
            txtBoxNoGumCH1Close.Text = GlobalVar.lightNoGumLogoCH1CloseCmd;
            txtBoxLabeledGumCH1Open.Text = GlobalVar.lightLabeledGumCH1OpenCmd;
            txtBoxLabeledGumCH1Close.Text = GlobalVar.lightLabeledGumCH1CloseCmd;
            txtBoxGumedCH2Open.Text = GlobalVar.lightGumedCH2OpenCmd;
            txtBoxGumedCH2Close.Text = GlobalVar.lightGumedCH2CloseCmd;
            txtBoxLabelLogoCH2Open.Text = GlobalVar.lightLabeledLogoCH2OpenCmd;
            txtBoxLabelLogoCH2Close.Text = GlobalVar.lightLabeledLogoCH2CloseCmd;
            txtBoxAoiOpen.Text = GlobalVar.lightCH3OpenCmd;
            txtBoxAoiClose.Text = GlobalVar.lightCH3CloseCmd;
        }

        #endregion

        #region 光源控制器使用到的进制转换和其他方法
        private string GetXorString(string s, Encoding encode)
        {
            byte[] b = encode.GetBytes(s);//按照指定编码将string编程字节数组
            string result = "";
            int res = 0;
            if (b.Length > 0)
            {
                res = b[0];
                for (int i = 1; i < b.Length; i++)
                {
                    res = res ^ b[i];
                }
                //if (res.ToString().Length<2)
                //{
                //    MessageBox.Show("异或运算错误");
                //    return result;
                //}
                result = res.ToString("x2").ToUpper();
            }
            return result;
        }

        private void SetTextBoxVal(TextBox tb, string val)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new CbDelegate<TextBox, string>(this.SetTextBoxVal), tb, val);
            }
            else
            {
                tb.Text = val;
            }
        }

        private void SetCtrCmdToTextBox()
        {
            //打开命令
            string cmdOpen = CombinCMDStrings("1");
            string cmdClose = CombinCMDStrings("2");
            switch ((CHEnum)cmbLightCtrCH.SelectedValue)
            {
                case CHEnum.通道1:
                    if ((WorkStation)cmbLightSta.SelectedValue == WorkStation.未贴背胶电镀件拍照位 || (WorkStation)cmbLightSta.SelectedValue == WorkStation.贴背胶电镀件拍照位)
                    {
                        SetTextBoxVal((WorkStation)cmbLightSta.SelectedValue == WorkStation.未贴背胶电镀件拍照位 ? txtBoxNoGumCH1Open : txtBoxLabeledGumCH1Open, cmdOpen);
                        SetTextBoxVal((WorkStation)cmbLightSta.SelectedValue == WorkStation.未贴背胶电镀件拍照位 ? txtBoxNoGumCH1Close : txtBoxLabeledGumCH1Close, cmdClose);
                    }
                    else
                    {
                        MessageBox.Show("请选择'未贴背胶电镀件拍照位'或者'背胶拍照位'");
                    }
                    break;
                case CHEnum.通道2:
                    if ((WorkStation)cmbLightSta.SelectedValue == WorkStation.背胶拍照位 || (WorkStation)cmbLightSta.SelectedValue == WorkStation.贴电镀件到塑胶件拍照位)
                    {
                        SetTextBoxVal((WorkStation)cmbLightSta.SelectedValue == WorkStation.背胶拍照位 ? txtBoxGumedCH2Open : txtBoxLabelLogoCH2Open, cmdOpen);
                        SetTextBoxVal((WorkStation)cmbLightSta.SelectedValue == WorkStation.背胶拍照位 ? txtBoxGumedCH2Close : txtBoxLabelLogoCH2Close, cmdClose);
                    }
                    else
                    {
                        MessageBox.Show("请选择'贴背胶电镀件拍照位'或者'贴电镀件到塑胶件拍照位'");
                    }
                    break;
                case CHEnum.通道3:
                    if ((WorkStation)cmbLightSta.SelectedValue == WorkStation.AOI偏位拍照位)
                    {
                        SetTextBoxVal(txtBoxAoiOpen, cmdOpen);
                        SetTextBoxVal(txtBoxAoiClose, cmdClose);
                    }
                    else
                    {
                        MessageBox.Show("请选择'AOI偏位拍照位'");
                    }
                    break;
                case CHEnum.通道4:
                    break;
                default:
                    break;
            }

        }

        private string CombinCMDStrings(string cmd)
        {
            string finalString = lightCtrPatternCmd + cmd + ((int)cmbLightCtrCH.SelectedValue).ToString() + lightCtrData;
            finalString = finalString.ToUpper() + GetXorString(finalString.ToUpper(), Encoding.Default);
            return finalString;
        }
        #endregion

        #region 窗体事件
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            if (serialPort1.BytesToRead > 0)
            {
                this.Invoke(new Action(() =>
                {
                    txtBoxReturnCmdVal.Text = serialPort1.ReadExisting();
                    if ((int)cmbLightCtrCMD.SelectedValue == 4)
                    {
                        traBarBrightVal.Value = Int32.Parse(txtBoxReturnCmdVal.Text.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
                        //txtBoxBrightVal.Text = Int32.Parse(textBox1.Text.Substring(4,2), System.Globalization.NumberStyles.HexNumber).ToString();
                    }
                }));

            }
        }

        private void traBarBrightVal_ValueChanged(object sender, EventArgs e)
        {
            txtBoxBrightVal.Text = traBarBrightVal.Value.ToString();
            lightCtrData = traBarBrightVal.Value.ToString("x3");
            if ((CmdEnum)cmbLightCtrCMD.SelectedValue == CmdEnum.设置通道亮度)
            {
                btnLightExecute_Click(null, null);
                SetCtrCmdToTextBox();
            }
        }

        private void btnLightExecute_Click(object sender, EventArgs e)
        {
            using (serialPort1)
            {
                if (!serialPort1.IsOpen)
                {
                    serialPort1.Open();
                }
                string cmd = CombinCMDStrings(lightCtrCmd);

                serialPort1.Write(cmd);
                //serialPort1.Write("#4206417");
                System.Threading.Thread.Sleep(100);
                Application.DoEvents();
            }
        }

        private void cmbLightCtrCMD_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch ((CmdEnum)cmbLightCtrCMD.SelectedValue)
            {
                case CmdEnum.打开通道:
                    {
                        traBarBrightVal.Enabled = false;
                        txtBoxBrightVal.Enabled = false;
                        cmbLightSta.Enabled = true;
                        lightCtrCmd = "1";
                    }
                    break;
                case CmdEnum.关闭通道:
                    {
                        traBarBrightVal.Enabled = false;
                        txtBoxBrightVal.Enabled = false;
                        cmbLightSta.Enabled = true;
                        lightCtrCmd = "2";
                    }
                    break;
                case CmdEnum.设置通道亮度:
                    {
                        traBarBrightVal.Enabled = true;
                        txtBoxBrightVal.Enabled = true;
                        cmbLightSta.Enabled = false;
                        lightCtrCmd = "3";
                    }
                    break;
                case CmdEnum.读出通道亮度:
                    {
                        traBarBrightVal.Enabled = true;
                        txtBoxBrightVal.Enabled = true;
                        cmbLightSta.Enabled = false;
                        lightCtrCmd = "4";
                    }
                    break;
                default:
                    {
                        traBarBrightVal.Enabled = false;
                        txtBoxBrightVal.Enabled = false;
                        cmbLightSta.Enabled = true;
                    }
                    break;
            }
        }

        private void txtBoxBrightVal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int brightVal = 0;
                if (!int.TryParse(txtBoxBrightVal.Text.Trim(), out brightVal))
                {
                    MessageBox.Show("请输入有效值!");
                    return;
                }
                if (brightVal > 255)
                {
                    MessageBox.Show("最大值只能到255!");
                    return;
                }
                traBarBrightVal.Value = brightVal;
            }
        }

        private void btnSaveCHConfig_Click(object sender, EventArgs e)
        {
            GlobalVar.lightNoGumLogoCH1OpenCmd = txtBoxNoGumCH1Open.Text;
            GlobalVar.lightNoGumLogoCH1CloseCmd = txtBoxNoGumCH1Close.Text;
            GlobalVar.lightLabeledGumCH1OpenCmd = txtBoxLabeledGumCH1Open.Text;
            GlobalVar.lightLabeledGumCH1CloseCmd = txtBoxLabeledGumCH1Close.Text;
            GlobalVar.lightGumedCH2OpenCmd = txtBoxGumedCH2Open.Text;
            GlobalVar.lightGumedCH2CloseCmd = txtBoxGumedCH2Close.Text;
            GlobalVar.lightLabeledLogoCH2OpenCmd = txtBoxLabelLogoCH2Open.Text;
            GlobalVar.lightLabeledLogoCH2CloseCmd = txtBoxLabelLogoCH2Close.Text;
            GlobalVar.lightCH3OpenCmd = txtBoxAoiOpen.Text;
            GlobalVar.lightCH3CloseCmd = txtBoxAoiClose.Text;
            GlobalVar.lightContrCom = cmbLightCom.SelectedItem.ToString();
            if (!serialPort1.IsOpen)
            {
                serialPort1.PortName = GlobalVar.lightContrCom;
            }
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "频闪控制器", "串口号", GlobalVar.lightContrCom);
            INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "频闪控制器", "未贴背胶的Logo通道1打开命令", GlobalVar.lightNoGumLogoCH1OpenCmd);
            INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "频闪控制器", "未贴背胶的Logo通道1关闭命令", GlobalVar.lightNoGumLogoCH1CloseCmd);
            INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "频闪控制器", "贴背胶的Logo通道1打开命令", GlobalVar.lightLabeledGumCH1OpenCmd);
            INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "频闪控制器", "贴背胶的Logo通道1关闭命令", GlobalVar.lightLabeledGumCH1CloseCmd);
            INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "频闪控制器", "拍背胶的通道2打开命令", GlobalVar.lightGumedCH2OpenCmd);
            INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "频闪控制器", "拍背胶的通道2关闭命令", GlobalVar.lightGumedCH2CloseCmd);
            INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "频闪控制器", "拍塑胶件的通道2打开命令", GlobalVar.lightLabeledLogoCH2OpenCmd);
            INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "频闪控制器", "拍塑胶件的通道2关闭命令", GlobalVar.lightLabeledLogoCH2CloseCmd);
            INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "频闪控制器", "AOI偏位测试的通道3打开命令", GlobalVar.lightCH3OpenCmd);
            INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "频闪控制器", "AOI偏位测试的通道3关闭命令", GlobalVar.lightCH3CloseCmd);
        }

        private void btnSaveVisionCfgData_Click(object sender, EventArgs e)
        {
            if (toolType != "TB5")
            {
                MessageBox.Show("当前打开工具不是AOI偏位测试的，不保存修改数据");
                return;
            }
            if (JudgeTextBoxIsNullOrSpace(txtBoxOffsetXVmin)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxOffsetXVmax)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxOffsetYVmin)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxOffsetYVmax)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxOffsetDegVmin)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxOffsetDegVmax)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxOffsetX)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxOffsetY)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxOffsetDeg)) return;
            if (JudgeTextBoxValIsAppropriate(txtBoxOffsetXVmin)) return;
            if (JudgeTextBoxValIsAppropriate(txtBoxOffsetXVmax)) return;
            if (JudgeTextBoxValIsAppropriate(txtBoxOffsetYVmin)) return;
            if (JudgeTextBoxValIsAppropriate(txtBoxOffsetYVmax)) return;
            if (JudgeTextBoxValIsAppropriate(txtBoxOffsetDegVmin)) return;
            if (JudgeTextBoxValIsAppropriate(txtBoxOffsetDegVmax)) return;
            if (JudgeTextBoxValIsAppropriate(txtBoxOffsetX)) return;
            if (JudgeTextBoxValIsAppropriate(txtBoxOffsetY)) return;
            if (JudgeTextBoxValIsAppropriate(txtBoxOffsetDeg)) return;
            if (CompareMinAndMaxValIsAppropriate(txtBoxOffsetXVmin, txtBoxOffsetXVmax)) return;
            if (CompareMinAndMaxValIsAppropriate(txtBoxOffsetYVmin, txtBoxOffsetYVmax)) return;
            if (CompareMinAndMaxValIsAppropriate(txtBoxOffsetDegVmin, txtBoxOffsetDegVmax)) return;
            GlobalVar.aoiProCenterXOffsetVmin = Convert.ToDouble(txtBoxOffsetXVmin.Text.Trim());
            GlobalVar.aoiProCenterXOffsetVmax = Convert.ToDouble(txtBoxOffsetXVmax.Text.Trim());
            GlobalVar.aoiProCenterYOffsetVmin = Convert.ToDouble(txtBoxOffsetYVmin.Text.Trim());
            GlobalVar.aoiProCenterYOffsetVmax = Convert.ToDouble(txtBoxOffsetYVmax.Text.Trim());
            GlobalVar.aoiProDegOffsetVmin = Convert.ToDouble(txtBoxOffsetDegVmin.Text.Trim());
            GlobalVar.aoiProDegOffsetVmax = Convert.ToDouble(txtBoxOffsetDegVmax.Text.Trim());
            GlobalVar.aoiProCenterOffsetX = Convert.ToDouble(txtBoxOffsetX.Text.Trim());
            GlobalVar.aoiProCenterOffsetY = Convert.ToDouble(txtBoxOffsetY.Text.Trim());
            GlobalVar.aoiProCenterOffsetDeg = Convert.ToDouble(txtBoxOffsetDeg.Text.Trim());
            switch (GlobalVar.curProName)
            {
                case "Renault":
                    {
                        INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "AOI偏位检测", "X中心偏移最小值", GlobalVar.aoiProCenterXOffsetVmin.ToString());
                        INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "AOI偏位检测", "X中心偏移最大值", GlobalVar.aoiProCenterXOffsetVmax.ToString());
                        INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "AOI偏位检测", "Y中心偏移最小值", GlobalVar.aoiProCenterYOffsetVmin.ToString());
                        INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "AOI偏位检测", "Y中心偏移最大值", GlobalVar.aoiProCenterYOffsetVmax.ToString());
                        INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "AOI偏位检测", "θ中心偏移最小值", GlobalVar.aoiProDegOffsetVmin.ToString());
                        INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "AOI偏位检测", "θ中心偏移最大值", GlobalVar.aoiProDegOffsetVmax.ToString());
                        INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "AOI偏位检测", "X中心偏移补偿值", GlobalVar.aoiProCenterOffsetX.ToString());
                        INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "AOI偏位检测", "Y中心偏移补偿值", GlobalVar.aoiProCenterOffsetY.ToString());
                        INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "AOI偏位检测", "θ中心偏移补偿值", GlobalVar.aoiProCenterOffsetDeg.ToString());
                    }
                    break;
                case "Lada":
                    {
                        INI.INIWriteValue(GlobalVar.bRecipeLadaFilePath, "AOI偏位检测", "X中心偏移最小值", GlobalVar.aoiProCenterXOffsetVmin.ToString());
                        INI.INIWriteValue(GlobalVar.bRecipeLadaFilePath, "AOI偏位检测", "X中心偏移最大值", GlobalVar.aoiProCenterXOffsetVmax.ToString());
                        INI.INIWriteValue(GlobalVar.bRecipeLadaFilePath, "AOI偏位检测", "Y中心偏移最小值", GlobalVar.aoiProCenterYOffsetVmin.ToString());
                        INI.INIWriteValue(GlobalVar.bRecipeLadaFilePath, "AOI偏位检测", "Y中心偏移最大值", GlobalVar.aoiProCenterYOffsetVmax.ToString());
                        INI.INIWriteValue(GlobalVar.bRecipeLadaFilePath, "AOI偏位检测", "θ中心偏移最小值", GlobalVar.aoiProDegOffsetVmin.ToString());
                        INI.INIWriteValue(GlobalVar.bRecipeLadaFilePath, "AOI偏位检测", "θ中心偏移最大值", GlobalVar.aoiProDegOffsetVmax.ToString());
                        INI.INIWriteValue(GlobalVar.bRecipeLadaFilePath, "AOI偏位检测", "X中心偏移补偿值", GlobalVar.aoiProCenterOffsetX.ToString());
                        INI.INIWriteValue(GlobalVar.bRecipeLadaFilePath, "AOI偏位检测", "Y中心偏移补偿值", GlobalVar.aoiProCenterOffsetY.ToString());
                        INI.INIWriteValue(GlobalVar.bRecipeLadaFilePath, "AOI偏位检测", "θ中心偏移补偿值", GlobalVar.aoiProCenterOffsetDeg.ToString());
                    }
                    break;
            }
            InspectionClass.tb5.Inputs["XVCentDisMin"].Value = GlobalVar.aoiProCenterXOffsetVmin;
            InspectionClass.tb5.Inputs["XVCentDisMax"].Value = GlobalVar.aoiProCenterXOffsetVmax;
            InspectionClass.tb5.Inputs["YVCentDisMin"].Value = GlobalVar.aoiProCenterYOffsetVmin;
            InspectionClass.tb5.Inputs["YVCentDisMax"].Value = GlobalVar.aoiProCenterYOffsetVmax;
            InspectionClass.tb5.Inputs["AngleRangeMin"].Value = GlobalVar.aoiProDegOffsetVmin;
            InspectionClass.tb5.Inputs["AngleRangeMax"].Value = GlobalVar.aoiProDegOffsetVmax;
            InspectionClass.tb5.Inputs["XVCentDisOffset"].Value = GlobalVar.aoiProCenterOffsetX;
            InspectionClass.tb5.Inputs["YVCentDisOffset"].Value = GlobalVar.aoiProCenterOffsetY;
            InspectionClass.tb5.Inputs["AngleRangeOffset"].Value = GlobalVar.aoiProCenterOffsetDeg;
        }

        private void btnSaveVisionFile_Click(object sender, EventArgs e)
        {
            try
            {
                switch (toolType)
                {
                    case "TB1":
                        {
                            if (InspectionClass.tb1 != null && cogTBEditNoGum.Subject != null && !isSaveTool)
                            {
                                InspectionClass.tb1 = cogTBEditNoGum.Subject;
                                CogSerializer.SaveObjectToFile(InspectionClass.tb1, GlobalVar.bVpp1FilePath, typeof(BinaryFormatter), CogSerializationOptionsConstants.Minimum);
                                System.Threading.Thread.Sleep(300);
                                isSaveTool = true;
                            }
                        }
                        break;
                    case "TB2":
                        {
                            if (InspectionClass.tb2 != null && cogTBEditLabeledGum.Subject != null && !isSaveTool)
                            {
                                InspectionClass.tb2 = cogTBEditLabeledGum.Subject;
                                CogSerializer.SaveObjectToFile(InspectionClass.tb2, GlobalVar.bVpp2FilePath, typeof(BinaryFormatter), CogSerializationOptionsConstants.Minimum);
                                System.Threading.Thread.Sleep(300);
                                isSaveTool = true;
                            }
                        }
                        break;
                    case "TB3":
                        {
                            if (InspectionClass.tb3 != null && cogTBEditGumed.Subject != null && !isSaveTool)
                            {
                                InspectionClass.tb3 = cogTBEditGumed.Subject;
                                CogSerializer.SaveObjectToFile(InspectionClass.tb3, GlobalVar.bVpp3FilePath, typeof(BinaryFormatter), CogSerializationOptionsConstants.Minimum);
                                System.Threading.Thread.Sleep(300);
                                isSaveTool = true;
                            }
                        }
                        break;
                    case "TB4":
                        {
                            if (InspectionClass.tb4 != null && cogTBEditLabeledLogo.Subject != null && !isSaveTool)
                            {
                                InspectionClass.tb4 = cogTBEditLabeledLogo.Subject;
                                CogSerializer.SaveObjectToFile(InspectionClass.tb4, GlobalVar.bVpp4FilePath, typeof(BinaryFormatter), CogSerializationOptionsConstants.Minimum);
                                System.Threading.Thread.Sleep(300);
                                isSaveTool = true;
                            }
                        }
                        break;
                    case "TB5":
                        {
                            if (InspectionClass.tb5 != null && cogTBEditAOITest.Subject != null && !isSaveTool)
                            {
                                InspectionClass.tb5 = cogTBEditAOITest.Subject;
                                CogSerializer.SaveObjectToFile(InspectionClass.tb5, GlobalVar.bVpp5FilePath, typeof(BinaryFormatter), CogSerializationOptionsConstants.Minimum);
                                System.Threading.Thread.Sleep(300);
                                isSaveTool = true;
                            }
                        }
                        break;
                }
                MessageBox.Show("工具数据保存成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show("工具数据保存失败 : " + ex.Message);
            }

        }

        private void btnOpenAOIOffsetVisionFile_Click(object sender, EventArgs e)
        {
            if (isSaveTool)
            {
                toolType = "TB5";
                isSaveTool = false;
                cogTBEditNoGum.Visible = false;
                cogTBEditLabeledGum.Visible = false;
                cogTBEditGumed.Visible = false;
                cogTBEditLabeledLogo.Visible = false;
                cogTBEditAOITest.Visible = true;
                tabControl4.Enabled = true;
                //cogTBEditAOITest.Subject = InspectionClass.tb5;
            }
            else
            {
                MessageBox.Show("请点击保存修改按钮");
            }
        }

        private void btnOpenLabeledLogoVisionFile_Click(object sender, EventArgs e)
        {
            if (isSaveTool)
            {
                toolType = "TB4";
                isSaveTool = false;
                cogTBEditNoGum.Visible = false;
                cogTBEditLabeledGum.Visible = false;
                cogTBEditGumed.Visible = false;
                cogTBEditLabeledLogo.Visible = true;
                cogTBEditAOITest.Visible = false;
                tabControl4.Enabled = false;
                //cogTBEditLabeledLogo.Subject = InspectionClass.tb4;
            }
            else
            {
                MessageBox.Show("请点击保存修改按钮");
            }
        }

        private void btnOpenNoLabeledLogoVisionFile_Click(object sender, EventArgs e)
        {
            if (isSaveTool)
            {
                toolType = "TB3";
                isSaveTool = false;
                cogTBEditNoGum.Visible = false;
                cogTBEditLabeledGum.Visible = false;
                cogTBEditGumed.Visible = true;
                cogTBEditLabeledLogo.Visible = false;
                cogTBEditAOITest.Visible = false;
                tabControl4.Enabled = false;
                //cogTBEditGumed.Subject = InspectionClass.tb3;
            }
            else
            {
                MessageBox.Show("请点击保存修改按钮");
            }
        }

        private void btnOpenGumedVisionFile_Click(object sender, EventArgs e)
        {
            if (isSaveTool)
            {
                toolType = "TB2";
                isSaveTool = false;
                cogTBEditNoGum.Visible = false;
                cogTBEditLabeledGum.Visible = true;
                cogTBEditGumed.Visible = false;
                cogTBEditLabeledLogo.Visible = false;
                cogTBEditAOITest.Visible = false;
                tabControl4.Enabled = false;
                //cogTBEditLabeledGum.Subject = InspectionClass.tb2;
            }
            else
            {
                MessageBox.Show("请点击保存修改按钮");
            }
        }

        private void btnOpenNoGumedVisionFile_Click(object sender, EventArgs e)
        {
            if (isSaveTool)
            {
                toolType = "TB1";
                isSaveTool = false;
                cogTBEditNoGum.Visible = true;
                cogTBEditLabeledGum.Visible = false;
                cogTBEditGumed.Visible = false;
                cogTBEditLabeledLogo.Visible = false;
                cogTBEditAOITest.Visible = false;
                tabControl4.Enabled = false;
                //cogTBEditNoGum.Subject = InspectionClass.tb1;
            }
            else
            {
                MessageBox.Show("请点击保存修改按钮");
            }
        }

        private void FrmVisionSetting_Enter(object sender, EventArgs e)
        {
            if (!isFirstOpenThisForm)
            {
                isFirstOpenThisForm = true;
            }
            else
            {
                InitVisionCfgFile();
            }
        }

        private void FrmVisionSetting_Leave(object sender, EventArgs e)
        {
            if (!isSaveTool)
            {
                MessageBox.Show("请点击'保存当前工具数据'按钮再退出");
            }
        }

        private void btnSaveOffsetNoGum_Click(object sender, EventArgs e)
        {
            if (JudgeTextBoxIsNullOrSpace(txtBoxNoGumOffsetX)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxNoGumOffsetY)) return;
            double vx,vy = 0;
            if (!double.TryParse(txtBoxNoGumOffsetX.Text, out vx)) { MessageBox.Show("请输入有效值"); return; }
            if (!double.TryParse(txtBoxNoGumOffsetY.Text, out vy)) { MessageBox.Show("请输入有效值"); return; }
            GlobalVar.noGumRobotOffsetX = vx;
            GlobalVar.noGumRobotOffsetY = vy;
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "第二工位Robot工具补偿值", "OffsetX", vx.ToString());
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "第二工位Robot工具补偿值", "OffsetY", vy.ToString());
        }

        private void btnSaveOffsetLabelLogo_Click(object sender, EventArgs e)
        {
            if (JudgeTextBoxIsNullOrSpace(txtBoxLabelLogoOffsetX)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxLabelLogoOffsetY)) return;
            double vx = 0;
            double vy = 0;
            if (!double.TryParse(txtBoxLabelLogoOffsetX.Text, out vx)) { MessageBox.Show("请输入有效值"); return; }
            if (!double.TryParse(txtBoxLabelLogoOffsetY.Text, out vy)) { MessageBox.Show("请输入有效值"); return; }
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "第四工位Robot工具补偿值", "OffsetX", vx.ToString("f4"));
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "第四工位Robot工具补偿值", "OffsetY", vy.ToString("f4"));
            GlobalVar.gumedRobotOffsetX = vx;
            GlobalVar.gumedRobotOffsetY = vy;
        }   
        #endregion

        #region 视觉变量声明
        string toolType = "";
        bool isSaveTool = true;
        /// <summary>
        /// 是否第一次打开本窗体
        /// </summary>
        bool isFirstOpenThisForm = false;
        #endregion 

        #region 初始化窗体加载方法
        private void InitVisionCfgFile()
        {
            txtBoxGumedRobotToolX.Text = GlobalVar.noGumTeachRobotTX.ToString();
            txtBoxGumedRobotToolY.Text = GlobalVar.noGumTeachRobotTY.ToString();
            txtBoxGumedRobotToolU.Text = GlobalVar.noGumTeachRobotTU.ToString();
            txtBoxLabeledLogoRobotToolX.Text = GlobalVar.gumedTeachRobotTX.ToString();
            txtBoxLabeledLogoRobotToolY.Text = GlobalVar.gumedTeachRobotTY.ToString();
            txtBoxLabeledLogoRobotToolU.Text = GlobalVar.gumedTeachRobotTU.ToString();
            txtBoxNoGumedXRatio.Text = GlobalVar.noLabelLogoRatioX.ToString();
            txtBoxNoGumedYRatio.Text = GlobalVar.noLabelLogoRatioY.ToString();
            txtBoxNoGumedXTeach.Text = GlobalVar.noLabelLogoProTeachCenterX.ToString();
            txtBoxNoGumedYTeach.Text = GlobalVar.noLabelLogoProTeachCenterY.ToString();
            txtBoxNoGumedDegTeach.Text = GlobalVar.noLabelLogoProTeachDeg.ToString();
            txtBoxGumedXRatio.Text = GlobalVar.noGumRatioX.ToString();
            txtBoxGumedYRatio.Text = GlobalVar.noGumRatioY.ToString();
            txtBoxGumedXTeach.Text = GlobalVar.noGumProTeachCenterX.ToString();
            txtBoxGumedYTeach.Text = GlobalVar.noGumProTeachCenterY.ToString();
            txtBoxGumedDegTeach.Text = GlobalVar.noGumProTeachDeg.ToString();
            txtBoxNoLabeledLogoXRatio.Text = GlobalVar.labeledLogoRatioX.ToString();
            txtBoxNoLabeledLogoYRatio.Text = GlobalVar.labeledLogoRatioY.ToString();
            txtBoxNoLabeledLogoXTeach.Text = GlobalVar.labeledLogoProTeachCenterX.ToString();
            txtBoxNoLabeledLogoYTeach.Text = GlobalVar.labeledLogoProTeachCenterY.ToString();
            txtBoxNoLabeledLogoDegTeach.Text = GlobalVar.labeledLogoProTeachDeg.ToString();
            txtBoxLabeledLogoXRatio.Text = GlobalVar.gumedRatioX.ToString();
            txtBoxLabeledLogoYRatio.Text = GlobalVar.gumedRatioY.ToString();
            txtBoxLabeledLogoXTeach.Text = GlobalVar.gumedProTeachCenterX.ToString();
            txtBoxLabeledLogoYTeach.Text = GlobalVar.gumedProTeachCenterY.ToString();
            txtBoxLabeledLogoDegTeach.Text = GlobalVar.gumedProTeachDeg.ToString();
            txtBoxNoGumOffsetX.Text = GlobalVar.noGumRobotOffsetX.ToString();
            txtBoxNoGumOffsetY.Text = GlobalVar.noGumRobotOffsetY.ToString();
            txtBoxLabelLogoOffsetX.Text = GlobalVar.gumedRobotOffsetX.ToString();
            txtBoxLabelLogoOffsetY.Text = GlobalVar.gumedRobotOffsetY.ToString();
    
            // 在设定界面显示AOI判定上下限 - 薛骄奎 2022/3/17
            //txtBoxOffsetXVmax.Text = GlobalVar.aoiProCenterYOffsetVmax.ToString();
            //txtBoxOffsetXVmin.Text = GlobalVar.aoiProCenterXOffsetVmin.ToString();
            //txtBoxOffsetYVmax.Text = GlobalVar.aoiProCenterYOffsetVmax.ToString();
            //txtBoxOffsetYVmin.Text = GlobalVar.aoiProCenterXOffsetVmin.ToString();
            //txtBoxOffsetDegVmax.Text = GlobalVar.aoiProDegOffsetVmax.ToString();
            //txtBoxOffsetDegVmin.Text = GlobalVar.aoiProDegOffsetVmin.ToString();
            //txtBoxOffsetX.Text = GlobalVar.aoiProCenterOffsetX.ToString();
            //txtBoxOffsetY.Text = GlobalVar.aoiProCenterOffsetY.ToString();
            //txtBoxOffsetDeg.Text = GlobalVar.aoiProCenterOffsetDeg.ToString();
        }

        private void InitToolBlockDis()
        {
            cogTBEditNoGum.Visible = true;
            cogTBEditLabeledGum.Visible = false;
            cogTBEditGumed.Visible = false;
            cogTBEditLabeledLogo.Visible = false;
            cogTBEditAOITest.Visible = false;
            cogTBEditNoGum.Subject = InspectionClass.tb1;
            cogTBEditLabeledGum.Subject = InspectionClass.tb2;
            cogTBEditGumed.Subject = InspectionClass.tb3;
            cogTBEditLabeledLogo.Subject = InspectionClass.tb4;
            cogTBEditAOITest.Subject = InspectionClass.tb5;
            tabControl4.Enabled = false;
        }


        #endregion

        #region 其他方法
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
        /// <summary>
        /// 判断两文本框的值是否合理
        /// </summary>
        /// <param name="tbVmin"></param>
        /// <param name="tbVmax"></param>
        /// <returns></returns>
        private bool CompareMinAndMaxValIsAppropriate(TextBox tbVmin, TextBox tbVmax)
        {
            if (Convert.ToDouble(tbVmax.Text.Trim()) < Convert.ToDouble(tbVmin.Text.Trim()))
            {
                tbVmax.BackColor = Color.OrangeRed;
                tbVmax.Clear();
                tbVmax.Focus();
                tbVmin.BackColor = Color.OrangeRed;
                tbVmin.Clear();
                tbVmin.Focus();
                MessageBox.Show("最小值不能大于最大值设定");
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool JudgeTextBoxValIsAppropriate(TextBox tb)
        {
            double tempV = 0;
            if (double.TryParse(tb.Text.Trim(), out tempV))
            {
                if (tb.BackColor == Color.OrangeRed)
                {
                    tb.BackColor = Color.FromKnownColor(KnownColor.Window);
                }
                return false;
            }
            else
            {
                MessageBox.Show("请输入有效值");
                tb.BackColor = Color.OrangeRed;
                tb.Clear();
                tb.Focus();
                return true;
            }
        }
        #endregion

        
    }
}
