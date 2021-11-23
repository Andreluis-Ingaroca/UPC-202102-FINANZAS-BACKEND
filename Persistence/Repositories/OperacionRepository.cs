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
    public class OperacionRepository : BaseRepository,IOperacionRepository
    {
        public OperacionRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Operacion operacion)
        {
            await _context.Operaciones.AddAsync(operacion);
        }

        public async Task<Operacion> FindByIdAsync(int id)
        {
            return await _context.Operaciones.FindAsync(id);
        }

        public async Task<IEnumerable<Operacion>> ListAsync()
        {
            return await _context.Operaciones.ToListAsync();
        }

        public void Remove(Operacion operacion)
        {
            _context.Operaciones.Remove(operacion);
        }

        public void Update(Operacion operacionRequest)
        {
            _context.Operaciones.Update(operacionRequest);
        }
    }
}
