using Application;
using Application.Interfaces;
using BackEndAdminPro.DTOs;
using Entities;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.UserServices;

namespace BackEndAdminPro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserApplication _userApplication;

        public UserController(IUserApplication userApplication)
        {
            _userApplication = userApplication;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                User u = UserService.ConvertDTO(user);

                var userFound = await _userApplication.ValidateUserByEmail(u.Email);
                if (userFound == null)
                {
                    var created = await _userApplication.SaveAsync(u);
                    return Ok(created);
                }
                return BadRequest("User already exists");
            }
            catch (Exception ex )
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                return Ok(await _userApplication.GetAllAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
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

                if (await _userApplication.GetByIdAsync(id) != null) return BadRequest("The user doesn't exists");

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
