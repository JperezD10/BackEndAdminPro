using Application;
using Application.Interfaces;
using Entities;
using Entities.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.UserServices;

namespace BackEndAdminPro.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserApplication _userApplication;
        private string[] extentions = new string[] { "jpg", "png","jpeg" };
        public UserController(IUserApplication userApplication)
        {
            _userApplication = userApplication;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers(int from)
        {
            try
            {
                var users = await _userApplication.GetAllAsync();
                int total = users.Count();
                var usersFiltered = users.Skip(from-1).Take(5).ToList();
                UserResponseDTO response = new UserResponseDTO() { users = usersFiltered, totalUsers = total };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("Image/{id}")]
        public async Task<IActionResult> UpdateImage(int id, IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName);
            if (!extension.Contains(extension)) return BadRequest("Image error");
            if (file.Length <= 0) return BadRequest("File error");
            
            var user = await _userApplication.GetByIdAsync(id);
            user.Image = ToByteService.convertImage(file);
            _userApplication.Save(user);
            var memoryStream = ImageService.decodeImage(user.Image);
            return Ok(memoryStream);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditUser(int id, [FromBody] UserDTO user)
        {
            if (id == 0) return BadRequest("Incorrect Id");

            try
            {
                var userFound = await _userApplication.GetByIdAsync(id);
                if (userFound == null) return NotFound("User doesn't found");

                var emailFound = await _userApplication.ValidateUserByEmail(user.Email);
                if (emailFound != null) return BadRequest("A user with that email already exists");

                User userModified = UserService.ConvertDTO(user);

                userFound = userModified;

                await _userApplication.SaveAsync(userFound);
                return Ok(userFound);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                if (id == 0) return BadRequest("Incorrect Id");

                if (await _userApplication.GetByIdAsync(id) == null) return BadRequest("The user doesn't exists");

                await _userApplication.DeleteAsync(id);
                return Ok("User deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
