using System.Collections.Generic;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Data.Entities.Shops;
using TakeawayContext = Takeaway.Data.DbContexts.TakeawayContext;

namespace Takeaway.Data.Repositories
{

    [Repository]
    public class CustomerRepository : DataRepository<TakeawayContext, Customer, int>, ICustomerRepository
    {
        public CustomerRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public Customer GetCustomerByPhone(string phoneNumber, int shopId)
        {
            return GetOne(r => r.Phone1 == phoneNumber && r.ShopId==shopId);
        }

        public List<Customer> GetAll(int shopId)
        {
            return GetMany(r => r.ShopId == shopId);
        }
    }
}