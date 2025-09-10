using System;
using System.ComponentModel.DataAnnotations;

namespace Reclutamiento.DTOs.UsuarioDTOs;

public class UsuarioCrearDTO
{
     [Required]
        [EmailAddress]
        public string? CorreoElectronico { get; set; }

        [Required]
        public string? Contrasena { get; set; }

}
