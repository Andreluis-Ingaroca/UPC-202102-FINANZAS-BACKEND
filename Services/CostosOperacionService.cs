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
    public class CostosOperacionService : ICostosOperacionService
    {
        private readonly ICostosOperacionRepository _costosOperacionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CostosOperacionService(ICostosOperacionRepository costosOperacionRepository, IUnitOfWork unitOfWork)
        {
            _costosOperacionRepository = costosOperacionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CostosOperacionResponse> AssignCostoOperacionAsync(int costoId, int operacionId, CostosOperacion costosOperacion)
        {
            try
            {
                await _costosOperacionRepository.AssignCostoOperacion(costoId, operacionId, costosOperacion.Monto, costosOperacion.CostoInicial, costosOperacion.Porcentaje);
                await _unitOfWork.CompleteAsync();

                CostosOperacion costosOperacion1 = await _costosOperacionRepository.FindByCostoIdAndOperacionId(costoId, operacionId);

                return new CostosOperacionResponse(costosOperacion1);
            }
            catch (Exception ex)
            {
                return new CostosOperacionResponse($"Error al asignar el costo con operacion: {ex.Message}");
            }
        }

        public async Task<IEnumerable<CostosOperacion>> ListAsync()
        {
            return await _costosOperacionRepository.ListAsync();
        }

        public async Task<IEnumerable<CostosOperacion>> ListByOperacionIdAsync(int operacionId)
        {
            return await _costosOperacionRepository.ListByOperacionId(operacionId);
        }

        public async Task<CostosOperacionResponse> UnassignCostoOperacionAsync(int costoId, int operacionId)
        {
            try
            {
                CostosOperacion costosOperacion1 = await _costosOperacionRepository.FindByCostoIdAndOperacionId(costoId, operacionId);

                await _costosOperacionRepository.UnassignCostoOperacion(costoId,operacionId);
                await _unitOfWork.CompleteAsync();


                return new CostosOperacionResponse(costosOperacion1);
            }
            catch (Exception ex)
            {
                return new CostosOperacionResponse($"Error al desasignar el costo con operacion: {ex.Message}");
            }
        }
    }
}
