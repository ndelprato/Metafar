using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metafar.Core.Models.Dtos
{
    public class SaldoDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo tarjeta es requerido")]
        public string Tarjeta { get; set; }
         [Required(ErrorMessage = "El campo monto es requerido")]
        public string Monto { get; set; }
        public DateTime Fecha { get; set; }
    }
}
