using Autofac;

namespace Core.Common.Core
{
    public interface IApplicationScopeInstance
    {
        IContainer Container { get; set; }
        T ResolveStatic<T>();
        T ResolveScope<T>();
    }

    public class ApplicationScopeInstance : IApplicationScopeInstance
    {
        public IContainer Container
        {
            get { return ApplicationScope.Container; }
            set { ApplicationScope.Container = value; }
        }

        public virtual T ResolveStatic<T>()
        {
            return ApplicationScope.ResolveStatic<T>();
        }

        public virtual T ResolveScope<T>()
        {
            return ApplicationScope.ResolveScope<T>();
        }

    }
}