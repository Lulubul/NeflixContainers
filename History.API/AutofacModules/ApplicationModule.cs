using Autofac;
using History.Infrastructure;

namespace History.API.Infrastructure.AutofacModules
{
    public class ApplicationModule: Autofac.Module
    {
        private readonly string _connectionString;

        public ApplicationModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new HistoryRepository(_connectionString))
                .As<IHistoryRepository>()
                .InstancePerLifetimeScope();
        }
    }
}
