using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Profiles.Infrastructure;

namespace Profiles.API.Application.Queries
{
    public interface IProfileQueries
    {
        Task<IList<ProfileEntity>> GetUserProfile(Guid userId);
    }

    public class ProfileQueries : IProfileQueries
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileQueries(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public Task<IList<ProfileEntity>> GetUserProfile(Guid userId)
        {
            return _profileRepository.GetProfiles(userId);
        }
    }
}
