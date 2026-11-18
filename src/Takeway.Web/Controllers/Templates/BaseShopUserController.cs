using System.Web.Mvc;
using Core.Common.Utility.Extension;
using Takeaway.Service.Validations;
using Takeway.Web.Application.Filter;
using Takeway.Web.Models.Admin;

namespace Takeway.Web.Controllers.Templates
{


    public interface IShopUserController
    {
        ActionResult Index();
        ActionResult List();
        ActionResult Create();
        ActionResult Create(ShopUserViewModel model);
        ActionResult Edit(int? id);
        ActionResult Edit(ShopUserViewModel model);
        ActionResult Delete(int id);
    }
    [AreaAuthorization]
    public class BaseShopUserController : BaseContoller
    {
        // GET: ShopUser
        public virtual ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public virtual ActionResult List()
        {
            var viewModel = new ShopUserViewModel { ShopUsers = ShopService.GetAllShopUsers(ShopId) };
            return View(viewModel);
        }

        public virtual ActionResult Create()
        {
            var viewModel = new ShopUserViewModel();
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(ShopUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.ErrorMessage = "Please enter valid information";
                return View(model);
            }


            //validation
            var validator = new WsShopUserValidator();
            var result = validator.Validate(model.NewShopUser);
            if (!result.IsValid)
            {
                ModelState.AddModelError("", result.ToErrorMessage());
                return View(model);
            }

            //Saving users
            var user = ShopService.SaveShopUser(model.NewShopUser, ShopId);

            if (user == null)
            {
                model.ErrorMessage = "Issue saving the user";
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


        public virtual ActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("List");
            var viewModel = new ShopUserViewModel { NewShopUser = ShopService.GetShopUserById(id.Value, ShopId) };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(ShopUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.ErrorMessage = "Please enter valid information";
                return View(model);
            }

            //validation
            var validator = new WsShopUserValidator();
            var result = validator.Validate(model.NewShopUser);
            if (!result.IsValid)
            {
                ModelState.AddModelError("", result.ToErrorMessage());
                return View(model);
            }

            //Saving users
            var user = ShopService.SaveShopUser(model.NewShopUser, ShopId);

            if (user == null)
            {
                model.ErrorMessage = "Issue saving the user";
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

        public virtual ActionResult Delete(int id)
        {
            var model = new ShopUserViewModel();

            var user = ShopService.RemoveShopUseById(id, ShopId);
            if (user == null)
            {
                model.ErrorMessage = "Issue saving the user";
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
    }
}