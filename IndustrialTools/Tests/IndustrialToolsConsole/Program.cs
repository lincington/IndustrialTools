using IndustrialTools.Common;
using Microsoft.IdentityModel.Logging;
using SqlSugar;

namespace IndustrialToolsConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string MyConnection = "Data Source =192.168.1.70; Port=3306;User ID =root; Password=201015;Initial Catalog =stock;AllowPublicKeyRetrieval=True;Charset=utf8;SslMode=None;Max pool size=10";
            string MsConnection = "server=192.168.1.70,1433;uid=sa;pwd=Zhouenlai@305;database=stock;Encrypt=True;TrustServerCertificate=True;";
            string PgConnection = "HOST=192.168.1.70;PORT=5432;USER ID=postgres;PASSWORD=201015;DATABASE=stock";
            //string OrConnection = "Data Source=192.168.1.70/orcl;User ID=system;Password=haha";
            ComplexInfoModel complexInfoModel = new ComplexInfoModel()
            {
                Key = DbType.MySql,
                Text = "MySql"
            };  

            string interpolated = $"Welcome, {complexInfoModel.Text}!";


            Console.WriteLine(interpolated);

            int result = 0;
            try
            {
                result = SugarDbFactory.New(MyConnection, DbType.MySql).Ado.ExecuteCommand("SELECT  1");
                //result += SugarDbFactory.New(MsConnection, DbType.SqlServer).Ado.ExecuteCommand("SELECT 2");
                //result += SugarDbFactory.New(PgConnection, DbType.PostgreSQL).Ado.ExecuteCommand("SELECT 3");
                //result += SugarDbFactory.New(OrConnection, DbType.OceanBase).Ado.ExecuteCommand("SELECT 4");
            }
            catch (Exception ex)
            {
                LogHelper.LogExceptionMessage(ex);
            }
        }
    }

    public class ComplexInfoModel
    {
        public DbType Key { get; set; }
        public string Text { get; set; }
    }
}
