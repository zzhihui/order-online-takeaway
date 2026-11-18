using System.Collections.Generic;
using Takeaway.Service.Contract.Entities.Settings;
using Takeaway.Service.Contract.Entities.Shop;
using Takeway.Web.ViewModels.Reports;

namespace Takeway.Web.ViewModels.Order
{
    public class OrderViewModel: ViewModelBase
    {
        public WsShop ShopInfo { get; set; }
        public WsSetting AllowOnlineOrder { get; set; }
        public WsSetting AppAvailable { get; set; }

    }
}