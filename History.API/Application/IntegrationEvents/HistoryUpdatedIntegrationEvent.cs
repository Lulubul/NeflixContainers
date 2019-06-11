using EventBusRabbitMQ;
using System;

namespace History.API.Application.IntegrationEvents
{
    public class HistoryUpdatedIntegrationEvent : IntegrationEvent
    {
        public string UserId { get; set; }
        public string ProfileId { get; set; }
        public string WatchingItemId { get; set; }
        public DateTime Date { get; set; }
        public string Genres { get; set; }
        public string ReleaseYear { get; set; }

        public HistoryUpdatedIntegrationEvent (string userId, string profileId, string watchingItemId, DateTime date, string genres, string releaseYear)
        {
            UserId = userId;
            ProfileId = profileId;
            WatchingItemId = watchingItemId;
            Date = date;
            Genres = genres;
            ReleaseYear = releaseYear;
        }
    }
}
