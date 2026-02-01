using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DenunciasWebApp.Models {
    public enum ComplaintStatus {
        Active,
        Processing,
        Done
    }

    public class Complaint{
        public int Id {get; set;}

        [Required]
        [StringLength(150)]
        public string Title {get; set;} = string.Empty;

        [Required]
        [StringLength(1000)] 
        public string Description {get; set;} = string.Empty;

        public DateTime CreatetAt {get; set;} = DateTime.UtcNow;

        public ComplaintStatus Status {get; set;} = ComplaintStatus.Active;

        [Required]
        public string UserId {get; set;} = string.Empty;

        [ForeignKey(nameof(UserId))]
        public ApplicationUser? User {get; set;}

    }
}