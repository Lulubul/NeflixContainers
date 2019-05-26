using AutoMapper;
using Identity.API.Configuration;
using Identity.Domain.Exceptions;
using Identity.Domain.Model;
using Identity.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
        private readonly AppSettings _appSettings;

        public UserService(IUserRepository userRepository, IMapper mapper, IPasswordHasher<UserEntity> passwordHasher, IOptions<AppSettings> appSettings)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _appSettings = appSettings.Value;
        }

        public async Task<User> Login(UserLogin user)
        {
            var dbUser = await _userRepository.GetUserByEmail(user.Email);
            if (dbUser == null || _passwordHasher.VerifyHashedPassword(dbUser, dbUser.Password, user.Password) == PasswordVerificationResult.Failed)
            {
                throw new LoginDomainException("User or password are incorrect. Please try again");
            }
            var userModel = _mapper.Map<UserEntity, User>(dbUser);

            var tokenHandler = new JwtSecurityTokenHandler();
            userModel.Token = GenerateToken(userModel.Id.ToString());
            return userModel;
        }

        public async Task<User> AddUser(UserRegister user)
        {
            var newUser = _mapper.Map<UserRegister, UserEntity>(user);
            user.Id = Guid.NewGuid().ToString();
            newUser.Password = _passwordHasher.HashPassword(newUser, user.Password);
            newUser.Id = user.Id.ToString();
            await _userRepository.AddUser(newUser);
            var userModel =  _mapper.Map<UserRegister, User>(user);
            userModel.Token = GenerateToken(userModel.Id.ToString());
            return userModel;
        }

        private string GenerateToken(string id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, id)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
