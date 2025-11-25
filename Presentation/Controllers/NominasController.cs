namespace ContabilidadBackend.Presentation.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ContabilidadBackend.Core.DTOs;
    using ContabilidadBackend.Core.Interfaces;
    using ContabilidadBackend.Consumos;

    [ApiController]
    [Route("api/[controller]")]
    public class NominasController : ControllerBase
    {
        private readonly INominaService _nominaService;
        private readonly RRHHConsumerService _rrhhService;

        public NominasController(INominaService nominaService, RRHHConsumerService rrhhService)
        {
            _nominaService = nominaService;
            _rrhhService = rrhhService;
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarNomina([FromBody] NominaDTO nomina)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var resultado = await _nominaService.RegistrarNominaAsync(nomina);
                return CreatedAtAction(nameof(ObtenerNomina), new { id = resultado.Id }, resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("mes/{mes}/año/{año}")]
        public async Task<IActionResult> ObtenerNominasDelMes(int mes, int año)
        {
            try
            {
                var nominas = await _nominaService.ObtenerNominasDelMesAsync(mes, año);
                return Ok(nominas);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("total-mes/{mes}/año/{año}")]
        public async Task<IActionResult> ObtenerTotalNominasDelMes(int mes, int año)
        {
            try
            {
                var total = await _nominaService.ObtenerTotalNominasDelMesAsync(mes, año);
                return Ok(new { mes, año, total });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerNomina(int id)
        {
            try
            {
                var nominas = await _nominaService.ObtenerTodasAsync();
                var nomina = nominas.FirstOrDefault(n => n.Id == id);
                if (nomina == null)
                    return NotFound();

                return Ok(nomina);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("sincronizar-rrhh")]
        public async Task<IActionResult> SincronizarDeRRHH()
        {
            try
            {
                var nominasRRHH = await _rrhhService.ObtenerNominasAsync();
                return Ok(new { message = "Nóminas sincronizadas", count = nominasRRHH.Count, datos = nominasRRHH });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
