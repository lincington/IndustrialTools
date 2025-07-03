
using IndustrialTools.Common.Models;

namespace IndustrialTools.Common
{
    public interface IDataBaseFactory
    {
        public static void New(string ConnectionString, DbType dbType) { }
        public static void ExecuteSql(string ConnectionStringSql, DbType dbType, string dbName) { }
    }


    public class DataBaseFactory : IDataBaseFactory
    {

    }
}
