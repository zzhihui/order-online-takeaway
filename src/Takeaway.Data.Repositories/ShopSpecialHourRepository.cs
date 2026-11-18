using System;
using System.Collections.Generic;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Data.DbContexts;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Repositories
{
    [Repository]
    public class ShopSpecialHourRepository : DataRepository<TakeawayContext, ShopSpecialHour, int>, IShopSpecialHourRepository
    {
        public ShopSpecialHourRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public List<ShopSpecialHour> GetValidSpecials(int shopId)
        {

            var start = DateTime.Today;
            var end = DateTime.Today.AddDays(1).AddSeconds(-1);

            return
                GetMany(
                    r =>
                        r.Isactive.HasValue && r.Isactive.Value && r.DateOfYear.HasValue &&
                        r.DateOfYear >= start && r.DateOfYear <= end
                        && r.ShopId==shopId
                        );
        }
    }

}