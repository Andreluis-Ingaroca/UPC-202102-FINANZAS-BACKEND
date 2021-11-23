using Finanzas.Domain.Models;
using Finanzas.Domain.Services.Communications;
using Finanzas.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Domain.Services
{
    public interface ICarteraService
    {
        Task<IEnumerable<Cartera>> ListAsync();
        Task<IEnumerable<Cartera>> ListByPerfilIdAsync(int perfilId);
        Task<IEnumerable<ToShowOperacionesResource>> ListOperacionesByCarteraId(int carteraId);
        Task<CarteraResponse> GetByIdAsync(int id);
        Task<CarteraResponse> SaveAsync(int perfilId, Cartera cartera);
        Task<CarteraResponse> UpdateAsync(int id, Cartera cartera);
        Task<CarteraResponse> DeleteAsync(int id);

    }
}
