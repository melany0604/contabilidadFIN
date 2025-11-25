using Microsoft.AspNetCore.Mvc;
using ContabilidadBackend.Core.DTOs;
using ContabilidadBackend.Core.Interfaces;
using System.Threading.Tasks;
using System;

namespace ContabilidadBackend.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CierreVentasController : ControllerBase
    {
        private readonly ICierreVentasService _service;

        public CierreVentasController(ICierreVentasService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarCierre([FromBody] CierreVentasDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var resultado = await _service.RegistrarCierreAsync(dto);
            return Ok(resultado);
        }

        [HttpGet("fecha/{fecha}")]
        public async Task<IActionResult> ObtenerPorFecha(DateTime fecha)
        {
            var resultados = await _service.ObtenerPorFechaAsync(fecha);
            return Ok(resultados);
        }

        // CAMBIO IMPORTANTE: "anio" en lugar de "año" para que Swagger no falle
        [HttpGet("mes/{mes}/anio/{anio}")]
        public async Task<IActionResult> ObtenerPorMesAnio(int mes, int anio)
        {
            var resultados = await _service.ObtenerPorMesAnioAsync(mes, anio);
            return Ok(resultados);
        }
    }
}
