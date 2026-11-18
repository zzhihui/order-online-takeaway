using Takeaway.Data.Entities.Authentication;
using Takeaway.Data.Entities.Health;

namespace Takeaway.Data.Repositories
{
    public interface ISyncStatusRepository : IDataRepository<SyncStatus, int>
    {
        SyncStatus GetFirtRecord(int shopId);
    }
}