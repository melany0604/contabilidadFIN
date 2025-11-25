using ContabilidadBackend.Core.DTOs;
using ContabilidadBackend.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContabilidadBackend.Core.Interfaces
{
    public interface IPresupuestoService
    {
        Task<Presupuesto> CrearPresupuestoAsync(PresupuestoDTO dto);

        // Corregido: Verifica (sin r al final para coincidir con el resto)
        Task<decimal> VerificaDisponibilidadAsync(string departamento, decimal montoSolicitado);

        Task<List<Presupuesto>> ObtenerPresupuestosAsync();

        Task<bool> ActualizarPresupuestoAsync(long idPresupuesto, decimal montoEjecutado);

        // CORREGIDO: Cambiamos Task a Task<Presupuesto> para que el controlador pueda recibir el objeto
        Task<Presupuesto> RestarSaldoAsync(long id, decimal monto);
    }
}
