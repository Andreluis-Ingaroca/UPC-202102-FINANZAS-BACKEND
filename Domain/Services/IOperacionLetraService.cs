using Finanzas.Domain.Models;
using Finanzas.Domain.Services.Communications;
using Finanzas.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Domain.Services
{
    public interface IOperacionLetraService
    {
        Task<IEnumerable<OperacionLetra>> ListAsync();
        Task<IEnumerable<OperacionLetra>> ListByOperacionIdAsync(int operacionId);
        Task<IEnumerable<ResultOperacionLetraResource>> ListLetraOperacionesByCarteraId(int operacionId);
        Task<IEnumerable<OperacionLetra>> ListByLetraIdAsync(int letraId);
        Task<OperacionLetraResponse> GetByLetraIdAndOperacionAsync(int letraId, int operacionId);
        Task<OperacionLetraResponse> AssignOperacionLetraAsync(int operacionId, int letraId);
        Task<OperacionLetraResponse> UnassignOperacionLetraAsync(int operacionId, int letraId);
    } 
}
