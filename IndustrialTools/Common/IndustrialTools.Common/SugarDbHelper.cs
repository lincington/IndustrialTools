using SqlSugar;
using DbType = SqlSugar.DbType;

namespace IndustrialTools.Common
{

    public interface ISugarDbFactory 
    {        
        public static void New(string ConnectionString, DbType dbType)  {     }

        public static void ExecuteSql(string ConnectionStringSql, DbType dbType, string dbName) { }

    }

    public  class SugarDbFactory : ISugarDbFactory
    {
        public static SqlSugarClient New(string ConnectionString, DbType dbType)
        {
           SqlSugarClient SqlServerDb = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = ConnectionString,
                DbType = dbType,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            });

            //调式代码 用来打印SQL 
            SqlServerDb.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql + "\r\n" +
                SqlServerDb.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                Console.WriteLine();
            };
            //SqlServerDb.DbMaintenance.CreateDatabase();
            return SqlServerDb;
        }
    }

    public  class SugarDbContext 
    {

        private static string MyConnection = "Data Source =192.168.1.70; Port=3306;User ID = root; Password=201015;Initial Catalog = stock; Charset=utf8;SslMode=none;Max pool size=10";
        private static string MsConnection = "server=192.168.1.70,1433;uid=sa;pwd=Zhouenlai@305;database=stock";
        private static string PgConnection = "PORT=5432;DATABASE=stock;HOST=192.168.1.70;PASSWORD=201015;USER ID=postgres";
        private static string OrConnection = "Data Source=192.168.1.70/orcl;User ID=system;Password=haha";

        //static  string    SqlServerConnectionString;
        //static  string    MySqlConnectionString;
        //static  string    PostgreSQLConnectionString;
        //static  string    SqliteConnectionString;
        //static  string    OracleConnectionString;

        public SugarDbContext()
        {

            SqlSugarClient SqlServerDb = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = "server=localhost;database=dbname;uid=username;pwd=password;",
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            });

            //调式代码 用来打印SQL 
            SqlServerDb.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql + "\r\n" +
                SqlServerDb.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                Console.WriteLine();
            };

            SqlSugarClient MySqlDb = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = "server=localhost;database=dbname;uid=username;pwd=password;",
                DbType = DbType.MySql,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            });

            //调式代码 用来打印SQL 
            MySqlDb.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql + "\r\n" +
                SqlServerDb.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                Console.WriteLine();
            };

            SqlSugarClient PostgreSQLDb = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = "Host=localhost;Database=dbname;Username=username;Password=password;",
                DbType = DbType.PostgreSQL,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            });
            //调式代码 用来打印SQL 
            PostgreSQLDb.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql + "\r\n" +
                SqlServerDb.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                Console.WriteLine();
            };
            SqlSugarClient Sqlitedb = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = "Data Source=yourdatabasepath.db;", // 确保路径正确，例如 "Data Source=D:\\yourdatabase.db;"
                DbType = DbType.Sqlite,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            });
            //调式代码 用来打印SQL 
            Sqlitedb.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql + "\r\n" +
                SqlServerDb.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                Console.WriteLine();
            };

            SqlSugarClient Oracledb = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = "User Id=username;Password=password;Data Source=localhost:1521/XE;", // 确保使用正确的 Data Source 格式和端口号
                DbType = DbType.Oracle,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            });

            //调式代码 用来打印SQL 
            Oracledb.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql + "\r\n" +
                SqlServerDb.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                Console.WriteLine();
            };
        }
    }
}
