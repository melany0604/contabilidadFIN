using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ContabilidadBackend.Consumos
{
    public class MarketingService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://marketinglechesc-production.up.railway.app/";

        public MarketingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<SolicitudMarketingDTO>> ObtenerSolicitudesAprobacionAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}api/Solicitudes/pendientes");
                if (!response.IsSuccessStatusCode) return new List<SolicitudMarketingDTO>();

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<List<SolicitudMarketingDTO>>(content);
                return result ?? new List<SolicitudMarketingDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Marketing: {ex.Message}");
                return new List<SolicitudMarketingDTO>();
            }
        }

        public async Task<List<GastoMarketingDTO>> ObtenerGastosAprobadosAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}api/Gastos/aprobados");
                if (!response.IsSuccessStatusCode) return new List<GastoMarketingDTO>();

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<List<GastoMarketingDTO>>(content);
                return result ?? new List<GastoMarketingDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error obteniendo gastos marketing: {ex.Message}");
                return new List<GastoMarketingDTO>();
            }
        }
    }

    public class SolicitudMarketingDTO
    {
        public long Id { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public string Estado { get; set; } = "Pendiente";
        public DateTime FechaCreacion { get; set; }
    }

    public class GastoMarketingDTO
    {
        public long Id { get; set; }
        public string Concepto { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public string Estado { get; set; } = "Aprobado";
        public DateTime FechaAprobacion { get; set; }
    }
}
