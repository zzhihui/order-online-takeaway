using Android.App;
using Android.OS;
using Cirrious.MvvmCross.Binding.BindingContext;
using Takeway.Mobile.Core.ViewModels.Client;


namespace Takeway.Mobile.AndroidClient.Views
{
    [Activity(Label = "@string/app_page_basket_title", NoHistory = true)]
    public class BasketView : ViewBase<ShopClientBasketViewModel>
    {
        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.Basket);
            BindableProgress = new BindableProgress(this);
            var set = this.CreateBindingSet<BasketView, ShopClientBasketViewModel>();
            set.Bind(BindableProgress).For(p => p.Visible).To(vm => vm.IsBusy);
            set.Apply();
        }

    }
}