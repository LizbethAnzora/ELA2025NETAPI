using Xunit;
using Microsoft.EntityFrameworkCore;
using Reclutamiento.Context;
using Reclutamiento.Entidades;
using Reclutamiento.Implementaciones;
using System;
using System.Threading.Tasks;


public class UsuarioRepositoryTest
{
    
    private ReclutamientoContext GetDbContext()
    {
       
        var options = new DbContextOptionsBuilder<ReclutamientoContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new ReclutamientoContext(options);

        // Datos de prueba: Usuario Administrador (CP-HU01-1)
        context.Usuarios.Add(new Usuario
        {
            Id = 1,
            NombreCompleto = "Admin User",
            CorreoElectronico = "admin@reclutamiento.com",
            HashContraseña = "password_hash_valido",
            Rol = Rol.Admin,
            FechaCreacion = DateTime.Now
        });

        context.SaveChanges();
        return context;
    }

   
    /// CP-HU01-1: Login con credenciales válidas (Admin).
    /// Verifica que un usuario Admin pueda ser encontrado por su correo.
    [Fact]
    public async Task GetByEmailAsync_AdminValido_DebeRetornarUsuarioAdmin()
    {
        // Arrange
        using var context = GetDbContext();
        var repository = new UsuarioRepository(context);
        string emailValido = "admin@reclutamiento.com";

        // Act
        var usuarioEncontrado = await repository.GetByEmailAsync(emailValido, "Admin User");

        // Assert
        Assert.NotNull(usuarioEncontrado);
        Assert.Equal(emailValido, usuarioEncontrado.CorreoElectronico);
        Assert.Equal(Rol.Admin, usuarioEncontrado.Rol);
    }

   
    /// CP-HU01-2: Login con credenciales inválidas (simulado).
    /// Verifica que se lance una excepción cuando el email NO es encontrado.
    [Fact]
    public async Task GetByEmailAsync_EmailInvalido_DebeLanzarKeyNotFoundException()
    {
        // Arrange
        using var context = GetDbContext();
        var repository = new UsuarioRepository(context);
        string emailInvalido = "usuario_invalido@reclutamiento.com";

        // Act & Assert
        // Esperamos que el método lance KeyNotFoundException
        await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            repository.GetByEmailAsync(emailInvalido, "Nombre Invalido"));
    }
}