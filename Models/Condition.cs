using System.ComponentModel.DataAnnotations;

namespace ProjektDotNet.Models
{
    public class Condition
    {
        public int Id { get; set; }
        [MaxLength(10)]

        [Required]
        [Display(Name = "Skick")]
        public string? Condtition { get; set; }

        public virtual ICollection<Game>? Games { get; set; }
    }
}
