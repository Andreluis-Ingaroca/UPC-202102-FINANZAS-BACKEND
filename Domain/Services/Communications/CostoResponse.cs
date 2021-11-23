using Finanzas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Domain.Services.Communications
{
    public class CostoResponse : BaseResponse<Costo>
    {
        public CostoResponse(Costo resource) : base(resource)
        {
        }

        public CostoResponse(string message) : base(message)
        {
        }
    }
}
