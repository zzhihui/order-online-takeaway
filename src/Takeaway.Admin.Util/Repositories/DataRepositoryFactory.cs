using Autofac;
using Core.Common.Core;

namespace Takeaway.Admin.Util.Repositories
{
    public interface IDataRepositoryFactory
    {
        R CreateInstance<R>();

    }

    public class DataRepositoryFactory : IDataRepositoryFactory
    {
        private ILifetimeScope _scope;

        public DataRepositoryFactory(IApplicationScopeInstance applicationScope)
        {
            _scope = applicationScope.Container.BeginLifetimeScope();
        }
        public R CreateInstance<R>()
        {
            return _scope.Resolve<R>();
        }


        public void Dispose()
        {
            if (_scope == null) return;

            _scope.Dispose();
            _scope = null;
        }

    }
}