using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Resources
{
    public class PerfilResource
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Doi { get; set; }
        public int UsuarioId { get; set; }
        public UsuarioResource Usuario { get; set; }
    }
}
