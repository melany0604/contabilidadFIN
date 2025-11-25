using ContabilidadBackend.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContabilidadBackend.Core.Interfaces
{
    public interface IFacturacionMensualService
    {
        // El controlador llama a "GenerarFacturacionAsync"
        Task<FacturacionMensual> GenerarFacturacionAsync(int mes, int anio);

        // El controlador llama a "ObtenerPorMesAnioAsync"
        Task<FacturacionMensual> ObtenerPorMesAnioAsync(int mes, int anio);

        Task<List<FacturacionMensual>> ObtenerTodasAsync();
    }
}


