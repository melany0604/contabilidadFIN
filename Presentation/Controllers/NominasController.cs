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

        // ... (Tus otros métodos POST y GET se mantienen igual) ...

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

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerNomina(int id)
        {
            // ... (Tu implementación actual) ...
            return Ok(); // Simplificado para el ejemplo
        }

        // CORRECCIÓN PRINCIPAL AQUÍ
        [HttpPost("sincronizar-rrhh")]
        public async Task<IActionResult> SincronizarDeRRHH([FromQuery] int? mes, [FromQuery] int? anio)
        {
            try
            {
                // 1. Definir fecha (si no envían parámetros, usar fecha actual)
                int mesConsulta = mes ?? DateTime.Now.Month;
                int anioConsulta = anio ?? DateTime.Now.Year;

                // 2. Obtener datos del microservicio externo (RRHH)
                // SOLUCIÓN DEL ERROR: Ahora pasamos los parámetros requeridos
                var nominasExternas = await _rrhhService.ObtenerNominasAsync(mesConsulta, anioConsulta);

                if (nominasExternas == null || !nominasExternas.Any())
                {
                    return Ok(new { message = "No se encontraron nóminas en RRHH para este periodo." });
                }

                // 3. Guardar en Base de Datos Local (Contabilidad)
                int contadorImportados = 0;
                foreach (var nominaExt in nominasExternas)
                {
                    // Mapeamos de NominaRRHH (Externo) a NominaDTO (Interno)
                    var nuevaNomina = new NominaDTO
                    {
                        IdEmpleado = (int)nominaExt.IdEmpleado, // Asegúrate de que los tipos coincidan
                        MontoNeto = nominaExt.Monto,
                        Mes = nominaExt.Mes,
                        Anio = nominaExt.Anio,
                        Estado = "Sincronizado",
                        // Ajusta estas propiedades según tu DTO real
                        Departamento = "General",
                        FechaPago = DateTime.UtcNow
                    };

                    // Guardamos usando tu servicio local
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
