using System.ComponentModel.DataAnnotations.Schema;

namespace Big_Bang_Assessment.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public string? AppointmentDate { get; set; }
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patient? Patient { get; set; }
        public int DoctorId { get; set; }
        [ForeignKey("Doctor_id")]
        public Doctor? Doctors { get; set; }
        public bool IsConfirmed { get; set; }
      
    }
}
