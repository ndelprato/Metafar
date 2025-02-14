using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metafar.Core.Models
{
    public class Saldo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Tarjeta { get; set; }
        [Required]
        public string Monto { get; set; }
        [Required]  
        public DateTime Fecha { get; set; } 
    }
}
