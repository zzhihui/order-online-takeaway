using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using SendGrid;
using Takeaway.Data.Entities.Settings;

namespace Takeaway.BusinessEngine.Utility
{
    public class EmailSender
    {
        public static bool SendEmail(EmailSettingsType settings, string fromEmail, string toEmail, string subject, string body, string bodyHtml, List<string> attachments = null)
        {
            try
            {


                if (!settings.SmtpEnabled)
                    return false;

                var myMessage = new SendGridMessage();
                myMessage.AddTo(toEmail);
                if (string.IsNullOrEmpty(fromEmail))
                {
                    fromEmail = "no-reply@kingfood.ie";
                }
                myMessage.From = new MailAddress(fromEmail);
                myMessage.Subject = subject;
                myMessage.Text = body;
                myMessage.Html = bodyHtml;

                //add attachement
                if (attachments != null)
                {
                    foreach (var attachment in attachments)
                    {
                        myMessage.AddAttachment(attachment);
                    }
                }


                // Create credentials, specifying your user name and password.
                var credentials = new NetworkCredential(settings.SmtpUserName, settings.SmtpPassword);

                // Create an Web transport for sending email.
                var transportWeb = new Web(credentials);

                // Send the email.
                transportWeb.Deliver(myMessage);



            }
            catch (System.Exception ex)
            {
                Trace.TraceError(string.Format("Util:[EmailException] {0}-{1}", ex.Message, ex.StackTrace));
                return false;
            }

            return true;
        }
    }
}