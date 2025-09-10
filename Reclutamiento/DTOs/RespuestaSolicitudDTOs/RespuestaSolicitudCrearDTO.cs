using System;
using System.ComponentModel.DataAnnotations;

namespace Reclutamiento.DTOs.RespuestaSolicitudDTOs;

public class RespuestaSolicitudCrearDTO
{
[Required]
        public string? ContenidoMensaje { get; set; }

}
