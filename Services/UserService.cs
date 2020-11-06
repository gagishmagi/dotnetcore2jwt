using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using DotnetCore3Jwt.Helpers;
using DotnetCore3Jwt.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DotnetCore3Jwt.Services
{
    public class UserService : IUserService
    {

        private readonly Settings _settings;

        public UserService(IOptions<Settings> settings)
        {
            _settings = settings.Value;
        }

        private List<User> _users = new List<User>
        {
            new User { Id = 1, FirstName = "Test" , LastName = "Testing", Username = "test", Password = "test" },
            new User { Id = 2, FirstName = "Gagi" , LastName = "Shmagi", Username = "gagi", Password = "password" }
        };

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            if(user == null) return null;

            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);

        }

        private string generateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.Secret);
            var tokenDesciptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(new[] {new Claim("id", user.Id.ToString())}),
                Expires = DateTime.UtcNow.AddDays('2'),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDesciptor);
            return tokenHandler.WriteToken(token);
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public User GetById(int id)
        {
           return _users.FirstOrDefault( u => u.Id == id);
        }
    }
}
