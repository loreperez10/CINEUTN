using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Web.Repos.Models
{
    [Table("FutAdoptado")]
    public class FutAdoptado
    {

        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Display(Name = "Descripción")]
        [StringLength(50)]
        public string? Descripcion { get; set; }

        [Display(Name = "Imagen")]
        public string? ImagemGato { get; set; }

        [Display(Name = "Género")]
        public int? GeneroRefId { get; set; }
        [ForeignKey("GeneroRefId")]
        public virtual Genero? Genero { get; set; }

        [Display(Name = "Edad")]
        public int? EdadRefId { get; set; }
        [ForeignKey("EdadRefId")]
        public virtual Edad? Edad { get; set; }

        [Display(Name = "Vacuna")]
        public int? VacunaRefId { get; set; }
        [ForeignKey("VacunaRefId")]
        public virtual Vacuna? Vacuna { get; set; }

        [Display(Name = "Enfermedad")]
        public int? EnfermedadRefId { get; set; }
        [ForeignKey("EnfermedadRefId")]
        public virtual Enfermedad? Enfermedad { get; set; }

        [Display(Name = "Fecha Registro")]
        [Column(TypeName = "smalldatetime")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? FechaRegistro { get; set; }
    }
}
