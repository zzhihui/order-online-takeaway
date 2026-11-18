using System.Collections.Generic;
using Takeaway.Data.Entities.Licence;

namespace Takeaway.Data.Repositories
{
    public interface ILicenceInfoRepository : IDataRepository<LicenceInfo, int>
    {
        List<LicenceInfo> GetLicence(string hwid, int shopId);
    }
}