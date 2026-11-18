using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Areas.DemoMulti.Controllers
{
    public class CategoryController : BaseCategoryController
    {

        public CategoryController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemoMulti);
        }

    }
}