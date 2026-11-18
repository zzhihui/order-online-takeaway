using System.ComponentModel.DataAnnotations;

namespace Takeway.Web.Models
{
    public class ShopUserModel: BaseModel
    {
        [StringLength(50, MinimumLength = 6)]
        public string UserName { get; set; }

        /// <summary>
        /// Password for the login user. Note: this is only used for validation purpose
        /// </summary>
        [StringLength(50, MinimumLength = 6)]
        public string Password { get; set; }

        /// <summary>
        /// Unique Id for each takeway shop
        /// </summary>
        public int ShopId { get; set; }

        /// <summary>
        /// URL of the server
        /// </summary>
        public string ServerUrl { get; set; }

        /// <summary>
        /// Token for authentication
        /// </summary>
        public string AuthToken { get; set; }

        public int UserGroupId { get; set; }

        [StringLength(50, MinimumLength = 4)]
        public string Pin { get; set; }

    }
}