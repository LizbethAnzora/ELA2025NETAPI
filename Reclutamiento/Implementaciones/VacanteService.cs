using System;
using Reclutamiento.DTOs.VacanteDTOs;
using Reclutamiento.Entidades;
using Reclutamiento.Interfaces;

namespace Reclutamiento.Implementaciones;

public class VacanteService : IVacanteService
{
    private readonly IVacanteRepository _vacanteRepository;

    public VacanteService(IVacanteRepository vacanteRepository)
    {
        _vacanteRepository = vacanteRepository;
    }

    public async Task<IEnumerable<VacantePublicaDTO>> GetPublicVacantesAsync()
    {
        var vacantes = await _vacanteRepository.GetAllActiveAsync();
        return vacantes.Select(v => new VacantePublicaDTO { Id = v.Id, Titulo = v.Titulo, Descripcion = v.Descripcion, Ubicacion = v.Ubicacion });
    }

    public async Task<IEnumerable<VacanteAdminDTO>> GetAllVacantesAsync()
    {
        var vacantes = await _vacanteRepository.GetAllAsync();
        return vacantes.Select(v => new VacanteAdminDTO { Id = v.Id, Titulo = v.Titulo, Descripcion = v.Descripcion, Requisitos = v.Requisitos, Ubicacion = v.Ubicacion, EstaActiva = v.EstaActiva, FechaCreacion = v.FechaCreacion, FechaActualizacion = v.FechaActualizacion });
    }

    public async Task<VacanteAdminDTO> GetVacanteByIdAsync(int id)
    {
        var vacante = await _vacanteRepository.GetByIdAsync(id);
        if (vacante == null) return null;
        return new VacanteAdminDTO { Id = vacante.Id, Titulo = vacante.Titulo, Descripcion = vacante.Descripcion, Requisitos = vacante.Requisitos, Ubicacion = vacante.Ubicacion, EstaActiva = vacante.EstaActiva, FechaCreacion = vacante.FechaCreacion, FechaActualizacion = vacante.FechaActualizacion };
    }

    public async Task<VacanteAdminDTO> CreateVacanteAsync(VacanteCrearDTO dto, int creadorId)
    {
        var vacante = new Vacante
        {
            Titulo = dto.Titulo,
            Descripcion = dto.Descripcion,
            Requisitos = dto.Requisitos,
            Ubicacion = dto.Ubicacion,
            EstaActiva = dto.EstaActiva,
            CreadaPor = creadorId
        };
        await _vacanteRepository.AddAsync(vacante);
        await _vacanteRepository.SaveAsync();
        return new VacanteAdminDTO { Id = vacante.Id, Titulo = vacante.Titulo, Descripcion = vacante.Descripcion, Requisitos = vacante.Requisitos, Ubicacion = vacante.Ubicacion, EstaActiva = vacante.EstaActiva, FechaCreacion = vacante.FechaCreacion, FechaActualizacion = vacante.FechaActualizacion };
    }

    public async Task UpdateVacanteAsync(int id, VacanteCrearDTO dto)
    {
        var vacante = await _vacanteRepository.GetByIdAsync(id);
        if (vacante == null) throw new Exception("Vacante no encontrada.");
        vacante.Titulo = dto.Titulo;
        vacante.Descripcion = dto.Descripcion;
        vacante.Requisitos = dto.Requisitos;
        vacante.Ubicacion = dto.Ubicacion;
        vacante.EstaActiva = dto.EstaActiva;
        _vacanteRepository.Update(vacante);
        await _vacanteRepository.SaveAsync();
    }

    public async Task DisableVacanteAsync(int id)
    {
        var vacante = await _vacanteRepository.GetByIdAsync(id);
        if (vacante == null) throw new Exception("Vacante no encontrada.");
        vacante.EstaActiva = false;
        _vacanteRepository.Update(vacante);
        await _vacanteRepository.SaveAsync();
    }
}