using System.Collections.Generic;
using Profiles.API.Application.Model;

namespace Profiles.API.Application.Queries
{
    public class ProfilesByUserIdQueryResult
    {
        public readonly IEnumerable<UserProfile> Profiles;

        public ProfilesByUserIdQueryResult(IEnumerable<UserProfile> profiles)
        {
            Profiles = profiles;
        }
    }
}