using ContabilidadBackend.Core.DTOs;
using ContabilidadBackend.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContabilidadBackend.Core.Interfaces
{
    public interface ICierreVentasService
    {
        Task<CierreVentas> RegistrarCierreAsync(CierreVentasDTO dto);

        Task<List<CierreVentas>> ObtenerPorFechaAsync(DateTime fecha);

        Task<List<CierreVentas>> ObtenerPorMesAnioAsync(int mes, int anio);

        Task<List<CierreVentas>> ObtenerTodosAsync();
    }
}