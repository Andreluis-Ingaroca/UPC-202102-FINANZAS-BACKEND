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
    public class OperacionCarteraRepository : BaseRepository, IOperacionCarteraRepository
    {
        public OperacionCarteraRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(OperacionCartera operacionCartera)
        {
            await _context.OperacionCarteras.AddAsync(operacionCartera);
        }

        public async Task AssignOperacionCartera(int operacionId, int carteraId, float valorRecibidoTotal, float tceaCartera)
        {
            OperacionCartera operacionCartera = await FindByOperacionIdAndCarteraId(operacionId,carteraId);
            if (operacionCartera == null)
            {
                operacionCartera = new OperacionCartera { CarteraId = carteraId, OperacionId = operacionId, TCEACartera = tceaCartera, ValorRecibidoTotal = valorRecibidoTotal };
                await AddAsync(operacionCartera);
            }
        }

        public async Task<OperacionCartera> FindByOperacionIdAndCarteraId(int operacionId, int carteraId)
        {
            return await _context.OperacionCarteras.FindAsync(operacionId,carteraId);
        }

        public async Task<IEnumerable<OperacionCartera>> ListAsync()
        {
            return await _context.OperacionCarteras.ToListAsync();
        }

        public async Task<IEnumerable<OperacionCartera>> ListByCarteraId(int carteraId)
        {
            return await _context.OperacionCarteras.
                Where(oc => oc.CarteraId==carteraId)
                .ToListAsync();
        }

        public async Task<IEnumerable<OperacionCartera>> ListByOperacionId(int operacionId)
        {
            return await _context.OperacionCarteras.
                Where(oc => oc.OperacionId == operacionId)
                .ToListAsync();
        }

        public void Remove(OperacionCartera operacionCartera)
        {
            _context.OperacionCarteras.Remove(operacionCartera);
        }

        public async void UnassignOperacionCartera(int operacionId, int carteraId)
        {
            OperacionCartera operacionCartera = await FindByOperacionIdAndCarteraId(operacionId,carteraId);
            if (operacionCartera != null)
            {
                Remove(operacionCartera);
            }
        }
    }
}
