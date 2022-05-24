
namespace KEE.Renault
{
    partial class MainFrm
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
            this.panelShadow = new System.Windows.Forms.Panel();
            this.lblTitleChildForm = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.PictureBox();
            this.btnMaximize = new System.Windows.Forms.PictureBox();
            this.btnMinmize = new System.Windows.Forms.PictureBox();
            this.iconCurrentChildForm = new FontAwesome.Sharp.IconPictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnHome = new System.Windows.Forms.PictureBox();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.iBtnSysSetting = new FontAwesome.Sharp.IconButton();
            this.iBtnStaTest = new FontAwesome.Sharp.IconButton();
            this.iBtnUserManage = new FontAwesome.Sharp.IconButton();
            this.iBtnIOMonitor = new FontAwesome.Sharp.IconButton();
            this.iBtnSportSetting = new FontAwesome.Sharp.IconButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.iBtnMotionSetting = new FontAwesome.Sharp.IconButton();
            this.iBtnVisionSetting = new FontAwesome.Sharp.IconButton();
            this.iBtnHomePage = new FontAwesome.Sharp.IconButton();
            this.panelDesktop = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbProList = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbUserList = new System.Windows.Forms.ComboBox();
            this.txtPassWord = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMaximize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinmize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconCurrentChildForm)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnHome)).BeginInit();
            this.panelMenu.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panelDesktop.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelShadow
            // 
            this.panelShadow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(24)))), ((int)(((byte)(58)))));
            this.panelShadow.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelShadow.Location = new System.Drawing.Point(190, 40);
            this.panelShadow.Name = "panelShadow";
            this.panelShadow.Size = new System.Drawing.Size(910, 10);
            this.panelShadow.TabIndex = 7;
            // 
            // lblTitleChildForm
            // 
            this.lblTitleChildForm.AutoSize = true;
            this.lblTitleChildForm.Font = new System.Drawing.Font("幼圆", 13F);
            this.lblTitleChildForm.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblTitleChildForm.Location = new System.Drawing.Point(53, 15);
            this.lblTitleChildForm.Name = "lblTitleChildForm";
            this.lblTitleChildForm.Size = new System.Drawing.Size(80, 18);
            this.lblTitleChildForm.TabIndex = 1;
            this.lblTitleChildForm.Text = "首    页";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(25)))), ((int)(((byte)(62)))));
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Controls.Add(this.btnMaximize);
            this.panel1.Controls.Add(this.btnMinmize);
            this.panel1.Controls.Add(this.lblTitleChildForm);
            this.panel1.Controls.Add(this.iconCurrentChildForm);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(190, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(910, 40);
            this.panel1.TabIndex = 6;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.Image = global::KEE.Renault.Properties.Resources.关__闭;
            this.btnExit.Location = new System.Drawing.Point(870, 7);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(30, 30);
            this.btnExit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnExit.TabIndex = 3;
            this.btnExit.TabStop = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            this.btnExit.MouseLeave += new System.EventHandler(this.btnExit_MouseLeave);
            this.btnExit.MouseHover += new System.EventHandler(this.btnExit_MouseHover);
            // 
            // btnMaximize
            // 
            this.btnMaximize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMaximize.BackColor = System.Drawing.Color.Transparent;
            this.btnMaximize.Image = global::KEE.Renault.Properties.Resources.最大化;
            this.btnMaximize.Location = new System.Drawing.Point(837, 7);
            this.btnMaximize.Name = "btnMaximize";
            this.btnMaximize.Size = new System.Drawing.Size(30, 30);
            this.btnMaximize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnMaximize.TabIndex = 3;
            this.btnMaximize.TabStop = false;
            this.btnMaximize.Click += new System.EventHandler(this.btnMaximize_Click);
            this.btnMaximize.MouseLeave += new System.EventHandler(this.btnMaximize_MouseLeave);
            this.btnMaximize.MouseHover += new System.EventHandler(this.btnMaximize_MouseHover);
            // 
            // btnMinmize
            // 
            this.btnMinmize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinmize.BackColor = System.Drawing.Color.Transparent;
            this.btnMinmize.Image = global::KEE.Renault.Properties.Resources.最小化1;
            this.btnMinmize.Location = new System.Drawing.Point(803, 7);
            this.btnMinmize.Name = "btnMinmize";
            this.btnMinmize.Size = new System.Drawing.Size(30, 30);
            this.btnMinmize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnMinmize.TabIndex = 2;
            this.btnMinmize.TabStop = false;
            this.btnMinmize.Click += new System.EventHandler(this.btnMinmize_Click);
            this.btnMinmize.MouseLeave += new System.EventHandler(this.btnMinmize_MouseLeave);
            this.btnMinmize.MouseHover += new System.EventHandler(this.btnMinmize_MouseHover);
            // 
            // iconCurrentChildForm
            // 
            this.iconCurrentChildForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(25)))), ((int)(((byte)(62)))));
            this.iconCurrentChildForm.ForeColor = System.Drawing.Color.MediumPurple;
            this.iconCurrentChildForm.IconChar = FontAwesome.Sharp.IconChar.Home;
            this.iconCurrentChildForm.IconColor = System.Drawing.Color.MediumPurple;
            this.iconCurrentChildForm.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconCurrentChildForm.Location = new System.Drawing.Point(15, 10);
            this.iconCurrentChildForm.Name = "iconCurrentChildForm";
            this.iconCurrentChildForm.Size = new System.Drawing.Size(32, 32);
            this.iconCurrentChildForm.TabIndex = 0;
            this.iconCurrentChildForm.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnHome);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(5, 0, 20, 0);
            this.panel2.Size = new System.Drawing.Size(190, 120);
            this.panel2.TabIndex = 0;
            // 
            // btnHome
            // 
            this.btnHome.Image = global::KEE.Renault.Properties.Resources.logo;
            this.btnHome.Location = new System.Drawing.Point(5, 1);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(145, 104);
            this.btnHome.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnHome.TabIndex = 0;
            this.btnHome.TabStop = false;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.panelMenu.Controls.Add(this.iBtnSysSetting);
            this.panelMenu.Controls.Add(this.iBtnStaTest);
            this.panelMenu.Controls.Add(this.iBtnUserManage);
            this.panelMenu.Controls.Add(this.iBtnIOMonitor);
            this.panelMenu.Controls.Add(this.iBtnSportSetting);
            this.panelMenu.Controls.Add(this.panel3);
            this.panelMenu.Controls.Add(this.iBtnMotionSetting);
            this.panelMenu.Controls.Add(this.iBtnVisionSetting);
            this.panelMenu.Controls.Add(this.iBtnHomePage);
            this.panelMenu.Controls.Add(this.panel2);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(190, 700);
            this.panelMenu.TabIndex = 5;
            // 
            // iBtnSysSetting
            // 
            this.iBtnSysSetting.Dock = System.Windows.Forms.DockStyle.Top;
            this.iBtnSysSetting.FlatAppearance.BorderSize = 0;
            this.iBtnSysSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iBtnSysSetting.Font = new System.Drawing.Font("微软雅黑", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.iBtnSysSetting.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.iBtnSysSetting.IconChar = FontAwesome.Sharp.IconChar.Cog;
            this.iBtnSysSetting.IconColor = System.Drawing.Color.Gainsboro;
            this.iBtnSysSetting.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iBtnSysSetting.IconSize = 26;
            this.iBtnSysSetting.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iBtnSysSetting.Location = new System.Drawing.Point(0, 470);
            this.iBtnSysSetting.Name = "iBtnSysSetting";
            this.iBtnSysSetting.Padding = new System.Windows.Forms.Padding(30, 0, 10, 0);
            this.iBtnSysSetting.Size = new System.Drawing.Size(190, 50);
            this.iBtnSysSetting.TabIndex = 16;
            this.iBtnSysSetting.Tag = "FrmSysSetting";
            this.iBtnSysSetting.Text = " 系统设置";
            this.iBtnSysSetting.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iBtnSysSetting.UseVisualStyleBackColor = true;
            this.iBtnSysSetting.Click += new System.EventHandler(this.iBtnSysSetting_Click);
            // 
            // iBtnStaTest
            // 
            this.iBtnStaTest.Dock = System.Windows.Forms.DockStyle.Top;
            this.iBtnStaTest.FlatAppearance.BorderSize = 0;
            this.iBtnStaTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iBtnStaTest.Font = new System.Drawing.Font("微软雅黑", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.iBtnStaTest.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.iBtnStaTest.IconChar = FontAwesome.Sharp.IconChar.SlidersH;
            this.iBtnStaTest.IconColor = System.Drawing.Color.Gainsboro;
            this.iBtnStaTest.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iBtnStaTest.IconSize = 26;
            this.iBtnStaTest.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iBtnStaTest.Location = new System.Drawing.Point(0, 420);
            this.iBtnStaTest.Name = "iBtnStaTest";
            this.iBtnStaTest.Padding = new System.Windows.Forms.Padding(30, 0, 10, 0);
            this.iBtnStaTest.Size = new System.Drawing.Size(190, 50);
            this.iBtnStaTest.TabIndex = 15;
            this.iBtnStaTest.Tag = "FrmQCDataTest";
            this.iBtnStaTest.Text = " 数据测试";
            this.iBtnStaTest.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iBtnStaTest.UseVisualStyleBackColor = true;
            this.iBtnStaTest.Click += new System.EventHandler(this.iBtnStaTest_Click);
            // 
            // iBtnUserManage
            // 
            this.iBtnUserManage.Dock = System.Windows.Forms.DockStyle.Top;
            this.iBtnUserManage.FlatAppearance.BorderSize = 0;
            this.iBtnUserManage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iBtnUserManage.Font = new System.Drawing.Font("微软雅黑", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.iBtnUserManage.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.iBtnUserManage.IconChar = FontAwesome.Sharp.IconChar.Users;
            this.iBtnUserManage.IconColor = System.Drawing.Color.Gainsboro;
            this.iBtnUserManage.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iBtnUserManage.IconSize = 26;
            this.iBtnUserManage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iBtnUserManage.Location = new System.Drawing.Point(0, 370);
            this.iBtnUserManage.Name = "iBtnUserManage";
            this.iBtnUserManage.Padding = new System.Windows.Forms.Padding(30, 0, 10, 0);
            this.iBtnUserManage.Size = new System.Drawing.Size(190, 50);
            this.iBtnUserManage.TabIndex = 17;
            this.iBtnUserManage.Tag = "FrmUserManage";
            this.iBtnUserManage.Text = " 用户管理";
            this.iBtnUserManage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iBtnUserManage.UseVisualStyleBackColor = true;
            this.iBtnUserManage.Click += new System.EventHandler(this.iBtnUserManage_Click);
            // 
            // iBtnIOMonitor
            // 
            this.iBtnIOMonitor.Dock = System.Windows.Forms.DockStyle.Top;
            this.iBtnIOMonitor.FlatAppearance.BorderSize = 0;
            this.iBtnIOMonitor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iBtnIOMonitor.Font = new System.Drawing.Font("微软雅黑", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.iBtnIOMonitor.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.iBtnIOMonitor.IconChar = FontAwesome.Sharp.IconChar.Desktop;
            this.iBtnIOMonitor.IconColor = System.Drawing.Color.Gainsboro;
            this.iBtnIOMonitor.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iBtnIOMonitor.IconSize = 26;
            this.iBtnIOMonitor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iBtnIOMonitor.Location = new System.Drawing.Point(0, 320);
            this.iBtnIOMonitor.Name = "iBtnIOMonitor";
            this.iBtnIOMonitor.Padding = new System.Windows.Forms.Padding(30, 0, 10, 0);
            this.iBtnIOMonitor.Size = new System.Drawing.Size(190, 50);
            this.iBtnIOMonitor.TabIndex = 14;
            this.iBtnIOMonitor.Tag = "FrmIOMonitor";
            this.iBtnIOMonitor.Text = " 信号监视";
            this.iBtnIOMonitor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iBtnIOMonitor.UseVisualStyleBackColor = true;
            this.iBtnIOMonitor.Click += new System.EventHandler(this.iBtnIOMonitor_Click);
            // 
            // iBtnSportSetting
            // 
            this.iBtnSportSetting.Dock = System.Windows.Forms.DockStyle.Top;
            this.iBtnSportSetting.FlatAppearance.BorderSize = 0;
            this.iBtnSportSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iBtnSportSetting.Font = new System.Drawing.Font("微软雅黑", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.iBtnSportSetting.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.iBtnSportSetting.IconChar = FontAwesome.Sharp.IconChar.Tools;
            this.iBtnSportSetting.IconColor = System.Drawing.Color.Gainsboro;
            this.iBtnSportSetting.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iBtnSportSetting.IconSize = 26;
            this.iBtnSportSetting.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iBtnSportSetting.Location = new System.Drawing.Point(0, 270);
            this.iBtnSportSetting.Name = "iBtnSportSetting";
            this.iBtnSportSetting.Padding = new System.Windows.Forms.Padding(30, 0, 10, 0);
            this.iBtnSportSetting.Size = new System.Drawing.Size(190, 50);
            this.iBtnSportSetting.TabIndex = 13;
            this.iBtnSportSetting.Tag = "FrmAxiasSetting";
            this.iBtnSportSetting.Text = " 运动设置";
            this.iBtnSportSetting.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iBtnSportSetting.UseVisualStyleBackColor = true;
            this.iBtnSportSetting.Click += new System.EventHandler(this.iBtnSportSetting_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.lblVersion);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 615);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(5, 0, 20, 0);
            this.panel3.Size = new System.Drawing.Size(190, 85);
            this.panel3.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Gainsboro;
            this.label2.Location = new System.Drawing.Point(8, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 19);
            this.label2.TabIndex = 10;
            this.label2.Text = "版权：KEBDT";
            // 
            // lblVersion
            // 
            this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblVersion.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblVersion.Location = new System.Drawing.Point(9, 22);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(76, 19);
            this.lblVersion.TabIndex = 9;
            this.lblVersion.Text = "版本：V0.1";
            // 
            // iBtnMotionSetting
            // 
            this.iBtnMotionSetting.Dock = System.Windows.Forms.DockStyle.Top;
            this.iBtnMotionSetting.FlatAppearance.BorderSize = 0;
            this.iBtnMotionSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iBtnMotionSetting.Font = new System.Drawing.Font("微软雅黑", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.iBtnMotionSetting.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.iBtnMotionSetting.IconChar = FontAwesome.Sharp.IconChar.Chalkboard;
            this.iBtnMotionSetting.IconColor = System.Drawing.Color.Gainsboro;
            this.iBtnMotionSetting.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iBtnMotionSetting.IconSize = 26;
            this.iBtnMotionSetting.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iBtnMotionSetting.Location = new System.Drawing.Point(0, 220);
            this.iBtnMotionSetting.Name = "iBtnMotionSetting";
            this.iBtnMotionSetting.Padding = new System.Windows.Forms.Padding(30, 0, 10, 0);
            this.iBtnMotionSetting.Size = new System.Drawing.Size(190, 50);
            this.iBtnMotionSetting.TabIndex = 4;
            this.iBtnMotionSetting.Tag = "FrmAxiasMain";
            this.iBtnMotionSetting.Text = "轴卡设置";
            this.iBtnMotionSetting.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iBtnMotionSetting.UseVisualStyleBackColor = true;
            this.iBtnMotionSetting.Click += new System.EventHandler(this.iBtnMotionSetting_Click);
            // 
            // iBtnVisionSetting
            // 
            this.iBtnVisionSetting.Dock = System.Windows.Forms.DockStyle.Top;
            this.iBtnVisionSetting.FlatAppearance.BorderSize = 0;
            this.iBtnVisionSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iBtnVisionSetting.Font = new System.Drawing.Font("微软雅黑", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.iBtnVisionSetting.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.iBtnVisionSetting.IconChar = FontAwesome.Sharp.IconChar.Camera;
            this.iBtnVisionSetting.IconColor = System.Drawing.Color.Gainsboro;
            this.iBtnVisionSetting.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iBtnVisionSetting.IconSize = 26;
            this.iBtnVisionSetting.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iBtnVisionSetting.Location = new System.Drawing.Point(0, 170);
            this.iBtnVisionSetting.Name = "iBtnVisionSetting";
            this.iBtnVisionSetting.Padding = new System.Windows.Forms.Padding(30, 0, 10, 0);
            this.iBtnVisionSetting.Size = new System.Drawing.Size(190, 50);
            this.iBtnVisionSetting.TabIndex = 3;
            this.iBtnVisionSetting.Tag = "FrmVisionSetting";
            this.iBtnVisionSetting.Text = " 视觉设置";
            this.iBtnVisionSetting.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iBtnVisionSetting.UseVisualStyleBackColor = true;
            this.iBtnVisionSetting.Click += new System.EventHandler(this.iBtnVisionSetting_Click);
            // 
            // iBtnHomePage
            // 
            this.iBtnHomePage.Dock = System.Windows.Forms.DockStyle.Top;
            this.iBtnHomePage.FlatAppearance.BorderSize = 0;
            this.iBtnHomePage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iBtnHomePage.Font = new System.Drawing.Font("微软雅黑", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.iBtnHomePage.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.iBtnHomePage.IconChar = FontAwesome.Sharp.IconChar.PlayCircle;
            this.iBtnHomePage.IconColor = System.Drawing.Color.Gainsboro;
            this.iBtnHomePage.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iBtnHomePage.IconSize = 26;
            this.iBtnHomePage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iBtnHomePage.Location = new System.Drawing.Point(0, 120);
            this.iBtnHomePage.Name = "iBtnHomePage";
            this.iBtnHomePage.Padding = new System.Windows.Forms.Padding(30, 0, 10, 0);
            this.iBtnHomePage.Size = new System.Drawing.Size(190, 50);
            this.iBtnHomePage.TabIndex = 1;
            this.iBtnHomePage.Tag = "FrmHomePage";
            this.iBtnHomePage.Text = " 主    页";
            this.iBtnHomePage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iBtnHomePage.UseVisualStyleBackColor = true;
            this.iBtnHomePage.Click += new System.EventHandler(this.iBtnHomePage_Click);
            // 
            // panelDesktop
            // 
            this.panelDesktop.Controls.Add(this.groupBox1);
            this.panelDesktop.Controls.Add(this.pictureBox1);
            this.panelDesktop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDesktop.Font = new System.Drawing.Font("幼圆", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panelDesktop.Location = new System.Drawing.Point(190, 50);
            this.panelDesktop.Name = "panelDesktop";
            this.panelDesktop.Size = new System.Drawing.Size(910, 650);
            this.panelDesktop.TabIndex = 10;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbProList);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbUserList);
            this.groupBox1.Controls.Add(this.txtPassWord);
            this.groupBox1.Controls.Add(this.btnLogin);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.groupBox1.Location = new System.Drawing.Point(0, 249);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(910, 401);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "权限登录";
            // 
            // cmbProList
            // 
            this.cmbProList.Font = new System.Drawing.Font("幼圆", 32F, System.Drawing.FontStyle.Bold);
            this.cmbProList.FormattingEnabled = true;
            this.cmbProList.Items.AddRange(new object[] {
            "Renault",
            "Lada"});
            this.cmbProList.Location = new System.Drawing.Point(401, 22);
            this.cmbProList.Name = "cmbProList";
            this.cmbProList.Size = new System.Drawing.Size(270, 51);
            this.cmbProList.TabIndex = 60;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("幼圆", 32F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label3.Location = new System.Drawing.Point(175, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(244, 43);
            this.label3.TabIndex = 59;
            this.label3.Text = "产品选择：";
            // 
            // cmbUserList
            // 
            this.cmbUserList.Font = new System.Drawing.Font("幼圆", 32F, System.Drawing.FontStyle.Bold);
            this.cmbUserList.FormattingEnabled = true;
            this.cmbUserList.Location = new System.Drawing.Point(401, 83);
            this.cmbUserList.Name = "cmbUserList";
            this.cmbUserList.Size = new System.Drawing.Size(270, 51);
            this.cmbUserList.TabIndex = 58;
            this.cmbUserList.TextUpdate += new System.EventHandler(this.cmbUserList_TextUpdate);
            this.cmbUserList.Enter += new System.EventHandler(this.cmbUserList_Enter);
            this.cmbUserList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cmbUserList_MouseDown);
            // 
            // txtPassWord
            // 
            this.txtPassWord.Font = new System.Drawing.Font("幼圆", 32F, System.Drawing.FontStyle.Bold);
            this.txtPassWord.Location = new System.Drawing.Point(401, 143);
            this.txtPassWord.Name = "txtPassWord";
            this.txtPassWord.PasswordChar = '*';
            this.txtPassWord.Size = new System.Drawing.Size(270, 54);
            this.txtPassWord.TabIndex = 57;
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.Transparent;
            this.btnLogin.Font = new System.Drawing.Font("幼圆", 32F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnLogin.Location = new System.Drawing.Point(401, 203);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(270, 61);
            this.btnLogin.TabIndex = 56;
            this.btnLogin.Text = "登 陆";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("幼圆", 32F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label4.Location = new System.Drawing.Point(220, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(199, 43);
            this.label4.TabIndex = 54;
            this.label4.Text = "用户名：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("幼圆", 32F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label5.Location = new System.Drawing.Point(265, 151);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(154, 43);
            this.label5.TabIndex = 55;
            this.label5.Text = "密码：";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = global::KEE.Renault.Properties.Resources.KEE_logo_200MM;
            this.pictureBox1.Location = new System.Drawing.Point(249, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(402, 290);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // MainFrm
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.Controls.Add(this.panelDesktop);
            this.Controls.Add(this.panelShadow);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IsMdiContainer = true;
            this.MinimumSize = new System.Drawing.Size(1100, 700);
            this.Name = "MainFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFrm_FormClosing);
            this.Load += new System.EventHandler(this.MainFrm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMaximize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinmize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconCurrentChildForm)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnHome)).EndInit();
            this.panelMenu.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panelDesktop.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelShadow;
        private FontAwesome.Sharp.IconButton iBtnMotionSetting;
        private FontAwesome.Sharp.IconButton iBtnVisionSetting;
        private System.Windows.Forms.PictureBox btnMaximize;
        private System.Windows.Forms.PictureBox btnMinmize;
        private System.Windows.Forms.Label lblTitleChildForm;
        private FontAwesome.Sharp.IconPictureBox iconCurrentChildForm;
        private FontAwesome.Sharp.IconButton iBtnHomePage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox btnExit;
        private System.Windows.Forms.PictureBox btnHome;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Panel panelDesktop;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblVersion;
        private FontAwesome.Sharp.IconButton iBtnSysSetting;
        private FontAwesome.Sharp.IconButton iBtnStaTest;
        private FontAwesome.Sharp.IconButton iBtnUserManage;
        private FontAwesome.Sharp.IconButton iBtnIOMonitor;
        private FontAwesome.Sharp.IconButton iBtnSportSetting;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbProList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbUserList;
        private System.Windows.Forms.TextBox txtPassWord;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

