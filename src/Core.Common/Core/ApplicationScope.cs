using Autofac;
using Autofac.Integration.Wcf;

namespace Core.Common.Core
{
    public class ApplicationScope
    {
        public static IContainer Container { get; set; }
        public static string ServiceCredentialUserName { get; set; }
        public static string ServiceCredentialPassword { get; set; }


        public static T ResolveStatic<T>()
        {
            return Container.Resolve<T>();
        }

        public static T ResolveScope<T>()
        {
            return AutofacInstanceContext.Current.OperationLifetime.Resolve<T>();
        }

    }
}