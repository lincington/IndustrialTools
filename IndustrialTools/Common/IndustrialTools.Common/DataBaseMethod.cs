

namespace IndustrialTools.Common
{

    public interface IDataBaseSettings
    {
        public string ConnectionString { get; set; } 

    }

    public class DataBaseSettings : IDataBaseSettings
    {
        public string ConnectionString { get ; set ; }= string.Empty;
    }

    public class MySql {}
    public class   SqlServer { }
    public class   Oracle { }
    public class   PostgreSQL { }
    public class   SQLite { }
    public class   Access { }
    public class   MongoDB { }
    public class   Redis { }

}
