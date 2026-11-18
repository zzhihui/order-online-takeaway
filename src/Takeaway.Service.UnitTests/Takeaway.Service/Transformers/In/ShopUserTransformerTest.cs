using Core.Common.Infrastructure.Autofac;
using Core.Test.Utilities.Fixtures;
using Core.Test.Utilities.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Takeaway.Data.Entities.Authentication;
using Takeaway.Service.Contract.Entities.Authentication;
using Takeaway.Service.Transformers;
using Takeaway.Service.Transformers.In;

namespace Takeaway.Service.UnitTests.Takeaway.Service.Transformers.In
{
    [TestClass]
    public class ShopUserTransformerTest : AutoMockFixture<ShopUserTransformer>
    {
        [TestMethod]
        public void MapperTest()
        {
            var container = new BootStrapper().BuildTransfromer();
            container.ForEach<ITransformerMapper>(t => t.Initialise());
            var transfomer = new TransformerBase();

            var source = ObjectMother.CreateAndFill<WsShopUser>();

            var act = transfomer.Transform<WsShopUser, ShopUser>(source);

            Assert.AreEqual(source.Id, act.Id);
            Assert.AreEqual(source.Password, act.Password);
            Assert.AreEqual(source.AuthToken, act.AuthToken);
            Assert.AreEqual(source.ServerUrl, act.ServerUrl);
            Assert.AreEqual(source.ShopId, act.ShopId);
            Assert.AreEqual(source.UserName, act.UserName);



        }
    }
}
