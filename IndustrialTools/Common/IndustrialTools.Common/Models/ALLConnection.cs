using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndustrialTools.Common.Models
{
    public class ALLConnection  {}
    public class DBConnection {
        public string GUID { get; set; } = Guid.NewGuid().ToString();
        public string ConnectionString { get; set; }= "";
        public string ConnectionName { get; set; } = "";
        public string Type { get; set; } = "";
        public string Host { get; set; } = "";
        public int Port  { get; set; }
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";
        public int ConnectionTimeout { get; set; } = 1000;
    }
    public class ModbusConnection { }
    public class PLCConnection { }
    public class COMConnection { }
    public class TCPUDPConnection { }
    public class MQConnection { }
    public class RPCConnection { }



}
