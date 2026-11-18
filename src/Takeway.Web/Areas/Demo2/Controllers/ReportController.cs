using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Areas.Demo2.Controllers
{
    public class ReportController : BaseReportController, IReportController
    {

        public ReportController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemo);
        }

    }
}