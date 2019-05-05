using System.Threading.Tasks;
using Identity.API.Application.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
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

            User newUser = await _usersService.AddUser(user);
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

            User user = await _usersService.Login(userLogin);
            if (user == null)
            {
                return Unauthorized();
            }
            return Ok(user);
        }

        /// <summary>
        /// Handle logout page postback
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void Logout(string logoutId)
        {
        }
    }
}
