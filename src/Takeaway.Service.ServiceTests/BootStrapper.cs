//using System.Collections.Generic;
//using System.Reflection;
//using Autofac;
//using Core.Common.Core;
//using Core.Common.Infrastructure.Autofac;
//using Takeaway.Data.Repositories;

//namespace Takeaway.Service.ServiceTests
//{
//    public class BootStrapper
//    {
//        public IContainer Build()
//        {
//            var builder = new ContainerBuilder();
        
//            //builder.FromAssemblyContaining<ServerSettingCreator>()
//            //   .AnnotatedWith<RepositoryCreatorAttribute>()
//            //   .WithDefaults();

//            //builder.FromAssemblyContaining<OrderController>()
//            //   .AnnotatedWith<EntityControllerAttribute>()
//            //   .WithDefaults();
            
            
//            var assemblies= new List<Assembly>()
//            {
//                typeof(IUnitOfWork).Assembly,
//                typeof(IDataQuery).Assembly,
//                typeof(ServerSettingRepository).Assembly
//            };

//            builder.RegisterProgramModules(assemblies);
//            return builder.Build();
//        } 
//    }
//}
