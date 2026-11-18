using System.Reflection;
using Autofac;
using Core.Common.Infrastructure.Autofac;
using Takeaway.Service.Services;
using Takeaway.Service.Transformers;

namespace Takeaway.Service.UnitTests
{
    public class BootStrapper
    {
        public IContainer BuildTransfromer()
        {
            var builder = new ContainerBuilder();

            builder.FromAssemblyContaining<ITransformer>().AnnotatedWith<TransformerAttribute>().WithDefaults();

            return builder.Build();
        }
    }
}