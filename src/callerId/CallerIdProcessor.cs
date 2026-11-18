using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using callerId.Telmessage;
using callerId.Utils;
using log4net;
using ServiceStack.Text;
using Takeaway.Admin.Util.Repositories;
using Takeaway.Service.Contract.Entities.Shop;
using Takeway.Mobile.Core.Utilities;

namespace callerId
{
    public class CallerIdProcessor
    {
        private static readonly ILog Logger = LogManager.GetLogger("CallerId");
        private readonly IWsPhoneRecordRepository _wsPhoneRecordRepository;
        private readonly ICallerIdDapperRepository _callerIdDapperRepository;

        private const int Channel1Code = 1;
        private const int Channel2Code = 2;


        public CallerIdProcessor()
        {
            _wsPhoneRecordRepository = new WsPhoneRecordRepository();
            _callerIdDapperRepository= new CallerIdDapperRepository();
        }



        public void ProcessCallerId()
        {
            ITelMessage tel = new RlRecoder2();
            tel.IsSw(5);

            //Run Test Mode
            if (ConfigurationManager.AppSettings["TestMode"] == "true")
                RunTestMode();
          
            while (true)
            {
                var list = tel.GetTelNum();
                int i = 0;
                foreach (var item in list)
                {
                    i++;
                    Logger.DebugFormat("Check Phone Number Channel {0}:{1}", i, item.PhoneNum);
                    if (!string.IsNullOrEmpty(item.PhoneNum))
                        UpdatePhoneNumber(item.PhoneNum, i);
                }
                Thread.Sleep(1*500);
            }

        }

        private void RunTestMode()
        {
            if (ConfigurationManager.AppSettings["UseRedisData"] == "true")
            {
                for (int i = 0; i < 5; i++)
                {
                    _wsPhoneRecordRepository.SaveItem(new WsPhoneRecord()
                    {
                        Created = DateTime.Now,
                        Phone = "081000000" + i.ToString(),
                        Channel = Channel1Code.ToString(),
                        ShopId = Constants.ShopIdValue
                    });
                }

                for (int i = 0; i < 5; i++)
                {
                    _wsPhoneRecordRepository.SaveItem(new WsPhoneRecord()
                    {
                        Created = DateTime.Now,
                        Phone = "081000000" + i.ToString(),
                        Channel = Channel2Code.ToString(),
                        ShopId = Constants.ShopIdValue
                    });
                }

            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    _callerIdDapperRepository.SaveItem(new WsPhoneRecord()
                    {
                        Created = DateTime.Now,
                        Phone = "081000000" + i.ToString(),
                        Channel = Channel1Code.ToString(),
                        ShopId = Constants.ShopIdValue
                    });
                }

                for (int i = 0; i < 5; i++)
                {
                    _callerIdDapperRepository.SaveItem(new WsPhoneRecord()
                    {
                        Created = DateTime.Now,
                        Phone = "081000000" + i.ToString(),
                        Channel = Channel2Code.ToString(),
                        ShopId = Constants.ShopIdValue
                    });
                }


            }

        }

        private void UpdatePhoneNumber(string phoneNum, int channel)
        {
            if (ConfigurationManager.AppSettings["UseRedisData"] == "true")
            {

                _wsPhoneRecordRepository.SaveItem(new WsPhoneRecord()
                {
                    Created = DateTime.Now,
                    Phone = phoneNum,
                    Channel = channel.ToString(),
                    ShopId = Constants.ShopIdValue
                });

            }
            else
            {
                _callerIdDapperRepository.SaveItem(new WsPhoneRecord()
                {
                    Created = DateTime.Now,
                    Phone = phoneNum,
                    Channel = channel.ToString(),
                    ShopId = Constants.ShopIdValue
                });



            }
        }
        
    }
}