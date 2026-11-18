using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Core.Common.Core;
using Core.Common.Infrastructure.EntityFramework;
using ServiceStack.Common.Utils;
using Takeaway.Data.Exception;

namespace Takeaway.Admin.Util.Repositories
{
    public interface IDataRepository
    {

    }


    public interface IDataRepository<TEntity, in TKey> :
        IDataRepository
        where TEntity : class
        , IEntity<TKey>
    {
        //TEntity GetById(TKey id);
        TEntity GetOne(Expression<Func<TEntity, bool>> where);

        void Add(TEntity entity);
        void Remove(TEntity entity);

        //List<TEntity> GetAll();
        List<TEntity> GetMany(Expression<Func<TEntity, bool>> where);

    }



    public class DataRepository<TContext, TEntity, TKey>
        : IHandler,
        IDataRepository<TEntity, TKey>
        where TContext : DbContext, new()
        where TEntity : class, 
        IEntity<TKey>
    {

        public DataRepository(IUnitOfWork unitOfWork)
        {
            DataFactory = unitOfWork.ContextFactory;
        }


        public IUnitOfWorkContext DataFactory { get; set; }

        public IDbSet<TEntity> Entities
        {
            get { return DataContext.Set<TEntity>(); }
        }

        public TContext DataContext
        {
            get { return DataFactory.Context<TContext>(); }
        }


        public virtual List<TEntity> GetAllEntities()
        {
            return Execute(() => Entities.ToList());
        }

        public virtual TEntity GetOne(Expression<Func<TEntity, bool>> @where)
        {
            return Execute(() => Entities.Where(where).FirstOrDefault());
        }

        public virtual void Add(TEntity entity)
        {
            Execute(() => Entities.Add(entity));
        }

        public virtual void Remove(TEntity entity)
        {
            Execute(() => Entities.Remove(entity));
        }

        //public virtual List<TEntity> GetAll()
        //{
        //    return Execute(() => Entities.ToList());
        //}

        public virtual List<TEntity> GetMany(Expression<Func<TEntity, bool>> @where)
        {
            return Execute(() => Entities.Where(where).ToList());
        }

        //public virtual void Update(TEntity entity, TKey id)
        //{
        //    Execute(() =>
        //    {

        //        //var orginal=Entities.Find(id);
        //        //DataContext.Entry(orginal).CurrentValues.SetValues(entity);



        //        //var attachedEntry = DataContext.Entry(entity);
        //        //foreach (var a  in attachedEntry.CurrentValues.SetValues())
        //        //{

        //        //}
        //        //DataContext.Entry(entity).State= EntityState.Modified;

        //        // DataContext.Entry(entity).CurrentValues.SetValues(entityNew);





        //        //foreach (var p in entityNew.GetType().GetProperties())
        //        //{
        //        //    DataContext.Entry(entityNew).Property(p.Name).IsModified = true;
        //        //}

        //        //DataContext.Entry(entity).CurrentValues.SetValues(entityNew);
        //    });
        //}

        public TResponse Execute<TResponse>(Func<TResponse> action)
        {
            try
            {
                return action();
            }
            catch (DataException ex)
            {
                Trace.TraceError(string.Format("DataRepository:[DataException] {0}-{1}", ex.Message, ex.StackTrace));
                throw new DataRepositoryException(ex);
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
                Trace.TraceError(string.Format("DataRepository:[DataException] {0}-{1}", ex.Message, ex.StackTrace));
                throw new DataRepositoryException(ex);
            }
        }
    }


}