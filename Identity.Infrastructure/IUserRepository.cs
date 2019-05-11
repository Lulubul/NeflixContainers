using Identity.API.Application.Model;
using System.Threading.Tasks;

namespace Identity.Infrastructure
{
    public interface IUserRepository
    {
        Task<User> AddUser(UserEntity user);
        Task<UserEntity> Login(UserEntity userLogin);
    }
}