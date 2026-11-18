using System;
using System.Data.Entity.Validation;
using System.Diagnostics;
using log4net;

namespace Core.Common.Core
{

    public interface IUnitOfWork : IDisposable
    {
        IUnitOfWorkContext ContextFactory { get; set; }
        void Commit();
    }

    public class UnitOfWork : IUnitOfWork
    {
        public IUnitOfWorkContext ContextFactory { get; set; }
        protected readonly ILog Logger;

        public UnitOfWork(IUnitOfWorkContext contextFactory)
        {
            ContextFactory = contextFactory;
            Logger = LogManager.GetLogger(this.GetType());
        }

        public void Commit()
        {
            foreach (var context in ContextFactory.GetContexts())
            {
                HandleExceptions(() => context.SaveChanges());
            }
        }

        public void Dispose()
        {
            if (ContextFactory != null)
                ContextFactory.Dispose();
        }

        private void HandleExceptions(Action action)
        {
            try
            {
                action();
            }
            catch (DbEntityValidationException ex)
            {

                Trace.TraceError(string.Format("UnitOfWork:[DbEntityValidationException] {0}-{1}", ex.Message, ex.StackTrace));
                foreach (var error in ex.EntityValidationErrors)
                {
                    var entry = error.Entry;

                    Trace.TraceError(string.Format("UnitOfWork:[DbEntityValidationException] DbEntityValidationException: Entity Type:{0}", entry.Entity.GetType()));
                    foreach (var detail in error.ValidationErrors)
                    {
                        var message = detail.ErrorMessage;
                        Trace.TraceError(string.Format("UnitOfWork:[DbEntityValidationException] DbEntityValidationException: ValidationErrors Message:{0}", message));
                        var property = detail.PropertyName;
                        Trace.TraceError(string.Format("UnitOfWork:[DbEntityValidationException] DbEntityValidationException: ValidationErrors property:{0}", property));
                    }
                }
                throw;
            }
        }

    }
}