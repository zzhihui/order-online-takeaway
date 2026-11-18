using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Areas.Demo2.Controllers
{
    public class CategoryController : BaseCategoryController
    {

        public CategoryController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemo);
        }

    }
}