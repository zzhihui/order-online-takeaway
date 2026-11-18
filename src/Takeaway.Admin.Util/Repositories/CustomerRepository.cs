using System.Collections.Generic;
using System.Linq;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Admin.Util.DbContexts;
using Takeaway.Admin.Util.Entities.Shops;


namespace Takeaway.Admin.Util.Repositories
{


    public interface ICustomerRepository : IDataRepository<Customer, int>
    {
        Customer GetCustomerByPhone(string phoneNumber, int shopId);
        List<Customer> GetAll(int shopId);
        List<Customer> GetCustomersByPhone(string customerPhone, int shopId);
        List<Customer> GetCustomersByName(string customerName, int shopId);
        List<Customer> GetCustomersByEmail(string customerEmail, int shopId);
        List<Customer> GetCustomersByAddress(string customerAddress, int shopId);
        Customer CheckExistingCustomer(string phoneNumber, string address, int shopId);
    }



    [Repository]
    public class CustomerRepository : DataRepository<TakeawayContext, Customer, int>, ICustomerRepository
    {
        public CustomerRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public Customer GetCustomerByPhone(string phoneNumber, int shopId)
        {
            return GetMany(r => r.Phone1 == phoneNumber && r.ShopId==shopId).OrderByDescending(r=>r.Id).FirstOrDefault();
        }

        public List<Customer> GetAll(int shopId)
        {
            return GetMany(r => r.ShopId == shopId);
        }

        public List<Customer> GetCustomersByPhone(string customerPhone, int shopId)
        {
            return GetMany(r => r.Phone1 == customerPhone && r.ShopId == shopId).OrderByDescending(r => r.Id).ToList();
        }

        public List<Customer> GetCustomersByName(string customerName, int shopId)
        {
            return GetMany(r => r.FullName.Contains(customerName)  && r.ShopId == shopId).OrderByDescending(r => r.Id).ToList();
        }

        public List<Customer> GetCustomersByEmail(string customerEmail, int shopId)
        {
            return GetMany(r => r.Email.Contains(customerEmail) && r.ShopId == shopId).OrderByDescending(r => r.Id).ToList();
        }

        public List<Customer> GetCustomersByAddress(string customerAddress, int shopId)
        {
            return GetMany(r => r.Address1.Contains(customerAddress) && r.ShopId == shopId).OrderByDescending(r => r.Id).ToList();
        }

        public Customer CheckExistingCustomer(string phoneNumber, string address, int shopId)
        {
            return GetOne(r => r.Address1 == address && r.Phone1 == phoneNumber && r.ShopId == shopId);
        }
    }
}