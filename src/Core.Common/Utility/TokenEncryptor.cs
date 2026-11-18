using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Core.Common.Utility
{
    public class TokenEncryptor
    {
        private const string PrivateKey = "REPLACE_ME";
        private const string PublicKey = "REPLACE_ME";
        private static readonly UnicodeEncoding Encoder = new UnicodeEncoding();


        public static string Decrypt(string data)
        {

            var rsa = new RSACryptoServiceProvider();
            var dataArray = data.Split(new char[] { ',' });
            var dataByte = new byte[dataArray.Length];
            for (var i = 0; i < dataArray.Length; i++)
            {
                dataByte[i] = Convert.ToByte(dataArray[i]);
            }

            rsa.FromXmlString(PrivateKey);
            var decryptedByte = rsa.Decrypt(dataByte, false);
            return Encoder.GetString(decryptedByte);

        }

        public static string Encrypt(string data)
        {

            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(PublicKey);
            var dataToEncrypt = Encoder.GetBytes(data);
            var encryptedByteArray = rsa.Encrypt(dataToEncrypt, false).ToArray();
            var length = encryptedByteArray.Count();
            var item = 0;
            var sb = new StringBuilder();
            foreach (var x in encryptedByteArray)
            {
                item++;
                sb.Append(x);

                if (item < length)
                    sb.Append(",");
            }

            return sb.ToString();

        }



    }
}