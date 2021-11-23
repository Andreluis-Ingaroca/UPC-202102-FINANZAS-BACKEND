using Finanzas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Domain.Services.Communications
{
    public class OperacionLetraResponse : BaseResponse<OperacionLetra>
    {
        public OperacionLetraResponse(OperacionLetra resource) : base(resource)
        {
        }

        public OperacionLetraResponse(string message) : base(message)
        {
        }
    }
}
