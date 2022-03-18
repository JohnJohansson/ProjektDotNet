using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektDotNet.Models
{
    public class Game
    {
        public int Id { get; set; }

        [MaxLength(50)]

        [Required]
        [Display(Name = "Namn på spelet")]
        public string? Name { get; set; }
        [Required]
        [Display(Name = "Pris")]
        public int Price { get; set; }

        [Display(Name = "Bild")]
        public string? ImgName { get; set; }    
        [NotMapped]
        public IFormFile? ImgFile { get; set; }

        [Display(Name = "Skick")]
        public Condition? Condition { get; set; }
        [Display(Name = "Konsol")]
        public GameConsole? GameConsole { get; set; }
        [Display(Name = "Utgivare")]
        public Publisher? Publisher { get; set; }
        [Display(Name = "Inehåll")]
        public GameContent? GameContent { get; set; }


        //Foregin key connecting to the table CIB
        [ForeignKey("Publisher")]
        [Display(Name = "Publicerad av")]
        public int PublisherFK { get; set; }
        //FK
        [ForeignKey("GameConsole")]
        [Display(Name = "Konsol")]
        public int GameConsoleFK { get; set; }
        //FK
        [ForeignKey("Condition")]
        [Display(Name = "Skick")]
        public int ConditionFK { get; set; }
        //FK
        [ForeignKey("GameContent")]
        [Display(Name = "Inehåll")]
        public int GameContentFK { get; set; }

    }
}
