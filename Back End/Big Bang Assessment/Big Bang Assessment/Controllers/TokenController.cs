using Big_Bang_Assessment.Context;
using Big_Bang_Assessment.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Big_Bang_Assessment.Controllers
{
    [EnableCors("MyCorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly HospitalContext _context;

        private const string DoctorsRole = "Doctors";
        private const string PatientsRole = "Patients";
        private const string AdminRole = "Admin";

        public TokenController(IConfiguration configuration, HospitalContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("Doctors")]
        public async Task<IActionResult> PostDoctor(Doctor _userData)
        {
            if (_userData != null && !string.IsNullOrEmpty(_userData.DoctorName) && !string.IsNullOrEmpty(_userData.DoctorPwd))
            {
                var user = await _context.Doctors.FirstOrDefaultAsync(x =>
                    x.DoctorName == _userData.DoctorName && x.DoctorPwd == _userData.DoctorPwd);

                if (user != null)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("DoctorId", user.DoctorId.ToString()),
                        new Claim("DoctorName", user.DoctorName),
                        new Claim("DoctorPwd", user.DoctorPwd),
                        new Claim(ClaimTypes.Role, DoctorsRole)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:ValidIssuer"],
                        _configuration["Jwt:ValidAudience"],
                        claims,
                        expires: DateTime.UtcNow.AddDays(1),
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("Patients")]
        public async Task<IActionResult> PostPatient(Patient _userData)
        {
            if (_userData != null && !string.IsNullOrEmpty(_userData.PatientName) && !string.IsNullOrEmpty(_userData.Password))
            {
                var user = await _context.Patients.FirstOrDefaultAsync(x =>
                    x.PatientName == _userData.PatientName && x.Password == _userData.Password);

                if (user != null)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("PatientId", user.PatientId.ToString()),
                        new Claim("PatientName", user.PatientName),
                        new Claim("Password", user.Password),
                        new Claim(ClaimTypes.Role, PatientsRole)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:ValidIssuer"],
                        _configuration["Jwt:ValidAudience"],
                        claims,
                        expires: DateTime.UtcNow.AddDays(1),
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("Admin")]
        public async Task<IActionResult> PostAdmin(Admin staffData)
        {
            if (staffData != null && !string.IsNullOrEmpty(staffData.AdminEmail) && !string.IsNullOrEmpty(staffData.AdPassword))
            {
                if (staffData.AdminEmail == "rahulsk@gmail.com" && staffData.AdPassword == "Rahulsk@10")
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("AdminId", "1"), // Set the admin ID accordingly
                        new Claim("AdminEmail", staffData.AdminEmail),
                        new Claim("AdPassword", staffData.AdPassword),
                        new Claim(ClaimTypes.Role, AdminRole)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:ValidIssuer"],
                        _configuration["Jwt:ValidAudience"],
                        claims,
                        expires: DateTime.UtcNow.AddDays(1),
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
