namespace ContabilidadBackend.Consumos
{
    using System.Net.Http.Json; // Necesario para ReadFromJsonAsync
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Net.Http;
    using System;

    public class RRHHConsumerService
    {
        private readonly HttpClient _httpClient;
        private readonly string _rrhhBaseUrl = "https://brrhh-production.up.railway.app/api";

        public RRHHConsumerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<dynamic>> ObtenerNominasAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_rrhhBaseUrl}/Nomina/Listar");
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
                Console.WriteLine($"Error consumiendo RRHH Nominas: {ex.Message}");
                return new List<dynamic>();
            }
        }

        public async Task<List<dynamic>> ObtenerEmpleadosAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_rrhhBaseUrl}/Empleados/Listar");
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
                Console.WriteLine($"Error consumiendo RRHH Empleados: {ex.Message}");
                return new List<dynamic>();
            }
        }

        public async Task<List<dynamic>> ObtenerDepartamentosAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_rrhhBaseUrl}/DepartamentoEmpresa/Listar");
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
                Console.WriteLine($"Error consumiendo RRHH Departamentos: {ex.Message}");
                return new List<dynamic>();
            }
        }
    }
}