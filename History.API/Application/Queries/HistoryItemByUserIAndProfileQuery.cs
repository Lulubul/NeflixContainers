using System;
using MediatR;

namespace History.API.Application.Queries
{
    public class HistoryItemByUserIAndProfileQuery: IRequest<HistoryItemByUserIAndProfileResult>
    {
        public string UserId { get; set; }
        public string ProfileId { get; set; }

        public HistoryItemByUserIAndProfileQuery(string userId, string profileId)
        {
            UserId = userId;
            ProfileId = profileId;
        }
    }
}