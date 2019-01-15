using AutoMapper;
using Profiles.API.Application.Model;
using Profiles.Infrastructure;

namespace Profiles.API.Application
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<ProfileEntity, UserProfile>();
        }
    }
}
