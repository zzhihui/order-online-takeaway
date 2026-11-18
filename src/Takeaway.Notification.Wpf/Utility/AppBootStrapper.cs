using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Windows;
using Autofac;
using Caliburn.Micro;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Admin.Util.Services;
using Takeaway.Admin.Wpf.Validation;
using Takeaway.Admin.Wpf.ViewModels;

using Takeway.Mobile.Core.Utilities;

namespace Takeaway.Admin.Wpf.Utility
{
    public class AppBootStrapper : BootstrapperBase
    {

        private IContainer container;


        public AppBootStrapper()
        {

            Initialize();
            InitApp();
        }

        private void InitApp()
        {
            var defaultSystemLocal = ConfigurationManager.AppSettings["SystemLocale"];
            if (!string.IsNullOrEmpty(defaultSystemLocal) && SystemLocaleHelper.SupportLocale(defaultSystemLocal))
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(defaultSystemLocal);
            }
            else
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("");

            Constants.SiteUrl = ConfigurationManager.AppSettings["ShopUrl"];
            Constants.SiteWebUrl = ConfigurationManager.AppSettings["ShopSiteUrl"];


        }

        protected override void Configure()
        {
            var builder = new ContainerBuilder();

            this.Configure(builder);

            this.container = builder.Build();

            base.Configure();
        }


        protected virtual void Configure(ContainerBuilder builder)
        {

            builder.Register(context => new WindowManager()).As<IWindowManager>().SingleInstance();
            builder.Register(context => new EventAggregator()).As<IEventAggregator>().SingleInstance();
            builder.Register(context => new AppScope()).As<IAppScope>().SingleInstance();
            builder.Register(context => new AppSettings()).As<IAppSettings>().SingleInstance();
            builder.Register(context => new ValidationHelper()).As<IValidationHelper>().SingleInstance();
           
            


            var assemblies = new List<Assembly>()
            {
                typeof (IUnitOfWork).Assembly,
                typeof (IOfflineShopService).Assembly
                
            };
            builder.RegisterProgramModules(assemblies);
            builder.RegisterAssemblyTypes(typeof (IShell).Assembly)
                .AnnotatedWith<OfflineViewModelAttribute>()
                .AsImplementedInterfaces();

            builder.RegisterType<ShellViewModel>().As<IShell>();


        }

        protected override object GetInstance(Type service, string key)
        {
            return string.IsNullOrWhiteSpace(key)
                ? this.container.Resolve(service)
                : this.container.ResolveNamed(key, service);
        }


        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return this.container.Resolve(typeof(IEnumerable<>).MakeGenericType(service)) as IEnumerable<object>;
        }

        protected override void BuildUp(object instance)
        {
            this.container.InjectProperties(instance);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<IShell>();
        }


    }
}