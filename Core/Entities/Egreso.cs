using System;

namespace ContabilidadBackend.Core.Entities
{
    public class Egreso
    {
        public long Id { get; set; }
        public string Concepto { get; set; } // "nomina", "gasto-marketing", etc
        public decimal Monto { get; set; }
        public long IdProveedor { get; set; }
        public long IdPresupuesto { get; set; }
        public string Estado { get; set; } = "Pendiente"; // Pendiente, Autorizado, Pagado
        public long? IdAprobacionGerente { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
        public DateTime? FechaPago { get; set; }
    }
}
