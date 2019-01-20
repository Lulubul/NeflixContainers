using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieMetadata.API.Application;

namespace MovieMetadata.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesQueries _movieQueries;

        public MoviesController(IMoviesQueries movieQueries)
        {
            _movieQueries = movieQueries;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> GetMoviesInCategories()
        {
            var movies = await _movieQueries.GetTopMoviesInCategories();
            return Ok(movies);
        }

        // GET: api/<controller>
        [HttpGet]
        [Route("~/genres")]
        public async Task<IActionResult> GetGenres()
        {
            var movies = await _movieQueries.GetGenres();
            return Ok(movies);
        }
    }

}