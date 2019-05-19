using AutoMapper;
using Identity.Domain.Model;
using Identity.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Identity.API.Services
{
    public interface IUserService
    {
        Task<User> Login(UserLogin userLogin);
        Task<User> AddUser(UserRegister user);
    }

    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<UserEntity> _passwordHasher;

        public UserService(IUserRepository userRepository, IMapper mapper, IPasswordHasher<UserEntity> passwordHasher)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<User> Login(UserLogin user)
        {
            var dbUser = await _userRepository.GetUserByEmail(user.Email);
            if (dbUser == null)
            {
                return null;
            }
            if (_passwordHasher.VerifyHashedPassword(dbUser, dbUser.Password, user.Password) == PasswordVerificationResult.Failed)
            {
                return null;
            }
            return _mapper.Map<UserEntity, User>(dbUser);
        }

        public async Task<User> AddUser(UserRegister user)
        {
            var newUser = _mapper.Map<UserRegister, UserEntity>(user);
            user.Id = Guid.NewGuid().ToString();
            newUser.Password = _passwordHasher.HashPassword(newUser, user.Password);
            newUser.Id = user.Id.ToString();
            await _userRepository.AddUser(newUser);
            return _mapper.Map<UserRegister, User>(user);
        }
    }
}
