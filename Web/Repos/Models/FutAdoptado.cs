using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
        public string? ImagemPelicula { get; set; }

        [Display(Name = "Clasificación")]
        public int? Clasificacion { get; set; }

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
        [ForeignKey("CiudadRefId")]
        public virtual Vacuna? Vacuna { get; set; }

        [Display(Name = "Fecha Registro")]
        [Column(TypeName = "smalldatetime")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? FechaRegistro { get; set; }
    }
}
