using System;
using Android.App;
using Android.Content;
using Android.OS;
using Cirrious.MvvmCross.Binding.BindingContext;
using Takeway.Mobile.AndroidClient.Services;
using Takeway.Mobile.Core.Utilities;
using Takeway.Mobile.Core.ViewModels.Client;

namespace Takeway.Mobile.AndroidClient.Views
{
    [Activity(Label = "@string/app_page_home_title")]
    public class HomeView : ViewBase<ShopClientHomeViewModel>
    {
        protected bool IsBound = false;
        protected TakeawayServiceBinder Binder;
        private Intent _takeawayServiceIntent;
        private TakeawayReceiver _takeawayReceiver;
        private TakeawayServiceConnection _takeawayServiceConnection;


        protected override void OnViewModelSet()
        {

            ActionBar.SetHomeButtonEnabled(false);
            ActionBar.SetDisplayHomeAsUpEnabled(false);

            SetContentView(Resource.Layout.Home);

            _takeawayServiceIntent = new Intent("indasoft.TakeawayService.Client");
            _takeawayReceiver = new TakeawayReceiver();

            BindableProgress = new BindableProgress(this);
            var set = this.CreateBindingSet<HomeView, ShopClientHomeViewModel>();
            set.Bind(BindableProgress).For(p => p.Visible).To(vm => vm.IsBusy);
            set.Apply();
        }

        protected override void OnStart()
        {
            base.OnStart();

            var intentFilter = new IntentFilter(TakeawayService.SpecialOfferUpdateAction) { Priority = (int)IntentFilterPriority.HighPriority };
            RegisterReceiver(_takeawayReceiver, intentFilter);

            _takeawayServiceConnection = new TakeawayServiceConnection(this);
            BindService(_takeawayServiceIntent, _takeawayServiceConnection, Bind.AutoCreate);

            ScheduleTakeawayUpdates();


        }

        private void ScheduleTakeawayUpdates()
        {
            if (!IsAlarmSet())
            {
                var alarm = (AlarmManager)GetSystemService(Context.AlarmService);

                var pendingServiceIntent = PendingIntent.GetService(this, 0, _takeawayServiceIntent, PendingIntentFlags.CancelCurrent);
                alarm.SetRepeating(AlarmType.Rtc, 0, Constants.NotifyInterval, pendingServiceIntent);
            }
            else
            {
                Console.WriteLine(@"alarm already set");
            }
        }

        private bool IsAlarmSet()
        {
            return PendingIntent.GetBroadcast(this, 0, _takeawayServiceIntent, PendingIntentFlags.NoCreate) != null;
        }

        //public void GetShopUpdate()
        //{

        //}


        class TakeawayReceiver : BroadcastReceiver
        {
            public override void OnReceive(Context context, Android.Content.Intent intent)
            {
                //((HomeView)context).GetShopUpdate();
                InvokeAbortBroadcast();
            }
        }



        class TakeawayServiceConnection : Java.Lang.Object, IServiceConnection
        {
            private readonly HomeView _view;

            public TakeawayServiceConnection(HomeView view)
            {
                _view = view;
            }


            public void OnServiceConnected(ComponentName name, IBinder service)
            {
                var serviceBinder = service as TakeawayServiceBinder;
                if (serviceBinder == null) return;
                var binder = (TakeawayServiceBinder)service;
                _view.Binder = binder;
                _view.IsBound = true;
            }

            public void OnServiceDisconnected(ComponentName name)
            {
                _view.IsBound = false;
            }
        }
    }
}