using Autofac;

namespace Takeaway.Admin.Service
{
    public class QuartzAutofacModule : Module
    {
        public void Configure(ContainerBuilder builder)
        {
            builder.RegisterModule(this);
        }

        protected override void Load(ContainerBuilder builder)
        {
            // register Quartz Jobs
            builder.RegisterType<Quartz.Job.DirectoryScanJob>().AsSelf();
            builder.RegisterType<Quartz.Job.FileScanJob>().AsSelf();
            builder.RegisterType<Quartz.Job.NativeJob>().AsSelf();
            builder.RegisterType<Quartz.Job.NoOpJob>().AsSelf();
            builder.RegisterType<Quartz.Job.SendMailJob>().AsSelf();
        }
    }
}