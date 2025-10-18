using Xunit;
using Microsoft.EntityFrameworkCore;
using Reclutamiento.Context;
using Reclutamiento.Entidades;
using Reclutamiento.Implementaciones;
using System;
using System.Linq; 
using System.Threading.Tasks;
using System.Collections.Generic; 

public class UsuarioRepositoryTest
{
    private ReclutamientoContext GetDbContext()
    {
       
        var options = new DbContextOptionsBuilder<ReclutamientoContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new ReclutamientoContext(options);

       
        context.Database.EnsureCreated();
        context.Usuarios.RemoveRange(context.Usuarios.ToList());
        context.SaveChanges();

        
        context.Usuarios.Add(new Usuario
        {
            Id = 1,
            NombreCompleto = "Admin User",
            CorreoElectronico = "admin@reclutamiento.com",
            HashContraseña = "password_hash_valido",
            Rol = Rol.Admin,
            FechaCreacion = DateTime.Now
        });

        context.Usuarios.Add(new Usuario
        {
            Id = 2,
            NombreCompleto = "Admin Secundario",
            CorreoElectronico = "admin.secundario@reclutamiento.com",
            HashContraseña = "password_hash_secundario",
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
        await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            repository.GetByEmailAsync(emailInvalido, "Nombre Invalido"));
    }

    /// CP-HU10-1: Crear administrador.
    [Fact]
    public async Task AddAsync_CrearAdmin_DebeGuardarUsuarioConRolAdmin()
    {
        // ARRANGE
        using var context = GetDbContext();
        var repository = new UsuarioRepository(context);

        var nuevoAdmin = new Usuario
        {
            NombreCompleto = "Admin Creado HU10",
            CorreoElectronico = "creado.admin@test.com",
            HashContraseña = "password_newly_hashed_789",
            Rol = Rol.Admin,
            FechaCreacion = DateTime.Now
        };
        var initialCount = await context.Usuarios.CountAsync();

        // ACT
        await repository.AddAsync(nuevoAdmin);
        await repository.SaveAsync();

        // ASSERT
        var adminGuardado = await context.Usuarios
                                         .FirstOrDefaultAsync(u => u.CorreoElectronico == nuevoAdmin.CorreoElectronico);

        Assert.NotNull(adminGuardado);
        Assert.Equal(Rol.Admin, adminGuardado.Rol);
        Assert.Equal(initialCount + 1, await context.Usuarios.CountAsync());
    }

    /// CP-HU10-2: Editar administrador (nombre, email).
    [Fact]
    public async Task Update_EditarAdmin_DebeModificarNombreYEmail()
    {
        // ARRANGE
        using var context = GetDbContext();
        var repository = new UsuarioRepository(context);
        const int adminIdAEditar = 2;

        var usuarioAEditar = await context.Usuarios.FindAsync(adminIdAEditar);

        var nuevoNombre = "Admin Modificado HU10";
        var nuevoEmail = "admin.modificado.hu10@newtest.com";
        var hashOriginal = usuarioAEditar!.HashContraseña;

        usuarioAEditar.NombreCompleto = nuevoNombre;
        usuarioAEditar.CorreoElectronico = nuevoEmail;

        // ACT
        repository.Update(usuarioAEditar); 
        await repository.SaveAsync();

        // ASSERT
        context.Entry(usuarioAEditar).State = EntityState.Detached;
        var adminEditado = await context.Usuarios.FindAsync(adminIdAEditar);

        Assert.NotNull(adminEditado);
        Assert.Equal(nuevoNombre, adminEditado.NombreCompleto);
        Assert.Equal(nuevoEmail, adminEditado.CorreoElectronico);
        Assert.Equal(hashOriginal, adminEditado.HashContraseña); 
    }

    /// CP-HU10-3: Eliminar administrador.
    [Fact]
    public async Task Delete_EliminarAdmin_DebeRemoverUsuarioDeBD()
    {
        // ARRANGE
        using var context = GetDbContext();
        var repository = new UsuarioRepository(context);
        const int adminIdAEliminar = 2; 

        var usuarioAEliminar = await context.Usuarios.FindAsync(adminIdAEliminar);
        var initialCount = await context.Usuarios.CountAsync();

        // ACT
        repository.Delete(usuarioAEliminar!); 
        await repository.SaveAsync();

        // ASSERT
        var adminEliminado = await context.Usuarios.FindAsync(adminIdAEliminar);
        var totalUsuarios = await context.Usuarios.CountAsync();

        Assert.Null(adminEliminado);
        Assert.Equal(initialCount - 1, totalUsuarios);
    }
}