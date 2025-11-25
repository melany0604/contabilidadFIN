namespace ContabilidadBackend.Core.Interfaces
{
    using ContabilidadBackend.Core.DTOs;
    using ContabilidadBackend.Core.Entities;

    public interface ICierreVentasService
    {
        Task<CierreVentasDia> CerrarVentasDelDiaAsync(CierreVentasDTO cierre);
        Task<CierreVentasDia> ObtenerCierrePorFechaAsync(DateTime fecha);
        Task<List<CierreVentasDia>> ObtenerCierresMesAsync(int mes, int año);
    }
}
