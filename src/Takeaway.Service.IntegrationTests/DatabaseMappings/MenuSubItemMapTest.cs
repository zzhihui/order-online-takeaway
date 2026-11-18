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
    public class MenuSubItemMapTest
    {
        [TestMethod]
        public void CreateCollection()
        {

            using (var db = new TakeawayContext())
            {
                var query = (from r in db.MenuSubItems
                             select r).ToList();

                Assert.IsNotNull(query);
            }
        }
       
    }
}
