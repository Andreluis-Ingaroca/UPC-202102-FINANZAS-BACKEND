using Finanzas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Domain.Persistence.Repositories
{
    public interface IOperacionRepository
    {
        Task<IEnumerable<Operacion>> ListAsync();
        Task AddAsync(Operacion operacion);
        Task<Operacion> FindByIdAsync(int id);
        void Update(Operacion operacionRequest);
        void Remove(Operacion operacion);
    }
}
