using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Resources
{
    public class TasaResource
    {
        public int Id { get; set; }
        public float Monto { get; set; }
        public int PeriodoId { get; set; }
        public PeriodoResource Periodo { get; set; }
        public int PeriodoCapitalizacionId { get; set; }
        public PeriodoResource PeriodoCapitalizacion { get; set; }
        public bool Nominal { get; set; }
    }
}
