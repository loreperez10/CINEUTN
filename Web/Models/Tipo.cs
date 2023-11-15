using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    [Table("Tipo")]
    public class Tipo
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [StringLength(50)]
        public string Descripcion { get; set; } = null!;

        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaRegistro { get; set; }
    }
}
