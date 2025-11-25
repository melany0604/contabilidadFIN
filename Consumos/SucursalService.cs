using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ContabilidadBackend.Consumos
{
    public class SucursalService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://sucursalsantacruz-production.up.railway.app/";

        public SucursalService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(BaseUrl);
        }

        public async Task<dynamic> ObtenerDashboardAsync(string codigoSucursal)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Sucursal/dashboard/{codigoSucursal}");
                if (!response.IsSuccessStatusCode) return null;

                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Sucursal: {ex.Message}");
                return null;
            }
        }
    }
}
