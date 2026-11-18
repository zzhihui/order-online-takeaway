using System.Collections.Generic;
using Takeaway.Service.Contract.Entities.Settings;
using Takeaway.Service.Contract.Entities.Shop;

namespace Takeway.Web.ViewModels.Admin
{
    public class DeliveryCostViewModel: ViewModelBase
    {
        public List<WsDeliveryCost> DeliveryCosts { get; set; }
        public WsDeliveryCost SelectedDeliveryCost { get; set; }
    }
}