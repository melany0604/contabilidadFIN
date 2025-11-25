namespace ContabilidadBackend.Presentation.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ContabilidadBackend.Core.DTOs;
    using ContabilidadBackend.Core.Interfaces;

    [ApiController]
    [Route("api/[controller]")]
    public class EgresosController : ControllerBase
    {
        private readonly IEgresoService _egresoService;
        private readonly ISolicitudGastoService _solicitudGastoService;

        public EgresosController(IEgresoService egresoService, ISolicitudGastoService solicitudGastoService)
        {
            _egresoService = egresoService;
            _solicitudGastoService = solicitudGastoService;
        }

        [HttpPost("solicitud-nomina")]
        public async Task<IActionResult> SolicitudNomina([FromBody] EgresoDTO egreso)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                egreso.TipoEgreso = "Nómina";
                var resultado = await _egresoService.CrearEgresoAsync(egreso);
                return CreatedAtAction(nameof(ObtenerEgreso), new { id = resultado.Id }, resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("solicitud-gasto")]
        public async Task<IActionResult> SolicitudGasto([FromBody] SolicitudGastoDTO solicitud)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var resultado = await _solicitudGastoService.CrearSolicitudAsync(solicitud);
                return CreatedAtAction(nameof(ObtenerSolicitud), new { id = resultado.Id }, resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerEgresos()
        {
            var egresos = await _egresoService.ObtenerTodosAsync();
            return Ok(egresos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerEgreso(int id)
        {
            // Asumiendo que EgresoService también tiene ObtenerPorIdAsync(long)
            var egreso = await _egresoService.ObtenerPorIdAsync(id);
            if (egreso == null) return NotFound();
            return Ok(egreso);
        }

        [HttpGet("solicitud/{id}")]
        public async Task<IActionResult> ObtenerSolicitud(int id)
        {
            try
            {
                // ANTES: Traías todas y filtrabas en memoria (Lento)
                // AHORA: Buscas solo la que necesitas (Rápido)
                var solicitud = await _solicitudGastoService.ObtenerPorIdAsync(id);

                if (solicitud == null)
                    return NotFound(new { mensaje = "Solicitud no encontrada" });

                return Ok(solicitud);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}

