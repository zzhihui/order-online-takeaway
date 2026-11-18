using Android.Content;
using Android.Util;
using Android.Views;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Droid.Views;
using Cirrious.MvvmCross.Views;
using Java.Lang;
using Takeway.Mobile.Core.ViewModels;
using Takeway.Mobile.Core.ViewModels.Client;

namespace Takeway.Mobile.AndroidClient.Views
{

    public interface IBaseView<TViewModel>
        : IMvxView
        where TViewModel : BaseViewModel
    {
       
    }


    public abstract class ViewBase<TViewModel> 
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

                    ViewModel.ShowViewModelScreen<ShopClientHomeViewModel>();
                    return true;

                case Resource.Id.MybasketMenuItem:
                    ViewModel.ShowViewModelScreen<ShopClientBasketViewModel>();
                    return true;

                case Resource.Id.SMSUsMenuItem:
                    SendSms();
                    return true;
                case Resource.Id.CallUsMenuItem:
                    CallPhone();
                    return true;

                case Resource.Id.HelpMenuItem:
                    ViewModel.ShowViewModelScreen<ShopClientHelpViewModel>();
                    return true;

                case Resource.Id.AboutMenuItem:
                    ViewModel.ShowViewModelScreen<ShopClientAboutUsViewModel>();
                    return true;


                case Resource.Id.SettingMenuItem:
                    ViewModel.ShowViewModelScreen<ShopClientSettingViewModel>();
                    return true;

            }
            return base.OnOptionsItemSelected(item);
        }

        private void SendSms()
        {
            try
            {
                var globalViewModel = Mvx.Resolve<IGlobalModel>();

                var smsUri = Android.Net.Uri.Parse(string.Format("smsto:{0}", globalViewModel.WsShopInfo.ShopMobile));
                var smsIntent = new Intent(Intent.ActionSendto, smsUri);
                smsIntent.PutExtra("sms_body", "Hi Fusion");
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

                string url = string.Format("tel:{0}", globalViewModel.WsShopInfo.ShopNumber);

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