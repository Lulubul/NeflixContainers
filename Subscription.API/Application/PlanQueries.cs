using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Subscription.API.Application.Model;
using Subscription.Infrastructure;
using System.Linq;
using AutoMapper;

namespace Subscription.API.Application
{
    public interface IPlanQueries
    {
        Task<IEnumerable<Plan>> GetAllPlans();
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

        public async Task<IEnumerable<Plan>> GetAllPlans()
        {
            var plans = await _planRepository.GetAllPlans();
            return plans.Select(plan => _mapper.Map<PlanEntity,Plan>(plan));
        }
    }
}
