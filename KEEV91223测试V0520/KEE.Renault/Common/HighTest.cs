using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KEE.Renault.Utility;
using Modbus.Device;

namespace KEE.Renault.Common
{
    public  class HighTest
    {
        public static ushort[] ModbusSerialRtuMasterReadRegisters(string com)
        {
            using (SerialPort port = new SerialPort(com))
            {
                ushort[] res = { 0,0};
                try
                {
                    port.BaudRate = 19200;
                    port.DataBits = 8;
                    port.Parity = Parity.Even;
                    port.StopBits = StopBits.One;
                    if (!port.IsOpen)
                    {
                        port.Open();
                    }
                    var adapter = new SerialPortAdapter(port);
                    // create modbus master
                    IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(adapter);

                    byte slaveId = 1;
                    ushort startAddress = 100;

                    // write three registers
                    res = master.ReadHoldingRegisters(slaveId, startAddress, 2);
                }
                catch (Exception ex)
                {
                    GlobalVar.myLog.Error($"测高模组的串口出现异常错误: {ex.Message}");
                }
                // configure serial port
                
                return res;
            }
        }
    }
}
