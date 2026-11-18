using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Takeaway.Builder.Data.Builders;
using Takeaway.Data.DbContexts;
using Takeaway.Data.Entities;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Service.IntegrationTests.DatabaseMappings
{
    [TestClass]
    public class ShopGeneralHourMapTest
    {
        [TestMethod]
        public void CreateCollection()
        {

            using (var db = new TakeawayContext())
            {
                var query = (from r in db.ShopGeneralHours
                             select r).ToList();

                Assert.IsNotNull(query);
            }
        }
       
    }
}
