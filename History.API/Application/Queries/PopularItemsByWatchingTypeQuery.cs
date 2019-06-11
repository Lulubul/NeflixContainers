using History.Infrastructure.Entities;
using MediatR;

namespace History.API.Application.Queries
{
    public class PopularItemsByWatchingTypeQuery : IRequest<HistoryItemByUserIAndProfileResult>
    {
        public WatchingItemType WatchingType { get; set; }

        public PopularItemsByWatchingTypeQuery(WatchingItemType watchingType)
        {
            WatchingType = watchingType;
        }
    }
}