namespace ContabilidadBackend.Application.Services
{
    using ContabilidadBackend.Core.DTOs;
    using ContabilidadBackend.Core.Entities;
    using ContabilidadBackend.Core.Interfaces;
    using ContabilidadBackend.Infrastructure.Data;

    public class PedidoFabricaService : IPedidoFabricaService
    {
        private readonly ContabilidadContext _context;

        public PedidoFabricaService(ContabilidadContext context)
        {
            _context = context;
        }

        public async Task<PedidoFabrica> RegistrarPedidoAsync(PedidoFabricaDTO pedidoDto)
        {
            var pedido = new PedidoFabrica
            {
                IdSucursal = pedidoDto.IdSucursal,
                NombreSucursal = pedidoDto.NombreSucursal,
                NombreGerente = pedidoDto.NombreGerente,
                ConceptoProducto = pedidoDto.ConceptoProducto,
                Cantidad = pedidoDto.Cantidad,
                MontoFactura = pedidoDto.MontoFactura,
                NumeroFactura = pedidoDto.NumeroFactura,
                Estado = "Recibido",
                FechaPedido = DateTime.UtcNow
            };

            _context.PedidosFabrica.Add(pedido);
            await _context.SaveChangesAsync();
            return pedido;
        }

        public async Task<PedidoFabrica> RegistrarPagoAsync(int idPedido)
        {
            var pedido = _context.PedidosFabrica.FirstOrDefault(p => p.Id == idPedido);
            if (pedido == null)
                throw new Exception("Pedido no encontrado");

            pedido.Estado = "Pagado";
            pedido.FechaPago = DateTime.UtcNow;

            _context.PedidosFabrica.Update(pedido);
            await _context.SaveChangesAsync();
            return pedido;
        }

        public async Task<List<PedidoFabrica>> ObtenerPedidosPendientesAsync()
        {
            return await Task.FromResult(_context.PedidosFabrica
                .Where(p => p.Estado == "Recibido" || p.Estado == "Pendiente")
                .ToList());
        }

        public async Task<decimal> ObtenerTotalFacturasPendientesAsync()
        {
            var total = _context.PedidosFabrica
                .Where(p => p.Estado == "Recibido" || p.Estado == "Pendiente")
                .Sum(p => p.MontoFactura);

            return await Task.FromResult(total);
        }

        public async Task<List<PedidoFabrica>> ObtenerTodasAsync()
        {
            return await Task.FromResult(_context.PedidosFabrica.ToList());
        }
    }
}

