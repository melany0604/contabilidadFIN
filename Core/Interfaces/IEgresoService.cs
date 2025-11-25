using ContabilidadBackend.Core.DTOs;
using ContabilidadBackend.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContabilidadBackend.Core.Interfaces
{
    public interface IEgresoService
    {
        Task<Egreso> CrearEgresoAsync(EgresoDTO dto);

        // --- CORREGIDO: Renombrado de ObtenerEgresosAsync a ObtenerTodosAsync para coincidir con el Controller
        Task<List<Egreso>> ObtenerTodosAsync();

        // --- AGREGADO: Faltaba este método que el Controller está usando
        Task<Egreso?> ObtenerPorIdAsync(long id);

        Task<bool> AutorizarEgresoAsync(long idEgreso, long idGerente);
    }
}