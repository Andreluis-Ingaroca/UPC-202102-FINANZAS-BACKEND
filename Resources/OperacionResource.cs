using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Resources
{
    public class OperacionResource
    {
        public int Id { get; set; }
        public int TasaId { get; set; }
        public TasaResource Tasa { get; set; }
        public bool AñoCalendario { get; set; }
        public DateTime FechaDescuento { get; set; }
        public float Retencion { get; set; }
        public bool RetencionPorcentaje { get; set; }
    }
}
