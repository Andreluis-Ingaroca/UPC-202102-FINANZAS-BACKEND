using Finanzas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Domain.Services.Communications
{
    public class AuthenticationResponse
    {
        public int Id { get; set; }
        public string Correo { get; set; }
        public string Token { get; set; }
        public AuthenticationResponse(Usuario usuario, string token)
        {
            Id = usuario.Id;
            Correo = usuario.Correo;
            Token = token;
        }
    }
}
