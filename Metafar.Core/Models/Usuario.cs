using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metafar.Core.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string Tarjeta { get; set; }
        public string Password { get; set; }
        public int Intentos { get; set; }



    }
}
