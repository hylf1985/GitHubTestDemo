using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEE.Renault.MyMotion
{
   
    public enum StopModelEnum
    {
        减速停止 = 0,
        立即停止 = 1
    }
    public enum CoordModelEnum
    {
        相对坐标 = 0,
        绝对坐标 = 1
    }
    public enum HomeModel
    {
        一次回零 = 0,
        一次回零加反找 = 1,
        两次回零 = 2,
        一次回零加EZ = 3,//中文+
        EZ回零 = 4,
        一次回零加反找EZ = 5,
        原点锁存 = 6,
        原点加EZ锁存 = 7,
        EZ锁存 = 8,
        原点加反向EZ锁存 = 9,
        限位一次回零 = 10,
        限位回零反找 = 11,
        限位两次回零 = 12
    }
    public enum HomeSpeedModel
    {
        低速回零 = 0,
        高速回零 = 1
    }
}
