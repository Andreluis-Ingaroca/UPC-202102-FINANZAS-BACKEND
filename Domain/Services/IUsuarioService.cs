using Finanzas.Domain.Models;
using Finanzas.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Domain.Services
{
    public interface IUsuarioService
    {
        Task<AuthenticationResponse> Authenticate(AuthenticationRequest request);
        Task<IEnumerable<Usuario>> ListAsync();
        Task<UsuarioResponse> GetById(int id);
        Task<UsuarioResponse> SaveAsync(Usuario usuario);
        Task<UsuarioResponse> UpdateAsync(int id, Usuario usuarioRequest);
        Task<UsuarioResponse> DeleteAsync(int id);
    }
}
