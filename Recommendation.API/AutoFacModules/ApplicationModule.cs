using Autofac;
using Recommendation.API.Application;
using Recommendation.Infrastructure;

namespace Recommendation.API.AutoFacModules
{
    public class ApplicationModule : Autofac.Module
    {
        private readonly string _connectionString;

        public ApplicationModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<RecommendationsService>()
                .As<IRecommendationsService>()
                .InstancePerLifetimeScope();


            builder
                .Register(c => new RecommendationRepository(_connectionString))
                .As<IRecommendationRepository>()
                .InstancePerLifetimeScope();
        }
    }
}
