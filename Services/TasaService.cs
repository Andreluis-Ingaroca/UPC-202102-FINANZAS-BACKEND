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
    public class TasaService : ITasaService
    {
        private readonly ITasaRepository _tasaRepository;
        private readonly IPeriodoRepository _periodoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TasaService(ITasaRepository tasaRepository, IPeriodoRepository periodoRepository, IUnitOfWork unitOfWork)
        {
            _tasaRepository = tasaRepository;
            _periodoRepository = periodoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<TasaResponse> DeleteAsync(int id)
        {
            var existingTasa = await _tasaRepository.FindByIdAsync(id);
            if (existingTasa == null)
            {
                return new TasaResponse("Tasa no encontrada");
            }
            try
            {
                _tasaRepository.Remove(existingTasa);
                await _unitOfWork.CompleteAsync();

                return new TasaResponse(existingTasa);
            }
            catch(Exception ex)
            {
                return new TasaResponse($"Un error ocurrio al eliminar la tasa: {ex.Message}");
            }
        }

        public async Task<TasaResponse> GetByIdAsync(int id)
        {
            var existingTasa = await _tasaRepository.FindByIdAsync(id);
            if (existingTasa == null)
            {
                return new TasaResponse("Tasa no encontrada");
            }
            return new TasaResponse(existingTasa);
        }

        public async Task<IEnumerable<Tasa>> ListAsync()
        {
            return await _tasaRepository.ListAsync();
        }

        public async Task<TasaResponse> SaveAsync(int periodoId, int periodoCapitalizacionId, Tasa tasa)
        {
            var existingPeriodo = await _periodoRepository.FindByIdAsync(periodoId);
            if (existingPeriodo == null)
            {
                return new TasaResponse("Periodo no encontrado");
            }

            if (tasa.Nominal)
            {
                var existingPeriodoCapitalizacion = await _periodoRepository.FindByIdAsync(periodoCapitalizacionId);
                if (existingPeriodoCapitalizacion == null)
                {
                    return new TasaResponse("Periodo de capitalizacion no encontrado");
                }
                tasa.PeriodoCapitalizacionId = periodoCapitalizacionId;
            }
            else
            {
                tasa.PeriodoCapitalizacionId = 1;
            }

            try
            {
                tasa.PeriodoId = periodoId;
                await _tasaRepository.AddAsync(tasa);
                await _unitOfWork.CompleteAsync();
                return new TasaResponse(tasa);
            }
            catch(Exception ex)
            {
                return new TasaResponse($"Un error ocurrio al guardar la tasa: {ex.Message}");
            }

        }

        public async Task<TasaResponse> UpdateAsync(int id, Tasa tasaRequest)
        {
            var existingTasa = await _tasaRepository.FindByIdAsync(id);
            if (existingTasa == null)
            {
                return new TasaResponse("Tasa no encontrada");
            }
            existingTasa.Monto = tasaRequest.Monto;
            existingTasa.Nominal = tasaRequest.Nominal;
            try
            {
                _tasaRepository.Update(existingTasa);
                await _unitOfWork.CompleteAsync();

                return new TasaResponse(existingTasa);
            }
            catch (Exception ex)
            {
                return new TasaResponse($"Un error ocurrio al eliminar la tasa: {ex.Message}");
            }
        }
    }
}
