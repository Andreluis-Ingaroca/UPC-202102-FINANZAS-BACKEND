using Finanzas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Domain.Services.Communications
{
    public class TasaResponse : BaseResponse<Tasa>
    {
        public TasaResponse(Tasa resource) : base(resource)
        {
        }

        public TasaResponse(string message) : base(message)
        {
        }
    }
}
