using Autofac;
using Identity.API.Application;
using Identity.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace Identity.API.Infrastructure.AutofacModules
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
            builder.Register(c => new UserRepository(_connectionString))
                .As<IUserRepository>()
                .InstancePerLifetimeScope();

            builder.Register(c => new PlanRepository(_connectionString))
                .As<IPlanRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PlanQueries>().As<IPlanQueries>();
            builder.Register(c => new PasswordHasher<UserEntity>())
                .As<IPasswordHasher<UserEntity>>()
                .InstancePerLifetimeScope();
        }
    }
}
