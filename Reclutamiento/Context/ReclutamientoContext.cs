using System;
using Microsoft.EntityFrameworkCore;
using Reclutamiento.Entidades;

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
        

}
