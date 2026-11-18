using System.Collections.Generic;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Data.DbContexts;
using Takeaway.Data.Entities.Licence;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Repositories
{
     [Repository]
    public class LicenceInfoRepository : DataRepository<TakeawayContext, LicenceInfo, int>, ILicenceInfoRepository
    {
         public LicenceInfoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
         {
         }

         public List<LicenceInfo> GetLicence(string hwid, int shopId)
         {
             return GetMany(r => r.SettingKey == hwid && r.ShopId==shopId);
         }
    }
}