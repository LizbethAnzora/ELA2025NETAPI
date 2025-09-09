using System;

namespace Reclutamiento.Entidades
{
    public class Vacante
    {
        // Corresponde a 'id' en la base de datos.
        public int Id { get; set; }

        // Corresponde a 'titulo'.
        public string? Titulo { get; set; }

        // Corresponde a 'descripcion'.
        public string? Descripcion { get; set; }

        // Corresponde a 'requisitos'. Puede ser nulo en la base de datos.
        public string? Requisitos { get; set; }

        // Corresponde a 'ubicacion'.
        public string? Ubicacion { get; set; }

        // Corresponde a 'esta_activa'. El valor predeterminado es 'true'.
        public bool EstaActiva { get; set; }

        // Corresponde a 'creada_por'. Es la clave foránea que referencia a la tabla 'usuarios'.
        public int CreadaPor { get; set; }

        // Corresponde a 'fecha_creacion'.
        public DateTime FechaCreacion { get; set; }

        // Corresponde a 'fecha_actualizacion'.
        public DateTime FechaActualizacion { get; set; }
    }
}
