using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace Web.Repos.Models
{
        [Table("Edad")]
        public class Edad
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
