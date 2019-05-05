using System.ComponentModel.DataAnnotations;

namespace Identity.API.Application.Model
{
    public class UserLogin
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
