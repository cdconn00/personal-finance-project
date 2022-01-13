using Microsoft.AspNetCore.Mvc;
using NpgsqlTypes;
using pfp_api.Core;
using pfp_api.Models;
using System.Collections;
using System.Data;
using System.Net;

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
        public IActionResult Get([FromBody] RegistrationRequest req)
        {
            if (string.IsNullOrEmpty(req.FirstName) || string.IsNullOrEmpty(req.LastName) || string.IsNullOrEmpty(req.Email) || string.IsNullOrEmpty(req.Password))
            {
                return BadRequest("Missing or invalid request parameters");
            }

            User u = new User(-1, req.FirstName, req.LastName, req.Email, BCrypt.Net.BCrypt.HashPassword(req.Password));
            u.Save();

            if (u.Id == -1)
            {
                var message = "Unable to create account. That email already exists.";
                return BadRequest(message);
            }
            else
            {
                return Ok(u.APIKey);
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