using System;
using Polly;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Polly.Retry;

namespace Identity.Infrastructure
{
    public class UserIdentityContextSeed
    {
        public async Task SeedAsync(UserIdentityContext context, ILogger<UserIdentityContextSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(UserIdentityContextSeed));
            await policy.ExecuteAsync(async () =>
            {
                if (!context.Plans.Any())
                {
                    await context.Plans.AddRangeAsync(GetPreconfiguredPlans());
                    await context.SaveChangesAsync();
                }
            });
        }

        private IEnumerable<PlanEntity> GetPreconfiguredPlans()
        {
            return new List<PlanEntity>()
            {
                new PlanEntity() {
                    Id = "236ca1ac-305c-4f3a-b8bd-86e4275f22ed",
                    CancelAnytime = true,
                    HD = true,
                    MonthlyPrice = 10,
                    Name = "Standard",
                    NoScreens = 2,
                    UltraHD = false
                },
                new PlanEntity() {
                    Id = "5b9167c0-8b59-4cba-a04f-881cee4ebaaa",
                    CancelAnytime = true,
                    HD = true,
                    MonthlyPrice = 12,
                    Name = "Premium",
                    NoScreens = 4,
                    UltraHD = false
                },
                new PlanEntity() {
                    Id = "f68e7f55-6df3-4372-bb10-f53c30880549",
                    CancelAnytime = true,
                    HD = false,
                    MonthlyPrice = 8,
                    Name = "Basic",
                    NoScreens = 1,
                    UltraHD = false
                }
            };
        }

        private AsyncRetryPolicy CreatePolicy(ILogger<UserIdentityContextSeed> logger, string prefix, int retries = 3)
        {
            return Policy.Handle<SqlException>().
                WaitAndRetryAsync(
                    retryCount: retries,
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                        logger.LogWarning(exception, "[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}", prefix, exception.GetType().Name, exception.Message, retry, retries);
                    }
                );
        }
    }
}
