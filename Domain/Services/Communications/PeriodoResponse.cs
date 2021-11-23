using Finanzas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Domain.Services.Communications
{
    public class PeriodoResponse : BaseResponse<Periodo>
    {
        public PeriodoResponse(Periodo resource) : base(resource)
        {
        }

        public PeriodoResponse(string message) : base(message)
        {
        }
    }
}
