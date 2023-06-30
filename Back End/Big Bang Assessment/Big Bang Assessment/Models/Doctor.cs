using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Big_Bang_Assessment.Models
{
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }

        public string? DoctorName { get; set; }

        public string? DoctorSpeciality { get; set; }

        public int DoctorAge { get; set; }

        public string? DoctorGender { get; set; }

        public string? DoctorEmail { get; set; }

        public string? DoctorPwd { get; set; }

        public int DoctorExperience { get; set; }

        public string? Description { get; set; }

        public int PhoneNumber { get; set; }

        public string? Status { get; set; }

        public string? DoctorImage { get; set; }

        public ICollection<Patient>? Patients { get; set; }
    }
}
