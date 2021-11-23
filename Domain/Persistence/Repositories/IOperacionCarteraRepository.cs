using Finanzas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Domain.Persistence.Repositories
{
    public interface IOperacionCarteraRepository
    {
        Task<IEnumerable<OperacionCartera>> ListAsync();
        Task<IEnumerable<OperacionCartera>> ListByCarteraId(int carteraId);
        Task<IEnumerable<OperacionCartera>> ListByOperacionId(int operacionId);
        Task<OperacionCartera> FindByOperacionIdAndCarteraId(int operacionId, int carteraId);
        Task AddAsync(OperacionCartera operacionCartera);
        void Remove(OperacionCartera operacionCartera);
        Task AssignOperacionCartera(int operacionId, int carteraId, float valorRecibidoTotal, float tceaCartera);
        void UnassignOperacionCartera(int operacionId, int carteraId);
    }
}
