using System.Collections.Generic;
using System.Linq;
using Core.Common.Infrastructure.Autofac;
using Core.Test.Utilities.Fixtures;
using Core.Test.Utilities.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Takeaway.Data.Entities.Shops;
using Takeaway.Service.Contract.Entities.Shop;
using Takeaway.Service.Transformers;
using Takeaway.Service.Transformers.In;

namespace Takeaway.Service.UnitTests.Takeaway.Service.Transformers.In
{
    [TestClass]
    public class ShopTransformerTest : AutoMockFixture<ShopTransformer>
    {
        [TestMethod]
        public void MapperTest()
        {

            var container = new BootStrapper().BuildTransfromer();
            container.ForEach<ITransformerMapper>(t => t.Initialise());
            var transfomer = new TransformerBase();

            var source = ObjectMother.CreateAndFill<WsShop>();

            source.ShopNews = new List<WsShopNews>()
            {
                ObjectMother.CreateAndFill<WsShopNews>(),
                ObjectMother.CreateAndFill<WsShopNews>(),
                ObjectMother.CreateAndFill<WsShopNews>(),
                ObjectMother.CreateAndFill<WsShopNews>()
            };




            var act = transfomer.Transform<WsShop, ShopInfo>(source);

            Assert.AreEqual(source.Id, act.Id);
            Assert.AreEqual(source.ShopName, act.Name);
            Assert.AreEqual(source.MinOrder, act.ShopMinOrder);
            Assert.AreEqual(source.ShopNumber, act.ShopNumber);
            Assert.AreEqual(source.ShopUrl, act.ShopWeb);
            Assert.AreEqual(source.ShopMobile, act.ShopMobile);
            Assert.AreEqual(source.ShopBackGroundImagePath, act.ShopImage);
            Assert.AreEqual(source.ShopAddress1, act.Address1);
            Assert.AreEqual(source.ShopAddress2, act.Address2);
            Assert.AreEqual(source.ShopSpecialOffer, act.SpecialOffer);
            Assert.AreEqual(source.ShopMobile, act.ShopMobile);
            Assert.AreEqual(source.ShopOpenHours, act.ShopOpenHours);

            for (var i = 0; i < source.ShopNews.Count; i++)
            {
                Assert.AreEqual(source.ShopNews[i].Id, act.ShopNews.ToList()[i].Id);
                Assert.AreEqual(source.ShopNews[i].News, act.ShopNews.ToList()[i].News);
            }


        }
    }
}


