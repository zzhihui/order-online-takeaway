using System.Collections.Generic;
using System.Web.Mvc;
using Takeaway.Service.Contract.Entities.Shop;
using Takeway.Web.Application.Filter;
using Takeway.Web.Models.Admin;
using Takeway.Web.ViewModels.Admin;

namespace Takeway.Web.Controllers.Templates
{
    public interface IShopNewsController
    {
        ActionResult Index();
        ActionResult List();
        ActionResult Add();
        ActionResult Add(ShopNewsViewModel model);
        ActionResult Edit(int? id);
        ActionResult Edit(ShopNewsViewModel model);
        ActionResult Delete(int? id);
    }



    [AreaAuthorization]
    public class BaseShopNewsController: BaseContoller, IShopNewsController
    {
        public virtual ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public virtual ActionResult List()
        {
            var news = ShopService.GetShopNews(ShopId) ?? new List<WsShopNews>();

            var viewModel = new ShopNewsViewModel { ShopNewses = news };
            return View(viewModel);
        }
        
        public virtual ActionResult Add()
        {
            var viewModel = new ShopNewsViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Add(ShopNewsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.ErrorMessage = "Please enter valid information";
                return View(viewModel);
            }

            var item = ShopService.SaveShopNews(viewModel.SelectedShopNews, ShopId);

            if (item == null)
            {
                viewModel.ErrorMessage = "Issue saving...";
                ModelState.AddModelError("", viewModel.ErrorMessage);
                return View(viewModel);
            }
            if (!string.IsNullOrEmpty(item.ErrorMessage))
            {
                ModelState.AddModelError("", item.ErrorMessage);
                return View(viewModel);
            }
            return RedirectToAction("List");
        }

        public virtual ActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("List");
            var viewModel = new ShopNewsViewModel { SelectedShopNews = ShopService.GetShopNewsById(id.Value, ShopId) };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(ShopNewsViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                viewModel.ErrorMessage = "Please enter valid information";
                return View(viewModel);
            }

            //Saving item
            var saveItem = ShopService.SaveShopNews(viewModel.SelectedShopNews, ShopId);

            if (saveItem == null)
            {
                viewModel.ErrorMessage = "Issue saving item";
                ModelState.AddModelError("", viewModel.ErrorMessage);
                return View(viewModel);
            }
            if (!string.IsNullOrEmpty(saveItem.ErrorMessage))
            {
                ModelState.AddModelError("", saveItem.ErrorMessage);
                return View(viewModel);
            }
            return RedirectToAction("List");
        }


        
        public virtual ActionResult Delete(int? id)
        {

            if (!id.HasValue)
                return RedirectToAction("List");
            var model = new ShopNewsViewModel();
            var item = ShopService.RemoveShopNewsById(id.Value, ShopId);
            if (item == null)
            {
                model.ErrorMessage = "Issue removing item";
                ModelState.AddModelError("", model.ErrorMessage);
                return View(model);
            }
            if (!string.IsNullOrEmpty(item.ErrorMessage))
            {
                ModelState.AddModelError("", item.ErrorMessage);
                return View(model);
            }
            return RedirectToAction("List");
        }
    }
}