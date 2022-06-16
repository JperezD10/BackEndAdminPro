using Application.Interfaces;
using BackEndAdminPro.Configuration;
using Entities;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.AuthServices;
using Services.UserServices;

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

        [HttpPost("Register")]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var userFound = await _userApplication.ValidateUserByEmail(user.Email);
                if (userFound != null) return BadRequest("User already exists");

                User u = UserService.ConvertDTO(user);
                u.Role = "User";
                u.Image = "";
                var parameters = new TokenParameters()
                {
                    Id = u.Id.ToString(),
                    PasswordHash = u.Password,
                    Email = u.Email,
                };

                var jwtToken = _tokenService.GenerateToken(parameters);
                var created = await _userApplication.SaveAsync(u);

                UserCreatedResponseDTO response = new UserCreatedResponseDTO()
                {
                    Token = jwtToken,
                    user = u
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var userFound = await _userApplication.LoginAsync(dto.Email, EncriptadoService.ComputeSha256Hash(dto.Password));

            if (userFound == null) return BadRequest(new LoginResponseDTO
            {
                Login = false,
                Error = "Invalid email or password"
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
