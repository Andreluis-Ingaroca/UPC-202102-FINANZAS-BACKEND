using Finanzas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Domain.Persistence.Repositories
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> ListAsync();
        Task<Usuario> FindByIdAsync(int id);
        Task AddAsync(Usuario usuario);
        void Update(Usuario usuarioRequest);
        void Remove(Usuario usuario);
    }
}
