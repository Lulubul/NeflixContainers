using AutoMapper;
using Identity.Domain.Model;
using Identity.Infrastructure;

namespace Identity.API.Application
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<UserLogin, UserEntity>();
            CreateMap<UserRegister, UserEntity>();
            CreateMap<UserRegister, User>();
            CreateMap<UserEntity, User>();
            CreateMap<PlanEntity, SubscriptionPlan>();
        }
    }
}
