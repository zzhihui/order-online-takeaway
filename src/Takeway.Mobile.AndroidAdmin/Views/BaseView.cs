using Android.Content;
using Android.Util;
using Android.Views;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Droid.Views;
using Cirrious.MvvmCross.Views;
using Java.Lang;
using Takeaway.Service.Contract.Entities.Settings;
using Takeway.Mobile.Core.Utilities;
using Takeway.Mobile.Core.ViewModels;
using Takeway.Mobile.Core.ViewModels.Admin;

namespace Takeway.Mobile.AndroidAdmin.Views
{

    public interface IBaseView<TViewModel>
    : IMvxView
    where TViewModel : BaseViewModel
    {

    }



    public abstract class BaseView<TViewModel> 
        : MvxActivity
        ,IBaseView<TViewModel>
        where TViewModel : BaseViewModel
    {

        protected BindableProgress BindableProgress;

        public new TViewModel ViewModel
        {
            get { return (TViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        protected override void OnCreate(Android.OS.Bundle bundle)
        {

            ActionBar.SetHomeButtonEnabled(true);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            base.OnCreate(bundle);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.main_menu, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    base.OnBackPressed();
                    break;

                case Resource.Id.MyHomeMenuItem:
                    return true;

                case Resource.Id.OrderMenuItem:
                    return true;
                case Resource.Id.ReportMenuItem:
                    return true;

                case Resource.Id.HelpMenuItem:
                    return true;

                case Resource.Id.AboutMenuItem:
                    ViewModel.ShowViewModelScreen<ShopAdminAboutUsViewModel>();
                    return true;
                case Resource.Id.SMSUsMenuItem:
                    SendSMS();
                    return true;
                case Resource.Id.CallUsMenuItem:
                    CallPhone();
                    return true;
                case Resource.Id.SettingMenuItem:
                    return true;

            }
            return base.OnOptionsItemSelected(item);
        }

        private void SendSMS()
        {
            try
            {
                var globalViewModel = Mvx.Resolve<IGlobalModel>();
                var configurationItem = globalViewModel.GetSettingByKey(WsSettingKey.IndaSoftSMS.ToString());
                if (configurationItem == null)
                    return;
                var smsUri = Android.Net.Uri.Parse(string.Format("smsto:{0}", configurationItem.SettingValue));
                var smsIntent = new Intent(Intent.ActionSendto, smsUri);
                smsIntent.PutExtra("sms_body", "Hi IndaSoft");
                StartActivity(smsIntent);
            }
            catch (Exception ex)
            {
                Log.Error("SendSMS", ex.Message);
            }
        }

        private void CallPhone()
        {
            try
            {
                var globalViewModel = Mvx.Resolve<IGlobalModel>();
                var configurationItem = globalViewModel.GetSettingByKey(WsSettingKey.IndaSoftPhone.ToString());
                if (configurationItem == null)
                    return;


                string url = string.Format("tel:{0}", configurationItem.SettingValue);

                var uri = Android.Net.Uri.Parse(url);
                var intent = new Intent(Intent.ActionView, uri);
                StartActivity(intent);

            }
            catch (Exception ex)
            {
                Log.Error("CallPhone", ex.Message);
            }
        }

    }


}