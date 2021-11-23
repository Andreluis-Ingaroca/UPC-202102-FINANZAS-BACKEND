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
    public class CostoRepository : BaseRepository, ICostoRepository
    {
        public CostoRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsdync(Costo costo)
        {
            await _context.Costos.AddAsync(costo);
        }

        public async Task<Costo> FindByIdAsync(int id)
        {
            return await _context.Costos.FindAsync(id);
        }

        public async Task<IEnumerable<Costo>> ListAsync()
        {
            return await _context.Costos.ToListAsync();
        }

        public void Remove(Costo costo)
        {
            _context.Costos.Remove(costo);
        }

        public void Update(Costo costoRequest)
        {
            _context.Costos.Update(costoRequest);
        }
    }
}
