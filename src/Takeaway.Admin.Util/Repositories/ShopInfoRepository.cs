using System.Transactions;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Admin.Util.DbContexts;
using Takeaway.Admin.Util.Entities.Shops;

namespace Takeaway.Admin.Util.Repositories
{

    public interface IShopInfoRepository : IDataRepository<ShopInfo, int>
    {
     
    }

    [Repository]
    public class ShopInfoRepository : DataRepository<TakeawayContext, ShopInfo, int>, IShopInfoRepository
    {
        public ShopInfoRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

     
    }
}