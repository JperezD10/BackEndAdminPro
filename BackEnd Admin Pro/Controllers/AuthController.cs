using Application.Interfaces;
using BackEndAdminPro.Configuration;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.AuthServices;

namespace BackEndAdminPro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserApplication _userApplication;
        private readonly ITokenHandlerService _tokenService;

        public AuthController(IUserApplication userApplication, ITokenHandlerService tokenService)
        {
            _userApplication = userApplication;
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            if(!ModelState.IsValid) return BadRequest(new LoginResponseDTO 
            { 
                Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList(),
                Login = false
            });

            var userFound = await _userApplication.LoginAsync(dto.Email, EncriptadoService.ComputeSha256Hash(dto.Password));

            if (userFound == null) return BadRequest(new LoginResponseDTO
            {
                Login = false,
                Errors = new List<string> { "Invalid email or password" }
            });

            var parameters = new TokenParameters()
            {
                Id = userFound.Id.ToString(),
                PasswordHash = userFound.Password,
                Email = userFound.Email,
            };

            var jwtToken = _tokenService.GenerateToken(parameters);

            return Ok(new LoginResponseDTO
            {
                Login = true,
                Token = jwtToken,
            });
        }
    }
}
