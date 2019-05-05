using AutoMapper;
using Profiles.API.Application.Commands;
using Profiles.API.Application.Model;
using Profiles.Infrastructure.Entities;

namespace Profiles.API.Application
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<ProfileEntity, UserProfile>();
            CreateMap<UserProfile, CreateProfileCommand>();
            CreateMap<UserProfile, UpdateProfileCommand>();
            CreateMap<CreateProfileCommand, ProfileEntity>();
            CreateMap<UpdateProfileCommand, ProfileEntity>();
        }
    }
}
