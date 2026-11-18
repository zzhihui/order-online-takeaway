using Core.Common.Core;
using Takeaway.Admin.Util.DbContexts;
using Takeaway.Admin.Util.Entities.Settings;

namespace Takeaway.Admin.Util.Repositories
{
    public interface IOfflineServerSettingRepository : IDataRepository<OfflineSetting, int>
    {

    }


    public class OfflineServerSettingRepository : DataRepository<TakeawayContext, OfflineSetting, int>, IOfflineServerSettingRepository
    {
        public OfflineServerSettingRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}