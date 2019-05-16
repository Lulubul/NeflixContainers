using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace History.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IRecommendationsService _recommendationsService;

        public RecommendationController(IRecommendationsService recommendationsService, IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
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
