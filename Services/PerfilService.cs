using Finanzas.Domain.Models;
using Finanzas.Domain.Persistence.Repositories;
using Finanzas.Domain.Services;
using Finanzas.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Services
{
    public class PerfilService : IPerfilService
    {
        private readonly IPerfilRepository _perfilRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PerfilService(IPerfilRepository perfilRepository, IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork)
        {
            _perfilRepository = perfilRepository;
            _usuarioRepository = usuarioRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PerfilResponse> DeleteAsync(int id)
        {
            var existingPerfil = await _perfilRepository.FindByIdAsync(id);
            if (existingPerfil == null)
            {
                return new PerfilResponse("Perfil no encontrado");
            }
            try
            {
                _perfilRepository.Remove(existingPerfil);
                await _unitOfWork.CompleteAsync();

                return new PerfilResponse(existingPerfil);
            }
            catch(Exception ex)
            {
                return new PerfilResponse($"Un error ocurrio al eliminar el perfil: {ex.Message}");
            }
        }

        public async Task<PerfilResponse> GetById(int id)
        {
            var existingPerfil = await _perfilRepository.FindByIdAsync(id);
            if (existingPerfil == null)
            {
                return new PerfilResponse("Perfil no encontrado");
            }
            return new PerfilResponse(existingPerfil);
        }

        public async Task<PerfilResponse> GetByUserId(int userId)
        {
            var perfil = await _perfilRepository.FindByUsuarioIdAsync(userId);
            return new PerfilResponse(perfil.First());
        }

        public async Task<IEnumerable<Perfil>> ListAsync()
        {
            return await _perfilRepository.ListAsync();
        }

        public async Task<PerfilResponse> SaveAsync(int userId, Perfil perfil)
        {
            var existingUser = await _usuarioRepository.FindByIdAsync(userId);
            if (existingUser == null)
            {
                return new PerfilResponse("Usuario no encontrado");
            }
            try
            {
                perfil.UsuarioId = userId;
                await _perfilRepository.AddAsync(perfil);
                await _unitOfWork.CompleteAsync();

                return new PerfilResponse(perfil);
            }
            catch(Exception ex)
            {
                return new PerfilResponse($"Un error ocurrio al guardar el perfil: {ex.Message}");
            }
        }

        public async Task<PerfilResponse> UpdateAsync(int id, Perfil perfilRequest)
        {
            var existingPerfil = await _perfilRepository.FindByIdAsync(id);
            if (existingPerfil == null)
            {
                return new PerfilResponse("Perfil no encontrado");
            }
            existingPerfil.Doi = perfilRequest.Doi;
            existingPerfil.Nombre = perfilRequest.Nombre;
            try
            {
                _perfilRepository.Update(existingPerfil);
                await _unitOfWork.CompleteAsync();

                return new PerfilResponse(existingPerfil);
            }
            catch(Exception ex)
            {
                return new PerfilResponse($"Un error ocurrio al intentar actualizar el perfil: {ex.Message}");
            }
        }
    }
}
