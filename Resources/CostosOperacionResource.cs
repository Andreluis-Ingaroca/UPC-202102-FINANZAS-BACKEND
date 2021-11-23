using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Resources
{
    public class CostosOperacionResource
    {
        public int CostoId { get; set; }
        public CostoResource Costo { get; set; }
        public int OperacionId { get; set; }
        public OperacionResource Operacion { get; set; }
        public bool CostoInicial { get; set; }
        public bool Porcentaje { get; set; }
        public float Monto { get; set; }
    }
}
