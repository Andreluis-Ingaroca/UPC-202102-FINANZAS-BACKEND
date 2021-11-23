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
    public class CostosOperacionRepository : BaseRepository, ICostosOperacionRepository
    {
        public CostosOperacionRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(CostosOperacion costosOperacion)
        {
            await _context.CostosOperaciones.AddAsync(costosOperacion);
        }

        public async Task AssignCostoOperacion(int costoId, int operacionId, float monto, bool costoInicial, bool porcentaje)
        {
            CostosOperacion costosOperacion = await FindByCostoIdAndOperacionId(costoId,operacionId);
            if (costosOperacion == null)
            {
                costosOperacion = new CostosOperacion { CostoId = costoId, OperacionId = operacionId, Monto=monto, CostoInicial=costoInicial, Porcentaje=porcentaje };
                await AddAsync(costosOperacion);
            }
        }

        public async Task<CostosOperacion> FindByCostoIdAndOperacionId(int costoId, int operacionId)
        {
            return await _context.CostosOperaciones.FindAsync(operacionId,costoId);
        }

        public async Task<IEnumerable<CostosOperacion>> ListAsync()
        {
            return await _context.CostosOperaciones.ToListAsync();
        }

        public async Task<IEnumerable<CostosOperacion>> ListByCostoId(int costoId)
        {
            return await _context.CostosOperaciones.
                Where(co => co.CostoId==costoId)
                .Include(co=> co.Costo)
                .ToListAsync();
        }

        public async Task<IEnumerable<CostosOperacion>> ListByOperacionId(int operacionId)
        {
            return await _context.CostosOperaciones.
                Where(co => co.OperacionId== operacionId)
                .ToListAsync();
        }

        public void Remove(CostosOperacion costosOperacion)
        {
            _context.CostosOperaciones.Remove(costosOperacion);
        }

        public async Task UnassignCostoOperacion(int costoId, int operacionId)
        {
            CostosOperacion costosOperacion = await FindByCostoIdAndOperacionId(costoId, operacionId);
            if (costosOperacion != null)
            {
                Remove(costosOperacion);
            }
        }
    }
}
