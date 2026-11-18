using System.Collections.Generic;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Data.DbContexts;
using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Repositories
{
    [Repository]
    public class DriverRepository : DataRepository<TakeawayContext, Driver, int>, IDriverRepository
    {
        public DriverRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }


        public List<Driver> GetAll(int shopId)
        {
            return GetMany(r => r.ShopId == shopId);
        }
    }
}