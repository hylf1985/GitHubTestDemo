using KEE.Renault.Common;
using KEE.Renault.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KEE.Renault.MenuForm
{
    public partial class FrmIOMonitor : Form
    {
        public FrmIOMonitor()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            SetDataGridViewBuffer(dataGridView1);
            SetDataGridViewBuffer(dataGridView2);
        }

        #region datagridview操作
        private void RefeshedDGV()
        {
            if (dataGridView1.DataSource != null)
            {
                dataGridView1.DataSource = null;
            }
            if (dataGridView2.DataSource != null)
            {
                dataGridView2.DataSource = null;
            }
            dataGridView1.DataSource = GlobalVar.lsAxiasDIs;
            dataGridView1.DgvStyle1();
            ChangeHeaderText(dataGridView1);
            dataGridView2.DataSource = GlobalVar.lsAxiasDOs;
            dataGridView2.DgvStyle1();
            ChangeHeaderText(dataGridView2);
        }
        private void ChangeHeaderText(DataGridView dgv)
        {
            if (dgv != null)
            {
                dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns["ID"].ReadOnly = true;
                dgv.Columns["ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns["ID"].Width = 60;
                dgv.Columns["Card"].ReadOnly = true;
                dgv.Columns["Card"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns["Card"].Width = 50;
                dgv.Columns["PinDefinition"].ReadOnly = true;
                dgv.Columns["PinDefinition"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns["PinDefinition"].Width = 100;
                dgv.Columns["IoPinStatus"].ReadOnly = true;
                dgv.Columns["IoPinStatus"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns["IoPinStatus"].Width = 60;
                dgv.Columns["IoPinStatus"].Visible = false;
                dgv.Columns["CurIOStatus"].ReadOnly = true;
                dgv.Columns["CurIOStatus"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns["CurIOStatus"].Width = 60;
                dgv.Columns["CurIOStatus"].Visible = false;
                dgv.Columns["PinStatus"].ReadOnly = true;
                dgv.Columns["PinStatus"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns["PinStatus"].Width = 50;
                dgv.Columns["PinDefinitionName"].ReadOnly = true;
                dgv.Columns["PinDefinitionName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns["PinDefinitionName"].Width = 200;
                dgv.Columns[0].HeaderText = "序号";
                dgv.Columns[1].HeaderText = "卡号";
                dgv.Columns[2].HeaderText = "IO定义";
                dgv.Columns[3].HeaderText = "IO实时状态";
                dgv.Columns[4].HeaderText = "IO状态";
                dgv.Columns[5].HeaderText = "状态";
                dgv.Columns[6].HeaderText = "接脚定义名称";

            }
        }
        private void SetDataGridViewBuffer(DataGridView dgv)
        {
            if (dgv != null)
            {
                Type dgvType = dgv.GetType();
                PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(dgv, true, null);
            }
        }
        /// <summary>
        /// 如果IO有更新，后续根据实际情况开发此功能
        /// </summary>
        private void ResetIOTable()
        {
            LsIODIPinDefinition.Resetid();
            LsIODOPinDefinition.Resetid();
            GlobalVar.InitIOPara();
            GlobalVar.InitAxiasDI();
            GlobalVar.InitAxiasDO();
            RefeshedDGV();
        }
        #endregion 

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                this.dataGridView1.Refresh();
                this.dataGridView2.Refresh();
                Application.DoEvents();
            }));
        }

        private void FrmIOMonitor_Enter(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void FrmIOMonitor_Load(object sender, EventArgs e)
        {
            RefeshedDGV();
        }

        private void FrmIOMonitor_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }
    }
}
