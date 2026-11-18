using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Takeway.Web.Infrastructure.Security;
using Takeway.Web.Models;

namespace Takeway.Web.Controllers.Templates
{
    public interface IAccountController
    {
        ActionResult Login(string returnUrl);
        ActionResult Login(LoginModel model, string returnUrl);
        ActionResult LogOff();
    }


    public class BaseAccountController : BaseContoller
    {
        [AllowAnonymous]
        public virtual ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            var viewModel = new LoginModel();
            return View(viewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, ShopId, persistCookie: model.RememberMe ))
            {
                var area = System.Web.HttpContext.Current.Request.RequestContext.RouteData.DataTokens["area"];
                var areaName = "";
                if (area != null)
                    areaName = area.ToString();

                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                    model.UserName,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(30),
                    false,
                    areaName,
         FormsAuthentication.FormsCookiePath);

                // Encrypt the ticket.
                string encTicket = FormsAuthentication.Encrypt(ticket);

                // Create the cookie.
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
                
                return RedirectToLocal(returnUrl);
            }
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult LogOff()
        {
            WebSecurity.Logout();

            return RedirectToAction("Index", "Home");
        }

        public virtual ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}