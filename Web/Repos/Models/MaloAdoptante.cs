using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Web.Repos.Models
{
    [Table("MaloAdoptante")]
    public class MaloAdoptante
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [StringLength(50)]
        public string NombreyApellido { get; set; } = null!;

        [StringLength(50)]
        public string Direccion { get; set; } = null!;

        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaRegistro { get; set; }
    }
}
