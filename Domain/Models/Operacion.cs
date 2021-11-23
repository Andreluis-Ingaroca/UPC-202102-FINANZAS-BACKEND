using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Domain.Models
{
    public class Operacion
    {
        public int Id { get; set; }
        public int TasaId { get; set; }
        public Tasa Tasa { get; set; }
        public bool AñoCalendario { get; set; }
        public float Retencion { get; set; }
        public bool RetencionPorcentaje { get; set; }
        public DateTime FechaDescuento { get; set; }
        public List<CostosOperacion> CostosOperaciones { get; set; }
        public List<OperacionCartera> OperacionCarteras { get; set; }
        public List<OperacionLetra> OperacionLetras { get; set; }
    }
}
