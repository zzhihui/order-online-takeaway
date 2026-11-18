using System;
using Core.Common.Utility;
using Core.Test.Utilities.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Common.UnitTests.Utility
{
    [TestClass]
    public class EncryptionHelperTest : AutoMockFixture<EncryptionHelper>
    {
        [TestMethod]
        public void VerifyEncryption()
        {

            const string randomString = "password";

            var encryptString = Sut.EncryptXml(randomString);

            var decryptString = Sut.DecriptEncryptXml("S2JiOdtkMghKSpmN043Ukg==");

            Assert.AreEqual(randomString,decryptString);
            Assert.AreNotEqual(randomString,encryptString);



        }
    }
}
