using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DenunciasWebApp.Models
{
    public class Complaint
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public int StatusId { get; set; }

        [ForeignKey(nameof(StatusId))]
        public ComplaintStatus? Status { get; set; }

        [Required]
        public int CrimeId { get; set; }

        [ForeignKey(nameof(CrimeId))]
        public Crime? Crime { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [ForeignKey(nameof(UserId))]
        public ApplicationUser? User { get; set; }
    }
}
