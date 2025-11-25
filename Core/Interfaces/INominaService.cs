namespace ContabilidadBackend.Core.Interfaces
{
    using ContabilidadBackend.Core.DTOs;
    using ContabilidadBackend.Core.Entities;

    public interface INominaService
    {
        Task<Nomina> RegistrarNominaAsync(NominaDTO nomina);
        Task<List<Nomina>> ObtenerNominasDelMesAsync(int mes, int año);
        Task<decimal> ObtenerTotalNominasDelMesAsync(int mes, int año);
        Task<List<Nomina>> ObtenerTodasAsync();
    }
}
