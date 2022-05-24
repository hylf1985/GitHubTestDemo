using ClassINI;
using KEE.Renault.Utility;
using RegalPrinter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KEE.Renault.MenuForm
{
    public partial class FrmSysSetting : Form
    {
        public FrmSysSetting()
        {
            InitializeComponent();
            InitOffsetVal();
            InitSystemCheckItem();
        }

        private void btnSaveOffset_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalVar.firToolXOffset = Convert.ToDouble(txtBoxTool1X.Text);
                GlobalVar.firToolYOffset = Convert.ToDouble(txtBoxTool1Y.Text);
                GlobalVar.firToolDegOffset = Convert.ToDouble(txtBoxTool1θ.Text);
                GlobalVar.secToolXOffset = Convert.ToDouble(txtBoxTool2X.Text);
                GlobalVar.secToolYOffset = Convert.ToDouble(txtBoxTool2Y.Text);
                GlobalVar.secToolDegOffset = Convert.ToDouble(txtBoxTool2θ.Text);
                GlobalVar.thrToolXOffset = Convert.ToDouble(txtBoxTool3X.Text);
                GlobalVar.thrToolYOffset = Convert.ToDouble(txtBoxTool3Y.Text);
                GlobalVar.thrToolDegOffset = Convert.ToDouble(txtBoxTool3θ.Text);
                GlobalVar.fourToolXOffset = Convert.ToDouble(txtBoxTool4X.Text);
                GlobalVar.fourToolYOffset = Convert.ToDouble(txtBoxTool4Y.Text);
                GlobalVar.fourToolDegOffset = Convert.ToDouble(txtBoxTool4θ.Text);
                GlobalVar.fiveToolXOffset = Convert.ToDouble(txtBoxTool5X.Text);
                GlobalVar.fiveToolYOffset = Convert.ToDouble(txtBoxTool5Y.Text);
                GlobalVar.fiveToolDegOffset = Convert.ToDouble(txtBoxTool5θ.Text);
                GlobalVar.sixToolXOffset = Convert.ToDouble(txtBoxTool6X.Text);
                GlobalVar.sixToolYOffset = Convert.ToDouble(txtBoxTool6Y.Text);
                GlobalVar.sixToolDegOffset = Convert.ToDouble(txtBoxTool6θ.Text);
                ClassINI.INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "治具补偿参数", "Tool1X", GlobalVar.firToolXOffset.ToString());
                ClassINI.INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "治具补偿参数", "Tool1Y", GlobalVar.firToolYOffset.ToString());
                ClassINI.INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "治具补偿参数", "Tool1Deg", GlobalVar.firToolDegOffset.ToString());
                ClassINI.INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "治具补偿参数", "Tool2X", GlobalVar.secToolXOffset.ToString());
                ClassINI.INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "治具补偿参数", "Tool2Y", GlobalVar.secToolYOffset.ToString());
                ClassINI.INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "治具补偿参数", "Tool2Deg", GlobalVar.secToolDegOffset.ToString());
                ClassINI.INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "治具补偿参数", "Tool3X", GlobalVar.thrToolXOffset.ToString());
                ClassINI.INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "治具补偿参数", "Tool3Y", GlobalVar.thrToolYOffset.ToString());
                ClassINI.INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "治具补偿参数", "Tool3Deg", GlobalVar.thrToolDegOffset.ToString());
                ClassINI.INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "治具补偿参数", "Tool4X", GlobalVar.fourToolXOffset.ToString());
                ClassINI.INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "治具补偿参数", "Tool4Y", GlobalVar.fourToolYOffset.ToString());
                ClassINI.INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "治具补偿参数", "Tool4Deg", GlobalVar.fourToolDegOffset.ToString());
                ClassINI.INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "治具补偿参数", "Tool5X", GlobalVar.fiveToolXOffset.ToString());
                ClassINI.INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "治具补偿参数", "Tool5Y", GlobalVar.fiveToolYOffset.ToString());
                ClassINI.INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "治具补偿参数", "Tool5Deg", GlobalVar.fiveToolDegOffset.ToString());
                ClassINI.INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "治具补偿参数", "Tool6X", GlobalVar.sixToolXOffset.ToString());
                ClassINI.INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "治具补偿参数", "Tool6Y", GlobalVar.sixToolYOffset.ToString());
                ClassINI.INI.INIWriteValue(GlobalVar.bRecipeRenaultFilePath, "治具补偿参数", "Tool6Deg", GlobalVar.sixToolDegOffset.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InitOffsetVal()
        {
            txtBoxTool1X.Text = GlobalVar.firToolXOffset.ToString();
            txtBoxTool1Y.Text = GlobalVar.firToolYOffset.ToString();
            txtBoxTool1θ.Text = GlobalVar.firToolDegOffset.ToString();
            txtBoxTool2X.Text = GlobalVar.secToolXOffset.ToString();
            txtBoxTool2Y.Text = GlobalVar.secToolYOffset.ToString();
            txtBoxTool2θ.Text = GlobalVar.secToolDegOffset.ToString();
            txtBoxTool3X.Text = GlobalVar.thrToolXOffset.ToString();
            txtBoxTool3Y.Text = GlobalVar.thrToolYOffset.ToString();
            txtBoxTool3θ.Text = GlobalVar.thrToolDegOffset.ToString();
            txtBoxTool4X.Text = GlobalVar.fourToolXOffset.ToString();
            txtBoxTool4Y.Text = GlobalVar.fourToolYOffset.ToString();
            txtBoxTool4θ.Text = GlobalVar.fourToolDegOffset.ToString();
            txtBoxTool5X.Text = GlobalVar.fiveToolXOffset.ToString();
            txtBoxTool5Y.Text = GlobalVar.fiveToolYOffset.ToString();
            txtBoxTool5θ.Text = GlobalVar.fiveToolDegOffset.ToString();
            txtBoxTool6X.Text = GlobalVar.sixToolXOffset.ToString();
            txtBoxTool6Y.Text = GlobalVar.sixToolYOffset.ToString();
            txtBoxTool6θ.Text = GlobalVar.sixToolDegOffset.ToString();
            txtPrintX.Text = GlobalVar.printX;
            txtPrintY.Text = GlobalVar.printY;
            txtUploadFolder.Text = GlobalVar.fileServerPath;
        }

        private void chkBoxSafeDoorIsClose_CheckedChanged(object sender, EventArgs e)
        {
            GlobalVar.isCloseSafeDoor = chkBoxSafeDoorIsClose.Checked;
            ClassINI.INI.INIWriteValue(GlobalVar.bConfigFilePath, "System", "是否屏蔽安全门", GlobalVar.isCloseSafeDoor ? "true" : "false");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbPrintContext.Text))
            {
                ActionPrinttest(tbPrintContext.Text);
            }
            else
            {
                ActionPrinttest("ceshi12323232");
            }
            //Ring r = new Ring();
            //if (!r.PrinterWorkOffline("Ring 4012PIM"))
            //{
            //    r.StartPrinter("Ring 4012PIM", "ring");
            //    r.FMT(1, "30", "8", false, 0, 1);//设定标签纸的长和宽
            //    r.DMD(1);
            //    r.DPD(1);
            //    r.ACL();
            //    r.FAG(2);
            //    r.DMX(GlobalVar.printX, GlobalVar.printY, 24, 24, 3, 6, 3, 0, "1212");
            //    r.PRT(1, 0, 1);
            //    Thread.Sleep(100);
            //    r.EndPrinter();
            //}
        }
        private void ActionPrinttest(string finalBarcode)
        {
            Ring r = new Ring();
            if (!r.PrinterWorkOffline("Ring 4012PIM"))
            {
                r.StartPrinter("Ring 4012PIM", "ring");
                r.FMT(1, "30", "8", false, 0, 1);//设定标签纸的长和宽
                r.DMD(1);
                r.DPD(1);
                r.ACL();
                r.FAG(2);
                r.DMX(GlobalVar.printX, GlobalVar.printY, 24, 24, 3, 6, 3, 0, finalBarcode);
                r.PRT(1, 0, 1);
                Thread.Sleep(100);
                r.EndPrinter();
            }
        }

        private void btnSavePrintParam_Click(object sender, EventArgs e)
        {
            GlobalVar.printX = txtPrintX.Text;
            GlobalVar.printY = txtPrintY.Text;
            ClassINI.INI.INIWriteValue(GlobalVar.bConfigFilePath, "打印机", "X方向起点", GlobalVar.printX);
            ClassINI.INI.INIWriteValue(GlobalVar.bConfigFilePath, "打印机", "Y方向起点", GlobalVar.printY);
           
        }

        private void btnSaveUpServerPath_Click(object sender, EventArgs e)
        {
            GlobalVar.fileServerPath = txtUploadFolder.Text;//@"\\10.65.4.200\文件服务器\公用\test"
            INI.INIWriteValue(GlobalVar.bConfigFilePath, "System", "文件服务器路径", GlobalVar.fileServerPath);
        }

        private void chkBoxIsAnalyPic_CheckedChanged(object sender, EventArgs e)
        {
            GlobalVar.isAnalysisPic = chkBoxIsAnalyPic.Checked;
            ClassINI.INI.INIWriteValue(GlobalVar.bConfigFilePath, "System", "是否追踪分析图片", GlobalVar.isAnalysisPic ? "true" : "false");
        }

        private void chkBoxIsRecordTraceLog_CheckedChanged(object sender, EventArgs e)
        {
            GlobalVar.isRecordTraceLog = chkBoxIsRecordTraceLog.Checked;
            ClassINI.INI.INIWriteValue(GlobalVar.bConfigFilePath, "System", "是否记录Trace日志", GlobalVar.isRecordTraceLog ? "true" : "false");
        }

        private void InitSystemCheckItem()
        {
            chkBoxSafeDoorIsClose.Checked = GlobalVar.isCloseSafeDoor;
            chkBoxIsRecordTraceLog.Checked = GlobalVar.isRecordTraceLog;
            chkBoxIsAnalyPic.Checked = GlobalVar.isAnalysisPic;
        }
    }
}
