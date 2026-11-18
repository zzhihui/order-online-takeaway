#define TakeawayAdminPadApp

using System.Collections.Generic;
using System.Reflection;
using Android.Content;
using Cirrious.CrossCore.Platform;
using Cirrious.CrossCore.Plugins;
using Cirrious.MvvmCross.Droid.Platform;
using Cirrious.MvvmCross.ViewModels;
using Takeway.Mobile.Core.Application;
using Takeway.Mobile.Core.Converters;

namespace Takeway.Mobile.AndroidAdmin
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

        protected override IMvxApplication CreateApp()
        {
        
            #if TakeawayAdminAppOffline
            return new TakeawayAdminAppOffline();    
            #elif TakeawayAdminPadAppOffline
            return new TakeawayAdminPadAppOffline();
            #elif TakeawayAdminPadApp
            return new TakeawayAdminPadApp();
            #else
            return new TakeawayAdminApp();
            #endif
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }
       
    }
}