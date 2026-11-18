using System;
using System.Diagnostics;
using Core.Common.Core;
using FluentValidation;
using log4net;
using Takeaway.BusinessEngine.Exception;
using Takeaway.Service.Contract.Entities;
using Takeaway.Service.Exceptions;
using Core.Common.Utility.Extension;

namespace Takeaway.Service.Services
{
    public interface IService : IHandler
    {
        void Validate<T>(T request, string ruleSet) where T : WsEntity;
    }

    public class ServiceBase : IService
    {
        protected readonly IValidatorFactory ValidatorFactory;
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly ILog Logger;

        public ServiceBase(IValidatorFactory validatorFactory,
                           IUnitOfWork unitOfWork)
        {
            ValidatorFactory = validatorFactory;
            UnitOfWork = unitOfWork;
            Logger = LogManager.GetLogger(this.GetType());
        }

        public void Validate<T>(T request, string ruleSet = "") where T : WsEntity
        {
            var validator = ValidatorFactory.GetValidator(request.GetType());
            validator.ValidateAndThrow(request, ruleSet);
        }

        public TResponse Execute<TResponse>(Func<TResponse> action)
        {
            try
            {

                Logger.DebugFormat("Perform Action");
                var response = action();
                Logger.DebugFormat("Commit Unit of Work");
                UnitOfWork.Commit();
                return response;
            }
            catch (BusinessEngineException ex)
            {
                Trace.TraceError(string.Format("ServiceHandler:[BusinessEngineException] {0}-{1}", ex.Message, ex.StackTrace));
                Logger.ErrorFormat("ServiceHandler:[BusinessEngineException] {0}-{1}", ex.Message, ex.StackTrace);
                throw new ServiceException(ex);
            }
            catch (ValidationException ex)
            {
                Trace.TraceError(string.Format("ServiceHandler:[ValidationException] {0}-{1}", ex.Message, ex.StackTrace));
                Logger.ErrorFormat("ServiceHandler:[ValidationException] {0}-{1}", ex.Message, ex.StackTrace);
                throw new ServiceException(ex);
            }
            catch (TransformerException ex)
            {
                Trace.TraceError(string.Format("ServiceHandler:[TransformerException] {0}-{1}", ex.Message, ex.StackTrace));
                Logger.ErrorFormat("ServiceHandler:[TransformerException] {0}-{1}", ex.Message, ex.StackTrace);
                throw new ServiceException(ex);
            }
            catch (Exception ex)
            {
                Trace.TraceError(string.Format("ServiceHandler:[Exception] {0}-{1}", ex.Message, ex.StackTrace));
                Logger.ErrorFormat("ServiceHandler:[Exception] {0}-{1}", ex.Message, ex.StackTrace);
                throw;
            }
        }


        public TResponse Query<TResponse>(Func<TResponse> action)
        {
            try
            {

                Logger.DebugFormat("Perform Action");
                var response = action();
                return response;
            }
            catch (BusinessEngineException ex)
            {
                Trace.TraceError(string.Format("ServiceHandler:[BusinessEngineException] {0}-{1}", ex.Message, ex.StackTrace));
                Logger.ErrorFormat("ServiceHandler:[BusinessEngineException] {0}-{1}", ex.Message, ex.StackTrace);
                throw new ServiceException(ex);
            }
            catch (ValidationException ex)
            {
                Trace.TraceError(string.Format("ServiceHandler:[ValidationException] {0}-{1}", ex.Message, ex.StackTrace));
                Logger.ErrorFormat("ServiceHandler:[ValidationException] {0}-{1}", ex.Message, ex.StackTrace);
                throw new ServiceException(ex);
            }
            catch (TransformerException ex)
            {
                Trace.TraceError(string.Format("ServiceHandler:[TransformerException] {0}-{1}", ex.Message, ex.StackTrace));
                Logger.ErrorFormat("ServiceHandler:[TransformerException] {0}-{1}", ex.Message, ex.StackTrace);
                throw new ServiceException(ex);

            }
            catch (Exception ex)
            {
                Trace.TraceError(string.Format("ServiceHandler:[Exception] {0}-{1}", ex.Message, ex.StackTrace));
                Logger.ErrorFormat("ServiceHandler:[Exception] {0}-{1}", ex.Message, ex.StackTrace);
                throw;
            }
        }

    }
}