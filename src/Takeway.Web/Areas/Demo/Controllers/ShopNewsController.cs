using System.Collections.Generic;
using System.Web.Mvc;
using Takeaway.Service.Contract.Entities.Shop;
using Takeway.Web.Controllers.Templates;
using Takeway.Web.Models.Admin;
using Takeway.Web.ViewModels.Admin;

namespace Takeway.Web.Areas.Demo.Controllers
{

   


    public class ShopNewsController : BaseShopNewsController
    {
        public ShopNewsController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemo);
        }


       
    }
}