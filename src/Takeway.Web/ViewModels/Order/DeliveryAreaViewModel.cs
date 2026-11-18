using System.Collections.Generic;
using Takeaway.Service.Contract.Entities.Shop;
using Takeway.Web.ViewModels.Reports;

namespace Takeway.Web.ViewModels.Order
{
    public class DeliveryAreaViewModel : ViewModelBase
    {
        public List<WsDeliveryArea> DeliveryAreas { get; set; }

    }
}