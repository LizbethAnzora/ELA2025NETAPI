using System;
using System.ComponentModel.DataAnnotations;

namespace Reclutamiento.DTOs.VacanteDTOs;

public class VacanteCrearDTO
{
    [Required]
        [MaxLength(100)]
        public string? Titulo { get; set; }
        
        [Required]
        public string? Descripcion { get; set; }
        
        public string? Requisitos { get; set; }
        
        [MaxLength(100)]
        public string? Ubicacion { get; set; }
        
        public bool EstaActiva { get; set; }

}
