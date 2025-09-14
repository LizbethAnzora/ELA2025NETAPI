using Reclutamiento.Entidades;
using Microsoft.EntityFrameworkCore;


namespace Reclutamiento.Context;

public class ReclutamientoContext : DbContext
{
    public ReclutamientoContext(DbContextOptions<ReclutamientoContext> options)
            : base(options)
    {

    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Vacante> Vacantes { get; set; }
    public DbSet<Solicitud> Solicitudes { get; set; }
    public DbSet<RespuestaSolicitud> RespuestasSolicitudes { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>()
            .Property(u => u.Rol)
            .HasConversion<string>();

        modelBuilder.Entity<Solicitud>()
            .Property(s => s.Estado)
            .HasConversion<string>();
    }
}