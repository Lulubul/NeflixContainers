using AutoMapper;
using Identity.API.Application.Model;
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
            CreateMap<UserEntity, User>()
                 .ForMember(x => x.Id, opt => opt.MapFrom(x => x.RowKey));
        }
    }
}
