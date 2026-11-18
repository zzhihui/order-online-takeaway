using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Areas.DemoChinese.Controllers
{
    public class CategoryController : BaseCategoryController
    {
        public CategoryController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemo);
        }
    }
}