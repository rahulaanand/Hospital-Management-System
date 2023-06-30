using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Big_Bang_Assessment.Repo; // Update the namespace for your repository
using Big_Bang_Assessment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Big_Bang_Assessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorRepo _doctorRepo;

        public DoctorsController(IDoctorRepo doctorRepo)
        {
            _doctorRepo = doctorRepo;
        }

        // GET api/doctors
        [HttpGet]
        public ActionResult<IEnumerable<Doctor>> GetDoctors()
        {
            var doctors = _doctorRepo.GetAllDoctors();
            return Ok(doctors);
        }

        // GET api/doctors/{id}
        [HttpGet("{id}")]
        public ActionResult<Doctor> GetDoctor(int id)
        {
            var doctor = _doctorRepo.GetDoctorById(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return Ok(doctor);
        }

        // POST api/doctors
        [HttpPost]
        public async Task<ActionResult<Doctor>> Post([FromForm] Doctor doctor, IFormFile imageFile)
        {
            try
            {
                var createdDoctor = await _doctorRepo.AddDoctor(doctor, imageFile);
                return CreatedAtAction(nameof(GetDoctor), new { id = createdDoctor.DoctorId }, createdDoctor);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        // PUT api/doctors/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Doctor>> Put(int id, [FromForm] Doctor doctor, IFormFile imageFile)
        {
            try
            {
                var updatedDoctor = await _doctorRepo.UpdateDoctor(id, doctor, imageFile);
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

        // DELETE api/doctors/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteDoctor(int id)
        {
            var existingDoctor = _doctorRepo.GetDoctorById(id);
            if (existingDoctor == null)
            {
                return NotFound();
            }

            _doctorRepo.DeleteDoctor(id);

            return NoContent();
        }
    }
}
