using System.Collections.Generic;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Data.Entities.Authentication;
using TakeawayContext = Takeaway.Data.DbContexts.TakeawayContext;

namespace Takeaway.Data.Repositories
{


    [Repository]
    public class ShopUserRepository : DataRepository<TakeawayContext, ShopUser, int>, IShopUserRepository
    {
        public ShopUserRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public ShopUser GetActiveShopUser(string userName, string encryptedPassword, int shopId)
        {
            return
                GetOne(
                    r =>
                        r.UserName == userName && r.Password == encryptedPassword && r.Isactive.HasValue &&
                        r.Isactive.Value
                        && r.ShopId==shopId
                        );
        }

        public ShopUser GetActiveShopUserByName(string userName, int shopId)
        {
            return
                GetOne(
                    r =>
                        r.UserName == userName && r.Isactive.HasValue &&
                        r.Isactive.Value
                        && r.ShopId==shopId
                        );
        }

        public ShopUser GetShopAdmin(string userName, string password, int shopId)
        {
            return
               GetOne(
                   r =>
                       r.UserName == userName &&
                       r.Password == password &&
                       r.UserGroupId == 1 && //Administrator
                       r.Isactive.HasValue &&
                       r.Isactive.Value
                       && r.ShopId==shopId
                       );
        }

        public ShopUser GetUserByPin(string pin, int shopId)
        {
            return
               GetOne(
                   r =>
                       r.Pin == pin &&
                       r.Isactive.HasValue &&
                       r.Isactive.Value
                       && r.ShopId==shopId
                       );
        }

        public List<ShopUser> GetAllActive(int shopId)
        {
            return GetMany(r => r.Isactive.HasValue && r.Isactive.Value && r.ShopId==shopId);
        }

        public bool CheckUserPinExists(string pin, int id, int shopId)
        {
            return GetOne(r => r.Isactive.HasValue && r.Isactive.Value && r.Pin == pin && r.Id != id && r.ShopId==shopId
            
            )!=null;
        }

        public bool CheckShopUserNameUnique(string username, int id, int shopId)
        {
            return GetOne(r => r.Isactive.HasValue && r.Isactive.Value && r.UserName == username && r.Id != id && r.ShopId==shopId) != null;
        }

        public ShopUser GetById(int userId, int shopId)
        {
            return GetOne(r => r.ShopId == shopId && r.Id == userId);
        }
    }
}