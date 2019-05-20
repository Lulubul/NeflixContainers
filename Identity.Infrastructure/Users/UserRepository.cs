using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Identity.Domain.Exceptions;

namespace Identity.Infrastructure
{
    public interface IUserRepository
    {
        Task<UserEntity> AddUser(UserEntity user);
        Task<UserEntity> GetUserByEmail(string userEmail);
    }

    public class UserRepository : IUserRepository
    {
        private readonly UserIdentityContext _identityContext;

        public UserRepository(UserIdentityContext identityContext)
        {
            _identityContext = identityContext;
        }

        public async Task<UserEntity> GetUserByEmail(string userEmail)
        {
            return await _identityContext.Users.FirstOrDefaultAsync((user) => user.Email == userEmail);
        }

        public async Task<UserEntity> AddUser(UserEntity newUser)
        {
            var emailAddressExist = await _identityContext.Users.AnyAsync((user) => user.Email == newUser.Email);
            if (emailAddressExist)
            {
                throw new RegisterDomainException($"User with {newUser.Email} address already exists");
            }
            _identityContext.Users.Add(newUser);
            await _identityContext.SaveChangesAsync();
            return newUser;
        }

    }
}
