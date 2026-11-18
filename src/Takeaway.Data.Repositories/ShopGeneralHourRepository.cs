using System.Collections.Generic;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Data.DbContexts;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Repositories
{
    [Repository]
    public class ShopGeneralHourRepository : DataRepository<TakeawayContext, ShopGeneralHour, int>, IShopGeneralHourRepository
    {
        public ShopGeneralHourRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public ICollection<ShopGeneralHour> GetShopGeneralHours(int shopId)
        {
            return GetMany(r => r.ShopId == shopId);
        }
    }

}