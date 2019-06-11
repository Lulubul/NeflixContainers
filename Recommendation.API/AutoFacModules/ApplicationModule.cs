using Autofac;
using Recommendation.API.Application;
using Recommendation.Infrastructure;

namespace Recommendation.API.AutoFacModules
{
    public class ApplicationModule : Autofac.Module
    {
        public ApplicationModule()
        {
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<RecommendationsService>()
                .As<IRecommendationsService>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<RecommendationRepository>()
                .As<IRecommendationRepository>()
                .InstancePerLifetimeScope();
        }
    }
}
