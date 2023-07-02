using Big_Bang_Assessment.Models;
using Big_Bang_Assessment.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Big_Bang_Assessment.Repo
{
    public interface IDoctorRepo
    {
        public IEnumerable<Doctor> GetDoctor();
        public Doctor DoctorbyId(int Doctor_Id);
        Task<Doctor> CreateDoctor([FromForm] Doctor doctor, IFormFile imageFile);
        Task<Doctor> PutDoctor(int Doctor_id, Doctor doctor, IFormFile imageFile);
        public Doctor DeleteDoctor(int Doctor_Id);

        public Task<UpdateStatus> UpdateStatus(UpdateStatus status);

        public Task<ICollection<Doctor>> RequestedDoctor();
        public Task<ICollection<Doctor>> AcceptedDoctor();
    }
}
