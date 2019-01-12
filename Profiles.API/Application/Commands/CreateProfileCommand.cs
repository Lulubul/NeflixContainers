using MediatR;
using Profiles.API.Application.Model;

namespace Profiles.API.Application.Commands
{
    public class CreateProfileCommand: IRequest<UserProfile>
    {
    }
}
