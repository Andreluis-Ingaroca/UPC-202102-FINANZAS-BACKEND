using Finanzas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Domain.Persistence.Repositories
{
    public interface ILetraRepository
    {
        Task<IEnumerable<Letra>> ListAsync();
        Task<IEnumerable<Letra>> ListByCarteraId(int carteraId);
        Task AddAsync(Letra letra);
        Task<Letra> FindByIdAsync(int id);
        void Update(Letra letraRequest);
        void Remove(Letra letra);
    }
}
