using System.ComponentModel.DataAnnotations;

namespace Sanitario.Models
{
    public class Animale
    {
        [Key]
        public int IdAnimale { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Tipologia { get; set; }
        [Required]
        public DateOnly DataRegistrazione { get; set; }
        [Required]
        public DateOnly DataNascita { get; set; }
        [Required]
        public string ColoreMantello { get; set; }

        public string CodiceFiscaleProprietario { get; set; } = "";

        public string Microchip { get; set; } = "";

        public virtual ICollection<Visita> Visite { get; set; }
    }
}
