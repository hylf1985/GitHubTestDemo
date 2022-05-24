
namespace KEE.PrintService
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txBoxEndSn = new System.Windows.Forms.TextBox();
            this.txBoxStartSn = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSavePrintParam = new System.Windows.Forms.Button();
            this.txtPrintY = new System.Windows.Forms.TextBox();
            this.txtPrintX = new System.Windows.Forms.TextBox();
            this.tbPrintContext = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lsvRevicedMsg = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txBoxEndSn);
            this.groupBox1.Controls.Add(this.txBoxStartSn);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnSavePrintParam);
            this.groupBox1.Controls.Add(this.txtPrintY);
            this.groupBox1.Controls.Add(this.txtPrintX);
            this.groupBox1.Controls.Add(this.tbPrintContext);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnPrint);
            this.groupBox1.Font = new System.Drawing.Font("幼圆", 14F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(9, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(779, 247);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "打印机参数设定";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(37, 197);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(506, 21);
            this.label6.TabIndex = 36;
            this.label6.Text = "提示 : 打印流水号区间功能只能在设备不运行时使用";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(366, 171);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(188, 19);
            this.label5.TabIndex = 35;
            this.label5.Text = "流水号结束数字 ：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 171);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(188, 19);
            this.label4.TabIndex = 34;
            this.label4.Text = "流水号起始数字 ：";
            // 
            // txBoxEndSn
            // 
            this.txBoxEndSn.Location = new System.Drawing.Point(569, 166);
            this.txBoxEndSn.Name = "txBoxEndSn";
            this.txBoxEndSn.Size = new System.Drawing.Size(100, 28);
            this.txBoxEndSn.TabIndex = 33;
            this.txBoxEndSn.Tag = "printY";
            // 
            // txBoxStartSn
            // 
            this.txBoxStartSn.Location = new System.Drawing.Point(225, 166);
            this.txBoxStartSn.Name = "txBoxStartSn";
            this.txBoxStartSn.Size = new System.Drawing.Size(112, 28);
            this.txBoxStartSn.TabIndex = 32;
            this.txBoxStartSn.Tag = "printX";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(188, 19);
            this.label3.TabIndex = 31;
            this.label3.Text = "二维码标签抬头 ：";
            // 
            // btnSavePrintParam
            // 
            this.btnSavePrintParam.Location = new System.Drawing.Point(517, 67);
            this.btnSavePrintParam.Name = "btnSavePrintParam";
            this.btnSavePrintParam.Size = new System.Drawing.Size(88, 95);
            this.btnSavePrintParam.TabIndex = 30;
            this.btnSavePrintParam.Text = "保存";
            this.btnSavePrintParam.UseVisualStyleBackColor = true;
            this.btnSavePrintParam.Click += new System.EventHandler(this.btnSavePrintParam_Click);
            // 
            // txtPrintY
            // 
            this.txtPrintY.Location = new System.Drawing.Point(320, 102);
            this.txtPrintY.Name = "txtPrintY";
            this.txtPrintY.Size = new System.Drawing.Size(100, 28);
            this.txtPrintY.TabIndex = 29;
            this.txtPrintY.Tag = "printY";
            // 
            // txtPrintX
            // 
            this.txtPrintX.Location = new System.Drawing.Point(320, 65);
            this.txtPrintX.Name = "txtPrintX";
            this.txtPrintX.Size = new System.Drawing.Size(100, 28);
            this.txtPrintX.TabIndex = 28;
            this.txtPrintX.Tag = "printX";
            // 
            // tbPrintContext
            // 
            this.tbPrintContext.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbPrintContext.Location = new System.Drawing.Point(225, 138);
            this.tbPrintContext.Name = "tbPrintContext";
            this.tbPrintContext.Size = new System.Drawing.Size(286, 21);
            this.tbPrintContext.TabIndex = 27;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(31, 31);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(752, 19);
            this.label8.TabIndex = 26;
            this.label8.Text = "先在Bartender上调试好位置，再用打印机上导出位置数据（换算关系1mm=24d）";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(253, 19);
            this.label2.TabIndex = 25;
            this.label2.Text = "Y轴起点(值越小越靠前): ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(242, 19);
            this.label1.TabIndex = 24;
            this.label1.Text = "X轴起点(值越小越靠右):";
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(609, 67);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(165, 95);
            this.btnPrint.TabIndex = 21;
            this.btnPrint.Text = "打印";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lsvRevicedMsg);
            this.groupBox2.Location = new System.Drawing.Point(9, 257);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(779, 266);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "条码队列";
            // 
            // lsvRevicedMsg
            // 
            this.lsvRevicedMsg.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
            this.lsvRevicedMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvRevicedMsg.FullRowSelect = true;
            this.lsvRevicedMsg.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lsvRevicedMsg.HideSelection = false;
            this.lsvRevicedMsg.Location = new System.Drawing.Point(3, 17);
            this.lsvRevicedMsg.Name = "lsvRevicedMsg";
            this.lsvRevicedMsg.ShowItemToolTips = true;
            this.lsvRevicedMsg.Size = new System.Drawing.Size(773, 246);
            this.lsvRevicedMsg.TabIndex = 12;
            this.lsvRevicedMsg.UseCompatibleStateImageBehavior = false;
            this.lsvRevicedMsg.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "时间";
            this.columnHeader3.Width = 200;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "条码内容";
            this.columnHeader4.Width = 200;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 529);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "打印机服务";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSavePrintParam;
        private System.Windows.Forms.TextBox txtPrintY;
        private System.Windows.Forms.TextBox txtPrintX;
        private System.Windows.Forms.TextBox tbPrintContext;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lsvRevicedMsg;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txBoxEndSn;
        private System.Windows.Forms.TextBox txBoxStartSn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
    }
}

