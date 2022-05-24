using csIOC0640;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KEE.IODataGridView
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            InitIO640();
            GlobalVar.Initpara();
            GlobalVar.InitAxiasDI();
            GlobalVar.InitAxiasDO();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
        private void InitIO640()
        {
            if (IOC0640.ioc_board_init() <= 0)
            {
                MessageBox.Show("没有找到IO640卡");
            }
        }
       

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            IOC0640.ioc_board_close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                if (GlobalVar.lsAxiasDIs[7].IoPinStatus) { }
                if (GlobalVar.lsAxiasDIs[21].IoPinStatus) { }
                if (GlobalVar.lsAxiasDOs[0].IoPinStatus) { }
                
            }));

            
        }
    }
}
