using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// 有关程序集的一般信息由以下
// 控制。更改这些特性值可修改
// 与程序集关联的信息。
[assembly: AssemblyTitle("KEE.Renault")]
[assembly: AssemblyDescription("峰实电子全自动组装系统")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("KEE")]
[assembly: AssemblyProduct("KEE.Renault")]
[assembly: AssemblyCopyright("Copyright ©  2021")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// 将 ComVisible 设置为 false 会使此程序集中的类型
//对 COM 组件不可见。如果需要从 COM 访问此程序集中的类型
//请将此类型的 ComVisible 特性设置为 true。
[assembly: ComVisible(false)]

// 如果此项目向 COM 公开，则下列 GUID 用于类型库的 ID
[assembly: Guid("80a044cf-d9ad-4a83-b4f8-48d439e345ce")]

// 程序集的版本信息由下列四个值组成: 
//
//      主版本
//      次版本
//      生成号
//      修订号
//
//可以指定所有这些值，也可以使用“生成号”和“修订号”的默认值
//通过使用 "*"，如下所示:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.22.0520.15")]
[assembly: AssemblyFileVersion("1.22.0520.15")]

/*
 * [1.21.1206.1]  次版本表示年份，生成号表示版本修改日期，修订号表示版本修改次数
 * 【21.12.06】 因设备报警，导致的动作异常如射出及不停的放料以及多放料等异常动作
 * 【21.12.07】 因工单号带-符号导致我图片分析保存出现问题，故增加判断工单号带-
 * 【21.12.08】 图片存储为彩色带Graphics图片
 * 【21.12.10】 优化上料真空报警以及感应器异常报警的快速清除动作,增加生技界面上料真空建立时间的设定
 * 【21.12.13】 将机器人真空报警定义位普通报警，只让机器人暂停，不让设备暂停----不行，因为其他报警的时候也可能导致出标轴不停出标，应该将出标的前后动作独立处理啊
 * 【21.12.15】 修改上一次的逻辑，将出标动作独立在报警之外
 * 【21.12.21】 修改以上逻辑中出现的BUG,如供料州在取料位置，有信下来放料，收尾，出标机一直出标，机器人不去贴电镀件,修改第一工站因为轮盘到位信号异常设备静止不动
 * 【21.12.23】 因包胶轮坏掉更换泡棉导致背胶容易拱起，为此增加可选项：出标动作完成后继续出标的脉冲设定
 * 【22.02.16】 修改测试结果数据不匹配丢料的BUG(因）
 * 【22.02.18】 修改VP程序，更改方式是电镀件抓边中心与抓电镀菱形中心，自动补偿
 * 【22.02.25】 修改NG丢料与良品丢料方式的逻辑错误（果）
 * 【22.03.18】 第一工站增加电镀件内外轮廓偏位判断功能,用以判定电镀件是否NG
 * 【22.03.21】 将第一工站的电镀件内外轮廓数据存储，并修改上料异常的动作
 * 【22.03.30】 增加压合和测高上下感应器报警以及机器人暂停修改成报警
 * 【22.05.20】 修改停止后，有信给信号了，供料轴在启动时会移动到上料位置
 */
