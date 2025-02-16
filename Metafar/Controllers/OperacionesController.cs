using AutoMapper;
using Metafar.Core;
using Metafar.Core.Models;
using Metafar.Infraestructura;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/operaciones")]
    [ApiController]
    public class OperacionesController : ControllerBase
    {
         private readonly IOperacionesRepositorio _operacionesRepositorio;
        private readonly IMapper _mapper;

        public OperacionesController(IOperacionesRepositorio operacionesRepositorio, IMapper mapper)
        {
            _operacionesRepositorio = operacionesRepositorio;
            _mapper = mapper;
        }

    
    [HttpGet("{tarjeta}", Name ="GetOperacionesTarjeta")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<OperacionesDto> GetOperacionesTarjeta(string tarjeta)
        {
            var listaOperaciones = _operacionesRepositorio.GetOperacionesTarjeta(tarjeta);
            var listaOperacionesDto = new List<OperacionesDto>();
            foreach (var operacion in listaOperaciones)
            {
                listaOperacionesDto.Add(_mapper.Map<OperacionesDto>(operacion));
            }
          
            return Ok(listaOperacionesDto);
        }
        

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult GetOperaciones()
        {
            var listaOperaciones = _operacionesRepositorio.GetOperaciones();
            var listaOperacionesDTO = new List<OperacionesDto>();
            foreach (var lista in listaOperaciones)
            {
                listaOperacionesDTO.Add(_mapper.Map<OperacionesDto>(lista));
            }
            return Ok(listaOperacionesDTO);
        }



        [HttpPost(Name = "AltaOperacion")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult PostOperacion([FromBody]OperacionesDto operacionDto)
        {
            var operacion = _mapper.Map<Operaciones>(operacionDto);
            if (_operacionesRepositorio.GuardarOperacion(operacion))
            {
                return CreatedAtRoute("GetOperacionesTarjeta", new { tarjeta = operacion.Tarjeta }, operacion);
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
