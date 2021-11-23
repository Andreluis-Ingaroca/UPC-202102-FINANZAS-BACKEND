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
    public class PerfilRepository : BaseRepository, IPerfilRepository
    {
        public PerfilRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Perfil perfil)
        {
            await _context.Perfiles.AddAsync(perfil);
        }

        public async Task<Perfil> FindByIdAsync(int id)
        {
            return await _context.Perfiles.FindAsync(id);
        }

        public async Task<IEnumerable<Perfil>> FindByUsuarioIdAsync(int usuarioId)
        {
            return await _context.Perfiles.
                Where(p => p.UsuarioId == usuarioId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Perfil>> ListAsync()
        {
            return await _context.Perfiles.ToListAsync();
        }

        public void Remove(Perfil perfil)
        {
            _context.Perfiles.Remove(perfil);
        }

        public void Update(Perfil perfilRequest)
        {
            _context.Perfiles.Update(perfilRequest);
        }
    }
}
