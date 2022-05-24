using HY.Redis.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KEE.TestPrintService
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int cnt = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            label2.Text = (++cnt).ToString();
            using (RedisListService service = new RedisListService())
            {
                service.Publish("PrintServices", $"{textBox1.Text}");
            }
        }
    }
}
