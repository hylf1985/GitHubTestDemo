using csIOC0640;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEE.IODataGridView
{
    public class LsIODIPinDefinition
    {
        private static int id = 0;
        private int thisId = 0;
        private bool pinStatus = false;
        public LsIODIPinDefinition()
        {
            id++;
            thisId = id;
        }

        public static void Resetid()
        {
            id = 0;
        }

        public int ID 
        {
            get { return thisId; }
        }
        public ushort Card { get; set; }
        public ushort PinDefinition { get; set; }
        public bool IoPinStatus
        {
            get { pinStatus = IOC0640.ioc_read_inbit((ushort)((int)Card - 1), PinDefinition) == 0; return pinStatus; }
        }

        public bool CurIOStatus
        {
            get { return pinStatus; }
        }
        public Bitmap PinStatus 
        {
            get 
            {
                if (pinStatus)//pinStatus==io卡读取状态
                {
                    return KEE.IODataGridView.Properties.Resources.RedBtn;
                }
                else
                {
                    return KEE.IODataGridView.Properties.Resources.GrayBtn;
                }
            }
        }
        public string PinDefinitionName { get; set; }
    }

    public class LsIODOPinDefinition
    {
        private static int id = 0;
        private int thisId = 0;
        private bool pinStatus = false;
        public LsIODOPinDefinition()
        {
            id++;
            thisId = id;
        }

        public static void Resetid()
        {
            id = 0;
        }

        public int ID
        {
            get { return thisId; }
        }
        public ushort Card { get; set; }
        public ushort PinDefinition { get; set; }
        public bool IoPinStatus
        {
            get { pinStatus = IOC0640.ioc_read_outbit((ushort)((int)Card - 1), PinDefinition) == 0; return pinStatus; }
        }
        public bool CurIOStatus
        {
            get { return pinStatus; }
        }
        public Bitmap PinStatus
        {
            get
            {
                if (pinStatus)//pinStatus==io卡读取状态
                {
                    return KEE.IODataGridView.Properties.Resources.RedBtn;
                }
                else
                {
                    return KEE.IODataGridView.Properties.Resources.GrayBtn;
                }
            }
        }
        public string PinDefinitionName { get; set; }
    }
}
