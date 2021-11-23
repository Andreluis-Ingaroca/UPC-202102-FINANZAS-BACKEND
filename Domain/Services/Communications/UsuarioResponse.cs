using Finanzas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Domain.Services.Communications
{
    public class UsuarioResponse : BaseResponse<Usuario>
    {
        public UsuarioResponse(Usuario resource) : base(resource)
        {
        }

        public UsuarioResponse(string message) : base(message)
        {
        }
    }
}
