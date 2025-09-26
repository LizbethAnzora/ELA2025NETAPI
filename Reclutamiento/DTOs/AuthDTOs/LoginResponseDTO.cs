using System.ComponentModel.DataAnnotations;
using Reclutamiento.Entidades;

namespace ReclutamientoFrontend.WebApp.Models.Dtos
{
    public class LoginResponseDto
    {
        public int Id { get; set; }


        public string? NombreCompleto { get; set; }

        public string? CorreoElectronico { get; set; }

        public Rol Rol { get; set; } // Ejemplo: "Admin" o "Solicitante"
        
         public string Token { get; set; }

    }
}