using Microsoft.EntityFrameworkCore;
using Reclutamiento.Context;
using Reclutamiento.Entidades;
using Reclutamiento.Interfaces;

namespace Reclutamiento.Implementaciones;

public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(ReclutamientoContext context) : base(context) { }

        public async Task<Usuario> GetByEmailAsync(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.CorreoElectronico == email) ?? throw new KeyNotFoundException($"Usuario with email {email} not found.");
        }

        public async Task<Usuario> GetByGithubIdAsync(long githubId)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.IdGithub == githubId) ?? throw new KeyNotFoundException($"Usuario with GitHub ID {githubId} not found.");
        }

}