namespace Takeaway.Data.Entities.Authentication
{
    public class ShopUser : BaseDataEntity
    {
 
        public string UserName { get; set; }
        public string Password { get; set; }
        public int ShopId { get; set; }
        public string ServerUrl { get; set; }
        public string AuthToken { get; set; }

        public bool? Isactive { get; set; }

        public int UserGroupId { get; set; }

        public string Pin { get; set; }
    }
}

