using AutoMapper;
using MovieMetadata.API.Application.Models;
using MovieMetadata.Infrastructure.Entities;

namespace MovieMetadata.API.Application
{
    public class DomainProfile: Profile
    {
        public DomainProfile()
        {
            CreateMap<MovieEntity, Movie>();
            CreateMap<MovieGenreEntity, MovieGenre>();
        }
    }
}
