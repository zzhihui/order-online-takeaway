using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Takeaway.Service.Contract.Entities.Payment;
using Takeway.Web.Content;
using Takeway.Web.ViewModels.Reports;

namespace Takeway.Web.ViewModels.Order
{
    public class PaymentViewModel : ViewModelBase
    {
        public WsPayment Payment { get; set; }
        public int SelectedMonth { get; set; }
        public int SelectedYear { get; set; }

        public decimal? OrderTotal { get; set; }
        public decimal? DeliveryCharge { get; set; }

    }
}