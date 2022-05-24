using RCAPINet;
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

namespace KEE.EpsonRobot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private RCAPINet.Spel m_spel ;
        RCAPINet.SpelConnectionInfo spelConnectionInfo;
        private RCAPINet.Spel.EventReceivedEventHandler RobotReceivedEventHandler ;

        private void RobotInitialize()
        {
            try
            {
                spelConnectionInfo = new SpelConnectionInfo();
                m_spel = new RCAPINet.Spel();
                m_spel.Initialize();
                m_spel.Project = "C:\\EpsonRC70\\projects\\vision\\vision.sprj";
                RobotReceivedEventHandler = m_spel_EventReceived;
                m_spel.EventReceived += RobotReceivedEventHandler;
                spelConnectionInfo.ConnectionName = "LS3-B Sample";
                spelConnectionInfo.ConnectionNumber = 9;
                spelConnectionInfo.ConnectionType = SpelConnectionType.Virtual;
                m_spel.ShowWindow(RCAPINet.SpelWindows.Simulator);
                if (!m_spel.MotorsOn)
                {
                    m_spel.MotorsOn = true;
                }
                //m_spel.OperationMode = SpelOperationMode.Program;
                m_spel.Speed(10, 5, 5);
                m_spel.Accel(10, 10, 5, 5, 5, 5);
                m_spel.SpeedS(20, 10, 10);
                m_spel.AccelS(200, 200, 100, 100, 100, 100);
            }
            catch (RCAPINet.SpelException)
            {
                throw;
            }
        }
        public void m_spel_EventReceived(object sender, RCAPINet.SpelEventArgs e)
        {
            try
            {
                switch (e.Event)
                {
                    case SpelEvents.Pause:
                        Console.WriteLine("暂停");
                        break;
                    case SpelEvents.SafeguardOpen:
                        break;
                    case SpelEvents.SafeguardClose:
                        break;
                    case SpelEvents.ProjectBuildStatus:
                        break;
                    case SpelEvents.Error:
                        break;
                    case SpelEvents.Print:
                        break;
                    case SpelEvents.EstopOn:
                        break;
                    case SpelEvents.EstopOff:
                        break;
                    case SpelEvents.Continue:
                        break;
                    case SpelEvents.MotorOn:
                        break;
                    case SpelEvents.MotorOff:
                        break;
                    case SpelEvents.PowerHigh:
                        break;
                    case SpelEvents.PowerLow:
                        break;
                    case SpelEvents.TeachMode:
                        break;
                    case SpelEvents.AutoMode:
                        break;
                    case SpelEvents.TaskStatus:
                        break;
                    case SpelEvents.Shutdown:
                        break;
                    case SpelEvents.AllTasksStopped:
                        break;
                    case SpelEvents.Disconnected:
                        break;
                    default:
                        break;
                }
                MessageBox.Show(e.Message);
                Console.WriteLine("测试成功");
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RobotInitialize();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_spel.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        { 
            m_spel.Start(1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            m_spel.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            m_spel.Pause();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            m_spel.Continue();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                //m_spel.OperationMode = RCAPINet.SpelOperationMode.Program;
                SpelPoint sp = new SpelPoint(0, 450, 260, 0, 90, 180);
                m_spel.PowerHigh = true;
                m_spel.Pallet(1, "P10", "P11", "P12", "P13", 3, 3);
                m_spel.Pallet(2, "P15", "P16", "P17", "P18", 3, 3);
                m_spel.Go("lceshi1");
                
                for (int i = 1; i < 9; i++)
                {
                    if ((i % 2)>0)
                    {
                        
                        m_spel.Jump3("Here -TLZ(50)", "P0 -TLZ(50)", "P0");
                        Thread.Sleep(500);
                        m_spel.Jump3("Here -TLZ(50)", "P1 -TLZ(50)", "P1");
                        Thread.Sleep(500);
                        m_spel.Jump3("Here -TLZ(50)", "P2 -TLZ(50)", "P2");
                    }
                }
                m_spel.Go("Here -TLZ(50)");
                m_spel.Go(sp);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + $"发生了错误:{m_spel.ErrorCode},--{m_spel.GetErrorMessage(m_spel.ErrorCode)}");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text = m_spel.GetRobotPos(SpelRobotPosType.World, 0, 0, 0).ToString();
        }
    }
}
