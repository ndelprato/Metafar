using System.ComponentModel.DataAnnotations;

namespace Metafar.Core;

public class OperacionesDto
{
    
        public string Operacion { get; set; }
        public string MontoInicial { get; set; }
        [Required(ErrorMessage = "El campo MontoFinal es requerido")]    
        public string MontoFinal { get; set; }
        [Required(ErrorMessage = "El campo tarjeta es requerido")]
        public string Tarjeta { get; set; }
        public DateTime Fecha { get; set; }

}
