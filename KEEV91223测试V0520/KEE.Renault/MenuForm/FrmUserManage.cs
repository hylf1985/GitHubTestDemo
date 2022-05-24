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
    public partial class FrmUserManage : Form
    {
        public FrmUserManage()
        {
            InitializeComponent();
           // GlobalVar.temLevel = GlobalVar.Level.工程师;
        }
        #region 更改用户的变量，不允许更改用户名，一旦添加，用户名不再修改，除非删除
        private string selectedPwd = "";
        private GlobalVar.Level selectedPower =  0;

        #endregion

        private void JudgePower()
        {
            cmbPower.Items.Clear();
            switch (GlobalVar.temLevel)
            {
                case GlobalVar.Level.工程师:
                    {
                        cmbPower.Items.Add(GlobalVar.Level.操作员);
                        cmbPower.Items.Add(GlobalVar.Level.生技);
                        cmbPower.Items.Add(GlobalVar.Level.品质);
                        cmbPower.Items.Add(GlobalVar.Level.工程师);
                    }
                    break;
                case GlobalVar.Level.品质:
                    {
                        cmbPower.Items.Add(GlobalVar.Level.操作员);
                        cmbPower.Items.Add(GlobalVar.Level.品质);
                    }
                    break;
                case GlobalVar.Level.生技:
                    {
                        cmbPower.Items.Add(GlobalVar.Level.操作员);
                        cmbPower.Items.Add(GlobalVar.Level.生技);
                    }
                    break;
                default:
                    {
                        cmbPower.Items.Add(GlobalVar.Level.操作员);
                    }
                    break;
            }

        }

        private void FrmUserSet_Load(object sender, EventArgs e)
        {
            //GlobalVar.temLevel = GlobalVar.Level.管理员;
            JudgePower();
            UpdateUIGrid();
        }

        private void UpdateUIGrid()
        {
            DataTable dt1 = GlobalVar.mySqlDb.ExecuteDataTable("select ID as 'Index', UserName as '用户名' ,UserPwd as '密码',UserPower as '权限' from UserData order by ID asc", null);
            Grid_User.Columns.Clear();
            Grid_User.DataSource = dt1;
            if (Grid_User.Columns.Count == 4)
            {
                Grid_User.Columns[0].Width = 60;
                Grid_User.Columns[1].Width = 110;
                //Grid_User.Columns[2].Width = 110;
                Grid_User.Columns[2].Visible = false;
                Grid_User.Columns[2].Width = 110;
                Grid_User.Columns[3].Width = 110;
            }
            DataGridViewStyle.DgvStyle1(Grid_User);
            //让标题彻底居中，消除排序按钮的影响
            foreach (DataGridViewColumn item in Grid_User.Columns)
            {
                item.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            tb_UserID.BackColor = Color.WhiteSmoke;
            tb_UserPwd.BackColor = Color.WhiteSmoke;
        }

        private bool TextBoxIsEmpty(TextBox box)
        {
            if (string.IsNullOrWhiteSpace(box.Text))
            {
                box.Focus();
                box.BackColor = Color.OrangeRed;
                return true;
            }
            return false;
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (TextBoxIsEmpty(tb_UserID) || TextBoxIsEmpty(tb_UserPwd))
            {
                MessageBox.Show("请输入有效内容");
                return;
            }
            if (string.IsNullOrEmpty(cmbPower.Text))
            {
                MessageBox.Show("请选择一个权限种别");
                return;
            }
            object obj = GlobalVar.mySqlDb.ExecuteScalar("select top 1 ID from UserData where UserName='" + tb_UserID.Text.Trim() + "'", null);
            if (obj != null)
            {
                MessageBox.Show("此用户【" + tb_UserID.Text + "】已存在，序号为" + obj.ToString());
                return;
            }
            if (Convert.ToInt32(GlobalVar.mySqlDb.ExecuteNonQuery("insert into UserData (UserName,UserPwd,UserPower) values ('" + tb_UserID.Text.Trim() + "','" + tb_UserPwd.Text.Trim() + "'," + (int)((GlobalVar.Level)cmbPower.SelectedItem) + ")", null)) > 0)
            {
                tb_SelectedUser.Text = "";
                tb_UserID.Text = "";
                tb_UserPwd.Text = "";
                cmbPower.SelectedIndex = -1;
                UpdateUIGrid();
                MessageBox.Show("添加成功");
            }
        }

        private void btn_Modify_Click(object sender, EventArgs e)
        {
            if (TextBoxIsEmpty(tb_UserID) || TextBoxIsEmpty(tb_UserPwd))
            {
                MessageBox.Show("请输入有效内容");
                return;
            }
            if (string.IsNullOrEmpty(cmbPower.Text))
            {
                MessageBox.Show("请选择一个权限种别");
                return;
            }
            object obj = GlobalVar.mySqlDb.ExecuteScalar("select top 1 ID from UserData where UserName='" + tb_SelectedUser.Text.Trim() + "'", null);
            if (obj == null)
            {
                MessageBox.Show("此用户【" + tb_UserID.Text + "】不存在，无法进行修改");
                return;
            }
            if (selectedPwd == tb_UserPwd.Text.Trim() && selectedPower == (GlobalVar.Level)cmbPower.SelectedItem)
            {
                tb_UserPwd.Focus();
                MessageBox.Show("选中用户没有做任何修改");
                return;
            }
            if (Convert.ToInt32(GlobalVar.mySqlDb.ExecuteNonQuery("update UserData set UserName='" + tb_UserID.Text.Trim() + "',UserPwd='" + tb_UserPwd.Text.Trim() + "',UserPower = '" + (int)((GlobalVar.Level)cmbPower.SelectedItem) + "' where UserName='" + tb_SelectedUser.Text.Trim() + "'", null)) > 0)
            {
                tb_SelectedUser.Text = "";
                tb_UserID.Text = "";
                tb_UserPwd.Text = "";
                cmbPower.SelectedIndex = -1;
                UpdateUIGrid();
                MessageBox.Show("修改成功");
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (tb_SelectedUser.Text == "")
            {
                MessageBox.Show("请选择要删除的用户");
                return;
            }
            if (MessageBox.Show("确定要删除此用户【" + tb_SelectedUser.Text + "】吗？", "提醒", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (Convert.ToInt32(GlobalVar.mySqlDb.ExecuteNonQuery("delete from UserData where UserName='" + tb_SelectedUser.Text.Trim() + "'", null)) > 0)
                {
                    tb_SelectedUser.Text = "";
                    tb_UserID.Text = "";
                    tb_UserPwd.Text = "";
                    UpdateUIGrid();
                    MessageBox.Show("删除成功");
                }
            }
        }

        private void Grid_User_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
            try
            {
                int index = Grid_User.CurrentRow.Index;
                if ((GlobalVar.Level)Convert.ToInt16(Grid_User.Rows[index].Cells[3].Value) > GlobalVar.temLevel)
                {
                    MessageBox.Show("对不起，你没有修改该用户的权限");
                    return;
                }
                else
                {
                    selectedPower = (GlobalVar.Level)Convert.ToInt16(Grid_User.Rows[index].Cells[3].Value);
                    selectedPwd = Grid_User.Rows[index].Cells[2].Value.ToString();
                    tb_SelectedUser.Text = Grid_User.Rows[index].Cells[1].Value.ToString();
                    tb_UserID.Text = Grid_User.Rows[index].Cells[1].Value.ToString();
                    tb_UserPwd.Text = Grid_User.Rows[index].Cells[2].Value.ToString();
                    cmbPower.SelectedItem = selectedPower;
                }
            }
            catch (Exception ex)
            {
                GlobalVar.myLog.Error(ex.Message);
            }
          
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            UpdateUIGrid();
        }

        private void cmbPower_KeyUp(object sender, KeyEventArgs e)
        {
            cmbPower.Text = "";
            cmbPower.Focus();
        }

        private void Grid_User_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 2 || e.ColumnIndex == 3)
            {
                if (e.Value != null && e.Value.ToString().Length > 0)
                {
                    e.Value = new string('*', e.Value.ToString().Length);
                }
            }
        }

        private void FrmUserManage_Enter(object sender, EventArgs e)
        {
            JudgePower();
            UpdateUIGrid();
        }
    }
}
