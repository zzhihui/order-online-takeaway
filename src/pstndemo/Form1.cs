using System;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using callerId.Utils;
using log4net;
using Takeaway.Service.Contract.Entities.Shop;
using Takeway.Mobile.Core.Utilities;

namespace pstndemo
{
    public partial class Form1 : Form
    {
        private static readonly ILog Logger = LogManager.GetLogger("CallerId");
        private readonly ICallerIdDapperRepository _callerIdDapperRepository;

        private const int Channel1Code = 1;
        private const int Channel2Code = 2;

        public Form1()
        {
            InitializeComponent();
            _callerIdDapperRepository = new CallerIdDapperRepository();
        }

        private void AppendStatus(string ms)
        {
            if (msg.Text.Length != 0) msg.Text += "\r\n";
            msg.Text = ms;
        }


        public static string FromUnicodeByteArray(byte[] characters)
        {
            UnicodeEncoding u = new UnicodeEncoding();
            string ustring = u.GetString(characters);
            return ustring;
        }

        public static string FromASCIIByteArray(byte[] characters)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            string constructedString = encoding.GetString(characters);
            return (constructedString);
        }

        protected override void DefWndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case BriSDKLib.BRI_EVENT_MESSAGE:
                    {
                        BriSDKLib.TBriEvent_Data EventData = (BriSDKLib.TBriEvent_Data)Marshal.PtrToStructure(m.LParam, typeof(BriSDKLib.TBriEvent_Data));
                        string strValue = "";
                        switch (EventData.lEventType)
                        {
                            case BriSDKLib.BriEvent_PhoneHook:
                                {
                                    strValue = "通道" + (EventData.uChannelID + 1).ToString() + "：电话机摘机";
                                }
                                break;
                            case BriSDKLib.BriEvent_PhoneHang:
                                {
                                    strValue = "通道" + (EventData.uChannelID + 1).ToString() + "：电话机挂机";
                                }
                                break;
                            case BriSDKLib.BriEvent_CallIn:
                                {////两声响铃结束后开始呼叫转移到CC
                                    strValue = "通道" + (EventData.uChannelID + 1).ToString() + "：来电响铃";
                                }
                                break;
                            case BriSDKLib.BriEvent_GetCallID:
                                {
                                    var phoneNumber = FromASCIIByteArray(EventData.szData);
                                    var channel = (EventData.uChannelID + 1).ToString();

                                    strValue = "通道" + channel + "：接收到来电号码 " + phoneNumber;
                                    UpdatePhoneNumber(phoneNumber.TrimEnd('\0'), channel);


                                }
                                break;
                            case BriSDKLib.BriEvent_StopCallIn:
                                {
                                    strValue = "通道" + (EventData.uChannelID + 1).ToString() + "：停止呼入，产生一个未接电话 ";
                                }
                                break;
                            case BriSDKLib.BriEvent_GetDTMFChar: strValue = "通道" + (EventData.uChannelID + 1).ToString() + "：接收到按键 " + FromASCIIByteArray(EventData.szData); break;
                            case BriSDKLib.BriEvent_RemoteHang:
                                {
                                    strValue = "通道" + (EventData.uChannelID + 1).ToString() + "：远程挂机 ";
                                }
                                break;
                            case BriSDKLib.BriEvent_Busy:
                                {

                                    strValue = "通道" + (EventData.uChannelID + 1).ToString() + "：接收到忙音,线路已经断开 ";
                                }
                                break;
                            case BriSDKLib.BriEvent_DialTone: strValue = "通道" + (EventData.uChannelID + 1).ToString() + "：检测到拨号音 "; break;
                            case BriSDKLib.BriEvent_PhoneDial: strValue = "通道" + (EventData.uChannelID + 1).ToString() + "：电话机拨号 " + FromASCIIByteArray(EventData.szData); break;
                            case BriSDKLib.BriEvent_RingBack: strValue = "通道" + (EventData.uChannelID + 1).ToString() + "：拨号后接收到回铃音 "; break;
                            case BriSDKLib.BriEvent_DevErr:
                                {
                                    if (EventData.lResult == 3)
                                    {
                                        strValue = "通道" + (EventData.uChannelID + 1).ToString() + "：设备可能被移除 ";
                                    }
                                }
                                break;
                            default: break;
                        }

                        if (strValue.Length > 0)
                        {
                            AppendStatus(strValue);
                        }


                    }
                    break;
                default:
                    base.DefWndProc(ref m);
                    break;
            }
        }


        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            StopDevice();
        }


        private void StopDevice()
        {
            Logger.InfoFormat("StopDevice");
            BriSDKLib.QNV_CloseDevice(BriSDKLib.ODT_ALL, 0);
        
        }


        private void UpdatePhoneNumber(string phoneNumber, string channel)
        {
            Execute(() =>
            {
                if (string.IsNullOrEmpty(phoneNumber))
                    return;

                Logger.InfoFormat("Call From Phone:{0}", phoneNumber);

                _callerIdDapperRepository.SaveItem(new WsPhoneRecord()
                {
                    Created = DateTime.Now,
                    Phone = phoneNumber,
                    Channel = channel,
                    ShopId = Constants.ShopIdValue
                });

            });
        }


        private void Form1_Load(object sender, EventArgs e)
        {

            if (ConfigurationManager.AppSettings["AutoStart"] != "true")
                return;
            //Hide UI
            HideUI();

            //Start Device
            StartDevice();



        }



        private void HideUI()
        {
            this.Hide();
            this.ShowInTaskbar = false;
        }


        private void Execute(Action action)
        {
            try
            {

                action();
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("{0}-{1}", ex.Message, ex.StackTrace);
            }
        }



        private void StartDevice()
        {
            Logger.InfoFormat("StartDevice");

            Execute(() =>
            {

                if (BriSDKLib.QNV_OpenDevice(BriSDKLib.ODT_LBRIDGE, 0, "") <= 0 || BriSDKLib.QNV_DevInfo(0, BriSDKLib.QNV_DEVINFO_GETCHANNELS) <= 0)
                {
                    Logger.InfoFormat("Failed to start the service");
                    AppendStatus("Failed to start the service");
                    Application.Exit();
                }

                for (Int16 i = 0; i < BriSDKLib.QNV_DevInfo(-1, BriSDKLib.QNV_DEVINFO_GETCHANNELS); i++)
                {   //在windowproc处理接收到的消息
                    BriSDKLib.QNV_Event(i, BriSDKLib.QNV_EVENT_REGWND, (Int32)this.Handle, "", new StringBuilder(0), 0);
                }

                Logger.InfoFormat("Device Started Successfully");
                AppendStatus("Device Started Successfully");

            });

        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartDevice();
        }

        private void button2_Click(object sender, EventArgs e)
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
                AppendStatus(i.ToString());
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
                AppendStatus(i.ToString());
            }
            
        }
    }
}
