using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using Core.Common.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Takeway.Mobile.Core.Services.RestServices;
using Takeway.Mobile.Core.Utilities;

namespace Takeaway.Service.ServiceTests
{
    public class ServiceTestBase : RestServiceBase
    {
        
        

        public ServiceTestBase()
        {
            
        }

        [TestInitialize]
        public void Setup()
        {
            Constants.SiteUrl = ConfigurationManager.AppSettings["ServiceUrl"];
            //Constants.ShopId = IntegerStringUtil.ConvertStringValue(ConfigurationManager.AppSettings["ShopId"]);
        

        }


        [TestCleanup]
        public void TearDown()
        {


           
        }



       

        protected T ExtractResponseObject<T>(Task<HttpResponseMessage> response)
        {
            var resultContent = response.Result.Content.ReadAsStringAsync().Result;
            var responseObject = JsonConvert.DeserializeObject<T>(resultContent);
            return responseObject;
        }
    }
}
