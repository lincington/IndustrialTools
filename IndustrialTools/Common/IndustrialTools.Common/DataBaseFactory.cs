using Dapper;
using IndustrialTools.Common.Models;
using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
using Npgsql;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Data.SqlClient;

namespace IndustrialTools.Common
{
    public interface IDataBaseFactory
    {
        public bool Test(string ConnectionString, DataBaseType dbType);
        public   List<TreeNode> MakeSure(string ConnectionString, DataBaseType dbType);
        public   DataTable  ExecuteSql(string ConnectionStringSql, DataBaseType dbType);
    }

    public class DataBaseFactory : IDataBaseFactory
    {
        string MysqlDB = "SELECT SCHEMA_NAME as DBNAME FROM information_schema.SCHEMATA;";
        string SQLServerDB = "SELECT  name as DBNAME FROM sys.databases;";
        string PostgreSQLDB = "SELECT   datname as DBNAME FROM pg_database;";

        string MysqlTable = "SHOW TABLES;";
        string SQLServerTable = "SELECT name FROM sys.tables;";
        string PostgreSQLTable = "SELECT table_name FROM information_schema.tables WHERE table_schema = 'public';";

        public bool IsConnected { get; set; } = false;
        public DataTable ExecuteSql(string ConnectionStringSql, DataBaseType dbType)
        {
            throw new NotImplementedException();
        }

        public List<TreeNode> MakeSure(string ConnectionString, DataBaseType dbType)
        {

            List<TreeNode> result = new List<TreeNode>();
            switch (dbType)
            {
                case DataBaseType.MySql:
                    // MySQL
                    using (IDbConnection db = new MySqlConnection(ConnectionString))
                    {
                        try
                        {
                            db.Open();
                            db.Query(MysqlDB).ToList().ForEach(x =>
                            {
                                TreeNode node = new TreeNode(x.DBNAME);
                                string dataname = Convert.ToString(x.DBNAME);
                                string MysqlTablesql = "use " + dataname + ";" + MysqlTable;
                                List<string>    gh = db.Query<string>(MysqlTablesql).ToList();

                                gh.ForEach(x =>
                                {
                                    TreeNode node2 = new TreeNode(x, Convert.ToString(dataname)+"."+ x);
                                    node.Children.Add(node2);
                                });

                                result.Add(node);
                            });
                            IsConnected = true;
                        }
                        catch (Exception)
                        {
                            IsConnected = false;
                        }
                    }
                    break;
                case DataBaseType.SqlServer:
                    // SQL Server
                    using (IDbConnection db = new SqlConnection(ConnectionString))
                    {
                        try
                        {
                            db.Open();
                            IsConnected = true;
                        }
                        catch (Exception)
                        {
                            IsConnected = false;
                        }
                    }
                    break;
                case DataBaseType.PostgreSQL:
                    // PostgreSQL
                    using (IDbConnection db = new NpgsqlConnection(ConnectionString))
                    {
                        try
                        {
                            db.Open();
                            IsConnected = true;
                        }
                        catch (Exception)
                        {
                            IsConnected = false;
                        }
                    }
                    break;
                case DataBaseType.Oracle:
                    // Oracle
                    using (IDbConnection db = new OracleConnection(ConnectionString))
                    {
                        try
                        {
                            db.Open();
                            IsConnected = true;
                        }
                        catch (Exception)
                        {
                            IsConnected = false;
                        }
                    }
                    break;

                case DataBaseType.SQLite:
                    // SQLite
                    using (IDbConnection db = new SqliteConnection("Data Source=TestDb.sqlite;Version=3;"))
                    {
                        try
                        {
                            db.Open();
                            IsConnected = true;
                        }
                        catch (Exception)
                        {
                            IsConnected = false;
                        }
                    }

                    break;
                default:
                    break;
            }
            return result;

        }

        public bool Test(string ConnectionString, DataBaseType dbType)
        {
            switch (dbType)
            {
                case DataBaseType.MySql:
                    // MySQL
                    using (IDbConnection db = new MySqlConnection(ConnectionString))
                    {
                        try
                        {
                            db.Open();
                            IsConnected = true;
                        }
                        catch (Exception )
                        {
                            IsConnected = false;
                        }
                    }
                    break;
                case DataBaseType.SqlServer:
                    // SQL Server
                    using (IDbConnection db = new SqlConnection(ConnectionString))
                    {
                        try
                        {
                            db.Open();
                            IsConnected = true;
                        }
                        catch (Exception)
                        {
                            IsConnected = false;
                        }
                    }
                    break;
                case DataBaseType.PostgreSQL:
                    // PostgreSQL
                    using (IDbConnection db = new NpgsqlConnection(ConnectionString))
                    {
                        try
                        {
                            db.Open();
                            IsConnected = true;
                        }
                        catch (Exception)
                        {
                            IsConnected = false;
                        }
                    }
                    break;
                case DataBaseType.Oracle:
                    // Oracle
                    using (IDbConnection db = new OracleConnection(ConnectionString))
                    {
                        try
                        {
                            db.Open();
                            IsConnected = true;
                        }
                        catch (Exception)
                        {
                            IsConnected = false;
                        }
                    }
                    break;

                case DataBaseType.SQLite:
                    // SQLite
                    using (IDbConnection db = new SqliteConnection("Data Source=TestDb.sqlite;Version=3;"))
                    {
                        try
                        {
                            db.Open();
                            IsConnected = true;
                        }
                        catch (Exception)
                        {
                            IsConnected = false;
                        }
                    }

                    break;
                default:
                    break;
            }
            return IsConnected;
        }
    }
}
