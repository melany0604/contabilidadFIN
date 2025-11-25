using ContabilidadBackend.Core.DTOs;
using ContabilidadBackend.Core.Entities;
using ContabilidadBackend.Core.Interfaces;
using ContabilidadBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ContabilidadBackend.Application.Services
{
    public class EgresoService : IEgresoService
    {
        private readonly ContabilidadContext _context;
        private readonly IPresupuestoService _presupuestoService;

        public EgresoService(ContabilidadContext context, IPresupuestoService presupuestoService)
        {
            _context = context;
            _presupuestoService = presupuestoService;
        }

        // RENOMBRADO: De RegistrarEgresoAsync a CrearEgresoAsync
        public async Task<Egreso> CrearEgresoAsync(EgresoDTO egresoDto)
        {
            var presupuestos = await _presupuestoService.ObtenerPresupuestosAsync();
            var presupuesto = presupuestos.FirstOrDefault(p => p.Departamento == egresoDto.Departamento);

            // Validamos que exista presupuesto y tenga saldo (Nota: IdPresupuesto viene en el DTO o se busca)
            long presupuestoId = presupuesto?.Id ?? egresoDto.IdPresupuesto;

            if (presupuesto != null && presupuesto.Saldo < egresoDto.Monto)
            {
                throw new InvalidOperationException("Presupuesto insuficiente para este egreso");
            }

            var egreso = new Egreso
            {
                Concepto = egresoDto.Concepto,
                Monto = egresoDto.Monto,
                Departamento = egresoDto.Departamento,
                TipoEgreso = egresoDto.TipoEgreso,
                IdProveedor = egresoDto.IdProveedor,
                IdPresupuesto = presupuestoId,
                Estado = "Pendiente", // Se crea como pendiente
                FechaRegistro = DateTime.UtcNow
            };

            _context.Egresos.Add(egreso);
            await _context.SaveChangesAsync();

            // Descontar saldo
            if (presupuesto != null)
            {
                await _presupuestoService.RestarSaldoAsync(presupuesto.Id, egresoDto.Monto);
            }

            return egreso;
        }

        // RENOMBRADO: De ObtenerEgresosAsync a ObtenerTodosAsync
        public async Task<List<Egreso>> ObtenerTodosAsync()
        {
            return await _context.Egresos.ToListAsync();
        }

        // AGREGADO: Implementación faltante
        public async Task<Egreso> ObtenerPorIdAsync(long id)
        {
            return await _context.Egresos.FindAsync(id);
        }

        // AGREGADO: Implementación faltante requerida por la interfaz
        public async Task<bool> AutorizarEgresoAsync(long idEgreso, long idGerente)
        {
            var egreso = await _context.Egresos.FindAsync(idEgreso);
            if (egreso == null) return false;

            egreso.Estado = "Autorizado";
            egreso.IdAprobacionGerente = idGerente;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<decimal> ObtenerTotalEgresosAsync()
        {
            return await _context.Egresos.SumAsync(e => e.Monto);
        }
    }
}