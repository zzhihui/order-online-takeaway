using System;
using Takeaway.Service.Contract.Entities.Authentication;
using Takeway.Web.Infrastructure.Utility;

namespace Takeway.Web.Infrastructure.Security
{
    public class WebSecurity
    {
        public static WsShopUser GetUser(string username)
        {
            var shopValidator = new ShopUserValidator();
            return shopValidator.GetUserByName(username);
        }

        public static WsShopUser GetCurrentUser()
        {
            return GetUser(CurrentUserName);
        }

        public static void CreateUser(WsShopUser user)
        {
            throw new NotImplementedException();
        }

        public static void Register()
        {
            throw new NotImplementedException();
        }

        public static bool Login(string userName, string password, int shopId,bool persistCookie = false)
        {
            var shopValidator = new ShopUserValidator();
            return shopValidator.AuthenticateShopAdmin(userName, password, shopId);
           
        }

        public static bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            return WebMatrix.WebData.WebSecurity.ChangePassword(userName, oldPassword, newPassword);
        }

        public static bool ConfirmAccount(string accountConfirmationToken)
        {
            return WebMatrix.WebData.WebSecurity.ConfirmAccount(accountConfirmationToken);
        }

        public static void CreateAccount(string userName, string password, bool requireConfirmationToken = false)
        {
            WebMatrix.WebData.WebSecurity.CreateAccount(userName, password, requireConfirmationToken);
        }

        public static string CreateUserAndAccount(string userName, string password, string email, bool requireConfirmationToken = false)
        {
            return WebMatrix.WebData.WebSecurity.CreateUserAndAccount(userName, password, new { Email = email }, requireConfirmationToken);
        }

        public static int GetUserId(string userName)
        {
            return WebMatrix.WebData.WebSecurity.GetUserId(userName);
        }

        public static void Logout()
        {
            WebMatrix.WebData.WebSecurity.Logout();
        }

        public static bool IsAuthenticated { get { return WebMatrix.WebData.WebSecurity.IsAuthenticated; } }

        public static string CurrentUserName { get { return WebMatrix.WebData.WebSecurity.CurrentUserName; } }
    }
}