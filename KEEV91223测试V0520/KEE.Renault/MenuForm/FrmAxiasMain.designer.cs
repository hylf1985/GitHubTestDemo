
namespace KEE.Renault.MenuForm
{
    partial class FrmAxiasMain
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabInsPos = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabFeedBackPos = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabCurSpeed = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel7 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabMotionStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel9 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStausHomeV = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel8 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabStopReason = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.iBtnTakeAxias = new FontAwesome.Sharp.IconButton();
            this.iBtnCamInd = new FontAwesome.Sharp.IconButton();
            this.iBtnPressAndHighTest = new FontAwesome.Sharp.IconButton();
            this.iBtnRobot = new FontAwesome.Sharp.IconButton();
            this.iBtnLabelingAxias = new FontAwesome.Sharp.IconButton();
            this.iBtnFeedRAxias = new FontAwesome.Sharp.IconButton();
            this.iBtnFeedAxias = new FontAwesome.Sharp.IconButton();
            this.iBtnFeedFromInjectAxias = new FontAwesome.Sharp.IconButton();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pnlDesktop = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.statusStrip2.SuspendLayout();
            this.pnlMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(25)))), ((int)(((byte)(62)))));
            this.panel1.Controls.Add(this.statusStrip2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 899);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1168, 40);
            this.panel1.TabIndex = 0;
            // 
            // statusStrip2
            // 
            this.statusStrip2.AutoSize = false;
            this.statusStrip2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(25)))), ((int)(((byte)(62)))));
            this.statusStrip2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusStrip2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.statusStrip2.GripMargin = new System.Windows.Forms.Padding(0);
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel3,
            this.statusLabInsPos,
            this.toolStripStatusLabel4,
            this.statusLabFeedBackPos,
            this.toolStripStatusLabel5,
            this.statusLabCurSpeed,
            this.toolStripStatusLabel7,
            this.statusLabMotionStatus,
            this.toolStripStatusLabel9,
            this.toolStripStausHomeV,
            this.toolStripStatusLabel8,
            this.statusLabStopReason});
            this.statusStrip2.Location = new System.Drawing.Point(0, 0);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(1168, 40);
            this.statusStrip2.TabIndex = 0;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.ForeColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(85, 35);
            this.toolStripStatusLabel3.Text = "指令位置 : ";
            // 
            // statusLabInsPos
            // 
            this.statusLabInsPos.ForeColor = System.Drawing.SystemColors.Control;
            this.statusLabInsPos.Name = "statusLabInsPos";
            this.statusLabInsPos.Size = new System.Drawing.Size(19, 35);
            this.statusLabInsPos.Text = "--";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.ForeColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(85, 35);
            this.toolStripStatusLabel4.Text = "反馈位置 : ";
            // 
            // statusLabFeedBackPos
            // 
            this.statusLabFeedBackPos.ForeColor = System.Drawing.SystemColors.Control;
            this.statusLabFeedBackPos.Name = "statusLabFeedBackPos";
            this.statusLabFeedBackPos.Size = new System.Drawing.Size(19, 35);
            this.statusLabFeedBackPos.Text = "--";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.ForeColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(85, 35);
            this.toolStripStatusLabel5.Text = "当前速度 : ";
            // 
            // statusLabCurSpeed
            // 
            this.statusLabCurSpeed.ForeColor = System.Drawing.SystemColors.Control;
            this.statusLabCurSpeed.Name = "statusLabCurSpeed";
            this.statusLabCurSpeed.Size = new System.Drawing.Size(19, 35);
            this.statusLabCurSpeed.Text = "--";
            // 
            // toolStripStatusLabel7
            // 
            this.toolStripStatusLabel7.ForeColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel7.Name = "toolStripStatusLabel7";
            this.toolStripStatusLabel7.Size = new System.Drawing.Size(85, 35);
            this.toolStripStatusLabel7.Text = "运动状态 : ";
            // 
            // statusLabMotionStatus
            // 
            this.statusLabMotionStatus.ForeColor = System.Drawing.SystemColors.Control;
            this.statusLabMotionStatus.Name = "statusLabMotionStatus";
            this.statusLabMotionStatus.Size = new System.Drawing.Size(19, 35);
            this.statusLabMotionStatus.Text = "--";
            // 
            // toolStripStatusLabel9
            // 
            this.toolStripStatusLabel9.ForeColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel9.Name = "toolStripStatusLabel9";
            this.toolStripStatusLabel9.Size = new System.Drawing.Size(85, 35);
            this.toolStripStatusLabel9.Text = "回零结果 : ";
            // 
            // toolStripStausHomeV
            // 
            this.toolStripStausHomeV.ForeColor = System.Drawing.SystemColors.Control;
            this.toolStripStausHomeV.Name = "toolStripStausHomeV";
            this.toolStripStausHomeV.Size = new System.Drawing.Size(19, 35);
            this.toolStripStausHomeV.Text = "--";
            // 
            // toolStripStatusLabel8
            // 
            this.toolStripStatusLabel8.ForeColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel8.Name = "toolStripStatusLabel8";
            this.toolStripStatusLabel8.Size = new System.Drawing.Size(85, 35);
            this.toolStripStatusLabel8.Text = "停止原因 : ";
            // 
            // statusLabStopReason
            // 
            this.statusLabStopReason.ForeColor = System.Drawing.SystemColors.Control;
            this.statusLabStopReason.Name = "statusLabStopReason";
            this.statusLabStopReason.Size = new System.Drawing.Size(19, 35);
            this.statusLabStopReason.Text = "--";
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(5, 899);
            this.panel3.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(1163, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(5, 899);
            this.panel4.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(5, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1158, 5);
            this.panel2.TabIndex = 7;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(5, 894);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1158, 5);
            this.panel6.TabIndex = 9;
            // 
            // pnlMenu
            // 
            this.pnlMenu.BackColor = System.Drawing.SystemColors.Control;
            this.pnlMenu.Controls.Add(this.iBtnTakeAxias);
            this.pnlMenu.Controls.Add(this.iBtnCamInd);
            this.pnlMenu.Controls.Add(this.iBtnPressAndHighTest);
            this.pnlMenu.Controls.Add(this.iBtnRobot);
            this.pnlMenu.Controls.Add(this.iBtnLabelingAxias);
            this.pnlMenu.Controls.Add(this.iBtnFeedRAxias);
            this.pnlMenu.Controls.Add(this.iBtnFeedAxias);
            this.pnlMenu.Controls.Add(this.iBtnFeedFromInjectAxias);
            this.pnlMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMenu.Location = new System.Drawing.Point(5, 5);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Size = new System.Drawing.Size(220, 889);
            this.pnlMenu.TabIndex = 10;
            // 
            // iBtnTakeAxias
            // 
            this.iBtnTakeAxias.Dock = System.Windows.Forms.DockStyle.Top;
            this.iBtnTakeAxias.FlatAppearance.BorderSize = 0;
            this.iBtnTakeAxias.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iBtnTakeAxias.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.iBtnTakeAxias.ForeColor = System.Drawing.Color.Black;
            this.iBtnTakeAxias.IconChar = FontAwesome.Sharp.IconChar.Tools;
            this.iBtnTakeAxias.IconColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.iBtnTakeAxias.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iBtnTakeAxias.IconSize = 24;
            this.iBtnTakeAxias.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iBtnTakeAxias.Location = new System.Drawing.Point(0, 420);
            this.iBtnTakeAxias.Name = "iBtnTakeAxias";
            this.iBtnTakeAxias.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.iBtnTakeAxias.Size = new System.Drawing.Size(220, 60);
            this.iBtnTakeAxias.TabIndex = 27;
            this.iBtnTakeAxias.Tag = "FrmTakeAxias";
            this.iBtnTakeAxias.Text = " 收料轴参数设定";
            this.iBtnTakeAxias.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iBtnTakeAxias.UseVisualStyleBackColor = true;
            this.iBtnTakeAxias.Click += new System.EventHandler(this.iBtnTakeAxias_Click);
            // 
            // iBtnCamInd
            // 
            this.iBtnCamInd.Dock = System.Windows.Forms.DockStyle.Top;
            this.iBtnCamInd.FlatAppearance.BorderSize = 0;
            this.iBtnCamInd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iBtnCamInd.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.iBtnCamInd.ForeColor = System.Drawing.Color.Black;
            this.iBtnCamInd.IconChar = FontAwesome.Sharp.IconChar.Tools;
            this.iBtnCamInd.IconColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.iBtnCamInd.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iBtnCamInd.IconSize = 24;
            this.iBtnCamInd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iBtnCamInd.Location = new System.Drawing.Point(0, 360);
            this.iBtnCamInd.Name = "iBtnCamInd";
            this.iBtnCamInd.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.iBtnCamInd.Size = new System.Drawing.Size(220, 60);
            this.iBtnCamInd.TabIndex = 26;
            this.iBtnCamInd.Tag = "FrmSplitter";
            this.iBtnCamInd.Text = "压合、测高设定";
            this.iBtnCamInd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iBtnCamInd.UseVisualStyleBackColor = true;
            this.iBtnCamInd.Click += new System.EventHandler(this.iBtnCamInd_Click);
            // 
            // iBtnPressAndHighTest
            // 
            this.iBtnPressAndHighTest.Dock = System.Windows.Forms.DockStyle.Top;
            this.iBtnPressAndHighTest.FlatAppearance.BorderSize = 0;
            this.iBtnPressAndHighTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iBtnPressAndHighTest.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.iBtnPressAndHighTest.ForeColor = System.Drawing.Color.Black;
            this.iBtnPressAndHighTest.IconChar = FontAwesome.Sharp.IconChar.Tools;
            this.iBtnPressAndHighTest.IconColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.iBtnPressAndHighTest.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iBtnPressAndHighTest.IconSize = 24;
            this.iBtnPressAndHighTest.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iBtnPressAndHighTest.Location = new System.Drawing.Point(0, 300);
            this.iBtnPressAndHighTest.Name = "iBtnPressAndHighTest";
            this.iBtnPressAndHighTest.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.iBtnPressAndHighTest.Size = new System.Drawing.Size(220, 60);
            this.iBtnPressAndHighTest.TabIndex = 25;
            this.iBtnPressAndHighTest.Tag = "FrmPressAltimetry";
            this.iBtnPressAndHighTest.Text = "分割器参数设定";
            this.iBtnPressAndHighTest.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iBtnPressAndHighTest.UseVisualStyleBackColor = true;
            this.iBtnPressAndHighTest.Click += new System.EventHandler(this.iBtnPressAndHighTest_Click);
            // 
            // iBtnRobot
            // 
            this.iBtnRobot.Dock = System.Windows.Forms.DockStyle.Top;
            this.iBtnRobot.FlatAppearance.BorderSize = 0;
            this.iBtnRobot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iBtnRobot.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.iBtnRobot.ForeColor = System.Drawing.Color.Black;
            this.iBtnRobot.IconChar = FontAwesome.Sharp.IconChar.Tools;
            this.iBtnRobot.IconColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.iBtnRobot.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iBtnRobot.IconSize = 24;
            this.iBtnRobot.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iBtnRobot.Location = new System.Drawing.Point(0, 240);
            this.iBtnRobot.Name = "iBtnRobot";
            this.iBtnRobot.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.iBtnRobot.Size = new System.Drawing.Size(220, 60);
            this.iBtnRobot.TabIndex = 24;
            this.iBtnRobot.Tag = "FrmEpsonRobot";
            this.iBtnRobot.Text = " 机械手相关设定";
            this.iBtnRobot.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iBtnRobot.UseVisualStyleBackColor = true;
            this.iBtnRobot.Click += new System.EventHandler(this.iBtnRobot_Click);
            // 
            // iBtnLabelingAxias
            // 
            this.iBtnLabelingAxias.Dock = System.Windows.Forms.DockStyle.Top;
            this.iBtnLabelingAxias.FlatAppearance.BorderSize = 0;
            this.iBtnLabelingAxias.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iBtnLabelingAxias.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.iBtnLabelingAxias.ForeColor = System.Drawing.Color.Black;
            this.iBtnLabelingAxias.IconChar = FontAwesome.Sharp.IconChar.Tools;
            this.iBtnLabelingAxias.IconColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.iBtnLabelingAxias.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iBtnLabelingAxias.IconSize = 24;
            this.iBtnLabelingAxias.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iBtnLabelingAxias.Location = new System.Drawing.Point(0, 180);
            this.iBtnLabelingAxias.Name = "iBtnLabelingAxias";
            this.iBtnLabelingAxias.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.iBtnLabelingAxias.Size = new System.Drawing.Size(220, 60);
            this.iBtnLabelingAxias.TabIndex = 23;
            this.iBtnLabelingAxias.Tag = "FrmLabelingAxias";
            this.iBtnLabelingAxias.Text = " 出标轴参数设定";
            this.iBtnLabelingAxias.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iBtnLabelingAxias.UseVisualStyleBackColor = true;
            this.iBtnLabelingAxias.Click += new System.EventHandler(this.iBtnLabelingAxias_Click);
            // 
            // iBtnFeedRAxias
            // 
            this.iBtnFeedRAxias.Dock = System.Windows.Forms.DockStyle.Top;
            this.iBtnFeedRAxias.FlatAppearance.BorderSize = 0;
            this.iBtnFeedRAxias.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iBtnFeedRAxias.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.iBtnFeedRAxias.ForeColor = System.Drawing.Color.Black;
            this.iBtnFeedRAxias.IconChar = FontAwesome.Sharp.IconChar.Tools;
            this.iBtnFeedRAxias.IconColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.iBtnFeedRAxias.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iBtnFeedRAxias.IconSize = 24;
            this.iBtnFeedRAxias.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iBtnFeedRAxias.Location = new System.Drawing.Point(0, 120);
            this.iBtnFeedRAxias.Name = "iBtnFeedRAxias";
            this.iBtnFeedRAxias.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.iBtnFeedRAxias.Size = new System.Drawing.Size(220, 60);
            this.iBtnFeedRAxias.TabIndex = 22;
            this.iBtnFeedRAxias.Tag = "FrmFeedRAxias";
            this.iBtnFeedRAxias.Text = " 上料R轴参数设定";
            this.iBtnFeedRAxias.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iBtnFeedRAxias.UseVisualStyleBackColor = true;
            this.iBtnFeedRAxias.Click += new System.EventHandler(this.iBtnFeedRAxias_Click);
            // 
            // iBtnFeedAxias
            // 
            this.iBtnFeedAxias.Dock = System.Windows.Forms.DockStyle.Top;
            this.iBtnFeedAxias.FlatAppearance.BorderSize = 0;
            this.iBtnFeedAxias.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iBtnFeedAxias.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.iBtnFeedAxias.ForeColor = System.Drawing.Color.Black;
            this.iBtnFeedAxias.IconChar = FontAwesome.Sharp.IconChar.Tools;
            this.iBtnFeedAxias.IconColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.iBtnFeedAxias.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iBtnFeedAxias.IconSize = 24;
            this.iBtnFeedAxias.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iBtnFeedAxias.Location = new System.Drawing.Point(0, 60);
            this.iBtnFeedAxias.Name = "iBtnFeedAxias";
            this.iBtnFeedAxias.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.iBtnFeedAxias.Size = new System.Drawing.Size(220, 60);
            this.iBtnFeedAxias.TabIndex = 21;
            this.iBtnFeedAxias.Tag = "FrmFeedAxias";
            this.iBtnFeedAxias.Text = " 上料轴参数设定";
            this.iBtnFeedAxias.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iBtnFeedAxias.UseVisualStyleBackColor = true;
            this.iBtnFeedAxias.Click += new System.EventHandler(this.iBtnFeedAxias_Click);
            // 
            // iBtnFeedFromInjectAxias
            // 
            this.iBtnFeedFromInjectAxias.Dock = System.Windows.Forms.DockStyle.Top;
            this.iBtnFeedFromInjectAxias.FlatAppearance.BorderSize = 0;
            this.iBtnFeedFromInjectAxias.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iBtnFeedFromInjectAxias.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.iBtnFeedFromInjectAxias.ForeColor = System.Drawing.Color.Black;
            this.iBtnFeedFromInjectAxias.IconChar = FontAwesome.Sharp.IconChar.Tools;
            this.iBtnFeedFromInjectAxias.IconColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.iBtnFeedFromInjectAxias.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iBtnFeedFromInjectAxias.IconSize = 24;
            this.iBtnFeedFromInjectAxias.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iBtnFeedFromInjectAxias.Location = new System.Drawing.Point(0, 0);
            this.iBtnFeedFromInjectAxias.Name = "iBtnFeedFromInjectAxias";
            this.iBtnFeedFromInjectAxias.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.iBtnFeedFromInjectAxias.Size = new System.Drawing.Size(220, 60);
            this.iBtnFeedFromInjectAxias.TabIndex = 20;
            this.iBtnFeedFromInjectAxias.Tag = "FrmFeedFromInjectAxias";
            this.iBtnFeedFromInjectAxias.Text = " 供料轴参数设定";
            this.iBtnFeedFromInjectAxias.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iBtnFeedFromInjectAxias.UseVisualStyleBackColor = true;
            this.iBtnFeedFromInjectAxias.Click += new System.EventHandler(this.iBtnFeedFromInjectAxias_Click);
            // 
            // panel5
            // 
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(225, 5);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(5, 889);
            this.panel5.TabIndex = 11;
            // 
            // pnlDesktop
            // 
            this.pnlDesktop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlDesktop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDesktop.Location = new System.Drawing.Point(230, 5);
            this.pnlDesktop.Name = "pnlDesktop";
            this.pnlDesktop.Size = new System.Drawing.Size(933, 889);
            this.pnlDesktop.TabIndex = 12;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FrmAxiasMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(1168, 939);
            this.Controls.Add(this.pnlDesktop);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.pnlMenu);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmAxiasMain";
            this.Text = "轴卡设置";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Enter += new System.EventHandler(this.FrmAxiasMain_Enter);
            this.Leave += new System.EventHandler(this.FrmAxiasMain_Leave);
            this.panel1.ResumeLayout(false);
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.pnlMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel pnlDesktop;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel statusLabInsPos;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel statusLabFeedBackPos;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel statusLabCurSpeed;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel7;
        private System.Windows.Forms.ToolStripStatusLabel statusLabMotionStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel9;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStausHomeV;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel8;
        private System.Windows.Forms.ToolStripStatusLabel statusLabStopReason;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private FontAwesome.Sharp.IconButton iBtnTakeAxias;
        private FontAwesome.Sharp.IconButton iBtnCamInd;
        private FontAwesome.Sharp.IconButton iBtnPressAndHighTest;
        private FontAwesome.Sharp.IconButton iBtnRobot;
        private FontAwesome.Sharp.IconButton iBtnLabelingAxias;
        private FontAwesome.Sharp.IconButton iBtnFeedRAxias;
        private FontAwesome.Sharp.IconButton iBtnFeedAxias;
        private FontAwesome.Sharp.IconButton iBtnFeedFromInjectAxias;
        private System.Windows.Forms.Timer timer1;
    }
}