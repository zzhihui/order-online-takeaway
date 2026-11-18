using System.Collections.Generic;
using System.Linq;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Data.DbContexts;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Repositories
{
    [Repository]
    public class ShopNewsRepository : DataRepository<TakeawayContext, ShopNews, int>, IShopNewsRepository
    {
        public ShopNewsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public List<ShopNews> GetLast5News(int shopId)
        {
            return GetMany(r=>r.ShopId==shopId).OrderByDescending(s => s.Id).Take(5).ToList();
        }
    }

}