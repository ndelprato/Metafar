using System.Net;
using AutoMapper;
using Metafar.Core;
using Metafar.Infraestructura.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IMapper _mapper;
        protected RespuestaApi _respuestaApi;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio, IMapper mapper)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _mapper = mapper;
            this._respuestaApi = new();
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
         
        public async Task<ActionResult> Login([FromBody] UsuarioLoginDto usuarioLoginDto)
        {
            var respuestaLogin =  await _usuarioRepositorio.Login(usuarioLoginDto);
            if (respuestaLogin == null || string.IsNullOrEmpty(respuestaLogin.Token)) 
            {
                _respuestaApi.IsSuccess = false;
                _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                _respuestaApi.ErrorMessages.Add("El nombre de usuario o la contraseña son incorrectos");
                return BadRequest(_respuestaApi);
            }
            _respuestaApi.IsSuccess = true;
            _respuestaApi.StatusCode = HttpStatusCode.OK;
            _respuestaApi.Result = respuestaLogin;
            return Ok(_respuestaApi);
            
        }
      


    }
}
