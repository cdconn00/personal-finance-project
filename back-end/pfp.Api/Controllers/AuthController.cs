using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Generators;
using pfp.Api.DTOs;
using pfp.Core.Entities;
using pfp.Application.Interfaces;

namespace pfp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost(template:"register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            };

            return Created("success", await _unitOfWork.Users.AddAsync(user));
        }

        [HttpPost(template: "login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var user = await _unitOfWork.Users.GetByEmailAsync(dto.Email);

            if (user == null) 
                return BadRequest(error: "Invalid email address and/or password.");

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
                return BadRequest(error: "Invalid email address and/or password.");

            return Ok(user);
        }
    }
}
