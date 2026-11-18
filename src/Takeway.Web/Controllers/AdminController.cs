using System.Web.Mvc;
using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Controllers
{
    public class AdminController : BaseContoller
    {

        public AdminController()
        {
           
        }


        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return RedirectToAction("Blank", "Home");
            //return RedirectToAction("List");

        }


        public ActionResult List()
        {
            return View();

        }

    }
}