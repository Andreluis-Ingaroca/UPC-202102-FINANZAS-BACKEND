using Finanzas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Domain.Persistence.Repositories
{
    public interface ITasaRepository
    {
        Task<IEnumerable<Tasa>> ListAsync();
        Task AddAsync(Tasa tasa);
        Task<Tasa> FindByIdAsync(int id);
        void Update(Tasa tasaRequest);
        void Remove(Tasa tasa);
    }
}
