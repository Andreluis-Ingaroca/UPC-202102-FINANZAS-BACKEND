using Finanzas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Domain.Persistence.Repositories
{
    public interface ICarteraRepository
    {
        Task<IEnumerable<Cartera>> ListAsync();
        Task<IEnumerable<Cartera>> ListByPerfilId(int perfilId);
        Task AddAsync(Cartera cartera);
        Task<Cartera> FindByIdAsync(int id);
        void Update(Cartera carteraRequest);
        void Remove(Cartera carteraRequest);
    }
}
