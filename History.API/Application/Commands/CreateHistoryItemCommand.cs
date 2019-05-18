using MediatR;
using System.ComponentModel.DataAnnotations;
using System;
using History.Infrastructure.Entities;

namespace History.API.Application.Commands
{
    public class CreateHistoryItemCommand : IRequest<bool>
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string ProfileId { get; set; }
        [Required]
        public string WatchingItemId { get; set; }
        [Required]
        public WatchingItemType WatchingItemType { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
