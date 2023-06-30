﻿using System.ComponentModel.DataAnnotations;

namespace Big_Bang_Assessment.Models
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }
        public string? PatientName { get; set; }
        public int Age { get; set; }
        public string? Gender { get; set; }
        public string? EmailId { get; set; }
        public string? Password { get; set; }
        public int Contact { get; set; }
        public Doctor? Doctors { get; set; }
    }
}