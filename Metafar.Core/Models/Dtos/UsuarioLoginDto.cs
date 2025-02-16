using System.ComponentModel.DataAnnotations;

namespace Metafar.Core;

public class UsuarioLoginDto
{
        [Required(ErrorMessage = "El campo tarjeta es requerido")]
        public string Tarjeta { get; set; }
        [Required(ErrorMessage = "El campo password es requerido")]
        public string Password { get; set; }
        public int Intentos { get; set; }

}
