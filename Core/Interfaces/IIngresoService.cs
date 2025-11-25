using ContabilidadBackend.Core.DTOs;
using ContabilidadBackend.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContabilidadBackend.Core.Interfaces
{
    public interface IIngresoService
    {
        Task<Ingreso> CrearIngresoAsync(IngresoDTO dto);

        // RENOMBRADO: De ObtenerIngresosAsync a ObtenerTodosAsync
        Task<List<Ingreso>> ObtenerTodosAsync();

        Task<List<Ingreso>> ObtenerIngresoPorConceptoAsync(string concepto);
    }
}