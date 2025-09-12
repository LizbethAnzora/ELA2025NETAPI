using System;

namespace Reclutamiento.Entidades;

// Se recomienda usar un 'enum' para los roles, ya que son valores fijos.
// Esto mejora la legibilidad y la seguridad del código, evitando errores de tipeo.
public enum Rol
{
    Solicitante,
    Admin
}

public class Usuario
{
    // Corresponde a 'id' en la base de datos.
    public int Id { get; set; }

    // Corresponde a 'id_github'. Usamos 'long' para un tipo BIGINT.
    public long? IdGithub { get; set; }

    public string? NombreCompleto { get; set; }

    // Corresponde a 'correo_electronico'.
    public string? CorreoElectronico { get; set; }

    // Corresponde a 'hash_contraseña'.
    public string? HashContraseña { get; set; }

    // Corresponde a 'rol'. Usamos el 'enum' que definimos arriba.
    public Rol Rol { get; set; }

    // Corresponde a 'fecha_creacion'.
    public DateTime FechaCreacion { get; set; }
}

