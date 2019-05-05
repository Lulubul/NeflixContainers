using AutoMapper;
using History.API.Application.Commands;
using History.API.Application.Model;
using History.Infrastructure;

namespace Profiles.API.Application
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<HistoryEntity, HistoryItem>();
            CreateMap<CreateHistoryItemCommand, HistoryEntity>();
        }
    }
}
