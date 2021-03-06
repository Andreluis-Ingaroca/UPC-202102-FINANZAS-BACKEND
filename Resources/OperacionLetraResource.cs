using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Resources
{
    public class OperacionLetraResource
    {
        public int LetraId { get; set; }
        public LetraResource Letra { get; set; }
        public int OperacionId { get; set; }
        public OperacionResource Operacion { get; set; }
        public float TEP { get; set; }
        public int NDias { get; set; }
        public float D { get; set; }
        public float Retencion { get; set; }
        public float CostosIniciales { get; set; }
        public float CostosFinales { get; set; }
        public float Descuento { get; set; }
        public float ValorNeto { get; set; }
        public float ValorEntregado { get; set; }
        public float ValorRecibido { get; set; }
        public float Flujo { get; set; }
        public float TCEA { get; set; }
    }
}
