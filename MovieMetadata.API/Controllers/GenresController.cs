using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieMetadata.API.Application;
using MovieMetadata.Infrastructure;

namespace MovieMetadata.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IMoviesQueries _movieQueries;

        public GenresController(IMoviesQueries movieQueries)
        {
            _movieQueries = movieQueries;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> GetAllGenres()
        {
            var genres = await _movieQueries.GetGenres();
            return Ok(genres);
        }
    }
}
