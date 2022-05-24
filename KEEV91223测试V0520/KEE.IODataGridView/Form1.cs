using HY_Data;
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
using csIOC0640;

namespace KEE.IODataGridView
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            //InitIO640();
            //GlobalVar.Initpara();
            //InitAxiasDI();
            //InitAxiasDO();
            SetDataGridViewBuffer(dataGridView1);
            SetDataGridViewBuffer(dataGridView2);
        }

        

        private void RefeshedDGV()
        {
            if (dataGridView1.DataSource!=null)
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

        private void Form1_Load(object sender, EventArgs e)
        {
            RefeshedDGV();

        }
       
        bool ioSwitch = false;
        

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

        /// <summary>
        /// 使DataGridView的列自适应宽度
        /// </summary>
        /// <param name="dgViewFiles"></param>
        private void AutoSizeColumn(DataGridView dgViewFiles)
        {
            int width = 0;
            //使列自使用宽度
            //对于DataGridView的每一个列都调整
            for (int i = 0; i < dgViewFiles.Columns.Count; i++)
            {
                //将每一列都调整为自动适应模式
                dgViewFiles.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);
                //记录整个DataGridView的宽度
                width += dgViewFiles.Columns[i].Width;
            }
            //判断调整后的宽度与原来设定的宽度的关系，如果是调整后的宽度大于原来设定的宽度，
            //则将DataGridView的列自动调整模式设置为显示的列即可，
            //如果是小于原来设定的宽度，将模式改为填充。
            if (width > dgViewFiles.Size.Width)
            {
                dgViewFiles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            }
            else
            {
                dgViewFiles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            //冻结某列 从左开始 0，1，2
            dgViewFiles.Columns[1].Frozen = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            this.Invoke(new Action(() =>
            {
                pictureBox20.Image = GlobalVar.lsAxiasDIs[7].PinStatus;
                this.dataGridView1.Refresh();
                this.dataGridView2.Refresh();
                Application.DoEvents();
                if (!ioSwitch)
                {
                    ioSwitch = true;
                    IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[0].Card - 1), GlobalVar.lsAxiasDOs[0].PinDefinition, 0);
                }
                else
                {
                    ioSwitch = false;
                    IOC0640.ioc_write_outbit((ushort)((int)GlobalVar.lsAxiasDOs[0].Card - 1), GlobalVar.lsAxiasDOs[0].PinDefinition, 1);
                }
            }));
        }

        private void SetDataGridViewBuffer(DataGridView dgv)
        {
            if (dgv!=null)
            {
                Type dgvType = dgv.GetType();
                PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(dgv, true, null);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //IOC0640.ioc_board_close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LsIODIPinDefinition.Resetid();
            LsIODOPinDefinition.Resetid();
            GlobalVar.Initpara();
            GlobalVar.InitAxiasDI();
            GlobalVar.InitAxiasDO();
            RefeshedDGV();
            //Form2 frm = new Form2();
            //frm.Show();
        }
    }
}
