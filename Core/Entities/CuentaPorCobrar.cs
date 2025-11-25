using System;

namespace ContabilidadBackend.Core.Entities
{
    public class CuentaPorCobrar
    {
        public long Id { get; set; }
        public long IdEmpleado { get; set; }
        public decimal Monto { get; set; }
        public string Concepto { get; set; } // "Faltante Ruta X"
        public string Estado { get; set; } = "Pendiente"; // Pendiente, Descontado
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    }
}
