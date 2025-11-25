using ContabilidadBackend.Core.DTOs;
using ContabilidadBackend.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContabilidadBackend.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngresosController : ControllerBase
    {
        private readonly IIngresoService _service;

        public IngresosController(IIngresoService service)
        {
            _service = service;
        }

        [HttpPost("venta-ruta")]
        public async Task<IActionResult> CrearVentaRuta([FromBody] IngresoDTO dto)
        {
            if (dto == null) return BadRequest("Los datos son requeridos");

            dto.Concepto = "venta-ruta";
            var result = await _service.CrearIngresoAsync(dto);
            return Ok(new { id = result.Id, mensaje = "Venta registrada" });
        }

        [HttpPost("venta-tienda")]
        public async Task<IActionResult> CrearVentaTienda([FromBody] IngresoDTO dto)
        {
            if (dto == null) return BadRequest("Los datos son requeridos");

            dto.Concepto = "venta-tienda";
            var result = await _service.CrearIngresoAsync(dto);
            return Ok(new { id = result.Id, mensaje = "Venta en tienda registrada" });
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerIngresos()
        {
            var ingresos = await _service.ObtenerIngresosAsync();
            return Ok(ingresos);
        }

        [HttpGet("{concepto}")]
        public async Task<IActionResult> ObtenerPorConcepto(string concepto)
        {
            var ingresos = await _service.ObtenerIngresoPorConceptoAsync(concepto);
            return Ok(ingresos);
        }
    }
}
