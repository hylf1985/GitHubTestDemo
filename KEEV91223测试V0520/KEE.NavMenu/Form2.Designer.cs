
namespace KEE.NavMenu
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnR_TRIG_1 = new System.Windows.Forms.Button();
            this.btnR_TRIG_0 = new System.Windows.Forms.Button();
            this.lblR_TRIGRes = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnF_TRIG_1 = new System.Windows.Forms.Button();
            this.btnF_TRIG_0 = new System.Windows.Forms.Button();
            this.lblF_TRIGRes = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnR_TRIG_1);
            this.groupBox1.Controls.Add(this.btnR_TRIG_0);
            this.groupBox1.Controls.Add(this.lblR_TRIGRes);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(60, 74);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 178);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "上升沿R_TRIG";
            // 
            // btnR_TRIG_1
            // 
            this.btnR_TRIG_1.Location = new System.Drawing.Point(143, 69);
            this.btnR_TRIG_1.Name = "btnR_TRIG_1";
            this.btnR_TRIG_1.Size = new System.Drawing.Size(75, 23);
            this.btnR_TRIG_1.TabIndex = 4;
            this.btnR_TRIG_1.Text = "1";
            this.btnR_TRIG_1.UseVisualStyleBackColor = true;
            this.btnR_TRIG_1.Click += new System.EventHandler(this.btnR_TRIG_1_Click);
            // 
            // btnR_TRIG_0
            // 
            this.btnR_TRIG_0.Location = new System.Drawing.Point(22, 69);
            this.btnR_TRIG_0.Name = "btnR_TRIG_0";
            this.btnR_TRIG_0.Size = new System.Drawing.Size(75, 23);
            this.btnR_TRIG_0.TabIndex = 3;
            this.btnR_TRIG_0.Text = "0";
            this.btnR_TRIG_0.UseVisualStyleBackColor = true;
            this.btnR_TRIG_0.Click += new System.EventHandler(this.btnR_TRIG_0_Click);
            // 
            // lblR_TRIGRes
            // 
            this.lblR_TRIGRes.AutoSize = true;
            this.lblR_TRIGRes.Location = new System.Drawing.Point(155, 145);
            this.lblR_TRIGRes.Name = "lblR_TRIGRes";
            this.lblR_TRIGRes.Size = new System.Drawing.Size(29, 12);
            this.lblR_TRIGRes.TabIndex = 2;
            this.lblR_TRIGRes.Text = "结果";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 145);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "结果";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnF_TRIG_1);
            this.groupBox2.Controls.Add(this.btnF_TRIG_0);
            this.groupBox2.Controls.Add(this.lblF_TRIGRes);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(376, 74);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(289, 178);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "下降沿F_TRIG";
            // 
            // btnF_TRIG_1
            // 
            this.btnF_TRIG_1.Location = new System.Drawing.Point(143, 69);
            this.btnF_TRIG_1.Name = "btnF_TRIG_1";
            this.btnF_TRIG_1.Size = new System.Drawing.Size(75, 23);
            this.btnF_TRIG_1.TabIndex = 4;
            this.btnF_TRIG_1.Text = "1";
            this.btnF_TRIG_1.UseVisualStyleBackColor = true;
            this.btnF_TRIG_1.Click += new System.EventHandler(this.btnF_TRIG_1_Click);
            // 
            // btnF_TRIG_0
            // 
            this.btnF_TRIG_0.Location = new System.Drawing.Point(22, 69);
            this.btnF_TRIG_0.Name = "btnF_TRIG_0";
            this.btnF_TRIG_0.Size = new System.Drawing.Size(75, 23);
            this.btnF_TRIG_0.TabIndex = 3;
            this.btnF_TRIG_0.Text = "0";
            this.btnF_TRIG_0.UseVisualStyleBackColor = true;
            this.btnF_TRIG_0.Click += new System.EventHandler(this.btnF_TRIG_0_Click);
            // 
            // lblF_TRIGRes
            // 
            this.lblF_TRIGRes.AutoSize = true;
            this.lblF_TRIGRes.Location = new System.Drawing.Point(155, 145);
            this.lblF_TRIGRes.Name = "lblF_TRIGRes";
            this.lblF_TRIGRes.Size = new System.Drawing.Size(29, 12);
            this.lblF_TRIGRes.TabIndex = 2;
            this.lblF_TRIGRes.Text = "结果";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "结果";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnR_TRIG_1;
        private System.Windows.Forms.Button btnR_TRIG_0;
        private System.Windows.Forms.Label lblR_TRIGRes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnF_TRIG_1;
        private System.Windows.Forms.Button btnF_TRIG_0;
        private System.Windows.Forms.Label lblF_TRIGRes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
    }
}