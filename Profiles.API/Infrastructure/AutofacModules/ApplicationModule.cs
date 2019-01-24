using Autofac;
using Profiles.Infrastructure;

namespace Profiles.API.Infrastructure.AutofacModules
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
            builder.Register(c => new ProfileRepository(_connectionString))
                .As<IProfileRepository>()
                .InstancePerLifetimeScope();
        }
    }
}
