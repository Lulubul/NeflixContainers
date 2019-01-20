using AutoMapper;
using Subscription.API.Application.Model;
using Subscription.Infrastructure;

namespace Subscription.API.Application
{
    public class DomainProfile: Profile
    {
        public DomainProfile()
        {
            CreateMap<PlanEntity, Plan>();
        }
    }
}
