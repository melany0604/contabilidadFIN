using ContabilidadBackend.Core.DTOs;
using ContabilidadBackend.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContabilidadBackend.Core.Interfaces
{
    public interface IPresupuestoService
    {
        Task<Presupuesto> CrearPresupuestoAsync(PresupuestoDTO dto);
        Task<decimal> VerificaDisponibilidadAsync(string departamento, decimal montoSolicitado);
        Task<List<Presupuesto>> ObtenerPresupuestosAsync();
        Task<bool> ActualizarPresupuestoAsync(long idPresupuesto, decimal montoEjecutado);
    }
}
