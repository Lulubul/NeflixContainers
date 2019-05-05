using System.Threading.Tasks;
using Identity.API.Application.Model;

namespace Identity.API.Controllers
{
    public interface IUsersService
    {
        Task<User> AddUser(UserRegister user);
        Task<User> Login(UserLogin userLogin);
    }
}