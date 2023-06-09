using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IS_Lab8_JWT.Model;
using IS_Lab8_JWT.Sevices.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace IS_Lab8_JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService userService;
        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticationRequest request)
        {
            var response = userService.Authenticate(request);
            if (response == null)
                return BadRequest(new
                {
                    message = "Username or password is incorrect"
                });
            return Ok(response);
        }
        [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("show")]
        public IActionResult GetUsers()
        {
            var users = userService.GetUsers();
            return Ok(users);
        }
        [Authorize(Roles = "user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("count")]
        public IActionResult CountUsers()
        {
            var users = userService.GetUsers();
            var wynik = users.Count();
            return Ok(wynik);
        }
    }
}