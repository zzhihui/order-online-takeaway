using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Takeaway.Data.DbContexts;
using Takeaway.Data.Entities;
using Takeaway.Data.Entities.Shops;
using Takeaway.Builder.Data.Builders;

namespace Takeaway.Service.IntegrationTests.DatabaseMappings
{
    [TestClass]
    public class OrderMapTest
    {
        [TestMethod]
        public void CreateCollection()
        {

            using (var db = new TakeawayContext())
            {
                var query = (from r in db.Orders
                             select r).ToList();

                Assert.IsNotNull(query);
            }
        }

       
        }

    
    
}
