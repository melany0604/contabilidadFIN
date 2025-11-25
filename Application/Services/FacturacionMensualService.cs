namespace ContabilidadBackend.Application.Services
{
    using ContabilidadBackend.Core.Entities;
    using ContabilidadBackend.Core.Interfaces;
    using ContabilidadBackend.Infrastructure.Data;

    public class FacturacionMensualService : IFacturacionMensualService
    {
        private readonly ContabilidadContext _context;
        private readonly IIngresoService _ingresoService;
        private readonly IEgresoService _egresoService;
        private readonly INominaService _nominaService;
        private readonly ISolicitudGastoService _solicitudService;

        public FacturacionMensualService(
            ContabilidadContext context,
            IIngresoService ingresoService,
            IEgresoService egresoService,
            INominaService nominaService,
            ISolicitudGastoService solicitudService)
        {
            _context = context;
            _ingresoService = ingresoService;
            _egresoService = egresoService;
            _nominaService = nominaService;
            _solicitudService = solicitudService;
        }

        public async Task<FacturacionMensual> GenerarFacturacionMensualAsync(int mes, int año)
        {
            var ingresos = await _ingresoService.ObtenerTodosAsync();
            var egresos = await _egresoService.ObtenerTodosAsync();
            var nominas = await _nominaService.ObtenerNominasDelMesAsync(mes, año);
            var solicitudes = await _solicitudService.ObtenerSolicitudesAprobadas();

            var totalIngresos = ingresos
                .Where(i => i.FechaRegistro.Month == mes && i.FechaRegistro.Year == año)
                .Sum(i => i.Monto);

            var totalEgresos = egresos
                .Where(e => e.FechaRegistro.Month == mes && e.FechaRegistro.Year == año)
                .Sum(e => e.Monto);

            var totalNominas = nominas.Sum(n => n.MontoNeto);

            var totalSolicitudes = solicitudes
                .Where(s => s.FechaAprobacion.HasValue && s.FechaAprobacion.Value.Month == mes && s.FechaAprobacion.Value.Year == año)
                .Sum(s => s.MontoSolicitado);

            var facturacion = new FacturacionMensual
            {
                Mes = mes,
                Año = año,
                TotalIngresos = totalIngresos,
                TotalEgresos = totalEgresos,
                TotalNominas = totalNominas,
                TotalSolicitudesAprobadas = totalSolicitudes,
                UtilidadBruta = totalIngresos - totalEgresos,
                UtilidadNeta = totalIngresos - totalEgresos - totalNominas - totalSolicitudes,
                FechaGeneracion = DateTime.UtcNow,
                Estado = "Cerrado"
            };

            _context.FacturacionesMensuales.Add(facturacion);
            await _context.SaveChangesAsync();
            return facturacion;
        }

        public async Task<FacturacionMensual> ObtenerFacturacionAsync(int mes, int año)
        {
            var facturacion = _context.FacturacionesMensuales
                .FirstOrDefault(f => f.Mes == mes && f.Año == año);

            return await Task.FromResult(facturacion);
        }

        public async Task<List<FacturacionMensual>> ObtenerTodasAsync()
        {
            return await Task.FromResult(_context.FacturacionesMensuales.ToList());
        }
    }
}
