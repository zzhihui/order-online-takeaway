using System.Configuration;
using log4net;
using Quartz;
using Takeaway.Admin.Service.Core;
using Takeaway.Admin.Util.Services;

namespace Takeaway.Admin.Service.Jobs
{
    [ProcessJob]
    [DisallowConcurrentExecution]
    public class SyncCustomer : IInterruptableJob, IBaseJob
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SyncCustomer).Name);
        volatile bool _stop;
        private readonly IOnlineService _onlineService;
        private readonly IOfflineShopService _offlineShopService;

        public SyncCustomer(IOnlineService onlineService, IOfflineShopService offlineShopService)
        {
            _onlineService = onlineService;
            _offlineShopService = offlineShopService;
        }

        public void Execute(IJobExecutionContext context)
        {

            Logger.Info("Start Sync Customer Job ");

            _stop = false;
            if (ConfigurationManager.AppSettings["FeatureSyncCustomerOn"] != "true")
            {
                Logger.Info("[SyncCustomer]Job is turned off  ");
            }
            else
            {
                //var lists = _onlineService.GetCustomers();
                //_offlineShopService.SyncCustomers(lists);

            }

            Logger.Info("Finish SyncCustomer Job ");

        }


        public void Interrupt() { _stop = true; }

    }
}