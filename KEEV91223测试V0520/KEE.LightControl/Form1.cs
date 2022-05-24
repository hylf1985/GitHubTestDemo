using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KEE.LightControl
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public delegate void CbDelegate<T1,T2>(T1 obj1, T2 obj2);

        public  readonly string lightCtrPatternCmd = "#";

        public string lightCtrCmd = "1";//默认是打开命令

        public string lightCtrCH = "1";//默认1通道

        public string lightCtrData = "064";//默认100亮度值转16进制用3字节

        public string lightCtrXor = "00";//异或2字节

        List<CmdEnum> cmds = new List<CmdEnum>();
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

        List<CHEnum> cHs = new List<CHEnum>();

        private void traBarBrightVal_ValueChanged(object sender, EventArgs e)
        {
            txtBoxBrightVal.Text = traBarBrightVal.Value.ToString() ;
            lightCtrData = traBarBrightVal.Value.ToString("x3");
            if ((CmdEnum)cmbLightCtrCMD.SelectedValue == CmdEnum.设置通道亮度)
            {
                btnExcuteCmd_Click(null, null);
                SetCtrCmdToTextBox();
            }
        }

        private void SetCtrCmdToTextBox()
        {
            //打开命令
            string cmdOpen  = CombinCMDStrings("1"); 
            string cmdClose = CombinCMDStrings("2");
            switch ((CHEnum)cmbLightCtrCH.SelectedValue)
            {
                case CHEnum.通道1:
                    SetTextBoxVal(txtBoxCH1Open, cmdOpen);
                    SetTextBoxVal(txtBoxCH1Close, cmdClose);
                    SetTextBoxVal(txtBoxCH1BrightVal, txtBoxBrightVal.Text);
                    break;
                case CHEnum.通道2:
                    break;
                case CHEnum.通道3:
                    break;
                case CHEnum.通道4:
                    break;
                default:
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
                if (brightVal>255)
                {
                    MessageBox.Show("最大值只能到255!");
                    return;
                }
                traBarBrightVal.Value = brightVal;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitLightCtrCMD();
            InitLightCtrCH();
            cmbLightCtrCMD.DataSource = cmds;
            cmbLightCtrCH.DataSource = cHs;
            //traBarBrightVal.Value = 100;
            //读取第一通道保存配置的亮度值
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            if (serialPort1.BytesToRead>0)
            {
                this.Invoke(new Action(() => 
                { 
                    textBox1.Text = serialPort1.ReadExisting();
                    if ((int)cmbLightCtrCMD.SelectedValue==4)
                    {
                        traBarBrightVal.Value = Int32.Parse(textBox1.Text.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
                        //txtBoxBrightVal.Text = Int32.Parse(textBox1.Text.Substring(4,2), System.Globalization.NumberStyles.HexNumber).ToString();
                    }
                }));
               
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
                        //btnExcuteCmd.Enabled = true;
                        lightCtrCmd = "1";
                    }
                    break;
                case CmdEnum.关闭通道:
                    {
                        traBarBrightVal.Enabled = false;
                        txtBoxBrightVal.Enabled = false;
                        //btnExcuteCmd.Enabled = true;
                        lightCtrCmd = "2";
                    }
                    break;
                case CmdEnum.设置通道亮度:
                    {
                        traBarBrightVal.Enabled = true;
                        txtBoxBrightVal.Enabled = true;
                        //btnExcuteCmd.Enabled = false;
                        lightCtrCmd = "3";
                    }
                    break;
                case CmdEnum.读出通道亮度:
                    {
                        traBarBrightVal.Enabled = true;
                        txtBoxBrightVal.Enabled = true;
                        //btnExcuteCmd.Enabled = true;
                        lightCtrCmd = "4";
                    }
                    break;
                default:
                    {
                        traBarBrightVal.Enabled = false;
                        txtBoxBrightVal.Enabled = false;
                        btnExcuteCmd.Enabled = true;
                    }
                    break;
            }
        }

        private void btnExcuteCmd_Click(object sender, EventArgs e)
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
                System.Threading.Thread.Sleep(300);
                Application.DoEvents();
            }
        }

        private string CombinCMDStrings(string cmd)
        {
            string finalString = lightCtrPatternCmd + cmd + ((int)cmbLightCtrCH.SelectedValue).ToString() + lightCtrData;
            finalString = finalString.ToUpper() + GetXorString(finalString.ToUpper(), Encoding.Default);
            return finalString;
        }


        /// <summary>
        /// 字符串转换为16进制字符
        /// </summary>
        /// <param name="s"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        private string GetXorString(string s, Encoding encode)
        {
            byte[] b = encode.GetBytes(s);//按照指定编码将string编程字节数组
            string result = "";
            int res = 0;
            if (b.Length>0)
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

        private void SetTextBoxVal(TextBox tb,string val)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new CbDelegate<TextBox, string>(this.SetTextBoxVal),tb, val);
            }
            else
            {
                tb.Text = val;
            }
        }
    }



    public enum CmdEnum
    {
        打开通道 = 1,
        关闭通道 = 2,
        设置通道亮度 = 3,
        读出通道亮度 = 4
    }
    public enum CHEnum
    {
        通道1 = 1,
        通道2 = 2,
        通道3 = 3,
        通道4 = 4
    }

}
