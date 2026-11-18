using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Areas.DemoChinese.Controllers
{
    public class ReportController : BaseReportController, IReportController
    {
        public ReportController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemo);
        }
    }
}