using DotnetCore3Jwt.Helpers;
using DotnetCore3Jwt.Models;
using DotnetCore3Jwt.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCore3Jwt.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {

        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [Helpers.Authorize]
        [HttpGet]
        public IActionResult GetAll(){
            var users = _userService.GetAll();
            return Ok(users);
            // return null;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model){
            var response = _userService.Authenticate(model);
            if( response == null)
                return BadRequest(new { Message = "Username or password is incorrect"});

            return Ok(response);
        }

    }
}
