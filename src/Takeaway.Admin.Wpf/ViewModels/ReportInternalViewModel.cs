using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using Caliburn.Micro;
using Core.Common.Infrastructure.Autofac;
using Newtonsoft.Json;
using Takeaway.Admin.Util.Services;
using Takeaway.Admin.Wpf.Utility;
using Takeaway.Service.Contract.Entities.Print;
using Takeaway.Service.Contract.Entities.Reports;


namespace Takeaway.Admin.Wpf.ViewModels
{

    public interface IReportInternalViewModel : IScreen
    {
        //TODO: This is internal report, Not External

    }
    [OfflineViewModel]
    public class ReportInternalViewModel : BaseViewModel, IReportInternalViewModel
    {

        const string KeyOrdeType = "ordertype";
        const string KeyValue = "total";

        #region Properties

        private DateTime? _fromDate;
        public DateTime? FromDate
        {
            get
            {
                return this._fromDate;
            }
            set
            {
                if (value != this._fromDate)
                {
                    this._fromDate = value;
                    NotifyOfPropertyChange(() => FromDate);
                }
            }
        }

        private DateTime? _fromDateHistory;
        public DateTime? FromDateHistory
        {
            get
            {
                return this._fromDateHistory;
            }
            set
            {
                if (value != this._fromDateHistory)
                {
                    this._fromDateHistory = value;
                    NotifyOfPropertyChange(() => FromDateHistory);
                }
            }
        }

        private DataTable _results;
        public DataTable Results
        {
            get { return _results; }
            set
            {
                _results = value;
                NotifyOfPropertyChange(() => Results);

            }
        }

        private DataTable _resultsHistory;
        public DataTable ResultsHistory
        {
            get { return _resultsHistory; }
            set
            {
                _resultsHistory = value;
                NotifyOfPropertyChange(() => ResultsHistory);

            }
        }

        private DataTable _resultsDriver;
        public DataTable ResultsDriver
        {
            get { return _resultsDriver; }
            set
            {
                _resultsDriver = value;
                NotifyOfPropertyChange(() => ResultsDriver);

            }
        }

        #endregion

        #region Contructor

        private readonly IOfflineShopService _shopService;
        private readonly IAppScope _appScope;
        private readonly IOnlineService _onlineService;

        public ReportInternalViewModel(IOfflineShopService shopService, IAppScope appScope, IOnlineService onlineService)
        {
            _shopService = shopService;
            _appScope = appScope;
            _onlineService = onlineService;
            IsAdmin = _appScope.IsAdmin;
            FromDateHistory = DateTime.Today;
            Results = CreateEmptyTable();
            ResultsHistory = CreateEmptyTable();
            ResultsDriver = CreateEmptyTable();


        }

        #endregion

        #region Methods

        private static DataTable CreateEmptyTable()
        {
            var r = new DataTable();
            r.Clear();
            r.Columns.Add("Report");
            return r;
        }

        public void DoDailyReport()
        {
            //HardCode Transactional report, 7 in the morning is cut off time
            var report = new WsReport
            {
                ReportTypeId = 2,

            };

            var currentDate = DateTime.Now;
            var yesterday = DateTime.Now.AddDays(-1);


            if (DateTime.Now.Hour <= 7)
                report.FromDate = new DateTime(yesterday.Year, yesterday.Month, yesterday.Day);
            else
                report.FromDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day);

            var transactionReport = _shopService.GetReport(report);
            if (transactionReport != null)
                Results = transactionReport.ReportResults;
        }

        public void DoDriverReport()
        {
            //HardCode Transactional report, 7 in the morning is cut off time
            var report = new WsReport
            {
                ReportTypeId = 11,

            };

            var currentDate = DateTime.Now;
            var yesterday = DateTime.Now.AddDays(-1);


            if (DateTime.Now.Hour <= 7)
                report.FromDate = new DateTime(yesterday.Year, yesterday.Month, yesterday.Day);
            else
                report.FromDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day);

            var transactionReport = _onlineService.GetReport(report);

