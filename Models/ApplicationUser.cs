using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DenunciasWebApp.Models
{
    public enum Sex
    {
        Masculino,
        Femenino,
        Otro
    }

    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(18, MinimumLength = 18)]
        public string CURP { get; set; } = string.Empty;

        [Required]
        public Sex Sex { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public DateTime BirthDate { get; set; }
    }
}
