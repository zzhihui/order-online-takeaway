namespace Takeaway.Data.Entities.Shops
{
    public partial class Customer : BaseDataEntity
    {

        public string FullName { get; set; }
        public string Username { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Email { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }

        public decimal? DeliveryCharge { get; set; }

        //Password for authentication
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public int? ValidationPending { get; set; }

        public int? IsFirstPurchase { get; set; }

        //Authentication code for SMS
        public string AuthenticationCode { get; set; }

        public int? MarketingPermission { get; set; }

        public int? RewardPoints { get; set; }

    }
}
