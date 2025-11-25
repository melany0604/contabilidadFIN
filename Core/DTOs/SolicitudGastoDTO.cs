namespace ContabilidadBackend.Core.DTOs
{
    public class SolicitudGastoDTO
    {
        public string TipoSolicitud { get; set; } // Marketing, Mantenimiento, Otros
        public string Departamento { get; set; }
        public decimal MontoSolicitado { get; set; }
        public string Descripcion { get; set; }
        // ID, Estado y fechas se auto-generan en BD
    }
}
