using System.IO.Ports;

namespace IndustrialTools.Common.Models
{
    public class ALLConnection : BindableBase
    {
        public string GUID { get; set; } = Guid.NewGuid().ToString();
        public string ConnectionString { get; set; } = "";
        public string ConnectionName { get; set; } = "localhost";

    }
    public class DBConnection :ALLConnection{
  
        public string Type { get; set; } = "";
        public string Host { get; set; } = "localhost";


        private int _Port = 3306;
        public int Port
        {
            get { return _Port; }
            set { SetProperty(ref _Port, value); }
        }

        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";
        public int ConnectionTimeout { get; set; } = 1000;
    }



    public class ModbusRTUConnection : COMConnection
    {
        public int Slave  { get; set; } = 1;
    }


    public class ModbusTCPConnection : TCPUDPConnection
    {
        public int Slave { get; set; } = 1;
    }
    public class PLCConnection : TCPUDPConnection {
    
    
    }
    public class COMConnection : ALLConnection {

        public string Name { get; set; } = "";
        public string ComNo { get; set; } = "";
        public int BaudRate { get; set; } = 9600;
        public int DataBits { get; set; } = 1;
        public Parity Parity { get; set; } = Parity.None;
        public StopBits StopBits { get; set; } = StopBits.One;


    }
    public class TCPUDPConnection : ALLConnection {

        public string Type { get; set; } = "";
        public string Host { get; set; } = "localhost";
        public int Port { get; set; }


    }
    public class MQConnection : TCPUDPConnection {
    
      
    }
    public class RPCConnection : TCPUDPConnection { }



}
