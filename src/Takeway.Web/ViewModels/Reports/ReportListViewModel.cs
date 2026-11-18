using System.Collections.Generic;
using Takeaway.Service.Contract.Entities.Reports;


namespace Takeway.Web.ViewModels.Reports
{
    public class ReportListViewModel:ViewModelBase
    {
        public List<WsReport> Reports { get; set; }

        public ReportListViewModel()
        {
            Reports= new List<WsReport>();
        }

    }
}