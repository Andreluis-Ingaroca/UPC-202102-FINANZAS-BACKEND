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
    public class LetraRepository : BaseRepository, ILetraRepository
    {
        public LetraRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Letra letra)
        {
            await _context.Letras.AddAsync(letra);
        }

        public void Remove(Letra letra)
        {
            _context.Letras.Remove(letra);
        }

        public async Task<Letra> FindByIdAsync(int id)
        {
            return await _context.Letras.FindAsync(id);
        }

        public async Task<IEnumerable<Letra>> ListAsync()
        {
            return await _context.Letras.ToListAsync();
        }

        public async Task<IEnumerable<Letra>> ListByCarteraId(int carteraId)
        {
            return await _context.Letras.
                Where(l => l.CarteraId==carteraId)
                .ToListAsync();
        }

        public void Update(Letra letraRequest)
        {
            _context.Letras.Update(letraRequest);
        }
    }
}
