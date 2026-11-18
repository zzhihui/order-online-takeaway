using Android.App;
using Android.Content.PM;
using Cirrious.MvvmCross.Binding.BindingContext;
using Takeway.Mobile.Core.ViewModels.Admin;

namespace Takeway.Mobile.AndroidAdmin.Views.Pad
{

    [Activity(Label = "@string/app_page_homeorder_title", ScreenOrientation = ScreenOrientation.Portrait)]
    public class HomePadView : BasePadView<ShopAdminHomePadViewModel>
    {
        protected override void OnViewModelSet()
        {

            ActionBar.SetDisplayShowHomeEnabled(true);
            ActionBar.SetDisplayHomeAsUpEnabled(false);
     
            SetContentView(Resource.Layout.HomeOrderView);

            BindableProgress = new BindableProgress(this);
            var set = this.CreateBindingSet<HomePadView, ShopAdminHomePadViewModel>();
            set.Bind(BindableProgress).For(p => p.Visible).To(vm => vm.IsBusy);
            set.Apply();

       
        }
      
    }
}