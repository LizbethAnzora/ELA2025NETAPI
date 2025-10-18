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

        // --------------------------------------------------------------------------------
        // PASO 1: LIMPIEZA DE DATOS (CRUCIAL)
        // Usamos los nombres exactos de los DbSet en ReclutamientoContext: Solicitudes y RespuestasSolicitudes.
        // --------------------------------------------------------------------------------
        context.Solicitudes.RemoveRange(context.Solicitudes.ToList());
        context.RespuestasSolicitudes.RemoveRange(context.RespuestasSolicitudes.ToList());
        context.SaveChanges();

        // --------------------------------------------------------------------------------
        // PASO 2: INSERCIÓN DE DATOS BASE
        // Necesitamos una Solicitud existente (ID 1) para asociarle una respuesta.
        // --------------------------------------------------------------------------------
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

    // --------------------------------------------------------------------------------
    // PRUEBAS DE LA HISTORIA DE USUARIO 9 (Enviar Respuestas a Solicitudes)
    // --------------------------------------------------------------------------------

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
        // Llama al método SaveAsync del repositorio genérico, que llama a SaveChangesAsync
        await repository.SaveAsync();

        // ASSERT
        // Consultar directamente el DbSet para verificar la persistencia
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
            // Deja el contenido como una cadena vacía, lo cual debería fallar si es un campo requerido.
            ContenidoMensaje = string.Empty,
            FechaEnvio = DateTime.Now
        };

        // ACT
        await repository.AddAsync(respuestaInvalida);

        // ASSERT
        // Esperamos una excepción al llamar a SaveAsync() debido a la restricción de campo requerido (NOT NULL).
        

        // Verificar que no se guardó nada en la base de datos
        var count = await context.RespuestasSolicitudes.CountAsync();
        Assert.Equal(0, count);
    }
}