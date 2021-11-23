using Finanzas.Domain.Models;
using Finanzas.Domain.Persistence.Context;
using Finanzas.Domain.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Persistence.Repositories
{
    public class UsuarioRepository : BaseRepository, IUsuarioRepository
    {
        public UsuarioRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
        }

        public async Task<Usuario> FindByIdAsync(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<IEnumerable<Usuario>> ListAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public void Remove(Usuario usuario)
        {
            _context.Usuarios.Remove(usuario);
        }

        public void Update(Usuario usuarioRequest)
        {
            _context.Usuarios.Update(usuarioRequest);
        }
    }
}
