using Finanzas.Domain.Models;
using Finanzas.Domain.Persistence.Repositories;
using Finanzas.Domain.Services;
using Finanzas.Domain.Services.Communications;
using Finanzas.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Services
{
    public class CarteraService : ICarteraService
    {
        private readonly ICarteraRepository _carteraRepository;
        private readonly IPerfilRepository _perfilRepository;
        private readonly IOperacionCarteraRepository _operacionCarteraRepository;
        private readonly ITasaRepository _tasaRepository;
        private readonly IOperacionRepository _operacionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CarteraService(ICarteraRepository carteraRepository, 
            IPerfilRepository perfilRepository, IUnitOfWork unitOfWork
            ,IOperacionCarteraRepository operacionCarteraRepository, ITasaRepository tasaRepository,
            IOperacionRepository operacionRepository)
        {
            _carteraRepository = carteraRepository;
            _perfilRepository = perfilRepository;
            _unitOfWork = unitOfWork;
            _operacionCarteraRepository = operacionCarteraRepository;
            _tasaRepository = tasaRepository;
            _operacionRepository = operacionRepository;
        }


        public async Task<IEnumerable<ToShowOperacionesResource>> ListOperacionesByCarteraId(int carteraId)
        {
            IEnumerable<OperacionCartera> operacionCarteras = await _operacionCarteraRepository.ListByCarteraId(carteraId);
            if (operacionCarteras == null)
            {
                return null;
            }
            List<OperacionCartera> operacionCarterasList = operacionCarteras.ToList();
            List<ToShowOperacionesResource> toShowOperacionesResources = new List<ToShowOperacionesResource>();

            operacionCarterasList.ForEach(async oc =>
            {
                ToShowOperacionesResource toAdd = new ToShowOperacionesResource();
                toAdd.Id = oc.OperacionId;
                toAdd.TCEACartera = oc.TCEACartera;
                var operacion = await _operacionRepository.FindByIdAsync(oc.OperacionId);
                toAdd.FechaDescuento = operacion.FechaDescuento;
                var tasa = await _tasaRepository.FindByIdAsync(operacion.TasaId);
                toAdd.Nominal = tasa.Nominal;
                toAdd.TasaMonto = tasa.Monto;
                toShowOperacionesResources.Add(toAdd);
            });
            IEnumerable<ToShowOperacionesResource> toShowOperacionesResourcesToReturn = toShowOperacionesResources.AsEnumerable();
            return toShowOperacionesResourcesToReturn;
        }

        public async Task<CarteraResponse> DeleteAsync(int id)
        {
            var existingCartera = await _carteraRepository.FindByIdAsync(id);
            if (existingCartera == null)
            {
                return new CarteraResponse("Cartera no encontrada");
            }
            try
            {
                _carteraRepository.Remove(existingCartera);
                await _unitOfWork.CompleteAsync();

                return new CarteraResponse(existingCartera);
            }
            catch(Exception ex)
            {
                return new CarteraResponse($"Un error ocurrio al eliminar la cartera {ex.Message}");
            }
        }

        public async Task<CarteraResponse> GetByIdAsync(int id)
        {
            var existingCartera = await _carteraRepository.FindByIdAsync(id);
            if (existingCartera == null)
            {
                return new CarteraResponse("Cartera no encontrada");
            }
            return new CarteraResponse(existingCartera);
        }

        public async Task<IEnumerable<Cartera>> ListAsync()
        {
            return await _carteraRepository.ListAsync();
        }

        public async Task<IEnumerable<Cartera>> ListByPerfilIdAsync(int perfilId)
        {
            return await _carteraRepository.ListByPerfilId(perfilId);
        }

        public async Task<CarteraResponse> SaveAsync(int perfilId, Cartera cartera)
        {
            var existingPerfil = await _perfilRepository.FindByIdAsync(perfilId);
            if (existingPerfil == null)
            {
                return new CarteraResponse("Perfil no encontrado");
            }
            try
            {
                cartera.PerfilId = perfilId;
                await _carteraRepository.AddAsync(cartera);
                await _unitOfWork.CompleteAsync();
                return new CarteraResponse(cartera);
            }
            catch(Exception ex)
            {
                return new CarteraResponse($"Un error ocurrio al guardar la cartera {ex.Message}");
            }
        }

        public async Task<CarteraResponse> UpdateAsync(int id, Cartera cartera)
        {
            var existingCartera = await _carteraRepository.FindByIdAsync(id);
            if (existingCartera == null)
            {
                return new CarteraResponse("Cartera no encontrada");
            }
            existingCartera.Nombre = cartera.Nombre;
            try
            {
                _carteraRepository.Update(existingCartera);
                await _unitOfWork.CompleteAsync();

                return new CarteraResponse(existingCartera);
            }
            catch(Exception ex)
            {
                return new CarteraResponse($"Un error ocurrio al actualizar la cartera: {ex.Message}");
            }
        }
    }
}
