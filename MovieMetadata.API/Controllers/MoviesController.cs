using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieMetadata.API.Application;
using MovieMetadata.Infrastructure;

namespace MovieMetadata.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesQueries _movieQueries;
        private readonly IMovieRepository _movieRepository;

        public MoviesController(IMoviesQueries movieQueries, IMovieRepository movieRepository)
        {
            _movieQueries = movieQueries;
            _movieRepository = movieRepository;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> GetMoviesInCategories()
        {
            var movies = await _movieQueries.GetTopMoviesInCategories();
            return Ok(movies);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetMoviesByIds(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return BadRequest($"Parameter is not defined in query {nameof(ids)}");
            }
            var movies = await _movieRepository.GetMoviesByIdsAsync(ids.Split(","));
            return Ok(movies);
        }

        // GET: api/<controller>
        [HttpGet("{name}")]
        public async Task<IActionResult> GetMovieByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest($"Parameter is not defined in query {nameof(name)}");
            }

            var movieStream = await _movieRepository.GetMovieByNameAsync(name);
            return new FileStreamResult(movieStream, new MediaTypeHeaderValue("video/mp4").MediaType)
            {
                EnableRangeProcessing = true
            };
        }
    }

}