namespace ContabilidadBackend.Core.DTOs
{
    public class SolicitudGastoDTO
    {
        public string TipoSolicitud { get; set; } // Marketing, Mantenimiento, Otros
        public string Departamento { get; set; }
        public decimal MontoSolicitado { get; set; }
        public string Descripcion { get; set; }
        public string? AprobadoPor { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public string? Observaciones { get; set; }
    }
}

