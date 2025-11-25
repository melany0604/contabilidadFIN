namespace ContabilidadBackend.Core.Interfaces
{
    using ContabilidadBackend.Core.Entities;

    public interface IFacturacionMensualService
    {
        Task<FacturacionMensual> GenerarFacturacionMensualAsync(int mes, int año);
        Task<FacturacionMensual> ObtenerFacturacionAsync(int mes, int año);
        Task<List<FacturacionMensual>> ObtenerTodasAsync();
    }
}
