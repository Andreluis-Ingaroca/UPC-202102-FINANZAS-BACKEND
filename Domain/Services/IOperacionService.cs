using Finanzas.Domain.Models;
using Finanzas.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Domain.Services
{
    public interface IOperacionService
    {
        Task<IEnumerable<Operacion>> ListAsync();
        Task<OperacionResponse> GetById(int id);
        Task<OperacionResponse> SaveAsync(int tasaId, Operacion operacion);
        Task<OperacionResponse> UpdateAsync(int id, Operacion operacionRequest);
        Task<OperacionResponse> DeleteAsync(int id);
    }
}
