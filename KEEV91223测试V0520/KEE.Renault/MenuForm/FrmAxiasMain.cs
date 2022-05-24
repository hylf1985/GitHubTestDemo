using csLTDMC;
using FontAwesome.Sharp;
using KEE.Renault.Common;
using KEE.Renault.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace KEE.Renault.MenuForm
{
    public partial class FrmAxiasMain : Form
    {
        public FrmAxiasMain()
        {
            InitializeComponent();
            InitLeftButton();
        }


        #region 变量声明
        private Panel leftBorderBtn;
        private IconButton currentBtn;
        private ushort curAxias = GlobalVar.InjectFeedAxiasNumber;
        #endregion

        #region 工具栏按钮切换状态变换的方法
        private struct RGBColors  //panelMenu工具栏图标、字体颜色
        {
            public static Color color1 = Color.FromArgb(172, 126, 241);//第一个按钮
            public static Color color2 = Color.FromArgb(249, 118, 176);//第二个按钮......
            public static Color color3 = Color.FromArgb(253, 138, 114);
            public static Color color4 = Color.FromArgb(95, 77, 221);
            public static Color color5 = Color.FromArgb(150, 133, 200);
            public static Color color6 = Color.FromArgb(223, 220, 150);
            public static Color color7 = Color.FromArgb(187, 110, 119);
            public static Color color8 = Color.FromArgb(61, 120, 120);
        }
        //方法
        private void ActicateButton(object senderBtn, Color color)
        {
            if (senderBtn != null) //判断按钮是否已被点击过
            {
                DisableButton(); //复位动作
                //按钮
                currentBtn = (IconButton)senderBtn; //标记按钮已被点击
                currentBtn.BackColor = Color.FromArgb(240, 240, 240);//背景色置位
                currentBtn.ForeColor = color;//前景色置位（字体）
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;//文字位置
                currentBtn.IconColor = color;//图标颜色置位
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;//文字在图片之前
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;//对齐方式
                //点击按钮后左边呈现彩色边框
                leftBorderBtn.BackColor = color;//左侧彩色边框颜色
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);//左侧彩色变宽个位置，X值越大越靠右
                leftBorderBtn.Visible = true; //true显示；false不显示
                leftBorderBtn.BringToFront();//置顶显示            
            }
        }
        //切换按钮时复位之前点击的按钮显示状态
        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(240, 240, 240); //背景色复位（底色）
                currentBtn.ForeColor = Color.Black; // 前景色复位(字体颜色)
                currentBtn.TextAlign = ContentAlignment.MiddleLeft; //文字位置复位
                currentBtn.IconColor = Color.Black; ;//图标颜色复位
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;//图片在文字之前
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;//对齐方式
            }
        }
        //切换窗体显示方法
        private void InitLeftButton()
        {
            leftBorderBtn = new Panel(); //实例化一个容器
            leftBorderBtn.Size = new Size(7, 60); //点击工具栏按钮时左侧条框长宽尺寸 
            pnlMenu.Controls.Add(leftBorderBtn);
        }
        #endregion

        private void FormDisplay(string frmName)
        {
            Form form = FormFactory.CreateForm(frmName);
            //this.IsMdiContainer = true;
            //form.MdiParent = (Form)this.Parent;
            //form.Parent = pnlDesktop;
            form.Dock = DockStyle.Fill;
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.WindowState = FormWindowState.Maximized;
            form.Dock = DockStyle.Fill;
            pnlDesktop.Controls.Add(form);
            pnlDesktop.Tag = form;
            form.BringToFront();
            form.TopMost = true;
            form.Show();
        }

        #region 窗体事件
        private void iBtnFeedFromInjectAxias_Click(object sender, EventArgs e)
        {
            ActicateButton(sender, RGBColors.color1);
            FormDisplay(((IconButton)sender).Tag.ToString());
            curAxias = GlobalVar.InjectFeedAxiasNumber;
        }

        private void iBtnFeedAxias_Click(object sender, EventArgs e)
        {
            ActicateButton(sender, RGBColors.color2);
            FormDisplay(((IconButton)sender).Tag.ToString());
            curAxias = GlobalVar.FeedAxiasNumber;
        }

        private void iBtnFeedRAxias_Click(object sender, EventArgs e)
        {
            ActicateButton(sender, RGBColors.color3);
            FormDisplay(((IconButton)sender).Tag.ToString());
            curAxias = GlobalVar.FeedRAxiasNumber;
        }

        private void iBtnLabelingAxias_Click(object sender, EventArgs e)
        {
            ActicateButton(sender, RGBColors.color4);
            FormDisplay(((IconButton)sender).Tag.ToString());
            curAxias = GlobalVar.LabelingAxiasNumber;
        }

        private void iBtnRobot_Click(object sender, EventArgs e)
        {
            ActicateButton(sender, RGBColors.color5);
            FormDisplay(((IconButton)sender).Tag.ToString());
        }

        private void iBtnTakeAxias_Click(object sender, EventArgs e)
        {
            ActicateButton(sender, RGBColors.color8);
            FormDisplay(((IconButton)sender).Tag.ToString());
            curAxias = GlobalVar.TakeAxiasNumber;
        }

        private void iBtnPressAndHighTest_Click(object sender, EventArgs e)
        {
            ActicateButton(sender, RGBColors.color6);
            FormDisplay(((IconButton)sender).Tag.ToString());
            //curAxias = GlobalVar.TakeAxiasNumber;
        }

        private void iBtnCamInd_Click(object sender, EventArgs e)
        {
            ActicateButton(sender, RGBColors.color7);
            FormDisplay(((IconButton)sender).Tag.ToString());
            //curAxias = GlobalVar.TakeAxiasNumber;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                double dcurrent_speed = LTDMC.dmc_read_current_speed(GlobalVar.CardId, curAxias); // 读取轴当前速度
                statusLabCurSpeed.Text = dcurrent_speed.ToString();
                int dunitPos = LTDMC.dmc_get_position(GlobalVar.CardId, curAxias); //读取指定轴指令位置值
                statusLabInsPos.Text = dunitPos.ToString();
                int encoderPos = LTDMC.dmc_get_encoder(GlobalVar.CardId, curAxias); //读取编码器反馈脉冲计数
                statusLabFeedBackPos.Text = encoderPos.ToString();
                if (LTDMC.dmc_check_done(GlobalVar.CardId, curAxias) == 0) // 读取指定轴运动状态
                {
                    statusLabMotionStatus.Text = "运行中";
                }
                else
                {
                    statusLabMotionStatus.Text = "停止中";
                    ushort homeRes = 2;
                LABEL:
                    if (GlobalVar.isHomeMotion)
                    {
                        LTDMC.dmc_get_home_result(GlobalVar.CardId, curAxias, ref homeRes);
                        if (homeRes == 1)
                        {
                            toolStripStausHomeV.Text = "回零完成";
                            LTDMC.dmc_set_position(GlobalVar.CardId, curAxias, 0);
                            LTDMC.dmc_set_encoder(GlobalVar.CardId, curAxias, 0);
                        }
                        else if (homeRes == 0)
                        {
                            toolStripStausHomeV.Text = "回零未完成";
                            goto LABEL;
                        }
                        GlobalVar.isHomeMotion = false;
                    }
                    if (homeRes == 2)
                    {
                        toolStripStausHomeV.Text = "--";
                    }
                }
                int stopReturnV = 0;
                LTDMC.dmc_get_stop_reason(GlobalVar.CardId, curAxias, ref stopReturnV);
                switch (stopReturnV)
                {
                    case 0:
                        { statusLabStopReason.Text = "正常停止"; }
                        break;
                    case 1:
                        { statusLabStopReason.Text = "ALM立即停止"; }
                        break;
                    case 2:
                        { statusLabStopReason.Text = "ALM减速停止"; }
                        break;
                    case 3:
                        { statusLabStopReason.Text = "LTC外部触发立即停止"; }
                        break;
                    case 4:
                        { statusLabStopReason.Text = "EMG立即停止"; }
                        break;
                    case 5:
                        { statusLabStopReason.Text = "正硬限位立即停止"; }
                        break;
                    case 6:
                        { statusLabStopReason.Text = "负硬限位立即停止"; }
                        break;
                    case 7:
                        { statusLabStopReason.Text = "正硬限位减速停止"; }
                        break;
                    case 8:
                        { statusLabStopReason.Text = "负硬限位减速停止"; }
                        break;
                    case 9:
                        { statusLabStopReason.Text = "正软限位立即停止"; }
                        break;
                    case 10:
                        { statusLabStopReason.Text = "负软限位立即停止"; }
                        break;
                    case 11:
                        { statusLabStopReason.Text = "正软限位减速停止"; }
                        break;
                    case 12:
                        { statusLabStopReason.Text = "负软限位减速停止"; }
                        break;
                    case 13:
                        { statusLabStopReason.Text = "命令立即停止"; }
                        break;
                    case 14:
                        { statusLabStopReason.Text = "命令减速停止"; }
                        break;
                    case 15:
                        { statusLabStopReason.Text = "其他原因立即停止"; }
                        break;
                    case 16:
                        { statusLabStopReason.Text = "其他原因减速停止"; }
                        break;
                    case 17:
                        { statusLabStopReason.Text = "未知原因立即停止"; }
                        break;
                    case 18:
                        { statusLabStopReason.Text = "未知原因减速停止"; }
                        break;
                    case 19:
                        { statusLabStopReason.Text = "外部IO减速停止"; }
                        break;
                }
                
            }));
        }

        private void FrmAxiasMain_Enter(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void FrmAxiasMain_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }
        #endregion

      
    }
}
