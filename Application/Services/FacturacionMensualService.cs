using ContabilidadBackend.Core.Entities;
using ContabilidadBackend.Core.Interfaces;
using ContabilidadBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using ContabilidadBackend.Core.Entities;
using ContabilidadBackend.Core.Interfaces;
using ContabilidadBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContabilidadBackend.Application.Services
{
    public class FacturacionMensualService : IFacturacionMensualService
    {
        private readonly ContabilidadContext _context;

        public FacturacionMensualService(ContabilidadContext context)
        {
            _context = context;
        }

        public async Task<FacturacionMensual> GenerarFacturacionAsync(int mes, int anio)
        {
            // 1. Evitar duplicados: Buscamos si ya existe (Usamos Año con Ñ si tu entidad lo tiene así)
            var existente = await _context.FacturacionesMensuales
                .FirstOrDefaultAsync(f => f.Mes == mes && f.Año == anio);

            if (existente != null)
            {
                _context.FacturacionesMensuales.Remove(existente);
                await _context.SaveChangesAsync();
            }

            // 2. Calcular Totales

            // Ingresos
            var totalIngresos = await _context.Ingresos
                .Where(x => x.FechaRegistro.Month == mes && x.FechaRegistro.Year == anio)
                .SumAsync(x => (decimal?)x.Monto) ?? 0;

            // Egresos
            var totalEgresos = await _context.Egresos
                .Where(x => x.FechaRegistro.Month == mes && x.FechaRegistro.Year == anio)
                .SumAsync(x => (decimal?)x.Monto) ?? 0;

            // Nóminas (CORRECCIÓN AQUÍ: Usamos x.Año si x.Anio daba error)
            var totalNominas = await _context.Nominas
                .Where(x => x.Mes == mes && x.Año == anio)
                .SumAsync(x => (decimal?)x.MontoNeto) ?? 0;

            // 3. Crear Objeto
            var nuevaFacturacion = new FacturacionMensual
            {
                Mes = mes,
                Año = anio, // Asignamos al campo Año de la entidad
                TotalIngresos = totalIngresos,
                TotalEgresos = totalEgresos,
                TotalNominas = totalNominas,
                TotalSolicitudesAprobadas = 0,
                UtilidadBruta = totalIngresos - totalEgresos,
                UtilidadNeta = (totalIngresos - totalEgresos) - totalNominas,
                FechaGeneracion = DateTime.UtcNow,
                Estado = "Generado"
            };

            _context.FacturacionesMensuales.Add(nuevaFacturacion);
            await _context.SaveChangesAsync();

            return nuevaFacturacion;
        }

        public async Task<FacturacionMensual> ObtenerPorMesAnioAsync(int mes, int anio)
        {
            return await _context.FacturacionesMensuales
                .FirstOrDefaultAsync(f => f.Mes == mes && f.Año == anio);
        }

        public async Task<List<FacturacionMensual>> ObtenerTodasAsync()
        {
            return await _context.FacturacionesMensuales.ToListAsync();
        }
    }
}