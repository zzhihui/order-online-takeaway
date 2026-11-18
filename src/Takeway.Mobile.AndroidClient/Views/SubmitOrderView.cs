using Android.App;
using Android.OS;
using Cirrious.MvvmCross.Binding.BindingContext;
using Takeway.Mobile.Core.ViewModels.Client;

namespace Takeway.Mobile.AndroidClient.Views
{
    [Activity(Label = "@string/app_page_submitorder_title", NoHistory = true)]
    public class SubmitOrderView : ViewBase<ShopClientSubmitOrderViewModel>
    {
        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.SubmitOrder);
            BindableProgress = new BindableProgress(this);
            var set = this.CreateBindingSet<SubmitOrderView, ShopClientSubmitOrderViewModel>();
            set.Bind(BindableProgress).For(p => p.Visible).To(vm => vm.IsBusy);
            set.Apply();
        }
    }
}