using System;

namespace Reclutamiento.DTOs.RespuestaSolicitudDTOs;

public class RespuestaSolicitudDTO
{
    public int Id { get; set; }
        public int IdSolicitud { get; set; }
        public int EnviadaPor { get; set; }
        public string? ContenidoMensaje { get; set; }
        public DateTime FechaEnvio { get; set; }

}
