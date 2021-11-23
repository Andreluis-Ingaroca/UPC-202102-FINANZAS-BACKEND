using Finanzas.Domain.Models;
using Finanzas.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Domain.Services
{
    public interface ILetraService
    {
        Task<IEnumerable<Letra>> ListAsync();
        Task<IEnumerable<Letra>> ListByCarteraId(int carteraId);
        Task<LetraResponse> GetById(int id);
        Task<LetraResponse> SaveAsync(int carteraId, Letra letra);
        Task<LetraResponse> UpdateAsync(int id, Letra letraRequest);
        Task<LetraResponse> DeleteAsync(int id);
    }
}
