using System.Globalization;
using System.Threading;
using Android.App;
using Android.Content.PM;
using Android.Graphics.Drawables;
using Android.Util;
using Android.Widget;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Core;
using Cirrious.MvvmCross.Droid.Views;
using Java.Lang;
using Takeaway.Service.Services;
using Takeway.Mobile.Core.Services.RestServices;
using Takeway.Mobile.Core.Utilities;

namespace Takeway.Mobile.AndroidAdmin
{
    [Activity(
        Label = "@string/app_name"
		, MainLauncher = true
		, Icon = "@drawable/icon"
		, Theme = "@style/Theme.Splash"
		, NoHistory = true
		, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen()
            : base(Resource.Layout.SplashScreen)
        {
            
        }

        public override void InitializationComplete()
        {

            try
            {
                if (!Constants.OnlineMode) return;

                var takeawayService = Mvx.Resolve<IShopRestServiceWrapper>();
                MvxAsyncDispatcher.BeginAsync(() => takeawayService.GetShopInfo());
            }
            catch (Exception ex)
            {
                Log.Warn("SplashScreen", ex.Message);
            }
            finally
            {
                base.InitializationComplete();
            }


        }
    }
}