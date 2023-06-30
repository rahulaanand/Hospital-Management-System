namespace Big_Bang_Assessment.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public string? AppointmentDate { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int DoctorId { get; set; }
        public bool IsConfirmed { get; set; }
        public Doctor Doctor { get; set; }

        public ICollection<Patient> Patients { get; set; }
    }
}
