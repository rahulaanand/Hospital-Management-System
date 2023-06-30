using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Big_Bang_Assessment.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        public string? AdminName { get; set; }
        public string? AdminEmail { get; set; }
        public string? Password { get; set; }
    }
}
