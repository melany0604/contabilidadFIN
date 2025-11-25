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

        
        public async Task<SolicitudGasto> ObtenerPorIdAsync(int id)
        {
            return await _context.SolicitudesGasto.FindAsync((long)id);
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
            };

            _context.SolicitudesGasto.Add(solicitud);
            await _context.SaveChangesAsync();
            return solicitud;
        }

        // 3. Aprobar Solicitud
        public async Task<SolicitudGasto> AprobarSolicitudAsync(AprobacionSolicitudDTO aprobacionDto)
        {
            var solicitud = await _context.SolicitudesGasto.FindAsync((long)aprobacionDto.IdSolicitud);

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
            var solicitud = await _context.SolicitudesGasto.FindAsync((long)rechazoDto.IdSolicitud);

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