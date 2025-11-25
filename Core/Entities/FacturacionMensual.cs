namespace ContabilidadBackend.Core.Entities
{
    public class FacturacionMensual
    {
        public int Id { get; set; }
        public int Mes { get; set; }
        public int Año { get; set; }
        public decimal TotalIngresos { get; set; }
        public decimal TotalEgresos { get; set; }
        public decimal TotalNominas { get; set; }
        public decimal TotalSolicitudesAprobadas { get; set; }
        public decimal UtilidadBruta { get; set; }
        public decimal UtilidadNeta { get; set; }
        public DateTime FechaGeneracion { get; set; } = DateTime.UtcNow;
        public string Estado { get; set; } // Abierto, Cerrado
    }
}
