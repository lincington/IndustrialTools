using IndustrialTools.Common;
using Moq;
using SqlSugar;
using Xunit;

namespace IndustrialTools.Modules.ModuleName.Tests.ViewModels
{
    public class ViewAViewModelFixture
    {
  

        public ViewAViewModelFixture()
        {
 
        }

        [Fact]
        public void MessagePropertyValueSet()
        {

          string MyConnection = "Data Source =192.168.1.70; Port=3306;User ID = root; Password=201015;Initial Catalog = stock; Charset=utf8;SslMode=none;Max pool size=10";
          string MsConnection = "server=192.168.1.70,1433;uid=sa;pwd=Zhouenlai@305;database=stock";
          string PgConnection = "PORT=5432;DATABASE=stock;HOST=192.168.1.70;PASSWORD=123456;USER ID=postgres";
          string OrConnection = "Data Source=192.168.1.70/orcl;User ID=system;Password=haha";

          int result =  SugarDbFactory.New(MyConnection, DbType.MySql).Ado.ExecuteCommand("SELECT 1");
          Assert.Equal(1, result);
          
          result = SugarDbFactory.New(MsConnection, DbType.SqlServer).Ado.ExecuteCommand("SELECT 2");
          Assert.Equal(2, result);
          
          result = SugarDbFactory.New(PgConnection, DbType.PostgreSQL).Ado.ExecuteCommand("SELECT 3");
          Assert.Equal(3, result);
          
          result = SugarDbFactory.New(OrConnection, DbType.OceanBase).Ado.ExecuteCommand("SELECT 4");
          Assert.Equal(4, result);

        }

        [Fact]
        public void MessagePropertyValueUpdated()
        {
            
        }

        [Fact]
        public void MessageINotifyPropertyChangedCalled()
        {
           
        }
    }
}
