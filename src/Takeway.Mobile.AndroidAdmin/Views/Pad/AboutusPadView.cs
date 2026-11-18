using Android.App;
using Android.Content.PM;
using Cirrious.MvvmCross.Binding.BindingContext;
using Takeway.Mobile.Core.ViewModels.Admin;

namespace Takeway.Mobile.AndroidAdmin.Views.Pad
{
     [Activity(Label = "@string/app_page_about_title", ScreenOrientation = ScreenOrientation.Portrait)]
    public class AboutusPadView : BasePadView<ShopAdminAboutUsViewModel>
    {
         protected override void OnViewModelSet()
         {
             SetContentView(Resource.Layout.Aboutus);

             BindableProgress = new BindableProgress(this);
             var set = this.CreateBindingSet<AboutusPadView, ShopAdminAboutUsViewModel>();
             set.Bind(BindableProgress).For(p => p.Visible).To(vm => vm.IsBusy);
             set.Apply();

         }
    }
}