using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Profiles.API.Application.Commands;
using Profiles.API.Application.Model;
using Profiles.API.Application.Queries;

namespace Profiles.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProfilesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<controller>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserProfile>), 200)]
        public async Task<IActionResult> GetUserProfiles([FromQuery]Guid? usedId)
        {
            if (usedId == null)
            {
                return BadRequest($"Parameter is not defined in query {nameof(usedId)}");
            }

            var result = await _mediator.Send(new ProfilesByUserIdQuery(usedId.Value));
            return Ok(result.Profiles);
        }

        // Post: api/<controller>
        [HttpPost]
        public async Task<IActionResult> CreateNewProfile([FromBody]CreateProfileCommand createProfileCommand)
        {
            if (createProfileCommand == null)
            {
                return BadRequest($"Parameter is not defined in body {nameof(createProfileCommand)}");
            }

            var draft = await _mediator.Send(createProfileCommand);
            return Ok(draft);
        }

        // Post: api/<controller>
        [HttpPut]
        public async Task<IActionResult> UpdateUserProfile([FromBody]UpdateProfileCommand updateProfileCommand)
        {
            if (updateProfileCommand == null)
            {
                return BadRequest($"Parameter is not defined in body {nameof(updateProfileCommand)}");
            }
            var draft = await _mediator.Send(updateProfileCommand);
            return Ok(draft);
        }
    }
}