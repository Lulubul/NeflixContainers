using System.Runtime.Serialization;
using MediatR;

namespace Profiles.API.Application.Commands
{
    public class CreateProfileCommand: IRequest<bool>
    {
        public string AvatarUrl { get; set; }
        public string Language { get; set; }
        public string Name { get; set; }
        public string MaturityLevel { get; set; }
        public string UserId { get; set; }

    }
}
