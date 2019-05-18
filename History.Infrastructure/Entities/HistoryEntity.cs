using System;

namespace History.Infrastructure.Entities
{
    public class HistoryEntity
    {
        public string UserId { get; set; }
        public string ProfileId { get; set; }
        public string WatchingItemId { get; set; }
        public WatchingItemType WatchingItemType { get; set; }
        public DateTime Date { get; set; }
    }
}