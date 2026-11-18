using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Takeaway.Service.Contract.Entities.Shop;
using Takeway.Web.ViewModels;
using Takeway.Web.ViewModels.Reports;

namespace Takeway.Web.Models.Admin
{
    public class KillOrderViewModel : ViewModelBase
    {
        public List<WsOrder> Orders { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please select a Date")]
        public DateTime FromDate { get; set; }

        public KillOrderViewModel()
        {
            Orders= new List<WsOrder>();
            FromDate = DateTime.Today.AddDays(1);

        }

    }
}