﻿using Big_Bang_Assessment.Context;
using Big_Bang_Assessment.Models;
using Big_Bang_Assessment.Models.DTOs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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

        public IEnumerable<Doctor> GetDoctor()
        {
            if(_context.Doctors!=null)
                return _context.Doctors.Include(x => x.Patients).ToList();
            throw new Exception();
        }

        public Doctor DoctorbyId(string name )
        {
            if(_context != null && _context.Doctors != null && _context.Patients != null)
            {
                return _context.Doctors.Include(x => x.Patients).FirstOrDefault(d => d.DoctorName == name);
            }
            throw new Exception();
        }

        public async Task<Doctor> CreateDoctor(Doctor doctor, IFormFile imageFile)
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

            if(_context.Doctors != null)
            {
                _context.Doctors.Add(doctor);
                await _context.SaveChangesAsync();
            }

            return doctor;
        }

        public async Task<Doctor> PutDoctor(int Doctor_id, Doctor doctor, IFormFile imageFile)
        {
            if(_context.Doctors != null)
            {
                var existingDoctor = await _context.Doctors.FindAsync(Doctor_id);
                if (existingDoctor == null)
                {
                    throw new Exception();
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

                    if(existingDoctor.DoctorImage != null)
                    {
                        var oldFilePath = Path.Combine(uploadsFolder, existingDoctor.DoctorImage);
                        if (File.Exists(oldFilePath))
                        {
                            File.Delete(oldFilePath);
                        }

                        existingDoctor.DoctorImage = fileName;
                    }
                   
                   
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
            throw new Exception();
           
        }

        public Doctor DeleteDoctor(int Doctor_Id)
        {
            if (_context.Doctors != null)
            {
                var doctor = _context.Doctors.FirstOrDefault(d => d.DoctorId == Doctor_Id);
                if (doctor != null)
                {
                    _context.Doctors.Remove(doctor);
                    _context.SaveChanges();
                    return doctor;
                }
            }
            throw new Exception();
        }

        public async Task<UpdateStatus> UpdateStatus(UpdateStatus status)
        {
            if (_context.Doctors != null)
            {
                var doc = await _context.Doctors.FirstOrDefaultAsync(s => s.DoctorId == status.id);
                if (doc != null)
                {
                    if (doc.Status == "Requested")
                    {
                        doc.Status = "Accepted";
                        await _context.SaveChangesAsync();
                        return status;
                    }
                    return status;

                }
            }
            throw new Exception();
        }

        public async Task<UpdateStatus> DeclineDoctorStatus(UpdateStatus status)
        {
            if (_context.Doctors != null)
            {
                var doc = await _context.Doctors.FirstOrDefaultAsync(s => s.DoctorId == status.id);
                if (doc != null)
                {
                    if (doc.Status == "Requested")
                    {
                        doc.Status = "Declined";
                        await _context.SaveChangesAsync();
                        return status;
                    }
                    return status;

                }
            }
            throw new Exception();
        }

        public async Task<ICollection<Doctor>> RequestedDoctor()
        {
            if (_context.Doctors != null)
            {
                var doc = await _context.Doctors.Where(s => s.Status == "Requested").ToListAsync();
                if (doc != null)
                {
                    return doc;
                }
            }
            
            throw new Exception();
        }

        public async Task<ICollection<Doctor>> AcceptedDoctor()
        {
            if (_context.Doctors != null )
            {
                var doc = await _context.Doctors.Where(s => s.Status == "Accepted")
               .Include(x => x.Patients)
               .ToListAsync();

                if (doc != null)
                {
                    return doc;
                }

            }

            throw new Exception();
        }

    }
}
