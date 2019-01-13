using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Profiles.Infrastructure;

namespace Profiles.API.Application.Commands
{
    public class CreateProfileCommandHandler: IRequestHandler<CreateProfileCommand, bool>
    {
        private readonly IProfileRepository _profileRepository;

        public CreateProfileCommandHandler(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<bool> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
        {
            bool result = await _profileRepository.AddUserProfile(Guid.Empty, null);
            return result;
        }
    }
}
