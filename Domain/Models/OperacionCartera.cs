using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Domain.Models
{
    public class OperacionCartera
    {
        public int CarteraId { get; set; }
        public Cartera Cartera { get; set; }
        public int OperacionId { get; set; }
        public Operacion Operacion { get; set; }
        public float ValorRecibidoTotal { get; set; }
        public float TCEACartera { get; set; }
    }
}
