using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Twilio;

namespace Takeaway.Service.IntegrationTests.SmsTests
{
    [TestClass]
    public class SmsCommunicationTest
    {
        [TestMethod]
        public void SimpleSMSTest()
        {

            string AccountSid = "REPLACE_ME";
            string AuthToken = "REPLACE_ME";
            var twilio = new TwilioRestClient(AccountSid, AuthToken);

            var message = twilio.SendMessage("+10000000000", "+10000000000","This is a test SMS");

            Console.WriteLine(message.Sid);
        

        }
    }
}
