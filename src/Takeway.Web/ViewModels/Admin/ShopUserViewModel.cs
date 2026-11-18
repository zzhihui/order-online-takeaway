using System.Collections.Generic;
using Takeaway.Service.Contract.Entities.Authentication;
using Takeaway.Service.Contract.Entities.Shop;
using Takeway.Web.ViewModels;
using Takeway.Web.ViewModels.Reports;

namespace Takeway.Web.Models.Admin
{
    public class ShopUserViewModel: ViewModelBase
    {
        public List<WsShopUser> ShopUsers { get; set; }
        public WsShopUser NewShopUser { get; set; }
    }
}