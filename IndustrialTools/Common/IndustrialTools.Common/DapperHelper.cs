using System.Data;
using System.Data.SQLite;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;
using MySqlConnector;
using Npgsql;
using Oracle.ManagedDataAccess.Client;


namespace IndustrialTools.Common
{
    public class DapperHelper
    {
        private static string ConnectionString = "Data Source=192.168.1.70;User Id=sa;Password=201015;Initial Catalog=Stock;TrustServerCertificate=true;Pooling=true;Min Pool Size=1";
        private static string ConnString = "server=192.168.1.70;userid=root;pwd=201015;port=3306;database=stock;SslMode=none";
        private static string PostgreSQLConnection = "PORT=5432;DATABASE=stock;HOST=192.168.1.70;PASSWORD=201015;USER ID=postgres";
        private static string Connection = "Data Source = 192.168.1.70; Port=3306;User ID = root; Password=201015;Initial Catalog =stock; Charset=utf8;SslMode=none;Max pool size=10";

        string MysqlTable = "SHOW TABLES;";
        string SQLServerTable = "SELECT name FROM sys.tables;";
        string PostgreSQLTable = "SELECT table_name FROM information_schema.tables WHERE table_schema = 'public';\r\n";
        string SQLiteTable = "SELECT name FROM sqlite_master WHERE type='table';";

        public DapperHelper()
        {

            // SQL Server
            using (IDbConnection db = new SqlConnection("Server=localhost;Database=TestDb;User Id=sa;Password=your_password;"))
            {
                try
                {
                    db.Open();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            // MySQL
            using (IDbConnection db = new MySqlConnection("Server=localhost;Database=TestDb;User=root;Password=your_password;"))
            {
                try
                {
                    db.Open();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            // PostgreSQL
            using (IDbConnection db = new NpgsqlConnection("Host=localhost;Database=TestDb;Username=postgres;Password=your_password"))
            {
                try
                {
                    db.Open();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            // SQLite
            using (IDbConnection db = new SQLiteConnection("Data Source=TestDb.sqlite;Version=3;"))
            {
                try
                {
                    db.Open();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            // Oracle
            using (IDbConnection db = new OracleConnection("User Id=your_user;Password=your_password;Data Source=localhost:1521/xe;"))
            {
                try
                {
                    db.Open();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }


        /// <summary>
        /// 添加.
        /// </summary>
        /// <typeparam name="T">实体类型.</typeparam>
        /// <param name="sql">传入sql执行语句.</param>
        /// <param name="t">传入实体类型.</param>
        /// <returns>int.</returns>
        public static int Add<T>(string sql, T t) where T : class
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                return connection.Execute(sql, t);
            }
        }

        /// <summary>
        /// 批量添加.
        /// </summary>
        /// <typeparam name="T">实体类型.</typeparam>
        /// <param name="sql">传入sql执行语句.</param>
        /// <param name="t">传入泛型类.</param>
        /// <returns>int.</returns>
        public static int Add<T>(string sql, List<T> t) where T : class
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                return connection.Execute(sql, t);
            }
        }

        /// <summary>
        /// 删除.
        /// </summary>
        /// <typeparam name="T">实体类型.</typeparam>
        /// <param name="sql">传入sql执行语句.</param>
        /// <param name="t">传入实体类型.</param>
        /// <returns>int.</returns>
        public static int Delete<T>(string sql, T t) where T : class
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                return connection.Execute(sql, t);
            }
        }

        /// <summary>
        /// 批量删除.
        /// </summary>
        /// <typeparam name="T">实体类型.</typeparam>
        /// <param name="sql">传入sql执行语句.</param>
        /// <param name="t">传入泛型类.</param>
        /// <returns>int.</returns>
        public static int Delete<T>(string sql, List<T> t) where T : class
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                return connection.Execute(sql, t);
            }
        }

        /// <summary>
        /// 修改.
        /// </summary>
        /// <typeparam name="T">实体类型.</typeparam>
        /// <param name="sql">传入sql执行语句.</param>
        /// <param name="t">传入实体类型.</param>
        /// <returns>int.</returns>
        public static int Update<T>(string sql, T t) where T : class
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                return connection.Execute(sql, t);
            }
        }

