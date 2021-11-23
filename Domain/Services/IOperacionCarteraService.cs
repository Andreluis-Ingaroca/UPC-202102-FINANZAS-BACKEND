using Finanzas.Domain.Models;
using Finanzas.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Domain.Services
{
    public interface IOperacionCarteraService
    {
        Task<IEnumerable<OperacionCartera>> ListAsync();
        Task<IEnumerable<OperacionCartera>> ListByCarteraAsync(int carteraId);
        Task<OperacionCarteraResponse> GetOperacionCarteraAsync(int operacionId, int carteraId);
        Task<OperacionCarteraResponse> AssignOperacionCarteraAsync(int operacionId, int carteraId);
        Task<OperacionCarteraResponse> UnassignOperacionCarteraAsync(int operacionId, int carteraId);

    }
}
