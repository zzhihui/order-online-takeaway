using Android.App;
using Cirrious.MvvmCross.Binding.BindingContext;
using Takeway.Mobile.Core.ViewModels.Client;

namespace Takeway.Mobile.AndroidClient.Views
{

     [Activity(Label = "Delivery Area")]
    public class DeliveryAreaView : ViewBase<ShopClientDeliveryAreaViewModel>
    {

         protected override void OnViewModelSet()
         {
             SetContentView(Resource.Layout.Help);
             BindableProgress = new BindableProgress(this);
             var set = this.CreateBindingSet<DeliveryAreaView, ShopClientDeliveryAreaViewModel>();
             set.Bind(BindableProgress).For(p => p.Visible).To(vm => vm.IsBusy);
             set.Apply();

         }
         
    }
}