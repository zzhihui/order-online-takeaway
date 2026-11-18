using System;
using System.Data;
using System.IO;
using System.Web.Mvc;
using Newtonsoft.Json;
using Takeaway.Service.Contract.Entities.Reports;
using Takeway.Web.Application.Extension;
using Takeway.Web.Application.Filter;
using Takeway.Web.Application.Utility;
using Takeway.Web.ViewModels.Reports;

namespace Takeway.Web.Controllers.Templates
{

    public interface IReportController
    {
        ActionResult Index();
        ActionResult List();

        ActionResult DailyReport(int? id);
        ActionResult DailyReport(ReportItemViewModel viewModel);
        ActionResult ExportDailyReport(ReportItemViewModel viewModel);

        ActionResult MultipleReport(int? id);
        ActionResult MultipleReport(ReportItemViewModel viewModel);
        ActionResult ExportMultipleReport(ReportItemViewModel viewModel);

        ActionResult NoDateReport(int? id);
        ActionResult NoDateReport(ReportItemViewModel viewModel);
        ActionResult ExportNoDateReport(ReportItemViewModel viewModel);
    }

    [AreaAuthorization]
    public class BaseReportController : BaseContoller
    {

        public DataTable LastResultsTable
        {
            get
            {
                return (DataTable)TempData[GetSessionKey(SessionKey.LastResultTable)];
            }
            set
            {
                TempData[GetSessionKey(SessionKey.LastResultTable)] = value;
            }
        }

        #region SupportMethods

        private static DataTable CreateEmptyTable()
        {
            var r = new DataTable();
            r.Clear();
            r.Columns.Add("Report");
            return r;
        }

        private void ExportToExcel()
        {
            string attach = "attachment;filename=report.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attach);
            Response.ContentType = "application/ms-excel";
            if (LastResultsTable != null)
            {
                foreach (DataColumn dc in LastResultsTable.Columns)
                {
                    Response.Write(dc.ColumnName + "\t");
                    //sep = ";";
                }
                Response.Write(System.Environment.NewLine);
                foreach (DataRow dr in LastResultsTable.Rows)
                {
                    for (int i = 0; i < LastResultsTable.Columns.Count; i++)
                    {
                        Response.Write(dr[i].ToString() + "\t");
                    }
                    Response.Write("\n");
                }
                Response.End();
            }
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


        #endregion

        #region Methods
        public virtual ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public virtual ActionResult List()
        {
            return View();
        }

        #region Single Date Report


        public virtual ActionResult DailyReport(int? id)
        {
            var viewModel = new ReportItemViewModel();
            if (!id.HasValue || id.Value <= 0) return View(viewModel);

            viewModel.ReportId = id.Value;
            var r = CreateEmptyTable();
            viewModel.Results = r;
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [MultipleButton(Name = "action", Argument = "DailyReport")]
        public virtual ActionResult DailyReport(ReportItemViewModel viewModel)
        {


            if (viewModel.FromDate <= DateTime.MinValue)
            {
                ModelState.AddModelError("", "Invalid DateTime");
                var r = CreateEmptyTable();
                viewModel.Results = r;
                return View(viewModel);
            }

            var report = new WsReport { FromDate = viewModel.FromDate, ReportTypeId = viewModel.ReportId };
            var transactionReport = ShopService.GetReport(report, ShopId);

            viewModel.Results = Deserialize(transactionReport.ReportResults.ToString(), typeof(DataTable));

            LastResultsTable = viewModel.Results;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MultipleButton(Name = "action", Argument = "ExportDailyReport")]
        public virtual ActionResult ExportDailyReport(ReportItemViewModel viewModel)
        {

            if (LastResultsTable != null)
            {
                ExportToExcel();
            }

            return RedirectToAction("DailyReport", new { reportId = viewModel.Report.ReportTypeId });
        }

        #endregion


        #region Multiple Date Report

        
        public virtual ActionResult MultipleReport(int? id)
        {
            var viewModel = new ReportItemViewModel();
            if (!id.HasValue || id.Value <= 0) return View(viewModel);

            viewModel.ReportId = id.Value;
            var r = CreateEmptyTable();
            viewModel.Results = r;
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [MultipleButton(Name = "action", Argument = "MultipleReport")]
        public virtual ActionResult MultipleReport(ReportItemViewModel viewModel)
        {

            if (viewModel.FromDate <= DateTime.MinValue)
            {
                ModelState.AddModelError("", "Invalid DateTime");
                var r = CreateEmptyTable();
                viewModel.Results = r;
                return View(viewModel);
            }
            if (viewModel.ToDate <= DateTime.MinValue)
            {
                ModelState.AddModelError("", "Invalid DateTime");
                var r = CreateEmptyTable();
                viewModel.Results = r;
                return View(viewModel);
            }

            var totalDays = (viewModel.ToDate - viewModel.FromDate).TotalDays;

            if (totalDays > 31 || totalDays < 0)
            {
                ModelState.AddModelError("", "Cannot Select more than 1 Month");
                var r = CreateEmptyTable();
                viewModel.Results = r;
                return View(viewModel);
            }


            var report = new WsReport
            {
                FromDate = viewModel.FromDate,
                ToDate = viewModel.ToDate,
                ReportTypeId = viewModel.ReportId
            };
            var transactionReport = ShopService.GetReport(report, ShopId);

            viewModel.Results = Deserialize(transactionReport.ReportResults.ToString(), typeof(DataTable));

            LastResultsTable = viewModel.Results;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MultipleButton(Name = "action", Argument = "ExportMultipleReport")]
        public virtual ActionResult ExportMultipleReport(ReportItemViewModel viewModel)
        {

            if (LastResultsTable != null)
            {
                ExportToExcel();
            }

            return RedirectToAction("MultipleReport", new { reportId = viewModel.Report.ReportTypeId });
        }

        #endregion


        #region No Date Report

        
        public virtual ActionResult NoDateReport(int? id)
        {
            var viewModel = new ReportItemViewModel();
            if (!id.HasValue || id.Value <= 0) return View(viewModel);

            viewModel.ReportId = id.Value;
            var r = CreateEmptyTable();
            viewModel.Results = r;
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [MultipleButton(Name = "action", Argument = "NoDateReport")]
        public virtual ActionResult NoDateReport(ReportItemViewModel viewModel)
        {

            var report = new WsReport { ReportTypeId = viewModel.ReportId };
            var transactionReport = ShopService.GetReport(report, ShopId);

            viewModel.Results = Deserialize(transactionReport.ReportResults.ToString(), typeof(DataTable));

            LastResultsTable = viewModel.Results;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MultipleButton(Name = "action", Argument = "ExportNoDateReport")]
        public virtual ActionResult ExportNoDateReport(ReportItemViewModel viewModel)
        {

            if (LastResultsTable != null)
            {
                ExportToExcel();
            }

            return RedirectToAction("NoDateReport", new { reportId = viewModel.Report.ReportTypeId });
        }

        #endregion

        #endregion


    }
}