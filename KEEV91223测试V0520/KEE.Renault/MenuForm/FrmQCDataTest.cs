using ClassINI;
using csIOC0640;
using KEE.Renault.CamOperator;
using KEE.Renault.Common;
using KEE.Renault.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KEE.Renault.MenuForm
{
    public partial class FrmQCDataTest : Form
    {
        public FrmQCDataTest()
        {
            InitializeComponent();
            InitCfgUIData();
            InitAllPictures();
            chkBoxSafeDoorIsClose.Checked = GlobalVar.isCloseSafeDoor;
        }

        List<PictureBox> picList = new List<PictureBox>();
        double realV = 0;
        CancellationTokenSource tokenSource = null;
        bool isTimeOut = true;
        bool isClickRotate = false;

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                TraversalAllPic();
                try
                {
                    if (!GlobalVar.totalRunFlag)
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
                            txtBoxRealV.Text = (GlobalVar.highIndSpaceHigh + realV - GlobalVar.highIndInitHighVal).ToString("f3");
                        }
                    }
                    else
                    {
                        this.errorProvider1.SetError(this.pictureBox9, "请先停止运行中设备");
                    }
                }
                catch (Exception ex)
                {
                    GlobalVar.myLog.Error($"QC测高产生异常错误：{ex.Message}");
                }
            }));
        }

        private void btnHighCylinder_Click(object sender, EventArgs e)
        {
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

        private void btnRotateNCam_Click(object sender, EventArgs e)
        {
            if (!GlobalVar.totalRunFlag)
            {
                if (GlobalVar.lsAxiasDIs[20].CurIOStatus && !GlobalVar.lsAxiasDIs[21].CurIOStatus && GlobalVar.lsAxiasDIs[22].CurIOStatus && !GlobalVar.lsAxiasDIs[23].CurIOStatus && GlobalVar.lsAxiasDIs[14].CurIOStatus && !GlobalVar.lsAxiasDIs[15].CurIOStatus && GlobalVar.lsAxiasDIs[24].CurIOStatus && !GlobalVar.lsAxiasDIs[25].CurIOStatus && GlobalVar.lsAxiasDIs[26].CurIOStatus && !GlobalVar.lsAxiasDIs[27].CurIOStatus)
                {
                    if (!isClickRotate)
                    {
                        isClickRotate = true;
                        tokenSource = new CancellationTokenSource(3000);
                        tokenSource.Token.Register(new Action(() => { isClickRotate = false; DialogResult = isTimeOut ? MessageBox.Show("轮盘反向转动超时") : MessageBox.Show("轮盘反向转动到位"); }));
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

        private void btnOpenAOILight_Click(object sender, EventArgs e)
        {
            //using (SerialPort sp = new SerialPort(GlobalVar.lightContrCom))
            //{
            //    if (!sp.IsOpen)
            //    {
            //        sp.Open();
            //    }
            //    sp.Write(GlobalVar.lightCH3OpenCmd);
            //}
            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[32].Card - 1), GlobalVar.lsAxiasDOs[32].PinDefinition, (ushort)1);
        }

        private void btnCloseAOILight_Click(object sender, EventArgs e)
        {
            //using (SerialPort sp = new SerialPort(GlobalVar.lightContrCom))
            //{
            //    if (!sp.IsOpen)
            //    {
            //        sp.Open();
            //    }
            //    sp.Write(GlobalVar.lightCH3CloseCmd);
            //}
            IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[32].Card - 1), GlobalVar.lsAxiasDOs[32].PinDefinition, (ushort)0);
        }

        private void FrmQCDataTest_Enter(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void FrmQCDataTest_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void btnAOITest_Click(object sender, EventArgs e)
        {
            InspectionClass.tb5.Run();
            if (InspectionClass.resultDisplayAOI != null)
            {
                this.cogAoiDis.Record = InspectionClass.resultDisplayAOI.Record;
            }
        }

        private void btnSaveHighPara_Click(object sender, EventArgs e)
        {
            if (JudgeTextBoxIsNullOrSpace(txtBoxInitHighV)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxSpaceHigh)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxMaxV)) return;
            if (JudgeTextBoxIsNullOrSpace(txtBoxMinV)) return;
            double a, b, d, f = 0;
            if (!double.TryParse(txtBoxInitHighV.Text.Trim(), out a)) { MessageBox.Show("请输入有效数值"); return; }
            if (!double.TryParse(txtBoxSpaceHigh.Text.Trim(), out b)) { MessageBox.Show("请输入有效数值"); return; }
            if (!double.TryParse(txtBoxMaxV.Text.Trim(), out d)) { MessageBox.Show("请输入有效数值"); return; }
            if (!double.TryParse(txtBoxMinV.Text.Trim(), out f)) { MessageBox.Show("请输入有效数值"); return; }
            GlobalVar.highIndInitHighVal = a;
            GlobalVar.highIndSpaceHigh = b;
            GlobalVar.highIndMaxVal = d;
            GlobalVar.highIndMinVal = f;
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "测高模组", "初始值", a.ToString());
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
            txtBoxInitHighV.Text = GlobalVar.highIndInitHighVal.ToString();
            txtBoxSpaceHigh.Text = GlobalVar.highIndSpaceHigh.ToString();
            txtBoxMaxV.Text = GlobalVar.highIndMaxVal.ToString();
            txtBoxMinV.Text = GlobalVar.highIndMinVal.ToString();
            tbLogoXMax.Text = GlobalVar.logoXmax.ToString();
            tbLogoXMin.Text = GlobalVar.logoXmin.ToString();
            tbLogoYMax.Text = GlobalVar.logoYmax.ToString();
            tbLogoYMin.Text = GlobalVar.logoYmin.ToString();
        }
        private void InitAllPictures()
        {
            foreach (var pic in panel1.Controls)
            {
                if (pic is PictureBox)
                {
                    picList.Add((PictureBox)pic);
                }

            }
            foreach (var pic in splitContainer1.Panel2.Controls)
            {
                if (pic is PictureBox)
                {
                    picList.Add((PictureBox)pic);
                }

            }

        }





        #endregion

        private void chkBoxSafeDoorIsClose_CheckedChanged(object sender, EventArgs e)
        {
            GlobalVar.isCloseSafeDoor = chkBoxSafeDoorIsClose.Checked;
        }

        #region
        private void btnSaveLogoPara_Click(object sender, EventArgs e)
        {
            if (JudgeTextBoxIsNullOrSpace(tbLogoXMax)) return;//判断是否为空
            if (JudgeTextBoxIsNullOrSpace(tbLogoXMin)) return;
            if (JudgeTextBoxIsNullOrSpace(tbLogoYMax)) return;
            if (JudgeTextBoxIsNullOrSpace(tbLogoYMin)) return;
            double w, x, y, z = 0;
            if (!double.TryParse(tbLogoXMax.Text.Trim(), out w)) { MessageBox.Show("请输入有效数值"); return; }//判断是否为有效值
            if (!double.TryParse(tbLogoXMin.Text.Trim(), out x)) { MessageBox.Show("请输入有效数值"); return; }
            if (!double.TryParse(tbLogoYMax.Text.Trim(), out y)) { MessageBox.Show("请输入有效数值"); return; }
            if (!double.TryParse(tbLogoYMin.Text.Trim(), out z)) { MessageBox.Show("请输入有效数值"); return; }
            GlobalVar.logoXmax = w;//赋值
            GlobalVar.logoXmin = x;
            GlobalVar.logoYmax = y;
            GlobalVar.logoYmin = z;

            switch (GlobalVar.curProName)
            {
                case "Renault":
                    INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "电镀件内外轮廓偏位判断值", "logoXmax", w.ToString());//写到Recipe INI文件
                    INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "电镀件内外轮廓偏位判断值", "logoXmin", x.ToString());
                    INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "电镀件内外轮廓偏位判断值", "logoYmax", y.ToString());
                    INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "电镀件内外轮廓偏位判断值", "logoYmin", z.ToString());
                    break;
                case "Lada":
                    INI.INIWriteValue(GlobalVar.bRecipeLadaFilePath, "电镀件内外轮廓偏位判断值", "logoXmax", w.ToString());
                    INI.INIWriteValue(GlobalVar.bRecipeLadaFilePath, "电镀件内外轮廓偏位判断值", "logoXmin", x.ToString());
                    INI.INIWriteValue(GlobalVar.bRecipeLadaFilePath, "电镀件内外轮廓偏位判断值", "logoYmax", y.ToString());
                    INI.INIWriteValue(GlobalVar.bRecipeLadaFilePath, "电镀件内外轮廓偏位判断值", "logoYmin", z.ToString());
                    break;
            }
        }
        #endregion

    }


}
