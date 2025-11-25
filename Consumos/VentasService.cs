using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ContabilidadBackend.Consumos
{
    public class VentasService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://ventassc-production.up.railway.app/";

        public VentasService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<VentaRegistro>> ObtenerVentasAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}api/Ventas/Ventas");
                if (!response.IsSuccessStatusCode) return new List<VentaRegistro>();

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<List<VentaRegistro>>(content);
                return result ?? new List<VentaRegistro>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error consumiendo Ventas: {ex.Message}");
                return new List<VentaRegistro>();
            }
        }

        public async Task<List<PedidoVentas>> ObtenerPedidosAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}api/Pedidos/ListaDePedidos");
                if (!response.IsSuccessStatusCode) return new List<PedidoVentas>();

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<List<PedidoVentas>>(content);
                return result ?? new List<PedidoVentas>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error obteniendo pedidos: {ex.Message}");
                return new List<PedidoVentas>();
            }
        }
    }

    public class VentaRegistro
    {
        public string Codigo { get; set; } = string.Empty;
        public string CodigoPedido { get; set; } = string.Empty;
        public string CodigoCliente { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public string Estado { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }
    }

    public class PedidoVentas
    {
        public string Codigo { get; set; } = string.Empty;
        public string CodigoCliente { get; set; } = string.Empty;
        public string CodigoEmpleado { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
    }
}