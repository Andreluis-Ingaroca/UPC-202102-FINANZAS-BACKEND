using Finanzas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Domain.Persistence.Repositories
{
    public interface ICostosOperacionRepository
    {
        Task<IEnumerable<CostosOperacion>> ListAsync();
        Task<IEnumerable<CostosOperacion>> ListByCostoId(int costoId);
        Task<IEnumerable<CostosOperacion>> ListByOperacionId(int operacionId);
        Task<CostosOperacion> FindByCostoIdAndOperacionId(int costoId, int operacionId);
        Task AddAsync(CostosOperacion costosOperacion);
        void Remove(CostosOperacion costosOperacion);
        Task AssignCostoOperacion(int costoId, int operacionId, float monto, bool costoInicial, bool porcentaje);
        Task UnassignCostoOperacion(int costoId, int operacionId);
    }
}
