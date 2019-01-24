using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Profiles.Infrastructure;

namespace Profiles.API.Application.Commands
{
    public class UpdateProfileCommandHandler: IRequestHandler<UpdateProfileCommand, bool>
    {
        private readonly IProfileRepository _profileRepository;

        public UpdateProfileCommandHandler(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository ?? throw new ArgumentNullException(nameof(profileRepository));
        }

        public async Task<bool> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            bool hasUserProfileUpdate = await _profileRepository.UpdateUserProfile(Guid.Empty, null);
            return hasUserProfileUpdate;
        }
    }
}
