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
        public async Task<IEnumerable<News>> GetNews([FromQuery]string userId, [FromQuery]string profileId)
        {
            var news = await _newsRepository.GetNewsAsync(userId, profileId);
            return news;
        }
    }
}
