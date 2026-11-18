using System;
using System.Data;

namespace Takeaway.Data.Repositories
{
    public interface IStoredProcedureRepository
    {
        DataTable SummaryReport(DateTime? fromDate, DateTime? toDate, int shopId);
        DataTable TransactionReport(DateTime fromDate, DateTime toDate, int shopId);
        DataTable TransactionDrinkReport(DateTime fromDate, DateTime toDate, int shopId);
        DataTable OrdeFoodReport(DateTime fromDate, DateTime toDate, int shopId);
        DataTable DriverReport(DateTime fromDate, DateTime toDate, int shopId);
        DataTable CustomerReport(int shopId);
        DataTable GetCustomerOrders(string phoneNumber, int shopId);

        DataTable BusyHourReport(DateTime fromDate, DateTime toDate, int shopId);
        DataTable CustomerOrderReport(int shopId);
        DataTable TransactionReportOnline(DateTime fromDate, DateTime toDate, int shopId);
        DataTable SummaryOnline(DateTime fromDate, DateTime toDate, int shopId);
        DataTable AdminStatus();
        DataTable CustomerPayOutReport();
        DataTable AdminReportSummaryOnlineReport(DateTime fromDateValue, DateTime toDateValue, int shopId);
        DataTable AdminReportTransactionOnlineReport(DateTime fromDateValue, DateTime toDateValue, int shopId);
        DataTable GetReportInvoiceOnlineReport(DateTime fromDateValue, DateTime toDateValue, int shopId);
        DataTable GetAdminReportInvoiceReport(DateTime fromDateValue, DateTime toDateValue);
    }
}