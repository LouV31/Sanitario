using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sanitario.Models
{
    public class Medicinale
    {
        [Key]
        public int IdMedicinale { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Descrizione { get; set; }
        [Required]
        public double Prezzo { get; set; }
        [ForeignKey("Cassetto")]
        public int IdCassetto { get; set; }


        public virtual Cassetto Cassetto { get; set; }
        public virtual ICollection<CuraPrescritta> CurePrescritte { get; set; }
        public virtual ICollection<DettagliVendita> DettagliVendite { get; set; }
    }
}
