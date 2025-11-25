using ContabilidadBackend.Core.DTOs;
using ContabilidadBackend.Core.Entities;
using ContabilidadBackend.Core.Interfaces;
using ContabilidadBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContabilidadBackend.Application.Services
{
    public class PresupuestoService : IPresupuestoService
    {
        private readonly ContabilidadContext _context;

        public PresupuestoService(ContabilidadContext context)
        {
            _context = context;
        }

        public async Task<Presupuesto> CrearPresupuestoAsync(PresupuestoDTO presupuestoDto)
        {
            var presupuesto = new Presupuesto
            {
                Departamento = presupuestoDto.Departamento,
                MontoTotal = presupuestoDto.MontoTotal,
                Saldo = presupuestoDto.MontoTotal,
                Mes = presupuestoDto.Mes,
                // CORREGIDO: Usar Anio en lugar de Año
                Anio = presupuestoDto.Anio,
                FechaCreacion = DateTime.UtcNow
            };

            _context.Presupuestos.Add(presupuesto);
            await _context.SaveChangesAsync();
            return presupuesto;
        }

        // CORREGIDO: Recibe long y devuelve Presupuesto
        public async Task<Presupuesto> RestarSaldoAsync(long id, decimal monto)
        {
            var presupuesto = await _context.Presupuestos.FindAsync(id);
            if (presupuesto == null)
                throw new KeyNotFoundException("Presupuesto no encontrado");

            if (presupuesto.Saldo - monto < 0)
                throw new InvalidOperationException("Saldo insuficiente en presupuesto");

            presupuesto.Saldo -= monto;
            presupuesto.MontoEjecutado += monto; // Buena práctica: actualizar lo ejecutado también

            await _context.SaveChangesAsync();

            // Retornamos el objeto para que el Controller no de error
            return presupuesto;
        }

        public async Task<List<Presupuesto>> ObtenerPresupuestosAsync()
        {
            return await _context.Presupuestos.ToListAsync();
        }

        // CORREGIDO: Renombrado de Verificar... a Verifica... para cumplir con la interfaz
        public async Task<decimal> VerificaDisponibilidadAsync(string departamento, decimal monto)
        {
            var presupuesto = await _context.Presupuestos
                .FirstOrDefaultAsync(p => p.Departamento == departamento);

            if (presupuesto == null)
                return 0; // O lanzar excepción según lógica

            // Si hay saldo, devolvemos el saldo restante, si no -1 (o false)
            return presupuesto.Saldo >= monto ? presupuesto.Saldo : -1;
        }

        // AGREGADO: Implementación del método que faltaba
        public async Task<bool> ActualizarPresupuestoAsync(long idPresupuesto, decimal montoEjecutado)
        {
            var presupuesto = await _context.Presupuestos.FindAsync(idPresupuesto);
            if (presupuesto == null) return false;

            presupuesto.MontoEjecutado += montoEjecutado;
            presupuesto.Saldo = presupuesto.MontoTotal - presupuesto.MontoEjecutado;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}

