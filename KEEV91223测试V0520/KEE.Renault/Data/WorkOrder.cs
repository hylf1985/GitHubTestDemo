using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEE.Renault.Data
{
    public class WorkOrder
    {
        /// <summary>
        /// 工单日期
        /// </summary>
        public DateTime WoDateTime { get; set; } = DateTime.Today;
        /// <summary>
        /// 班别
        /// </summary>
        public string Class { get; set; } = "白班";
        /// <summary>
        /// 料号
        /// </summary>
        public string PartNo { get; set; } = "A3C0995980000";
        /// <summary>
        /// 工单号
        /// </summary>
        public string Wo { get; set; } = "CMAA-2109000263";
        /// <summary>
        /// 排程单号
        /// </summary>
        public string Schedule { get; set; } = "CMGA-2111000112";
        /// <summary>
        /// 项次
        /// </summary>
        public string Item { get; set; } = "6";
        /// <summary>
        /// 是否选择该排程
        /// </summary>
        public bool IsChecked { get; set; } = false;
    }
}
