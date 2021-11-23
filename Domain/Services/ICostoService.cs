using Finanzas.Domain.Models;
using Finanzas.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Domain.Services
{
    public interface ICostoService
    {
        Task<IEnumerable<Costo>> ListAsync();
        Task<CostoResponse> GetById(int id);
        Task<CostoResponse> SaveAsync(Costo costo);
        Task<CostoResponse> UpdateAsync(int id, Costo costoRequest);
        Task<CostoResponse> DeleteAsync(int id);
    }
}
