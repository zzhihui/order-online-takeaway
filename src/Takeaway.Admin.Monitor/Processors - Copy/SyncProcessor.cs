using System;
using System.Collections.Specialized;
using log4net;
using Quartz;
using Quartz.Impl;
using Takeaway.Admin.Service.Core;

namespace Takeaway.Admin.Service.Processors
{
    public class SyncProcessor
    {

        private static readonly ILog Log = LogManager.GetLogger(typeof(SyncProcessor).Name);
        private readonly IScheduler _scheduler = null;

        public SyncProcessor()
        {
            // create scheduler
            try
            {
                var properties = new NameValueCollection();
                properties["quartz.plugin.triggHistory.type"] = "Quartz.Plugin.History.LoggingJobHistoryPlugin";
                properties["quartz.plugin.jobInitializer.type"] = "Quartz.Plugin.Xml.XMLSchedulingDataProcessorPlugin";
                properties["quartz.plugin.jobInitializer.fileNames"] = "quartz_jobs.xml";
                properties["quartz.plugin.jobInitializer.failOnFileNotFound"] = "true";
                // watch for file changes 1800 seconds (30 minutes)
                properties["quartz.plugin.jobInitializer.scanInterval"] = "1800";

                // Grab the Scheduler instance from the Factory 
                ISchedulerFactory sf = new StdSchedulerFactory(properties);
                _scheduler = sf.GetScheduler();
                _scheduler.JobFactory = new AutofacJobFactory();
            }
            catch (SchedulerException schedulerException)
            {
                var errMsg = string.Format("ERROR:- There has been a scheduler issue with BatchProcessor: : {0}{2}StackTrace ---{2}{1}", schedulerException.Message, schedulerException.StackTrace, Environment.NewLine);
                Log.Error(errMsg);
                Console.WriteLine(errMsg);
            }
            catch (Exception exception)
            {
                var errMsg = string.Format("ERROR:- There has been an issue with BatchProcessor: : {0}{2}StackTrace ---{2}{1}", exception.Message, exception.StackTrace, Environment.NewLine);
                Log.Error(errMsg);
                Console.WriteLine(errMsg);
            }
        }


        public void Start()
        {
            if (_scheduler == null) return;
            _scheduler.Start();

            // start host
            Log.Info("Started");
        }

        public void Stop()
        {
            if (_scheduler == null) return;
            // stop host
            _scheduler.Shutdown();
            Log.Info("Stopped");
        }
         
    }
}