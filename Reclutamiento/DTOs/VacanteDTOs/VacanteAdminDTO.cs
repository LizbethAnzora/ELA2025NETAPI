using System;

namespace Reclutamiento.DTOs.VacanteDTOs;

public class VacanteAdminDTO
{
    public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public string? Requisitos { get; set; }
        public string? Ubicacion { get; set; }
        public bool EstaActiva { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }

}
