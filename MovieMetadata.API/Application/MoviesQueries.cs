using System.Collections.Generic;
using System.Threading.Tasks;
using MovieMetadata.API.Application.Models;
using MovieMetadata.Infrastructure;
using System.Linq;
using AutoMapper;

namespace MovieMetadata.API.Application
{
    public interface IMoviesQueries
    {
        Task<IEnumerable<Movie>> GetTopMoviesInCategories();
        Task<IEnumerable<MovieGenre>> GetGenres();
    }

    public class MoviesQueries: IMoviesQueries
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MoviesQueries(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Movie>> GetTopMoviesInCategories()
        {
            var movies = await _movieRepository.GetTopMoviesInCategoriesAsync();
            return movies.Select((movie) => _mapper.Map<Movie>(movie));
        }

        public async Task<IEnumerable<MovieGenre>> GetGenres()
        {
            var movies = await _movieRepository.GetGenresAsync();
            return movies.Select((movie) => _mapper.Map<MovieGenre>(movie));
        }
    }
}
