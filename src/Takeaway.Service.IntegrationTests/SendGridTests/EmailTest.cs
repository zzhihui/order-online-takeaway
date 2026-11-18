using Microsoft.VisualStudio.TestTools.UnitTesting;
using Takeaway.BusinessEngine.Utility;
using Takeaway.Data.Entities.Settings;

namespace Takeaway.Service.IntegrationTests.SendGridTests
{
    [TestClass]
    public class EmailTest
    {
        #if !DEBUG
            [Ignore] 
        #endif

        [TestMethod]
        public void SampleEmailTests()
        {

            var emailSettingType = new EmailSettingsType { SmtpEnabled = true, SmtpUserName = "ICHIBANSUSHI", SmtpPassword = "818IWB8AWCmc", SmtpServer = "smtp.sendgrid.net" };

            var result=EmailSender.SendEmail(emailSettingType, "noreply@ichiban.ie", "bingliangchan@hotmail.com", "test",
                "this is test", "this is test");

            Assert.IsTrue(result);

        }
    }
}
