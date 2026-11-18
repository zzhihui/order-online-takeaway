using System.Data.SqlClient;
using System.Linq;
using Dapper;
using ServiceStack.Redis;
using Takeaway.Service.Contract.Entities.Shop;

namespace callerId.Utils
{

    public interface ICallerIdDapperRepository
    {
        void SaveItem(WsPhoneRecord item);
    }


    public class CallerIdDapperRepository : ICallerIdDapperRepository
    {
        public void SaveItem(WsPhoneRecord item)
        {
            using (var connection = new SqlConnection(ConnectionString()))
            {
                connection.Open();
                connection.Execute(
                    @"INSERT INTO CallerId 
                    ( number, lineId, shopId,created ) VALUES 
                    ( @Phone, @Channel,@ShopId ,@created );", item);
            }
        }




        private string ConnectionString()
        {
            var sqlBuilder = new SqlConnectionStringBuilder();
            sqlBuilder.DataSource = ".";
            sqlBuilder.InitialCatalog = "takeaway_local";
            sqlBuilder.UserID = "sa";
            sqlBuilder.Password = "REPLACE_ME";
            sqlBuilder.PersistSecurityInfo = true;
            sqlBuilder.IntegratedSecurity = false;
            sqlBuilder.MultipleActiveResultSets = true;
            return sqlBuilder.ToString();
        }


    }
}