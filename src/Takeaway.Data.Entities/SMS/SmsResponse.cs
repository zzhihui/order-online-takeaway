namespace Takeaway.Data.Entities.SMS
{
    public class SmsResponse: BaseDataEntity
    {
        public bool? IsSuccess { get; set; }
        public string ErrorMessage { get; set; }


    }
}