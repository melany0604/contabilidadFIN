namespace ContabilidadBackend.Core.Interfaces
{
    using ContabilidadBackend.Core.DTOs;
    using ContabilidadBackend.Core.Entities;

    public interface ICierreVentasService
    {
        // Cambiado CierreVentasDia -> CierreVentas
        Task<CierreVentas> CerrarVentasDelDiaAsync(CierreVentasDTO cierre);
        Task<CierreVentas> ObtenerCierrePorFechaAsync(DateTime fecha);
        Task<List<CierreVentas>> ObtenerCierresMesAsync(int mes, int año);
    }
}
