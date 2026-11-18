using System.Web.Mvc;
using Takeaway.Service.Contract.Entities.Shop;
using Takeaway.Service.Contract.Enum;
using Takeway.Web.Application.Filter;
using Takeway.Web.Models.Admin;

namespace Takeway.Web.Controllers.Templates
{
    public interface IMenuItemController
    {
        ActionResult Index();
        ActionResult List();
        ActionResult Category(int? id);
        ActionResult Categoryitem(int? id);
        ActionResult CategoryitemEdit(int? id);
        ActionResult CategoryitemEdit(MenuItemViewModel model);
        ActionResult CategoryitemDelete(int? id);
    }

    [AreaAuthorization]
    [ValidateInput(false)]
    public class BaseMenuItemController : BaseContoller
    {

        public virtual ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public virtual ActionResult List()
        {
            var viewModel = new MenuItemViewModel();
            viewModel.CategoryGroupId = (int)MenuItemCategoryGroupEnum.External;
            //Takeaway
            viewModel.MenuItemCategories = ShopService.GetAdminMenuItemCategoryByGroupId((int)MenuItemCategoryGroupEnum.External, ShopId);

            return View(viewModel);
        }

        
        public virtual ActionResult Category(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("List");

            var viewModel = new MenuItemViewModel
            {
                MenuItemCategories = ShopService.GetAdminMenuItemCategoryByGroupId(id.Value, ShopId),
                CategoryGroupId = id.Value
            };
            return View(viewModel);

        }

        public virtual ActionResult Categoryitem(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("List");

            var viewModel = new MenuItemViewModel
            {

                MenuItemsForCategories = ShopService.GetMenuItemsByCategory(id.Value, ShopId)
            };

            viewModel.MenuItemCategories = ShopService.GetAdminMenuItemCategoryByGroupId(viewModel.CategoryGroupId, ShopId);


            return View(viewModel);

        }

        public virtual ActionResult CategoryitemEdit(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("List");

            var viewModel = new MenuItemViewModel
            {
                EditMenuItem = ShopService.GetMenuItemById(id.Value, ShopId),
            };

            viewModel.MenuItemCategories = ShopService.GetAdminMenuItemCategoryByGroupId(viewModel.CategoryGroupId, ShopId);

            return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult CategoryitemEdit(MenuItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.ErrorMessage = "Please enter valid information";
                return View(model);
            }

            //Saving users
            var menuItem = ShopService.SaveMenuItem(model.EditMenuItem, ShopId);

            if (menuItem == null)
            {
                model.ErrorMessage = "Issue saving the user";
                ModelState.AddModelError("", model.ErrorMessage);
                return View(model);
            }
            else if (!string.IsNullOrEmpty(menuItem.ErrorMessage))
            {
                ModelState.AddModelError("", menuItem.ErrorMessage);
                return View(model);
            }
            else
                return RedirectToAction("Categoryitem", new { id = menuItem.MenuItemCategoryId });

        }

        public virtual ActionResult CategoryitemDelete(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("List");


            var model = new MenuItemViewModel
            {
                EditMenuItem = ShopService.GetMenuItemById(id.Value, ShopId),
            };

            var removedItem = ShopService.RemoveMenuItemById(id.Value, ShopId);

            if (removedItem == null)
            {
                model.ErrorMessage = "Issue saving the item";
                ModelState.AddModelError("", model.ErrorMessage);
                return View(model);
            }
            if (!string.IsNullOrEmpty(removedItem.ErrorMessage))
            {
                ModelState.AddModelError("", removedItem.ErrorMessage);
                return View(model);
            }

            return RedirectToAction("Categoryitem", new { id = model.EditMenuItem.MenuItemCategoryId });

        }

        public virtual ActionResult CategoryitemCreate()
        {

            var viewModel = new MenuItemViewModel
            {
                EditMenuItem = new WsMenuItem()
            };

            viewModel.EditMenuItem.FoodCategoryId = 2;


            viewModel.MenuItemCategories = ShopService.GetAdminMenuItemCategoryByGroupId(viewModel.CategoryGroupId, ShopId);

            return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult CategoryitemCreate(MenuItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.ErrorMessage = "Please enter valid information";
                return View(model);
            }

            //Saving users
            var menuItem = ShopService.SaveMenuItem(model.EditMenuItem, ShopId);

            if (menuItem == null)
            {
                model.ErrorMessage = "Issue saving the user";
                ModelState.AddModelError("", model.ErrorMessage);
                model.MenuItemCategories = ShopService.GetAdminMenuItemCategoryByGroupId(model.CategoryGroupId, ShopId);
                return View(model);
            }
            else if (!string.IsNullOrEmpty(menuItem.ErrorMessage))
            {
                ModelState.AddModelError("", menuItem.ErrorMessage);
                return View(model);
            }
            else
                return RedirectToAction("Categoryitem", new { id = menuItem.MenuItemCategoryId });

        }


        public virtual ActionResult CategoryItemSelection(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("List");

            var viewModel = new MenuItemViewModel
            {
                EditMenuItem = ShopService.GetMenuItemById(id.Value, ShopId),
                MenuSubItems = ShopService.GetAllMenuSubItems(ShopId)
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult CategoryItemSelection(MenuItemViewModel viewModel)
        {

            var act = ShopService.SaveMenuItemWithSubItem(viewModel.SelectedMenuItemId, viewModel.EditMenuItem.Id,
                ShopId);
            return RedirectToAction("CategoryItemSelection", new { id = viewModel.EditMenuItem.Id });
        }


        public virtual ActionResult RemoveSubItemItem(int? id, int? itemId)
        {

            if (!id.HasValue)
                return RedirectToAction("List");
            if (!itemId.HasValue)
                return RedirectToAction("List");

            var act = ShopService.RemoveMenuItemWithSubItem(id.Value, itemId.Value, ShopId);

            //Save Selected Item to List
            return RedirectToAction("CategoryItemSelection", new { id = itemId });
        }
    }
}