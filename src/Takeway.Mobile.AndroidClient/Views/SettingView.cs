using Android.App;
using Android.OS;
using Cirrious.MvvmCross.Binding.BindingContext;
using Takeway.Mobile.Core.ViewModels.Client;

namespace Takeway.Mobile.AndroidClient.Views
{
    [Activity(Label = "@string/app_page_setting_title", NoHistory = true)]
    public class SettingView : ViewBase<ShopClientSettingViewModel>
    {
        protected override void OnViewModelSet()
        {
     
            SetContentView(Resource.Layout.Setting);
            BindableProgress = new BindableProgress(this);
            var set = this.CreateBindingSet<SettingView, ShopClientSettingViewModel>();
            set.Bind(BindableProgress).For(p => p.Visible).To(vm => vm.IsBusy);
            set.Apply();
        }
    }
}