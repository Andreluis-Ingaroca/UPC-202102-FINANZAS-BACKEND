using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Resources
{
    public class OperacionCarteraResource
    {
        public int CarteraId { get; set; }
        public int OperacionId { get; set; }
        public OperacionResource Operacion { get; set; }
        public float ValorRecibidoTotal { get; set; }
        public float TCEACartera { get; set; }
    }
}
