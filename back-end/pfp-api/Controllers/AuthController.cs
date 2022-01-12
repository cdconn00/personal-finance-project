using Microsoft.AspNetCore.Mvc;
using NpgsqlTypes;
using pfp_api.Core;
using pfp_api.Models;
using System.Collections;
using System.Data;
namespace pfp_api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("")]
        public string Test()
        {
            return "test";
        } 

        [HttpPost("register")]
        public void Get([FromBody] RegistrationRequest req)
        {
            User u = new User(-1, req.FirstName, req.LastName, req.Email, BCrypt.Net.BCrypt.HashPassword(req.Password));
            u.Save();

            if (u.Id == -1)
            {
                // error trying to register user
            }
            else
            {
                
                // user registered successfully, communicate back
            }
        }

        public class RegistrationRequest
        {
            public string Email { get; set; } = "";
            public string FirstName { get; set; } = "";
            public string LastName { get; set; } = "";
            public string Password { get; set; } = "";
        }
    }
}