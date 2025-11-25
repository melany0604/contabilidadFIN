namespace ContabilidadBackend.Core.DTOs
{
    public class NominaDTO
    {
        public int IdEmpleado { get; set; }
        public string NombreEmpleado { get; set; }
        public decimal Salario { get; set; }
        public decimal Deducciones { get; set; }
        public decimal Bonificacion { get; set; }
        public int Mes { get; set; }
        public int Año { get; set; }
        // ID y fechas se auto-generan en BD
    }
}
