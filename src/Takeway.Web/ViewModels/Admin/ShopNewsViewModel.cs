using System.Collections.Generic;
using Takeaway.Service.Contract.Entities.Settings;
using Takeaway.Service.Contract.Entities.Shop;

namespace Takeway.Web.ViewModels.Admin
{
    public class ShopNewsViewModel: ViewModelBase
    {
        public List<WsShopNews> ShopNewses { get; set; }
        public WsShopNews SelectedShopNews { get; set; }
    }
}