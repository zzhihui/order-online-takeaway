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
    public class MenuItemCategoryTransformerTest : AutoMockFixture<MenuItemCategoryTransformer>
    {
        [TestMethod]
        public void MapperTest()
        {
            var container = new BootStrapper().BuildTransfromer();
            container.ForEach<ITransformerMapper>(t => t.Initialise());
            var transfomer = new TransformerBase();

            var source = ObjectMother.CreateAndFill<WsMenuItemCategory>();
            var act = transfomer.Transform<WsMenuItemCategory, MenuItemCategory>(source);

            Assert.AreEqual(source.Id, act.Id);
            Assert.AreEqual(source.Name, act.Name);
            Assert.AreEqual(source.Description, act.Description);
            Assert.AreEqual(source.WebImageUrl, act.ImageUrl);
            Assert.AreEqual(source.Isactive, act.Isactive);

        }
    }
}
