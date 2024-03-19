using System.ComponentModel.DataAnnotations;

namespace Sanitario.Models
{
    public class AnimaleSmarrito
    {

        [Key]
        public int IdAnimaleSmarrito { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Tipologia { get; set; }
        public DateOnly DataRegistrazione { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        [Required]
        public DateOnly DataNascita { get; set; }
        [Required]
        public string ColoreMantello { get; set; }
        public string CodiceFiscaleProprietario { get; set; } = "";
        public string Microchip { get; set; } = "";
        public DateOnly DataInizioRicovero { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        [Required]
        public string Foto { get; set; }
    }
}
