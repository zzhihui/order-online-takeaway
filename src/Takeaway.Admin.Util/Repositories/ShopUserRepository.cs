using System.Collections.Generic;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Admin.Util.DbContexts;
using Takeaway.Admin.Util.Entities.Authentication;


namespace Takeaway.Admin.Util.Repositories
{
    public interface IShopUserRepository : IDataRepository<ShopUser, int>
    {
       
    }

    [Repository]
    public class ShopUserRepository : DataRepository<TakeawayContext, ShopUser, int>, IShopUserRepository
    {
        public ShopUserRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

       
    }
}