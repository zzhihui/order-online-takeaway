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
    public class OrderTransformerTest : AutoMockFixture<OrderTransformer>
    {
        [TestMethod]
        public void MappingTest()
        {
            var container = new BootStrapper().BuildTransfromer();
            container.ForEach<ITransformerMapper>(t => t.Initialise());
            var transfomer = new TransformerBase();

            var source = ObjectMother.CreateAndFill<WsOrder>();
            source.DeliveryType = ObjectMother.CreateAndFill<WsDeliveryOption>();
            source.OrderItems = new List<WsMenuItem> { NewMenuItem(), NewMenuItem(), NewMenuItem() };


            var act = transfomer.Transform<WsOrder, Order>(source);


            Assert.AreEqual(act.Id, source.Id);
            Assert.AreEqual(act.OrderTotal, source.OrderTotal);
            Assert.AreEqual(act.ContactName, source.ContactName);
            Assert.AreEqual(act.PhoneNumber, source.PhoneNumber);
            Assert.AreEqual(act.Address, source.Address);
            Assert.AreEqual(act.Comment, source.Comment);
            Assert.AreEqual(act.DeliveryOptionId, source.DeliveryType.Id);
            Assert.AreEqual(act.CustomerId, source.CustomerId);
            Assert.AreEqual(act.StatusId, source.StatusId);
            Assert.AreEqual(act.OrderItems.Count, source.OrderItems.Count);

        }

        private WsMenuItem NewMenuItem()
        {
            var source = ObjectMother.CreateAndFill<WsMenuItem>();
            source.SubItemSelections = new List<WsMenuSubItem>()
            {
                ObjectMother.CreateAndFill<WsMenuSubItem>(r => { r.Selections = new List<WsMenuItem>()
                {
                    ObjectMother.CreateAndFill<WsMenuItem>(),
                    ObjectMother.CreateAndFill<WsMenuItem>(),
                    ObjectMother.CreateAndFill<WsMenuItem>()

                };}),
                 ObjectMother.CreateAndFill<WsMenuSubItem>(r => { r.Selections = new List<WsMenuItem>()
                {
                    ObjectMother.CreateAndFill<WsMenuItem>(),
                    ObjectMother.CreateAndFill<WsMenuItem>(),
                    ObjectMother.CreateAndFill<WsMenuItem>(),
                    ObjectMother.CreateAndFill<WsMenuItem>()

                };}),
                 ObjectMother.CreateAndFill<WsMenuSubItem>(r => { r.Selections = new List<WsMenuItem>()
                {
                    ObjectMother.CreateAndFill<WsMenuItem>(),
                    ObjectMother.CreateAndFill<WsMenuItem>(),
                    ObjectMother.CreateAndFill<WsMenuItem>(),
                    ObjectMother.CreateAndFill<WsMenuItem>(),
                    ObjectMother.CreateAndFill<WsMenuItem>()

                };}),
            };

            return source;
        }

    }
}
