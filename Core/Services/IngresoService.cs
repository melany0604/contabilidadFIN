using ContabilidadBackend.Core.DTOs;
using ContabilidadBackend.Core.Entities;
using ContabilidadBackend.Core.Interfaces;
using ContabilidadBackend.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContabilidadBackend.Core.Services
{
    public class IngresoService : IIngresoService
    {
        private readonly GenericRepository<Ingreso> _repository;

        public IngresoService(GenericRepository<Ingreso> repository)
        {
            _repository = repository;
        }

        public async Task<Ingreso> CrearIngresoAsync(IngresoDTO dto)
        {
            var ingreso = new Ingreso
            {
                NroFactura = dto.NroFactura,
                Monto = dto.Monto,
                Concepto = dto.Concepto,
                MetodoPago = dto.MetodoPago,
                IdChoferOVendedor = dto.IdChoferOVendedor
            };

            return await _repository.AddAsync(ingreso);
        }

        public async Task<List<Ingreso>> ObtenerIngresosAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<List<Ingreso>> ObtenerIngresoPorConceptoAsync(string concepto)
        {
            var ingresos = await _repository.GetAllAsync();
            return ingresos.Where(x => x.Concepto == concepto).ToList();
        }
    }
}
