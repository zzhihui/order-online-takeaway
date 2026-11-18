using System;
using System.Collections.Generic;
using Autofac;
using log4net;
using Quartz;
using Quartz.Spi;

namespace Takeaway.Admin.Service.Core
{
    public class AutofacJobFactory : IJobFactory
    {
        #region Private Variables

        private static readonly ILog Log = LogManager.GetLogger(typeof(AutofacJobFactory).Name);
        private readonly Dictionary<string, IContainer> _containers = null;
        private readonly Dictionary<IJob, ILifetimeScope> _jobLifeTimeScopes = null;

        #endregion Private Variables

        #region Constructor

        public AutofacJobFactory()
        {
            _containers = new Dictionary<string, IContainer>();
            _jobLifeTimeScopes = new Dictionary<IJob, ILifetimeScope>();

            // Quartz
            var builder = GetQuartzContainerBuilder();
            _containers.Add("Quartz", builder.Build());

            //// Register the Motor Product-Specific Implementations
            builder = GetSharedContainerBuilder();
            _containers.Add("App", builder.Build());
            
        }

        #endregion Constructor

        #region IJobFactory Implementation

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            try
            {
                var jobDetail = bundle.JobDetail;
                var jobType = jobDetail.JobType;
                var product = jobDetail.JobDataMap.GetString("Product");

                if (string.IsNullOrWhiteSpace(product) &&
                    jobDetail.JobType.ToString().StartsWith("Quartz"))
                    product = "Quartz";

              var scope = GetContainerForProduct(product).BeginLifetimeScope();

                var job = scope.Resolve(jobType) as IJob;

                if (job != null)
                {
                    _jobLifeTimeScopes.Add(job, scope);
                }
                else
                {
                    scope.Dispose();
                }

                return job;
            }
            catch (Exception exception)
            {
                var errMsg = string.Format("ERROR:- There has been an issue with AutofacJobFactory: : {0}{2}StackTrace ---{2}{1}", exception.Message, exception.StackTrace, Environment.NewLine);
                Log.Error(errMsg);
                Console.WriteLine(errMsg);

                throw new SchedulerException("Unable to instantiate job.", exception);
            }
        }

        public void ReturnJob(IJob job)
        {
            if (job != null && _jobLifeTimeScopes.ContainsKey(job))
            {
                _jobLifeTimeScopes[job].Dispose();
                _jobLifeTimeScopes[job] = null;
                _jobLifeTimeScopes.Remove(job);
            }
        }

        #endregion IJobFactory Implementation

        #region Build & getting containers

        private IContainer GetContainerForProduct(string productId)
        {
            if (_containers == null || _containers.Count == 0)
                throw new Exception("Containers are not configured");

            if (string.IsNullOrWhiteSpace(productId))
                throw new Exception("Product value cannot be blank");

            if (!_containers.ContainsKey(productId))
                throw new Exception(string.Format("Container for product [{0}] not found", productId));

            return _containers[productId];
        }

        private ContainerBuilder GetSharedContainerBuilder()
        {
            var builder = new ContainerBuilder();


            //// Register the Application Core
            //builder.RegisterType<UnitOfWorkInterceptor>().WithDefaults().AsSelf();
            //builder.RegisterModule(new Common.Transformers.AutofacModule());
            //builder.RegisterModule(new Common.Services.AutofacModule());

            //// Register the Batch Processor Implementations
            builder.RegisterModule(new AutofacModule());

            return builder;
        }

        private ContainerBuilder GetQuartzContainerBuilder()
        {
            var builder = new ContainerBuilder();

            // Register the Batch Processor Implementations
            builder.RegisterModule(new QuartzAutofacModule());

            return builder;
        }

        #endregion
    }
}