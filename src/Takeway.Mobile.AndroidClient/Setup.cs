#define TakeawayClientApp

using System.Collections.Generic;
using System.Reflection;
using Android.Content;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.CrossCore.Plugins;
using Cirrious.MvvmCross.Droid.Platform;
using Cirrious.MvvmCross.ViewModels;
using Takeway.Mobile.AndroidClient.Error;
using Takeway.Mobile.Core.Application;
using Takeway.Mobile.Core.Converters;
using Takeway.Mobile.Core.Error;

namespace Takeway.Mobile.AndroidClient
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext)
            : base(applicationContext)
        {
        }

        protected override List<Assembly> ValueConverterAssemblies
        {
            get
            {
                var toReturn = base.ValueConverterAssemblies;
                toReturn.Add(typeof(DateStringConverter).Assembly);
                return toReturn;
            }
        }


        public override void LoadPlugins(IMvxPluginManager pluginManager)
        {
            pluginManager.EnsurePluginLoaded<Cirrious.MvvmCross.Plugins.Visibility.PluginLoader>();
            pluginManager.EnsurePluginLoaded<Cirrious.MvvmCross.Plugins.Email.PluginLoader>();
            pluginManager.EnsurePluginLoaded<Cirrious.MvvmCross.Plugins.DownloadCache.PluginLoader>();
            pluginManager.EnsurePluginLoaded<Cirrious.MvvmCross.Plugins.File.PluginLoader>();
            pluginManager.EnsurePluginLoaded<Cirrious.MvvmCross.Plugins.Sqlite.PluginLoader>();
            pluginManager.EnsurePluginLoaded<Cirrious.MvvmCross.Plugins.WebBrowser.PluginLoader>();
            base.LoadPlugins(pluginManager);
        }

        protected override void InitializeFirstChance()
        {
         
            var errorHub = new ErrorApplicationObject();
            Mvx.RegisterSingleton<IErrorReporter>(errorHub);
            Mvx.RegisterSingleton<IErrorSource>(errorHub);

        }


        protected override IMvxApplication CreateApp()
        {
            #if TakeawayClientAppOffline
            return new TakeawayClientAppOffline();
            #else
            return new TakeawayClientApp();
            #endif
        }

        protected override void InitializeLastChance()
        {
            // create a new error displayer - it will hook itself into the framework
            var errorDisplayer = new ErrorDisplayer(ApplicationContext);

            base.InitializeLastChance();
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }
       
    }
}