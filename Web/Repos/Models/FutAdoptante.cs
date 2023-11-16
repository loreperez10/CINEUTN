using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Repos.Models
{

    [Table("FutAdoptante")]
    public class FutAdoptante
    {

        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [StringLength(50)]
        public string NombreyApellido { get; set; } = null!;

        [StringLength(50)]
        public string Contacto { get; set; } = null!;

        [StringLength(50)]
        public string Interes { get; set; } = null!;

        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaRegistro { get; set; }
    }
}
