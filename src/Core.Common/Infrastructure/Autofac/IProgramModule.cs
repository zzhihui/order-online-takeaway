namespace Core.Common.Infrastructure.Autofac
{
    public interface IProgramModule
    {
        void Configure(IProgramConfigurator configuration);
    }
}