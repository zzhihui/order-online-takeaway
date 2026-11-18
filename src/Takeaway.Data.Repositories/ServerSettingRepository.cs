using System.Collections.Generic;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Data.Entities.Settings;
using TakeawayContext = Takeaway.Data.DbContexts.TakeawayContext;

namespace Takeaway.Data.Repositories
{

    [Repository]
    public class ServerSettingRepository : DataRepository<TakeawayContext, ServerSetting, int>, IServerSettingRepository
    {
        public ServerSettingRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public List<ServerSetting> GetAllNonServer(int shopId)
        {
            return GetMany(r => (!r.IsServer.HasValue || !r.IsServer.Value) && r.ShopId==shopId);
        }

        public List<ServerSetting> GetAllSystemSetting(int shopId)
        {
            return GetMany(r => r.ShopId == shopId);
        }
    }
}