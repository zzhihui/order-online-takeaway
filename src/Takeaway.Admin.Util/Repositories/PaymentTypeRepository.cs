using System.Collections.Generic;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Admin.Util.DbContexts;
using Takeaway.Admin.Util.Entities.Shops;

namespace Takeaway.Admin.Util.Repositories
{

    public interface IPaymentTypeRepository : IDataRepository<PaymentType, int>
    {
        List<PaymentType> GetAll();
    }

    [Repository]
    public class PaymentTypeRepository : DataRepository<TakeawayContext, PaymentType, int>, IPaymentTypeRepository
    {
        public PaymentTypeRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }


        public List<PaymentType> GetAll()
        {
            return GetAllEntities();
        }
    }
}