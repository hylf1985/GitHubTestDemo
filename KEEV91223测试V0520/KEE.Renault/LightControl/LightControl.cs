using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEE.Renault.LightControl
{
    public enum CmdEnum
    {
        打开通道 = 1,
        关闭通道 = 2,
        设置通道亮度 = 3,
        读出通道亮度 = 4
    }
    public enum CHEnum
    {
        通道1 = 1,
        通道2 = 2,
        通道3 = 3,
        通道4 = 4
    }

    public enum WorkStation
    {
        未贴背胶电镀件拍照位 = 1,
        背胶拍照位 = 2,
        贴背胶电镀件拍照位 = 3,
        贴电镀件到塑胶件拍照位 = 4,
        AOI偏位拍照位 = 5
    }
}
