using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Resources
{
    public class SaveCostosOperacionResource
    {
        public bool CostoInicial { get; set; }
        public bool Porcentaje { get; set; }
        public float Monto { get; set; }
    }
}
