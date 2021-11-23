using Finanzas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Domain.Persistence.Repositories
{
    public interface IPeriodoRepository
    {
        Task<IEnumerable<Periodo>> ListAsync();
        Task AddAsync(Periodo periodo);
        Task<Periodo> FindByIdAsync(int id);
        void Update(Periodo periodoRequest);
        void Remove(Periodo periodo);
    }
}
