using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Big_Bang_Assessment.Repo; // Update the namespace for your repository
using Big_Bang_Assessment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Big_Bang_Assessment.Models.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Big_Bang_Assessment.Controllers
{
    [EnableCors("MyCorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorRepo _doctorRepo;

        public DoctorsController(IDoctorRepo doctorRepo)
        {
            _doctorRepo = doctorRepo;
        }

        [Authorize(Roles ="Doctor,Admin,Patient")]
        // GET api/doctors
        [HttpGet]
        public ActionResult<IEnumerable<Doctor>> GetAllDoctors()
        {
            var doctors = _doctorRepo.GetDoctor();
            return Ok(doctors);
        }

        // GET api/doctors/{id}
        [HttpGet("get bt name")]
        public ActionResult<Doctor> GetDoctorById(string name)
        {
            var doctor = _doctorRepo.DoctorbyId(name);
            if (doctor == null)
            {
                return NotFound();
            }
            return Ok(doctor);
        }


        // POST api/doctors
        [HttpPost]
        public async Task<ActionResult<Doctor>> AddDoctor([FromForm] Doctor doctor, IFormFile imageFile)
        {
            try
            {
                var createdDoctor = await _doctorRepo.CreateDoctor(doctor, imageFile);
                return CreatedAtAction("GetDoctorById", new { id = createdDoctor.DoctorId }, createdDoctor);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [Authorize(Roles = "Doctor,Admin")]

        // PUT api/doctors/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Doctor>> UpdateDoctor(int id, [FromForm] Doctor doctor, IFormFile imageFile)
        {
            try
            {
                var updatedDoctor = await _doctorRepo.PutDoctor(id, doctor, imageFile);
                if (updatedDoctor == null)
                {
                    return NotFound();
                }

                return Ok(updatedDoctor);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [Authorize(Roles = "Doctor,Admin")]
        // DELETE api/doctors/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteDoctor(int id)
        {
            var existingDoctor = _doctorRepo.DeleteDoctor(id);
            if (existingDoctor == null)
            {
                return NotFound();
            }

            return NoContent();
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPut("Update status")]
        public async Task<ActionResult<UpdateStatus>> UpdateStatus(UpdateStatus status)
        {
            var result = await _doctorRepo.UpdateStatus(status);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Decline Doctor")]
        public async Task<ActionResult<UpdateStatus>> UpdateDeclineStatus(UpdateStatus status)
        {
            var result = await _doctorRepo.DeclineDoctorStatus(status);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Requested status")]
        public async Task<ActionResult<UpdateStatus>> GetRequestedDoctors()
        {
            var result = await _doctorRepo.RequestedDoctor();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("Accepted status")]
        public async Task<ActionResult<UpdateStatus>> GetAcceptedDoctors()
        {
            var result = await _doctorRepo.AcceptedDoctor();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }


    }
}
