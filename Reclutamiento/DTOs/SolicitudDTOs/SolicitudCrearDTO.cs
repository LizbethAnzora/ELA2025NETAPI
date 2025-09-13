using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Reclutamiento.DTOs.SolicitudDTOs;

public class SolicitudCrearDTO
{
        [Required]
        public int IdVacante { get; set; }

        [Required]
        public string? NombreCompleto { get; set; }

        [Required]
        [EmailAddress]
        public string? CorreoElectronico { get; set; }
        
        [MaxLength(10)]
        public string? NumeroTelefono { get; set; }
        
        public string? Foto { get; set; }
        
        public string? CamposPersonalizados { get; set; }
}
