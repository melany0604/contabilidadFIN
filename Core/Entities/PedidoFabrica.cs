namespace ContabilidadBackend.Core.Entities
{
    public class PedidoFabrica
    {
        public int Id { get; set; }
        public int IdSucursal { get; set; }
        public string NombreSucursal { get; set; }
        public string NombreGerente { get; set; }
        public decimal MontoFactura { get; set; }
        public string ConceptoProducto { get; set; }
        public int Cantidad { get; set; }
        public string Estado { get; set; } // Recibido, Pagado, Pendiente
        public DateTime FechaPedido { get; set; } = DateTime.UtcNow;
        public DateTime? FechaPago { get; set; }
        public string NumeroFactura { get; set; }
    }
}
