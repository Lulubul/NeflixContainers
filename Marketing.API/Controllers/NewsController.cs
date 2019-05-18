using Marketing.Domain;
using Marketing.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsRepository _newsRepository;

        public NewsController(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        // GET: api/<controller>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<News>), 200)]
        public async Task<IActionResult> GetNews([FromQuery]string userId, [FromQuery]string profileId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest($"Parameter is not defined in query {nameof(userId)}");
            }

            if (string.IsNullOrEmpty(profileId))
            {
                return BadRequest($"Parameter is not defined in query {nameof(profileId)}");
            }

            var news = await _newsRepository.GetNewsAsync(userId, profileId);
            return Ok(news);
        }
    }
}
