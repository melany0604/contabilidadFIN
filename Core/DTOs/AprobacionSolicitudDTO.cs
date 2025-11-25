namespace ContabilidadBackend.Core.DTOs
{
    public class AprobacionSolicitudDTO
    {
        public int IdSolicitud { get; set; }
        public string Estado { get; set; } // Aprobada, Rechazada
        public string AprobadoPor { get; set; }
        public string Observaciones { get; set; }
    }
}


