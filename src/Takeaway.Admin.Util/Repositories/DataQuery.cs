using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using Core.Common.Core;
using Takeaway.Data.Exception;

namespace Takeaway.Admin.Util.Repositories
{

    public interface IDataQuery : IDisposable
    {

    }


    public interface IDataQuery<out TContext, TEntity>
        : IDataQuery
        where TContext : class
        where TEntity : class
    {
        DbQuery<TEntity> Entities { get; }

        TContext Context { get; }


        TEntity GetOne(Expression<Func<TEntity, bool>> where);

        List<TEntity> GetMany(Expression<Func<TEntity, bool>> where);

    }




    public class DataQuery<TContext, TEntity> : IHandler,
         IDataQuery<TContext, TEntity>
        where TContext : DbContext, new()
        where TEntity : class
    {

        public DbQuery<TEntity> Entities
        {
            get { return Context.Set<TEntity>().AsNoTracking(); }
        }

        private TContext _context;
        public TContext Context
        {
            get { return _context ?? (_context = new TContext()); }
        }
        public TEntity GetOne(Expression<Func<TEntity, bool>> @where)
        {
            return Execute(() => Entities.FirstOrDefault(@where));
        }

        public List<TEntity> GetMany(Expression<Func<TEntity, bool>> @where)
        {
            return Execute(() => Entities.Where(@where).ToList());
        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }

        public TResponse Execute<TResponse>(Func<TResponse> action)
        {
            try
            {
                return action();
            }
            catch (DataException ex)
            {
                Trace.TraceError(string.Format("DataQuery:[DataException] {0}-{1}", ex.Message, ex.StackTrace));
                throw new DataQueryException(ex);
            }
        }

        public void Execute(Action action)
        {
            try
            {
                action();
            }
            catch (DataException ex)
            {
                Trace.TraceError(string.Format("DataQuery:[DataException] {0}-{1}", ex.Message, ex.StackTrace));
                throw new DataQueryException(ex);
            }
        }
    }
}