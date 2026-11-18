using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Takeaway.Data.DbContexts;

namespace Takeaway.Service.IntegrationTests.DatabaseMappings
{
    [TestClass]
    public class ShopUserMapTest
    {
        [TestMethod]
        public void CreateCollection()
        {

            using (var db = new TakeawayContext())
            {
                var query = (from r in db.ShopUsers
                             select r).ToList();

                Assert.IsNotNull(query);
            }
        }

        
    }
}
