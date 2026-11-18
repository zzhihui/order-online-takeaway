using Core.Common.Core;
using Takeaway.Data.DbContexts;
using Takeaway.Data.Entities.Health;

namespace Takeaway.Data.Repositories
{
    public class SyncStatusRepository : DataRepository<TakeawayContext, SyncStatus, int>, ISyncStatusRepository
    {
        public SyncStatusRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public SyncStatus GetFirtRecord(int shopId)
        {
            return GetOne(r => r.ShopId == shopId);
        }
    }
}