using System;

namespace ContabilidadBackend.Core.DTOs
{
    public class NominaDTO
    {
        // Propiedades existentes
        public int IdEmpleado { get; set; }
        public string NombreEmpleado { get; set; } = string.Empty; // Inicializado para evitar warnings
        public decimal Salario { get; set; }
        public decimal Deducciones { get; set; }
        public decimal Bonificacion { get; set; }
        public int Mes { get; set; }

        // CAMBIO 1: Renombrar 'Año' a 'Anio' para coincidir con el Controlador
        public int Anio { get; set; }

        // CAMBIO 2: Agregamos las propiedades que faltaban según los errores
        public decimal MontoNeto { get; set; }
        public string Estado { get; set; } = "Pendiente";
        public string Departamento { get; set; } = "General";
        public DateTime? FechaPago { get; set; }

        // Opcional: Si necesitas el ID de la nómina
        public int Id { get; set; }
    }
}
