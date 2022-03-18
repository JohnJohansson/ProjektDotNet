using System.ComponentModel.DataAnnotations;

namespace ProjektDotNet.Models
{
    public class GameConsole
    {
        public int Id { get; set; }
        [MaxLength(20)]

        [Required]
        [Display(Name = "Konsol")]
        public string? ConsoleName { get; set; }

        public virtual ICollection<Game>? Games { get; set; }
    }
}
