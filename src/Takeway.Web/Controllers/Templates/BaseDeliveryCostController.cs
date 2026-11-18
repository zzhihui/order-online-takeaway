using System.Web.Mvc;
using Takeway.Web.Application.Filter;
using Takeway.Web.ViewModels.Admin;

namespace Takeway.Web.Controllers.Templates
{

    public interface IBaseDeliveryCostController
    {
        ActionResult Index();
        ActionResult List();
        ActionResult Edit(int? id);
        ActionResult Edit(DeliveryCostViewModel viewModel);
    }

    [AreaAuthorization]
    public class BaseDeliveryCostController: BaseContoller, IBaseDeliveryCostController
    {
        public virtual ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public virtual ActionResult List()
        {
            var viewModel = new DeliveryCostViewModel { DeliveryCosts = ShopService.GetDeliveryCosts(ShopId) };
            return View(viewModel);
        }
        
        public virtual ActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("List");
            var viewModel = new DeliveryCostViewModel { SelectedDeliveryCost = ShopService.GetDeliveryCostsById(id.Value, ShopId) };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(DeliveryCostViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                viewModel.ErrorMessage = "Please enter valid information";
                return View(viewModel);
            }

            //Saving item
            var saveItem = ShopService.SaveDeliveryCost(viewModel.SelectedDeliveryCost, ShopId);

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
    }
}