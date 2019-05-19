using Autofac;
using Identity.API.Application;
using Identity.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace Identity.API.Infrastructure.AutofacModules
{
    public class ApplicationModule: Autofac.Module
    {
        public ApplicationModule()
        {
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserRepository>()
                .As<IUserRepository>()
                .InstancePerLifetimeScope();
            builder.Register(c => new PasswordHasher<UserEntity>())
                .As<IPasswordHasher<UserEntity>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PlanRepository>()
                .As<IPlanRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<PlanQueries>().As<IPlanQueries>();
        }
    }
}
