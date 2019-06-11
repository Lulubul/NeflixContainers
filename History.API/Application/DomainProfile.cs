using AutoMapper;
using History.API.Application.Commands;
using History.API.Application.IntegrationEvents;
using History.API.Application.Model;
using History.Infrastructure.Entities;

namespace Profiles.API.Application
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<HistoryItem, HistoryEntity>();
            CreateMap<HistoryEntity, HistoryItem>();
            CreateMap<CreateHistoryItemCommand, HistoryEntity>();
            CreateMap<CreateHistoryItemCommand, HistoryUpdatedIntegrationEvent>();
        }
    }
}
