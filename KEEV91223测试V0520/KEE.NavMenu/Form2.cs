using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KEE.NavMenu
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        RisingTrig risingTrig  = new RisingTrig();
        FallTrig fallTrig = new FallTrig();

        private void timer1_Tick(object sender, EventArgs e)
        {
            BeginInvoke(new Action(() => 
            {
                lblR_TRIGRes.Text = risingTrig.Q.ToString();
                lblF_TRIGRes.Text = fallTrig.Q.ToString();
            }));
        }


        private void btnR_TRIG_0_Click(object sender, EventArgs e)
        {
            risingTrig.CLK = false;
            lblR_TRIGRes.Text = risingTrig.Q.ToString();
        }

        private void btnR_TRIG_1_Click(object sender, EventArgs e)
        {
            risingTrig.CLK = true;
            lblR_TRIGRes.Text = risingTrig.Q.ToString();
        }

        private void btnF_TRIG_0_Click(object sender, EventArgs e)
        {
            fallTrig.CLK = false;
            lblF_TRIGRes.Text = fallTrig.Q.ToString();
        }

        private void btnF_TRIG_1_Click(object sender, EventArgs e)
        {
            fallTrig.CLK = true;
            lblF_TRIGRes.Text = fallTrig.Q.ToString();
        }
        bool test = false;
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (!test)
            {
                test = true; btnF_TRIG_0_Click(null, null);
            }
            else
            { test = false; btnF_TRIG_1_Click(null, null); }
        }
    }
}
