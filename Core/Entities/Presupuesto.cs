using System;

namespace ContabilidadBackend.Core.Entities
{
    public class Presupuesto
    {
        public long Id { get; set; }
        public string Departamento { get; set; } // "RRHH", "Marketing", "Ventas"
        public decimal MontoTotal { get; set; }
        public decimal MontoEjecutado { get; set; } = 0;
        public int Mes { get; set; }
        public int Anio { get; set; }
        public string Estado { get; set; } = "Activo";
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    }
}
