using System.ComponentModel.DataAnnotations;

namespace ProjektDotNet.Models
{
    public class Publisher
    {
        public int Id { get; set; }
        [MaxLength(25)]

        [Required]
        [Display(Name = "Utgivare")]
        public string? PublisherName { get; set; }

        public virtual ICollection<Game>? Games { get; set; }
    }
}
