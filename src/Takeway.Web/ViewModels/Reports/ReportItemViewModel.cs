using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Takeaway.Service.Contract.Entities.Reports;


namespace Takeway.Web.ViewModels.Reports
{
    public class ReportItemViewModel : ViewModelBase
    {
        public WsReport Report { get; set; }

        public int ReportId { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please select a from date")]
        public DateTime FromDate { get; set; }


        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please select a to date")]
        public DateTime ToDate { get; set; }

        public DataTable Results { get; set; }

        
    }
}