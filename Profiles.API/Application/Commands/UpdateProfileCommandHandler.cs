using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Profiles.Infrastructure;
using Profiles.Infrastructure.Entities;

namespace Profiles.API.Application.Commands
{
    public class UpdateProfileCommandHandler: IRequestHandler<UpdateProfileCommand, bool>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IMapper _mapper;

        public UpdateProfileCommandHandler(IProfileRepository profileRepository, IMapper mapper)
        {
            _profileRepository = profileRepository ?? throw new ArgumentNullException(nameof(profileRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var profileEntity = _mapper.Map<UpdateProfileCommand, ProfileEntity>(request);
            bool hasUserProfileUpdate = await _profileRepository.UpdateUserProfile(profileEntity);
            return hasUserProfileUpdate;
        }
    }
}
