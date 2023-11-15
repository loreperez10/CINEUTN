using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web.Repos.Models
{
    [Table("CondicionDePago")]

    public class CondicionDePago
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
