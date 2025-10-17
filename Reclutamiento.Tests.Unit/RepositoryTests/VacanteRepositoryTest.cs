using Xunit;
using Microsoft.EntityFrameworkCore;
using Reclutamiento.Context;
using Reclutamiento.Entidades;
using Reclutamiento.Implementaciones;
using System;
using System.Linq;
using System.Threading.Tasks;

public class VacanteRepositoryTest
{
    private ReclutamientoContext GetDbContext(string databaseName)
    {
        
        var options = new DbContextOptionsBuilder<ReclutamientoContext>()
            .UseInMemoryDatabase(databaseName: databaseName)
            .Options;

        var context = new ReclutamientoContext(options);

        // Datos de prueba para la HU2
        context.Vacantes.AddRange(new[]
        {
            // Vacante Activa (Debe ser listada) - Para CP-HU02-1 y CP-HU02-2
            new Vacante { Id = 1, Titulo = "Desarrollador C# Senior", Descripcion = "Experiencia con .NET.", Ubicacion = "Remoto", EstaActiva = true, CreadaPor = 1, FechaCreacion = DateTime.Now },
            
            // Vacante Inactiva (NO debe ser listada) - Para CP-HU02-1
            new Vacante { Id = 2, Titulo = "Especialista en QA", Descripcion = "Experiencia con Selenium.", Ubicacion = "Oficina", EstaActiva = false, CreadaPor = 1, FechaCreacion = DateTime.Now.AddDays(-1) },
            
            // Otra Vacante Activa (Debe ser listada) - Para CP-HU02-1
            new Vacante { Id = 3, Titulo = "Diseñador UX/UI", Descripcion = "Diseño de interfaces.", Ubicacion = "Híbrido", EstaActiva = true, CreadaPor = 1, FechaCreacion = DateTime.Now.AddDays(-2) }
        });

        context.SaveChanges();
        return context;
    }

   
    /// CP-HU02-1: Lista de vacantes activas (positivo).
    /// Verifica que solo se devuelvan las vacantes con EstaActiva = true. 
    [Fact]
    public async Task GetAllActiveAsync_DebeRetornarSoloVacantesActivas()
    {
        // Arrange
        using var context = GetDbContext("TestActivas");
        var repository = new VacanteRepository(context);

        // Act
        var vacantesActivas = await repository.GetAllActiveAsync();

        // Assert
        // 1. Verificar la cantidad: Solo hay 2 activas.
        Assert.Equal(2, vacantesActivas.Count());

        // 2. Verificar que NINGUNA vacante inactiva esté presente.
        Assert.All(vacantesActivas, v => Assert.True(v.EstaActiva));

        // 3. Verificar que la vacante inactiva (ID 2) NO esté en la lista.
        Assert.DoesNotContain(vacantesActivas, v => v.Id == 2);
    }

    

    /// CP-HU02-2: Campos mínimos presentes en cada vacante listada.
    /// Verifica que los campos esenciales (Título, Descripción, Ubicación) no sean nulos.
    [Fact]
    public async Task GetAllActiveAsync_VacantesDebenTenerCamposMinimos()
    {
        // Arrange
        using var context = GetDbContext("TestCamposMinimos");
        var repository = new VacanteRepository(context);

        // Act
        var vacantesActivas = await repository.GetAllActiveAsync();

        // Assert
        Assert.All(vacantesActivas, v =>
        {
            // Verificar que los campos no sean nulos ni cadenas vacías
            Assert.False(string.IsNullOrWhiteSpace(v.Titulo));
            Assert.False(string.IsNullOrWhiteSpace(v.Descripcion));
            Assert.False(string.IsNullOrWhiteSpace(v.Ubicacion));
        });
    }
}