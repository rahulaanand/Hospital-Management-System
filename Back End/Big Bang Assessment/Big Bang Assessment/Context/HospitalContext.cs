using Big_Bang_Assessment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Big_Bang_Assessment.Context
{
    public class HospitalContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }

        public DbSet<Appointment> Appointments { get; set; }
        public HospitalContext(DbContextOptions<HospitalContext> options) : base(options)
        {

        }
    }
}
