﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Web.Models;
using Web.Repos.Models;

namespace Web.ViewModels
{
    public class FutAdoptadoViewModels
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor, ingresar la descripción.")]
        [Display(Name = "Descripción")]
        public string? Descripcion { get; set; }

        [Display(Name = "Imagem Gato")]
        public IFormFile Imagem { get; set; }

        [Display(Name = "Imagen")]
        public string? ImagemGato { get; set; }

        [Display(Name = "Género")]
        public int? GeneroRefId { get; set; }

        [Display(Name = "Edad")]
        public int? EdadRefId { get; set; }

        [Display(Name = "Vacuna")]
        public int? VacunaRefId { get; set; }

        [Display(Name = "Enfermedad")]
        public int? EnfermedadRefId { get; set; }

        [Display(Name = "Fecha Registro")]
        [DataType(DataType.Date)]
        public DateTime? FechaRegistro { get; set; }
    }
}
