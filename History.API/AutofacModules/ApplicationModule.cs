using Autofac;
using History.Infrastructure;

namespace History.API.Infrastructure.AutofacModules
{
    public class ApplicationModule: Autofac.Module
    {
        public ApplicationModule()
        {
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<HistoryRepository>()
                .As<IHistoryRepository>()
                .InstancePerLifetimeScope();
        }
    }
}
