using AutoMapper;
using Metafar.Core;
using Metafar.Core.Models;
using Metafar.Infraestructura;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet("{tarjeta}", Name ="GetOperacionesTarjetaPaginado")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<OperacionesDto> GetOperacionesTarjetaPaginado(string tarjeta,[FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var totalOperaciones = _operacionesRepositorio.GetTotalOperaciones();
                
                var listaOperaciones = _operacionesRepositorio.GetOperacionesTarjetaPaginado(tarjeta, pageNumber, pageSize);
                if (listaOperaciones == null || !listaOperaciones.Any())
                {
                    return NotFound($"No se encontraron operaciones para la tarjeta {tarjeta}");
                }
                //var listaOperacionesDto = new List<OperacionesDto>();
                var itemOperaciones = listaOperaciones.Select(x => _mapper.Map<OperacionesDto>(x)).ToList();
                var response = new
                {
                    totalPages = (int)Math.Ceiling(totalOperaciones / (double)pageSize),
                    totalItems = totalOperaciones,
                    pageNumber,
                    pageSize,
                    Items = itemOperaciones
                };
                return Ok(response);
            }
            catch (Exception)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError,"Error al obtener las operaciones");
            }
            
        }
    }
}
