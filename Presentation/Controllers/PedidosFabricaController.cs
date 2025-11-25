namespace ContabilidadBackend.Presentation.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ContabilidadBackend.Core.DTOs;
    using ContabilidadBackend.Core.Interfaces;

    [ApiController]
    [Route("api/[controller]")]
    public class PedidosFabricaController : ControllerBase
    {
        private readonly IPedidoFabricaService _pedidoService;

        public PedidosFabricaController(IPedidoFabricaService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarPedido([FromBody] PedidoFabricaDTO pedido)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var resultado = await _pedidoService.RegistrarPedidoAsync(pedido);
                return CreatedAtAction(nameof(ObtenerPedido), new { id = resultado.Id }, resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id}/pagar")]
        public async Task<IActionResult> RegistrarPago(int id)
        {
            try
            {
                var resultado = await _pedidoService.RegistrarPagoAsync(id);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("pendientes")]
        public async Task<IActionResult> ObtenerPedidosPendientes()
        {
            try
            {
                var pedidos = await _pedidoService.ObtenerPedidosPendientesAsync();
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("total-pendientes")]
        public async Task<IActionResult> ObtenerTotalFacturasPendientes()
        {
            try
            {
                var total = await _pedidoService.ObtenerTotalFacturasPendientesAsync();
                return Ok(new { totalFacturasPendientes = total });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPedido(int id)
        {
            try
            {
                var pedidos = await _pedidoService.ObtenerTodasAsync();
                var pedido = pedidos.FirstOrDefault(p => p.Id == id);
                if (pedido == null)
                    return NotFound();

                return Ok(pedido);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
