using System;
using Core.Test.Utilities.Fixtures;
using Core.Test.Utilities.RegexDefinedTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Ploeh.AutoFixture;
using Takeaway.Service.Contract.Entities.Shop;
using Takeaway.Service.Services;

namespace Takeaway.Service.UnitTests.Takeaway.Service.Contract
{
    [TestClass]
    public class WsShopTest : AutoMockFixture<WsShop>
    {
        [TestMethod]
        public void JsonString()
        {
            var fixture = new Fixture();
            var arrage = fixture.Create<WsShop>();
            

            var jsonstring = JsonConvert.SerializeObject(arrage);
            var revertJsonObj = JsonConvert.DeserializeObject<WsShop>(jsonstring);

            Assert.IsNotNull(revertJsonObj);

            Assert.AreEqual(arrage.Id, revertJsonObj.Id);
            Assert.AreEqual(arrage.ShopName, revertJsonObj.ShopName);




        }
    }
}
