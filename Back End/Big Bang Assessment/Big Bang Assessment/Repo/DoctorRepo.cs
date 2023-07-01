using Big_Bang_Assessment.Context;
using Big_Bang_Assessment.Models;
using Big_Bang_Assessment.Models.DTOs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Big_Bang_Assessment.Repo
{
    public class DoctorRepo : IDoctorRepo
    {
        private readonly HospitalContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DoctorRepo(HospitalContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IEnumerable<Doctor> GetAllDoctors()
        {
            return _context.Doctors.Include(x => x.Patients).ToList();
        }

        public Doctor GetDoctorById(int doctorId)
        {
            return _context.Doctors.FirstOrDefault(d => d.DoctorId == doctorId);
        }

        public async Task<DoctorDTO> AddDoctor([FromForm] Doctor doctor, IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                throw new ArgumentException("Invalid file");
            }

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            doctor.DoctorImage = fileName;
            doctor.Status = "Requested";

            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();

            // Create a DoctorDTO object with the desired properties
            var doctorDto = new DoctorDTO
            {
                DoctorName = doctor.DoctorName,
                DoctorSpeciality = doctor.DoctorSpeciality,
                DoctorAge = doctor.DoctorAge,
                DoctorGender = doctor.DoctorGender,
                DoctorEmail = doctor.DoctorEmail,
                DoctorPwd = doctor.DoctorPwd,
                DoctorExperience = doctor.DoctorExperience,
                Description = doctor.Description,
                PhoneNumber = doctor.PhoneNumber,
                DoctorImage = doctor.DoctorImage
            };

            return doctorDto;
        }


        public async Task<Doctor> UpdateDoctor(int doctorId, Doctor doctor, IFormFile imageFile)
        {
            var existingDoctor = await _context.Doctors.FindAsync(doctorId);
            if (existingDoctor == null)
            {
                return null;
            }

            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                // Delete the old image file
                var oldFilePath = Path.Combine(uploadsFolder, existingDoctor.DoctorImage);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }

                existingDoctor.DoctorImage = fileName;
            }

            existingDoctor.DoctorName = doctor.DoctorName;
            existingDoctor.DoctorSpeciality = doctor.DoctorSpeciality;
            existingDoctor.DoctorAge = doctor.DoctorAge;
            existingDoctor.DoctorGender = doctor.DoctorGender;
            existingDoctor.DoctorEmail = doctor.DoctorEmail;
            existingDoctor.DoctorPwd = doctor.DoctorPwd;
            existingDoctor.DoctorExperience = doctor.DoctorExperience;
            existingDoctor.Description = doctor.Description;
            existingDoctor.PhoneNumber = doctor.PhoneNumber;

            await _context.SaveChangesAsync();

            return existingDoctor;
        }


        public void DeleteDoctor(int doctorId)
        {
            var doctor = _context.Doctors.FirstOrDefault(d => d.DoctorId == doctorId);
            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);
                _context.SaveChanges();
            }
        }
    }
}
