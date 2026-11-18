using System;
using System.Data;
using System.Data.SqlClient;
using Takeaway.Data.DbContexts;

namespace Takeaway.Data.Repositories
{
    public class StoredProcedureRepository : IStoredProcedureRepository
    {

        public DataTable SummaryReport(DateTime? fromDate, DateTime? toDate, int shopId)
        {
            var dataset = new DataSet();
            using (var conn = new SqlConnection(GetAdoConnectionString()))
            {

                var cmd = new SqlCommand("SP_ReportSummaryReport", conn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = fromDate;
                cmd.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = toDate;
                cmd.Parameters.Add("@ShopId", SqlDbType.Int).Value = shopId;

                var adapter = new SqlDataAdapter { SelectCommand = cmd };
                adapter.Fill(dataset);
            }
            return dataset.Tables[0];
        }

        public DataTable TransactionReport(DateTime fromDate, DateTime toDate, int shopId)
        {
            var dataset = new DataSet();
            using (var conn = new SqlConnection(GetAdoConnectionString()))
            {

                var cmd = new SqlCommand("SP_ReportTransactionReport", conn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = fromDate;
                cmd.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = toDate;
                cmd.Parameters.Add("@ShopId", SqlDbType.Int).Value = shopId;

                var adapter = new SqlDataAdapter { SelectCommand = cmd };
                adapter.Fill(dataset);
            }
            return dataset.Tables[0];
        }

        public DataTable TransactionDrinkReport(DateTime fromDate, DateTime toDate, int shopId)
        {
            var dataset = new DataSet();
            using (var conn = new SqlConnection(GetAdoConnectionString()))
            {

                var cmd = new SqlCommand("SP_ReportDrinkTransactionReport", conn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = fromDate;
                cmd.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = toDate;
                cmd.Parameters.Add("@ShopId", SqlDbType.Int).Value = shopId;

                var adapter = new SqlDataAdapter { SelectCommand = cmd };
                adapter.Fill(dataset);
            }
            return dataset.Tables[0];
        }

        public DataTable OrdeFoodReport(DateTime fromDate, DateTime toDate, int shopId)
        {
            var dataset = new DataSet();
            using (var conn = new SqlConnection(GetAdoConnectionString()))
            {

                var cmd = new SqlCommand("SP_ReportOrdeFoodReport", conn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = fromDate;
                cmd.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = toDate;
                cmd.Parameters.Add("@ShopId", SqlDbType.Int).Value = shopId;

                var adapter = new SqlDataAdapter { SelectCommand = cmd };
                adapter.Fill(dataset);
            }
            return dataset.Tables[0];
        }

        public DataTable DriverReport(DateTime fromDate, DateTime toDate, int shopId)
        {
            var dataset = new DataSet();
            using (var conn = new SqlConnection(GetAdoConnectionString()))
            {

                var cmd = new SqlCommand("SP_ReportDriverReport", conn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = fromDate;
                cmd.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = toDate;
                cmd.Parameters.Add("@ShopId", SqlDbType.Int).Value = shopId;

                var adapter = new SqlDataAdapter { SelectCommand = cmd };
                adapter.Fill(dataset);
            }
            return dataset.Tables[0];
        }

        public DataTable CustomerReport(int shopId)
        {
            var dataset = new DataSet();
            using (var conn = new SqlConnection(GetAdoConnectionString()))
            {

                var cmd = new SqlCommand("SP_ReportCustomerReport", conn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@ShopId", SqlDbType.Int).Value = shopId;
                var adapter = new SqlDataAdapter { SelectCommand = cmd };
                adapter.Fill(dataset);
            }
            return dataset.Tables[0];
        }

        public DataTable GetCustomerOrders(string phoneNumber, int shopId)
        {
            var dataset = new DataSet();
            using (var conn = new SqlConnection(GetAdoConnectionString()))
            {

                var cmd = new SqlCommand("SP_GetCustomerOrders", conn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@Phone", SqlDbType.VarChar).Value = phoneNumber;
                cmd.Parameters.Add("@ShopId", SqlDbType.Int).Value = shopId;

                var adapter = new SqlDataAdapter { SelectCommand = cmd };
                adapter.Fill(dataset);
            }
            return dataset.Tables[0];
        }

        public DataTable BusyHourReport(DateTime fromDate, DateTime toDate, int shopId)
        {
            var dataset = new DataSet();
            using (var conn = new SqlConnection(GetAdoConnectionString()))
            {

                var cmd = new SqlCommand("SP_ReportBusyHourSummary", conn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = fromDate;
                cmd.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = toDate;
                cmd.Parameters.Add("@ShopId", SqlDbType.Int).Value = shopId;

                var adapter = new SqlDataAdapter { SelectCommand = cmd };
                adapter.Fill(dataset);
            }
            return dataset.Tables[0];
        }

        public DataTable CustomerOrderReport(int shopId)
        {
            var dataset = new DataSet();
            using (var conn = new SqlConnection(GetAdoConnectionString()))
            {

                var cmd = new SqlCommand("SP_ReportCustomerOrderReport", conn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add("@ShopId", SqlDbType.Int).Value = shopId;
                var adapter = new SqlDataAdapter { SelectCommand = cmd };
                adapter.Fill(dataset);
            }
            return dataset.Tables[0];
        }



        private string GetAdoConnectionString()
        {

            using (var context = new TakeawayContext())
            {
                return context.Database.Connection.ConnectionString;
            }


        }
    }
}