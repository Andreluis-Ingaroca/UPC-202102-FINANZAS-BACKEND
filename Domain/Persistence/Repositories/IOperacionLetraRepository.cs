using Finanzas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Domain.Persistence.Repositories
{
    public interface IOperacionLetraRepository
    {
        Task<IEnumerable<OperacionLetra>> ListAsync();
        Task<IEnumerable<OperacionLetra>> ListByOperacionId(int operacionId);
        Task<IEnumerable<OperacionLetra>> ListByLetraId(int letraId);
        Task<OperacionLetra> FindByLetraIdAndOperacionId(int operacionId, int letraId);
        Task AddAsync(OperacionLetra operacionLetra);
        void Remove(OperacionLetra operacionLetra);
        Task AssingOperacionLetra(int operacionId, int letraId, float tep, int nDias,float ret, float d,float ci, float cf, float descuento, float valorNeto, float valorEntregado, float valorRecibido, float tcea, float flujo);
        void UnassingOperacionLetra(int operacionId, int letraId);
    }
}
