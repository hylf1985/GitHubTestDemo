using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEE.Renault.Data
{
    public class TestData
    {
        //public int Id { get; set; }

        public string FinalBarcode { get; set; }

        public string SN { get; set; }

        public string WO { get; set; }

        public string Class { get; set; }

        public string HightValue { get; set; }

        public string HightRes { get; set; }

        public DateTime HightTestDt { get; set; }

        public string XVCentDis { get; set; }
        
        public string YVCentDis { get; set; }

        public string Deg { get; set; }

        public string AOIRes { get; set; }

        public DateTime AOITestDt { get; set; }

        public string FinalTestRes { get; set; }

        public string UserId { get; set; }

        public bool IsDel { get; set; }

    }

    public class HighData
    {
        public string HightValue { get; set; }

        public string HightRes { get; set; }

        public DateTime HightTestDt { get; set; }
    }

    public class AoiData
    {
        public string XVCentDis { get; set; }

        public string YVCentDis { get; set; }

        public string Deg { get; set; }

        public string AOIRes { get; set; }

        public DateTime AOITestDt { get; set; }
    }
}
