namespace ContabilidadBackend.Core.Entities
{
    public class CierreVentasDia
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        public string Sucursal { get; set; }
        public decimal VentasEnRuta { get; set; }
        public decimal VentasEnTienda { get; set; }
        public decimal VentasTotal { get; set; }
        public decimal Devoluciones { get; set; }
        public decimal VentasNetas { get; set; }
        public string Estado { get; set; } // Abierto, Cerrado
        public DateTime? HoraCierre { get; set; }
    }
}

