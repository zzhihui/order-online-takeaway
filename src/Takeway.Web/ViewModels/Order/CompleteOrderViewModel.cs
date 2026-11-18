using Takeaway.Service.Contract.Entities.Shop;
using Takeway.Web.ViewModels.Reports;

namespace Takeway.Web.ViewModels.Order
{
    public class CompleteOrderViewModel: ViewModelBase
    {
        public string OrderId { get; set; }

        public decimal? OrderTotal { get; set; }
        public decimal? DeliveryCharge { get; set; }
        public decimal? SubTotal { get; set; }

        public bool? IsDelivery { get; set; }

    }
}