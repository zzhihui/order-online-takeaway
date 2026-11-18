using System;
using System.Collections.Generic;
using System.Linq;
using Cirrious.MvvmCross.Plugins.Sqlite;

namespace Takeway.Mobile.Core.Services.DataServices
{
    public interface IOrderRepository
    {
        List<DbOrderItem> All();

        void Add(DbOrderItem collectedItem);

        DbOrderItem Get(int id);

        List<DbOrderItem> GetRecent();
    }


    public class OrderRepository : IOrderRepository
    {

        private readonly ISQLiteConnection _connection;

        public OrderRepository(ISQLiteConnectionFactory factory)
        {
            _connection = factory.Create("IndaSoftTakeaway.sql");
            _connection.CreateTable<DbOrderItem>();

        }
        public List<DbOrderItem> All()
        {
            return _connection
               .Table<DbOrderItem>()
               .OrderByDescending(r => r.Id).ToList();
        }

        public void Add(DbOrderItem collectedItem)
        {
            _connection.Insert(collectedItem);
        }

        public DbOrderItem Get(int id)
        {
            return _connection.Get<DbOrderItem>(id);
        }

        public List<DbOrderItem> GetRecent()
        {
            return _connection
               .Table<DbOrderItem>().Where(r=>r.Created>DateTime.Now.AddMonths(-1))
               .OrderByDescending(r => r.Id).ToList();
        }
    }
}