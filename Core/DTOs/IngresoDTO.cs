namespace ContabilidadBackend.Core.DTOs
{
    public class IngresoDTO
    {
        public string NroFactura { get; set; }
        public decimal Monto { get; set; }
        public string Concepto { get; set; } // "venta-ruta", "venta-tienda"
        public string MetodoPago { get; set; }
        public long IdChoferOVendedor { get; set; }
    }
}
