//using System;
//using System.Linq;
//using Microsoft.VisualStudio.TestTools.UnitTesting; using Takeaway.Data.DbContexts;
//using Takeaway.Data.Entities;

//namespace Takeaway.Service.IntegrationTests.DatabaseMappings
//{
//    [TestClass]
//    public class ReportLabelMappingTest
//    {
//        [TestMethod]
//        public void CreateCollection()
//        {

//            using (var db = new TakeawayContext())
//            {
//                var query = (from r in db.ReportLabels
//                             select r).ToList();

//                Assert.IsNotNull(query);
//            }
//        }
//    }
//}
