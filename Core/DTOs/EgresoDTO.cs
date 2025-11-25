namespace ContabilidadBackend.Core.DTOs
{
    public class EgresoDTO
    {
        public string Concepto { get; set; }
        public string? TipoEgreso { get; set; }
        public decimal Monto { get; set; }
        public long IdProveedor { get; set; }
        public long IdPresupuesto { get; set; }
        public long? IdAprobacionGerente { get; set; }
    }
}
