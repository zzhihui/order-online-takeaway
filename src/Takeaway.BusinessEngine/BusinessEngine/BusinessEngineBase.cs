using System;
using System.Diagnostics;
using Core.Common.Core;
using log4net;
using Takeaway.BusinessEngine.Exception;
using Takeaway.Data.Exception;

namespace Takeaway.BusinessEngine.BusinessEngine
{

    public interface IBusinessEngine : IHandler
    {

    }

    public interface IBusinessEngine<T> : IBusinessEngine
    {

    }

    public class BusinessEngineBase : IBusinessEngine
    {
        protected readonly ILog Logger;

        public BusinessEngineBase()
        {
            Logger = LogManager.GetLogger(this.GetType());
        }

        public TResponse Execute<TResponse>(Func<TResponse> action)
        {
            try
            {
                return action();
            }
            catch (DataQueryException ex)
            {
                Trace.TraceError(string.Format("BusinessEngine:[DataQueryException] {0}-{1}", ex.Message, ex.StackTrace));
                Logger.ErrorFormat("BusinessEngine:[DataQueryException] {0}-{1}", ex.Message, ex.StackTrace);
                throw new BusinessEngineException(ex);
            }
            catch (DataRepositoryException ex)
            {
                Trace.TraceError(string.Format("BusinessEngine:[DataRepositoryException] {0}-{1}", ex.Message, ex.StackTrace));
                Logger.ErrorFormat("BusinessEngine:[DataRepositoryException] {0}-{1}", ex.Message, ex.StackTrace);
                throw new BusinessEngineException(ex);
            }
            catch (System.Exception ex)
            {
                Trace.TraceError(string.Format("BusinessEngine:[Exception] {0}-{1}", ex.Message, ex.StackTrace));
                Logger.ErrorFormat("BusinessEngine:[Exception] {0}-{1}", ex.Message, ex.StackTrace);
                throw;
            }
        }



        public void Execute(Action action)
        {
            try
            {
                action();
            }
            catch (DataQueryException ex)
            {
                Trace.TraceError(string.Format("BusinessEngine:[DataQueryException] {0}-{1}", ex.Message, ex.StackTrace));
                throw new BusinessEngineException(ex);
            }
            catch (DataRepositoryException ex)
            {
                Trace.TraceError(string.Format("BusinessEngine:[DataRepositoryException] {0}-{1}", ex.Message, ex.StackTrace));
                throw new BusinessEngineException(ex);
            }
            catch (System.Exception ex)
            {
                Trace.TraceError(string.Format("BusinessEngine:[Exception] {0}-{1}", ex.Message, ex.StackTrace));
                throw;
            }
        }

    }
}