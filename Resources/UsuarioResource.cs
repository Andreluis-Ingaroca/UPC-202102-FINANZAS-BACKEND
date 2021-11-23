using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Resources
{
    public class UsuarioResource
    {
        public int Id { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }
    }
}
