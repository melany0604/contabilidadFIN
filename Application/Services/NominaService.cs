namespace ContabilidadBackend.Application.Services
{
    using ContabilidadBackend.Core.DTOs;
    using ContabilidadBackend.Core.Entities;
    using ContabilidadBackend.Core.Interfaces;
    using ContabilidadBackend.Infrastructure.Data;

    public class NominaService : INominaService
    {
        private readonly ContabilidadContext _context;

        public NominaService(ContabilidadContext context)
        {
            _context = context;
        }

        public async Task<Nomina> RegistrarNominaAsync(NominaDTO nominaDto)
        {
            var nomina = new Nomina
            {
                IdEmpleado = nominaDto.IdEmpleado,
                NombreEmpleado = nominaDto.NombreEmpleado,
                Salario = nominaDto.Salario,
                Deducciones = nominaDto.Deducciones,
                Bonificacion = nominaDto.Bonificacion,
                MontoNeto = nominaDto.Salario - nominaDto.Deducciones + nominaDto.Bonificacion,
                Estado = "Pendiente",
                Mes = nominaDto.Mes,
                Año = nominaDto.Anio,
                FechaGeneracion = DateTime.UtcNow
            };

            _context.Nominas.Add(nomina);
            await _context.SaveChangesAsync();
            return nomina;
        }

        public async Task<List<Nomina>> ObtenerNominasDelMesAsync(int mes, int año)
        {
            return await Task.FromResult(_context.Nominas
                .Where(n => n.Mes == mes && n.Año == año)
                .ToList());
        }

        public async Task<decimal> ObtenerTotalNominasDelMesAsync(int mes, int año)
        {
            var total = _context.Nominas
                .Where(n => n.Mes == mes && n.Año == año)
                .Sum(n => n.MontoNeto);

            return await Task.FromResult(total);
        }

        public async Task<List<Nomina>> ObtenerTodasAsync()
        {
            return await Task.FromResult(_context.Nominas.ToList());
        }

        public async Task<Nomina> ObtenerPorIdAsync(int id)
        {
            return await _context.Nominas.FindAsync(id);
        }
    }
}


