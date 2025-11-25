using Microsoft.AspNetCore.Mvc;
using ContabilidadBackend.Core.Interfaces;
using System.Threading.Tasks;

namespace ContabilidadBackend.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacturacionMensualController : ControllerBase
    {
        private readonly IFacturacionMensualService _service;

        public FacturacionMensualController(IFacturacionMensualService service)
        {
            _service = service;
        }

        // --- CORRECCIÓN: Cambiar 'año' por 'anio' ---
        [HttpPost("generar/{mes}/{anio}")]
        public async Task<IActionResult> GenerarFacturacion(int mes, int anio)
        {
            try
            {
                var resultado = await _service.GenerarFacturacionAsync(mes, anio);
                return Ok(resultado);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // --- CORRECCIÓN: Cambiar 'año' por 'anio' ---
        [HttpGet("{mes}/{anio}")]
        public async Task<IActionResult> ObtenerFacturacion(int mes, int anio)
        {
            var resultado = await _service.ObtenerPorMesAnioAsync(mes, anio);
            if (resultado == null) return NotFound(new { mensaje = "No se ha generado facturación para este mes" });
            return Ok(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodas()
        {
            return Ok(await _service.ObtenerTodasAsync());
        }
    }
}
