using System.Collections.Generic;
using DotnetCore3Jwt.Models;

namespace DotnetCore3Jwt.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
         User GetById(int id);
    }

}
