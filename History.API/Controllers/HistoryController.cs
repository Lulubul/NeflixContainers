using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using History.API.Application.Commands;
using History.API.Application.Model;
using History.API.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace History.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HistoryController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        // GET: api/<controller>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<HistoryItem>), 200)]
        public async Task<IActionResult> GetHistory([FromQuery]string userId, [FromQuery]string profileId)
        {
            if (userId == null)
            {
                return BadRequest($"Parameter is not defined in query {nameof(userId)}");
            }

            if (profileId == null)
            {
                return BadRequest($"Parameter is not defined in query {nameof(profileId)}");
            }

            var result = await _mediator.Send(new HistoryItemByUserIAndProfileQuery(userId, profileId));
            return Ok(result.HistoryItems);
        }

        // POST: api/<controller>
        [HttpPost]
        public async Task<IActionResult> AddItemInHistory([FromBody]CreateHistoryItemCommand historyItem)
        {
            if (historyItem == null)
            {
                return BadRequest($"Parameter is not defined in query {nameof(historyItem)}");
            }

            return Ok(await _mediator.Send(historyItem));
        }
    }
}
