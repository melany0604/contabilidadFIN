using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ContabilidadBackend.Consumos
{
    public class RRHHService
    {
        private readonly HttpClient _httpClient;
        // Asegúrate que esta URL sea correcta y accesible desde donde corras la app
        private const string BaseUrl = "https://rrhhcloud2-production.up.railway.app/";

        // Opciones para evitar errores si el JSON viene en minúsculas
        private readonly JsonSerializerOptions _jsonOptions;

        public RRHHService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(BaseUrl);
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<EmpleadoRRHH?> ObtenerEmpleadoAsync(long idEmpleado)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Empleados/{idEmpleado}");
                if (!response.IsSuccessStatusCode) return null;

                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<EmpleadoRRHH>(content, _jsonOptions);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo RRHH: {ex.Message}");
                return null;
            }
        }

        public async Task<List<NominaRRHH>> ObtenerNominasAsync(int mes, int anio)
        {
            try
            {
                // Verifica que la ruta 'api/Nominas/mes/...' coincida exactamente con tu API de RRHH
                var response = await _httpClient.GetAsync($"api/Nominas/mes/{mes}/anio/{anio}");

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error HTTP RRHH: {response.StatusCode}");
                    return new List<NominaRRHH>();
                }

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<List<NominaRRHH>>(content, _jsonOptions);
                return result ?? new List<NominaRRHH>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error obteniendo nóminas RRHH: {ex.Message}");
                return new List<NominaRRHH>();
            }
        }
    }

    // DTOs
    public class EmpleadoRRHH
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Puesto { get; set; }
        public decimal Salario { get; set; }
    }

    public class NominaRRHH
    {
        public long Id { get; set; }
        public long IdEmpleado { get; set; }
        public decimal Monto { get; set; } // Asegúrate que en el JSON de RRHH venga como "monto" o "total"
        public int Mes { get; set; }
        public int Anio { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}