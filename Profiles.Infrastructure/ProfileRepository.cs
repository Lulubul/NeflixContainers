using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Profiles.Infrastructure
{

    public interface IProfileRepository
    {
        Task<IList<UserProfile>> GetProfiles(Guid userId);
    }

    public class ProfileRepository : IProfileRepository
    {
        public Task<IList<UserProfile>> GetProfiles(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
