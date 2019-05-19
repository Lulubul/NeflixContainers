using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using Identity.Infrastructure;
using Identity.Domain.Model;

namespace Identity.API.Application
{
    public interface IPlanQueries
    {
        Task<IEnumerable<SubscriptionPlan>> GetAllPlans();
    }

    public class PlanQueries: IPlanQueries
    {
        private readonly IPlanRepository _planRepository;
        private readonly IMapper _mapper;

        public PlanQueries(IPlanRepository planRepository, IMapper mapper)
        {
            _planRepository = planRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SubscriptionPlan>> GetAllPlans()
        {
            var plans = await _planRepository.GetAllPlans();
            return plans.Select(plan => _mapper.Map<PlanEntity, SubscriptionPlan>(plan));
        }
    }
}
