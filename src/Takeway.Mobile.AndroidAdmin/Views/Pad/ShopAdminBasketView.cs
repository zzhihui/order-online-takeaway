using Android.App;
using Android.Content.PM;
using Cirrious.MvvmCross.Binding.BindingContext;
using Takeway.Mobile.Core.ViewModels.Admin;

namespace Takeway.Mobile.AndroidAdmin.Views.Pad
{

    [Activity(Label = "Your Order", ScreenOrientation = ScreenOrientation.Portrait)]
    public class ShopAdminBasketView : BasePadView<ShopAdminBasketViewModel>
    {
        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.Basket);

            BindableProgress = new BindableProgress(this);
            var set = this.CreateBindingSet<ShopAdminBasketView, ShopAdminBasketViewModel>();
            set.Bind(BindableProgress).For(p => p.Visible).To(vm => vm.IsBusy);
            set.Apply();
        }
    }
}