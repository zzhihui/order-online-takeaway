using Core.Common.Utility;
using Core.Test.Utilities.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Common.UnitTests.Utility
{
    [TestClass]
    public class TokenEncryptorTest : AutoMockFixture<TokenEncryptor>
    {
        [TestMethod]
        public void VerifyToken()
        {

            const string token = "REPLACE_ME,IndaSoft";
            var encryptedToken = TokenEncryptor.Encrypt(token);
            var decryptedToken = TokenEncryptor.Decrypt(encryptedToken);

            Assert.AreEqual(token, decryptedToken);

        }
    }
}
