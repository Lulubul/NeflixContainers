using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Recommendation.API.Application;

namespace History.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationsController : ControllerBase
    {
        private readonly IRecommendationsService _recommendationsService;

        public RecommendationsController(IRecommendationsService recommendationsService)
        {
            _recommendationsService = recommendationsService ?? throw new ArgumentNullException(nameof(recommendationsService));
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> GetVideoRecommendationsByUser([FromQuery]Guid? userId, [FromQuery]Guid? profileId)
        {
            if (userId == null)
            {
                return BadRequest($"Parameter is not defined in query {nameof(userId)}");
            }

            if (profileId == null)
            {
                return BadRequest($"Parameter is not defined in query {nameof(profileId)}");
            }

            var recommendations = await _recommendationsService.GetVideoRecommendationsByUser(userId.Value, profileId.Value);
            return Ok(recommendations);
        }
    }
}
