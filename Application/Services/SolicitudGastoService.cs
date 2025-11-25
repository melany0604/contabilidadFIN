namespace ContabilidadBackend.Application.Services
{
    using ContabilidadBackend.Core.DTOs;
    using ContabilidadBackend.Core.Entities;
    using ContabilidadBackend.Core.Interfaces;
    using ContabilidadBackend.Infrastructure.Data;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SolicitudGastoService : ISolicitudGastoService
    {
        private readonly ContabilidadContext _context;

        public SolicitudGastoService(ContabilidadContext context)
        {
            _context = context;
        }

        // --- CORRECCIÓN AQUÍ: Quitamos (long) ---
        public async Task<SolicitudGasto> ObtenerPorIdAsync(int id)
        {
            // El error decía que la propiedad es 'int', así que pasamos 'id' directo
            return await _context.SolicitudesGasto.FindAsync(id);
        }

        public async Task<SolicitudGasto> CrearSolicitudAsync(SolicitudGastoDTO solicitudDto)
        {
            var solicitud = new SolicitudGasto
            {
                TipoSolicitud = solicitudDto.TipoSolicitud,
                Departamento = solicitudDto.Departamento,
                MontoSolicitado = solicitudDto.MontoSolicitado,
                Descripcion = solicitudDto.Descripcion,
                Estado = "Pendiente",
                FechaSolicitud = DateTime.UtcNow
                // No asignamos AprobadoPor ni Observaciones, serán null (y ahora la entidad lo permite)
            };

            _context.SolicitudesGasto.Add(solicitud);
            await _context.SaveChangesAsync();
            return solicitud;
        }

        public async Task<SolicitudGasto> AprobarSolicitudAsync(AprobacionSolicitudDTO aprobacionDto)
        {
            // --- CORRECCIÓN AQUÍ: Quitamos (long) ---
            var solicitud = await _context.SolicitudesGasto.FindAsync(aprobacionDto.IdSolicitud);

            if (solicitud == null)
                throw new Exception("Solicitud no encontrada");

            solicitud.Estado = "Aprobada";
            solicitud.AprobadoPor = aprobacionDto.AprobadoPor;
            solicitud.FechaAprobacion = DateTime.UtcNow;
            solicitud.Observaciones = aprobacionDto.Observaciones;

            _context.SolicitudesGasto.Update(solicitud);
            await _context.SaveChangesAsync();
            return solicitud;
        }

        public async Task<SolicitudGasto> RechazarSolicitudAsync(AprobacionSolicitudDTO rechazoDto)
        {
            // --- CORRECCIÓN AQUÍ: Quitamos (long) ---
            var solicitud = await _context.SolicitudesGasto.FindAsync(rechazoDto.IdSolicitud);

            if (solicitud == null)
                throw new Exception("Solicitud no encontrada");

            solicitud.Estado = "Rechazada";
            solicitud.AprobadoPor = rechazoDto.AprobadoPor;
            solicitud.FechaAprobacion = DateTime.UtcNow;
            solicitud.Observaciones = rechazoDto.Observaciones;

            _context.SolicitudesGasto.Update(solicitud);
            await _context.SaveChangesAsync();
            return solicitud;
        }

        public async Task<List<SolicitudGasto>> ObtenerSolicitudesPendientesAsync()
        {
            return await _context.SolicitudesGasto
                .Where(s => s.Estado == "Pendiente")
                .ToListAsync();
        }

        public async Task<List<SolicitudGasto>> ObtenerSolicitudesAprobadas()
        {
            return await _context.SolicitudesGasto
                .Where(s => s.Estado == "Aprobada")
                .ToListAsync();
        }

        public async Task<List<SolicitudGasto>> ObtenerTodasAsync()
        {
            return await _context.SolicitudesGasto.ToListAsync();
        }
    }
}