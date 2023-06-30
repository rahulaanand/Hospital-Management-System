using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Big_Bang_Assessment.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string AdminName { get; set; }


        [Required(ErrorMessage = "Email address is required.")]
        public string AdminEmail { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
