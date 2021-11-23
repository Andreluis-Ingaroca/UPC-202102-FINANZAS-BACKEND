using AutoMapper;
using Finanzas.Domain.Models;
using Finanzas.Domain.Persistence.Repositories;
using Finanzas.Domain.Services;
using Finanzas.Domain.Services.Communications;
using Finanzas.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Finanzas.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppSetings _appSettings;

        public UsuarioService(IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork, IOptions<AppSetings> appSetings, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _unitOfWork = unitOfWork;
            _appSettings = appSetings.Value;
        }

        public async Task<AuthenticationResponse> Authenticate(AuthenticationRequest request)
        {
            IEnumerable<Usuario> users= await ListAsync();
            if (users == null)
                return null;
            var user = users.ToList().SingleOrDefault(x =>
                x.Correo == request.Correo && x.Contraseña == request.Contraseña
                );
            if (user == null) return null;

            var token = GenerateJwtToken(user);
            return new AuthenticationResponse(user, token);
        }

        public async Task<UsuarioResponse> DeleteAsync(int id)
        {
            var existingUsuario = await _usuarioRepository.FindByIdAsync(id);
            if (existingUsuario == null)
            {
                return new UsuarioResponse("Usuario no encontrado");
            }
            try
            {
                _usuarioRepository.Remove(existingUsuario);
                await _unitOfWork.CompleteAsync();

                return new UsuarioResponse(existingUsuario);
            }
            catch(Exception ex)
            {
                return new UsuarioResponse($"Error al eliminar usuario: {ex.Message}");
            }
        }

        public async Task<UsuarioResponse> GetById(int id)
        {
            var existingUsuario = await _usuarioRepository.FindByIdAsync(id);
            if (existingUsuario == null)
            {
                return new UsuarioResponse("Usuario no encontrado");
            }
            return new UsuarioResponse(existingUsuario);
        }

        public async Task<IEnumerable<Usuario>> ListAsync()
        {
            return await _usuarioRepository.ListAsync();
        }

        public async Task<UsuarioResponse> SaveAsync(Usuario usuario)
        {
            try
            {
                bool different = true;
                IEnumerable<Usuario> users = await ListAsync();
                if (users != null)
                    users.ToList().ForEach(savedUser =>
                    {
                        if (savedUser.Correo == usuario.Correo)
                            different = false;
                    });

                if (!different)
                    return new UsuarioResponse("No pueden existir dos users con el mismo mail");

                await _usuarioRepository.AddAsync(usuario);
                await _unitOfWork.CompleteAsync();

                return new UsuarioResponse(usuario);

            }
            catch(Exception ex)
            {
                return new UsuarioResponse($"Error al guardar usuario: {ex.Message}");
            }
        }

        public async Task<UsuarioResponse> UpdateAsync(int id, Usuario usuarioRequest)
        {
            var existingUsuario = await _usuarioRepository.FindByIdAsync(id);
            if (existingUsuario == null)
            {
                return new UsuarioResponse("Usuario no encontrado");
            }
            existingUsuario.Contraseña = usuarioRequest.Contraseña;
            existingUsuario.Correo = usuarioRequest.Correo;
            try
            {
                _usuarioRepository.Update(existingUsuario);
                await _unitOfWork.CompleteAsync();

                return new UsuarioResponse(existingUsuario);
            }
            catch (Exception ex)
            {
                return new UsuarioResponse($"Error al actualizar usuario: {ex.Message}");
            }
        }

        private string GenerateJwtToken(Usuario user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokentDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokentDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
