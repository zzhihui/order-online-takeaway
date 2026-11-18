using System.Collections.Generic;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Admin.Util.DbContexts;
using Takeaway.Admin.Util.Entities.Shops;

namespace Takeaway.Admin.Util.Repositories
{
    public interface IDriverRepository : IDataRepository<Driver, int>
    {
       
    }


    [Repository]
    public class DriverRepository : DataRepository<TakeawayContext, Driver, int>, IDriverRepository
    {
        public DriverRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
       
    }
}