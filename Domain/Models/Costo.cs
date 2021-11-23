using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Domain.Models
{
    public class Costo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<CostosOperacion> CostosOperaciones { get; set; }
    }
}
