namespace ContabilidadBackend.Core.Entities
{
    public class Nomina
    {
        public int Id { get; set; }
        public int IdEmpleado { get; set; }
        public string NombreEmpleado { get; set; }
        public decimal Salario { get; set; }
        public decimal Deducciones { get; set; }
        public decimal Bonificacion { get; set; }
        public decimal MontoNeto { get; set; }
        public string Estado { get; set; } // Pagada, Pendiente, Procesando
        public DateTime FechaGeneracion { get; set; } = DateTime.UtcNow;
        public DateTime? FechaPago { get; set; }
        public int Mes { get; set; }
        public int Año { get; set; }
    }
}
