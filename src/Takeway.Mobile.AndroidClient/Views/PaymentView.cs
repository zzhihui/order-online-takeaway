using Android.App;
using Cirrious.MvvmCross.Binding.BindingContext;
using Takeway.Mobile.Core.ViewModels.Client;


namespace Takeway.Mobile.AndroidClient.Views
{

    [Activity(Label = "Payment")]
    public class PaymentView : ViewBase<ShopPaymentViewModel>
    {
        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.Payment);

            BindableProgress = new BindableProgress(this);
            var set = this.CreateBindingSet<PaymentView, ShopPaymentViewModel>();
            set.Bind(BindableProgress).For(p => p.Visible).To(vm => vm.IsBusy);
            set.Apply();
        }
    }
}