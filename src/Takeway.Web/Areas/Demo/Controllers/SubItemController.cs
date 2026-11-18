using System.Collections.Generic;
using System.Web.Mvc;
using Takeaway.Service.Contract.Entities.Shop;
using Takeway.Web.Application.Extension;
using Takeway.Web.Application.Utility;
using Takeway.Web.Controllers.Templates;
using Takeway.Web.ViewModels.Order;

namespace Takeway.Web.Areas.Demo.Controllers
{
    public class SubItemController : BaseSubItemController
    {
        public SubItemController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemo);
        }
    }
}