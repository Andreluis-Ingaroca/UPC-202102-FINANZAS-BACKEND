using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Resources
{
    public class ToShowOperacionesResource
    {
        public int Id { get; set; }
        public DateTime FechaDescuento { get; set; }
        public float TasaMonto { get; set; }
        public bool Nominal { get; set; }
        public float TCEACartera { get; set; }

    }
}
