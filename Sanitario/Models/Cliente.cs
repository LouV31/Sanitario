using System.ComponentModel.DataAnnotations;

namespace Sanitario.Models
{
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Cognome { get; set; }
        [Required]
        public string CodiceFiscale { get; set; }

        public virtual ICollection<Vendita> Vendite { get; set; }
    }
}
