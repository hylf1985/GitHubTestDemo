using KEE.Renault.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KEE.Renault
{
    public partial class frmNotify : Form
    {
        public bool mFlicked;

        public frmNotify()
        {
            InitializeComponent();
        }
        
        public void SetMsg(string MSG)
        {
            try
            {
                txtMsg.Text= MSG;
                txtMsg.Select(0,0);
            }
            catch (Exception ex)
            {
                GlobalVar.myLog.Error($"SetMsg出现错误：{ex.Message}");
            }
        }

        private void tmrflicker_Tick(object sender, EventArgs e)
        {
            mFlicked= ! mFlicked;
            if (mFlicked)
            {
                txtMsg.BackColor= Color.Yellow;
            }
            else
            {
                txtMsg.BackColor= Color.FromKnownColor(KnownColor.Control);
            }
        }

        private void frmNotify_FormClosing(object sender, FormClosingEventArgs e)
        {
            tmrflicker.Enabled = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
