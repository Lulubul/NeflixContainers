using System.Threading.Tasks;
using Identity.API.Services;
using Identity.Domain.Exceptions;
using Identity.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userRepository)
        {
            _userService = userRepository;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]UserRegister user)
        {
            if (user == null)
            {
                return BadRequest($"Parameter is not defined in query {nameof(UserRegister)}");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User newUser = null;
            try
            {
                newUser = await _userService.AddUser(user);
            }
            catch (RegisterDomainException exception)
            {
                return BadRequest(exception);
            }
            return Ok(newUser);
        }

        /// <summary>
        /// Handle postback from username/password login
        /// </summary>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]UserLogin userLogin)
        {
            if (userLogin == null)
            {
                return BadRequest($"Parameter is not defined in query {nameof(userLogin)}");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = null;
            try
            {
                user = await _userService.Login(userLogin);
            }
            catch(LoginDomainException e)
            {
                return Unauthorized(e.Message);
            }
            if (user == null)
            {
                return Unauthorized();
            }
            return Ok(user);
        }
    }
}
