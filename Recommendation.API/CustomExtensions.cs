using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Recommendation.API
{
    public static class CustomExtensions
    {
        public static IServiceCollection AddCustomHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            var healthChecksBuilder = services.AddHealthChecks();
            healthChecksBuilder.AddCheck("self", () => HealthCheckResult.Healthy());
            return services;
        }
    }
}
