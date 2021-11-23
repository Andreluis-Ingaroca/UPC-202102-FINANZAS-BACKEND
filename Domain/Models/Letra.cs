using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Domain.Models
{
    public class Letra
    {
        public int Id { get; set; }
        public int CarteraId { get; set; }
        public Cartera Cartera { get; set; }
        public float ValorNominal { get; set; }
        public DateTime FechaGiro { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string NombreGirador { get; set; }
        public List<OperacionLetra> OperacionLetras { get; set; }
    }
}