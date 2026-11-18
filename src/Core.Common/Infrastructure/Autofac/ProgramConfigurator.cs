using System;
using Autofac;

namespace Core.Common.Infrastructure.Autofac
{

    public interface IProgramConfigurator
    {
        void Build(Action<ContainerBuilder> build);
    }
    public class ProgramConfigurator : IProgramConfigurator
    {
        private readonly ContainerBuilder _builder;

        public ProgramConfigurator(ContainerBuilder builder)
        {
            _builder = builder;
        }

        public void Build(Action<ContainerBuilder> build)
        {
            build(_builder);
        }
    }
}