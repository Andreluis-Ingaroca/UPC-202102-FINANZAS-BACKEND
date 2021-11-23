using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Resources
{
    public class SaveOperacionResource
    {
        public bool AñoCalendario { get; set; }
        public DateTime FechaDescuento { get; set; }
        public float Retencion { get; set; }
        public bool RetencionPorcentaje { get; set; }
    }
}
