using System.IO.Ports;

namespace IndustrialTools.Common
{
    public class SystemConfigOption
    {
        public List<CANOption> CANOptions { get; set; } = new List<CANOption>();
        public List<Product> ProductOptions  { get; set; } = new List<Product>();
        public List<SerialPortOption> SerialPortOptions { get; set; } = new List<SerialPortOption>();
        public MesConfigOption MesConfigOption { get; set; } = new MesConfigOption();
    }

    public class Product : Option
    {
        public string Name { get; set; } = "";
        public string  Baudrate { get; set; } = "";
    }

    public class Option
    {
        public string EquipmentStation { get; set; } = "Left";
        public int EquipmentStationNo { get; set; } = 1;
        public string GUID { get; set; } = Guid.NewGuid().ToString("N");
        public bool IsEnabled { get; set; } = false;
    }
    public class MesConfigOption 
    {
        public bool IsEnabled { get; set; } = false;
        public string GUID { get; set; } = Guid.NewGuid().ToString("N");
        public bool IsFormalUrl { get; set; } = false;
        public bool IsTestUrl { get; set; } = false;
        public string FormalUrl { get; set; } = "";
        public string TestUrl { get; set; } = "";
        public int IsPass { get; set; } // 执行结果，1表示Pass，0表示Fail
        public string LineNum { get; set; } = ""; // 行号，由设备提供
        public string BarCode { get; set; } = ""; // 条码SN
        public string TranBarCode { get; set; } = ""; // 转换条码SN
        public string BindBarCode { get; set; } = ""; // 绑定条码SN
        public string DeviceCode { get; set; } = ""; // 设备编号
        public string LineCode { get; set; } = ""; // 拉线编号
        public string CommandCard { get; set; } = ""; // 指令卡
        public string ProcessCode { get; set; } = ""; // 工序号
        public string NGCode { get; set; } = ""; // 不良代码
        public int IsRecordOnly { get; set; } = 0; // 是否只记录不过站
        public string CheckBy { get; set; } = ""; // 创建人工号
    }

    public class SerialPortOption : Option
    {
        public string Name { get; set; } = "";
        public string ComNo { get; set; } = "";
        public int BaudRate { get; set; } = 9600;
        public int DataBits { get; set; } = 1;
        public Parity Parity { get; set; } = Parity.None;
        public StopBits StopBits { get; set; } = StopBits.One;
    }

    public class CANOption : Option
    {
        public string Name { get; set; } = "CAN";
        public CANIdentity CANIdentity { get; set; } = CANIdentity.LYS;
        public UInt32 DevType { get; set; } = 4;
        public UInt32 DevIndex { get; set; } = 0;
        public UInt32 CanIndex { get; set; } = 0;
    }

    public enum CANIdentity
    {
        None = 0,
        LYS = 1,
        TMS = 2,
        ZLG = 3
    }

    public class IPOption : Option
    {
        public string IpAddress { get; set; } = "192.168.0.1";
        public int Port { get; set; } = 5000;
    }

    public class PLCOption : IPOption
    {
        public string Name { get; set; } = "PLC";
    }

    public class VoiceOption : IPOption
    {
        public string Name { get; set; } = "Voice";
    }

    public class InspectedDevicesOption : Option
    {
        public string EquipmentStation { get; set; } = string.Empty;  

        public double NoiseCompensation { get; set; }
    }

}
