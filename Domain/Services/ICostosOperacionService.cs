using Finanzas.Domain.Models;
using Finanzas.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Domain.Services
{
    public interface ICostosOperacionService
    {
        Task<IEnumerable<CostosOperacion>> ListAsync();
        Task<IEnumerable<CostosOperacion>> ListByOperacionIdAsync(int operacionId);
        Task<CostosOperacionResponse> AssignCostoOperacionAsync(int costoId, int operacionId, CostosOperacion costosOperacion);
        Task<CostosOperacionResponse> UnassignCostoOperacionAsync(int costoId, int operacionId);
    }
}
