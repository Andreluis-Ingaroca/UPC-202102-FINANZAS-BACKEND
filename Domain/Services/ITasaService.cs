using Finanzas.Domain.Models;
using Finanzas.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Domain.Services
{
    public interface ITasaService
    {
        Task<IEnumerable<Tasa>> ListAsync();
        Task<TasaResponse> GetByIdAsync(int id);
        Task<TasaResponse> SaveAsync(int periodoId, int periodoCapitalizacionId, Tasa tasa);
        Task<TasaResponse> UpdateAsync(int id, Tasa tasaRequest);
        Task<TasaResponse> DeleteAsync(int id);

    }
}
