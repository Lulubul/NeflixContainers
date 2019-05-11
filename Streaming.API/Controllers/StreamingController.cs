using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Streaming.Infrasturcture;

namespace Streaming.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StreamingController: ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public StreamingController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
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
