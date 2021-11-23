using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Domain.Models
{
    public class Periodo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public List<Tasa> TasasEfectivas { get; set; }
        public List<Tasa> TasasNominales { get; set; }
    }
}
