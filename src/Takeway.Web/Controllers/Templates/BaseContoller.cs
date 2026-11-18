using System.Configuration;
using System.Web.Mvc;
using Takeaway.Service.Services;
using Takeway.Mobile.Core.Services.RestServices;
using Takeway.Mobile.Core.Utilities;

namespace Takeway.Web.Controllers.Templates
{
    public class BaseContoller: Controller
    {
        protected IShopService ShopService;
        protected static int ShopId;

        #region ShopURL
        //protected const string ServiceUrlLocal = "http://localhost:49041";
        protected const string ServiceUrlKingfood = "https://indasoftkingfoodapi.azurewebsites.net";
        #endregion

        #region ShopIds

        protected const int ShopIdDemo = 1;
        protected const int ShopIdDemoMulti = 4;

        #endregion

        public void SetUpShop(string url, int shopId)
        {
            ShopService = new ShopRestService();
            Constants.SiteUrl = ConfigurationManager.AppSettings["ShopUrl"];
            ShopId = shopId;
        }
        
        protected string GetSessionKey(string sessionKey)
        {
            return $"{System.Web.HttpContext.Current.Request.RequestContext.RouteData.DataTokens["area"]}_{sessionKey}";
        }
    }
}