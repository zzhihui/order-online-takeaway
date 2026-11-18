using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Areas.Demo.Controllers
{
    public class OrderController : BaseOrderController, IOrderController
    {
        
        public OrderController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemo);
        }
       
    }
}