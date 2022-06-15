using Application.Interfaces;
using Entities.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.HospitalServices;

namespace BackEndAdminPro.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalController : ControllerBase
    {
        private readonly IHospitalApplication _hospitalApplication;
        private readonly IUserApplication _userApplication;

        public HospitalController(IHospitalApplication hospitalApplication, IUserApplication userApplication)
        {
            _hospitalApplication = hospitalApplication;
            _userApplication = userApplication;
        }

        [HttpGet]
        public async Task<IActionResult> GetHospitals()
        {
            return Ok(await _hospitalApplication.GetAllAsync());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditHospitals(int id, [FromBody] HospitalDTO dto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var userFound = await _userApplication.GetByIdAsync(dto.User);
                var hospitalFound = await _hospitalApplication.GetByIdAsync(id);

                if (userFound == null || hospitalFound == null) return NotFound("user or hospital not found");

                var hospital = HospitalService.convertDTO(dto, userFound);

                hospitalFound = hospital;

                await _hospitalApplication.SaveAsync(hospitalFound);

                return Ok("Hospital updated");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHospital(int id)
        {
            try
            {
                if (id == 0) return BadRequest("Incorrect Id");

                if (await _hospitalApplication.GetByIdAsync(id) != null) return BadRequest("The hospital doesn't exists");

                await _hospitalApplication.DeleteAsync(id);
                return Ok("Hospital deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostHospital(HospitalDTO dto)
        {
            try
            {
                var hospitalExists = (await _hospitalApplication.GetAllAsync()).Where(h => h.Name == dto.Name).Any();
                if (hospitalExists) return NotFound("Hospital already exists");

                var user = await _userApplication.GetByIdAsync(dto.User);
                if (user == null) return NotFound("User doesn't exists");

                var Hospital = HospitalService.convertDTO(dto, user);

                await _hospitalApplication.SaveAsync(Hospital);

                return Ok("Hospital created");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
