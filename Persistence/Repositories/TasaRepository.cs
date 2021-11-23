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
    public class TasaRepository : BaseRepository, ITasaRepository
    {
        public TasaRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Tasa tasa)
        {
            await _context.Tasas.AddAsync(tasa);
        }

        public async Task<Tasa> FindByIdAsync(int id)
        {
            return await _context.Tasas.FindAsync(id);
        }

        public async Task<IEnumerable<Tasa>> ListAsync()
        {
            return await _context.Tasas.ToListAsync();
        }

        public void Remove(Tasa tasa)
        {
            _context.Tasas.Remove(tasa);
        }

        public void Update(Tasa tasaRequest)
        {
            _context.Tasas.Update(tasaRequest);
        }
    }
}
