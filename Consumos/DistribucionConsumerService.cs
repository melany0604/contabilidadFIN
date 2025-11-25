namespace ContabilidadBackend.Consumos
{
    using System.Net.Http.Json; // Asegúrate de tener este using
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Net.Http;
    using System;

    public class DistribucionConsumerService
    {
        private readonly HttpClient _httpClient;
        // Nota: Asegúrate que esta URL sea accesible desde el entorno donde corre ContabilidadBackend
        private readonly string _distribucionBaseUrl = "https://proyectodistribucion-production.up.railway.app/api";

        public DistribucionConsumerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<dynamic>> ObtenerPedidosPendientesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_distribucionBaseUrl}/Pedidos/pendientes");
                if (response.IsSuccessStatusCode)
                {
                    // CAMBIO AQUÍ: Usar ReadFromJsonAsync
                    var resultado = await response.Content.ReadFromJsonAsync<List<dynamic>>();
                    return resultado ?? new List<dynamic>();
                }
                return new List<dynamic>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Distribución Pedidos: {ex.Message}");
                return new List<dynamic>();
            }
        }

        public async Task<List<dynamic>> ObtenerGastosAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_distribucionBaseUrl}/Gastos");
                if (response.IsSuccessStatusCode)
                {
                    // CAMBIO AQUÍ
                    var resultado = await response.Content.ReadFromJsonAsync<List<dynamic>>();
                    return resultado ?? new List<dynamic>();
                }
                return new List<dynamic>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Distribución Gastos: {ex.Message}");
                return new List<dynamic>();
            }
        }

        public async Task<List<dynamic>> ObtenerRutasAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_distribucionBaseUrl}/Rutas");
                if (response.IsSuccessStatusCode)
                {
                    // CAMBIO AQUÍ
                    var resultado = await response.Content.ReadFromJsonAsync<List<dynamic>>();
                    return resultado ?? new List<dynamic>();
                }
                return new List<dynamic>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Distribución Rutas: {ex.Message}");
                return new List<dynamic>();
            }
        }
    }
}