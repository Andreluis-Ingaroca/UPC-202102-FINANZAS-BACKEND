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
    public class OperacionLetraRepository : BaseRepository, IOperacionLetraRepository
    {
        public OperacionLetraRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(OperacionLetra operacionLetra)
        {
            await _context.OperacionLetras.AddAsync(operacionLetra);
        }

        public async Task AssingOperacionLetra(int operacionId, int letraId, float tep, int nDias, float ret, float d, float ci, float cf, float descuento, float valorNeto, float valorEntregado, float valorRecibido, float tcea, float flujo)
        {
            OperacionLetra operacionLetra = await FindByLetraIdAndOperacionId(operacionId, letraId);
            if (operacionLetra == null)
            {
                operacionLetra = new OperacionLetra { LetraId = letraId, 
                                                      OperacionId = operacionId, 
                                                      D = d, 
                                                      Retencion=ret,
                                                      CostosIniciales = ci,
                                                      CostosFinales = cf,
                                                      Descuento = descuento, 
                                                      NDias = nDias, 
                                                      TCEA = tcea, 
                                                      TEP = tep, 
                                                      ValorEntregado = valorEntregado, 
                                                      ValorNeto = valorNeto, 
                                                      ValorRecibido = valorRecibido,
                                                      Flujo=flujo};
                await AddAsync(operacionLetra);
            }
        }

        public async Task<OperacionLetra> FindByLetraIdAndOperacionId(int operacionId, int letraId)
        {
            return await _context.OperacionLetras.FindAsync(operacionId,letraId);
        }

        public async Task<IEnumerable<OperacionLetra>> ListAsync()
        {
            return await _context.OperacionLetras
                .ToListAsync();
        }

        public async Task<IEnumerable<OperacionLetra>> ListByLetraId(int letraId)
        {
            return await _context.OperacionLetras.
                Where(ol => ol.LetraId==letraId)
                .ToListAsync();
        }

        public async Task<IEnumerable<OperacionLetra>> ListByOperacionId(int operacionId)
        {
            return await _context.OperacionLetras.
                Where(ol => ol.OperacionId== operacionId)
                .ToListAsync();
        }

        public void Remove(OperacionLetra operacionLetra)
        {
            _context.OperacionLetras.Remove(operacionLetra);
        }

        public async void UnassingOperacionLetra(int operacionId, int letraId)
        {
            OperacionLetra operacionLetra = await FindByLetraIdAndOperacionId(operacionId,letraId);
            if (operacionLetra != null)
            {
                Remove(operacionLetra);
            }
        }
    }
}
