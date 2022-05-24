using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEE.Renault.Step
{
    public enum EnumFirstStation
    {
        /// <summary>
        /// 移动到放料位置
        /// </summary>
        MoveToInjectTakeProPos = 1,
        /// <summary>
        /// 等待云信机械手放料OK信号
        /// </summary>
        WaitYunshinFinishedOk = 2,
        /// <summary>
        /// 移动到三轴同动的上料位置1
        /// </summary>
        MoveToFeedPos1 = 3,
        /// <summary>
        /// 判断料感信号以及气缸初始状态是否正常
        /// </summary>
        JudgeFeedSensorAndCylinderSensor = 4,
        /// <summary>
        /// 打开真空发生器，并退回气缸
        /// </summary>
        OpenFeedVacuumSolenoid = 5,
        /// <summary>
        /// 移动到凸轮分割器的放料位置
        /// </summary>
        MoveToTakeProPos = 6,
        /// <summary>
        /// 检查气缸状态,并控制气缸下降
        /// </summary>
        JudgeCylinderSensorAndControlDown = 7,
        /// <summary>
        /// 检测真空发生器和气缸状态，并控制发生器和气缸
        /// </summary>
        JudgeCloseFeedSensorAndCylinderSensor = 8,
        /// <summary>
        /// 检测气缸退回状态并移动到上料位置2
        /// </summary>
        MoveToFeedPos2 = 9,
        /// <summary>
        /// 判断料感信号以及气缸初始状态是否正常
        /// </summary>
        JudgeFeedSensorAndCylinderSensor1 = 10,
        /// <summary>
        /// 打开真空发生器，并退回气缸
        /// </summary>
        OpenFeedVacuumSolenoid1 = 11,
        /// <summary>
        /// 移动到凸轮分割器的放料位置
        /// </summary>
        MoveToTakeProPos1 = 12,
        /// <summary>
        /// 检查气缸状态,并控制气缸下降
        /// </summary>
        JudgeCylinderSensorAndControlDown1 = 13,
        /// <summary>
        /// 检测真空发生器和气缸状态，并控制发生器和气缸
        /// </summary>
        JudgeCloseFeedSensorAndCylinderSensor1 = 14,
        MoveToFeedPos3 = 15,
        /// <summary>
        /// 判断料感信号以及气缸初始状态是否正常
        /// </summary>
        JudgeFeedSensorAndCylinderSensor2 = 16,
        /// <summary>
        /// 打开真空发生器，并退回气缸
        /// </summary>
        OpenFeedVacuumSolenoid2 = 17,
        /// <summary>
        /// 移动到凸轮分割器的放料位置
        /// </summary>
        MoveToTakeProPos2 = 18,
        /// <summary>
        /// 检查气缸状态,并控制气缸下降
        /// </summary>
        JudgeCylinderSensorAndControlDown2 = 19,
        /// <summary>
        /// 检测真空发生器和气缸状态，并控制发生器和气缸
        /// </summary>
        JudgeCloseFeedSensorAndCylinderSensor2 = 20,
        MoveToFeedPos4 = 21,
        /// <summary>
        /// 判断料感信号以及气缸初始状态是否正常
        /// </summary>
        JudgeFeedSensorAndCylinderSensor3 = 22,
        /// <summary>
        /// 打开真空发生器，并退回气缸
        /// </summary>
        OpenFeedVacuumSolenoid3 = 23,
        /// <summary>
        /// 移动到凸轮分割器的放料位置
        /// </summary>
        MoveToTakeProPos3 = 24,
        /// <summary>
        /// 检查气缸状态,并控制气缸下降
        /// </summary>
        JudgeCylinderSensorAndControlDown3 = 25,
        /// <summary>
        /// 检测真空发生器和气缸状态，并控制发生器和气缸
        /// </summary>
        JudgeCloseFeedSensorAndCylinderSensor3 = 26,
    }

    public enum EnumSecStation
    {
        /// <summary>
        /// 检测所有气缸以及感应器状态包含出标感应器以及Epson给的出标开始信号
        /// </summary>
        SenseAllSensor =1,
        /// <summary>
        /// 出标
        /// </summary>
        Labeling=2,
        /// <summary>
        /// 上下气缸打出
        /// </summary>
        UpDownCylinderOut = 3,
        /// <summary>
        /// 通知Epson贴背胶
        /// </summary>
        NotifyEpsonStickGum =4,
        /// <summary>
        /// Epson通知左右气缸后退
        /// </summary>
        LeftRightCylinderBack =5,
        /// <summary>
        /// 通知Epson气缸后退到位，撕标动作完成
        /// </summary>
        NotifyEpsonLRCylinderBackIsOk =6,
        /// <summary>
        /// 撕标气缸前进
        /// </summary>
        LeftRightCylinderForce = 7,
        /// <summary>
        /// 上下气缸退回，
        /// </summary>
        UpDownCylinderBack =8

    }

    public enum EnumThrStation
    {
        /// <summary>
        /// 检测所有气缸以及感应器状态包含出标感应器以及Epson给的出标开始信号
        /// </summary>
        SenseAllSensor = 1,
        /// <summary>
        /// 气缸打出
        /// </summary>
        CylinderDown=2,
        /// <summary>
        /// 保压时间
        /// </summary>
        PressIsArrived =3,
        /// <summary>
        /// 气缸回退
        /// </summary>
        CylinderUp =4,
    }

    public enum EnumFourStation
    {
        /// <summary>
        /// 检测所有气缸以及感应器状态包含出标感应器以及Epson给的出标开始信号
        /// </summary>
        SenseAllSensor = 1,
        /// <summary>
        /// 气缸打出
        /// </summary>
        CylinderDown = 2,
        /// <summary>
        /// 保压时间
        /// </summary>
        PressIsArrived = 3,
        /// <summary>
        /// 气缸回退
        /// </summary>
        CylinderUp = 4,
    }

    public enum EnumSixStation
    {
        /// <summary>
        /// 检测所有气缸以及感应器状态包含出标感应器以及Epson给的出标开始信号
        /// </summary>
        MoveToFeedPos = 1,
        CylinderDown =2,
        DetectVaccSensor =3,
        CylinderUp =4,
        MoveToOkOrNgPos =5,
        CylinderTakeDown =6,
        CylinderTakeUp =7
    }
}
