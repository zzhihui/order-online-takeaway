using System.Web.Mvc;
using Takeway.Web.Controllers.Templates;
using Takeway.Web.ViewModels.Admin;

namespace Takeway.Web.Areas.Demo.Controllers
{
    public class DeliveryCostController : BaseDeliveryCostController
    {
        public DeliveryCostController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemo);
        }

    }
}