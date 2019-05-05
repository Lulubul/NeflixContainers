using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Profiles.API.Application.Model;
using Profiles.Infrastructure;
using Profiles.Infrastructure.Entities;

namespace Profiles.API.Application.Commands
{
    public class CreateProfileCommandHandler: IRequestHandler<CreateProfileCommand, bool>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IMapper _mapper;

        public CreateProfileCommandHandler(IProfileRepository profileRepository, IMapper mapper)
        {
            _profileRepository = profileRepository ?? throw new ArgumentNullException(nameof(profileRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
        {
            var newProfileEntity = _mapper.Map<CreateProfileCommand, ProfileEntity>(request);
            newProfileEntity.PartitionKey = request.UserId;
            var profileId = Guid.NewGuid();
            newProfileEntity.RowKey = profileId.ToString();

            var addResponse = await _profileRepository.AddUserProfile(newProfileEntity);
            return addResponse;
        }
    }
}
