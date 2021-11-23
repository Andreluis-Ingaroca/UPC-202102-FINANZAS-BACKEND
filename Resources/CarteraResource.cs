using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Resources
{
    public class CarteraResource
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int PerfilId { get; set; }
        public PerfilResource Perfil { get; set; }
        public List<LetraResource> Letras { get; set; }
        public List<OperacionCarteraResource> operacionCarteras { get; set; }

    }
}
