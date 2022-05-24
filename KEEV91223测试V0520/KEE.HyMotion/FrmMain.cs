using KEE.HyMotion.Common;
using KEE.HyMotion.MyMotion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KEE.HyMotion
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            LSParamCommonInit.InitAxiasEnums();
            Form form = FormFactory.CreateForm("FrmFeedFromInjectAxias");
            form.MdiParent = this;
            form.Parent = splitContainer2.Panel2;
            form.Show();
        }

        private void trvMenu_AfterSelect(object sender, TreeViewEventArgs e)
        {
            foreach (TreeNode node in trvMenu.Nodes)
            {
                node.BackColor = Color.White;
                node.ForeColor = Color.Black;
            }
            e.Node.BackColor = SystemColors.Highlight;
            e.Node.ForeColor = Color.White;

            Form form = FormFactory.CreateForm(e.Node.Tag?.ToString());
            form.MdiParent = this;
            form.Parent = splitContainer2.Panel2;
            form.Show();
        }
    }
}
