using Finanzas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Domain.Persistence.Repositories
{
    public interface IPerfilRepository
    {
        Task<IEnumerable<Perfil>> ListAsync();
        Task<Perfil> FindByIdAsync(int id);
        Task<IEnumerable<Perfil>> FindByUsuarioIdAsync(int usuarioId);
        Task AddAsync(Perfil perfil);
        void Update(Perfil perfilRequest);
        void Remove(Perfil perfil);
    }
}
