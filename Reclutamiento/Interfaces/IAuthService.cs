using System;
using Reclutamiento.DTOs;
using ReclutamientoFrontend.WebApp.Models.Dtos;

namespace Reclutamiento.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDto> AdminLogin(AdminLoginDTO dto);
    Task<string> GithubLogin(string githubId);
    

}
