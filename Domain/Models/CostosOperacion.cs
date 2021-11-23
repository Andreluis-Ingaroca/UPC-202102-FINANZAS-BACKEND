using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Domain.Models
{
    public class CostosOperacion
    {
        public int CostoId { get; set; }
        public Costo Costo { get; set; }
        public int OperacionId { get; set; }
        public Operacion Operacion { get; set; }
        public bool CostoInicial { get; set; }
        public bool Porcentaje { get; set; }
        public float Monto { get; set; }
    }
}
