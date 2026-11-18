using Takeway.Web.Controllers.Templates;

namespace Takeway.Web.Areas.IndaSoftAdmin.Controllers
{
    public class ReportController : BaseReportController, IReportController
    {
        public ReportController()
        {
            SetUpShop(ServiceUrlKingfood, ShopIdDemoMulti);
        }
    }
}