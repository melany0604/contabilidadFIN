using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ContabilidadBackend.Consumos
{
    public class RRHHService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://brrhh-production.up.railway.app/";

        public RRHHService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(BaseUrl);
        }

        public async Task<dynamic> VerificarEmpleadoAsync(long idEmpleado)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Empleados/{idEmpleado}/estado");
                if (!response.IsSuccessStatusCode) return null;

                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<dynamic>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo RRHH: {ex.Message}");
                return null;
            }
        }
    }
}
