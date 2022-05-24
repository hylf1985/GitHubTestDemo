
namespace KEE.LightControl
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnExcuteCmd = new System.Windows.Forms.Button();
            this.txtBoxBrightVal = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.traBarBrightVal = new System.Windows.Forms.TrackBar();
            this.cmbLightCtrCH = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbLightCtrCMD = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtBoxCH3BrightVal = new System.Windows.Forms.TextBox();
            this.txtBoxCH2BrightVal = new System.Windows.Forms.TextBox();
            this.txtBoxCH1BrightVal = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtBoxCH3Close = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtBoxCH3Open = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtBoxCH2Close = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtBoxCH2Open = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBoxCH1Close = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbCom = new System.Windows.Forms.ComboBox();
            this.txtBoxCH1Open = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.traBarBrightVal)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnExcuteCmd);
            this.groupBox1.Controls.Add(this.txtBoxBrightVal);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.traBarBrightVal);
            this.groupBox1.Controls.Add(this.cmbLightCtrCH);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmbLightCtrCMD);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(556, 129);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "光源控制器操作";
            // 
            // btnExcuteCmd
            // 
            this.btnExcuteCmd.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.btnExcuteCmd.Location = new System.Drawing.Point(236, 34);
            this.btnExcuteCmd.Name = "btnExcuteCmd";
            this.btnExcuteCmd.Size = new System.Drawing.Size(101, 36);
            this.btnExcuteCmd.TabIndex = 7;
            this.btnExcuteCmd.Text = "执行命令";
            this.btnExcuteCmd.UseVisualStyleBackColor = true;
            this.btnExcuteCmd.Click += new System.EventHandler(this.btnExcuteCmd_Click);
            // 
            // txtBoxBrightVal
            // 
            this.txtBoxBrightVal.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtBoxBrightVal.Location = new System.Drawing.Point(437, 80);
            this.txtBoxBrightVal.Name = "txtBoxBrightVal";
            this.txtBoxBrightVal.Size = new System.Drawing.Size(107, 32);
            this.txtBoxBrightVal.TabIndex = 6;
            this.txtBoxBrightVal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBoxBrightVal_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label3.Location = new System.Drawing.Point(19, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "亮度 : ";
            // 
            // traBarBrightVal
            // 
            this.traBarBrightVal.Location = new System.Drawing.Point(86, 74);
            this.traBarBrightVal.Maximum = 255;
            this.traBarBrightVal.Name = "traBarBrightVal";
            this.traBarBrightVal.Size = new System.Drawing.Size(345, 45);
            this.traBarBrightVal.TabIndex = 4;
            this.traBarBrightVal.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.traBarBrightVal.ValueChanged += new System.EventHandler(this.traBarBrightVal_ValueChanged);
            // 
            // cmbLightCtrCH
            // 
            this.cmbLightCtrCH.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.cmbLightCtrCH.FormattingEnabled = true;
            this.cmbLightCtrCH.Location = new System.Drawing.Point(412, 37);
            this.cmbLightCtrCH.Name = "cmbLightCtrCH";
            this.cmbLightCtrCH.Size = new System.Drawing.Size(132, 33);
            this.cmbLightCtrCH.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label2.Location = new System.Drawing.Point(341, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "通道 : ";
            // 
            // cmbLightCtrCMD
            // 
            this.cmbLightCtrCMD.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.cmbLightCtrCMD.FormattingEnabled = true;
            this.cmbLightCtrCMD.Location = new System.Drawing.Point(92, 36);
            this.cmbLightCtrCMD.Name = "cmbLightCtrCMD";
            this.cmbLightCtrCMD.Size = new System.Drawing.Size(141, 33);
            this.cmbLightCtrCMD.TabIndex = 1;
            this.cmbLightCtrCMD.SelectedIndexChanged += new System.EventHandler(this.cmbLightCtrCMD_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label1.Location = new System.Drawing.Point(19, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "命令 : ";
            // 
            // serialPort1
            // 
            this.serialPort1.PortName = "COM7";
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.txtBoxCH3BrightVal);
            this.groupBox2.Controls.Add(this.txtBoxCH2BrightVal);
            this.groupBox2.Controls.Add(this.txtBoxCH1BrightVal);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txtBoxCH3Close);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtBoxCH3Open);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtBoxCH2Close);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtBoxCH2Open);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtBoxCH1Close);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cmbCom);
            this.groupBox2.Controls.Add(this.txtBoxCH1Open);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.groupBox2.Location = new System.Drawing.Point(12, 147);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(556, 333);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "光源控制器配置参数设定";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(199, 298);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(141, 29);
            this.textBox1.TabIndex = 23;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(390, 268);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 59);
            this.button1.TabIndex = 22;
            this.button1.Text = "保存光源控制器参数配置";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // txtBoxCH3BrightVal
            // 
            this.txtBoxCH3BrightVal.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtBoxCH3BrightVal.Location = new System.Drawing.Point(390, 226);
            this.txtBoxCH3BrightVal.Name = "txtBoxCH3BrightVal";
            this.txtBoxCH3BrightVal.ReadOnly = true;
            this.txtBoxCH3BrightVal.Size = new System.Drawing.Size(117, 32);
            this.txtBoxCH3BrightVal.TabIndex = 21;
            // 
            // txtBoxCH2BrightVal
            // 
            this.txtBoxCH2BrightVal.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtBoxCH2BrightVal.Location = new System.Drawing.Point(390, 151);
            this.txtBoxCH2BrightVal.Name = "txtBoxCH2BrightVal";
            this.txtBoxCH2BrightVal.ReadOnly = true;
            this.txtBoxCH2BrightVal.Size = new System.Drawing.Size(117, 32);
            this.txtBoxCH2BrightVal.TabIndex = 20;
            // 
            // txtBoxCH1BrightVal
            // 
            this.txtBoxCH1BrightVal.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtBoxCH1BrightVal.Location = new System.Drawing.Point(390, 76);
            this.txtBoxCH1BrightVal.Name = "txtBoxCH1BrightVal";
            this.txtBoxCH1BrightVal.ReadOnly = true;
            this.txtBoxCH1BrightVal.Size = new System.Drawing.Size(117, 32);
            this.txtBoxCH1BrightVal.TabIndex = 19;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label11.Location = new System.Drawing.Point(354, 44);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(196, 25);
            this.label11.TabIndex = 18;
            this.label11.Text = "各通道亮度值 (0-255)";
            // 
            // txtBoxCH3Close
            // 
            this.txtBoxCH3Close.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtBoxCH3Close.Location = new System.Drawing.Point(199, 264);
            this.txtBoxCH3Close.Name = "txtBoxCH3Close";
            this.txtBoxCH3Close.ReadOnly = true;
            this.txtBoxCH3Close.Size = new System.Drawing.Size(141, 32);
            this.txtBoxCH3Close.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label9.Location = new System.Drawing.Point(12, 268);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(181, 25);
            this.label9.TabIndex = 16;
            this.label9.Text = "第三通道关闭命令 : ";
            // 
            // txtBoxCH3Open
            // 
            this.txtBoxCH3Open.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtBoxCH3Open.Location = new System.Drawing.Point(199, 226);
            this.txtBoxCH3Open.Name = "txtBoxCH3Open";
            this.txtBoxCH3Open.ReadOnly = true;
            this.txtBoxCH3Open.Size = new System.Drawing.Size(141, 32);
            this.txtBoxCH3Open.TabIndex = 15;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label10.Location = new System.Drawing.Point(12, 230);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(181, 25);
            this.label10.TabIndex = 14;
            this.label10.Text = "第三通道打开命令 : ";
            // 
            // txtBoxCH2Close
            // 
            this.txtBoxCH2Close.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtBoxCH2Close.Location = new System.Drawing.Point(199, 188);
            this.txtBoxCH2Close.Name = "txtBoxCH2Close";
            this.txtBoxCH2Close.ReadOnly = true;
            this.txtBoxCH2Close.Size = new System.Drawing.Size(141, 32);
            this.txtBoxCH2Close.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label7.Location = new System.Drawing.Point(12, 192);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(181, 25);
            this.label7.TabIndex = 12;
            this.label7.Text = "第二通道关闭命令 : ";
            // 
            // txtBoxCH2Open
            // 
            this.txtBoxCH2Open.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtBoxCH2Open.Location = new System.Drawing.Point(199, 150);
            this.txtBoxCH2Open.Name = "txtBoxCH2Open";
            this.txtBoxCH2Open.ReadOnly = true;
            this.txtBoxCH2Open.Size = new System.Drawing.Size(141, 32);
            this.txtBoxCH2Open.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label8.Location = new System.Drawing.Point(12, 154);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(181, 25);
            this.label8.TabIndex = 10;
            this.label8.Text = "第二通道打开命令 : ";
            // 
            // txtBoxCH1Close
            // 
            this.txtBoxCH1Close.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtBoxCH1Close.Location = new System.Drawing.Point(199, 113);
            this.txtBoxCH1Close.Name = "txtBoxCH1Close";
            this.txtBoxCH1Close.ReadOnly = true;
            this.txtBoxCH1Close.Size = new System.Drawing.Size(141, 32);
            this.txtBoxCH1Close.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label5.Location = new System.Drawing.Point(12, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(181, 25);
            this.label5.TabIndex = 8;
            this.label5.Text = "第一通道关闭命令 : ";
            // 
            // cmbCom
            // 
            this.cmbCom.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.cmbCom.FormattingEnabled = true;
            this.cmbCom.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
            "COM10",
            "COM11",
            "COM12",
            "COM13",
            "COM14",
            "COM15",
            "COM16",
            "COM17",
            "COM18",
            "COM19",
            "COM20"});
            this.cmbCom.Location = new System.Drawing.Point(199, 37);
            this.cmbCom.Name = "cmbCom";
            this.cmbCom.Size = new System.Drawing.Size(141, 33);
            this.cmbCom.TabIndex = 7;
            // 
            // txtBoxCH1Open
            // 
            this.txtBoxCH1Open.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtBoxCH1Open.Location = new System.Drawing.Point(199, 75);
            this.txtBoxCH1Open.Name = "txtBoxCH1Open";
            this.txtBoxCH1Open.ReadOnly = true;
            this.txtBoxCH1Open.Size = new System.Drawing.Size(141, 32);
            this.txtBoxCH1Open.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label4.Location = new System.Drawing.Point(12, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(181, 25);
            this.label4.TabIndex = 5;
            this.label4.Text = "第一通道打开命令 : ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label6.Location = new System.Drawing.Point(107, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 25);
            this.label6.TabIndex = 0;
            this.label6.Text = "串口号 : ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 492);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.traBarBrightVal)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbLightCtrCMD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBoxBrightVal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar traBarBrightVal;
        private System.Windows.Forms.ComboBox cmbLightCtrCH;
        private System.Windows.Forms.Label label2;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button btnExcuteCmd;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtBoxCH3BrightVal;
        private System.Windows.Forms.TextBox txtBoxCH2BrightVal;
        private System.Windows.Forms.TextBox txtBoxCH1BrightVal;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtBoxCH3Close;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtBoxCH3Open;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtBoxCH2Close;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtBoxCH2Open;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBoxCH1Close;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbCom;
        private System.Windows.Forms.TextBox txtBoxCH1Open;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
    }
}

