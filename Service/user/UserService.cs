using Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.user;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Zxcvbn;

namespace Service.user;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    IConfiguration _configuration;
    public UserService(IUserRepository userRepository, IConfiguration configuration)
    {
        _configuration = configuration;
        this.userRepository = userRepository;
    }
    public async Task<IEnumerable<User>> GetUsers()
    {
        return await userRepository.Get();
    }
    public async Task<User> getUser(string email, string password)
    {

        var user = await userRepository.getUser(email, password);
        user.Token = generateJwtToken(user);

        return user;
    }
    public async Task<User> GetUsreById(int id)
    {
        return await userRepository.GetById(id);
    }
    public async Task<User> Register(User user)
    {
        var res = Core.EvaluatePassword(user.Password);
        if (res.Score >= 2)
        {
            return await userRepository.Register(user);
        }
        else return null;
    }
    public async Task<User> Login(string userName, string password)
    {
        return await userRepository.Login(userName, password);
    }
    public async Task<User> UpdateUser(int id, User user)
    {
        return await userRepository.Put(id, user);
    }
    private string generateJwtToken(User user)
    {
        // authentication successful so generate jwt token
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration.GetSection("key").Value);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.Name, user.UserId.ToString()),
                // new Claim("roleId", 7.ToString())
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);

    }
}
