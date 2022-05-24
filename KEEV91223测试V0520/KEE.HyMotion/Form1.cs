using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using csLTDMC;

namespace KEE.HyMotion
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region 变量声明
        /// <summary>
        /// 轴号
        /// </summary>
        private ushort _CardID = 0;
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            //获取轴卡数量，返回值：0，表示没有找到轴卡，或者控制卡异常，1-8：轴卡数量，负值：表明由2张或2张以上控制卡的硬件设置卡号相同
            short num = LTDMC.dmc_board_init();
            if (num<=0 ||num>8)
            {
                MessageBox.Show("初始卡失败!", "出错");
            }
            ushort _num = 0;
            ushort[] cardids = new ushort[8];
            uint[] cardtypes = new uint[8];
            short res = LTDMC.dmc_get_CardInfList(ref _num, cardtypes, cardids);
            if (res != 0)
            {
                MessageBox.Show("获取卡信息失败!");
            }
            _CardID = cardids[0];
            timer1.Start();
        }

        /// <summary>
        /// 获取选择的轴序号
        /// </summary>
        /// <returns></returns>
        private ushort GetAxis()
        {
            return Convert.ToUInt16(numericUpDown1.Value);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ushort axis = GetAxis(); //轴号
            double dcurrent_speed = 0;//速度值
            double dunitPos = 0; //


            dcurrent_speed = LTDMC.dmc_read_current_speed(_CardID, axis); // 读取轴当前速度
            textBox1.Text = dcurrent_speed.ToString();
            dunitPos = LTDMC.dmc_get_position(_CardID, axis); //读取指定轴指令位置值
            textBox2.Text = dunitPos.ToString();
            if (LTDMC.dmc_check_done(_CardID, axis) == 0) // 读取指定轴运动状态
            {
                textBox4.Text = "运行中";
            }
            else
            {
                textBox4.Text = "停止中";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ushort axis = GetAxis(); //轴号

            double dStartVel;//起始速度
            double dMaxVel;//运行速度
            double dTacc;//加速时间
            double dTdec;//减速时间
            double dStopVel;//停止速度
            double dS_para;//S段时间
            int dDist;//目标位置
            ushort sPosi_mode; //运动模式0：相对坐标模式，1：绝对坐标模式


            dStartVel = decimal.ToDouble(numericUpDown3.Value);
            dMaxVel = decimal.ToDouble(numericUpDown4.Value);
            dTacc = decimal.ToDouble(numericUpDown5.Value);
            dTdec = decimal.ToDouble(numericUpDown6.Value);
            dStopVel = decimal.ToDouble(numericUpDown8.Value);
            dS_para = decimal.ToDouble(numericUpDown7.Value);
            dDist = decimal.ToInt32(numericUpDown9.Value);
            sPosi_mode = 0;



            LTDMC.dmc_set_profile(_CardID, axis, dStartVel, dMaxVel, dTacc, dTdec, dStopVel);//设置速度参数

            LTDMC.dmc_set_s_profile(_CardID, axis, 0, dS_para);//设置S段速度参数

            LTDMC.dmc_set_dec_stop_time(_CardID, axis, dTdec); //设置减速停止时间

            LTDMC.dmc_pmove(_CardID, axis, dDist, sPosi_mode);//定长运动
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ushort axis = GetAxis(); //轴号
            ushort stop_mode = 0; //制动方式，0：减速停止，1：紧急停止

            LTDMC.dmc_stop(_CardID, axis, stop_mode);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ushort axis = GetAxis(); //轴号
            int dpos = 0;// 当前位置

            LTDMC.dmc_set_position(_CardID, axis, dpos); //设置指定轴的当前指令位置值
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ushort axis = GetAxis(); //轴号
            double dNewVel;// 新的运行速度
            double dTaccDec;//变速时间

            dNewVel = decimal.ToDouble(numericUpDown10.Value);
            dTaccDec = decimal.ToDouble(numericUpDown11.Value);

            LTDMC.dmc_change_speed(_CardID, axis, dNewVel, dTaccDec); //在线变速
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ushort axis = GetAxis(); //轴号
            int dNewPos;// 新的目标位置

            dNewPos = decimal.ToInt32(numericUpDown12.Value);

            LTDMC.dmc_reset_target_position(_CardID, axis, dNewPos, 1);//在线变位
        }

        private void button8_MouseDown(object sender, MouseEventArgs e)
        {
            ushort axis = GetAxis(); //轴号

            double dStartVel;//起始速度
            double dMaxVel;//运行速度
            double dTacc;//加速时间
            double dTdec;//减速时间
            double dStopVel;//停止速度
            double dS_para;//S段时间
            int dDist;//目标位置


            dStartVel = decimal.ToDouble(numericUpDown3.Value);
            dMaxVel = decimal.ToDouble(numericUpDown4.Value);
            dTacc = decimal.ToDouble(numericUpDown5.Value);
            dTdec = decimal.ToDouble(numericUpDown6.Value);
            dStopVel = decimal.ToDouble(numericUpDown8.Value);
            dS_para = decimal.ToDouble(numericUpDown7.Value);
            dDist = decimal.ToInt32(numericUpDown9.Value);



            LTDMC.dmc_set_profile(_CardID, axis, dStartVel, dMaxVel, dTacc, dTdec, dStopVel);//设置速度参数

            LTDMC.dmc_set_s_profile(_CardID, axis, 0, dS_para);//设置S段速度参数

            LTDMC.dmc_vmove(_CardID, axis, 1);
        }

        private void button8_MouseUp(object sender, MouseEventArgs e)
        {
            LTDMC.dmc_stop(_CardID, 0, 0);
        }

        private void button6_MouseDown(object sender, MouseEventArgs e)
        {
            ushort axis = GetAxis(); //轴号

            double dStartVel;//起始速度
            double dMaxVel;//运行速度
            double dTacc;//加速时间
            double dTdec;//减速时间
            double dStopVel;//停止速度
            double dS_para;//S段时间
            int dDist;//目标位置


            dStartVel = decimal.ToDouble(numericUpDown3.Value);
            dMaxVel = decimal.ToDouble(numericUpDown4.Value);
            dTacc = decimal.ToDouble(numericUpDown5.Value);
            dTdec = decimal.ToDouble(numericUpDown6.Value);
            dStopVel = decimal.ToDouble(numericUpDown8.Value);
            dS_para = decimal.ToDouble(numericUpDown7.Value);
            dDist = decimal.ToInt32(numericUpDown9.Value);



            LTDMC.dmc_set_profile(_CardID, axis, dStartVel, dMaxVel, dTacc, dTdec, dStopVel);//设置速度参数

            LTDMC.dmc_set_s_profile(_CardID, axis, 0, dS_para);//设置S段速度参数

            LTDMC.dmc_vmove(_CardID, axis, 0);
        }

        private void button6_MouseUp(object sender, MouseEventArgs e)
        {
            LTDMC.dmc_stop(_CardID, 0, 0);
        }
    }
}
