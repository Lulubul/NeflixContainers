using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace History.Infrastructure.Entities
{
    public class HistoryEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
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