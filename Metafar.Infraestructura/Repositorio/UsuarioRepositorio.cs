using System.IdentityModel.Tokens.Jwt;
using System.Runtime.ExceptionServices;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using Metafar.Core;
using Metafar.Core.Models;
using Metafar.Infraestructura.Data;
using Metafar.Infraestructura.Repositorio.IRepositorio;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using XSystem.Security.Cryptography;
 

namespace Metafar.Infraestructura;

public class UsuarioRepositorio : IUsuarioRepositorio
{
    private readonly ApplicationDbContext _bd;
    private string claveSecreta;

    public UsuarioRepositorio(ApplicationDbContext bd, IConfiguration configuration)
    {
        _bd = bd;
        claveSecreta = configuration.GetValue<string>("AppiSettings:Secreta");
    }

    public async Task<Usuario> Registro(UsuarioRegistroDto usuarioRegistroDto)
    {
       var passwordEncriptado = obtenermd5(usuarioRegistroDto.Password);
       Usuario usuario = new Usuario
       {
           Tarjeta = usuarioRegistroDto.Tarjeta,
           Password = passwordEncriptado,
           Intentos = 3
       };
       _bd.Usuario.Add(usuario);
         await _bd.SaveChangesAsync();
         usuario.Password = passwordEncriptado;
         return usuario;
    }
    public async Task<UsuarioLoginRespuestaDto> Login(UsuarioLoginDto usuarioLoginDto)
    {
        var passwordEncriptado = obtenermd5(usuarioLoginDto.Password);
        var usuario = _bd.Usuario.FirstOrDefault(u => u.Tarjeta == usuarioLoginDto.Tarjeta && u.Password == passwordEncriptado);

        if (usuario == null)
        {
            return new UsuarioLoginRespuestaDto()
            {
                Token = "",
                Usuario = null
            };
            
        }
            var manejadorToken = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(claveSecreta);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Name, usuario.Tarjeta)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = manejadorToken.CreateToken(tokenDescriptor);

            UsuarioLoginRespuestaDto usuarioLoginRespuestaDto = new UsuarioLoginRespuestaDto()
            {
                Token = manejadorToken.WriteToken(token),
                Usuario = usuario
            };

            return usuarioLoginRespuestaDto;

    }


    public string obtenermd5(string valor)
    {
       MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
        byte[] bs = Encoding.UTF8.GetBytes(valor);
        bs = x.ComputeHash(bs);
        string resp = "";
        for (int i = 0; i < bs.Length; i++)
        {
            resp += bs[i].ToString("x2").ToLower();
        }
        return resp;
    }
}
