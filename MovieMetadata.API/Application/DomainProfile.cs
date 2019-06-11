using AutoMapper;
using MovieMetadata.API.Application.Models;
using MovieMetadata.Infrastructure.Entities;

namespace MovieMetadata.API.Application
{
    public class DomainProfile: Profile
    {
        public DomainProfile()
        {
            CreateMap<MovieEntity, Movie>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.RowKey));
            CreateMap<MovieGenreEntity, MovieGenre>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.RowKey));
        }
    }
}
