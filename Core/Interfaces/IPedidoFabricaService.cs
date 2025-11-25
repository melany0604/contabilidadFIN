namespace ContabilidadBackend.Core.Interfaces
{
    using ContabilidadBackend.Core.DTOs;
    using ContabilidadBackend.Core.Entities;

    public interface IPedidoFabricaService
    {
        Task<PedidoFabrica> RegistrarPedidoAsync(PedidoFabricaDTO pedido);
        Task<PedidoFabrica> RegistrarPagoAsync(int idPedido);
        Task<List<PedidoFabrica>> ObtenerPedidosPendientesAsync();
        Task<List<PedidoFabrica>> ObtenerTodasAsync();
        Task<decimal> ObtenerTotalFacturasPendientesAsync();
    }
}

