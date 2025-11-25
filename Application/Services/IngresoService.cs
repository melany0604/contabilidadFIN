using ContabilidadBackend.Core.DTOs;
using ContabilidadBackend.Core.Entities;
using ContabilidadBackend.Core.Interfaces;
using ContabilidadBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContabilidadBackend.Application.Services
{
    public class IngresoService : IIngresoService
    {
        private readonly ContabilidadContext _context;

        public IngresoService(ContabilidadContext context)
        {
            _context = context;
        }

        public async Task<Ingreso> CrearIngresoAsync(IngresoDTO dto)
        {
            var ingreso = new Ingreso
            {
                Concepto = dto.Concepto,
                Monto = dto.Monto,
                // Mapea las demás propiedades necesarias del DTO a la Entidad
                FechaRegistro = System.DateTime.UtcNow
            };

            _context.Ingresos.Add(ingreso);
            await _context.SaveChangesAsync();
            return ingreso;
        }

        // RENOMBRADO: Implementación correcta de ObtenerTodosAsync
        public async Task<List<Ingreso>> ObtenerTodosAsync()
        {
            return await _context.Ingresos.ToListAsync();
        }

        public async Task<List<Ingreso>> ObtenerIngresoPorConceptoAsync(string concepto)
        {
            return await _context.Ingresos
                                 .Where(i => i.Concepto.Contains(concepto))
                                 .ToListAsync();
        }
    }
}
