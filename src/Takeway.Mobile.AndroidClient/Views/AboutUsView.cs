using Android.App;
using Android.OS;
using Cirrious.MvvmCross.Binding.BindingContext;
using Takeway.Mobile.Core.ViewModels.Client;

namespace Takeway.Mobile.AndroidClient.Views
{

     [Activity(Label = "@string/app_page_about_title")]
    public class AboutUsView : ViewBase<ShopClientAboutUsViewModel>
    {
       
         protected override void OnViewModelSet()
         {
             SetContentView(Resource.Layout.Aboutus);

             BindableProgress = new BindableProgress(this);
             var set = this.CreateBindingSet<AboutUsView, ShopClientMainViewModel>();
             set.Bind(BindableProgress).For(p => p.Visible).To(vm => vm.IsBusy);
             set.Apply();
         }



    }
}