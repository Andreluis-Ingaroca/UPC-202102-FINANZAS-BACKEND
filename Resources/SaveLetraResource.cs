using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Resources
{
    public class SaveLetraResource
    {
        public float ValorNominal { get; set; }
        public DateTime FechaGiro { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string NombreGirador { get; set; }
    }
}
