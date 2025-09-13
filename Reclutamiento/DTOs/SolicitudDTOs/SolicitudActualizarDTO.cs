using System;
using System.Text.Json;

namespace Reclutamiento.DTOs.SolicitudDTOs;

public class SolicitudActualizarDTO
{
        public string? NombreCompleto { get; set; }
        public string? CorreoElectronico { get; set; }
        public string? NumeroTelefono { get; set; }
        public string? Foto { get; set; }
        public string? CamposPersonalizados { get; set; }
}
