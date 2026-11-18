using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Core.Common.Core
{

    public interface IUnitOfWorkContext : IDisposable
    {
        T Context<T>() where T : DbContext, new();
        List<DbContext> GetContexts();
    }

    public class UnitOfWorkContext : IUnitOfWorkContext
    {
        private readonly List<DbContext> _contexts;
        public List<DbContext> GetContexts()
        {
            return _contexts;
        }

        public UnitOfWorkContext()
        {
            _contexts = new List<DbContext>();
        }

        public T Context<T>() where T : DbContext, new()
        {
            T context = _contexts.OfType<T>().FirstOrDefault();
            if (context == null)
            {
                context = new T();
                _contexts.Add(context);
            }
            return context;
        }

        public void Dispose()
        {
            foreach (DbContext context in _contexts)
                context.Dispose();
        }

    }
}