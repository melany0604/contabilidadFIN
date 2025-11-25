namespace ContabilidadBackend.Core.DTOs
{
    public class CuentaPorCobrarDTO
    {
        public long IdEmpleado { get; set; }
        public decimal Monto { get; set; }
        public string Concepto { get; set; }
    }
}
