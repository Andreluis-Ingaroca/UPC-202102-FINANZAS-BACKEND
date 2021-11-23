using Finanzas.Domain.Models;
using Finanzas.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Domain.Services
{
    public interface IPeriodoService
    {
        Task<IEnumerable<Periodo>> ListAsync();
        Task<PeriodoResponse> GetById(int id);
        Task<PeriodoResponse> SaveAsync(Periodo periodo);
        Task<PeriodoResponse> UpdateAsync(int id, Periodo periodoRequest);
        Task<PeriodoResponse> DeleteAsync(int id);
    }
}
