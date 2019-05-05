using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public ProfilesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // GET: api/<controller>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserProfile>), 200)]
        public async Task<IActionResult> GetUserProfiles([FromQuery]Guid? userId)
        {
            if (userId == null)
            {
                return BadRequest($"Parameter is not defined in query {nameof(userId)}");
            }

            var result = await _mediator.Send(new ProfilesByUserIdQuery(userId.Value));
            return Ok(result.Profiles);
        }

        // Post: api/<controller>
        [HttpPost]
        public async Task<IActionResult> CreateNewProfile([FromQuery]Guid? userId, [FromBody]UserProfile userProfile)
        {
            if (userId == null)
            {
                return BadRequest($"Parameter is not defined in query {nameof(userId)}");
            }

            if (userProfile == null)
            {
                return BadRequest($"Parameter is not defined in body {nameof(userProfile)}");
            }

            var createProfileCommand = _mapper.Map<UserProfile, CreateProfileCommand>(userProfile);
            createProfileCommand.UserId = userId.ToString();
            var draft = await _mediator.Send(createProfileCommand);
            return Ok(draft);
        }

        // Post: api/<controller>
        [HttpPut]
        public async Task<IActionResult> UpdateUserProfile([FromQuery]Guid? userId, [FromBody]UserProfile userProfile)
        {
            if (userId == null)
            {
                return BadRequest($"Parameter is not defined in query {nameof(userId)}");
            }

            if (userProfile == null)
            {
                return BadRequest($"Parameter is not defined in body {nameof(userProfile)}");
            }
            var updateProfileCommand = _mapper.Map<UserProfile, UpdateProfileCommand>(userProfile);
            updateProfileCommand.UserId = userId.ToString();
            var draft = await _mediator.Send(updateProfileCommand);
            return Ok(draft);
        }
    }
}