using System.Runtime.Serialization;
using MediatR;

namespace Profiles.API.Application.Commands
{
    [DataContract]
    public class UpdateProfileCommand: IRequest<bool>
    {
    }
}
