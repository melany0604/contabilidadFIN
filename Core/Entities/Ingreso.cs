using System;

namespace ContabilidadBackend.Core.Entities
{
    public class Ingreso
    {
        public long Id { get; set; }
        public string NroFactura { get; set; }
        public decimal Monto { get; set; }
        public string Concepto { get; set; } // "venta-ruta", "venta-tienda"
        public string MetodoPago { get; set; } // Efectivo, QR, Transferencia
        public long IdChoferOVendedor { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
        public string Estado { get; set; } = "Registrado"; // Registrado, Verificado
    }
}

