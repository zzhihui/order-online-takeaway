using System.Collections.Generic;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Admin.Util.DbContexts;
using Takeaway.Admin.Util.Entities.Settings;


namespace Takeaway.Admin.Util.Repositories
{
    public interface IServerSettingRepository : IDataRepository<ServerSetting, int>
    {
    
    }
    [Repository]
    public class ServerSettingRepository : DataRepository<TakeawayContext, ServerSetting, int>, IServerSettingRepository
    {
        public ServerSettingRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

    
    }
}