using System;

// Un 'enum' para el estado de la solicitud. Esto hace que sea más fácil
// manejar los diferentes estados de forma segura y legible en el código.
public enum EstadoSolicitud
{
    Pendiente,
    Revisada,
    Aceptada,
    Rechazada
}

public class Solicitud
{
    // Corresponde a 'id' en la base de datos.
    public int Id { get; set; }

    // Clave foránea que referencia a la tabla 'usuarios'.
    public int IdUsuario { get; set; }

    // Clave foránea que referencia a la tabla 'vacantes'.
    public int IdVacante { get; set; }

    // Corresponde a 'nombre_completo'.
    public string? NombreCompleto { get; set; }

    // Corresponde a 'correo_electronico'.
    public string? CorreoElectronico { get; set; }

    // Corresponde a 'numero_telefono'. Puede ser nulo.
    public string? NumeroTelefono { get; set; }

    // Corresponde a 'foto'. Puede ser nulo.
    public string? Foto { get; set; }

    // Corresponde a 'campos_personalizados', que es JSON.
    // Lo manejaremos como una cadena de texto para luego deserializarlo
    // en la lógica de la aplicación según sea necesario.
    public string? CamposPersonalizados { get; set; }

    // Corresponde a 'estado'. Usamos el 'enum' definido arriba.
    public EstadoSolicitud Estado { get; set; }

    // Corresponde a 'respuesta_enviada'.
    public bool RespuestaEnviada { get; set; }

    // Corresponde a 'fecha_envio'.
    public DateTime FechaEnvio { get; set; }
}