using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Domain.Models
{
    public class Cartera
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int PerfilId { get; set; }
        public Perfil Perfil { get; set; }
        public List<Letra> Letras { get; set; }
        public List<OperacionCartera> operacionCarteras { get; set; }
    }
}
