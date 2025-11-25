namespace ContabilidadBackend.Core.Interfaces
{
    using ContabilidadBackend.Core.DTOs;
    using ContabilidadBackend.Core.Entities;

    public interface ISolicitudGastoService
    {
        Task<SolicitudGasto> CrearSolicitudAsync(SolicitudGastoDTO solicitud);
        Task<SolicitudGasto> AprobarSolicitudAsync(AprobacionSolicitudDTO aprobacion);
        Task<SolicitudGasto> RechazarSolicitudAsync(AprobacionSolicitudDTO rechazo);
        Task<List<SolicitudGasto>> ObtenerSolicitudesPendientesAsync();
        Task<List<SolicitudGasto>> ObtenerSolicitudesAprobadas();
        Task<List<SolicitudGasto>> ObtenerTodasAsync();
    }
}

