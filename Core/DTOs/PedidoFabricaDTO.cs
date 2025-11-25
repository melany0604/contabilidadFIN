namespace ContabilidadBackend.Core.DTOs
{
    public class PedidoFabricaDTO
    {
        public int IdSucursal { get; set; }
        public string NombreSucursal { get; set; }
        public string NombreGerente { get; set; }
        public string ConceptoProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal MontoFactura { get; set; }
        public string NumeroFactura { get; set; }
        // ID, Estado y fechas se auto-generan en BD
    }
}

