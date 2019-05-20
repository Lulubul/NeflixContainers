using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure
{
    public interface IPlanRepository
    {
        Task<IEnumerable<PlanEntity>> GetAllPlans();
    }

    public class PlanRepository : IPlanRepository
    {
        private readonly UserIdentityContext _identityContext;

        public PlanRepository(UserIdentityContext identityContext)
        {
            _identityContext = identityContext;
        }

        public async Task<IEnumerable<PlanEntity>> GetAllPlans()
        {
            return await _identityContext.Plans.ToListAsync();
        }
    }
}
