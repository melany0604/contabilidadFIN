using ContabilidadBackend.Core.DTOs;
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
    public class CierreVentasService : ICierreVentasService
    {
        private readonly ContabilidadContext _context;

        public CierreVentasService(ContabilidadContext context)
        {
            _context = context;
        }

        public async Task<CierreVentas> RegistrarCierreAsync(CierreVentasDTO dto)
        {
            var cierre = new CierreVentas
            {
                Fecha = dto.Fecha, // Asegúrate de que tu DTO tenga esta propiedad
                Sucursal = dto.Sucursal,
                VentasEnRuta = dto.VentasEnRuta,
                VentasEnTienda = dto.VentasEnTienda,
                VentasTotal = dto.VentasEnRuta + dto.VentasEnTienda,
                Devoluciones = dto.Devoluciones,
                VentasNetas = (dto.VentasEnRuta + dto.VentasEnTienda) - dto.Devoluciones,
                HoraCierre = DateTime.UtcNow,
                Estado = "Cerrado"
            };

            _context.CierresVentas.Add(cierre);
            await _context.SaveChangesAsync();
            return cierre;
        }

        public async Task<List<CierreVentas>> ObtenerPorFechaAsync(DateTime fecha)
        {
            // Usamos .Date para ignorar la hora y evitar errores 404
            return await _context.CierresVentas
                .Where(x => x.Fecha.Date == fecha.Date)
                .ToListAsync();
        }

        public async Task<List<CierreVentas>> ObtenerPorMesAnioAsync(int mes, int anio)
        {
            return await _context.CierresVentas
                .Where(x => x.Fecha.Month == mes && x.Fecha.Year == anio)
                .ToListAsync();
        }

        public async Task<List<CierreVentas>> ObtenerTodosAsync()
        {
            return await _context.CierresVentas.ToListAsync();
        }
    }
}