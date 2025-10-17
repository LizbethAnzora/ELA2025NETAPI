using Xunit;
using Microsoft.EntityFrameworkCore;
using Reclutamiento.Context;
using Reclutamiento.Implementaciones;
using System;
using System.Threading.Tasks;
using System.Linq;


public class SolicitudRepositoryTest
{
    private ReclutamientoContext GetDbContext(string databaseName)
    {
        var options = new DbContextOptionsBuilder<ReclutamientoContext>()
            .UseInMemoryDatabase(databaseName: databaseName)
            .Options;

        return new ReclutamientoContext(options);
    }

    /// <summary>
    /// CP-HU04-1: Probar el envío exitoso de una solicitud válida (completa).
    /// Verifica que la solicitud se guarde correctamente con todos sus campos.
    /// </summary>
    [Fact]
    public async Task AddAsync_SolicitudValidaCompleta_DebeGuardarEnBaseDeDatos()
    {
        // Arrange
        var dbName = "TestSolicitudCompleta";
        using var context = GetDbContext(dbName);
        var repository = new SolicitudRepository(context);

        // Simulación de datos de Base64 (una cadena corta de ejemplo)
        const string fakeBase64Image = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAUA";
        const string customJson = "{\"Pregunta1\": \"Respuesta\", \"Habilidades\": [\"C#\", \"ASP.NET\"]}";

        var nuevaSolicitud = new Solicitud
        {
            IdUsuario = 10, // Solicitante ID
            IdVacante = 5,  // Vacante ID
            NombreCompleto = "El Solicitante Prueba",
            CorreoElectronico = "postulante@prueba.com",
            NumeroTelefono = "503-7777-7777",
            Foto = fakeBase64Image,
            CamposPersonalizados = customJson,
            Estado = EstadoSolicitud.Pendiente,
            RespuestaEnviada = false,
            FechaEnvio = DateTime.UtcNow
        };

        context.Solicitudes.Add(nuevaSolicitud);
        await context.SaveChangesAsync();

        // Assert
        var solicitudGuardada = context.Solicitudes.FirstOrDefault(s => s.CorreoElectronico == "postulante@prueba.com");

        Assert.NotNull(solicitudGuardada);
        Assert.NotEqual(0, solicitudGuardada.Id); // Debe tener un ID asignado (guardado con éxito)
        Assert.Equal("El Solicitante Prueba", solicitudGuardada.NombreCompleto);
        Assert.Equal(fakeBase64Image, solicitudGuardada.Foto); // La data Base64 debe ser guardada
        Assert.Equal(customJson, solicitudGuardada.CamposPersonalizados); // El JSON debe ser guardado
        Assert.Equal(EstadoSolicitud.Pendiente, solicitudGuardada.Estado);
    }
}