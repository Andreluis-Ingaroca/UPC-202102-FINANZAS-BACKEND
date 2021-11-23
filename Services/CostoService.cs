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
    public class CostoService : ICostoService
    {
        private readonly ICostoRepository _costoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CostoService(ICostoRepository costoRepository, IUnitOfWork unitOfWork)
        {
            _costoRepository = costoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CostoResponse> DeleteAsync(int id)
        {
            var existingCosto = await _costoRepository.FindByIdAsync(id);
            if (existingCosto == null)
            {
                return new CostoResponse("Costo no encontrado");
            }
            try
            {
                _costoRepository.Remove(existingCosto);
                await _unitOfWork.CompleteAsync();

                return new CostoResponse(existingCosto);
            }
            catch(Exception ex)
            {
                return new CostoResponse($"Un error ocurrio al eliminar el costo: {ex.Message}");
            }
        }

        public async Task<CostoResponse> GetById(int id)
        {
            var existingCosto = await _costoRepository.FindByIdAsync(id);
            if (existingCosto == null)
            {
                return new CostoResponse("Costo no encontrado");
            }
            return new CostoResponse(existingCosto);
        }

        public async Task<IEnumerable<Costo>> ListAsync()
        {
            return await _costoRepository.ListAsync();
        }

        public async Task<CostoResponse> SaveAsync(Costo costo)
        {
            try
            {
                await _costoRepository.AddAsdync(costo);
                await _unitOfWork.CompleteAsync();

                return new CostoResponse(costo);
            }
            catch(Exception ex)
            {
                return new CostoResponse($"Un error ocurrio al guardar el costo: {ex.Message}");
            }
        }

        public async Task<CostoResponse> UpdateAsync(int id, Costo costoRequest)
        {
            var existingCosto = await _costoRepository.FindByIdAsync(id);
            if (existingCosto == null)
            {
                return new CostoResponse("Costo no encontrado");
            }
            try
            {
                existingCosto.Nombre = costoRequest.Nombre;
                _costoRepository.Update(existingCosto);
                await _unitOfWork.CompleteAsync();

                return new CostoResponse(existingCosto);
            }
            catch (Exception ex)
            {
                return new CostoResponse($"Un error ocurrio al actualizar el costo: {ex.Message}");
            }
        }
    }
}
