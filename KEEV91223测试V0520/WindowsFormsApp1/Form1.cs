using RegalPrinter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Ring r = new Ring();
            if (!r.PrinterWorkOffline("Ring 4012PIM"))
            {
                r.StartPrinter("Ring 4012PIM", "ring");
                r.FMT(1, "30", "8", false, 0, 1);//设定标签纸的长和宽
                r.DMD(1);
                r.DPD(1);
                r.ACL();
                r.FAG(2);
                r.DMX("14.3","6", 24, 24, 3, 6, 3, 0, "1232131");
                r.PRT(1, 0, 1);
                r.EndPrinter();
            }
        }
    }
}
