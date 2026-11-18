using System.Collections.Generic;
using Takeaway.Data.Entities.Settings;

namespace Takeaway.Data.Repositories
{
    public interface IServerSettingRepository : IDataRepository<ServerSetting, int>
    {
        List<ServerSetting> GetAllNonServer(int shopId);
        List<ServerSetting> GetAllSystemSetting(int shopId);
    }
}