using History.Infrastructure.Entities;
using System;

namespace History.API.Application.Model
{
    public class HistoryItem
    {
        public string UserId { get; set; }
        public string ProfileId { get; set; }
        public string WatchingItemId { get; set; }
        public WatchingItemType WatchingItemType { get; set; }
        public DateTime Date { get; set; }
        public double? TimeWatched { get; set; }
        public string Genres { get; set; }
        public string VideoId { get; set; }
        public string ReleaseYear { get; set; }
    }
}
