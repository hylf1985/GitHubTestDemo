using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KEE.Renault
{
    public partial class FrmUserLogin : Form
    {
        public string mPassword = "123456";
        public string mUserID = "admin";
        public FrmUserLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tb_UserName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if ((txtID.Text.Length == 0) || (txtPassWord.Text.Length == 0))
                    {
                        MessageBox.Show("使用者名称或密码不能为空");
                    }
                    else
                    {
                        mUserID = txtID.Text;
                        mPassword = txtPassWord.Text;
                        Close();
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if ((txtID.Text.Length == 0) || (txtPassWord.Text.Length == 0))
                {
                    MessageBox.Show("密码不能为空");
                }
                else
                {
                    mUserID = txtID.Text;
                    mPassword = txtPassWord.Text;
                    Close();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void txtPassWord_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if ((txtID.Text.Length == 0) || (txtPassWord.Text.Length == 0))
                    {
                        MessageBox.Show("使用者名称或密码不能为空");
                    }
                    else
                    {
                        mUserID = txtID.Text;
                        mPassword = txtPassWord.Text;
                        Close();
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
