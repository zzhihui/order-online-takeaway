using System.Collections.Generic;
using System.Linq;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Data.DbContexts;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Repositories
{
    [Repository]
    public class ShopInfoRepository : DataRepository<TakeawayContext, ShopInfo, int>, IShopInfoRepository
    {
        public ShopInfoRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public ShopInfo GetShopInfo(int shopId)
        {
            return GetOne(r => r.ShopId == shopId && r.Isactive.HasValue && r.Isactive.Value);
        }

        public List<ShopInfo> GetAllShopsNeedTestOrder(int shopId)
        {
            return GetMany(r => r.RequireTestOrder.HasValue && r.RequireTestOrder.Value == 1 && r.Isactive.HasValue && r.Isactive.Value);
        }
    }
}