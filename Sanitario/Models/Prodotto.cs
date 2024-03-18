using System.ComponentModel.DataAnnotations;

namespace Sanitario.Models
{
    public class Prodotto
    {
        [Key]
        public int IdProdotto { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Descrizione { get; set; }
        [Required]
        public double Prezzo { get; set; }

        public virtual ICollection<DettagliVendita> DettagliVendite { get; set; }

    }
}
