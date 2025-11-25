using System;

namespace ContabilidadBackend.Core.Entities
{
    public class CuentaPorPagar
    {
        public long Id { get; set; }
        public long IdProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public decimal MontoTotal { get; set; }
        public decimal SaldoPendiente { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string Estado { get; set; } = "Pendiente"; // Pendiente, Autorizado, Pagado
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    }
}
