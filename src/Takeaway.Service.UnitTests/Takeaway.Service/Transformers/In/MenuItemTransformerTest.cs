using System.Collections.Generic;
using System.Linq;
using Autofac;
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
    public class MenuItemTransformerTest : AutoMockFixture<MenuItemTransformer>
    {
        [TestMethod]
        public void MapperTest()
        {
            var container =new BootStrapper().BuildTransfromer();
            container.ForEach<ITransformerMapper>(t => t.Initialise());
            var transfomer = new TransformerBase();

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

            var act = transfomer.Transform<WsMenuItem, MenuItem>(source);

            Assert.AreEqual(source.Id, act.Id);
            Assert.AreEqual(source.Name, act.ItemName);
            Assert.AreEqual(source.TranslatedName, act.TranslateName);
            Assert.AreEqual(source.Price, act.UnitPrice);
            Assert.AreEqual(source.Description, act.ItemDescription);
            Assert.AreEqual(source.HasSubOrder, act.HasSubOrder);
            Assert.AreEqual(source.ImageUrl, act.ItemPic);
            Assert.AreEqual(source.Isactive, act.Isactive);

            Assert.AreEqual(source.SubItemSelections.Count, act.MenuItemSubItems.Count);

            for (var i = 0; i < source.SubItemSelections.Count; i++)
            {
                var sourceSubItem = source.SubItemSelections[i];
                var destSubItem = act.MenuItemSubItems.ToList()[i];

                Assert.AreEqual(sourceSubItem.Id, destSubItem.MenuSubItem.Id);
                Assert.AreEqual(sourceSubItem.Name, destSubItem.MenuSubItem.Name);
                Assert.AreEqual(sourceSubItem.Description, destSubItem.MenuSubItem.Description);
                Assert.AreEqual(sourceSubItem.Selections.Count, destSubItem.MenuSubItem.MenuSubItemItems.Count);

                for (var j = 0; j < source.SubItemSelections[i].Selections.Count; j++)
                {

                    var sourceSubItemSelection = source.SubItemSelections[i].Selections[j];
                    var destinationSubItemSelection =
                        act.MenuItemSubItems.ToList()[i].MenuSubItem.MenuSubItemItems.ToList()[j];

                    Assert.AreEqual(sourceSubItemSelection.Id, destinationSubItemSelection.MenuItem.Id);
                    Assert.AreEqual(sourceSubItemSelection.Name, destinationSubItemSelection.MenuItem.ItemName);
                    Assert.AreEqual(sourceSubItemSelection.TranslatedName, destinationSubItemSelection.MenuItem.TranslateName);
                    Assert.AreEqual(sourceSubItemSelection.Price, destinationSubItemSelection.MenuItem.UnitPrice);
                    Assert.AreEqual(sourceSubItemSelection.Description, destinationSubItemSelection.MenuItem.ItemDescription);
                    Assert.AreEqual(sourceSubItemSelection.HasSubOrder, destinationSubItemSelection.MenuItem.HasSubOrder);
                    Assert.AreEqual(sourceSubItemSelection.ImageUrl, destinationSubItemSelection.MenuItem.ItemPic);
                    Assert.AreEqual(sourceSubItemSelection.Isactive, destinationSubItemSelection.MenuItem.Isactive);

                }

            }


        }
    }
}
