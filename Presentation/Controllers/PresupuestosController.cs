using ContabilidadBackend.Core.DTOs;
using ContabilidadBackend.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ContabilidadBackend.Core.DTOs;
using ContabilidadBackend.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic; 
using System;

namespace ContabilidadBackend.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PresupuestosController : ControllerBase
    {
        private readonly IPresupuestoService _service;

        public PresupuestosController(IPresupuestoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CrearPresupuesto([FromBody] PresupuestoDTO dto)
        {
            if (dto == null) return BadRequest("Los datos son requeridos");

            var result = await _service.CrearPresupuestoAsync(dto);
            return Ok(new { id = result.Id, mensaje = "Presupuesto creado" });
        }

        [HttpGet("disponibilidad")]
        public async Task<IActionResult> VerificaDisponibilidad([FromQuery] string departamento, [FromQuery] decimal montoSolicitado)
        {
            var disponible = await _service.VerificaDisponibilidadAsync(departamento, montoSolicitado);
            return Ok(new { departamento, montoSolicitado, disponible, puedeAutorizar = disponible >= montoSolicitado });
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerPresupuestos()
        {
            var presupuestos = await _service.ObtenerPresupuestosAsync();
            return Ok(presupuestos);
        }

        // --- MÉTODO ACTUALIZADO Y CORREGIDO ---
        [HttpPut("{id}/restar-saldo")]
        public async Task<IActionResult> RestarSaldo(int id, [FromBody] ActualizarSaldoDTO datos)
        {
            try
            {
                if (datos == null || datos.RestarSaldo <= 0)
                    return BadRequest(new { error = "El monto a restar debe ser mayor a cero" });

                var presupuesto = await _service.RestarSaldoAsync(id, datos.RestarSaldo);

                return Ok(new
                {
                    mensaje = "Saldo reducido correctamente",
                    presupuesto = new
                    {
                        id = presupuesto.Id,
                        departamento = presupuesto.Departamento,
                        saldoAnterior = presupuesto.Saldo + datos.RestarSaldo,
                        saldoActual = presupuesto.Saldo,
                        montoRestado = datos.RestarSaldo
                    }
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }
    }
}