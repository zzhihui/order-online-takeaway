using System.Web.Mvc;

namespace Takeway.Web.Controllers.Templates
{

    public interface IErrorController
    {
        ActionResult Index();
        ActionResult Error404();
    }


    public class BaseErrorController : BaseContoller
    {
        public virtual ActionResult Index()
        {
            return View();
        }


        public virtual ActionResult Error404()
        {
            return View();
        }
    }

}