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
    public class OperacionService : IOperacionService
    {
        private readonly IOperacionRepository _operacionRepository;
        private readonly ITasaRepository _tasaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OperacionService(IOperacionRepository operacionRepository, IUnitOfWork unitOfWork, ITasaRepository tasaRepository)
        {
            _operacionRepository = operacionRepository;
            _unitOfWork = unitOfWork;
            _tasaRepository = tasaRepository;
        }

        public async Task<OperacionResponse> DeleteAsync(int id)
        {
            var existingOperacion = await _operacionRepository.FindByIdAsync(id);
            if (existingOperacion == null)
            {
                return new OperacionResponse("Operacion no encontrada");
            }
            try
            {
                _operacionRepository.Remove(existingOperacion);
                await _unitOfWork.CompleteAsync();

                return new OperacionResponse(existingOperacion);
            }
            catch(Exception ex)
            {
                return new OperacionResponse($"Un error ocurrio al eliminar la operacion: {ex.Message}");
            }
        }

        public async Task<OperacionResponse> GetById(int id)
        {
            var existingOperacion = await _operacionRepository.FindByIdAsync(id);
            if (existingOperacion == null)
            {
                return new OperacionResponse("Operacion no encontrada");
            }
            return new OperacionResponse(existingOperacion);
        }

        public async Task<IEnumerable<Operacion>> ListAsync()
        {
            return await _operacionRepository.ListAsync();
        }

        public async Task<OperacionResponse> SaveAsync(int tasaId, Operacion operacion)
        {
            var existingTasa = await _tasaRepository.FindByIdAsync(tasaId);
            if (existingTasa == null)
            {
                return new OperacionResponse("Tasa no encontrada");
            }
            try
            {
                operacion.TasaId = tasaId;
                await _operacionRepository.AddAsync(operacion);
                await _unitOfWork.CompleteAsync();

                return new OperacionResponse(operacion);
            }
            catch (Exception ex)
            {
                return new OperacionResponse($"Un error ocurrio al guardar la operacion: {ex.Message}");
            }
        }

        public async Task<OperacionResponse> UpdateAsync(int id, Operacion operacionRequest)
        {
            var existingOperacion = await _operacionRepository.FindByIdAsync(id);
            if (existingOperacion == null)
            {
                return new OperacionResponse("Operacion no encontrada");
            }
            existingOperacion.AñoCalendario = operacionRequest.AñoCalendario;
            existingOperacion.Retencion = operacionRequest.Retencion;
            existingOperacion.RetencionPorcentaje = operacionRequest.RetencionPorcentaje;
            try
            {
                _operacionRepository.Update(operacionRequest);
                await _unitOfWork.CompleteAsync();

                return new OperacionResponse(existingOperacion);
            }
            catch(Exception ex)
            {
                return new OperacionResponse($"Un error ocurrio al actualizar la operacion: {ex.Message}"); 
            }
        }
    }
}
