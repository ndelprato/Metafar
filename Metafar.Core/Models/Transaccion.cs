using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metafar.Core.Models
{
    internal class Transaccion
    {
        [Key]
        public int Id { get; set; }
        public string Operacion { get; set; }
        public string MontoInicial { get; set; }    
        public string MontoFinal { get; set; }
        public string Tarjeta { get; set; }
        public DateTime Fecha { get; set; }

    }
}
