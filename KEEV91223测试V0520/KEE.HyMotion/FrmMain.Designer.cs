
namespace KEE.HyMotion
{
    partial class FrmMain
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("射出取料轴参数设定");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("上料轴参数设定");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("上料R轴参数设定");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("丢料轴参数设定");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("出标轴参数设定");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Epson");
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.trvMenu = new System.Windows.Forms.TreeView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabInsPos = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabFeedBackPos = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabCurSpeed = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel7 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabMotionStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel9 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabStopReason = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.statusStrip1);
            this.splitContainer1.Size = new System.Drawing.Size(983, 551);
            this.splitContainer1.SplitterDistance = 522;
            this.splitContainer1.TabIndex = 3;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.trvMenu);
            this.splitContainer2.Size = new System.Drawing.Size(983, 522);
            this.splitContainer2.SplitterDistance = 198;
            this.splitContainer2.TabIndex = 4;
            // 
            // trvMenu
            // 
            this.trvMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvMenu.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.trvMenu.FullRowSelect = true;
            this.trvMenu.ItemHeight = 40;
            this.trvMenu.Location = new System.Drawing.Point(0, 0);
            this.trvMenu.Name = "trvMenu";
            treeNode1.Name = "tnFeedFromInjectAxias";
            treeNode1.Tag = "FrmFeedFromInjectAxias";
            treeNode1.Text = "射出取料轴参数设定";
            treeNode2.Name = "tnFeedAxias";
            treeNode2.Tag = "FrmFeedAxias";
            treeNode2.Text = "上料轴参数设定";
            treeNode3.Name = "tnFeedRAxias";
            treeNode3.Tag = "FrmFeedRAxias";
            treeNode3.Text = "上料R轴参数设定";
            treeNode4.Name = "tnTakeAxias";
            treeNode4.Tag = "FrmTakeAxias";
            treeNode4.Text = "丢料轴参数设定";
            treeNode5.Name = "tnLabelingAxias";
            treeNode5.Tag = "FrmLabelingAxias";
            treeNode5.Text = "出标轴参数设定";
            treeNode6.Name = "tnRobot";
            treeNode6.Tag = "FrmRobot";
            treeNode6.Text = "Epson";
            this.trvMenu.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6});
            this.trvMenu.ShowLines = false;
            this.trvMenu.Size = new System.Drawing.Size(198, 522);
            this.trvMenu.TabIndex = 0;
            this.trvMenu.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvMenu_AfterSelect);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.statusLabInsPos,
            this.toolStripStatusLabel3,
            this.statusLabFeedBackPos,
            this.toolStripStatusLabel5,
            this.statusLabCurSpeed,
            this.toolStripStatusLabel7,
            this.statusLabMotionStatus,
            this.toolStripStatusLabel9,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel4,
            this.statusLabStopReason});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(983, 25);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(81, 20);
            this.toolStripStatusLabel1.Text = "指令位置 : ";
            // 
            // statusLabInsPos
            // 
            this.statusLabInsPos.Name = "statusLabInsPos";
            this.statusLabInsPos.Size = new System.Drawing.Size(21, 20);
            this.statusLabInsPos.Text = "--";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(81, 20);
            this.toolStripStatusLabel3.Text = "反馈位置 : ";
            // 
            // statusLabFeedBackPos
            // 
            this.statusLabFeedBackPos.Name = "statusLabFeedBackPos";
            this.statusLabFeedBackPos.Size = new System.Drawing.Size(21, 20);
            this.statusLabFeedBackPos.Text = "--";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(81, 20);
            this.toolStripStatusLabel5.Text = "当前速度 : ";
            // 
            // statusLabCurSpeed
            // 
            this.statusLabCurSpeed.Name = "statusLabCurSpeed";
            this.statusLabCurSpeed.Size = new System.Drawing.Size(21, 20);
            this.statusLabCurSpeed.Text = "--";
            // 
            // toolStripStatusLabel7
            // 
            this.toolStripStatusLabel7.Name = "toolStripStatusLabel7";
            this.toolStripStatusLabel7.Size = new System.Drawing.Size(81, 20);
            this.toolStripStatusLabel7.Text = "运动状态 : ";
            // 
            // statusLabMotionStatus
            // 
            this.statusLabMotionStatus.Name = "statusLabMotionStatus";
            this.statusLabMotionStatus.Size = new System.Drawing.Size(21, 20);
            this.statusLabMotionStatus.Text = "--";
            // 
            // toolStripStatusLabel9
            // 
            this.toolStripStatusLabel9.Name = "toolStripStatusLabel9";
            this.toolStripStatusLabel9.Size = new System.Drawing.Size(81, 20);
            this.toolStripStatusLabel9.Text = "回零结果 : ";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(21, 20);
            this.toolStripStatusLabel2.Text = "--";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(81, 20);
            this.toolStripStatusLabel4.Text = "停止原因 : ";
            // 
            // statusLabStopReason
            // 
            this.statusLabStopReason.Name = "statusLabStopReason";
            this.statusLabStopReason.Size = new System.Drawing.Size(21, 20);
            this.statusLabStopReason.Text = "--";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(983, 551);
            this.Controls.Add(this.splitContainer1);
            this.IsMdiContainer = true;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "轴卡参数设定";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TreeView trvMenu;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabInsPos;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel statusLabFeedBackPos;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel statusLabCurSpeed;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel7;
        private System.Windows.Forms.ToolStripStatusLabel statusLabMotionStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel9;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel statusLabStopReason;
    }
}