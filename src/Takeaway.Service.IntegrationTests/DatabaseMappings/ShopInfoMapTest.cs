using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting; using Takeaway.Data.DbContexts;
using Takeaway.Data.Entities;

namespace Takeaway.Service.IntegrationTests.DatabaseMappings
{
    [TestClass]
    public class ShopInfoMapTest
    {
        [TestMethod]
        public void CreateCollection()
        {

            using (var db = new TakeawayContext())
            {
                var query = (from r in db.ShopInfoes
                             select r).ToList();

                Assert.IsNotNull(query);
            }
        }
    }
}
