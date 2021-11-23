using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Resources
{
    public class ResultOperacionLetraResource
    {
        public int LetraId { get; set; }
        public float ValorNominal { get; set; }
        public DateTime FechaGiro { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string NombreGirador { get; set; }
        public DateTime FechaDescuento { get; set; }
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
