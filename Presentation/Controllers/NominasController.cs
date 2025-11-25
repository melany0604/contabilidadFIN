using Microsoft.AspNetCore.Mvc;
using ContabilidadBackend.Core.DTOs;
using ContabilidadBackend.Core.Interfaces;
using ContabilidadBackend.Consumos;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ContabilidadBackend.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NominasController : ControllerBase
    {
        private readonly INominaService _nominaService;
        private readonly RRHHService _rrhhService;

        public NominasController(INominaService nominaService, RRHHService rrhhService)
        {
            _nominaService = nominaService;
            _rrhhService = rrhhService;
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarNomina([FromBody] NominaDTO nomina)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var resultado = await _nominaService.RegistrarNominaAsync(nomina);
                return CreatedAtAction(nameof(ObtenerNomina), new { id = resultado.Id }, resultado);
            }
            catch (Exception ex) { return BadRequest(new { error = ex.Message }); }
        }

        // --- CORRECCIÓN 1: Implementación real del GET ---
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerNomina(int id)
        {
            // Asumiendo que tu servicio tiene un método ObtenerPorIdAsync
            var nomina = await _nominaService.ObtenerPorIdAsync(id);

            if (nomina == null)
            {
                return NotFound(new { mensaje = $"No se encontró la nómina con ID {id}" });
            }

            return Ok(nomina);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodas()
        {
            var nominas = await _nominaService.ObtenerTodasAsync();
            return Ok(nominas);
        }

        [HttpPost("sincronizar-rrhh")]
        public async Task<IActionResult> SincronizarDeRRHH([FromQuery] int? mes, [FromQuery] int? anio)
        {
            try
            {
                int mesConsulta = mes ?? DateTime.Now.Month;
                int anioConsulta = anio ?? DateTime.Now.Year;

                var nominasExternas = await _rrhhService.ObtenerNominasAsync(mesConsulta, anioConsulta);

                if (nominasExternas == null || !nominasExternas.Any())
                {
                    return Ok(new { message = "No se encontraron nóminas en RRHH para este periodo." });
                }

                int contadorImportados = 0;
                foreach (var nominaExt in nominasExternas)
                {
                    var nuevaNomina = new NominaDTO
                    {
                        IdEmpleado = (int)nominaExt.IdEmpleado,

                        // CORRECCIÓN AQUÍ: Como NominaRRHH no tiene nombre, usamos el ID
                        NombreEmpleado = $"Empleado Importado {nominaExt.IdEmpleado}",

                        MontoNeto = nominaExt.Monto,
                        Salario = nominaExt.Monto,
                        Deducciones = 0,
                        Bonificacion = 0,
                        Mes = nominaExt.Mes,
                        Anio = nominaExt.Anio,
                        Estado = "Sincronizado",
                        Departamento = "General",
                        FechaPago = DateTime.UtcNow
                    };

                    await _nominaService.RegistrarNominaAsync(nuevaNomina);
                    contadorImportados++;
                }

                return Ok(new
                {
                    message = "Sincronización completada",
                    recibidos = nominasExternas.Count,
                    guardados = contadorImportados
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = $"Error en sincronización: {ex.Message}" });
            }
        }
    }
}