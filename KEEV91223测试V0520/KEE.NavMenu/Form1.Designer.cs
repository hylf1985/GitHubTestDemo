
namespace KEE.NavMenu
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
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.txtBoxYOffset = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBoxXOffset = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cmbClass = new System.Windows.Forms.ComboBox();
            this.txtBoxWO = new System.Windows.Forms.TextBox();
            this.txtBoxSN = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.txtUploadFolder = new System.Windows.Forms.TextBox();
            this.btnClearAlarm = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button5 = new System.Windows.Forms.Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(6, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(137, 10);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(73, 21);
            this.textBox1.TabIndex = 1;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(137, 50);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(305, 21);
            this.textBox2.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(240, 8);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 500;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(443, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 18;
            this.label2.Text = "Y偏移 : ";
            // 
            // txtBoxYOffset
            // 
            this.txtBoxYOffset.Location = new System.Drawing.Point(500, 129);
            this.txtBoxYOffset.Name = "txtBoxYOffset";
            this.txtBoxYOffset.Size = new System.Drawing.Size(100, 21);
            this.txtBoxYOffset.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(443, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 16;
            this.label1.Text = "X偏移 : ";
            // 
            // txtBoxXOffset
            // 
            this.txtBoxXOffset.Location = new System.Drawing.Point(500, 102);
            this.txtBoxXOffset.Name = "txtBoxXOffset";
            this.txtBoxXOffset.Size = new System.Drawing.Size(100, 21);
            this.txtBoxXOffset.TabIndex = 15;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(562, 77);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 18);
            this.textBox3.TabIndex = 14;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(623, 101);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 49);
            this.button3.TabIndex = 13;
            this.button3.Text = "打印";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 263);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(676, 78);
            this.dataGridView1.TabIndex = 19;
            this.dataGridView1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDoubleClick);
            // 
            // cmbClass
            // 
            this.cmbClass.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.cmbClass.FormattingEnabled = true;
            this.cmbClass.Location = new System.Drawing.Point(467, 227);
            this.cmbClass.Name = "cmbClass";
            this.cmbClass.Size = new System.Drawing.Size(183, 27);
            this.cmbClass.TabIndex = 20;
            // 
            // txtBoxWO
            // 
            this.txtBoxWO.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.txtBoxWO.Location = new System.Drawing.Point(467, 199);
            this.txtBoxWO.Name = "txtBoxWO";
            this.txtBoxWO.Size = new System.Drawing.Size(183, 25);
            this.txtBoxWO.TabIndex = 22;
            // 
            // txtBoxSN
            // 
            this.txtBoxSN.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.txtBoxSN.Location = new System.Drawing.Point(467, 171);
            this.txtBoxSN.Name = "txtBoxSN";
            this.txtBoxSN.Size = new System.Drawing.Size(183, 25);
            this.txtBoxSN.TabIndex = 21;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.Control;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Location = new System.Drawing.Point(562, 347);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 23;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // txtUploadFolder
            // 
            this.txtUploadFolder.Location = new System.Drawing.Point(40, 349);
            this.txtUploadFolder.Name = "txtUploadFolder";
            this.txtUploadFolder.Size = new System.Drawing.Size(473, 21);
            this.txtUploadFolder.TabIndex = 24;
            this.txtUploadFolder.Text = "\\\\192.168.3.107\\文件服务器\\公用\\test";
            // 
            // btnClearAlarm
            // 
            this.btnClearAlarm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearAlarm.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClearAlarm.Image = global::KEE.NavMenu.Properties.Resources.报警解除填充;
            this.btnClearAlarm.Location = new System.Drawing.Point(204, 101);
            this.btnClearAlarm.Name = "btnClearAlarm";
            this.btnClearAlarm.Size = new System.Drawing.Size(156, 156);
            this.btnClearAlarm.TabIndex = 12;
            this.btnClearAlarm.Text = "清除报警";
            this.btnClearAlarm.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnClearAlarm.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClearAlarm.UseVisualStyleBackColor = true;
            // 
            // btnPause
            // 
            this.btnPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPause.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPause.Image = global::KEE.NavMenu.Properties.Resources.停止;
            this.btnPause.Location = new System.Drawing.Point(60, 101);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(140, 156);
            this.btnPause.TabIndex = 11;
            this.btnPause.Text = "暂  停";
            this.btnPause.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnPause.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPause.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(80, 396);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(175, 184);
            this.listBox1.TabIndex = 25;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(488, 396);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(112, 28);
            this.button5.TabIndex = 26;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 12;
            this.listBox2.Location = new System.Drawing.Point(267, 396);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(175, 184);
            this.listBox2.TabIndex = 27;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 983);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.txtUploadFolder);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.cmbClass);
            this.Controls.Add(this.txtBoxWO);
            this.Controls.Add(this.txtBoxSN);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBoxYOffset);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBoxXOffset);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnClearAlarm);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Button btnClearAlarm;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBoxYOffset;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBoxXOffset;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cmbClass;
        private System.Windows.Forms.TextBox txtBoxWO;
        private System.Windows.Forms.TextBox txtBoxSN;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox txtUploadFolder;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ListBox listBox2;
    }
}