            if (transactionReport != null)
                ResultsDriver = Deserialize(transactionReport.ReportResults.ToString(), typeof(DataTable));
        }

        public void DoDoDriverReportPrint()
        {
            var report = new WsReport
            {
                ReportTypeId = 12,

            };

            var currentDate = DateTime.Now;
            var yesterday = DateTime.Now.AddDays(-1);


            if (DateTime.Now.Hour <= 7)
                report.FromDate = new DateTime(yesterday.Year, yesterday.Month, yesterday.Day);
            else
                report.FromDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day);


            var transactionReport = _onlineService.GetReport(report);
            DataTable summaryResults =  Deserialize(transactionReport.ReportResults.ToString(), typeof(DataTable));

            if (summaryResults == null || summaryResults.Rows.Count==0)
                return;

            var rawData = new List<WsPrintRawData>();


            foreach (DataRow row in summaryResults.Rows)
            {

                rawData.Add(
                    new WsPrintRawData() { TextPart = row[KeyOrdeType].ToString(), ValuePart = row[KeyValue].ToString() });
            }

            rawData.Add(new WsPrintRawData() { TextPart = report.FromDate.Value.ToString("yyyy.MM.dd"), ValuePart = "" });

            PrinterQueue.PrintReport(GetPrinterSettings(_appScope.Settings), _appScope.ShopInfo, rawData, "Takeaway Receipt");



        }

        private string BuildRowData(DataTable table, int rowId)
        {
            var builder = new StringBuilder();
            builder.AppendLine("-----------------");


            for (var i = 0; i < table.Columns.Count; i++)
            {
                builder.AppendFormat(" [{0}]:  {1} {2}", table.Columns[i].ColumnName, table.Rows[rowId].ItemArray[i], System.Environment.NewLine);
            }
            builder.AppendLine("-----------------");
            return builder.ToString();
        }

        private string BuildDataHeader(DataTable resultsDriver)
        {
            var builder = new StringBuilder();

            for (var i = 0; i < resultsDriver.Columns.Count; i++)
            {
                builder.AppendFormat(" [{0}] ", resultsDriver.Columns[i].ColumnName);
            }
            builder.AppendFormat("{0}", System.Environment.NewLine);

            return builder.ToString();
        }

        public void DoHistoryReport()
        {
            //HardCode Transactional report, 7 in the morning is cut off time
            var report = new WsReport
            {
                ReportTypeId = 2,

            };
            var currentDate = DateTime.Now;
            if (FromDateHistory.HasValue)
                currentDate = FromDateHistory.Value;

            report.FromDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day);

            var transactionReport = _shopService.GetReport(report);

            if (transactionReport != null)
                ResultsHistory = transactionReport.ReportResults;
        }

        public object Deserialize(string jsonText, Type valueType)
        {
            var json = new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore,
                ObjectCreationHandling = ObjectCreationHandling.Replace,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };


            var sr = new StringReader(jsonText);
            var reader = new JsonTextReader(sr);
            var result = json.Deserialize(reader, valueType);
            reader.Close();

            return result;
        }

        public void DoDailyReportPrint()
        {


            //HardCode Transactional report, 7 in the morning is cut off time
            var report = new WsReport
            {
                ReportTypeId = 1,

            };

            var currentDate = DateTime.Now;
            var yesterday = DateTime.Now.AddDays(-1);


            if (DateTime.Now.Hour <= 7)
                report.FromDate = new DateTime(yesterday.Year, yesterday.Month, yesterday.Day);
            else
                report.FromDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day);


            var transactionReport = _shopService.GetReport(report);
            var summaryResults = transactionReport.ReportResults;

            if (summaryResults == null || summaryResults.Rows.Count < 2)
                return;

            var rawData = new List<WsPrintRawData>();


            foreach (var row in summaryResults.Rows)
            {
                rawData.Add(
                    new WsPrintRawData() { TextPart = row[KeyOrdeType].ToString(), ValuePart = row[KeyValue].ToString() });
            }

            rawData.Add(new WsPrintRawData() { TextPart = report.FromDate.Value.ToString("yyyy.MM.dd"), ValuePart = "" });
            
            PrinterQueue.PrintReport(GetPrinterSettings(_appScope.Settings), _appScope.ShopInfo, rawData, "Takeaway Receipt");
        }

        public void DoHistoryReportPrint()
        {


            //HardCode Transactional report, 7 in the morning is cut off time
            var report = new WsReport
            {
                ReportTypeId = 1,

            };

            var currentDate = DateTime.Now;
            if (FromDateHistory.HasValue)
                currentDate = FromDateHistory.Value;

            report.FromDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day);


            var transactionReport = _shopService.GetReport(report);
            var summaryResults = transactionReport.ReportResults;

            if (summaryResults == null || summaryResults.Rows.Count < 2)
                return;

            var rawData = new List<WsPrintRawData>();


            foreach (var row in summaryResults.Rows)
            {
                rawData.Add(
                    new WsPrintRawData() { TextPart = row[KeyOrdeType].ToString(), ValuePart = row[KeyValue].ToString() });
            }

            rawData.Add(new WsPrintRawData() { TextPart = report.FromDate.Value.ToString("yyyy.MM.dd"), ValuePart = "" });

            PrinterQueue.PrintReport(GetPrinterSettings(_appScope.Settings), _appScope.ShopInfo, rawData, "Takeaway Receipt");
        }

        #endregion

    }


}