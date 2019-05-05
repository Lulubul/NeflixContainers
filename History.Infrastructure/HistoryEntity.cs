using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace History.Infrastructure
{
    public class HistoryEntity : TableEntity
    {
        public string UserId { get; set; }
        public string ProfileId { get; set; }
        public string WatchingItemId { get; set; }
        public WatchingItemType WatchingItemType { get; set; }
        public DateTime Date { get; set; }
    }
}