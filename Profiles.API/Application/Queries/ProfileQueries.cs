using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Profiles.API.Application.Model;
using Profiles.Infrastructure;

namespace Profiles.API.Application.Queries
{
    public interface IProfileQueries
    {
        Task<IList<UserProfile>> GetUserProfile(Guid userId);
    }

    public class ProfileQueries : IProfileQueries
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IMapper _mapper;

        public ProfileQueries(IProfileRepository profileRepository, IMapper mapper)
        {
            _profileRepository = profileRepository;
            _mapper = mapper;
        }

        public async Task<IList<UserProfile>> GetUserProfile(Guid userId)
        {
            var profiles = await _profileRepository.GetProfiles(userId);
            return profiles.Select(_mapper.Map<ProfileEntity, UserProfile>).ToList();
        }
    }
}
