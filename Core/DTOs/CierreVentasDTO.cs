namespace ContabilidadBackend.Core.DTOs
{
    public class CierreVentasDTO
    {
        public string Sucursal { get; set; }
        public decimal VentasEnRuta { get; set; }
        public decimal VentasEnTienda { get; set; }
        public decimal Devoluciones { get; set; }
        // Totales se calculan automáticamente
        // ID y fechas se auto-generan en BD
    }
}
