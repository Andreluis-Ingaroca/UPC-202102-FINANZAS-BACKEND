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
    public class CarteraRepository : BaseRepository, ICarteraRepository
    {
        public CarteraRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Cartera cartera)
        {
            await _context.Carteras.AddAsync(cartera);
        }

        public async Task<Cartera> FindByIdAsync(int id)
        {
            return await _context.Carteras.FindAsync(id);
        }

        public async Task<IEnumerable<Cartera>> ListAsync()
        {
            return await _context.Carteras.Include(c=>  c.Letras).Include(c=> c.operacionCarteras).ToListAsync();
        }

        public async Task<IEnumerable<Cartera>> ListByPerfilId(int perfilId)
        {
            return await _context.Carteras.
                Where(c => c.PerfilId==perfilId)
                .Include(c => c.Letras)
                .Include(c => c.operacionCarteras)
                .ToListAsync();
        }

        public void Remove(Cartera carteraRequest)
        {
            _context.Carteras.Remove(carteraRequest);
        }

        public void Update(Cartera carteraRequest)
        {
            _context.Carteras.Update(carteraRequest);
        }
    }
}
