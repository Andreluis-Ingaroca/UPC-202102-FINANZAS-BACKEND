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
    public class PeriodoRepository : BaseRepository, IPeriodoRepository
    {
        public PeriodoRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Periodo periodo)
        {
            await _context.Periodos.AddAsync(periodo);
        }

        public async Task<Periodo> FindByIdAsync(int id)
        {
            return await _context.Periodos.FindAsync(id);
        }

        public async Task<IEnumerable<Periodo>> ListAsync()
        {
            return await _context.Periodos.ToListAsync();
        }

        public void Remove(Periodo periodo)
        {
            _context.Periodos.Remove(periodo);
        }

        public void Update(Periodo periodoRequest)
        {
            _context.Periodos.Update(periodoRequest);
        }
    }
}
