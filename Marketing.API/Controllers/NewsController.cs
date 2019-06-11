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
        private readonly IMailService _mailService;

        public NewsController(INewsRepository newsRepository, IMailService mailService)
        {
            _newsRepository = newsRepository;
            _mailService = mailService;
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

        [HttpPost]
        public async Task<IActionResult> SendNewsAsMail([FromQuery]string userId, [FromQuery]string email)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest($"Parameter is not defined in query {nameof(userId)}");
            }

            if (string.IsNullOrEmpty(email))
            {
                return BadRequest($"Parameter is not defined in query {nameof(email)}");
            }

            await _mailService.SendMail(userId, email);
            return Ok();
        }
    }
}
