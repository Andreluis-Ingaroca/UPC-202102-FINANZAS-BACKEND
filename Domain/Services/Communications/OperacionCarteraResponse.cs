using Finanzas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Domain.Services.Communications
{
    public class OperacionCarteraResponse : BaseResponse<OperacionCartera>
    {
        public OperacionCarteraResponse(OperacionCartera resource) : base(resource)
        {
        }

        public OperacionCarteraResponse(string message) : base(message)
        {
        }
    }
}
