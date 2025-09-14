using System;
using System.ComponentModel.DataAnnotations;

namespace Reclutamiento.DTOs;

public class AdminLoginDTO
{
    [Required]
    public string? NombreCompleto { get; set; }

    [Required]
    [EmailAddress]
    public string? CorreoElectronico { get; set; }

    [Required]
    public string? Contrasena { get; set; }
        

}