using System;
using System.Web.Mvc;
using Takeway.Web.Controllers.Templates;
using Takeway.Web.Models;

namespace Takeway.Web.Controllers
{
    public class HomeController : Controller
    {
     
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
        }

        public ActionResult Blank()
        {

            return View();
        }
        public ActionResult Helpclient()
        {

            return View();
        }

        public ActionResult Privacypolicy()
        {

            return View();
        }


        public ActionResult ChargeCalculator()
        {
            var viewModel = new ChargeCalculatorViewModel
            {
                TotalSale = 1000,
                NumberOfTransactions = 50
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChargeCalculator(ChargeCalculatorViewModel viewModel)
        {

            viewModel.CardCharge =
                Math.Round((decimal) (viewModel.NumberOfTransactions*0.5) + viewModel.TotalSale*(decimal) 0.025, 2,
                    MidpointRounding.ToEven);

            viewModel.TransferAmount = viewModel.TotalSale - viewModel.CardCharge;

            return View(viewModel);
        }

    }
}