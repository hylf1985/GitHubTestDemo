using Cognex.VisionPro;
using Cognex.VisionPro.Display;
using Cognex.VisionPro.ToolBlock;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KEE.SaveGriphicsImg
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        CogToolBlock tb = new CogToolBlock();

        private void Form1_Load(object sender, EventArgs e)
        {
            
            Bitmap curImg = new Bitmap(@"D:\SourceCode\c#\AOI项目\新开项目\雷诺组装线\图片\AOI\12-59-14.jpg");
            CogImage8Grey image8Grey = new CogImage8Grey(curImg);
            //
            tb = (CogToolBlock)CogSerializer.LoadObjectFromFile(@"D:\SourceCode\c#\AOI项目\新开项目\雷诺组装线\tb123.vpp", typeof(BinaryFormatter), CogSerializationOptionsConstants.Minimum);
            tb.Inputs["OutputImage"].Value = image8Grey;
            
            cogToolBlockEditV21.Subject = tb;
            tb.Ran += Tb_Ran;
            tb.Run();
            //CamDisplay1.Record = tb.CreateLastRunRecord().SubRecords["CogIPOneImageTool1.OutputImage"];
           
        }

        private void Tb_Ran(object sender, EventArgs e)
        {
            CogDisplayContentBitmapConstants content = new CogDisplayContentBitmapConstants();
            CogRectangle reg = new CogRectangle();
            CamDisplay1.Record = tb.CreateLastRunRecord().SubRecords["CogIPOneImageTool1.OutputImage"];
            try
            {
                Bitmap Image = CamDisplay1.CreateContentBitmap(content, reg, 0) as Bitmap;
                Image.Save(@"C:\Users\Software\Desktop\Image\123.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception)
            {
                throw;
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.tb = cogToolBlockEditV21.Subject;
            CogSerializer.SaveObjectToFile(this.tb,@"D:\SourceCode\c#\AOI项目\新开项目\雷诺组装线\tb123.vpp", typeof(BinaryFormatter), CogSerializationOptionsConstants.Minimum);
        }
    }
}
