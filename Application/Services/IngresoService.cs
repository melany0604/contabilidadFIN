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
        public async Task<Ingreso> ObtenerPorIdAsync(long id)
        {
            return await _context.Ingresos.FindAsync(id);
        }

        public async Task<Ingreso> CrearIngresoAsync(IngresoDTO dto)
        {
            
            var ingreso = new Ingreso
            {
                NroFactura = dto.NroFactura,        
                Monto = dto.Monto,
                Concepto = dto.Concepto,
                MetodoPago = dto.MetodoPago,        
                IdChoferOVendedor = dto.IdChoferOVendedor,

                FechaRegistro = System.DateTime.UtcNow,
                Estado = "Registrado"               // Valor por defecto recomendado
            };

            _context.Ingresos.Add(ingreso);
            await _context.SaveChangesAsync();
            return ingreso;
        }

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
