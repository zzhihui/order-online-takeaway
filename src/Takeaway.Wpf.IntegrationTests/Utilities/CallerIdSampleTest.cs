//using System;
//using System.Threading;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Takeaway.Admin.Util.Repositories;
//using Takeaway.Service.Contract.Entities.Shop;

//namespace Takeaway.Wpf.IntegrationTests
//{
//    [TestClass]
//    public class CallerIdSampleTest
//    {
//        [TestMethod]
//        public void RandomCreateNumbers()
//        {

//          IWsPhoneRecordRepository wsPhoneRecordRepository = new WsPhoneRecordRepository();
//          const string Channel1Code = "1";
//          const string Channel2Code = "2";

//          var phoneNumber = 879111110;
//          var phoneNumberString = string.Format("0{0}", phoneNumber);

//            for (int i = 0; i < 20; i++)
//            {
//                phoneNumber++;
//                phoneNumberString = string.Format("0{0}", phoneNumber);

//                wsPhoneRecordRepository.SaveItem(new WsPhoneRecord() { Created = DateTime.Now, Phone = phoneNumberString, Channel = Channel1Code });

//                Thread.Sleep(1*1000);

//                if (i < 10)
//                {
                
//                phoneNumber++;
//                phoneNumberString = string.Format("0{0}", phoneNumber);

//                wsPhoneRecordRepository.SaveItem(new WsPhoneRecord() { Created = DateTime.Now, Phone = phoneNumberString, Channel = Channel2Code });
//                Thread.Sleep(1 * 1000);
//                }

//            }

//        }
//    }
//}
