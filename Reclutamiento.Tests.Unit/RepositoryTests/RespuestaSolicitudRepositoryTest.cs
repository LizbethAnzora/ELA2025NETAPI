using Xunit;
using Microsoft.EntityFrameworkCore;
using Reclutamiento.Context;
using Reclutamiento.Entidades;
using Reclutamiento.Implementaciones;
using System;
using System.Linq;
using System.Threading.Tasks;

public class RespuestaSolicitudRepositoryTest
{
    private ReclutamientoContext GetDbContext(string databaseName)
    {
        var options = new DbContextOptionsBuilder<ReclutamientoContext>()
            .UseInMemoryDatabase(databaseName: databaseName)
            .Options;

        var context = new ReclutamientoContext(options);
        context.Database.EnsureCreated();

        context.Solicitudes.RemoveRange(context.Solicitudes.ToList());
        context.RespuestasSolicitudes.RemoveRange(context.RespuestasSolicitudes.ToList());
        context.SaveChanges();

       
        if (!context.Solicitudes.Any())
        {
            context.Solicitudes.Add(new Solicitud
            {
                Id = 1,
                IdVacante = 1,
                NombreCompleto = "Candidato Base",
                CorreoElectronico = "candidato@test.com",
                NumeroTelefono = "55512345",
                FechaEnvio = DateTime.Now
            });
            context.SaveChanges();
        }
        return context;
    }

  
    /// CP-HU09-1: Crear respuesta para solicitud (positivo).
    /// Verifica que una respuesta válida se persista usando AddAsync y SaveAsync.
    [Fact]
    public async Task AddAsync_RespuestaValida_DebeGuardarAsociadaASolicitud()
    {
        // ARRANGE
        using var context = GetDbContext("TestCrearRespuesta_CP1_" + Guid.NewGuid());
        var repository = new RespuestaSolicitudRepository(context);
        const int solicitudIdExistente = 1;
        const int adminId = 10;
        var mensajeEsperado = "Su perfil ha pasado a la fase de entrevista.";

        var nuevaRespuesta = new RespuestaSolicitud
        {
            IdSolicitud = solicitudIdExistente,
            EnviadaPor = adminId,
            ContenidoMensaje = mensajeEsperado,
            FechaEnvio = DateTime.Now
        };

        // ACT
        await repository.AddAsync(nuevaRespuesta);
        
        await repository.SaveAsync();

        // ASSERT
        var respuestaGuardada = await context.RespuestasSolicitudes
                                            .FirstOrDefaultAsync(r => r.ContenidoMensaje == mensajeEsperado);

        Assert.NotNull(respuestaGuardada);
        Assert.True(respuestaGuardada.Id > 0, "La respuesta debe tener un ID asignado.");
        Assert.Equal(solicitudIdExistente, respuestaGuardada.IdSolicitud);
        Assert.Equal(mensajeEsperado, respuestaGuardada.ContenidoMensaje);
    }

    /// CP-HU09-2: Enviar respuesta sin contenido (negativo).
    /// Verifica que la operación falle si el campo ContenidoMensaje es NOT NULL.
    [Fact]
    public async Task AddAsync_RespuestaSinContenido_DebeFallarSiEsRequerido()
    {
        // ARRANGE
        using var context = GetDbContext("TestCrearRespuesta_CP2_" + Guid.NewGuid());
        var repository = new RespuestaSolicitudRepository(context);

        var respuestaInvalida = new RespuestaSolicitud
        {
            IdSolicitud = 1,
            EnviadaPor = 10,
            ContenidoMensaje = string.Empty,
            FechaEnvio = DateTime.Now
        };

        // ACT
        await repository.AddAsync(respuestaInvalida);

        // ASSERT
        var count = await context.RespuestasSolicitudes.CountAsync();
        Assert.Equal(0, count);
    }
}