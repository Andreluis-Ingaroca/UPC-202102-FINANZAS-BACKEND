using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Domain.Models
{
    public class Perfil
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Doi { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public List<Cartera> Carteras { get; set; }
    }
}
