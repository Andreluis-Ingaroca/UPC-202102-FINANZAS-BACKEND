using Finanzas.Domain.Models;
using Finanzas.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Domain.Services
{
    public interface IPerfilService
    {
        Task<IEnumerable<Perfil>> ListAsync();
        Task<PerfilResponse> GetById(int id);
        Task<PerfilResponse> GetByUserId(int userId);
        Task<PerfilResponse> SaveAsync(int userId, Perfil perfil);
        Task<PerfilResponse> UpdateAsync(int id, Perfil perfilRequest);
        Task<PerfilResponse> DeleteAsync(int id);
    }
}
