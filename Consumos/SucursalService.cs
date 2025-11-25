using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ContabilidadBackend.Consumos
{
    public class SucursalService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://sucursalsc-production.up.railway.app/";

        public SucursalService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // <CHANGE> Tipado sin dynamic - Retorna DTO completo
        public async Task<SucursalDTO?> ObtenerSucursalAsync(string codigoSucursal)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}api/Sucursal/{codigoSucursal}");
                if (!response.IsSuccessStatusCode) return null;

                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<SucursalDTO>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Sucursal: {ex.Message}");
                return null;
            }
        }

        public async Task<List<SucursalDTO>> ObtenerTodasSucursalesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}api/Sucursal/todos");
                if (!response.IsSuccessStatusCode) return new List<SucursalDTO>();

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<List<SucursalDTO>>(content);
                return result ?? new List<SucursalDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error obteniendo sucursales: {ex.Message}");
                return new List<SucursalDTO>();
            }
        }

        public async Task<SolicitudDTO?> ObtenerSolicitudesAsync(string codigoSucursal)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}api/Solicitud/pendientes/{codigoSucursal}");
                if (!response.IsSuccessStatusCode) return null;

                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<SolicitudDTO>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error obteniendo solicitudes: {ex.Message}");
                return null;
            }
        }
    }

    public class SucursalDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Gerente { get; set; } = string.Empty;
        public int EmpleadosActivos { get; set; }
        public decimal SaldoPresupuesto { get; set; }
        public decimal TotalPresupuesto { get; set; }
        public bool Activa { get; set; }
    }

    public class SolicitudDTO
    {
        public int Id { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string Departamento { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
    }
}