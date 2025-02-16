using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metafar.Core;
using Metafar.Core.Models;

namespace Metafar.Infraestructura.Repositorio.IRepositorio
{
    public interface IUsuarioRepositorio
    {
       
        Task<UsuarioLoginRespuestaDto> Login(UsuarioLoginDto usuarioLoginDto);
        Task<Usuario> Registro(UsuarioRegistroDto usuarioRegistroDto);
       
    }
}
