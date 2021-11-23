using Finanzas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Domain.Services.Communications
{
    public class CarteraResponse : BaseResponse<Cartera>
    {
        public CarteraResponse(Cartera resource) : base(resource)
        {
        }

        public CarteraResponse(string message) : base(message)
        {
        }
    }
}
