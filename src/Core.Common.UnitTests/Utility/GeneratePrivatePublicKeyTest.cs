using System;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Common.UnitTests.Utility
{
    [TestClass]
    public class PublicPrivateKeyGenerator
    {
        [TestMethod]
        public void GeneratePrivatePublicKey()
        {

            const int PROVIDER_RSA_FULL = 1;
            const string CONTAINER_NAME = "KeyContainer";
            var cspParams = new CspParameters(PROVIDER_RSA_FULL);
            cspParams.KeyContainerName = CONTAINER_NAME;
            cspParams.Flags = CspProviderFlags.UseMachineKeyStore;
            cspParams.ProviderName = "Microsoft Strong Cryptographic Provider";
            var rsa = new RSACryptoServiceProvider(cspParams);


            var publicPrivateKeyXML = rsa.ToXmlString(true);


            var publicOnlyKeyXML = rsa.ToXmlString(false);


            Console.WriteLine(publicPrivateKeyXML);
            Console.WriteLine(publicOnlyKeyXML);


        }
    }
}
