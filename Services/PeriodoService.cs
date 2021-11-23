using Finanzas.Domain.Models;
using Finanzas.Domain.Persistence.Repositories;
using Finanzas.Domain.Services;
using Finanzas.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Services
{
    public class PeriodoService : IPeriodoService
    {
        private readonly IPeriodoRepository _periodoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PeriodoService(IPeriodoRepository periodoRepository, IUnitOfWork unitOfWork)
        {
            _periodoRepository = periodoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PeriodoResponse> DeleteAsync(int id)
        {
            var existingPeriodo = await _periodoRepository.FindByIdAsync(id);
            if (existingPeriodo == null)
            {
                return new PeriodoResponse("Periodo no encontrado");
            }
            try
            {
                _periodoRepository.Remove(existingPeriodo);
                await _unitOfWork.CompleteAsync();

                return new PeriodoResponse(existingPeriodo);
            }
            catch(Exception ex)
            {
                return new PeriodoResponse($"Un error ocurrio al eliminar el periodo: {ex.Message}");
            }
        }

        public async Task<PeriodoResponse> GetById(int id)
        {
            var existingPeriodo = await _periodoRepository.FindByIdAsync(id);
            if (existingPeriodo == null)
            {
                return new PeriodoResponse("Periodo no encontrado");
            }
            return new PeriodoResponse(existingPeriodo);
        }

        public async Task<IEnumerable<Periodo>> ListAsync()
        {
            return await _periodoRepository.ListAsync();
        }

        public async Task<PeriodoResponse> SaveAsync(Periodo periodo)
        {
            try
            {
                await _periodoRepository.AddAsync(periodo);
                await _unitOfWork.CompleteAsync();

                return new PeriodoResponse(periodo);
            }
            catch(Exception ex)
            {
                return new PeriodoResponse($"Un error ocurrio al guardar el periodo: {ex.Message}");
            }
        }

        public async Task<PeriodoResponse> UpdateAsync(int id, Periodo periodoRequest)
        {
            var existingPeriodo = await _periodoRepository.FindByIdAsync(id);
            if (existingPeriodo == null)
            {
                return new PeriodoResponse("Periodo no encontrado");
            }
            existingPeriodo.Cantidad = periodoRequest.Cantidad;
            existingPeriodo.Nombre = periodoRequest.Nombre;
            try
            {
                _periodoRepository.Update(existingPeriodo);
                await _unitOfWork.CompleteAsync();

                return new PeriodoResponse(existingPeriodo);
            }
            catch (Exception ex)
            {
                return new PeriodoResponse($"Un error ocurrio al actualizar el periodo: {ex.Message}");
            }
        }
    }
}
