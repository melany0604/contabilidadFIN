namespace ContabilidadBackend.Presentation.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ContabilidadBackend.Core.Interfaces;

    [ApiController]
    [Route("api/[controller]")]
    public class FacturacionMensualController : ControllerBase
    {
        private readonly IFacturacionMensualService _facturacionService;

        public FacturacionMensualController(IFacturacionMensualService facturacionService)
        {
            _facturacionService = facturacionService;
        }

        [HttpPost("generar/{mes}/{año}")]
        public async Task<IActionResult> GenerarFacturacionMensual(int mes, int año)
        {
            try
            {
                var resultado = await _facturacionService.GenerarFacturacionMensualAsync(mes, año);
                return CreatedAtAction(nameof(ObtenerFacturacion), new { mes, año }, resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("{mes}/{año}")]
        public async Task<IActionResult> ObtenerFacturacion(int mes, int año)
        {
            try
            {
                var facturacion = await _facturacionService.ObtenerFacturacionAsync(mes, año);
                if (facturacion == null)
                    return NotFound();

                return Ok(facturacion);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodasFacturaciones()
        {
            try
            {
                var facturaciones = await _facturacionService.ObtenerTodasAsync();
                return Ok(facturaciones);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}

