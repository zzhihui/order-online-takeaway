namespace Takeaway.Data.Entities.SMS
{
    public class SmsRequest
    {
        public string AccountId { get; set; }
        public string AccountToken { get; set; }

        public string FromNumber { get; set; }
        public string ToNumber { get; set; }

        public string Header { get; set; }
        public string Body { get; set; }
        public string Footer { get; set; }


    }
}