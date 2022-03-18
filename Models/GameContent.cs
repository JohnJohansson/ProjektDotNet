using System.ComponentModel.DataAnnotations;

namespace ProjektDotNet.Models
{
    public class GameContent
    {
        public int Id { get; set; }
        [MaxLength(50)]

        [Required]
        [Display(Name = "Inehåll")]
        public string? Content { get; set; }
        public virtual ICollection<Game>? Games { get; set; }
    }
}
