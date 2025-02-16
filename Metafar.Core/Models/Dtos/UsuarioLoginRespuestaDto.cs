using System.ComponentModel.DataAnnotations;
using Metafar.Core.Models;

namespace Metafar.Core;

public class UsuarioLoginRespuestaDto
{
        public Usuario Usuario { get; set; }
        public string Token { get; set; }
}
