using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Takeaway.Admin.Wpf.Utility;
using Takeaway.Service.Contract.Entities.Print;

namespace Takeaway.Service.IntegrationTests.PrintTests
{
    [TestClass]
#if !DEBUG
            [Ignore("Only valid for release")] 
#endif
    public class PrinterUtilTest
    {
        [TestMethod]
        public void PrintSampleReceipt()
        {
            var printUtil = new PrinterUtil();
            printUtil.PrintRequest = new Takeaway.Service.Contract.Entities.Print.WsPrintRequest()
            {

            };

        }
    }
}
