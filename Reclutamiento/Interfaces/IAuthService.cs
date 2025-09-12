using System;
using Reclutamiento.DTOs;

namespace Reclutamiento.Interfaces;

public interface IAuthService
{
    Task<string> AdminLogin(AdminLoginDTO dto);
    Task<string> GithubLogin(string githubId);

}
