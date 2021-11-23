using Finanzas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Domain.Services.Communications
{
    public class PerfilResponse : BaseResponse<Perfil>
    {
        public PerfilResponse(Perfil resource) : base(resource)
        {
        }

        public PerfilResponse(string message) : base(message)
        {
        }
    }
}
