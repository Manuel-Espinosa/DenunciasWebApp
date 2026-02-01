using System.ComponentModel.DataAnnotations;

namespace DenunciasWebApp.Models
{
    public class Crime
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}
