using System;
using System.ComponentModel.DataAnnotations;

namespace Reclutamiento.DTOs.UsuarioDTOs;

public class UsuarioActualizarDTO
{
    [EmailAddress]

    
    public string? CorreoElectronico { get; set; }
    public string? NombreCompleto { get; set; }

    public string? Contrasena { get; set; }

}