        /// <summary>
        /// 批量修改.
        /// </summary>
        /// <typeparam name="T">实体类型.</typeparam>
        /// <param name="sql">传入sql执行语句.</param>
        /// <param name="t">传入泛型类.</param>
        /// <returns>int.</returns>
        public static int Update<T>(string sql, List<T> t) where T : class
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                return connection.Execute(sql, t);
            }
        }

        /// <summary>
        /// 查询.
        /// </summary>
        /// <typeparam name="T">实体类型.</typeparam>
        /// <param name="sql">传入sql执行语句.</param>
        /// <returns>泛型类.</returns>
        public static List<T> Query<T>(string sql) where T : class
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                return connection.Query<T>(sql).ToList();
            }
        }

        /// <summary>
        /// 查询指定数据.
        /// </summary>
        /// <typeparam name="T">实体类型.</typeparam>
        /// <param name="sql">传入sql执行语句.</param>
        /// <param name="t">传入泛型类.</param>
        /// <returns>类.</returns>
        public static T Query<T>(string sql, T t) where T : class
        {
            using IDbConnection connection = new SqlConnection(ConnectionString);
            return connection.Query<T>(sql, t).SingleOrDefault();
        }

        /// <summary>
        /// 查询的in操作.
        /// </summary>
        /// <typeparam name="T">实体类型.</typeparam>
        /// <param name="sql">传入sql执行语句.</param>
        /// <returns>泛型类.</returns>
        public static List<T> Query<T>(string sql, int[] ids)  where T : class
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                return connection.Query<T>(sql, new { ids }).ToList();
            }
        }

        /// <summary>
        /// 多语句操作.
        /// </summary>
        /// <typeparam name="T">实体类型.</typeparam>
        /// <param name="sql">传入sql执行语句.</param>
        public static void QueryMultiple(string sql )
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                var multiReader = connection.QueryMultiple(sql);
                //var userInfo = multiReader.Read<UserInfo>();
                //var student = multiReader.Read<Student>();
                multiReader.Dispose();
            }
        }

        /// <summary>
        /// dapper通用分页方法
        /// </summary>
        /// <typeparam name="T">泛型集合实体类</typeparam>
        /// <param name="conn">数据库连接池连接对象</param>
        /// <param name="files">列</param>
        /// <param name="tableName">表</param>
        /// <param name="where">条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前页显示条数</param>
        /// <param name="total">结果集总数</param>
        /// <returns></returns>
        public static IEnumerable<T> GetPageList<T>(IDbConnection conn, string files, string tableName, string where, string orderby, int pageIndex, int pageSize, out int total)
        {
            int skip = 1;
            if (pageIndex > 0)
            {
                skip = (pageIndex - 1) * pageSize + 1;
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT COUNT(1) FROM {0} where {1};", tableName, where);
            sb.AppendFormat(@"SELECT  {0}
                                FROM(SELECT ROW_NUMBER() OVER(ORDER BY {3}) AS RowNum,{0}
                                          FROM  {1}
                                          WHERE {2}
                                        ) AS result
                                WHERE  RowNum >= {4}   AND RowNum <= {5}
                                ORDER BY {3}", files, tableName, where, orderby, skip, pageIndex * pageSize);
            using (var reader = conn.QueryMultiple(sb.ToString()))
            {
                total = reader.ReadFirst<int>();
                return reader.Read<T>();
            }
        }
    }

    #region =================================================================================================================================== 
    // --------------------------------------------------------------添加----------------------------------------------------------------------
    // UserInfo user = new UserInfo();
    // user.Name = "九九";
    // user.Age = 10;
    // user.Memo = "这是备注";
    // user.CreateTime = DateTime.Now;

    // string sql = "Insert into UserInfo(Name,Age,Memo,CreateTime) values (@name, @Age, @Memo,@CreateTime)";
    // int result = DapperTools.Add<UserInfo>(sql, user);
    // if (result > 0)
    // {
    //     Console.WriteLine("添加成功");
    //     Console.ReadKey();
    // }

    // --------------批量添加--------------
    // UserInfo user = new UserInfo();
    // user.Name = "李奎";
    // user.Age = 50;
    // user.Memo = "这是备注";
    // user.CreateTime = DateTime.Now;

    // UserInfo user2 = new UserInfo();
    // user2.Name = "梁山伯";
    // user2.Age = 54;
    // user2.Memo = "这是备注";
    // user2.CreateTime = DateTime.Now;

    // List<UserInfo> list = new List<UserInfo>();
    // list.Add(user);
    // list.Add(user2);

    // string sql = "Insert into UserInfo(Name,Age,Memo,CreateTime) values (@name, @Age, @Memo,@CreateTime)";
    // int result = DapperTools.Add<UserInfo>(sql, list);
    // if (result > 0)
    // {
    //     Console.WriteLine("添加成功");
    //     Console.ReadKey();
    // }

    // ---------------------------------------------------------删除------------------------------------------------------------------
    // UserInfo user = new UserInfo();
    // user.Id = 18;

    // string sql = "delete from UserInfo where Id=@Id";
    // int result = DapperTools.Delete<UserInfo>(sql, user);
    // if (result > 0)
    // {
    //     Console.WriteLine("删除成功");
    //     Console.ReadKey();
    // }

    // --------------批量删除--------------
    // UserInfo user = new UserInfo();
    // user.Id = 15;

    // UserInfo user2 = new UserInfo();
    // user2.Id = 16;

    // List<UserInfo> list = new List<UserInfo>();
    // list.Add(user);
    // list.Add(user2);

    // string sql = "delete from UserInfo where Id=@Id";
    // int result = DapperTools.Delete<UserInfo>(sql, list);
    // if (result > 0)
    // {
    //     Console.WriteLine("添加成功");
    //     Console.ReadKey();
    // }

    // --------------修改--------------
    // UserInfo user = new UserInfo();
    // user.Id = 14;
    // user.Name = "九九";

    // string sql = "update UserInfo set Name=@Name,UpdateTime=GETDATE() where Id=@ID";
    // int result = DapperTools.Update<UserInfo>(sql, user);
    // if (result > 0)
    // {
    //     Console.WriteLine("修改成功");
    //     Console.ReadKey();
    // }

    // -------------------------------------批量修改---------------------------------------------
    // UserInfo user = new UserInfo();
    // user.Id = 13;
    // user.Name = "王文";

    // UserInfo user2 = new UserInfo();
    // user2.Id = 14;
    // user2.Name = "冰冰";

    // List<UserInfo> list = new List<UserInfo>();
    // list.Add(user);
    // list.Add(user2);

    // string sql = "update UserInfo set Name=@Name,UpdateTime=GETDATE() where Id=@ID";
    // int result = DapperTools.Update<UserInfo>(sql, list);
    // if (result > 0)
    // {
    //     Console.WriteLine("修改成功");
    //     Console.ReadKey();
    // }

    // ---------------------------------------查询--------------------------------------------------
    // string sql = "select * from UserInfo";
    // List<UserInfo> list = DapperTools.Query<UserInfo>(sql);
    // foreach (var item in list)
    // {
    //     Console.WriteLine(item.Id + "-" + item.Name + "-" + item.Age + "-" + item.Memo);
    // }

    // Console.ReadKey();

    // -----------------------------------------查询指定数据------------------------------------------
    // UserInfo user = new UserInfo();
    // user.Id = 14;

    // string sql = "select * from UserInfo where Id=@Id";
    // UserInfo userInfo = DapperTools.Query<UserInfo>(sql, user);

    // Console.WriteLine(userInfo.Id + "-" + userInfo.Name + "-" + userInfo.Age + "-" + userInfo.Memo);
    // Console.ReadKey();

    // -------------------------------------------查询的in操作------------------------------------------
    // string sql = "select * from UserInfo where Id in @ids";
    // int[] ids = { 1, 2 };

    // List<UserInfo> list = DapperTools.Query<UserInfo>(sql, ids);
    // foreach (var item in list)
    // {
    //     Console.WriteLine(item.Id + "-" + item.Name + "-" + item.Age + "-" + item.Memo);
    // }

    // --------------------------------------------多语句操作---------------------------------------------
    // string sql = "select * from userinfo;select * from student";
    // DapperTools.QueryMultiple(sql);
    #endregion


}
