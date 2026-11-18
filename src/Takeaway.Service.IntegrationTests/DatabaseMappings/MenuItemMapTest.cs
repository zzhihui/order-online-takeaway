using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Takeaway.Data.DbContexts;
using Takeaway.Data.Entities;
using Takeaway.Data.Entities.Shops;
using Takeaway.Builder.Data.Builders;

namespace Takeaway.Service.IntegrationTests.DatabaseMappings
{
    [TestClass]
    public class MenuItemMapTest
    {
        [TestMethod]
        public void CreateCollection()
        {

            using (var db = new TakeawayContext())
            {
                var query = (from r in db.MenuItems
                             select r).ToList();

                Assert.IsNotNull(query);
            }
        }


      
    }
}
