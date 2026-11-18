using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Areas.Demo.Controllers
{
    public class OptionItemController : BaseOptionItemController, IOptionItemController
    {
        public OptionItemController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemo);
        }

       

    }
}