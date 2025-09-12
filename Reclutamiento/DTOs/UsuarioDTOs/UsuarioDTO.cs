using System;

namespace Reclutamiento.DTOs.UsuarioDTOs;

public class UsuarioDTO
{
        public int Id { get; set; }
        public string? NombreCompleto { get; set; }
        public string? CorreoElectronico { get; set; }
        public string? Rol { get; set; }

}
