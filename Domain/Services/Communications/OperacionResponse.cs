using Finanzas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Domain.Services.Communications
{
    public class OperacionResponse : BaseResponse<Operacion>
    {
        public OperacionResponse(Operacion resource) : base(resource)
        {
        }

        public OperacionResponse(string message) : base(message)
        {
        }
    }
}
