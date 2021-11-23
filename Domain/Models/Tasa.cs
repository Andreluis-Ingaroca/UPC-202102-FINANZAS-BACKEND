using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Domain.Models
{
    public class Tasa
    {
        public int Id {get; set;}
        public float Monto {get; set;}
        public int PeriodoId { get; set; }
        public Periodo Periodo { get; set; }
        public int PeriodoCapitalizacionId { get; set; }
        public Periodo PeriodoCapitalizacion { get; set; }
        public List<Operacion> Operaciones { get; set; }
        public bool Nominal { get; set; }
    }
}
