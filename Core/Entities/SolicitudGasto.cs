namespace ContabilidadBackend.Core.Entities
{
    public class SolicitudGasto
    {
        public int Id { get; set; }
        public string TipoSolicitud { get; set; } // Marketing, Mantenimiento, Otros
        public string Departamento { get; set; } // De qué área solicita
        public decimal MontoSolicitado { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; } = "Pendiente"; // Pendiente, Aprobada, Rechazada
        public string? AprobadoPor { get; set; }
        public DateTime FechaSolicitud { get; set; } = DateTime.UtcNow;
        public DateTime? FechaAprobacion { get; set; }
        public string? Observaciones { get; set; }
    }
}

