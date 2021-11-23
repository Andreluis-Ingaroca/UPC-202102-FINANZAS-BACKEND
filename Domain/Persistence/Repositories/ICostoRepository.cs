using Finanzas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Domain.Persistence.Repositories
{
    public interface ICostoRepository
    {
        Task<IEnumerable<Costo>> ListAsync();
        Task AddAsdync(Costo costo);
        Task<Costo> FindByIdAsync(int id);
        void Update(Costo costoRequest);
        void Remove(Costo costo);
    }
}
