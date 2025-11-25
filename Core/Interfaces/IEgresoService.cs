using ContabilidadBackend.Core.DTOs;
using ContabilidadBackend.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContabilidadBackend.Core.Interfaces
{
    public interface IEgresoService
    {
        // Coincide con el controlador
        Task<Egreso> CrearEgresoAsync(EgresoDTO dto);

        // Cambiado de ObtenerEgresosAsync a ObtenerTodosAsync para coincidir con el controlador
        Task<List<Egreso>> ObtenerTodosAsync();

        // Agregado porque el controlador lo usa
        Task<Egreso> ObtenerPorIdAsync(long id);

        // Este ya estaba, pero faltaba implementarlo en el servicio
        Task<bool> AutorizarEgresoAsync(long idEgreso, long idGerente);
    }
}