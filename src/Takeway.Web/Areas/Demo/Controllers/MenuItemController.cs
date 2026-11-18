using System.Web.Mvc;
using System.Web.UI.WebControls;
using Takeaway.Service.Contract.Entities.Shop;
using Takeway.Web.Application.Extension;
using Takeway.Web.Controllers.Templates;
using Takeway.Web.Models.Admin;
using Takeway.Web.ViewModels.Order;

namespace Takeway.Web.Areas.Demo.Controllers
{
    public class MenuItemController : BaseMenuItemController, IMenuItemController
    {


        public MenuItemController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemo);
        }




    }
}