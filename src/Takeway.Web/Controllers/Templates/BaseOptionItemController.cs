using System.Web.Mvc;
using Core.Common.Utility.Extension;
using Takeaway.Service.Validations;
using Takeway.Web.Application.Filter;
using Takeway.Web.ViewModels.Admin;

namespace Takeway.Web.Controllers.Templates
{
    public interface IOptionItemController
    {
        ActionResult Index();
        ActionResult List();
        ActionResult Add();
        ActionResult Add(OptionItemViewModel viewModel);
        ActionResult Edit(int? id);
        ActionResult Edit(OptionItemViewModel viewModel);
        ActionResult Delete(int? id);
    }

    [AreaAuthorization]
    public class BaseOptionItemController : BaseContoller
    {
        public virtual ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public virtual ActionResult List()
        {
            var viewModel = new OptionItemViewModel { OptionItems = ShopService.GetOptionItems(ShopId) };
            return View(viewModel);
        }
        
        public virtual ActionResult Add()
        {
            var viewModel = new OptionItemViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Add(OptionItemViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.ErrorMessage = "Please enter valid information";
                return View(viewModel);
            }


            //validation
            var validator = new WsOptionItemValidator();
            var result = validator.Validate(viewModel.SelectedOptionItem);
            if (!result.IsValid)
            {
                ModelState.AddModelError("", result.ToErrorMessage());
                return View(viewModel);
            }

            //Saving users
            var user = ShopService.SaveOptionItem(viewModel.SelectedOptionItem, ShopId);

            if (user == null)
            {
                viewModel.ErrorMessage = "Issue saving...";
                ModelState.AddModelError("", viewModel.ErrorMessage);
                return View(viewModel);
            }
            if (!string.IsNullOrEmpty(user.ErrorMessage))
            {
                ModelState.AddModelError("", user.ErrorMessage);
                return View(viewModel);
            }
            return RedirectToAction("List");
        }

        public virtual ActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("List");
            var viewModel = new OptionItemViewModel { SelectedOptionItem = ShopService.GetOptionItemById(id.Value, ShopId) };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(OptionItemViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                viewModel.ErrorMessage = "Please enter valid information";
                return View(viewModel);
            }

            //validation
            var validator = new WsOptionItemValidator();
            var result = validator.Validate(viewModel.SelectedOptionItem);
            if (!result.IsValid)
            {
                ModelState.AddModelError("", result.ToErrorMessage());
                return View(viewModel);
            }

            //Saving item
            var saveItem = ShopService.SaveOptionItem(viewModel.SelectedOptionItem, ShopId);

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
            var model = new OptionItemViewModel();
            var item = ShopService.RemoveOptionItemById(id.Value, ShopId);
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