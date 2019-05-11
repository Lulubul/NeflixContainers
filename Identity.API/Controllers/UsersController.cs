using System.Threading.Tasks;
using Identity.API.Application.Model;
using Identity.API.Services;
using Identity.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userRepository)
        {
            _userService = userRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegister user)
        {
            if (user == null)
            {
                return BadRequest($"Parameter is not defined in query {nameof(UserRegister)}");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User newUser = await _userService.AddUser(user);
            return Ok(newUser);
        }

        /// <summary>
        /// Handle postback from username/password login
        /// </summary>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            if (userLogin == null)
            {
                return BadRequest($"Parameter is not defined in query {nameof(userLogin)}");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = await _userService.Login(userLogin);
            if (user == null)
            {
                return Unauthorized();
            }
            return Ok(user);
        }

    }
}
