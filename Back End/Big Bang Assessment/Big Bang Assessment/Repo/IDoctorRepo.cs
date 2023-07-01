using Big_Bang_Assessment.Models;
using Big_Bang_Assessment.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Big_Bang_Assessment.Repo
{
    public interface IDoctorRepo
    {
        IEnumerable<Doctor> GetAllDoctors();
        Doctor GetDoctorById(int doctorId);
        Task<DoctorDTO> AddDoctor([FromForm] Doctor doctor, IFormFile imageFile);
        Task<Doctor> UpdateDoctor(int doctorid, Doctor doctor, IFormFile imageFile);
        public void DeleteDoctor(int doctorId);
    }
}
