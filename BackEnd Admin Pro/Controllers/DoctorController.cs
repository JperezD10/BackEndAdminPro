using Application;
using Application.Interfaces;
using Entities;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DoctorServices;

namespace BackEndAdminPro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IUserApplication _userApplication;
        private readonly IHospitalApplication _hospitalApplication;
        private readonly IApplication<Doctor> _doctorApplication;

        public DoctorController(IUserApplication userApplication, IHospitalApplication hospitalApplication, IApplication<Doctor> doctorApplication)
        {
            _userApplication = userApplication;
            _hospitalApplication = hospitalApplication;
            _doctorApplication = doctorApplication;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDoctor(DoctorDTO dto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var user = await _userApplication.GetByIdAsync(dto.User);
                if (user == null) return NotFound("User doesn't exists");

                var hospital = await _hospitalApplication.GetByIdAsync(dto.Hospital);
                if (hospital == null) return NotFound("Hospital doesn't exists");

                var doctor = DoctorService.convertDTO(dto, user, hospital);
                await _doctorApplication.SaveAsync(doctor);

                return Ok("Doctor created");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetAllDoctors()
        {
            try
            {
                return Ok(await _doctorApplication.GetAllAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
