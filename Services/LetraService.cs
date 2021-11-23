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
    public class LetraService : ILetraService
    {
        private readonly ILetraRepository _letraRepository;
        private readonly ICarteraRepository _carteraRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LetraService(ILetraRepository letraRepository, ICarteraRepository carteraRepository, IUnitOfWork unitOfWork)
        {
            _letraRepository = letraRepository;
            _carteraRepository = carteraRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<LetraResponse> DeleteAsync(int id)
        {
            var existingLetra = await _letraRepository.FindByIdAsync(id);
            if (existingLetra == null)
            {
                return new LetraResponse("Letra no encontrada");
            }
            try
            {
                _letraRepository.Remove(existingLetra);
                await _unitOfWork.CompleteAsync();

                return new LetraResponse(existingLetra);
            }
            catch (Exception ex)
            {
                return new LetraResponse($"Un error ocurrio al eliminar la letra: {ex.Message}");
            }
        }

        public async Task<LetraResponse> GetById(int id)
        {
            var existingLetra = await _letraRepository.FindByIdAsync(id);
            if (existingLetra == null)
            {
                return new LetraResponse("Letra no encontrada");
            }
            return new LetraResponse(existingLetra);
        }

        public async Task<IEnumerable<Letra>> ListAsync()
        {
            return await _letraRepository.ListAsync();
        }

        public async Task<IEnumerable<Letra>> ListByCarteraId(int carteraId)
        {
            return await _letraRepository.ListByCarteraId(carteraId);
        }

        public async Task<LetraResponse> SaveAsync(int carteraId, Letra letra)
        {
            var existingCartera = await _carteraRepository.FindByIdAsync(carteraId);
            if (existingCartera == null)
            {
                return new LetraResponse("Cartera no encontrada");
            }
            try
            {
                letra.CarteraId = carteraId;
                await _letraRepository.AddAsync(letra);
                await _unitOfWork.CompleteAsync();

                return new LetraResponse(letra);
            }
            catch(Exception ex)
            {
                return new LetraResponse($"Un error ocurrio al guardar la cartera: {ex.Message}");
            }
        }

        public async Task<LetraResponse> UpdateAsync(int id, Letra letraRequest)
        {
            var existingLetra = await _letraRepository.FindByIdAsync(id);
            if (existingLetra == null)
            {
                return new LetraResponse("Letra no encontrada");
            }
            existingLetra.FechaGiro = letraRequest.FechaGiro;
            existingLetra.FechaVencimiento = letraRequest.FechaVencimiento;
            existingLetra.NombreGirador = letraRequest.NombreGirador;
            existingLetra.ValorNominal = letraRequest.ValorNominal;
            try
            {
                _letraRepository.Update(letraRequest);
                await _unitOfWork.CompleteAsync();

                return new LetraResponse(existingLetra);
            }
            catch(Exception ex)
            {
                return new LetraResponse($"Un error ocurrio al actualizar la letra: {ex.Message}");
            }
        }
    }
}
