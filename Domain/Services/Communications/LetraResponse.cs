using Finanzas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Domain.Services.Communications
{
    public class LetraResponse : BaseResponse<Letra>
    {
        public LetraResponse(Letra resource) : base(resource)
        {
        }

        public LetraResponse(string message) : base(message)
        {
        }
    }
}
