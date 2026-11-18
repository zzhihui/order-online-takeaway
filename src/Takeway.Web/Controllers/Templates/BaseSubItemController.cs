using System.Web.Mvc;
using Takeaway.Service.Contract.Entities.Shop;
using Takeway.Web.Application.Extension;
using Takeway.Web.Application.Filter;
using Takeway.Web.Application.Utility;
using Takeway.Web.ViewModels.Order;

namespace Takeway.Web.Controllers.Templates
{
    [AreaAuthorization]
    public class BaseSubItemController: BaseContoller
    {

        public WsMenuSubItem TempSubItemSelection
        {
            get
            {
                if (Session[GetSessionKey(SessionKey.TempSubItemSelection)] != null)
                    return (WsMenuSubItem)Session[GetSessionKey(SessionKey.TempSubItemSelection)];
                return null;
            }
            set
            {
                Session[GetSessionKey(SessionKey.TempSubItemSelection)] = value;
            }
        }

        
        // GET: ShopUser
        public virtual ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public virtual ActionResult List()
        {
            var viewModel = new MenuSubItemViewModel { MenuSubItems = ShopService.GetAllMenuSubItems(ShopId) };
            return View(viewModel);
        }
        
        public virtual ActionResult Delete(int id)
        {
            var model = new MenuSubItemViewModel();

            var user = ShopService.RemoveMenuSubItemById(id, ShopId);
            if (user == null)
            {
                model.ErrorMessage = "Issue Removing";
                ModelState.AddModelError("", model.ErrorMessage);
                return View(model);
            }
            if (!string.IsNullOrEmpty(user.ErrorMessage))
            {
                ModelState.AddModelError("", user.ErrorMessage);
                return View(model);
            }

            return RedirectToAction("List");
        }

        public virtual ActionResult Create()
        {
            var viewModel = new MenuSubItemViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(MenuSubItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.ErrorMessage = "Please enter valid information";
                return View(model);
            }

            //Saving subitem
            var subitem = ShopService.SaveMenuSubItem(model.SelectedMenuSubItem, ShopId);

            if (subitem == null)
            {
                model.ErrorMessage = "Issue saving subitem";
                ModelState.AddModelError("", model.ErrorMessage);
                return View(model);
            }
            else if (!string.IsNullOrEmpty(subitem.ErrorMessage))
            {
                ModelState.AddModelError("", subitem.ErrorMessage);
                return View(model);
            }
            else
                return RedirectToAction("List");
        }

        public virtual ActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("List");
            var viewModel = new MenuSubItemViewModel { SelectedMenuSubItem = ShopService.GetMenuSubItemsById(id.Value, ShopId) };
            return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(MenuSubItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.ErrorMessage = "Please enter valid information";
                return View(model);
            }

            //Saving subitem
            var subitem = ShopService.SaveMenuSubItem(model.SelectedMenuSubItem, ShopId);

            if (subitem == null)
            {
                model.ErrorMessage = "Issue saving subitem";
                ModelState.AddModelError("", model.ErrorMessage);
                return View(model);
            }
            else if (!string.IsNullOrEmpty(subitem.ErrorMessage))
            {
                ModelState.AddModelError("", subitem.ErrorMessage);
                return View(model);
            }
            else
                return RedirectToAction("List");

        }

        
        public virtual ActionResult AddNewSubItem(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("List");

            var viewModel = new MenuSubItemViewModel
            {
                MenuItems = ShopService.GetMenuItemsList(ShopId), //Load all items
                SelectedMenuSubItem = ShopService.GetMenuSubItemsById(id.Value, ShopId) //Load Selections for subitem
            };

            TempSubItemSelection = viewModel.SelectedMenuSubItem;


            return View(viewModel);
        }

        public virtual ActionResult RemoveSubItemItem(int? id)
        {

            if (!id.HasValue)
                return RedirectToAction("List");
            if (TempSubItemSelection == null)
                return RedirectToAction("List");

            var act = ShopService.RemoveMenuSubItemLink(TempSubItemSelection.Id, id.Value, ShopId);

            //Save Selected Item to List
            return RedirectToAction("AddNewSubItem", new { id = TempSubItemSelection.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MultipleButton(Name = "action", Argument = "AddMoreItemToSubItem")]
        public virtual ActionResult AddMoreItemToSubItem(MenuSubItemViewModel viewModel)
        {

            var act = ShopService.SaveMenuSubItemWithItem(viewModel.SelectedMenuSubItem.Id, viewModel.SelectedMenuItemId, viewModel.CostValue, ShopId);

            //Save Selected Item to List
            return RedirectToAction("AddNewSubItem", new { id = viewModel.SelectedMenuSubItem.Id });
        }


    }
}