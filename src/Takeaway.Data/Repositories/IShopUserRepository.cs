using System.Collections.Generic;
using Takeaway.Data.Entities.Authentication;

namespace Takeaway.Data.Repositories
{
    public interface IShopUserRepository : IDataRepository<ShopUser, int>
    {
        ShopUser GetActiveShopUser(string userName, string encryptedPassword, int shopId);
        ShopUser GetActiveShopUserByName(string userName, int shopId);
        ShopUser GetShopAdmin(string userName, string password, int shopId);
        ShopUser GetUserByPin(string pin, int shopId);
        List<ShopUser> GetAllActive(int shopId);
        bool CheckUserPinExists(string pin, int userId, int shopId);
        bool CheckShopUserNameUnique(string username, int id, int shopId);
        ShopUser GetById(int userId, int shopId);
    }
}