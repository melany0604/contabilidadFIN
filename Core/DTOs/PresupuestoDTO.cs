namespace ContabilidadBackend.Core.DTOs
{
    public class PresupuestoDTO
    {
        public string Departamento { get; set; }
        public decimal MontoTotal { get; set; }
        public int Mes { get; set; }
        public int Anio { get; set; }
    }
    public class ActualizarSaldoDTO
    {
        
        public decimal RestarSaldo { get; set; }
    }
}

