using Application.Interfaces;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace BackEndAdminPro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserApplication _userApplication;

        public AuthController(IUserApplication userApplication)
        {
            _userApplication = userApplication;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var userFound = await _userApplication.LoginAsync(dto.Email, EncriptadoService.ComputeSha256Hash(dto.Password));

            if (userFound == null) return BadRequest("Incorrect email or password");

            return Ok(userFound);
        }
    }
}
