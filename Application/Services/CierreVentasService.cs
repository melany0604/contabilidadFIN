namespace ContabilidadBackend.Application.Services
{
    using ContabilidadBackend.Core.DTOs;
    using ContabilidadBackend.Core.Entities;
    using ContabilidadBackend.Core.Interfaces;
    using ContabilidadBackend.Infrastructure.Data;
    using Microsoft.EntityFrameworkCore;

    public class CierreVentasService : ICierreVentasService
    {
        private readonly ContabilidadContext _context;

        public CierreVentasService(ContabilidadContext context)
        {
            _context = context;
        }

        public async Task<CierreVentas> CerrarVentasDelDiaAsync(CierreVentasDTO cierreDto)
        {
            var ventasNetas = cierreDto.VentasEnRuta + cierreDto.VentasEnTienda - cierreDto.Devoluciones;

            // Instanciamos CierreVentas
            var cierre = new CierreVentas
            {
                Fecha = DateTime.UtcNow,
                Sucursal = cierreDto.Sucursal,
                VentasEnRuta = cierreDto.VentasEnRuta,
                VentasEnTienda = cierreDto.VentasEnTienda,
                VentasTotal = cierreDto.VentasEnRuta + cierreDto.VentasEnTienda,
                Devoluciones = cierreDto.Devoluciones,
                VentasNetas = ventasNetas,
                Estado = "Cerrado",
                HoraCierre = DateTime.UtcNow
            };

            _context.CierresVentas.Add(cierre);
            await _context.SaveChangesAsync();
            return cierre;
        }

        public async Task<CierreVentas> ObtenerCierrePorFechaAsync(DateTime fecha)
        {
            // Usamos FirstOrDefaultAsync
            return await _context.CierresVentas
                .FirstOrDefaultAsync(c => c.Fecha.Date == fecha.Date);
        }

        public async Task<List<CierreVentas>> ObtenerCierresMesAsync(int mes, int año)
        {
            return await _context.CierresVentas
                .Where(c => c.Fecha.Month == mes && c.Fecha.Year == año)
                .ToListAsync();
        }
    }
}