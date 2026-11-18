using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Takeway.Mobile.Core.Services.RestServices;
using Takeway.Mobile.Core.Utilities;

namespace Takeaway.Service.ServiceTests.Api
{
    [TestClass]
    public class WsShopInfoTest: ServiceTestBase
    {
        [TestMethod]
        public void GetShopInfo()
        {
            var client = new ShopRestService();
            var result = client.GetShopInfo(Constants.ShopId);
            Assert.IsNotNull(result);

        }
    }
}
