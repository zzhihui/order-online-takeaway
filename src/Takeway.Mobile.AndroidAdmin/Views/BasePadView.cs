using Android.Content;
using Android.Util;
using Android.Views;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Droid.Views;
using Java.Lang;
using Takeaway.Service.Contract.Entities.Settings;
using Takeway.Mobile.Core.ViewModels;
using Takeway.Mobile.Core.ViewModels.Admin;

namespace Takeway.Mobile.AndroidAdmin.Views
{

    public abstract class BasePadView<TViewModel>
        : MvxActivity
        , IBaseView<TViewModel>
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
            MenuInflater.Inflate(Resource.Menu.MainPadMenu, menu);
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
                    ViewModel.ShowViewModelScreen<ShopAdminHomePadViewModel>();
                    return true;

                case Resource.Id.OrderMenuItem:
                    ViewModel.ShowViewModelScreen<ShopAdminBasketViewModel>();
                    return true;

                case Resource.Id.AboutMenuItem:
                    ViewModel.ShowViewModelScreen<ShopAdminAboutUsViewModel>();
                    return true;


            }
            return base.OnOptionsItemSelected(item);
        }



    }
}