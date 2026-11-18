using System;
using System.Text;
using Takeaway.Data.Entities.SMS;
using Twilio;

namespace Takeaway.BusinessEngine.Utility
{
    public interface ISmsUtil
    {
        SmsResponse SendSMS(SmsRequest request);
    }


    public class SmsUtil: ISmsUtil
    {
        public SmsResponse SendSMS(SmsRequest request)
        {

            var response = new SmsResponse { IsSuccess = true };

            try
            {


                var twilio = new TwilioRestClient(request.AccountId, request.AccountToken);
                var message = twilio.SendMessage(request.FromNumber, request.ToNumber, BuildMessage(request));

                if (message.ErrorCode.HasValue)
                {
                    response.IsSuccess = false;
                    response.ErrorMessage = message.ErrorMessage;
                }

            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = ex.Message;
            }

            return response;

        }

        private string BuildMessage(SmsRequest request)
        {
            var builder = new StringBuilder();
            builder.AppendFormat("{0}{1}{2}", request.Header, request.Body, request.Footer);
            return builder.ToString();
        }
    }
}