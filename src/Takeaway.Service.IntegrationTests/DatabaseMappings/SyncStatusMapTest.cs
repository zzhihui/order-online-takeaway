using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Takeaway.Data.DbContexts;

namespace Takeaway.Service.IntegrationTests.DatabaseMappings
{
    [TestClass]
    public class SyncStatusMapTest
    {
        [TestMethod]
        public void CreateCollection()
        {

            using (var db = new TakeawayContext())
            {
                var query = (from r in db.SyncStatuses
                             select r).ToList();

                Assert.IsNotNull(query);
            }
        }

     
    }
}
