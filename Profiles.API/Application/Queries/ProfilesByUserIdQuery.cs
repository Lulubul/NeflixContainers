using System;
using MediatR;

namespace Profiles.API.Application.Queries
{
    public class ProfilesByUserIdQuery: IRequest<ProfilesByUserIdQueryResult>
    {
        public Guid UserId { get; set; }

        public ProfilesByUserIdQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}