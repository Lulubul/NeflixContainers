using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Subscription.Infrastructure
{
    public interface IPlanRepository
    {
        Task<IEnumerable<PlanEntity>> GetAllPlans();
    }

    public class PlanRepository : IPlanRepository
    {
        public Task<IEnumerable<PlanEntity>> GetAllPlans()
        {
            throw new NotImplementedException();
        }
    }
}
