namespace ContabilidadBackend.Presentation.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ContabilidadBackend.Core.DTOs;
    using ContabilidadBackend.Core.Interfaces;

    [ApiController]
    [Route("api/[controller]")]
    public class CierreVentasController : ControllerBase
    {
        private readonly ICierreVentasService _cierreService;

        public CierreVentasController(ICierreVentasService cierreService)
        {
            _cierreService = cierreService;
        }

        [HttpPost("cerrar-dia")]
        public async Task<IActionResult> CerrarVentasDelDia([FromBody] CierreVentasDTO cierre)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var resultado = await _cierreService.CerrarVentasDelDiaAsync(cierre);
                return CreatedAtAction(nameof(ObtenerCierrePorFecha), new { fecha = resultado.Fecha }, resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("fecha/{fecha}")]
        public async Task<IActionResult> ObtenerCierrePorFecha(DateTime fecha)
        {
            try
            {
                var cierre = await _cierreService.ObtenerCierrePorFechaAsync(fecha);
                if (cierre == null)
                    return NotFound();

                return Ok(cierre);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("mes/{mes}/año/{año}")]
        public async Task<IActionResult> ObtenerCierresMes(int mes, int año)
        {
            try
            {
                var cierres = await _cierreService.ObtenerCierresMesAsync(mes, año);
                return Ok(cierres);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
