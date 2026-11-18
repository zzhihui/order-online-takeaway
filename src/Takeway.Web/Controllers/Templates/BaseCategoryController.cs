using System.Linq;
using System.Web.Mvc;
using Takeaway.Service.Contract.Entities.Shop;
using Takeaway.Service.Contract.Enum;
using Takeway.Web.Application.Filter;
using Takeway.Web.ViewModels.Order;

namespace Takeway.Web.Controllers.Templates
{
    [AreaAuthorization]
    public class BaseCategoryController: BaseContoller
    {
        
        // GET: ShopUser
        public virtual ActionResult Index()
        {
            return RedirectToAction("List");
        }
        
        public virtual ActionResult List()
        {
            var categories =
                ShopService.GetMenuItemCategoryByGroupId((int)MenuItemCategoryGroupEnum.External, ShopId)
                    .Where(r => !r.IsInternal.HasValue || !r.IsInternal.Value)
                    .ToList();

            var viewModel = new CategoryViewModel { CategoryCategories = categories };
            return View(viewModel);
        }

        public virtual ActionResult Create()
        {
            var viewModel = new CategoryViewModel();
            viewModel.SelectedCategory = new WsMenuItemCategory();
            viewModel.SelectedCategory.WebImageUrl = @"/Content/foodCategory.png";
            viewModel.SelectedCategory.CategoryGroupId = (int)MenuItemCategoryGroupEnum.External;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.ErrorMessage = "Please enter valid information";
                return View(model);
            }

            //Saving Category
            var category = ShopService.SaveMenuCategory(model.SelectedCategory, ShopId);

            if (category == null)
            {
                model.ErrorMessage = "Issue saving category";
                ModelState.AddModelError("", model.ErrorMessage);
                return View(model);
            }
            else if (!string.IsNullOrEmpty(category.ErrorMessage))
            {
                ModelState.AddModelError("", category.ErrorMessage);
                return View(model);
            }
            else
                return RedirectToAction("List");
        }

        
        public virtual ActionResult Delete(int? id)
        {

            if (!id.HasValue)
                return RedirectToAction("List");

            var model = new CategoryViewModel();

            var category = ShopService.RemoveMenuItemCategoryById(id.Value, ShopId);
            if (category == null)
            {
                model.ErrorMessage = "Issue saving category";
                ModelState.AddModelError("", model.ErrorMessage);
                return View(model);
            }
            if (!string.IsNullOrEmpty(category.ErrorMessage))
            {
                ModelState.AddModelError("", category.ErrorMessage);
                return View(model);
            }

            return RedirectToAction("List");
        }

        
        public virtual ActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("List");
            var viewModel = new CategoryViewModel { SelectedCategory = ShopService.GetMenuItemCategoryById(id.Value, ShopId) };
            return View(viewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.ErrorMessage = "Please enter valid information";
                return View(model);
            }

            //Saving Category
            var user = ShopService.SaveMenuCategory(model.SelectedCategory, ShopId);

            if (user == null)
            {
                model.ErrorMessage = "Issue saving Category";
                ModelState.AddModelError("", model.ErrorMessage);
                return View(model);
            }
            else if (!string.IsNullOrEmpty(user.ErrorMessage))
            {
                ModelState.AddModelError("", user.ErrorMessage);
                return View(model);
            }
            else
                return RedirectToAction("List");

        }
    }
}