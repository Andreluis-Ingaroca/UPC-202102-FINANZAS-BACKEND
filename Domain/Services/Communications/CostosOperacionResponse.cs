using Finanzas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Domain.Services.Communications
{
    public class CostosOperacionResponse : BaseResponse<CostosOperacion>
    {
        public CostosOperacionResponse(CostosOperacion resource) : base(resource)
        {
        }

        public CostosOperacionResponse(string message) : base(message)
        {
        }
    }
}
