using System;

namespace Reclutamiento.Entidades
{
    public class RespuestaSolicitud
    {
        // Corresponde a 'id' en la base de datos.
        public int Id { get; set; }

        // Clave foránea que referencia a la tabla 'solicitudes'.
        public int IdSolicitud { get; set; }

        // Clave foránea que referencia a la tabla 'usuarios' para saber quién envió la respuesta.
        public int EnviadaPor { get; set; }

        // Corresponde a 'contenido_mensaje'.
        public string? ContenidoMensaje { get; set; }

        // Corresponde a 'fecha_envio'.
        public DateTime FechaEnvio { get; set; }
    }
}