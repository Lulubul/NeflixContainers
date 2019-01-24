using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Profiles.API.Application.Model;
using Profiles.Infrastructure;
using Profiles.Infrastructure.Entities;

namespace Profiles.API.Application.Queries
{
    public class ProfilesByUserIdHandler: IRequestHandler<ProfilesByUserIdQuery, ProfilesByUserIdQueryResult>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IMapper _mapper;

        public ProfilesByUserIdHandler(IProfileRepository profileRepository, IMapper mapper)
        {
            _profileRepository = profileRepository ?? throw new ArgumentNullException(nameof(profileRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ProfilesByUserIdQueryResult> Handle(ProfilesByUserIdQuery request, CancellationToken cancellationToken)
        {
            var profiles = await _profileRepository.GetProfiles(request.UserId);
            var userProfiles = profiles.Select(_mapper.Map<ProfileEntity, UserProfile>);
            return new ProfilesByUserIdQueryResult(userProfiles);
        }
    }
}