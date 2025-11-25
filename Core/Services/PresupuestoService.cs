using ContabilidadBackend.Core.DTOs;
using ContabilidadBackend.Core.Entities;
using ContabilidadBackend.Core.Interfaces;
using ContabilidadBackend.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContabilidadBackend.Core.Services
{
    public class PresupuestoService : IPresupuestoService
    {
        private readonly GenericRepository<Presupuesto> _repository;

        public PresupuestoService(GenericRepository<Presupuesto> repository)
        {
            _repository = repository;
        }

        public async Task<Presupuesto> CrearPresupuestoAsync(PresupuestoDTO dto)
        {
            var presupuesto = new Presupuesto
            {
                Departamento = dto.Departamento,
                MontoTotal = dto.MontoTotal,
                Mes = dto.Mes,
                Anio = dto.Anio
            };

            return await _repository.AddAsync(presupuesto);
        }

        public async Task<decimal> VerificaDisponibilidadAsync(string departamento, decimal montoSolicitado)
        {
            var hoy = DateTime.UtcNow;
            var presupuestos = await _repository.GetAllAsync();
            var presupuesto = presupuestos
                .FirstOrDefault(x => x.Departamento == departamento && 
                                     x.Mes == hoy.Month && 
                                     x.Anio == hoy.Year &&
                                     x.Estado == "Activo");

            if (presupuesto == null) return 0;

            return presupuesto.MontoTotal - presupuesto.MontoEjecutado;
        }

        public async Task<List<Presupuesto>> ObtenerPresupuestosAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<bool> ActualizarPresupuestoAsync(long idPresupuesto, decimal montoEjecutado)
        {
            var presupuesto = await _repository.GetByIdAsync(idPresupuesto);
            if (presupuesto == null) return false;

            presupuesto.MontoEjecutado += montoEjecutado;
            await _repository.UpdateAsync(presupuesto);
            return true;
        }
    }
}
