using System;
using System.Configuration;
using log4net;
using Newtonsoft.Json;
using Quartz;
using Takeaway.Admin.Service.Core;
using Takeaway.Admin.Util.Services;
using Takeaway.Service.Contract.Entities.Shop;


namespace Takeaway.Admin.Service.Jobs
{
    [ProcessJob]
    [DisallowConcurrentExecution]
    public class SyncClientToCloud : IInterruptableJob, IBaseJob
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(SyncClientToCloud).Name);
        volatile bool _stop;

        private readonly IOnlineService _onlineService;
        private readonly IOfflineShopService _offlineShopService;

        public SyncClientToCloud(IOnlineService onlineService, IOfflineShopService offlineShopService)
        {
            _onlineService = onlineService;
            _offlineShopService = offlineShopService;
        }


        public void Execute(IJobExecutionContext context)
        {

            Log.Info("Start SyncClientToCloud Job ");

            _stop = false;
            if (ConfigurationManager.AppSettings["FeatureSyncClientToCloudOn"] != "true")
            {
                Log.Info("[SyncClientToCloud]Job is turned off  ");
            }
            else
            {
                var orders = _offlineShopService.GetPendingOrders();

                foreach (var order in orders)
                {

                    try
                    {

                        var orderDetail = JsonConvert.DeserializeObject<WsOrder>(order.Json);

                        var wsOrderSubmited = _onlineService.UpdateCloudOrder(orderDetail);

                        order.CloudOrderId = wsOrderSubmited.Id;
                        order.IsUploaded = true;

                        _offlineShopService.UpdateUploadedOrder(order);

                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            Log.ErrorFormat("[SyncClientToCloud] Error Uploading Order {0}-{1}{2}{3} ", ex.Message, ex.StackTrace, Environment.NewLine,
                             JsonConvert.SerializeObject(order));
                        }
                        catch
                        {
                            // ignored
                        }
                    }


                }


            }


            Log.Info("Finish SyncClientToCloud Job ");

        }

        public void Interrupt() { _stop = true; }
    }
}