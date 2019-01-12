using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Profiles.API.Application.Model;

namespace Profiles.API.Application.Commands
{
    public class UpdateProfileCommand: IRequest<UserProfile>
    {
    }
}
