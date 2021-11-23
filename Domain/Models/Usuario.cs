using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Finanzas.Domain.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }
        public int PerfilId { get; set; }
        public Perfil Perfil { get; set; }
    }
}
