using System;
using System.Text.Json;

namespace Reclutamiento.DTOs.SolicitudDTOs;

public class SolicitudDTO
{
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdVacante { get; set; }
        public string? NombreCompleto { get; set; }
        public string? CorreoElectronico { get; set; }
        public string? NumeroTelefono { get; set; }
        public string? Foto { get; set; }
        public string? CamposPersonalizados { get; set; }
        public EstadoSolicitud Estado { get; set; }
        public bool RespuestaEnviada { get; set; }
        public DateTime FechaEnvio { get; set; }

}
