using System;
using System.Collections.Generic;
using Core.Common.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Common.UnitTests.Utility
{
    [TestClass]
    public class GuidHashUtilTest
    {
        [TestMethod]
        public void GetUniqueKey_Test()
        {

            var a = GuidHashUtil.GetUniqueOrderKey();

            Assert.IsNotNull(a);

        }


        [TestMethod]
        public void GetUniqueKeyUnique_Test()
        {

            var collection = new List<string>();

            for (int i = 0; i < 1000; i++)
            {
                var item = GuidHashUtil.GetUniqueOrderKey();
                Assert.IsFalse(collection.Contains(item));
                collection.Add(item);
            }


            Assert.IsTrue(collection.Count==1000);

        }
    }
}
