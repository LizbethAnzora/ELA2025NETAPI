using System;
using Reclutamiento.Entidades;

namespace Reclutamiento.Interfaces;

public interface IUsuarioRepository : IRepository<Usuario>
{
    Task<Usuario> GetByEmailAsync(string email);
    Task<Usuario> GetByGithubIdAsync(long githubId);

}
